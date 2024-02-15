
function ShowDocumentacion() {
    
    /*if ($("#SoloLectura").val() == "1") {
        $("#gvDocumentacion .fecha-emision").attr('readonly', true);
        $("#gvDocumentacion .observaciones").attr('readonly', true);
        $("#gvDocumentacion .button-adjuntar").hide();
        $("#gvDocumentacionComplementario .fecha-emision").attr('readonly', true);
        $("#gvDocumentacionComplementario .observaciones").attr('readonly', true);
        $("#gvDocumentacionComplementario .button-adjuntar").hide();
    }
    else {
    */

        $("#gvDocumentacion .fecha-emision").datepicker({
            dateFormat: "dd/mm/yy",
            showOtherMonths: true,
            changeMonth: true,
            changeYear: true,
            firstDay: 7,
            showAnim: "slideDown",
            autoSize: true,
            maxDate: 0
        });

        $("#gvDocumentacion .fecha-emision").keypress(function() { return false; });
        $("#gvDocumentacion .observaciones").removeAttr('readonly', true);

        $("#gvDocumentacionComplementario .fecha-emision").datepicker({
            dateFormat: "dd/mm/yy",
            showOtherMonths: true,
            changeMonth: true,
            changeYear: true,
            firstDay: 7,
            showAnim: "slideDown",
            autoSize: true,
            maxDate: 0
        });

        $("#gvDocumentacionComplementario .fecha-emision").keypress(function () { return false; });
        $("#gvDocumentacionComplementario .observaciones").removeAttr('readonly', true);
    
}

function ValidarDocumentacion() {

    if ($("#gvDocumentacion input[type='checkbox'][data-opcional='0']:not(:checked)").length > 0) {
        return false;
    }
    else {
        var res = true;
        $("#gvDocumentacion input[type='checkbox']:checked").each(function () {
            if ($(this).parent().parent().parent().find(".fecha-emision").val().trim() == "") {
                res = false;
            }
        });
        return res;
    }
    return true;

}

function ValidarDocComplementario() {

    if ($("#gvDocumentacionComplementario input[type='checkbox'][data-opcional='0']:not(:checked)").length > 0) {
        return false;
    }
    else {
        var res = true;
        $("#gvDocumentacionComplementario input[type='checkbox']:checked").each(function () {
            if ($(this).parent().parent().parent().find(".fecha-emision").val().trim() == "") {
                res = false;
            }
        });
        return res;
    }
    return true;

}

function AdjuntarDocumentacion(obj, avalista, codigo, registro) {

    /*
    if ($(obj).parent().find(".fecha-emision").val().trim() == "") {
        alert('Seleccione la fecha de emisión');
        return false;
    }
    */
    $("#hfCtrlFecha").val($(obj).parent().find(".fecha-emision").attr('id'));

    if (avalista) {
        $("#hfDocumentacionCodigo").val('');
        $("#hfDocumentacionCodigoComplementario").val(codigo);
        $("#hfDocumentacionRegistro").val('');
        $("#hfDocumentacionRegistroComplementario").val(registro);
    }
    else {
        $("#hfDocumentacionCodigo").val(codigo);
        $("#hfDocumentacionCodigoComplementario").val('');
        $("#hfDocumentacionRegistro").val(registro);
        $("#hfDocumentacionRegistroComplementario").val('');
    }
    $("#fuDocumentacion").click();
}

function EliminarDocumentacion(avalista, codigo, registro) {
    if (avalista) {
        $("#hfDocumentacionCodigo").val('');
        $("#hfDocumentacionCodigoComplementario").val(codigo);
        $("#hfDocumentacionRegistro").val('');
        $("#hfDocumentacionRegistroComplementario").val(registro);
    }
    else {
        $("#hfDocumentacionCodigo").val(codigo);
        $("#hfDocumentacionCodigoComplementario").val('');
        $("#hfDocumentacionRegistro").val(registro);
        $("#hfDocumentacionRegistroComplementario").val('');
    }
    $("#btEliminarDocumento").click();
}

function VerDocumentacion(url) {
    url = "../Documentacion.ashx?u=" + encodeURIComponent(url);
    window.open(url, "_blank", "");
}

