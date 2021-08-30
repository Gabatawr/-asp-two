const createTeacherList = async () =>
{
  let selector = document.getElementById("TeacherIdList");
  let json;

  try {
    const response = await fetch("/api/Teachers");
    json = await response.json();
  } catch (ex) {
    console.log(`Error: ${ex.message}`);
    console.log(`Response: ${ex.response}`);
    return;
  }

  json.$values.forEach(teacher => {
    let option = document.createElement("option");
    option.textContent = teacher.name;
    option.value = teacher.id;
    selector.appendChild(option);
  });
}

const createTable = json => {
  cleanTable();
  json.$values.forEach(entity => {
    let td = [
      document.createElement("td"),
      document.createElement("td"),
      document.createElement("td")
    ];

    td[0].textContent = entity.name;
    td[1].textContent = entity.teacher.name;

    ["Edit", "Details", "Delete"].forEach((action) => {
      let link = document.createElement("a");
      link.textContent = action;
      link.href = "#";

      // PUT: Groups
      if (action === "Edit")
        $(link).on("click", () => {
          $("#Modal").modal("show");

          document.getElementById("Name").value = entity.name;
          createTeacherList().then(() => {
            let selector = document.getElementById("TeacherIdList");
            for (let i = 0; i < selector.options.length; i++) {
              if (entity.teacherId === selector.options[i].value) {
                selector.options[i].selected = true;
                break;
              }
            }
          });

          document.getElementById("ModalSaveBtn")
            .onclick = async () => { await flexFetch("Groups", "PUT", entity.id); }
        });

      // DELETE: Groups
      else if (action === "Delete")
        $(link).on("click", () => {
          flexFetch("Groups", "DELETE", entity.id);
        });
      else link.href = `/Groups/${action}/${entity.id}`;

      if (td[2].childNodes.length > 0)
        td[2].appendChild(document.createTextNode(" | "));
      td[2].appendChild(link);
    });

    let tr = document.createElement("tr");
    td.forEach(x => tr.appendChild(x));

    document.querySelector(".table")
      .querySelector("tbody")
      .appendChild(tr);
  });
}

const createBody = (id) => {
  let selector = document.getElementById("TeacherIdList");
  let teacherId = selector.selectedIndex === -1
    ? "00000000-0000-0000-0000-000000000000"
    : selector.options[selector.selectedIndex].value;

  return JSON.stringify(
    {
      Id: id === "" ? "00000000-0000-0000-0000-000000000000" : id,
      TeacherId: teacherId,
      Name: document.getElementById("Name").value
    });
}

$('#Modal').on('hide.bs.modal', (e) => {
  document.getElementById("Name").value = "";
  document.getElementById("TeacherIdList").textContent = "";
});

GetEntity("Groups");

// POST
document.getElementById("NewEntityBtn")
  .addEventListener("click", event => {
    createTeacherList();
    document.getElementById("ModalSaveBtn")
      .onclick = async () => { await flexFetch("Groups", "POST"); }
  });