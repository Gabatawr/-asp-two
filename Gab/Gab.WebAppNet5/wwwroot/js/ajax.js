console.log("Ajax Ready");

const createTable = json =>
{
  json.$values.forEach(t => {
    let td = [
      document.createElement("td"),
      document.createElement("td")
    ];

    td[0].textContent = t.name;
    ["Edit", "Details", "Delete"].forEach((action) => {
      let link = document.createElement("a");
      link.href = `/Teachers/${action}/${t.id}`;
      link.textContent = action;

      if (td[1].childNodes.length > 0)
        td[1].appendChild(document.createTextNode(" | "));
      td[1].appendChild(link);
    });

    let tr = document.createElement("tr");
    td.forEach(x => tr.appendChild(x));

    document.querySelector(".table")
      .querySelector("tbody")
      .appendChild(tr);
  });
}

const jsonHighlights = json =>
  JSON.stringify(json, undefined, 2)
    .replace(/&/g, "&amp;")
    .replace(/</g, "&lt;")
    .replace(/>/g, "&gt;")
    .replace(/("(\\u[a-zA-Z0-9]{2}|\\[^u]|[^\\"])*"(\s*:)?|\b(true|false|null)\b|-?\d+(?:\.\d*)?(?:[eE][+\-]?\d+)?)/g,
      (match) =>
      {
        let cls = "";
        if (/^"/.test(match)) {
          if (/:$/.test(match))
            cls = "key";
          else if (/[0-9a-f]{8}-[0-9a-f]{4}-[1-5][0-9a-f]{3}-[89ab][0-9a-f]{3}-[0-9a-f]{12}/i.test(match))
            cls = "guid";
          else if (/^"\d*"/.test(match))
            cls = "number";
          else cls = "string";
        }
        else if (/true|false/.test(match))
          cls = "boolean";
        else if (/null/.test(match))
          cls = "null";

        return `<span class="${cls}">${match}</span>`;
      });

(async () =>
{
  try
  {
    const response = await fetch("/api/Teachers");
    let json = await response.json();

    createTable(json);
    document.querySelector("#json").innerHTML = jsonHighlights(json);
  }
  catch (ex)
  {
    console.log(`Error: ${ex.message}`);
    console.log(`Response: ${ex.response}`);
  }
})();
