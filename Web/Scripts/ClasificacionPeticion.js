function imprimirPagina() {
    window.print();
    return false;
}

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


function limpiarCampo(textBoxClientId) {
    document.getElementById(textBoxClientId).value = '';
}
function imprimirPagina() {
    window.print();
}

function validarFormulario() {
    // Forzar la validación de ASP.NET
    ValidatorValidate(document.getElementById('<%=RequiredFieldValidator1.ClientID%>'));
    ValidatorValidate(document.getElementById('<%=RequiredFieldValidator2.ClientID%>'));
    ValidatorValidate(document.getElementById('<%=RequiredFieldValidator3.ClientID%>'));
    ValidatorValidate(document.getElementById('<%=RequiredFieldValidator4.ClientID%>'));

    ValidatorValidate(document.getElementById('<%=RequiredFieldValidator6.ClientID%>'));
    ValidatorValidate(document.getElementById('<%=RequiredFieldValidator7.ClientID%>'));

    ValidatorValidate(document.getElementById('<%=RequiredFieldValidator9.ClientID%>'));
    ValidatorValidate(document.getElementById('<%=RequiredFieldValidator10.ClientID%>'));
    ValidatorValidate(document.getElementById('<%=RequiredFieldValidator11.ClientID%>'));
    ValidatorValidate(document.getElementById('<%=RequiredFieldValidator12.ClientID%>'));
    ValidatorValidate(document.getElementById('<%=RequiredFieldValidator13.ClientID%>'));
    //falta el 14, derechos tabla
    ValidatorValidate(document.getElementById('<%=RequiredFieldValidator15.ClientID%>'));
    ValidatorValidate(document.getElementById('<%=RequiredFieldValidator16.ClientID%>'));
    ValidatorValidate(document.getElementById('<%=RequiredFieldValidator17.ClientID%>'));
    ValidatorValidate(document.getElementById('<%=RequiredFieldValidator18.ClientID%>'));
    ValidatorValidate(document.getElementById('<%=RequiredFieldValidator19.ClientID%>'));


    // Verificar si el validador está válido

    var esValidoList = [];
    esValidoList[0] = document.getElementById('<%=RequiredFieldValidator1.ClientID%>').isvalid;
    esValidoList[1] = document.getElementById('<%=RequiredFieldValidator2.ClientID%>').isvalid;
    esValidoList[2] = document.getElementById('<%=RequiredFieldValidator3.ClientID%>').isvalid;
    esValidoList[3] = document.getElementById('<%=RequiredFieldValidator4.ClientID%>').isvalid;
    esValidoList[4] = document.getElementById('<%=RequiredFieldValidator4.ClientID%>').isvalid;
    esValidoList[5] = document.getElementById('<%=RequiredFieldValidator6.ClientID%>').isvalid;
    esValidoList[6] = document.getElementById('<%=RequiredFieldValidator7.ClientID%>').isvalid;
    esValidoList[6] = document.getElementById('<%=RequiredFieldValidator7.ClientID%>').isvalid;
    esValidoList[8] = document.getElementById('<%=RequiredFieldValidator9.ClientID%>').isvalid;
    esValidoList[9] = document.getElementById('<%=RequiredFieldValidator10.ClientID%>').isvalid;
    esValidoList[10] = document.getElementById('<%=RequiredFieldValidator11.ClientID%>').isvalid;
    esValidoList[11] = document.getElementById('<%=RequiredFieldValidator12.ClientID%>').isvalid;
    esValidoList[12] = document.getElementById('<%=RequiredFieldValidator13.ClientID%>').isvalid;
    //falta el 14, derechos tabla
    esValidoList[14] = document.getElementById('<%=RequiredFieldValidator15.ClientID%>').isvalid;
    esValidoList[15] = document.getElementById('<%=RequiredFieldValidator16.ClientID%>').isvalid;
    esValidoList[16] = document.getElementById('<%=RequiredFieldValidator17.ClientID%>').isvalid;
    esValidoList[17] = document.getElementById('<%=RequiredFieldValidator18.ClientID%>').isvalid;
    esValidoList[18] = document.getElementById('<%=RequiredFieldValidator19.ClientID%>').isvalid;
    var salida = true;
    var salida1 = true;
    var tabla = document.getElementById('tablaDerechos');
    var filas = tabla.getElementsByTagName('tr');
    for (i = 0; i < esValidoList.length; i++) {
        if (esValidoList[i] == false) {
            salida = false;
            break;
        }

    }

    var tabla = document.getElementById('tablaDerechos');
    var filas = tabla.getElementsByTagName('tr');
    if (filas.length > 1) {
        salida1 = true;
    } else {
        alert('La tabla Derechos no puede estar vacía.');
        salida1 = false;
    }
    if (salida == false || salida1 == false) {
        return false
    } else {
        return true
    }
}

function agregarDerecho() {
    $('#liMenu1').hide();
    //$('#liMenu1').show();
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
document.addEventListener("DOMContentLoaded", function () {
    var checkbox = document.getElementById('<%= chkEsAnonimo.ClientID %>');
    checkbox.addEventListener('click', function (e) {
        e.preventDefault();
    });
});
$(document).ready(function () {
    $('#<%= rblGrupoEtnico.ClientID %> input[type=radio]').click(function (e) {
        e.preventDefault();
    });
});
$(document).ready(function () {

    toggleConclusionAsesoria();


    $('input[name$="respuestaEscrita"]').change(function () {
        toggleConclusionAsesoria();
    });

    function toggleConclusionAsesoria() {

        if ($('#<%= respuestaEscritaNo.ClientID %>').is(':checked')) {
            $('#divConclusionAsesoria').show();
        } else {
            $('#divConclusionAsesoria').hide();
        }
    }
});
window.onload = function () {

    var contactoCheckbox = document.getElementById('<%= chkContacto.ClientID %>');
    var correoDiv = document.getElementById('divCorreo');
    var telefonoDiv = document.getElementById('divTelefono');
    var direccionDiv = document.getElementById('divDireccion');

    toggleContacto(contactoCheckbox.checked);

    contactoCheckbox.addEventListener('change', function () {
        toggleContacto(this.checked);
    });

    function toggleContacto(show) {

        correoDiv.style.display = show ? 'block' : 'none';
        telefonoDiv.style.display = show ? 'block' : 'none';
        direccionDiv.style.display = show ? 'block' : 'none';
    }
}