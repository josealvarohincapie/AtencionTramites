<%@ Page Language="vb" AutoEventWireup="false" ValidateRequest="false" CodeBehind="RadicadoSolicitud.aspx.vb" Inherits="Web.RadicadoSolicitud" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script runat="server">
    Dim item As Integer = 0
</script>
<html xmlns="http://www.w3.org/1999/xhtml">
   <head runat="server">
      <title>Atención Trámites | Asesorías</title>
      <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
      <!-- Favicon -->
     <link rel="shortcut icon" href"../../Styles/images/favicon.ico">
      <!-- Bootstrap core CSS -->
      <link href="../../Plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
      <!-- Bootstrap theme -->
      <link href="../../Plugins/bootstrap/css/bootstrap-theme.min.css" rel="stylesheet" />
      <!-- Estilos  Datepicker jquery-ui -->
      <link href="../../Plugins/jquery-ui/jquery-ui.min.css" rel="stylesheet" />
      <link href="../../Plugins/jquery-ui/jquery-ui.theme.min.css" rel="stylesheet" />
      <!-- Custom styles for this template -->
      <link href="../../Styles/theme.css" rel="stylesheet" />
      <!-- Bootstrap core JavaScript -->
      <script src="../../Plugins/jquery/jquery.min.js" type="text/javascript"></script>
      <script src="../../Plugins/jquery/jquery.validate.min.js" type="text/javascript"></script>
      <script src="../../Plugins/bootstrap/js/bootstrap.min.js" type="text/javascript"></script>
      <script src="../../Plugins/bootstrap/js/bootstrap-datepicker.js" type="text/javascript"></script>
      <script src="../../Plugins/filestyle/bootstrap-filestyle.min.js" type="text/javascript"></script>
      <!-- Plugin Datepicker jquery-ui -->
      <script src="../../Plugins/jquery-ui/jquery-ui.min.js" type="text/javascript"></script>
      <script type="text/javascript" src="../../Scripts/RadicadoSolicitud.js"></script>
      <script type="text/javascript" src="../../Scripts/Metodos.js"></script>
      <script type="text/javascript" src="../../Scripts/ActividadDialog.js"></script>
      <script type="text/javascript" src="../../Scripts/Documentacion.js"></script>
      <script type="text/javascript" src="../../Scripts/Complementarios.js"></script>

      <script type="text/javascript">
          $(document).ready(function () {
              ValidarClienteDefinido();
          });
      </script>
       <script type="text/javascript">
           $(document).ready(function () {
               $("form").keypress(function (e) {
                   if (e.which == 13) {
                       return false;
                   }
               });
           });
       </script>

       <script type="text/javascript" language="JavaScript">

document.onkeydown = function (evt) { 
return (evt ? evt.which : event.keyCode) != 13; }



