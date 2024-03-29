﻿<%@ Page Language="vb" AutoEventWireup="false" ValidateRequest="false" CodeBehind="ClasificacionPeticion.aspx.vb" Inherits="Web.ClasificacionPeticion" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script runat="server">
    Dim item As Integer = 0
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css" />

    <link href="../../Styles/toastr.min.css" rel="stylesheet" />
    <link href="../../Styles/dashboard.css" rel="stylesheet" />

    <link href="../../Styles/font-awesome.min.css" rel="stylesheet" />
    <link href="../../Plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../../Plugins/datatables/css/jquery.dataTables.min.css" rel="stylesheet" />
    <!--
    <link href="Content/jquery-ui.css" rel="stylesheet" />
    <link href="Content/DataTables/css/dataTables.bootstrap.min.css" rel="stylesheet" />
    <link href="Content/DataTables/css/select.bootstrap.min.css" rel="stylesheet" />
    <link href="Content/DataTables/css/dataTables.jqueryui.min.css" rel="stylesheet" />
    <link href="Content/DataTables/css/buttons.jqueryui.min.css" rel="stylesheet" />
    <link href="Content/inspinia.css" rel="stylesheet" />-->
    <!--<link href="Content/animate.css" rel="stylesheet" />-->
    <!--<link href="Content/Ultimus.css" rel="stylesheet" />    
    <link href="../../Styles/dashboard.css" rel="stylesheet" />
    -->
    <script type="text/javascript" src="../../Plugins/bootstrap/js/bootstrap.min.js"></script>
    <!-- Inclusión de jQuery -->
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>
    <!-- <link href="../../Plugins/jquery/jquery.minv3.6.0.js"></script>-->
    <!-- Inclusión de Bootstrap CSS -->
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css" />
    <!-- <link href="../../Plugins/bootstrap/css/Bootstrap%20v3.4.1.min.css" rel="stylesheet" />-->
    <!-- Inclusión de Bootstrap JS -->
    <script type="text/javascript" src="https://stackpath.bootstrapcdn.com/bootstrap/3.4.1/js/bootstrap.min.js"></script>
   <!--  <script src="../../Plugins/bootstrap/js/Bootstrap%20v3.4.1.min.js"></script>-->
    <script type="text/javascript" src="../../Scripts/ClasificacionPeticion.js"></script>>
        
    <title>Formulario de Asesoría</title>
    <script>

        function limpiarCampo(textBoxClientId) {
            document.getElementById(textBoxClientId).value = '';
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
    </script>
    <style>

        .hiddencol
        {
            display: none;
        }

        body {
            background-color: #ffffff;
            color: #000000;
        }

        .infoUltimus {
            color: #000000;
        }

        .MenuBar {
            background-color: #6699ff;
        }

        .HeaderToggle {
            background-color: #6699ff;
        }

        .BaseHeader {
            background-color: #ffffff !important;
        }

        .myNav .nav-pills > li.active > a {
            background-color: #04A29C !important;
            color: white !important;
        }

        .myNav .nav-tabs > li.active > a {
            background-color: #04A29C !important;
            color: white !important;
        }

        .myNav .nav a {
            color: #808080;
            background-color: #d6d6d6;
            border-color: #808080;
        }

            .myNav .nav a:hover {
                background-color: #78c9c6 !important;
                color: #000000 !important;
                opacity: 0.5;
            }

        .myNav .nav-active {
            background-color: #04A29C !important;
            color: white !important;
        }

        #divMenu1 .tab-pane {
            display: none;
        }

        #divMenu1 .active {
            display: block;
        }

        #divMenu2 .tab-pane {
            display: none;
        }

        #divMenu2 .active {
            display: block;
        }

        #divMenu3 .tab-pane {
            display: none;
        }

        #divMenu3 .active {
            display: block;
        }
        #divMenu4 .tab-pane {
            display: none;
        }

        #divMenu4 .active {
            display: block;
        }
        #txtAsesoria{
        height: 80px;
        }
        .container{            
            display: flex;
            justify-content: center;
            height: 100vh;
            place-items: center;
        }        

        .columna {
            flex-grow: 1; /* Hace que las columnas ocupen el espacio disponible */
            flex: 1; /* Hace que cada columna ocupe un espacio igual */
            padding: 10px; /* Añade un poco de espacio alrededor del contenido de cada columna */
            
        }

        .contenedor {
            display: flex;
            justify-content: space-between;

        }
        .inputWarning {
            background-image: url(../Images/Warning_Icon_Red.png);
            background-position: right;
            background-repeat: no-repeat;
            border-color: #f6b94d;
        }
        .TextBoxCatalogo {
            height: 25px!important;
            border-bottom-left-radius: 4px!important;
            border-top-left-radius: 4px!important;
        }
        .modal-open .modal {
            overflow-x: hidden;
            overflow-y: auto;
        }
        .modal {
            position: fixed;
            top: 0;
            right: 0;
            bottom: 0;
            left: 0;
            z-index: 1050;
            display: none;
            overflow: hidden;
            -webkit-overflow-scrolling: touch;
            outline: 0;
        }
        .fade.in {
            opacity: 1;
        }
        .modal-dialog {
            width: 50%; 
            margin: auto; 
        }
        .modal-content {
            position: relative;
            background-color: #fff;
            -webkit-background-clip: padding-box;
            background-clip: padding-box;
            border: 1px solid #999;
            border: 1px solid rgba(0,0,0,.2);
            border-radius: 6px;
            outline: 0;
            -webkit-box-shadow: 0 3px 9px rgba(0,0,0,.5);
            box-shadow: 0 3px 9px rgba(0,0,0,.5);
        }
        .modal-header {
            padding: 15px;
            border-bottom: 1px solid #e5e5e5;
        }
        .modal-body {
            position: relative;
            padding: 15px;
        }
        .modal-footer {
            padding: 15px;
            text-align: right;
            border-top: 1px solid #e5e5e5;
        }
        .close {
            float: right;
            font-size: 21px;
            font-weight: 700;
            line-height: 1;
            color: #000;
            text-shadow: 0 1px 0 #fff;
            opacity: .2;
        }
        .btn-success {
            background-color: #5cb85c!important;
            color: #fff!important;
            border-color: #4cae4c!important;
        }
        .btn {
            font-weight: bold!important;
        }
        .btn-sm {
            padding: 5px 10px 5px 5px!important;
            font-size: 13px!important;
        }
        .btn-sm {
            padding: 5px 10px 5px 5px!important;
            font-size: 13px!important;
        }
        .modal-footer .btn+.btn {
            margin-bottom: 0;
            margin-left: 5px;
        }
        .row {
            margin-right: -15px;
            margin-left: -15px;
        }
        .modal-header .close {
            margin-top: -2px;
        }
        button.close {
            -webkit-appearance: none;
            padding: 0;
            cursor: pointer;
            background: 0 0;
            border: 0;
        }
        .close {
            float: right;
            font-size: 21px;
            font-weight: 700;
            line-height: 1;
            color: #000;
            text-shadow: 0 1px 0 #fff;
         
            opacity: .2;
        }
        .form-group {
            display: block;
            overflow: hidden;
            margin: 1px 1px 1px 1px!important;
            padding: 1px 1px 1px 1px;
        }
        .modal-backdrop {
            opacity: 0.3 !important; 
        }
    </style>