function UploadDocumentacion() {

    var files = $("#fuDocumentacion").get(0).files;

    if (files.length != 1) {
        alert("Debe adjuntar un único archivo");
        return;
    }

    if ($("#hfExtensionesValidas").val().split(",").indexOf(files[0].name.split('.').pop()) == -1) {
        $("#fuDocumentacion").val("");
        alert("El tipo de archivo no es soportado.");
        return;
    }

    __doPostBack('btDocumentacion', null);
}

function VerAnexo(token) {

    var url = "../AnexoCR.ashx?t=" + encodeURIComponent(token);
    window.open(url, "_blank", "");
    return false;
}

var _IdComunicacionRecibida;
var _ComunicacionRecibida;
var _Proceso;
var _Incidente;
function DevolverCorrespondenciaClick(control) {

    _IdComunicacionRecibida = $(control).data("fileid");
    _ComunicacionRecibida = $(control).closest(".CR-item");
    var numeroRadicado = $(".CR-NumeroRadicado", _ComunicacionRecibida).text();
    numeroRadicado = numeroRadicado.substring(1, numeroRadicado.length - 1);
    var temp = prompt("La correspondencia No. " + numeroRadicado + " será devuelta a gestión documental. Ingrese sus comentarios.");
    if (temp != null && temp != "")
        DevolverCorrespondencia(temp);
}

function AsociarCorrespondenciaClick(control) {

    _IdComunicacionRecibida = $(control).data("fileid");
    _ComunicacionRecibida = $(control).closest(".CR-item");
    var numeroRadicado = $(".CR-NumeroRadicado", _ComunicacionRecibida).text();
    numeroRadicado = numeroRadicado.substring(1, numeroRadicado.length - 1);
    var temp = prompt("Ingrese el número del incidente al cual asociar la correspondencia No. " + numeroRadicado + ".");
    if (temp != null && temp != "")
        BuscarIncidente(temp);
}

function DevolverCorrespondencia(comentarios) {

    var solicitud = {
        ComunicacionRecibida: _IdComunicacionRecibida,
        User: $("#lbUsuario").text().trim(),
        Comentarios: comentarios
    };

    $.ajax({
        type: 'POST',
        url: "RegistroSolicitud.aspx/DevolverCorrespondencia",
        cache: false,
        data: JSON.stringify(solicitud),
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: function (data) {

            if (data.d != "") {
                alert(data.d);
            }
            else {
                ActualizarCorrespondencia();
            }
        }
    }).fail(function (jqxhr, textStatus, error) {
        alert("Ocurrió un error al procesar la solicitud.");
    });
}

function BuscarIncidente(incidente) {

    var solicitud = {
        Incidente: incidente
    };

    $.ajax({
        type: 'POST',
        url: "RegistroSolicitud.aspx/BuscarIncidente",
        cache: false,
        data: JSON.stringify(solicitud),
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: function (data) {

            if (data == null || data.d == null) {
                alert("No se encontró el incidente indicado.");
            }
            else {
                _Proceso = data.d.Proceso;
                _Incidente = data.d.Incidente;
                var temp = confirm("La correspondencia será asociada al incidente: " + _Proceso + " " + _Incidente + " - " + data.d.Resumen + ".");
                if (temp == true)
                    AsociarCorrespondencia();
            }
        }
    }).fail(function (jqxhr, textStatus, error) {
        alert("Ocurrió un error al procesar la solicitud.");
    });
}

function AsociarCorrespondencia() {

    var solicitud = {
        ComunicacionRecibida: _IdComunicacionRecibida,
        Incidente: _Incidente
    };

    $.ajax({
        type: 'POST',
        url: "RegistroSolicitud.aspx/AsociarCorrespondencia",
        cache: false,
        data: JSON.stringify(solicitud),
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: function (data) {

            if (data.d != "") {
                alert(data.d);
            }
            else {
                ActualizarCorrespondencia();
            }
        }
    }).fail(function (jqxhr, textStatus, error) {
        alert("Ocurrió un error al procesar la solicitud.");
    });
}

function ActualizarCorrespondencia() {

    $(_ComunicacionRecibida).remove();
    if ($("#gvCorrespondenciaRecibida .CR-item").length == 0)
        $("#ibCorrespondenciaRecibida").hide();
}
