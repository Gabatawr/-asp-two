// Helpers
const cleanTable = () =>
  document.querySelector(".table")
    .querySelector("tbody").textContent = "";

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

const flexFetch = async (entity, method, id = "") => {
  try {
    await fetch(`/api/${entity}/${id}`,
      {
        method: method,
        headers:
        {
          'Content-Type': "application/json"
        },
        body: createBody(id)
      });
    GetEntity(entity);
  }
  catch (ex) {
    console.log(`Error: ${ex.message}`);
    console.log(`Response: ${ex.response}`);
  }
  finally {
    document.getElementById("ModalCloseBtn").click();
  }
}

// GET
const GetEntity = async (entity) => {
  try {
    const response = await fetch(`/api/${entity}`);
    let json = await response.json();

    createTable(json);
    document.querySelector("#json").innerHTML = jsonHighlights(json);
  }
  catch (ex) {
    console.log(`Error: ${ex.message}`);
    console.log(`Response: ${ex.response}`);
  }
};

