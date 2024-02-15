var _SelectedElement = null;
var __doPostBackOriginal = null;
function __onReturnPostBack() {
    $get('__EVENTTARGET').value = $get('__EVENTARGUMENT').value = '';
    Sys.WebForms.PageRequestManager.getInstance().remove_endRequest(__onReturnPostBack);
    ScriptInicial();
}

$(document).ready(function () {
    if (__doPostBackOriginal == null) {
        __doPostBackOriginal = __doPostBack;
        __doPostBack = function (a, b) {
            getSelectedTab();
            getScrollbarPosition();

            _SelectedElement = document.activeElement;
            if (_SelectedElement != null)
                $(_SelectedElement).blur();

            if (!(a != undefined && a.substr(0, 11) == "gvAdjuntos$" && a.substr(a.length - 13) == "$hlkDercargar"))
                $('#div-procesando').show();

            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(__onReturnPostBack);
            __doPostBackOriginal(a, b);
        }
    }

    ScriptInicial();
});

function ScriptInicial() {

    setSelectedTab();
    setScrollbarPosition();
    $('#div-procesando').hide();

    if (_SelectedElement != null)
        $(_SelectedElement).focus();

    $("input[type='text']").attr("autocomplete", "off");

    validacionJustificacion();
    limpiarNota();

    if ($("#SoloLectura").val() != "1") {

        $("#tbFechaVigencia").datepicker({
            dateFormat: "dd/mm/yy",
            showOtherMonths: true,
            changeMonth: true,
            changeYear: true,
            firstDay: 7,
            showAnim: "slideDown",
            autoSize: true
        });
    }

    $("#tbFechaVigencia").keypress(function () { return false; });

    InitActividad($("#ddlActividad"));
    InitActividad($("#ddlComplementarioActividad"));
    
    /*
    $("#DivRazonSocial").removeClass()
    if($("#btnModificarRazonSocial").length>0)
        $("#DivRazonSocial").addClass("input-group")
    */
    
    ShowDocumentacion();
    FixReportesCentinela();
    
}

function getSelectedTab() {

    $(".main-tab").each(function () {
        if ($(this).hasClass("active"))
            $("#hfSelectedTab").val($(this).find("a[role='tab']").attr("id"));
    });
}

function setSelectedTab(tab) {
    if (tab != undefined)
        $("#hfSelectedTab").val(tab);

    $("#" + $("#hfSelectedTab").val()).tab('show');
}

function getScrollbarPosition() {
    $("#hfScrollbarPosition").val(document.documentElement.scrollTop);
}

function setScrollbarPosition() {
    document.documentElement.scrollTop = $("#hfScrollbarPosition").val();
}

function ButtonPostBack(b) {
    __doPostBack(b.id, '');
    return false;
}

function FixReportesCentinela() {

    $("._LinkReporteCentinela").each(function () {
        var href = $(this).attr("href");
        if (href == undefined || href == "") {
            $(this).removeAttr("target");
            $(this).attr("href", "javascript:alert('Centinela no ha retornado respuesta')");
        }
    });
}