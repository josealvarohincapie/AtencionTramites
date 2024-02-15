var controlActividad = null;

function GetActividad(c) {

    controlActividad = c;

    $("#txtBuscarActividad").val("");
    $("#modal-actividad .list-group-item").show();

    $(c).blur();
    $("#modal-actividad").modal('show');
    $("#modal-actividad .list-group-item").unbind();
    $("#modal-actividad .list-group-item").click(function () {

        if (controlActividad != null)
            $(controlActividad).val($(this).attr("data-value"));

        controlActividad = null;
        $("#modal-actividad").modal('hide');
        return false;
    });

    $("#txtBuscarActividad").unbind();
    $("#txtBuscarActividad").keyup(function () {
        $("#modal-actividad .list-group-item").each(function () {
            if ($("#txtBuscarActividad").val().trim() == "" || $(this).text().toLowerCase().indexOf($("#txtBuscarActividad").val().toLowerCase().trim()) >= 0)
                $(this).show();
            else
                $(this).hide();
        });
    });
}

function InitActividad(c) {

    $(c).unbind();
    $(c).find("option").addClass("hide")

    $(c).focus(function () {

        GetActividad(this);
        return false;
    });
}