</head>
<body>    
    <form id="frmRadicado" runat="server">
        <div id="frmHeader" class="scroll-header">
            <div class="BaseHeader" id="divHeader" style="">
                <div class="row ">
                    <div class="col-xs-12 col-sm-6 col-md-3 " style="padding-top: 5px;">
                        <img src="data:image/png;base64,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" />
                    </div>
                    <div class="col-xs-12 col-sm-6 col-md-9">
                        <div class="infoUltimus">
                            <span><strong>Proceso:</strong> Asesorías </span>|
                            <span><strong>Etapa:</strong> Clasificación de la Petición </span>|
                            <span><strong>Incidente:</strong> 144 </span>
                            <br />
                            <span><strong>Usuario:</strong> DACARTEC </span>|
                            <span><strong>Fecha:</strong> 28-febrero-2024 </span>
                        </div>
                    </div>
                </div>
                <div class="row ">
                    <div class=" col-xs-12 col-md-12 col-lg-12">
                    </div>
                </div>
                <div class="row MenuBar" id="divMenuBar">
                    <div class=" col-xs-12 col-md-12 col-lg-12 ">
                        <div class="MenuBarContent navbar-collapse" id="bs-example-navbar-collapse-6" style="border-top: 0px;">
                            <ul class="nav navbar-nav" id="TabsMenuBar" style="float: left; margin: 0;">

                                <li id="MenuBarTab0" class="active" style="float: left;">
                                    <a class="nav-active">
                                        <i class="fa fa-circle-o"></i>&nbsp;Radicado
                                    </a>
                                </li>
                                <li id="MenuBarTab1" style="float: left;">
                                    <a href="https://iris.defensoria.gov.co/Correspondencia.Web/CorrExtEnviada/Seguimiento?UserID=LR+OsgiNZqWKwR2jZxx+24olE2nhHuca&amp;TaskID=1iEn42Y3KDAJ4s0tN8rOgo444mr/JDnlXe0WjTxnm60=&amp;Process=oZ53I/du3ztnPRx06ncmAg==&amp;Step=LiCSmafuK0Y=&amp;Incident=y38kn7rpPE4=&amp;JobFunction=7iwMdUAfD+ZOvaV55ptzBkw7Ww/vLHIJ&amp;UserFullName=0nU5I/o3u+w/7V4mukN3lw==&amp;UserEmail=P+1eJrpDd5c=&amp;Supervisor=hy57yc1JysM/7V4mukN3lw==&amp;SupervisorFullName=hy57yc1JysM/7V4mukN3lw==&amp;TaskStatus=wmJOgbc/QUk=&amp;Version=UyfSqXYT3AQ=&amp;StartTime=e37stcK54W6tVdH1Qtn1bPhadTn74cFC&amp;OcultarMenu=dq6ZkK5u5wc=&amp;SoloLectura=dq6ZkK5u5wc=&amp;co_solicitud=175&amp;co_solicitud_original=0&amp;co_solicitud_interna=0" onclick="">
                                        <i class="fa fa-circle-o"></i>&nbsp;Seguimiento
                                    </a>
                                </li>
                            </ul>
                        </div>

                    </div>
                </div>
            </div>
            <div class="row SubMenuBar">
                <div class=" col-xs-12 col-md-12 col-lg-12">
                    <div class="btn-group">                         
                        <button type="button" class="btn btn-default " id="btnHeaderToggle"
                            data-toggle="collapse" href="#divCollapseBody" 
                            style="color: black; text-decoration: none;">
                               <i class="fa fa-chevron-circle-up"></i>
                        </button>
                        <button type="button" class="btn btn-warning btn-sm" onclick="imprimirPagina()" id="btnBotones_ImprimirPantalla">
                            <strong>&nbsp;<i class="fa fa-print"></i><span class="hidden-sm hidden-xs">&nbsp;Imprimir Pantalla</span></strong>
                        </button>
                        <button type="button" runat="server" 
                            OnServerClick="GuardarClasificacion_OnClick"
                            class="btn btn-success btn-sm" 
                            id="btnBotones_GuardarRadicado"
                            validationgroup="vgClasificacionPeticion"
                            onclick="activarPestaniaClasificacion();"
                            >
                            <strong>&nbsp;<i class="fa fa-floppy-o"></i><span class="hidden-sm hidden-xs">&nbsp;Guardar</span></strong>
                        </button>
                        <button type="button" runat="server" class="btn btn-danger btn-sm" id="btnBotones_Completar" OnServerClick="Enviar_OnClick" validationgroup="vgDecision" >
                            <strong><i class="fa fa-circle-o"></i><span class="hidden-sm hidden-xs">&nbsp;Enviar</span></strong>
                        </button>

                    </div>
                </div>
            </div>
        </div>
        <div id="divCollapseBody" class="panel-collapse collapse in">
        <!-- Modal -->
        <div class="modal fade" id="divmodalCatalogo" tabindex="-1" role="dialog" aria-hidden="true" data-backdrop="static" style="display: none;">
            <div class="modal-dialog ">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                        <h4 class="modal-title">Catálogo</h4>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-lg-4 col-md-4 col-sm-4"></div>
                            <div class="col-lg-8 col-md-8 col-sm-8">
                                <div class="form-group">
                                    <div class="btn-group col-lg-8 col-md-8 col-sm-8">
                                        <input type="hidden" id="hiddenFiltroCod">
                                        <input type="text" id="txtDescripcion" name="txtLugarFiltro" class="form-control imput-xs" data-role="btnBuscarNacionalidad" fdprocessedid="7tqfvw">
                                    </div>
                                    <div class="btn-group">
                                        <button type="button" id="btnBuscar" class="btn btn-info btn-xs" fdprocessedid="kosw7m">
                                            <i class="fa fa-search"></i>&nbsp;Buscar
                                        </button>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <br>
                        <div class="row">
                            <div class="col-lg-12 col-md-12 col-sm-12">
                                <div id="tableCatalogo_wrapper" class="dataTables_wrapper no-footer"><div class="dataTables_length" id="tableCatalogo_length"><label>Mostrar <select name="tableCatalogo_length" aria-controls="tableCatalogo" class="" fdprocessedid="ia0i4de"><option value="10">10</option><option value="25">25</option><option value="50">50</option><option value="-1">All</option></select> registros</label></div><table id="tableCatalogo" class="table table-striped no-footer dataTable" role="grid" style="width: 0px;">
                                    <thead>
                                        <tr role="row"><th class="center sorting" tabindex="0" aria-controls="tableCatalogo" rowspan="1" colspan="1" aria-label=": Activar para ordenar la columna de manera ascendente" style="width: 0px;" aria-sort="descending"></th><th class="sorting" tabindex="0" aria-controls="tableCatalogo" rowspan="1" colspan="1" aria-label="Código: Activar para ordenar la columna de manera ascendente" style="width: 0px;">Código</th><th class="sorting" tabindex="0" aria-controls="tableCatalogo" rowspan="1" colspan="1" aria-label="Descripción: Activar para ordenar la columna de manera ascendente" style="width: 0px;">Descripción</th></tr>
                                    </thead>
                                <tbody><tr role="row" class="odd"><td class="center sorting_1">
            <button type="button" id="btnSeleccionar1" class="btn btn-default btn-xs" fdprocessedid="1qcc3b">
                <i class="fa fa-check"></i>&nbsp;&nbsp;...
            </button>
        </td><td>-860093103</td><td>DEFENSORIA DEL PUEBLO</td></tr><tr role="row" class="even"><td class="center sorting_1">
            <button type="button" id="btnSeleccionar2" class="btn btn-default btn-xs" fdprocessedid="hml65v">
                <i class="fa fa-check"></i>&nbsp;&nbsp;...
            </button>
        </td><td>-1298458332</td><td>PRUEBAS</td></tr></tbody></table><div class="dataTables_paginate paging_simple_numbers" id="tableCatalogo_paginate"><a class="paginate_button previous disabled" aria-controls="tableCatalogo" data-dt-idx="0" tabindex="0" id="tableCatalogo_previous">Anterior</a><span><a class="paginate_button current" aria-controls="tableCatalogo" data-dt-idx="1" tabindex="0">1</a></span><a class="paginate_button next disabled" aria-controls="tableCatalogo" data-dt-idx="2" tabindex="0" id="tableCatalogo_next">Siguiente</a></div></div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer" style="display:none;">
                        <button type="button" class="btn btn-success btn-sm" data-dismiss="modal" id="btnGuardarCatalogo">
                            <strong>&nbsp;<i class="fa fa-floppy-o"></i>&nbsp;Guardar</strong>
                        </button>
                        <button type="button" class="btn btn-danger btn-sm" data-dismiss="modal">
                            <strong>&nbsp;<i class="fa fa-reply"></i>&nbsp;Cerrar</strong>
                        </button>
                    </div>
                </div>
            </div>
        </div>
        
        <div id="frmBody" class="row" style="position: relative; top: 210px">
            <div class="col-xs-12 col-md-12 col-lg-12  ">
                <div class="starter-template" id="divStarterTemplate2">
                    <div id="body" class="l-main">
                        <div id="inner_body" class="l-wrapper">

                            <div id="divRespuesta" class="myNav">
                                <ul class="nav nav-pills nav-justified">
                                    <li id="liMenu0" class="active"><a data-toggle="tab" href="#menu0">Información del radicado</a></li>
                                    <li id="liMenu1" ><a data-toggle="tab" href="#menu1">Anexos del Radicado recibido</a></li>
                                    <li id="liMenu2" ><a data-toggle="tab" href="#menu2">Registro de peticionarios</a></li>
                                    <li id="liMenu3" ><a data-toggle="tab" href="#menu3">Clasificación de la petición</a></li>
                                    <li id="liMenu4" ><a data-toggle="tab" href="#menu4">Decisión</a></li>
                                </ul>
                                <div class="tab-content">

                                    <div id="menu0" class="tab-pane fade in active">
                                        <div id="divMenu0">
                                            <h3 class="TituloAcordeon">
                                                <a data-toggle="collapse" href="#divCollapse0" style="color: black; text-decoration: none;">
                                                    <i class="fa fa-chevron-circle-down"></i>
                                                    &nbsp;Radicado Enviado
                                                </a>
                                            </h3>
                                            <div id="divCollapse0" class="panel-collapse collapse in">

                                                <div class="contenedor">
                                                    <div class="columna">
                                                        <div class="row">
                                                            <div class="col-md-10">
                                                                <h3>Datos básicos del peticionario</h3>
                                                                <div class="form-group " id="divNumeroRadicado">
                                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label ControlsForms #custom_class" id="lblRespuesta_NumeroRadicado" name="lblRespuesta_NumeroRadicado" default_label="No. Radicado">No. Radicado:</label>
                                                                    <div class="input-group col-lg-8 col-md-8 col-sm-8 col-xs-8 ">
                                                                        <asp:TextBox ID="txtNumeroRadicado" runat="server" Text="" CssClass="form-control imput-xs TextBoxFramework" ReadOnly="true"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group" id="divCanalAtencion">
                                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label ControlsForms #custom_class" id="lblCanalAtencion" name="lblCanalAtencion" default_label="Canal de atención:">Canal de atención:</label>
                                                                    <div class="input-group input-group-sm">
                                                                        <asp:HiddenField ID="hddCodigoCanalAtencion" runat="server" />
                                                                        <asp:TextBox ID="txtCanalAtencion" runat="server" ReadOnly="true" Text="" CssClass="form-control TextBoxCatalogo inputSuccess"></asp:TextBox>
                                                                        <span class="input-group-btn">
                                                                            <button type="button" disabled="disabled" id="btn_LimpiarCanalAtencion"  onclick="limpiarCampo('<%= txtCanalAtencion.ClientID %>')" class="btn btn-default btn-medium">
                                                                                <i class="fa fa-close"></i>
                                                                            </button>
                                                                            <button type="button" disabled="disabled" id="btn_CanalAtencion" class="btn btn-success btn-medium btn-margin-catalogo btn-file" usehttppost="0" webapplication="" usedatasource="0">
                                                                                <i class="fa fa-search"></i>&nbsp;
                                                                            </button>
                                                                        </span>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group " id="divFecha">
                                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label ControlsForms #custom_class" id="lblFecha" name="lblFecha" default_label="Fecha">Fecha:</label>
                                                                    <div class="input-group col-lg-8 col-md-8 col-sm-8 col-xs-8 ">
                                                                        <asp:TextBox ID="txtFecha" runat="server" 
                                                                            TextMode="DateTime" 
                                                                            CssClass="form-control input-xs TextBoxFramework" ReadOnly="true"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group" id="divTipoSolicitante">
                                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label ControlsForms #custom_class" id="lblTipoSolicitante" name="lblTipoSolicitante" default_label="Tipo de solicitante:">Tipo de solicitante:</label>
                                                                    <div class="input-group input-group-sm">
                                                                        <asp:HiddenField ID="hddCodigoTipoSolicitante" runat="server" />
                                                                        <asp:TextBox ID="txtTipoSolicitante" runat="server" Text="" CssClass="form-control TextBoxCatalogo inputSuccess" ReadOnly="true"></asp:TextBox>
                                                                        <span class="input-group-btn">
                                                                            <button type="button" disabled="disabled" id="btn_LimpiarTipoSolicitante" onclick="limpiarCampo('<%= txtTipoSolicitante.ClientID %>')" class="btn btn-default btn-medium">
                                                                                <i class="fa fa-close"></i>
                                                                            </button>
                                                                            <button type="button" disabled="disabled" id="btn_TipoSolicitante" class="btn btn-success btn-medium btn-margin-catalogo btn-file" usehttppost="0" webapplication="" usedatasource="0">
                                                                                <i class="fa fa-search"></i>&nbsp;
                                                                            </button>
                                                                        </span>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group " id="divEsAnonimo" >
                                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label ControlsForms #custom_class" id="lblEsAnonimo" name="lblEsAnonimo" default_label="Es anónimo :">Es anónimo :</label>
                                                                    <div class="input-group col-lg-8 col-md-8 col-sm-8 col-xs-8 ">
                                                                        <asp:CheckBox ID="chkEsAnonimo" disabled="disabled" runat="server" CssClass="form-control imput-xs TextBoxFramework" ReadOnly="true"></asp:CheckBox>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group" id="divTipoDocumento">
                                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label ControlsForms #custom_class" id="lblTipoDocumento" name="lblTipoDocumento" default_label="Tipo documento de identificación:">Tipo documento de identificación:</label>
                                                                    <div class="input-group input-group-sm">
                                                                        <asp:HiddenField ID="hddCodigoTipoDocumento" runat="server" />
                                                                        <asp:TextBox ID="txtTipoDocumento" runat="server" Text="" ReadOnly="true" CssClass="form-control TextBoxCatalogo inputSuccess"></asp:TextBox>
                                                                        <span class="input-group-btn">
                                                                            <button type="button" disabled="disabled" id="btn_LimpiarTipoDocumento" onclick="limpiarCampo('<%= txtTipoDocumento.ClientID %>')" class="btn btn-default btn-medium">
                                                                                <i class="fa fa-close"></i>
                                                                            </button>
                                                                            <button type="button" disabled="disabled" id="btn_TipoDocumento" class="btn btn-success btn-medium btn-margin-catalogo btn-file" usehttppost="0" webapplication="" usedatasource="0">
                                                                                <i class="fa fa-search"></i>&nbsp;
                                                                            </button>
                                                                        </span>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group " id="divIdentificacion">
                                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label ControlsForms #custom_class" id="lblIdentificacion" name="lblIdentificacion" default_label="Número documento de identificación:">Número documento de identificación:</label>
                                                                    <div class="input-group col-lg-8 col-md-8 col-sm-8 col-xs-8 ">
                                                                        <asp:TextBox ID="txtIdentificacion" runat="server" 
                                                                            CssClass="form-control input-xs TextBoxFramework" 
                                                                            ReadOnly="true"></asp:TextBox>                                                                       
                                                                    </div>
                                                                </div>
                                                                <div class="form-group " id="divRemitente">
                                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label ControlsForms #custom_class" id="lblRemitente" name="lblRemitente" default_label="Remitente:">Remitente:</label>
                                                                    <div class="input-group col-lg-8 col-md-8 col-sm-8 col-xs-8 ">
                                                                        <asp:TextBox ID="txtRemitente" runat="server" CssClass="form-control input-xs TextBoxFramework" ReadOnly="true"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="columna">
                                                        <div class="row">
                                                            <div class="col-md-10">
                                                                <h3>Datos enfoques diferenciales y de género</h3>
                                                                <div class="form-group " id="divGrupoEtnico" >
                                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label ControlsForms #custom_class"
                                                                        id="lblGrupoEtnico" name="lblGrupoEtnico"
                                                                        default_label="¿Se reconoce como miembro de algún grupo étnico?">
                                                                        ¿Se reconoce como miembro de algún grupo étnico?
                                                                    </label>
                                                                    <div class="input-group col-lg-8 col-md-8 col-sm-8 col-xs-8 ">
                                                                        <asp:RadioButtonList ID="rblGrupoEtnico"   runat="server" RepeatDirection="Horizontal">
                                                                            <asp:ListItem  Text="Sí" Value="1"></asp:ListItem>
                                                                            <asp:ListItem  Text="No" Value="0"></asp:ListItem>
                                                                        </asp:RadioButtonList>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group" id="divSexoAsignado">
                                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label ControlsForms #custom_class" id="lblSexoAsignado" name="lblSexoAsignado" default_label="Sexo asignado:">Sexo asignado:</label>
                                                                    <div class="input-group input-group-sm">
                                                                        <asp:HiddenField ID="hddCodigoSexoAsignado" runat="server" />
                                                                        <asp:TextBox ID="txtSexoAsignado" ReadOnly="true" runat="server" Text="" CssClass="form-control TextBoxCatalogo inputSuccess"></asp:TextBox>
                                                                        <span class="input-group-btn">
                                                                            <button type="button" disabled="disabled" id="btn_LimpiarSexoAsignado" onclick="limpiarCampo('<%= txtSexoAsignado.ClientID %>')" class="btn btn-default btn-medium">
                                                                                <i class="fa fa-close"></i>
                                                                            </button>
                                                                            <button type="button" disabled="disabled" id="btn_SexoAsignado" class="btn btn-success btn-medium btn-margin-catalogo btn-file" usehttppost="0" webapplication="" usedatasource="0">
                                                                                <i class="fa fa-search"></i>&nbsp;
                                                                            </button>
                                                                        </span>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group" id="divIdentidadGenero">
                                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label ControlsForms #custom_class" id="lblIdentidadGenero" name="lblIdentidadGenero" default_label="Identidad de género:">Identidad de género:</label>
                                                                    <div class="input-group input-group-sm">
                                                                        <asp:HiddenField ID="hddCodigoIdentidadGenero" runat="server" />
                                                                        <asp:TextBox ID="txtIdentidadGenero" ReadOnly="true" runat="server" Text="" CssClass="form-control TextBoxCatalogo inputSuccess"></asp:TextBox>
                                                                        <span class="input-group-btn">
                                                                            <button type="button" disabled="disabled" id="btn_LimpiarIdentidadGenero" onclick="limpiarCampo('<%= txtIdentidadGenero.ClientID %>')" class="btn btn-default btn-medium">
                                                                                <i class="fa fa-close"></i>
                                                                            </button>
                                                                            <button type="button" disabled="disabled" id="btn_IdentidadGenero" class="btn btn-success btn-medium btn-margin-catalogo btn-file" usehttppost="0" webapplication="" usedatasource="0">
                                                                                <i class="fa fa-search"></i>&nbsp;
                                                                            </button>
                                                                        </span>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group" id="divOrientacionSexual">
                                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label ControlsForms #custom_class" id="lblOrientacionSexual" name="lblOrientacionSexual" default_label="Orientación sexual:">Orientación sexual:</label>
                                                                    <div class="input-group input-group-sm">
                                                                        <asp:HiddenField ID="hddCodigoOrientacionSexual" runat="server" />
                                                                        <asp:TextBox ID="txtOrientacionSexual" ReadOnly="true" runat="server" Text="" CssClass="form-control TextBoxCatalogo inputSuccess"></asp:TextBox>
                                                                        <span class="input-group-btn">
                                                                            <button type="button" disabled="disabled" id="btn_LimpiarOrientacionSexual" onclick="limpiarCampo('<%= txtOrientacionSexual.ClientID %>')" class="btn btn-default btn-medium">
                                                                                <i class="fa fa-close"></i>
                                                                            </button>
                                                                            <button type="button" disabled="disabled" id="btn_OrientacionSexual" class="btn btn-success btn-medium btn-margin-catalogo btn-file" usehttppost="0" webapplication="" usedatasource="0">
                                                                                <i class="fa fa-search"></i>&nbsp;
                                                                            </button>
                                                                        </span>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group" id="divExpresionGenero">
                                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label ControlsForms #custom_class" id="lblExpresionGenero" name="lblExpresionGenero" default_label="Expresión de género :">Expresión de género :</label>
                                                                    <div class="input-group input-group-sm">
                                                                        <asp:HiddenField ID="hddCodigoExpresionGenero" runat="server" />
                                                                        <asp:TextBox ID="txtExpresionGenero" ReadOnly="true" runat="server" Text="" CssClass="form-control TextBoxCatalogo inputSuccess"></asp:TextBox>
                                                                        <span class="input-group-btn">
                                                                            <button type="button" disabled="disabled" id="btn_LimpiarExpresionGenero" onclick="limpiarCampo('<%= txtExpresionGenero.ClientID %>')" class="btn btn-default btn-medium">
                                                                                <i class="fa fa-close"></i>
                                                                            </button>
                                                                            <button type="button" disabled="disabled" id="btn_ExpresionGenero" class="btn btn-success btn-medium btn-margin-catalogo btn-file" usehttppost="0" webapplication="" usedatasource="0">
                                                                                <i class="fa fa-search"></i>&nbsp;
                                                                            </button>
                                                                        </span>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group" id="divRangoEdad">
                                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label ControlsForms #custom_class" id="lblRangoEdad" name="lblRangoEdad" default_label="Rango de edad:">Rango de edad:</label>
                                                                    <div class="input-group input-group-sm">
                                                                        <asp:HiddenField ID="hddCodigoRangoEdad" runat="server" />
                                                                        <asp:TextBox ID="txtRangoEdad" ReadOnly="true" runat="server" Text="" CssClass="form-control TextBoxCatalogo inputSuccess"></asp:TextBox>
                                                                        <span class="input-group-btn">
                                                                            <button type="button" disabled="disabled" id="btn_LimpiarRangoEdad" onclick="limpiarCampo('<%= txtRangoEdad.ClientID %>')" class="btn btn-default btn-medium">
                                                                                <i class="fa fa-close"></i>
                                                                            </button>
                                                                            <button type="button" disabled="disabled" id="btn_RangoEdad" class="btn btn-success btn-medium btn-margin-catalogo btn-file" usehttppost="0" webapplication="" usedatasource="0">
                                                                                <i class="fa fa-search"></i>&nbsp;
                                                                            </button>
                                                                        </span>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div id="menu1" class="tab-pane fade">
                                        <div id="divMenu1">
                                             <h3 class="TituloAcordeon">
                                                 <a data-toggle="collapse" href="#divCollapse1" style="color: black; text-decoration: none;">
                                                     <i class="fa fa-chevron-circle-down"></i>
                                                     &nbsp;Anexos del Radicado Enviado
                                                 </a>
                                             </h3>
                                            <div id="divCollapse1" class="panel-collapse collapse in">
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div id="divListaDocumentos" class="dataTables_wrapper no-footer">                                                    
                                                        <asp:GridView ID="grdDocumentos"
                                                        emptyDataText="No existen documentos asociados al radicado"
                                                        runat="server" 
                                                        AutoGenerateColumns="false"
                                                        AllowSorting="True"
                                                        AllowPaging="true"
                                                        PageSize="2"
                                                        CssClass="table table-striped no-footer dataTable"
                                                        Width="0px"
                                                        HorizontalAlign="Center"
                                                        autogenerateselectbutton="false"  
                                                        UseAccessibleHeader="true">                                                        
                                                        <Columns>                                                                                                                        
                                                             <asp:BoundField DataField="TituloArchivo" HeaderText="Título" 
                                                                SortExpression="TituloArchivo"  />
                                                            <asp:BoundField DataField="FechaCreacion" HeaderText="Fecha" 
                                                                SortExpression="FechaCreacion"/>
                                                            <asp:BoundField DataField="NombreUsuarioCreacion" HeaderText="Usuario" 
                                                                SortExpression="NombreUsuarioCreacion" />
                                                            <asp:TemplateField ShowHeader="false">
                                                            <ItemTemplate>                                                                
                                                                <asp:ImageButton ID="btnVerDocumento" runat="server" 
                                                                    ToolTip="Ver" 
                                                                    OnClick="btnVerDocumento_Click"
                                                                    ImageUrl="~/Styles/images/fa-external-link.svg"
                                                                    />
                                                            </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="RutaVirtualArchivo" HeaderText="RutaVirtualArchivo" Visible="true" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" />
                                                        </Columns>
                                                    </asp:GridView>
                                                    </div>
                                                </div>
                                            </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div id="menu2" class="tab-pane fade">
                                        <div id="divMenu2">
                                            <h3 class="TituloAcordeon">
                                                <a data-toggle="collapse" href="#divCollapse2" style="color: black; text-decoration: none;">
                                                    <i class="fa fa-chevron-circle-down"></i>
                                                    &nbsp;Registro de peticionarios
                                                </a>
                                            </h3>
                                            <div id="divCollapse2" class="panel-collapse collapse in">
                                                <div id="divRegistroPeticionarios" runat="server">
                                                    <table id="example" class="table table-striped table-bordered" style="width:100%">
                                                        <thead>
                                                            <tr>
                                                                <th>N°</th>
                                                                <th>Anónimo</th>
                                                                <th>Contacto</th>
                                                                <th>Tipo de persona</th>
                                                                <th>Tipo de documento</th>
                                                                <th>Número de documento</th>
                                                                <th>Nombres y apellidos / Razón social</th>
                                                                <th>Confidencialidad</th>
                                                                <th>Acciones</th>
                                                                
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr>
                                                                <td>1</td>
                                                                <td>
                                                                    <input type="checkbox" checked disabled></td>
                                                                <td>contacto1@example.com</td>
                                                                <td>Natural</td>
                                                                <td>T.I</td>
                                                                <td>12345678</td>
                                                                <td>Mateo Polanco</td>
                                                                <td>Alta</td>
                                                                <td>
                                                                    <button type="button" class="btn btn-primary btn-sm">
                                                                        <i class="fas fa-pencil-alt"></i>
                                                                    </button>
                                                                    <button type="button" class="btn btn-danger btn-sm">
                                                                        <i class="fas fa-trash"></i>
                                                                    </button>
                                                                </td>
                                                                
                                                            </tr>
                                                            <tr>
                                                                <td>2</td>
                                                                <td>
                                                                    <input type="checkbox" disabled></td>
                                                                <td>contacto2@example.com</td>
                                                                <td>Jurídica</td>
                                                                <td>C.C</td>
                                                                <td>87654321</td>
                                                                <td>Samir Rodríguez</td>
                                                                <td>Media</td>
                                                                <td>
                                                                   <button type="button" class="btn btn-primary btn-sm">
                                                                       <i class="fas fa-pencil-alt"></i>
                                                                   </button>
                                                                    <button type="button" class="btn btn-danger btn-sm">
                                                                        <i class="fas fa-trash"></i>
                                                                    </button>
                                                                </td>
                                                                
                                                            </tr>
                                                        </tbody>
                                                        <script>
                                                            document.addEventListener('DOMContentLoaded', function () {
                                                                var totalRegistros = document.querySelector('#example tbody').rows.length;
                                                                document.querySelector('#totalRegistros').textContent = totalRegistros;
                                                            });
                                                        </script>
                                                        <tfoot>
                                                            <tr>
                                                                <td colspan="9" style="text-align: left;">Total de registros: <span id="totalRegistros"></span></td>
                                                            </tr>
                                                        </tfoot>
                                                    </table>
                                                    <div style="text-align: right;">
                                                     <button " type="button" class="btn btn-primary btn-sm">
                                                     <i class="fas fa-plus"></i>&nbsp;Agregar peticionario</button>
                                                   </div>

                                                </div>
                                            <div class="contenedor">
                                                <div class="columna">
                                                    <div class="row">
                                                        <div class="col-md-10">
                                                            <h3>Datos básicos del usuario (a)</h3>
                                                            <div class="form-group" id="divTipoSolicitante2">
                                                                <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label ControlsForms #custom_class" id="lblTipoSolicitante2" name="lblTipoSolicitante2" default_label="Tipo de solicitante:">Tipo de solicitante:</label>
                                                                <div class="input-group input-group-sm">
                                                                    <asp:HiddenField ID="hddTipoSolicitante2" runat="server" />
                                                                    <asp:TextBox ID="txtTipoSolicitante2" runat="server" validationgroup="vgRegistroPeticionario" CssClass="form-control TextBoxCatalogo inputSuccess"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="rqfTipoSolicitante2" validationgroup="vgRegistroPeticionario" runat="server" ControlToValidate="txtTipoSolicitante" ErrorMessage="El tipo de solicitante es obligatorio" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                    <span class="input-group-btn">
                                                                        <button type="button" id="btn_LimpiarTipoSolicitante2" onclick="limpiarCampo('<%= txtTipoSolicitante2.ClientID %>')" class="btn btn-default btn-medium">
                                                                            <i class="fa fa-close"></i>
                                                                        </button>
                                                                        <button type="button" id="btn_TipoSolicitante2" class="btn btn-success btn-medium btn-margin-catalogo btn-file"  data-toggle="modal" data-target="#divmodalCatalogo" usehttppost="0" webapplication="" usedatasource="0">
                                                                            <i class="fa fa-search"></i>&nbsp;
                                                                        </button>
                                                                    </span>
                                                                </div>
                                                            </div>
                                                            <div class="form-group " id="divEsAnonimo2">
                                                                <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label ControlsForms #custom_class" id="lblEsAnonimo2" name="lblEsAnonimo2" default_label="Es anónimo ">¿Anónimo? </label>
                                                                <div class="input-group col-lg-8 col-md-8 col-sm-8 col-xs-8 ">
                                                                    <asp:CheckBox ID="chkAnonimo2" runat="server" CssClass="form-control input-xs TextBoxFramework"></asp:CheckBox>
                                                                </div>
                                                            </div>
                                                            <div class="form-group" id="divTipoDocumento2">
                                                                <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label ControlsForms #custom_class" id="lblTipoDocumento2" name="lblTipoDocumento2" default_label="Tipo documento de identificación:">Tipo documento de identificación:</label>
                                                                <div class="input-group input-group-sm">
                                                                    <asp:HiddenField ID="hddCodigoTipoDocumento2" runat="server" />
                                                                    <asp:TextBox ID="txtTipoDocumento2" runat="server"   validationgroup="vgRegistroPeticionario" ReadOnly="true" CssClass="form-control TextBoxCatalogo inputSuccess"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="rqfTipoDocumento2" runat="server" validationgroup="vgRegistroPeticionario" ControlToValidate="txtTipoDocumento2" ErrorMessage="El tipo de documento es obligatorio" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                    <span class="input-group-btn">
                                                                        <button type="button" id="btn_LimpiarTipoDocumento2" onclick="limpiarCampo('<%= txtTipoDocumento2.ClientID %>')" class="btn btn-default btn-medium">
                                                                            <i class="fa fa-close"></i>
                                                                        </button>
                                                                        <button type="button" id="btn_TipoDocumento2" class="btn btn-success btn-medium btn-margin-catalogo btn-file"  data-toggle="modal" data-target="#divmodalCatalogo" usehttppost="0" webapplication="" usedatasource="0">
                                                                            <i class="fa fa-search"></i>&nbsp;
                                                                        </button>
                                                                    </span>
                                                                </div>
                                                            </div>
                                                            <div class="form-group " id="divIdentificacion2">
                                                                <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label ControlsForms #custom_class" id="lblIdentificacion2" name="lblIdentificacion2" default_label="Número documento de identificación:">Número documento de identificación:</label>
                                                                <div class="input-group col-lg-8 col-md-8 col-sm-8 col-xs-8 ">
                                                                    <asp:TextBox ID="txtIdentificacion2" runat="server" validationgroup="vgRegistroPeticionario"
                                                                        CssClass="form-control input-xs TextBoxFramework"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="rqfIdentificacion2" runat="server" validationgroup="vgRegistroPeticionario" ControlToValidate="txtIdentificacion2" ErrorMessage="El número de identificación es obligatorio" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                            <div class="form-group" id="divRemitente2">
                                                                <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label ControlsForms #custom_class" id="lblRemitente2" name="Remitente:">Remitente:</label>
                                                                <div class="input-group input-group-sm">
                                                                    <asp:HiddenField ID="hdnRemitente2" runat="server" />
                                                                    <asp:TextBox ID="txtRemitente2" runat="server" validationgroup="vgRegistroPeticionario" CssClass="form-control TextBoxCatalogo inputSuccess"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="rqvRemitente2" runat="server" validationgroup="vgRegistroPeticionario" ControlToValidate="txtRemitente2" ErrorMessage="El remitente es obligatorio" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                    <span class="input-group-btn">
                                                                        <button type="button" id="btn_LimpiarRemitente2" onclick="limpiarCampo('<%= txtRemitente2.ClientID %>')" class="btn btn-default btn-medium">
                                                                            <i class="fa fa-close"></i>
                                                                        </button>
                                                                        <button type="button" id="btn_Remitente2" class="btn btn-success btn-medium btn-margin-catalogo btn-file" data-toggle="modal" data-target="#divmodalCatalogo" usehttppost="0" webapplication="" usedatasource="0">
                                                                            <i class="fa fa-search"></i>&nbsp;
                                                                        </button>
                                                                    </span>
                                                                </div>
                                                            </div>
                                                            <div class="form-group" id="divEstadoCivil">
                                                                <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label ControlsForms #custom_class" id="lblEstadoCivil" name="Estado civil:">Estado civil:</label>
                                                                <div class="input-group input-group-sm">
                                                                    <asp:HiddenField ID="hdnEstadoCivil" runat="server" />
                                                                    <asp:TextBox ID="txtEstadoCivil" runat="server" validationgroup="vgRegistroPeticionario" CssClass="form-control TextBoxCatalogo inputSuccess"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="rqfEstadoCivil" runat="server" validationgroup="vgRegistroPeticionario" ControlToValidate="txtEstadoCivil" ErrorMessage="El estado civil es obligatorio" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                    <span class="input-group-btn">
                                                                        <button type="button" id="btn_LimpiarEstadoCivil" onclick="limpiarCampo('<%= txtEstadoCivil.ClientID %>')" class="btn btn-default btn-medium">
                                                                            <i class="fa fa-close"></i>
                                                                        </button>
                                                                        <button type="button" id="btn_EstadoCivil" class="btn btn-success btn-medium btn-margin-catalogo btn-file" data-toggle="modal" data-target="#divmodalCatalogo" usehttppost="0" webapplication="" usedatasource="0">
                                                                            <i class="fa fa-search"></i>&nbsp;
                                                                        </button>
                                                                    </span>
                                                                </div>
                                                            </div>
                                                            <div class="form-group" id="divNivelEstudio">
                                                                <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label ControlsForms #custom_class" id="lblNivelEstudio" name="Nivel de estudio:">Nivel de estudio:</label>
                                                                <div class="input-group input-group-sm">
                                                                    <asp:HiddenField ID="hdnNivelEstudio" runat="server" />
                                                                    <asp:TextBox ID="txtNivelEstudio" runat="server" validationgroup="vgRegistroPeticionario" CssClass="form-control TextBoxCatalogo inputSuccess"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="rfvNivelEstudio" runat="server" validationgroup="vgRegistroPeticionario" ControlToValidate="txtNivelEstudio" ErrorMessage="El nivel de estudio es obligatorio" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                    <span class="input-group-btn">
                                                                        <button type="button" id="btn_LimpiarNivelEstudio" onclick="limpiarCampo('<%= txtNivelEstudio.ClientID %>')" class="btn btn-default btn-medium">
                                                                            <i class="fa fa-close"></i>
                                                                        </button>
                                                                        <button type="button" id="btn_NivelEstudio" class="btn btn-success btn-medium btn-margin-catalogo btn-file" data-toggle="modal" data-target="#divmodalCatalogo" usehttppost="0" webapplication="" usedatasource="0">
                                                                            <i class="fa fa-search"></i>&nbsp;
                                                                        </button>
                                                                    </span>
                                                                </div>
                                                            </div>
                                                            <div class="form-group" id="divEstratoSocioeconomico">
                                                                <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label ControlsForms #custom_class" id="lblEstratoSocioeconomico" name="Estrato socioeconómico:">Estrato socioeconómico:</label>
                                                                <div class="input-group input-group-sm">
                                                                    <asp:HiddenField ID="hdnEstratoSocioeconomico" runat="server" />
                                                                    <asp:TextBox ID="txtEstratoSocioeconomico" runat="server" validationgroup="vgRegistroPeticionario" CssClass="form-control TextBoxCatalogo inputSuccess"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="rqfEstratoSocioeconomico" runat="server" validationgroup="vgRegistroPeticionario" ControlToValidate="txtEstratoSocioeconomico" ErrorMessage="El estrato socioeconómico es obligatorio" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                    <span class="input-group-btn">
                                                                        <button type="button" id="btn_LimpiarEstratoSocioeconomico" onclick="limpiarCampo('<%= txtEstratoSocioeconomico.ClientID %>')" class="btn btn-default btn-medium">
                                                                            <i class="fa fa-close"></i>
                                                                        </button>
                                                                        <button type="button" id="btn_EstratoSocioeconomico" class="btn btn-success btn-medium btn-margin-catalogo btn-file" data-toggle="modal" data-target="#divmodalCatalogo" usehttppost="0" webapplication="" usedatasource="0">
                                                                            <i class="fa fa-search"></i>&nbsp;
                                                                        </button>
                                                                    </span>
                                                                </div>
                                                            </div>
                                                            <div class="form-group " id="divNombreIdentitario">
                                                                <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label ControlsForms #custom_class" id="lblNombreIdentitario" name="lblNombreIdentitario" default_label="Nombre identitario o Jurídico:">Nombre identitario o Jurídico:</label>
                                                                <div class="input-group col-lg-8 col-md-8 col-sm-8 col-xs-8 ">
                                                                    <asp:TextBox ID="txtNombreIdentitario" runat="server" validationgroup="vgRegistroPeticionario"
                                                                        CssClass="form-control input-xs TextBoxFramework"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="rqfNombreIdentitario" runat="server" validationgroup="vgRegistroPeticionario" ControlToValidate="txtNombreIdentitario" ErrorMessage="El nombre identitario es obligatorio" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>

                                                                </div>
                                                            </div>
                                                            <div class="form-group " id="divConfidencial">
                                                                <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label ControlsForms #custom_class" id="lblConfidencial" name="lblConfidencial" default_label="Es confidencial :">¿Confidencial? </label>
                                                                <div class="input-group col-lg-8 col-md-8 col-sm-8 col-xs-8 ">
                                                                    <asp:CheckBox ID="chkConfidencial" runat="server" validationgroup="vgRegistroPeticionario" CssClass="form-control input-xs TextBoxFramework"></asp:CheckBox>
                                                                </div>
                                                            </div>
                                                            <div class="form-group " id="divContacto">
                                                                <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label ControlsForms #custom_class" id="lblContacto" name="lblContacto" default_label="Contacto :">¿Contacto? </label>
                                                                <div class="input-group col-lg-8 col-md-8 col-sm-8 col-xs-8 ">
                                                                    <asp:CheckBox ID="chkContacto" runat="server" validationgroup="vgRegistroPeticionario" CssClass="form-control input-xs TextBoxFramework"></asp:CheckBox>
                                                                </div>
                                                            </div>
                                                            <div class="form-group " id="divCorreo">
                                                                <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label ControlsForms #custom_class" id="lblCorreo" name="lblCorreo" default_label="Correo:">Correo:</label>
                                                                <div class="input-group col-lg-8 col-md-8 col-sm-8 col-xs-8 ">
                                                                    <asp:TextBox ID="txtCorreo" validationgroup="vgRegistroPeticionario" runat="server"
                                                                        CssClass="form-control input-xs TextBoxFramework"></asp:TextBox>
                                                                    <asp:RegularExpressionValidator ID="revEmail" runat="server" ErrorMessage="El correo electrónico no es válido" CssClass="text-danger" Display="Dynamic"
                                                                        ControlToValidate="txtCorreo" ValidationExpression="^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$" />
                                                                    <asp:RequiredFieldValidator ID="rfvEmail" runat="server" validationgroup="vgRegistroPeticionario" ErrorMessage="El campo de correo electrónico es obligatorio" CssClass="text-danger" Display="Dynamic"
                                                                        ControlToValidate="txtCorreo"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                            <div class="form-group " id="divTelefono">
                                                                <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label ControlsForms #custom_class" id="lblTelefono" name="lblTelefono" default_label="Teléfono:">Teléfono:</label>
                                                                <div class="input-group col-lg-8 col-md-8 col-sm-8 col-xs-8 ">
                                                                    <asp:TextBox ID="txtTelefono" runat="server" validationgroup="vgRegistroPeticionario"
                                                                        CssClass="form-control input-xs TextBoxFramework"></asp:TextBox>
                                                                    <asp:RegularExpressionValidator ID="revTelefono" runat="server" validationgroup="vgRegistroPeticionario" ErrorMessage="El número de teléfono no es válido" CssClass="text-danger" Display="Dynamic"
                                                                        ControlToValidate="txtTelefono" ValidationExpression="^\d{10}$" />
                                                                    <asp:RequiredFieldValidator ID="rfvTelefono" runat="server" validationgroup="vgRegistroPeticionario" ErrorMessage="El campo de teléfono es obligatorio" CssClass="text-danger" Display="Dynamic"
                                                                        ControlToValidate="txtTelefono"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                            <div class="form-group " id="divDireccion">
                                                                <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label ControlsForms #custom_class" id="lblDireccion" name="lblDireccion" default_label="Dirección:">Dirección:</label>
                                                                <div class="input-group col-lg-8 col-md-8 col-sm-8 col-xs-8 ">
                                                                    <asp:TextBox ID="txtDireccion" runat="server" validationgroup="vgRegistroPeticionario"
                                                                        CssClass="form-control input-xs TextBoxFramework"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="rfvDireccion" runat="server" validationgroup="vgRegistroPeticionario" ErrorMessage="El campo de dirección es obligatorio" CssClass="text-danger" Display="Dynamic"
                                                                        ControlToValidate="txtDireccion"></asp:RequiredFieldValidator>
                                                                    <asp:RegularExpressionValidator ID="revDireccion" runat="server" validationgroup="vgRegistroPeticionario" ErrorMessage="La dirección no es válida" CssClass="text-danger" Display="Dynamic"
                                                                        ControlToValidate="txtDireccion" ValidationExpression="^[\w\s\.,'-]{10,}$" />
                                                                </div>
                                                            </div>
                                                            <div class="form-group" id="divNacionalidad">
                                                                <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label ControlsForms #custom_class" id="lblNacionalidad" name="Nacionalidad:">Nacionalidad:</label>
                                                                <div class="input-group input-group-sm">
                                                                    <asp:HiddenField ID="hddCodigoNacionalidad" runat="server" />
                                                                    <asp:TextBox ID="txtNacionalidad" runat="server" validationgroup="vgRegistroPeticionario" CssClass="form-control TextBoxCatalogo inputSuccess"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="rfvNacionalidad" runat="server" validationgroup="vgRegistroPeticionario" ControlToValidate="txtNacionalidad" ErrorMessage="La Nacionalidad es obligatoria" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                    <span class="input-group-btn">
                                                                        <button type="button" id="btn_LimpiarNacionalidad" onclick="limpiarCampo('<%= txtNacionalidad.ClientID %>')" class="btn btn-default btn-medium">
                                                                            <i class="fa fa-close"></i>
                                                                        </button>
                                                                        <button type="button" id="btn_Nacionalidad" class="btn btn-success btn-medium btn-margin-catalogo btn-file" data-toggle="modal" data-target="#divmodalCatalogo" usehttppost="0" webapplication="" usedatasource="0">
                                                                            <i class="fa fa-search"></i>&nbsp;
                                                                        </button>
                                                                    </span>
                                                                </div>
                                                            </div>
                                                            <div class="form-group" id="divPais">
                                                                <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label ControlsForms #custom_class" id="lblPais" name="País:">País:</label>
                                                                <div class="input-group input-group-sm">
                                                                    <asp:HiddenField ID="hddCodigoPais" runat="server" />
                                                                    <asp:TextBox ID="txtPais" runat="server" validationgroup="vgRegistroPeticionario" CssClass="form-control TextBoxCatalogo inputSuccess"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="rfvPais" runat="server" validationgroup="vgRegistroPeticionario" ControlToValidate="txtPais" ErrorMessage="El Pais es obligatorio" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                    <span class="input-group-btn">
                                                                        <button type="button" id="btn_LimpiarPais" onclick="limpiarCampo('<%= txtPais.ClientID %>')" class="btn btn-default btn-medium">
                                                                            <i class="fa fa-close"></i>
                                                                        </button>
                                                                        <button type="button" id="btn_Pais" class="btn btn-success btn-medium btn-margin-catalogo btn-file" data-toggle="modal" data-target="#divmodalCatalogo" usehttppost="0" webapplication="" usedatasource="0">
                                                                            <i class="fa fa-search"></i>&nbsp;
                                                                        </button>
                                                                    </span>
                                                                </div>
                                                            </div>
                                                            <div class="form-group" id="divDepartamento">
                                                                <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label ControlsForms #custom_class" id="lblDepartamento" name="Departamento:">Departamento:</label>
                                                                <div class="input-group input-group-sm">
                                                                    <asp:HiddenField ID="hddCodigoDepartamento" runat="server" />
                                                                    <asp:TextBox ID="txtDepartamento" runat="server" validationgroup="vgRegistroPeticionario" CssClass="form-control TextBoxCatalogo inputSuccess"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="rfvDepartamento" runat="server" validationgroup="vgRegistroPeticionario" ControlToValidate="txtDepartamento" ErrorMessage="El Departamento es obligatorio" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                    <span class="input-group-btn">
                                                                        <button type="button" id="btn_LimpiarDepartamento" onclick="limpiarCampo('<%= txtDepartamento.ClientID %>')" class="btn btn-default btn-medium">
                                                                            <i class="fa fa-close"></i>
                                                                        </button>
                                                                        <button type="button" id="btn_Departamento" class="btn btn-success btn-medium btn-margin-catalogo btn-file" data-toggle="modal" data-target="#divmodalCatalogo" usehttppost="0" webapplication="" usedatasource="0">
                                                                            <i class="fa fa-search"></i>&nbsp;
                                                                        </button>
                                                                    </span>
                                                                </div>
                                                            </div>
                                                            <div class="form-group" id="divCiudad">
                                                                 <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label ControlsForms #custom_class" id="lblCiudad" name="Ciudad:">Ciudad:</label>
                                                                 <div class="input-group input-group-sm">
                                                                     <asp:HiddenField ID="hddCodigoCiudad" runat="server" />
                                                                     <asp:TextBox ID="txtCiudad" runat="server" validationgroup="vgRegistroPeticionario" CssClass="form-control TextBoxCatalogo inputSuccess"></asp:TextBox>
                                                                     <asp:RequiredFieldValidator ID="rfvCiudad" runat="server" validationgroup="vgRegistroPeticionario" ControlToValidate="txtCiudad" ErrorMessage="El Ciudad es obligatorio" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                     <span class="input-group-btn">
                                                                         <button type="button" id="btn_LimpiarCiudad" onclick="limpiarCampo('<%= txtCiudad.ClientID %>')" class="btn btn-default btn-medium">
                                                                             <i class="fa fa-close"></i>
                                                                         </button>
                                                                         <button type="button" id="btn_Ciudad" class="btn btn-success btn-medium btn-margin-catalogo btn-file" data-toggle="modal" data-target="#divmodalCatalogo" usehttppost="0" webapplication="" usedatasource="0">
                                                                             <i class="fa fa-search"></i>&nbsp;
                                                                         </button>
                                                                     </span>
                                                                 </div>
                                                             </div>
                                                            <div class="form-group" id="divCentroPoblado">
                                                                <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label ControlsForms #custom_class" id="lblCentroPoblado" name="Centro Poblado:">Centro Poblado:</label>
                                                                <div class="input-group input-group-sm">
                                                                    <asp:HiddenField ID="hddCodigoCentroPoblado" runat="server" />
                                                                    <asp:TextBox ID="txtCentroPoblado" runat="server" validationgroup="vgRegistroPeticionario" CssClass="form-control TextBoxCatalogo inputSuccess"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="rfvCentroPoblado" runat="server" validationgroup="vgRegistroPeticionario" ControlToValidate="txtCentroPoblado" ErrorMessage="El CentroPoblado es obligatorio" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                    <span class="input-group-btn">
                                                                        <button type="button" id="btn_LimpiarCentroPoblado" onclick="limpiarCampo('<%= txtCentroPoblado.ClientID %>')" class="btn btn-default btn-medium">
                                                                            <i class="fa fa-close"></i>
                                                                        </button>
                                                                        <button type="button" id="btn_CentroPoblado" class="btn btn-success btn-medium btn-margin-catalogo btn-file" data-toggle="modal" data-target="#divmodalCatalogo" usehttppost="0" webapplication="" usedatasource="0">
                                                                            <i class="fa fa-search"></i>&nbsp;
                                                                        </button>
                                                                    </span>
                                                                </div>
                                                            </div>
                                                            <div class="form-group " id="divCondicionDiscapacidad">
                                                                <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label ControlsForms #custom_class" id="lblCondicionDiscapacidad" name="lblCondicionDiscapacidad" default_label="Condicion Discapacidad ">¿Condición de discapacidad? </label>
                                                                <div class="input-group col-lg-8 col-md-8 col-sm-8 col-xs-8 ">
                                                                    <asp:CheckBox ID="chkCondicionDiscapacidad" validationgroup="vgRegistroPeticionario" runat="server" CssClass="form-control input-xs TextBoxFramework"></asp:CheckBox>
                                                                </div>
                                                            </div>
                                                            <div class="form-group" id="divSituacionDiscapacidad">
                                                                <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label ControlsForms #custom_class" id="lblSituacionDiscapacidad" name="Situacion Discapacidad:">Situación de discapacidad:</label>
                                                                <div class="input-group input-group-sm">
                                                                    <asp:HiddenField ID="hddCodigoSituacionDiscapacidad" runat="server" />
                                                                    <asp:TextBox ID="txtSituacionDiscapacidad" runat="server" validationgroup="vgRegistroPeticionario" CssClass="form-control TextBoxCatalogo inputSuccess"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="rfvSituacionDiscapacidad" runat="server" validationgroup="vgRegistroPeticionario" ControlToValidate="txtSituacionDiscapacidad" ErrorMessage="El SituacionDiscapacidad es obligatorio" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                    <span class="input-group-btn">
                                                                        <button type="button" id="btn_LimpiarSituacionDiscapacidad" onclick="limpiarCampo('<%= txtSituacionDiscapacidad.ClientID %>')" class="btn btn-default btn-medium">
                                                                            <i class="fa fa-close"></i>
                                                                        </button>
                                                                        <button type="button" id="btn_SituacionDiscapacidad" class="btn btn-success btn-medium btn-margin-catalogo btn-file" data-toggle="modal" data-target="#divmodalCatalogo" usehttppost="0" webapplication="" usedatasource="0">
                                                                            <i class="fa fa-search"></i>&nbsp;
                                                                        </button>
                                                                    </span>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="columna">
                                                    <div class="row">
                                                        <div class="col-md-10">
                                                            <h3>Datos enfoques diferenciales y de género</h3>
                                                            <div class="form-group " id="divGrupoEtnicoBln2" >
                                                                <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label ControlsForms #custom_class"
                                                                    id="lblGrupoEtnicoBln2" name="lblGrupoEtnicoBln"
                                                                    default_label="¿Se reconoce como miembro de algún grupo étnico?">
                                                                    ¿Se reconoce como miembro de algún grupo étnico?
                                                                </label>
                                                                <div class="input-group col-lg-8 col-md-8 col-sm-8 col-xs-8 ">
                                                                    <asp:RadioButtonList ID="rblGrupoEtnicoBln2" runat="server" RepeatDirection="Horizontal">
                                                                        <asp:ListItem Text="Sí" Value="1"></asp:ListItem>
                                                                        <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                </div>
                                                            </div>
                                                            <div class="form-group" id="divGrupoEtnico2">
                                                                <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label ControlsForms #custom_class" id="lblGrupoEtnico2" name="Grupo étnico:">Grupo étnico:</label>
                                                                <div class="input-group input-group-sm">
                                                                    <asp:HiddenField ID="hddCodigoGrupoEtnico2" runat="server" />
                                                                    <asp:TextBox ID="txtGrupoEtnico2" runat="server" validationgroup="vgRegistroPeticionario" CssClass="form-control TextBoxCatalogo inputSuccess"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="rfvGrupoEtnico2" runat="server" validationgroup="vgRegistroPeticionario" ControlToValidate="txtGrupoEtnico2" ErrorMessage="El Grupo étnico es obligatorio" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                    <span class="input-group-btn">
                                                                        <button type="button" id="btn_LimpiarGrupoEtnico2" onclick="limpiarCampo('<%= txtGrupoEtnico2.ClientID %>')" class="btn btn-default btn-medium">
                                                                            <i class="fa fa-close"></i>
                                                                        </button>
                                                                        <button type="button" id="btn_GrupoEtnico2" class="btn btn-success btn-medium btn-margin-catalogo btn-file" data-toggle="modal" data-target="#divmodalCatalogo" usehttppost="0" webapplication="" usedatasource="0">
                                                                            <i class="fa fa-search"></i>&nbsp;
                                                                        </button>
                                                                    </span>
                                                                </div>
