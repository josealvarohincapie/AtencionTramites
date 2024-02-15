<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ReporteSolicitudes.aspx.vb" ValidateRequest="false" Inherits="Web.ReporteSolicitudes" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Crédito Directo | Consultas</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <!-- Bootstrap core CSS -->
    <link href="../../Plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <!-- Bootstrap theme -->
    <link href="../../Plugins/bootstrap/css/bootstrap-theme.min.css" rel="stylesheet" />
    <!-- Custom styles for this template -->
    <link href="../../Styles/theme.css" rel="stylesheet" />
    <!-- Bootstrap core JavaScript -->
    <script src="../../Plugins/jquery/jquery.min.js" type="text/javascript"></script>
    <script src="../../Plugins/bootstrap/js/bootstrap.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).keypress(function (e) {
            if (e.which == 13) {
                return false;
            }
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="header">
                <table style="width: 100%">
                    <tr>
                        <td style="width: 30%">
                            <img src="../../Styles/images/4573_BAN_logo_HZ.jpg" style="width: 335px; height: 97px" alt="BANCOLDEX" />
                        </td>
                        <td style="width: 40%">
                            <h4>Reporte Solicitudes / Cr&eacute;dito Directo</h4>
                        </td>
                        <td style="width: 30%"></td>
                    </tr>
                </table>
            </div>
            <div class="panel panel-default">
                <div class="panel-heading">Resultados</div>
                <div class="panel-body">
                    <div class="form-group col-xs-4">
                        <span class="control-label">Filtro Solicitud:</span>
                        <asp:TextBox ID="tbIncidente" runat="server" class="form-control"></asp:TextBox>
                        <asp:DropDownList ID="ddlTipoProducto" runat="server" DataTextField="DESCRIPCION" DataValueField="CODIGO" class="form-control" Visible="false">
                        </asp:DropDownList>
                    </div>
                    <div class="form-group col-xs-12">
                        <asp:Button ID="btConsultar" runat="server" Text="Consultar" class="btn btn-danger" />
                    </div>
                    <div class="table-custom">
                                <asp:GridView ID="gvDetalle" runat="server" AutoGenerateColumns="false" class="table table-bordered">
                                <Columns>
                                    <asp:TemplateField HeaderText="DETALLE" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ibDetalle" runat="server" ImageUrl="../../Styles/images/magnifier.png" Width="16px" Height="16px" CommandName="Detalle" CommandArgument="<%# Container.DataItemIndex %>"/>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ESTADO" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ibEstado" runat="server" ImageUrl="../../Styles/images/map.png" Width="16px" Height="16px"/>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="DATOS">
                                        <ItemTemplate>
                                            <table class="reporte-solicitud" style="width:100%">
                                                <tr>
                                                    <td><span class="reporte-solicitud-label">Incidente:</span> <%# Eval("INCIDENTE") %></td>
                                                    <td><span class="reporte-solicitud-label">Tipo Producto:</span> <%# Eval("TIPO PRODUCTO")%></td>
                                                    <td><span class="reporte-solicitud-label">Divisa:</span> <%# Eval("DIVISA")%>. <%# Eval("VALOR_DIVISA")%></td>
                                                </tr>
                                                <tr>
                                                    <td><span class="reporte-solicitud-label">Identificación:</span> <%# Eval("TIPO IDENTIFICACION")%> - <%# Eval("IDENTIFICACION")%></td>
                                                    <td><span class="reporte-solicitud-label">Razón Social:</span> <%# Eval("RAZONSOCIAL")%></td>
                                                    <td><span class="reporte-solicitud-label">Responsables:</span> <%# Eval("RESPONSABLE_CLIENTE")%> / <%# Eval("RESPONSABLE_SOLICITUD")%></td>
                                                </tr>
                                                <tr>
                                                    <td><span class="reporte-solicitud-label">Dirección:</span> <%# Eval("DIRECCION")%>. <%# Eval("CIUDAD")%></td>
                                                    <td><span class="reporte-solicitud-label">Teléfono:</span> <%# Eval("TELEFONO")%></td>
                                                    <td><span class="reporte-solicitud-label">Correo:</span> <%# Eval("CORREO")%></td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3">
                                                        <!--<span class="reporte-solicitud-label">Unidad Cumplimiento:</span> <%# Eval("UNIDAD CUMPLIMIENTO")%> / -->
                                                        <span class="reporte-solicitud-label">Tipo Coincidencia:</span> <%# Eval("SEMEJANZA")%> / 
                                                        <span class="reporte-solicitud-label">Información para OCU:</span> <%# Eval("TIPO COINCIDENCIA")%>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField ="INCIDENTE" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden"/>
                                </Columns>
                                </asp:GridView>
                    </div>
                </div>
            </div>
            <!-- <footer class="footer"><p>...</p></footer> -->
        </div>
        <!-- /container -->
        <div class="modal fade" id="modal-detalle" tabindex="-1" role="dialog" aria-labelledby="Operacion" aria-hidden="true">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        <h4 class="modal-title">Detalle de la solicitud</h4>
                    </div>
                    <div class="modal-body container-fluid">
                        <div role="tabpanel">
                            <!-- Nav tabs -->
                            <ul class="nav nav-tabs" role="tablist">
                                <li role="presentation" class="active"><a href="#documentos" aria-controls="home" role="tab" data-toggle="tab">Documentos</a></li>
                                <li role="presentation"><a href="#relacionados" id="tab-relacionados" aria-controls="profile" role="tab" data-toggle="tab">Codeudores / Avalistas</a></li>
                                <li role="presentation"><a href="#notas" id="tab-notas" aria-controls="profile" role="tab" data-toggle="tab">Observaciones</a></li>
                                <li role="presentation"><a href="#historico" id="tab-historico" aria-controls="profile" role="tab" data-toggle="tab">Historico de tareas</a></li>
                            </ul>
                            <!-- Tab panes -->
                            <div class="tab-content">
                                <div role="tabpanel" class="tab-pane active" id="documentos">
                                    <div class="panel panel-default">
                                        <div class="panel-heading">Documentos</div>
                                        <div class="panel-body">
                                            <div class="table-custom" style="overflow-y:auto">
                                                <asp:GridView ID="gvDocumentos" runat="server" AutoGenerateColumns="False" class="table table-bordered reporte-solicitud">
                                                    <Columns>
                                                        <asp:BoundField DataField="NOMBRE" HeaderText="CLIENTE" />
                                                        <asp:BoundField DataField="DOCUMENTO" HeaderText="DOCUMENTO" />
                                                        <asp:BoundField DataField="OBSERVACIONES" HeaderText="OBSERVACIÓN" />
                                                        <asp:CheckBoxField DataField="ENTREGADO" HeaderText="ENTREGADO" ItemStyle-CssClass="center" />
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div role="tabpanel" class="tab-pane" id="relacionados">
                                    <div class="panel panel-default">
                                        <div class="panel-heading">Codeudores / Avalistas</div>
                                        <div class="panel-body">
                                            <div class="table-custom" style="overflow-y:auto">
                                                <asp:GridView ID="gvComplementarios" runat="server" AutoGenerateColumns="False" class="table table-bordered reporte-solicitud">
                                                    <Columns>
                                                        <%--<asp:BoundField DataField="INCIDENTE" HeaderText="INCIDENTE" />--%>
                                                        <asp:BoundField DataField="TIPOIDDES" HeaderText="TIPO ID" />
                                                        <asp:BoundField DataField="IDENTIFICACION" HeaderText="IDENTIFICACIÓN" />
                                                        <asp:BoundField DataField="NOMBRE" HeaderText="NOMBRE" />
                                                        <asp:BoundField DataField="TIPORELACIONDES" HeaderText="TIPO RELACIÓN" />
                                                        <asp:BoundField DataField="DIRECCION" HeaderText="DIRECCIÓN" />
                                                        <asp:BoundField DataField="CIUDAD" HeaderText="CIUDAD" />
                                                        <asp:BoundField DataField="TELEFONO" HeaderText="TELÉFONO" />
                                                        <asp:BoundField DataField="CORREO" HeaderText="CORREO" />
                                                        <asp:BoundField DataField="ACTIVIDADDES" HeaderText="ACTIVIDAD" />
                                                        <asp:BoundField DataField="SEMEJANZADES" HeaderText="SEMEJANZA" />
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div role="tabpanel" class="tab-pane" id="notas">
                                    <div class="panel panel-default">
                                        <div class="panel-heading">Notas</div>
                                        <div class="panel-body">
                                            <div class="table-custom" style="overflow-y:auto">
                                                <asp:GridView ID="gvObservaciones" runat="server" AutoGenerateColumns="True" class="table table-bordered reporte-solicitud">
                                                    <Columns>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div role="tabpanel" class="tab-pane" id="historico">
                                    <div class="panel panel-default">
                                        <div class="panel-heading">Historico de tareas</div>
                                        <div class="panel-body">
                                            <div class="table-custom" style="overflow-y:auto">
                                                <asp:GridView ID="gvTareas" runat="server" AutoGenerateColumns="True" class="table table-bordered reporte-solicitud">
                                                    <Columns>
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
                        <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>