</script>

   </head>
   <body>
      <form id="form1" runat="server">
         <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
         <asp:UpdatePanel ID="UpdatePanelPrincipal" runat="server" UpdateMode="Always">
            <ContentTemplate>
               <nav class="navbar navbar-default">
                  <div class="collapse navbar-collapse">
                     <ul class="nav navbar-nav">
                        <li>
                           <asp:LinkButton ID="lbEnviar" runat="server" OnClientClick="return(validarEnviar());"> <img src="../../Styles/images/ult_send.png" alt="Enviar" /> Enviar</asp:LinkButton>
                        </li>
                        <li>
                           <asp:LinkButton ID="lbGuardar" runat="server"> <img src="../../Styles/images/save.png" alt="Enviar" /> Guardar</asp:LinkButton>
                        </li>
                        
                        <li>
                           <a href="#" id="ibNotas" runat="server" visible="false" data-toggle="modal" data-target="#modal-notas"> <img src="../../Styles/images/ult_notes.png" alt="Notas" /> Notas</a>
                        </li>

                        <li>
                           <a href="#" id="ibAdjuntos" runat="server" visible="false" data-toggle="modal" data-target="#modal-adjuntos"> <img src="../../Styles/images/ult_attachment.png" alt="Adjuntos" /> Adjuntos</a>
                        </li>

                        <li>
                            <a href="#" id="ibCorrespondenciaRecibida" runat="server" data-toggle="modal" data-target="#modal-correspondencia-recibida">
                            <img src="../../Styles/images/inbox.png" alt="Correspondencia Recibida" />
                            Correspondencia Recibida</a>
                        </li>

                        <li class="hidden">
                           <a href="#" id="ibAyuda"> <img src="../../Styles/images/ult_help.png" alt="Ayuda" /> Ayuda</a>
                        </li>
                     </ul>
                  </div>
               </nav>
               <div class="container">
                  <div class="header">
                     <table style="width: 100%">
                        <tr>
                           <td style="width: 30%">
                              <img src="../../Styles/images/logo_defensoria.png" style="width: 335px; height: 97px" alt="BANCOLDEX" />
                           </td>
                           <td style="width: 40%">
                              <h4>
                                 <asp:Label ID="lbProceso" runat="server" Text=""></asp:Label>
                              </h4>
                           </td>
                           <td style="width: 30%">
                              <ul style="list-style-type: none">
                                 <li>
                                    <asp:Label ID="lbEtapa" runat="server" Text="" Font-Bold="true"></asp:Label>
                                 </li>
                                 <li>
                                    <asp:Label ID="lbIncidente" runat="server" Text="" Font-Bold="true"></asp:Label>
                                 </li>
                                 <li>
                                    <asp:Label ID="lbFecha" runat="server" Text="" Font-Bold="true"></asp:Label>
                                 </li>
                                 <li>
                                    <asp:Label ID="lbUsuario" runat="server" Text="" Font-Bold="true"></asp:Label>
                                 </li>
                              </ul>
                           </td>
                        </tr>
                     </table>
                  </div>
                  <div role="tabpanel">
                     <!-- Nav tabs -->
                     <ul class="nav nav-tabs" role="tablist">
                        <li role="presentation" class="active main-tab"><a href="#solicitud" id="tab-solicitud" role="tab" data-toggle="tab">Solicitud </a></li>
                        <li role="presentation" class="main-tab"><a href="#documentacion" id="tab-documentacion" role="tab" data-toggle="tab">Documentación <span id="countDocumentacion" runat="server" class="badge"></span> </a></li>
                        <li role="presentation" class="main-tab"><a href="#complementarios" id="tab-complementarios" role="tab" data-toggle="tab">Codeudor / Avalista <span id="countComplementarios" runat="server" class="badge"></span> </a></li>
                     </ul>
                     <!-- Tab panes -->
                     <div class="tab-content">
                        <div role="tabpanel" class="tab-pane active" id="solicitud">
                           
                           <asp:HiddenField ID="FechaInicioEtapa" ClientIDMode="Static" runat="server" />
                           <asp:HiddenField ID="hfScrollbarPosition" ClientIDMode="Static" runat="server" Value="0" />
                           <asp:HiddenField ID="hfSelectedTab" ClientIDMode="Static" runat="server" Value="tab-solicitud" />
                           <asp:HiddenField ID="hfExtensionesValidas" ClientIDMode="Static" runat="server" />
                           <asp:HiddenField ID="SoloLectura" ClientIDMode="Static" runat="server" />
                           <asp:HiddenField ID="ClienteExiste" ClientIDMode="Static" runat="server" />
                           <asp:HiddenField ID="hfEtapaReceptora" ClientIDMode="Static" runat="server" />

                           <div class="panel panel-default">
                              <div class="panel-heading">Información de la solicitud</div>
                              <div class="panel-body">
                                 <div class="form-group col-xs-4">
                                    <span class="control-label">Área:</span>
                                    <asp:DropDownList ID="ddlArea" runat="server" DataTextField="DESCRIPCION" DataValueField="CODIGO" CssClass="form-control">
                                    </asp:DropDownList>
                                 </div>
                                 <div class="form-group col-xs-4">
                                    <span class="control-label">Usuario BPM:</span>
                                    <asp:TextBox ID="tbFuncionarioRespCli" runat="server" CssClass="form-control"></asp:TextBox>
                                 </div>
                                 <div class="form-group col-xs-4">
                                    <span class="control-label">Funcionario responsable del cliente/solicitud:</span>
                                    <asp:TextBox ID="tbFuncionarioRespSol" runat="server" CssClass="form-control"></asp:TextBox>
                                 </div>
                                 <div class="form-group col-xs-5">
                                    <span class="control-label">Tipo de producto:</span>
                                    <asp:DropDownList ID="ddlTipoProducto" runat="server" ToolTip="Tipo Producto" DataTextField="DESCRIPCION" DataValueField="CODIGO" CssClass="form-control" AutoPostBack="True">
                                    </asp:DropDownList>
                                 </div>
                                 <div id="dvValorDivisa" runat="server" visible="true" class="form-group col-xs-2">
                                    <span class="control-label">Monto de crédito:</span>
                                    <asp:TextBox ID="tbValorDivisa" runat="server" ToolTip="Valor divisa" CssClass="form-control" style="text-align:right" onchange="this.value=addCommas(this.value);"></asp:TextBox>
                                 </div>
                                 <div class="form-group col-xs-3" runat="server" id="dvDivisa">
                                    <span class="control-label">Moneda:</span>
                                    <asp:DropDownList ID="ddlDivisa" runat="server" ToolTip="Divisa" DataTextField="DESCRIPCION" DataValueField="CODIGO" CssClass="form-control">
                                    </asp:DropDownList>
                                 </div>
                                 <div class="form-group col-xs-2">
                                    <span class="control-label">Fecha solicitud:</span>
                                    <asp:TextBox ID="tbFechaSolicitud" ToolTip="Fecha solicitud" ReadOnly="true" runat="server" CssClass="form-control"></asp:TextBox>
                                 </div>
                                  <div class="col-md-4">
                                    <span class="control-label">Tipo persona:</span>
                                     <asp:RadioButtonList ID="rblTipoPersona" runat="server" AutoPostBack="true" type="radio" RepeatDirection="Horizontal">
                                         <asp:ListItem Value="1">Jurídico</asp:ListItem>
                                         <asp:ListItem Value="2">Natural</asp:ListItem>
                                     </asp:RadioButtonList>
                                  </div>
                                 <div class="col-md-8">
                                    <div class="row">
                                       <div class="form-group col-xs-12">
                                          <asp:CheckBox ID="chkParticipacion" CssClass="checkbox-inline" runat="server" AutoPostBack="true" Text="¿Incluye personas jurídicas con participación accionaria mayor al 5%?" />
                                       </div>
                                    </div>
                                 </div>
                              </div>
                           </div>
                           <div id="ProyectFinance" runat="server" class="panel panel-default" visible="false">
                              <div class="panel-heading">
                                 Proyect Finance con deudor definido:
                                 <asp:RadioButtonList ID="rblClienteDefinido" runat="server" type="radio" RepeatDirection="Horizontal" onclick="ValidarClienteDefinido();">
                                    <asp:ListItem Value="1">Si</asp:ListItem>
                                    <asp:ListItem Value="0">No</asp:ListItem>
                                 </asp:RadioButtonList>
                              </div>
                           </div>
                           <div class="panel panel-default" id="divInformacionDeudor">
                              <div class="panel-heading">
                                 Información del deudor
                                 <asp:Label ID="lblEstado" runat="server" Text="" ForeColor="DarkRed" Style="margin-left: 285px"></asp:Label>
                              </div>
                              <div class="panel-body">
                                 <div class="form-group col-xs-4">
                                    <span class="control-label">Tipo identificación:</span>
                                    <asp:DropDownList ID="ddlTipoIdentificacion" runat="server" AutoPostBack="true" DataTextField="DESCRIPCION" DataValueField="CODIGO" CssClass="form-control">
                                    </asp:DropDownList>
                                 </div>
                                 <div class="form-group col-xs-4">
                                    <span class="control-label">Número de identificación:</span>
                                    <asp:TextBox ID="tbIdentificacion" runat="server" CssClass="form-control" AutoPostBack="True"></asp:TextBox>
                                 </div>
                                 <div class="form-group col-xs-4">
                                    <span class="control-label">Razón social:</span>
                                       <asp:TextBox ID="tbNombre" runat="server" CssClass="form-control" AutoPostBack="True"></asp:TextBox>
                                 </div>
                                 <div class="form-group col-xs-4">
                                    <span class="control-label">País:</span>
                                    <asp:DropDownList ID="ddlPais" runat="server" DataTextField="DESCRIPCION" DataValueField="CODIGO" CssClass="form-control" AutoPostBack="True">
                                    </asp:DropDownList>
                                 </div>
                                 <div class="form-group col-xs-4">
                                    <span class="control-label">Ciudad:</span>
                                    <asp:DropDownList ID="ddlCiudad" runat="server" DataTextField="DESCRIPCION" DataValueField="CODIGO" CssClass="form-control">
                                    </asp:DropDownList>
                                 </div>
                                 <div class="form-group col-xs-4">
                                    <span class="control-label">Correo electrónico:</span>
                                    <asp:TextBox ID="tbCorreo" runat="server" CssClass="form-control" onchange="if(isEmail(this.value))return(true);else{alert('Correo inválido');this.value='';return(false);}"></asp:TextBox>
                                 </div>
                                 <div class="form-group col-xs-4">
                                    <span class="control-label">Dirección:</span>
                                    <asp:TextBox ID="tbDireccion" runat="server" CssClass="form-control"></asp:TextBox>
                                 </div>
                                 <div class="form-group col-xs-4">
                                    <span class="control-label">Teléfono:</span>
                                    <asp:TextBox ID="tbTelefono" runat="server" CssClass="form-control"></asp:TextBox>
                                 </div>
                                 <div class="form-group col-xs-4 center" style="padding-top: 20px">
                                    <span class="control-label"></span>
                                    <asp:Button id="btLimpiar" runat="server" CssClass="btn btn-danger btn-xs" ClientIDMode="Static" Text="Limpiar" />
                                 </div>                                 
                              </div>
                           </div>
                           <div class="panel panel-default" runat="server" id="divReportesCentinela" visible="false">
                              <div class="panel-heading">
                                 Revisión OCU
                              </div>
                              <div class="panel-body">

                                <div runat="server" id="dvUnidadCumplimiento">

                                    <asp:HiddenField ID="tbSemejanza" runat="server" />
                                    <asp:HiddenField ID="tbUrlReporte" runat="server" />

                                    <div class="form-group col-xs-4 alert alert-warning">
                                        <span class="control-label checkbox-inline">
                                          <h6>
                                             <span>Resultado Centinela: </span><asp:Label ID="lbTipoCoincidencia" runat="server"></asp:Label>
                                          </h6>
                                       </span>
                                    </div>

                                    <div class="form-group col-xs-1">
                                        <div onclick="AbrirReporteCentinela($('#tbUrlReporte').val())">
                                            <img runat="server" id="imgPDF" alt="Reporte Centinela" src="../../Styles/images/pdf_icon.png" />
                                            <span></span>
                                        </div>
                                    </div>

                                    <div class="form-group col-xs-7">
                                       <span class="control-label">Información relevante para OCU:</span>
                                       <div style="overflow: auto; height: 160px; border: 1px solid #ccc">
                                          <asp:CheckBoxList ID="cblTipoCoincidencia" runat="server" DataTextField="DESCRIPCION" DataValueField="CODIGO" CellPadding="0" CellSpacing="0" RepeatLayout="Flow">
                                          </asp:CheckBoxList>
                                       </div>
                                    </div>
                                 </div>

                              </div>
                           </div>
                           <div class="panel panel-default" runat="server" id="dvDesicion">
                              <div class="panel-heading">
                                 Decisión

                                  <span class="control-label pull-right" runat="server" id="lblMontoRecomendado">
                                      Monto Recomendado:
                                      <asp:TextBox ID="tbMontoRecomendado" runat="server" ToolTip="Valor divisa" CssClass="form-control" style="text-align:right" onchange="this.value=addCommas(this.value);"></asp:TextBox>
                                  </span>
                              </div>
                              <div class="panel-body">

                                <div class="form-group col-xs-8" id="panelDesicion" runat="server">
                                 <asp:RadioButtonList ID="rbDesicion" runat="server" RepeatDirection="Horizontal"></asp:RadioButtonList>
                                </div>
                                <div class="form-group col-xs-4" id="panelDevolucion" runat="server">
                                    <span class="control-label" runat="server" id="lblEtapaDevolucion">Devolver a:</span>
                                    <asp:DropDownList ID="ddlEtapaDevolucion" runat="server" CssClass="form-control">
                                    </asp:DropDownList>

                                    <span class="control-label" runat="server" id="lblUsuarioAnalisis">Asignar a:</span>
                                    <asp:DropDownList ID="ddlUsuarioAnalisis" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                                </div>
                                <div class="clearfix"></div>
                                <div class="form-group col-xs-4">
                                 <asp:Button ID="btObservacion" runat="server" Text="Justificación" OnClientClick="return(false)" CssClass="btn btn-danger" data-toggle="modal" data-target="#modal-notas" />
                                </div>

                              </div>
                           </div>
                        </div>
                        <div role="tabpanel" class="tab-pane" id="documentacion">

                           <div class="panel panel-default">
                              <div class="panel-heading">Documentos</div>
                              <div class="panel-body">

                                  <div class="form-group col-xs-6">
                                      <asp:DropDownList ID="ddlTipoDocumental" ClientIDMode="Static" runat="server" DataTextField="DESCRIPCION" DataValueField="CODIGO" CssClass="form-control">
                                      </asp:DropDownList>
                                  </div>
                                  <div class="form-group col-xs-2">
                                    <asp:Button ID="btAgregarDocumento" runat="server" ClientIDMode="Static" Text="Agregar" CssClass="btn btn-danger" />
                                  </div>
                                  <div class="clearfix"></div>

                                 <div class="table-custom">
                                    <asp:HiddenField ID="hfFilesToken" runat="server" />
                                    <asp:HiddenField ID="hfCtrlFecha" runat="server" />
                                    <asp:FileUpload runat="server" ID="fuDocumentacion" ClientIDMode="Static" CssClass="hide" onchange="UploadDocumentacion()" />
                                    <asp:HiddenField ID="hfDocumentacionCodigo" ClientIDMode="Static" runat="server" />
                                    <asp:HiddenField ID="hfDocumentacionRegistro" ClientIDMode="Static" runat="server" />
                                    <asp:Button runat="server" ID="btDocumentacion" ClientIDMode="Static" CssClass="hide" />
                                    <asp:Button runat="server" ID="btEliminarDocumento" ClientIDMode="Static" CssClass="hide" />
                                    
                                    <asp:GridView ID="gvDocumentacion" runat="server" ClientIDMode="Static" ShowHeader="false" AutoGenerateColumns="False" CssClass="table table-bordered">
                                       <Columns>
                                          <asp:TemplateField HeaderText="DATOS">
                                             <ItemTemplate>
                                                <div class="row">
                                                   <div class="col-xs-10">
                                                      <input type="checkbox" <%# If(Eval("HasFile"), "Checked", "")%> disabled data-opcional='<%# If(Eval("Opcional"), "1", "0")%>' /> <%# Eval("Descripcion") %> <strong>(<%# If(String.IsNullOrEmpty(Eval("RazonSocial")) And String.IsNullOrEmpty(Eval("Identificacion")), "Solicitante", Eval("RazonSocial"))%>)</strong>
                                                   </div>
                                                   <div class="vencimiento col-xs-2">
                                                      <%# If(Eval("DiasVencimiento") > 0, "Expira en " & Eval("DiasVencimiento") & " dias", "")%>
                                                   </div>
                                                </div>
                                                <div class="row">
                                                   <div class="col-xs-10">
                                                      <textarea runat="server" id="txtObservaciones" class="observaciones form-control" rows="2" placeholder="Espacio opcional para comentarios"><%# Eval("Observaciones")%></textarea>
                                                   </div>
                                                   <div class="col-xs-2">
                                                      <div class="btn-group-vertical">
                                                         <input runat="server" id="txtFechaEmision" clientidmode="AutoID" type="text" class="fecha-emision form-control input-sm" placeholder="Fecha Emisión" value='<%# If(Eval("FechaEmision") > Date.MinValue, DirectCast(Eval("FechaEmision"), Date).ToString("dd/MM/yyyy"), "")%>'/>
                                                         <input type="button" runat="server" id="btVer" value="Ver" class='<%# "btn btn-xs btn-primary " & If(Eval("HasFile"), "", "hide")%>' onclick='<%# "VerDocumentacion(""" & Eval("Url") & """)"%>' />
                                                         <input type="button" runat="server" id="btAdjuntar" value="Adjuntar" class='<%# "button-adjuntar btn btn-xs " & If(Eval("Opcional"), "btn-primary", "btn-danger")%>' onclick='<%# "AdjuntarDocumentacion(this,false," & Eval("Codigo") & "," & (Container.DataItemIndex + 1) & ")"%>' />
                                                         <input type="button" runat="server" id="btEliminar" value="Eliminar" class="button-adjuntar btn btn-xs btn-default" onclick='<%# "EliminarDocumentacion(false," & Eval("Codigo") & "," & (Container.DataItemIndex + 1) & ")"%>' />
                                                      </div>
                                                   </div>
                                                </div>
                                             </ItemTemplate>
                                          </asp:TemplateField>
                                          <asp:BoundField DataField="HasFile" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                          <asp:BoundField DataField="CanRemove" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                       </Columns>
                                    </asp:GridView>
                                 </div>
                              </div>
                           </div>
                        </div>
                        <div role="tabpanel" class="tab-pane" id="complementarios">
                           <div class="panel panel-default">
                              <div class="panel-heading">Codeudores / Avalistas</div>
                              <div class="panel-body">
                                 <div class="table-custom table-responsive">
                                    <asp:GridView ID="gvComplementarios" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered reporte-solicitud">
                                       <Columns>
                                          <asp:BoundField DataField="TipoRelacion" HeaderText="Tipo Relación con Deudor" />
                                          <asp:BoundField DataField="TipoIdentificacion" HeaderText="Tipo de Identificación" />
                                          <asp:BoundField DataField="Identificacion" HeaderText="Identificación" />
                                          <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                                          <asp:BoundField DataField="DescripcionSemejanza" HeaderText="Tipo Coincidencia" />
                                          <asp:HyperLinkField Text="Control Previo" DataNavigateUrlFields="UrlReporte" Target="_blank" HeaderText="Reporte" ControlStyle-CssClass="_LinkReporteCentinela" />
                                          <asp:TemplateField ItemStyle-CssClass="center">
                                             <ItemTemplate>
                                                <asp:LinkButton ID="imgEliminar" runat="server" ToolTip="Eliminar"  CommandName="Eliminar" CommandArgument='<%# Eval("Identificacion")%>' OnClientClick="return(confirm('¿Desea eliminar el registro?'))"><img alt="" src="../../Styles/images/cross.png" /></asp:LinkButton>
                                             </ItemTemplate>
                                          </asp:TemplateField>
                                          <asp:TemplateField ItemStyle-CssClass="center">
                                             <ItemTemplate>
                                                <asp:LinkButton ID="imgVer" runat="server" ToolTip="Ver" Text="Ver Información"  CommandName="Ver" CommandArgument='<%# Eval("Identificacion")%>'>Ver Información<!--<img alt="" src="../../Styles/images/magnifier.png" />--></asp:LinkButton>
                                             </ItemTemplate>
                                          </asp:TemplateField>
                                       </Columns>
                                    </asp:GridView>
                                 </div>
                                 <div style="margin-top: 5px;">
                                     <asp:Button ID="btAgregarComplementario" ClientIDMode="Static" runat="server" Text="Agregar" CssClass="btn btn-danger" />
                                 </div>
                              </div>
                           </div>
                        </div>
                        
                     </div>
                  </div>
               </div>
               <!-- /container -->

               <div class="modal" id="modal-notas" tabindex="-1" role="dialog" aria-labelledby="Notas" aria-hidden="true">
                  <div class="modal-dialog modal-lg">
                     <div class="modal-content">
                        <div class="modal-header">
                           <h4 class="modal-title">Observaciones</h4>
                        </div>
                        <div class="modal-body">
                           <div class="row">
                              <div class="col-xs-12">

                                 <div class="table-custom" style="overflow-y:auto">
                                 <asp:GridView ID="gvNotas" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered">
                                     <HeaderStyle CssClass="gv-obervaciones" />
                                    <Columns>
                                       <asp:BoundField DataField="FECHA" HeaderText="Fecha" ItemStyle-Width="15%" />
                                       <asp:BoundField DataField="ETAPA" HeaderText="Etapa" ItemStyle-Width="15%" />
                                       <asp:BoundField DataField="USUARIO" HeaderText="Usuario" ItemStyle-Width="15%" />
                                       <asp:BoundField DataField="DECISION" HeaderText="Decisión" ItemStyle-Width="15%" />
                                       <asp:BoundField DataField="OBSERVACION" HeaderText="Nota" ItemStyle-Width="40%" />
                                    </Columns>
                                 </asp:GridView>
                                 </div>

                              </div>
                           </div>
                           <div class="form-group col-xs-12">
                              <span class="control-label">Observación:</span>
                              <asp:TextBox ID="tbObservacion" ClientIDMode="Static" runat="server" CssClass="form-control" TextMode="MultiLine" Height="100px"></asp:TextBox>
                              <asp:Label ID="lblValidacionNota" ClientIDMode="Static" runat="server" CssClass="error_label"></asp:Label>
                               <asp:HiddenField ID="tbUltimaObservacion" runat="server" />
                           </div>
                        </div>
                        <div class="modal-footer" style="border:0">
                           <asp:Button ID="btGuardarNota" ClientIDMode="Static" runat="server" Text="Guardar" CssClass="btn btn-danger" />
                           <asp:Button ID="btCerrarNota" ClientIDMode="Static" runat="server" Text="Cerrar" CssClass="btn btn-default" data-dismiss="modal" OnClientClick="limpiarNota(); return false" />
                        </div>
                     </div>
                  </div>
               </div>

               <div class="modal" id="modal-adjuntos" tabindex="-1" role="dialog" aria-labelledby="Notas" aria-hidden="true">
                  <div class="modal-dialog">
                     <div class="modal-content">
                        <div class="modal-header">
                           <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                           <h4 class="modal-title">Adjuntos</h4>
                        </div>
                        <div class="modal-body container-fluid">
                           <div class="form-group col-xs-12">

                              <div class="table-custom" style="overflow-y:auto">
                                 <asp:GridView ID="gvAdjuntos" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered">
                                    <Columns>
                                       <asp:TemplateField>
                                          <ItemTemplate>
                                             <asp:ImageButton ID="imgEliminar" runat="server" ImageUrl="../../Styles/images/cross.png" Width="16px" Height="16px" CommandName="Eliminar" CommandArgument="<%# Container.DataItemIndex %>" OnClientClick="return(confirm('Desea eliminar el registro?'))" />
                                          </ItemTemplate>
                                       </asp:TemplateField>
                                       <asp:BoundField DataField="ADJUNTO" HeaderText="Adjunto" />
                                       <asp:TemplateField HeaderText="Nombre">
                                          <ItemTemplate>
                                             <asp:LinkButton ID="hlkDercargar" runat="server" CommandName="Descargar" CommandArgument="<%# Container.DataItemIndex %>"></asp:LinkButton>
                                          </ItemTemplate>
                                       </asp:TemplateField>
                                       <asp:BoundField DataField="USUARIO" HeaderText="Usuario" />
                                       <asp:BoundField DataField="FECHA" HeaderText="Fecha" />
                                    </Columns>
                                 </asp:GridView>
                              </div>

                           </div>
                           <div class="form-group col-xs-12">
                              <span class="control-label">Adjunto:</span>
                              <asp:FileUpload ID="fuAdjunto" runat="server" CssClass="form-control" />
                           </div>
                        </div>
                        <div class="modal-footer">
                           <asp:Button ID="btGuardarAdjunto" runat="server" Text="Guardar" CssClass="btn btn-danger" />
                           <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                        </div>
                     </div>
                  </div>
               </div>

                <div class="modal" id="modal-correspondencia-recibida" tabindex="-1" role="dialog" aria-labelledby="Notas" aria-hidden="true">
                    <div class="modal-dialog modal-lg">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h4 class="modal-title">Correspondencia Recibida</h4>
                            </div>
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-xs-12">
                                        <asp:GridView ID="gvCorrespondenciaRecibida" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered reporte-solicitud" ShowHeader="false">
                                            <Columns>
                                                <asp:TemplateField ItemStyle-CssClass="CR-item">
                                                    <ItemTemplate>
                                                        <strong><asp:Label runat="server" ID="NumeroRadicado" CssClass="CR-NumeroRadicado" Text='<%# Eval("NUMERO_RADICADO") %>'></asp:Label></strong> <asp:Label runat="server" ID="DetalleAsunto" Text='<%# Eval("DETALLE_ASUNTO") %>'></asp:Label>
                                                        <asp:Label runat="server" ID="Fecha" Text='<%# Eval("FECHA") %>' CssClass="pull-right"></asp:Label><br />
                                                        Funcionario: <asp:Label runat="server" ID="Label2" Text='<%# Eval("FUNCIONARIO") %>'></asp:Label><br />
                                                        Cliente: <asp:Label runat="server" ID="Label3" Text='<%# Eval("CLIENTE") %>'></asp:Label><br />
                                                        Anexos:
                                                        <input type="button" value="Asociar a otro Incidente" class="_AsociarCorrespondencia btn btn-default btn-xs pull-right" data-fileid='<%# Eval("FILEID") %>' onclick="AsociarCorrespondenciaClick(this)" />
                                                        <input type="button" value="Devolver Correspondencia" class="_DevolverCorrespondencia btn btn-default btn-xs pull-right" data-fileid='<%# Eval("FILEID") %>' onclick="DevolverCorrespondenciaClick(this)" />
                                                        <br />
                                                        <asp:Literal ID="LiteralAnexoCR" runat="server" Text='<%# Eval("DETALLE_ANEXOS") %>'></asp:Literal>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer" style="border:0">
                                <input type="button" value="Cerrar" class="btn btn-default" data-dismiss="modal" />
                            </div>
                        </div>
                    </div>
                </div>

               <div id="div-procesando" style="display: none">
                  <div id="div-procesando-content">
                     <h4 class="modal-title">Procesando...</h4>
                     <div class="progress">
                        <div class="progress-bar progress-bar-striped active" role="progressbar" aria-valuenow="100" aria-valuemin="0" aria-valuemax="100" style="width: 100%">
                        </div>
                     </div>
                  </div>
               </div>
               <div class="modal" id="modal-actividad" role="dialog" style="z-index:2002"  data-backdrop="static" data-keyboard="false">
                  <div class="modal-dialog modal-lg">
                     <div class="modal-content">
                        <div class="modal-header">
                           <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                           <h4 class="modal-title">Actividades</h4>
                        </div>
                        <div class="modal-body" style="max-height: 80vh; overflow-y: auto;">
                           <div style="margin-bottom:5px">Buscar: <input id="txtBuscarActividad" type="text" /></div>
                           <div class="list-group">
                              <asp:Literal ID="listActividades" runat="server"></asp:Literal>
                           </div>
                        </div>
                     </div>
                  </div>
               </div>
               <div class="modal" id="modal-complementario" role="dialog" style="z-index:2000" data-backdrop="static" data-keyboard="false">
                  <div class="modal-dialog modal-lg">
                     <div class="modal-content">
                        <div class="modal-header">
                           <% If Val(Me.lbIncidente.Text) = 0 Then%>
                            <button type="button" data-dismiss="modal" class="close" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                           <% End If%>
                           <h4 class="modal-title">Codeudor / Avalista</h4>
                        </div>
                        <div class="modal-body">
                           <div role="tabpanel" style="margin-top: 10px;">
                              <ul class="nav nav-tabs" role="tablist">
                                 <li role="presentation" class="active"><a href="#complementario-datos" id="tab-complementario-datos" role="tab" data-toggle="tab">Codeudor / Avalista</a></li>
                                 <li role="presentation" class="main-tab"><a href="#complementario-documentacion" id="tab-complementario-documentacion" role="tab" data-toggle="tab">Documentación <span id="countDocumentacionComplementario" runat="server" class="badge"></span> </a></li>
                              </ul>
                              <div class="tab-content">
                                 <div role="tabpanel" class="tab-pane active" id="complementario-datos">
                                    <div class="panel panel-default">
                                       <div class="panel-heading">
                                          Información del Codeudor / Avalista
                                          <asp:Label ID="lblComplementarioEstado" runat="server" Text="" ForeColor="DarkRed" Style="margin-left: 285px"></asp:Label>
                                       </div>
                                       <div class="panel-body">
                                          
                                           <asp:HiddenField ID="hfComplementarioNew" runat="server" />
                                           <asp:HiddenField ID="tbComplementarioSemejanza" runat="server" />
                                           <asp:HiddenField ID="tbComplementarioUrlReporte" runat="server" />

                                          <div class="form-group col-xs-4">
                                             <span class="control-label">Tipo Relación con Deudor:</span>
                                             <asp:DropDownList ID="ddlComplementarioTipoComplementario" runat="server" data-val="true" DataTextField="DESCRIPCION" DataValueField="CODIGO" CssClass="form-control">
                                             </asp:DropDownList>
                                          </div>
                                          <div class="form-group col-xs-4">
                                             <span class="control-label">Tipo identificación:</span>
                                             <asp:DropDownList ID="ddlComplementarioTipoIdentificacion" ClientIDMode="Static" runat="server" data-val="true" DataTextField="DESCRIPCION" DataValueField="CODIGO" CssClass="form-control"></asp:DropDownList>
                                          </div>
                                          <div class="form-group col-xs-4">
                                             <span class="control-label">Número de identificación:</span>
                                             <asp:TextBox ID="tbComplementarioIdentificacion" ClientIDMode="Static" runat="server" CssClass="form-control" AutoPostBack="True" data-val="true"></asp:TextBox>
                                          </div>
                                          <div class="form-group col-xs-4">
                                             <span class="control-label">Nombre / Razón social:</span>
                                             <asp:TextBox ID="tbComplementarioNombre" ClientIDMode="Static" runat="server" CssClass="form-control" data-val="true" AutoPostBack="true"></asp:TextBox>
                                          </div>
                                          <div class="form-group col-xs-4">
                                             <span class="control-label">País:</span>
                                             <asp:DropDownList ID="ddlComplementarioPais" runat="server" DataTextField="DESCRIPCION" data-val="true" DataValueField="CODIGO" CssClass="form-control" AutoPostBack="True">
                                             </asp:DropDownList>
                                          </div>
                                          <div class="form-group col-xs-4">
                                             <span class="control-label">Ciudad:</span>
                                             <asp:DropDownList ID="ddlComplementarioCiudad" data-val="true" runat="server" DataTextField="DESCRIPCION" DataValueField="CODIGO" CssClass="form-control">
                                             </asp:DropDownList>
                                          </div>
                                          <div class="form-group col-xs-4">
                                             <span class="control-label">Correo electrónico:</span>
                                             <asp:TextBox ID="tbComplementarioCorreo" ClientIDMode="Static" runat="server" CssClass="form-control" data-val="true" onchange="if(isEmail(this.value))return(true);else{alert('Correo inválido');this.value='';return(false);}"></asp:TextBox>
                                          </div>
                                          <div class="form-group col-xs-4">
                                             <span class="control-label">Dirección:</span>
                                             <asp:TextBox ID="tbComplementarioDireccion" ClientIDMode="Static" runat="server" CssClass="form-control" data-val="true"></asp:TextBox>
                                          </div>
                                          <div class="form-group col-xs-4">
                                             <span class="control-label">Teléfono:</span>
                                             <asp:TextBox ID="tbComplementarioTelefono" ClientIDMode="Static" runat="server" CssClass="form-control" data-val="true"></asp:TextBox>
                                          </div>
                                          <div class="form-group col-xs-12">
                                             <asp:CheckBox ID="chkComplementarioParticipacion" CssClass="checkbox-inline" runat="server" AutoPostBack="true" Text="¿Incluye personas jurídicas con participación accionaria mayor al 5%?" />
                                          </div>
                                          <div class="form-group col-xs-12">
                                             <span class="control-label">Actividad económica:</span>
                                             <asp:DropDownList ID="ddlComplementarioActividad" ClientIDMode="Static" runat="server" DataTextField="DESCRIPCION" DataValueField="CODIGO" CssClass="form-control">
                                             </asp:DropDownList>
                                          </div>
                                       </div>
                                    </div>
                                 </div>
                                 <div role="tabpanel" class="tab-pane" id="complementario-documentacion">
                                    <div class="panel panel-default">
                                       <div class="panel-heading">Documentos</div>
                                       <div class="panel-body">

                                       <div class="form-group col-xs-6">
                                           <asp:DropDownList ID="ddlTipoDocComplementario" ClientIDMode="Static" runat="server" DataTextField="DESCRIPCION" DataValueField="CODIGO" CssClass="form-control">
                                             </asp:DropDownList>
                                        </div>
                                        <div class="form-group col-xs-2">
                                           <asp:Button ID="btAgregarDocComplementario" runat="server" ClientIDMode="Static" Text="Agregar" CssClass="btn btn-danger" />
                                        </div>
                                        <div class="clearfix"></div>

                                          <div class="table-custom">
                                    <asp:HiddenField ID="hfDocumentacionCodigoComplementario" ClientIDMode="Static" runat="server" />
                                    <asp:HiddenField ID="hfDocumentacionRegistroComplementario" ClientIDMode="Static" runat="server" />
                                    <asp:GridView ID="gvDocumentacionComplementario" runat="server" ClientIDMode="Static" ShowHeader="false" AutoGenerateColumns="False" CssClass="table table-bordered">
                                       <Columns>
                                          <asp:TemplateField HeaderText="DATOS">
                                             <ItemTemplate>
                                                <div class="row">
                                                   <div class="col-xs-10">
                                                      <input type="checkbox" <%# If(Eval("HasFile"), "Checked", "")%> disabled data-opcional='<%# If(Eval("Opcional"), "1", "0")%>' /> <%# Eval("Descripcion") %> <strong>(<%# If(String.IsNullOrEmpty(Eval("RazonSocial")) And Eval("Identificacion") = String.Empty, "Codeudor / Avalista", Eval("RazonSocial"))%>)</strong>
                                                   </div>
                                                   <div class="vencimiento col-xs-2">
                                                      <%# If(Eval("DiasVencimiento") > 0, "Expira en " & Eval("DiasVencimiento") & " dias", "")%>
                                                   </div>
                                                </div>
                                                <div class="row">
                                                   <div class="col-xs-10">
                                                      <textarea runat="server" id="txtObservaciones" class="observaciones form-control" rows="2" placeholder="Espacio opcional para comentarios"><%# Eval("Observaciones")%></textarea>
                                                   </div>
                                                   <div class="col-xs-2">
                                                      <div class="btn-group-vertical">
                                                         <input runat="server" id="txtFechaEmision" clientidmode="AutoID" type="text" class="fecha-emision form-control input-sm" placeholder="Fecha Emisión" value='<%# If(Eval("FechaEmision") > Date.MinValue, DirectCast(Eval("FechaEmision"), Date).ToString("dd/MM/yyyy"), "")%>'/>
                                                         <input type="button" runat="server" id="btVer" value="Ver" class='<%# "btn btn-xs btn-primary " & If(Eval("HasFile"), "", "hide")%>' onclick='<%# "VerDocumentacion(""" & Eval("Url") & """)"%>' />
                                                         <input type="button" runat="server" id="btAdjuntar" value="Adjuntar" class='<%# "button-adjuntar btn btn-xs " & If(Eval("Opcional"), "btn-primary", "btn-danger")%>' onclick='<%# "AdjuntarDocumentacion(this,true," & Eval("Codigo") & "," & (Container.DataItemIndex + 1) & ")"%>' />
                                                         <input type="button" runat="server" id="btEliminar" value="Eliminar" class="button-adjuntar btn btn-xs btn-default" onclick='<%# "EliminarDocumentacion(true," & Eval("Codigo") & "," & (Container.DataItemIndex + 1) & ")"%>' />
                                                         
                                                      </div>
                                                   </div>
                                                </div>
                                             </ItemTemplate>
                                          </asp:TemplateField>
                                           <asp:BoundField DataField="HasFile" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                           <asp:BoundField DataField="CanRemove" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                       </Columns>
                                    </asp:GridView>
                                 </div>
                                       </div>
                                    </div>
                                 </div>
                              </div>
                           </div>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btComplementarioGuardar" runat="server" ClientIDMode="Static" Text="Guardar" OnClientClick="if (ValidarComplementario()) return ButtonPostBack(this); else return false;" CssClass="btn btn-danger" />
                            <% If Val(Me.lbIncidente.Text) = 0 Then%>
                                <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                            <% Else %>
                                <asp:Button ID="btComplementarioCerrar" runat="server" ClientIDMode="Static" Text="Cerrar" OnClientClick="if (ValidarComplementario()) return ButtonPostBack(this); else return false;" CssClass="btn btn-default" />
                            <% End If%>
                        </div>
                     </div>
                  </div>
               </div>
            </ContentTemplate>
            <Triggers>
               
               <asp:PostBackTrigger ControlID="btDocumentacion" />
            </Triggers>
         </asp:UpdatePanel>
      </form>
   </body>
</html>