</div>
                                                            <div class="form-group" id="divSexoAsignado2">
                                                                 <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label ControlsForms #custom_class" id="lblSexoAsignado2" name="lblSexoAsignado" default_label="Sexo asignado:">Sexo asignado:</label>
                                                                 <div class="input-group input-group-sm">
                                                                     <asp:HiddenField ID="hddSexoAsignado2" runat="server" />
                                                                     <asp:TextBox ID="txtSexoAsignado2" ReadOnly="true" runat="server" validationgroup="vgRegistroPeticionario" CssClass="form-control TextBoxCatalogo inputSuccess"></asp:TextBox>
                                                                     <asp:RequiredFieldValidator ID="rfvSexoAsignado2" runat="server" validationgroup="vgRegistroPeticionario" ControlToValidate="txtSexoAsignado" ErrorMessage="El sexo asignado es obligatorio" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                     <span class="input-group-btn">
                                                                         <button type="button" id="btn_LimpiarSexoAsignado2" onclick="limpiarCampo('<%= txtSexoAsignado2.ClientID %>')" class="btn btn-default btn-medium">
                                                                             <i class="fa fa-close"></i>
                                                                         </button>
                                                                         <button type="button"  id="btn_SexoAsignado2" class="btn btn-success btn-medium btn-margin-catalogo btn-file" usehttppost="0" webapplication="" usedatasource="0">
                                                                             <i class="fa fa-search"></i>&nbsp;
                                                                         </button>
                                                                     </span>
                                                                 </div>
                                                             </div>
                                                            <div class="form-group" id="divIdentidadGenero2">
                                                                <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label ControlsForms #custom_class" id="lblIdentidadGenero2" name="lblIdentidadGenero" default_label="Identidad de género:">Identidad de género:</label>
                                                                <div class="input-group input-group-sm">
                                                                    <asp:HiddenField ID="hddIdentidadGenero2" runat="server" />
                                                                    <asp:TextBox ID="txtIdentidadGenero2" ReadOnly="true" runat="server" validationgroup="vgRegistroPeticionario" CssClass="form-control TextBoxCatalogo inputSuccess"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="rfvIdentidadGenero2" runat="server" validationgroup="vgRegistroPeticionario" ControlToValidate="txtIdentidadGenero2" ErrorMessage="La identidad de género es obligatoria" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                    <span class="input-group-btn">
                                                                        <button type="button"  id="btn_LimpiarIdentidadGenero2" onclick="limpiarCampo('<%= txtIdentidadGenero2.ClientID %>')" class="btn btn-default btn-medium">
                                                                            <i class="fa fa-close"></i>
                                                                        </button>
                                                                        <button type="button"  id="btn_IdentidadGenero2" class="btn btn-success btn-medium btn-margin-catalogo btn-file" usehttppost="0" webapplication="" usedatasource="0">
                                                                            <i class="fa fa-search"></i>&nbsp;
                                                                        </button>
                                                                    </span>
                                                                </div>
                                                            </div>
                                                            <div class="form-group" id="divOrientacionSexual2">
                                                                <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label ControlsForms #custom_class" id="lblOrientacionSexual2" name="lblOrientacionSexual" default_label="Orientación sexual:">Orientación sexual:</label>
                                                                <div class="input-group input-group-sm">
                                                                    <asp:HiddenField ID="hddOrientacionSexual2" runat="server" />
                                                                    <asp:TextBox ID="txtOrientacionSexual2" ReadOnly="true" runat="server" validationgroup="vgRegistroPeticionario" CssClass="form-control TextBoxCatalogo inputSuccess"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="rfvOrientacionSexual2" runat="server" validationgroup="vgRegistroPeticionario" ControlToValidate="txtOrientacionSexual2" ErrorMessage="La orientación sexual es obligatoria" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                    <span class="input-group-btn">
                                                                        <button type="button"  id="btn_LimpiarOrientacionSexual2" onclick="limpiarCampo('<%= txtOrientacionSexual2.ClientID %>')" class="btn btn-default btn-medium">
                                                                            <i class="fa fa-close"></i>
                                                                        </button>
                                                                        <button type="button"  id="btn_OrientacionSexual2" class="btn btn-success btn-medium btn-margin-catalogo btn-file" usehttppost="0" webapplication="" usedatasource="0">
                                                                            <i class="fa fa-search"></i>&nbsp;
                                                                        </button>
                                                                    </span>
                                                                </div>
                                                            </div>
                                                            <div class="form-group" id="divExpresionGenero2">
                                                                <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label ControlsForms #custom_class" id="lblExpresionGenero2" name="lblExpresionGenero" default_label="Expresión de género :">Expresión de género :</label>
                                                                <div class="input-group input-group-sm">
                                                                    <asp:HiddenField ID="hddExpresionGenero2" runat="server" />
                                                                    <asp:TextBox ID="txtExpresionGenero2" ReadOnly="true" runat="server" validationgroup="vgRegistroPeticionario" CssClass="form-control TextBoxCatalogo inputSuccess"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="rfvExpresionGenero2" runat="server" validationgroup="vgRegistroPeticionario" ControlToValidate="txtExpresionGenero2" ErrorMessage="La expresión de género es obligatoria" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                    <span class="input-group-btn">
                                                                        <button type="button" id="btn_LimpiarExpresionGenero2" onclick="limpiarCampo('<%= txtExpresionGenero2.ClientID %>')" class="btn btn-default btn-medium">
                                                                            <i class="fa fa-close"></i>
                                                                        </button>
                                                                        <button type="button" id="btn_ExpresionGenero2" class="btn btn-success btn-medium btn-margin-catalogo btn-file" usehttppost="0" webapplication="" usedatasource="0">
                                                                            <i class="fa fa-search"></i>&nbsp;
                                                                        </button>
                                                                    </span>
                                                                </div>
                                                            </div>
                                                            <div class="form-group" id="divRangoEdad2">
                                                                <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label ControlsForms #custom_class" id="lblRangoEdad2" name="lblRangoEdad" default_label="Rango de edad:">Rango de edad:</label>
                                                                <div class="input-group input-group-sm">
                                                                    <asp:HiddenField ID="hddRangoEdad2" runat="server" />
                                                                    <asp:TextBox ID="txtRangoEdad2" ReadOnly="true" runat="server" validationgroup="vgRegistroPeticionario" CssClass="form-control TextBoxCatalogo inputSuccess"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="rfvRangoEdad2" runat="server" validationgroup="vgRegistroPeticionario" ControlToValidate="txtRangoEdad2" ErrorMessage="El rango de edad es obligatorio" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                    <span class="input-group-btn">
                                                                        <button type="button"  id="btn_LimpiarRangoEdad2" onclick="limpiarCampo('<%= txtRangoEdad2.ClientID %>')" class="btn btn-default btn-medium">
                                                                            <i class="fa fa-close"></i>
                                                                        </button>
                                                                        <button type="button"  id="btn_RangoEdad2" class="btn btn-success btn-medium btn-margin-catalogo btn-file" usehttppost="0" webapplication="" usedatasource="0">
                                                                            <i class="fa fa-search"></i>&nbsp;
                                                                        </button>
                                                                    </span>
                                                                </div>
                                                            </div>
                                                            <div class="form-group" id="divGrupo">
                                                                <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label ControlsForms #custom_class" id="lblGrupo" name="Grupo:">Grupo:</label>
                                                                <div class="input-group input-group-sm">
                                                                    <asp:HiddenField ID="hddCodigoGrupo" runat="server" />
                                                                    <asp:TextBox ID="txtGrupo" runat="server" validationgroup="vgRegistroPeticionario" CssClass="form-control TextBoxCatalogo inputSuccess"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="rfvGrupo" runat="server" validationgroup="vgRegistroPeticionario" ControlToValidate="txtGrupo" ErrorMessage="El Grupo es obligatorio" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                    <span class="input-group-btn">
                                                                        <button type="button" id="btn_LimpiarGrupo" onclick="limpiarCampo('<%= txtGrupo.ClientID %>')" class="btn btn-default btn-medium">
                                                                            <i class="fa fa-close"></i>
                                                                        </button>
                                                                        <button type="button" id="btn_Grupo" class="btn btn-success btn-medium btn-margin-catalogo btn-file" data-toggle="modal" data-target="#divmodalCatalogo" usehttppost="0" webapplication="" usedatasource="0">
                                                                            <i class="fa fa-search"></i>&nbsp;
                                                                        </button>
                                                                    </span>
                                                                </div>
                                                            </div>
                                                            <div class="form-group" id="divSubgrupo">
                                                                <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label ControlsForms #custom_class" id="lblSubgrupo" name="Subgrupo:">Subgrupo:</label>
                                                                <div class="input-group input-group-sm">
                                                                    <asp:HiddenField ID="hddCodigoSubgrupo" runat="server" />
                                                                    <asp:TextBox ID="txtSubgrupo" runat="server" validationgroup="vgRegistroPeticionario" CssClass="form-control TextBoxCatalogo inputSuccess"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="rfvSubgrupo" runat="server" validationgroup="vgRegistroPeticionario" ControlToValidate="txtSubgrupo" ErrorMessage="El Subgrupo es obligatorio" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                    <span class="input-group-btn">
                                                                        <button type="button" id="btn_LimpiarSubgrupo" onclick="limpiarCampo('<%= txtSubgrupo.ClientID %>')" class="btn btn-default btn-medium">
                                                                            <i class="fa fa-close"></i>
                                                                        </button>
                                                                        <button type="button" id="btn_Subgrupo" class="btn btn-success btn-medium btn-margin-catalogo btn-file" data-toggle="modal" data-target="#divmodalCatalogo" usehttppost="0" webapplication="" usedatasource="0">
                                                                            <i class="fa fa-search"></i>&nbsp;
                                                                        </button>
                                                                    </span>
                                                                </div>
                                                            </div>
                                                            <div class="form-group" id="divComunidad">
                                                                <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label ControlsForms #custom_class" id="lblComunidad" name="Comunidad:">Comunidad:</label>
                                                                <div class="input-group input-group-sm">
                                                                    <asp:HiddenField ID="hddCodigoComunidad" runat="server" />
                                                                    <asp:TextBox ID="txtComunidad" runat="server" validationgroup="vgRegistroPeticionario" CssClass="form-control TextBoxCatalogo inputSuccess"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="rfvComunidad" runat="server" validationgroup="vgRegistroPeticionario" ControlToValidate="txtComunidad" ErrorMessage="El Comunidad es obligatorio" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                    <span class="input-group-btn">
                                                                        <button type="button" id="btn_LimpiarComunidad" onclick="limpiarCampo('<%= txtComunidad.ClientID %>')" class="btn btn-default btn-medium">
                                                                            <i class="fa fa-close"></i>
                                                                        </button>
                                                                        <button type="button" id="btn_Comunidad" class="btn btn-success btn-medium btn-margin-catalogo btn-file" data-toggle="modal" data-target="#divmodalCatalogo" usehttppost="0" webapplication="" usedatasource="0">
                                                                            <i class="fa fa-search"></i>&nbsp;
                                                                        </button>
                                                                    </span>
                                                                </div>
                                                            </div>
                                                            
                                                            
                                                            
                                                        </div>
                                                    </div>
                                            </div>
                                            </div>
                                        </div>
                                        </div>
                                    </div>
                                    <div id="menu3" class="tab-pane fade">
                                        <div id="divMenu3">
                                            <h3 class="TituloAcordeon">
                                                <a data-toggle="collapse" href="#divCollapse3" style="color: black; text-decoration: none;">
                                                    <i class="fa fa-chevron-circle-down"></i>
                                                    &nbsp;Clasificación de la petición
                                                </a>
                                            </h3>
                                            <div id="divCollapse3" class="panel-collapse collapse in">
                                            <div class="row">
                                                <div class="col-md-6 ">
                                                    <div class="form-group " id="divAsesorias">
                                                        <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label ControlsForms #custom_class" id="lblAsesoria" name="lblAsesoria" default_label="Asesorías:">Asesorías:</label>
                                                        <div class="input-group col-lg-8 col-md-8 col-sm-8 col-xs-8 help-text">
                                                            <asp:TextBox ID="txtAsesoria" runat="server"  TextMode="MultiLine" CssClass="form-control imput-xs TextBoxFramework" ReadOnly="true" 
                                                                Text="La asesoría consiste en orientar al peticionario en el ejercicio y defensa de los derechos humanos, ante las autoridades competentes o ante las entidades de carácter privado."></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group" id="divTipoPeticion">
                                                        <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label ControlsForms #custom_class" id="lblTipoPeticion" name="lblTipoPeticion" default_label="Tipo de petición:">Tipo de petición:</label>
                                                        <div class="input-group input-group-sm">
                                                            <asp:HiddenField ID="hddCodigoTipoPeticion" runat="server" />
                                                            <asp:TextBox ID="txtTipoPeticion" ReadOnly="false"
                                                                runat="server" 
                                                                validationgroup="vgClasificacionPeticion" 
                                                                CssClass="form-control TextBoxCatalogo inputSuccess">
                                                            </asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvTipoPeticion" 
                                                                runat="server" ControlToValidate="txtTipoPeticion" 
                                                                validationgroup="vgClasificacionPeticion"
                                                                ErrorMessage="El tipo de petición es obligatorio" 
                                                                ToolTip="El tipo de petición es obligatorio" 
                                                                CssClass="text-danger" Display="Dynamic">

                                                            </asp:RequiredFieldValidator>
                                                            <span class="input-group-btn">
                                                                <button type="button" id="btn_LimpiarTipoPeticion" onclick="limpiarCampo('<%= txtTipoPeticion.ClientID %>')" class="btn btn-default btn-medium">
                                                                    <i class="fa fa-close"></i>
                                                                </button>
                                                                <button type="button" id="btn_TipoPeticion" class="btn btn-success btn-medium btn-margin-catalogo btn-file"data-toggle="modal" data-target="#divmodalCatalogo" usehttppost="0" webapplication="" usedatasource="0">
                                                                    <i class="fa fa-search"></i>&nbsp;
                                                                </button>
                                                            </span>
                                                        </div>
                                                    </div>
                                                    <div class="form-group" id="divAreaDerecho">
                                                        <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label ControlsForms #custom_class" id="lblAreaDerecho" name="lblAreaDerecho" default_label="Área de derecho:">Área de derecho:</label>
                                                        <div class="input-group input-group-sm">
                                                            <asp:HiddenField ID="hddCodigoAreaDerecho" runat="server" />
                                                            <asp:TextBox ID="txtAreaDerecho" runat="server" ReadOnly="false"
                                                                validationgroup="vgClasificacionPeticion" CssClass="form-control TextBoxCatalogo inputSuccess"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rqrAreaDerecho" runat="server" validationgroup="vgClasificacionPeticion" ControlToValidate="txtAreaDerecho" ErrorMessage="El área de derecho es obligatorio" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                                                            <span class="input-group-btn">
                                                                <button type="button" id="btn_LimpiarAreaDerecho" onclick="limpiarCampo('<%= txtAreaDerecho.ClientID %>')" class="btn btn-default btn-medium">
                                                                    <i class="fa fa-close"></i>
                                                                </button>
                                                                <button type="button" id="btn_AreaDerecho" class="btn btn-success btn-medium btn-margin-catalogo btn-file" data-toggle="modal" data-target="#divmodalCatalogo"usehttppost="0" webapplication="" usedatasource="0">
                                                                    <i class="fa fa-search"></i>&nbsp;
                                                                </button>
                                                            </span>
                                                        </div>
                                                    </div>
                                                    
                                                    <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label ControlsForms #custom_class" id="lbltablaDerechos" name="lbltablaDerechos" default_label="Área de derecho:">Derechos:</label>
                                                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 col-lg-offset-4 " id="divDerechosTabla">
                                                      <table id="tablaDerechos" class="table TextBoxCatalogo ">
                                                                <thead>
                                                                    <tr>
                                                                        <th></th>
                                                                        <th>Acciones</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                </tbody>
                                                            </table>
                                                    <button type="button" class="btn btn-default btn-medium" onclick="agregarDerecho()">
                                                         <i class="fa fa-plus"></i>
                                                     </button>  
                                                         <asp:HiddenField ID="hiddenDerechos" runat="server" />
                                                    <hr/>
                                                    </div>
                                                    <div class="form-group " id="divDescripcionAsesoria">
                                                        <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label ControlsForms #custom_class" id="lblDescripcionAsesoria" name="lblDescripcionAsesoria" default_label="Descripción de asesoría:">Descripción de asesoría:</label>
                                                        <div class="input-group col-lg-8 col-md-8 col-sm-8 col-xs-8 ">
                                                            <asp:TextBox ID="txtDescripcionAsesoria" runat="server" ReadOnly="false"
                                                                validationgroup="vgClasificacionPeticion" TextMode="MultiLine" CssClass="form-control imput-xs TextBoxFramework"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rqfDecripcionAsesoria" runat="server" validationgroup="vgClasificacionPeticion" ControlToValidate="txtDescripcionAsesoria" ErrorMessage="La descripción de la asesoría es obligatoria" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>

                                                    <div class="form-group " id="divObservaciones">
                                                        <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label ControlsForms #custom_class" id="lblObservaciones" name="lblObservaciones" default_label="Observaciones:">Observaciones:</label>
                                                        <div class="input-group col-lg-8 col-md-8 col-sm-8 col-xs-8 ">
                                                            <asp:TextBox ID="txtObservaciones" 
                                                                MaxLength="2000"
                                                                runat="server" validationgroup="vgClasificacionPeticion" TextMode="MultiLine" CssClass="form-control imput-xs TextBoxFramework"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rqfObservaciones" runat="server" validationgroup="vgClasificacionPeticion" ControlToValidate="txtObservaciones" ErrorMessage="La observación es obligatoria" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                    <div class="form-group " id="divAsesoriaEscrita">
                                                        <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label ControlsForms #custom_class" id="lblAsesoriaEscrita" name="lblAsesoriaEscrita" default_label="¿La asesoría debe generar respuesta por escrito?">¿La asesoría debe generar respuesta por escrito?</label>
                                                        <div class="input-group col-lg-8 col-md-8 col-sm-8 col-xs-8 ">
                                                            <asp:RadioButton ID="respuestaEscritaSi" runat="server" ReadOnly="true" GroupName="respuestaEscrita" Text="&nbsp;&nbsp;Sí" />
                                                            <asp:RadioButton ID="respuestaEscritaNo" runat="server" ReadOnly="true" GroupName="respuestaEscrita" Text="&nbsp;&nbsp;No" />

                                                        </div>
                                                    </div>
                                                    <div class="form-group" id="divConclusionAsesoria">
                                                        <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label ControlsForms #custom_class" id="lblConclusionAsesoria" name="lblConclusionAsesoria" default_label="Conclusión Asesoria:">Conclusión Asesoria:</label>
                                                        <div class="input-group input-group-sm">
                                                            <asp:HiddenField ID="hddCodigoConclusionAsesoria" runat="server" />
                                                            <asp:TextBox ID="txtConclusionAsesoria" runat="server" ReadOnly="false"
                                                                validationgroup="vgClasificacionPeticion" CssClass="form-control TextBoxCatalogo inputSuccess"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rqfConclusionAsesoria" runat="server" validationgroup="vgClasificacionPeticion" ControlToValidate="txtConclusionAsesoria" ErrorMessage="La conclusión de la asesoría es obligatoria" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                                                            <span class="input-group-btn">
                                                                <button type="button" id="btn_LimpiarConclusionAsesoria" onclick="limpiarCampo('<%= txtConclusionAsesoria.ClientID %>')" class="btn btn-default btn-medium">
                                                                    <i class="fa fa-close"></i>
                                                                </button>
                                                                <button type="button" id="btn_ConclusionAsesoria" class="btn btn-success btn-medium btn-margin-catalogo btn-file" data-toggle="modal" data-target="#divmodalCatalogo" usehttppost="0" webapplication="" usedatasource="0">
                                                                    <i class="fa fa-search"></i>&nbsp;
                                                                </button>
                                                            </span>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div id="menu4" class="tab-pane fade">
                                        <div id="divMenu4">
                                            <h3 class="TituloAcordeon">
                                                <a data-toggle="collapse" href="#divCollapse3" style="color: black; text-decoration: none;">
                                                    <i class="fa fa-chevron-circle-down"></i>
                                                    &nbsp;Decisión
                                                </a>
                                            </h3>
                                            <div id="divCollapse4" class="panel-collapse collapse in">
                                            <div class="row">
                                                <div class="col-md-6">
                                                    <div class="form-group" id="divDecision">
                                                        <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label ControlsForms #custom_class" id="lblDecision" name="lblDecision" default_label="Decisión:">Decisión:</label>
                                                        <div class="input-group input-group-sm">
                                                            <asp:HiddenField ID="hddCodigoDecision" runat="server" />
                                                            <asp:TextBox ID="txtDecision" runat="server" validationgroup="vgDecision" Text="" CssClass="form-control TextBoxCatalogo error inputWarning"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvDecision" runat="server" validationgroup="vgDecision" ControlToValidate="txtDecision" ErrorMessage="La decisión es obligatoria" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                                                            <span class="input-group-btn">
                                                                <button type="button" id="btn_LimpiarDecision" onclick="limpiarCampo('<%= txtDecision.ClientID %>')" class="btn btn-default btn-medium">
                                                                    <i class="fa fa-close"></i>
                                                                </button>
                                                                <button type="button"  id="btn_Decision" class="btn btn-success btn-medium btn-margin-catalogo btn-file" data-toggle="modal" data-target="#divmodalCatalogo"usehttppost="0" webapplication="" usedatasource="0">
                                                                    <i class="fa fa-search"></i>&nbsp;
                                                                </button>
                                                            </span>
                                                        </div>
                                                    </div>
                                                    <div class="form-group " id="divComentarios">
                                                        <label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label ControlsForms #custom_class" id="lblComentarios" name="lblComentarios" default_label="Comentarios:">Comentarios:</label>
                                                        <div class="input-group col-lg-8 col-md-8 col-sm-8 col-xs-8 ">
                                                            <asp:TextBox ID="txtComentarios" runat="server" validationgroup="vgDecision"  TextMode="MultiLine" CssClass="form-control imput-xs TextBoxFramework"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvComentarios" runat="server" validationgroup="vgDecision" ControlToValidate="txtComentarios" ErrorMessage="El comentario es obligatorio" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                                                        </div>
                                                      </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    
                                    <asp:HiddenField ID="txtCodigoSolicitud" runat="server" Value="3"></asp:HiddenField>

                                </div>
                            </div>
                        </div>
                          
                    </div>
                </div>
            </div>
        </div>
        </div>
            </form>
</body>
</html>
