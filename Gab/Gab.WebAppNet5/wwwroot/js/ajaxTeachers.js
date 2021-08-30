const createTable = json => {
  cleanTable();
  json.$values.forEach(entity => {
    let td = [
      document.createElement("td"),
      document.createElement("td")
    ];

    td[0].textContent = entity.name;
    ["Edit", "Details", "Delete"].forEach((action) => {
      let link = document.createElement("a");
      link.textContent = action;
      link.href = "#";

      // PUT: Teachers
      if (action === "Edit")
        $(link).on("click", () => {
          $("#Modal").modal("show");
          document.getElementById("Name").value = entity.name;
          document.getElementById("ModalSaveBtn")
            .onclick = async () => { await flexFetch("Teachers", "PUT", entity.id); }
        });
      // DELETE: Teachers
      else if (action === "Delete")
        $(link).on("click", () => {
          flexFetch("Teachers", "DELETE", entity.id);
        });
      else link.href = `/Teachers/${action}/${entity.id}`;

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

const createBody = (id) => {
  return JSON.stringify(
  {
    Id: id === "" ? "00000000-0000-0000-0000-000000000000" : id,
    Name: document.getElementById("Name").value
  });
}

$('#Modal').on('hide.bs.modal', (e) => {
  document.getElementById("Name").value = "";
});

GetEntity("Teachers");

// POST
document.getElementById("NewEntityBtn")
  .addEventListener("click", event => {
    document.getElementById("ModalSaveBtn")
      .onclick = async () => { await flexFetch("Teachers", "POST"); }
  });