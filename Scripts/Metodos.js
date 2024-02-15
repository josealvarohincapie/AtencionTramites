function validarEnviar() {

    var str = '';
    if ($('#ddlArea').val() == 0) str += '- Área\n';
    //if ($('#tbFuncionarioRespSol').val().trim() == '') str += '- Funcionario responsable de la solicitud\n';
    if ($('#ddlTipoProducto').val() == 0) str += '- Tipo de Producto\n';
    if ($('#ddlDivisa').val() == 0) str += '- Moneda\n';
    if ($('#tbValorDivisa').val() == '' || $('#tbValorDivisa').val() == 0) str += '- Monto de crédito\n';
    if ($("input[name='rblTipoPersona']:checked").val() == undefined) str += '- Tipo persona\n';

    var val = $('#rblClienteDefinido input:checked').val() * 1;
    if (val != 0) {
        if ($('#ddlTipoIdentificacion').val() == 0) str += '- Tipo de Identificación\n';
        if ($('#tbIdentificacion').val().trim() == '' || $('#tbIdentificacion').val().trim() == 0) str += '- Número de Identificación\n';
        if ($('#tbNombre').val().trim() == '') str += '- Razón Social\n';
        if ($('#ddlPais').val() == 0) str += '- Pais\n';
        if ($('#ddlCiudad').val() == 0) str += '- Ciudad\n';
        if ($('#tbCorreo').val().trim() == 0) str += '- Correo Electronico\n';
        if ($('#tbDireccion').val().trim() == 0) str += '- Dirección\n';
        if ($('#tbTelefono').val().trim() == 0) str += '- Telefono\n';
    }

    if (!ValidaInformacionOCU()) str += '- Información relevante para OCU\n';

    if (str.trim() != '') {
        alert('Los siguientes campos son requeridos:\n' + str);
        return (false);
    }

    if (!ValidarDocumentacion()) {
        alert('Debe registrar todos los documentos y fechas de emisión');
        return false;
    }

    if ($('#SoloLectura').val() == "1") {
        if (!ValidarExisteComentario()) {
            alert('Debe ingresar una observación');
            return false;
        }
    }

    return (true);
}

function ValidaInformacionOCU() {
    var chkList = document.getElementById('cblTipoCoincidencia');
    if (chkList) {
        var chkInputs = chkList.getElementsByTagName("input");
        for (var i = 0; i < chkInputs.length; i++) {
            if (chkInputs[i].checked) {
                return true;
            }
        }
    }
    else
        return true;
    return false;
}

function isEmail(email) {
    var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    return regex.test(email);
}

function addCommas(x) {
    x = x.replace(/,/g, "") * 1;
    if(x > 0){
        var parts = x.toString().split(".");
        parts[0] = parts[0].replace(/\B(?=(\d{3})+(?!\d))/g, ",");
        return parts.join(".");
    }
    else
        return 0;
}

function validacionJustificacion() {

    $("#btGuardarNota").prop("disabled", true);
    $(document).on("keyup", "#tbObservacion", function () {
        var longitud = $("#tbObservacion").val().length;
        if (longitud < 26) {
            $("#lblValidacionNota").text("Debe ingresar al menos 26 caracteres.");
            $("#tbObservacion").addClass("error");
            $("#btGuardarNota").prop("disabled", true);
        } else {
            $("#lblValidacionNota").text("");
            $("#tbObservacion").removeClass("error");
            $("#btGuardarNota").prop("disabled", false);
        }
    });
}

function limpiarNota() {
    $("#tbObservacion").val("");
    $("#lblValidacionNota").text("");
    $("#tbObservacion").removeClass("error");
    $("#btGuardarNota").prop("disabled", true);
}

function AbrirReporteCentinela(url) {
    if (url == "")
        alert('No hay resultados para la consulta');
    else
        window.open(url, "_blank", "");
    return false;
}

function ValidarExisteComentario() {
    var etapa = $("#lbEtapa").text();
    var observacion = $("#tbUltimaObservacion").val();
    if (etapa == "Solicitar Credito" || etapa == "Correspondencia Recibida" || etapa == "Preparar Presentacion" || etapa == "Analizar Comite Externo" || observacion != "")
        return true;
    else
        return false;
}

function ValidarClienteDefinido() {
    var value = $('#rblClienteDefinido input:checked').val()*1;
    if (value == 1) {
        $("#divInformacionDeudor").show();
        $("#divReportesCentinela").show();
    }
    else if (value == 0) {
        $("#divInformacionDeudor").hide();
        $("#divReportesCentinela").hide();
    }
    else {
        $("#divInformacionDeudor").show();
        $("#divReportesCentinela").show();
    }
}