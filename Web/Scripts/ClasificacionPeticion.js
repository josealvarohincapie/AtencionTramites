function imprimirPagina() {
    window.print();
    return false;
}

function activarPestaniaClasificacion() {

    document.getElementById("menu0").className = "tab-pane fade";
    document.getElementById("menu1").className = "tab-pane fade";
    document.getElementById("menu2").className = "tab-pane fade";
    document.getElementById("menu3").className = "tab-pane fade in active";
    document.getElementById("menu4").className = "tab-pane fade";

    document.getElementById("liMenu0").className = "";
    document.getElementById("liMenu1").className = "";
    document.getElementById("liMenu2").className = "";
    document.getElementById("liMenu3").className = "active";
    document.getElementById("liMenu4").className = "";

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