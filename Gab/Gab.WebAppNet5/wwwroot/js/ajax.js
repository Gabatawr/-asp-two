console.log("Ajax Ready");

// Helpers
const cleanTable = () =>
  document.querySelector(".table")
    .querySelector("tbody").textContent = "";

const createTable = json => {
  cleanTable();
  json.$values.forEach(t => {
    let td = [
      document.createElement("td"),
      document.createElement("td")
    ];

    td[0].textContent = t.name;
    ["Edit", "Details", "Delete"].forEach((action) => {
      let link = document.createElement("a");

      if (action === "Edit")
      {
        link.href = "#";
        $(link).on("click", () => {
          $("#TeacherModal").modal("show");
          document.getElementById("Name").value = t.name;
          document.getElementById("TeacherSaveBtn")
            .onclick = async () => { await fetchPostPutHelper("PUT", t.id); }
        });
      }
      else link.href = `/Teachers/${action}/${t.id}`;

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
    (match) => {
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

const fetchPostPutHelper = async (method, id = "") => {
  try {
    await fetch(`/api/Teachers/${id}`,
      {
        method: method,
        headers:
        {
          'Content-Type': "application/json"
        },
        body: JSON.stringify(
        {
          Id: id === "" ? "00000000-0000-0000-0000-000000000000" : id,
          Name: document.getElementById("Name").value
        })
      });
    document.getElementById("Name").value = "";
    getTeachers();
  }
  catch (ex) {
    console.log(`Error: ${ex.message}`);
    console.log(`Response: ${ex.response}`);
  }
  finally {
    document.getElementById("TeacherModalClose").click();
  }
}

// GET: Teachers
const getTeachers = async () => {
  try {
    const response = await fetch("/api/Teachers");
    let json = await response.json();

    createTable(json);
    document.querySelector("#json").innerHTML = jsonHighlights(json);
  }
  catch (ex) {
    console.log(`Error: ${ex.message}`);
    console.log(`Response: ${ex.response}`);
  }
};

getTeachers();

// POST: Teacher
document.getElementById("TeacherCreateBtn")
  .addEventListener("click", event =>
  {
    document.getElementById("TeacherSaveBtn")
      .onclick = async () => { await fetchPostPutHelper("POST"); }
  });
