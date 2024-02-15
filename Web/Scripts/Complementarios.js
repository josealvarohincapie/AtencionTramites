
function ValidarComplementario() {
    var str = '';
    if ($('#ddlComplementarioTipoComplementario').val() == 0) str += '- Tipo de Complementario\n';
    if ($('#ddlComplementarioTipoIdentificacion').val() == 0) str += '- Tipo de Identificación\n';
    if ($('#tbComplementarioIdentificacion').val().trim() == '' || $('#tbComplementarioIdentificacion').val().trim() == 0) str += '- Número de Identificación\n';
    if ($('#tbComplementarioNombre').val().trim() == '') str += '- Nombre\n';
    if ($('#ddlComplementarioPais').val() == 0) str += '- País\n';
    if ($('#ddlComplementarioCiudad').val() == 0) str += '- Ciudad\n';
    if ($('#tbComplementarioCorreo').val().trim() == '') str += '- Correo electrónico\n';
    if ($('#tbComplementarioDireccion').val().trim() == '') str += '- Dirección\n';
    if ($('#tbComplementarioTelefono').val().trim() == '') str += '- Teléfono\n';
    if ($('#ddlComplementarioActividad').val() == 0) str += '- Actividad económica\n';
    
    if (str != '') {
        alert('Los siguientes campos son requeridos:\n' + str);
        return (false);
    }

    if (!ValidarDocComplementario()) {
        alert('Debe registrar todos los documentos y fechas de emisión');
        return false;
    }
    
    return true;
}

function AgregarComplementario() {
    $("#hfComplementarioNew").val('1');
    $('#ddlComplementarioTipoComplementario').val(0);
    $('#ddlComplementarioTipoIdentificacion').val(0);
    $('#tbComplementarioIdentificacion').val('');
    $('#tbComplementarioNombre').val('');
    $('#ddlComplementarioPais').val(0);
    $('#ddlComplementarioCiudad').val(0);
    $('#tbComplementarioCorreo').val('');
    $('#tbComplementarioDireccion').val('');
    $('#tbComplementarioTelefono').val('');
    $('#ddlComplementarioActividad').val(0);
    $('#modal-complementario').modal('show');
}

function ValidarComplementarios() {

    if ($("#gvComplementarios tr").length == 0) {
        return false;
    }
    else {
        return true;
    }
}