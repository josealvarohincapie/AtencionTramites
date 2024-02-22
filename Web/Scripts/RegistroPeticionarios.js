function AbrirDocumento(url) {

    document.getElementById("menu0").className = "tab-pane fade";
    document.getElementById("menu1").className = "tab-pane fade in active";
    document.getElementById("menu2").className = "tab-pane fade";
    document.getElementById("menu3").className = "tab-pane fade";

    document.getElementById("liMenu0").className = "";
    document.getElementById("liMenu1").className = "active";
    document.getElementById("liMenu2").className = "";
    document.getElementById("liMenu3").className = "";

    window.open(url);
    return false;
}

function agregarDerecho() {
    alert("qwrr");
    var table = document.getElementById("tablaDerechos").getElementsByTagName('tbody')[0];
    var newRow = table.insertRow(table.rows.length);

    // Celda para el input de derechos
    var cell1 = newRow.insertCell(0);
    var derechoInput = document.createElement("input");
    derechoInput.type = "text";
    derechoInput.className = "form-control";
    derechoInput.name = "derecho[]"; // Usa corchetes para indicar que es un array
    cell1.appendChild(derechoInput);

    // Celda para los botones
    var cell2 = newRow.insertCell(1);
    // Botón Eliminar
    var deleteButton = document.createElement("button");
    deleteButton.innerHTML = "";
    deleteButton.className = "btn btn-sm fa fa-close";
    deleteButton.onclick = function () {
        var row = this.parentNode.parentNode;
        row.parentNode.removeChild(row);
    };
    cell2.appendChild(deleteButton);
    // Botón Editar
    var editButton = document.createElement("button");
    editButton.innerHTML = "";
    editButton.className = "btn btn-primary btn-sm mr-2 btn-success btn-medium btn-margin-catalogo btn-file fa fa-search"; // Asumiendo que quieres un margen a la derecha (mr-2)
    editButton.onclick = function () {
        // Funcionalidad de edición aquí
        $('#divmodalCatalogo').modal('show');
    };
    cell2.appendChild(editButton);


}
function guardarDerechos() {
    var derechos = [];
    document.querySelectorAll('[name="derecho[]"]').forEach(function (element) {
        derechos.push(element.value);
    });
    document.getElementById('<%= hiddenDerechos.ClientID %>').value = JSON.stringify(derechos);

}