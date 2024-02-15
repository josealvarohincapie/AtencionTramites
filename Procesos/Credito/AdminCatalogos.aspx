<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="AdminCatalogos.aspx.vb" ValidateRequest="false" Inherits="Web.AdminCatalogos" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
   <head runat="server">
      <title>Crédito Directo | Administración</title>
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
                     <h4>Administraci&oacute;n / Cr&eacute;dito Directo</h4>
                  </td>
                  <td style="width: 30%"></td>
               </tr>
            </table>
         </div>
         <div role="tabpanel">
            <!-- Nav tabs -->
            <ul class="nav nav-tabs" role="tablist" style="width:100%">
               <li role="presentation" class="active"><a href="#catalogos" id="tab-catalogos" aria-controls="profile" role="tab" data-toggle="tab">Catálogos</a></li>
               <li role="presentation"><a href="#documentos" id="tab-documentos" aria-controls="profile" role="tab" data-toggle="tab">Documentos</a></li>
               <li role="presentation"><a href="#parametros" id="tab-parametros" aria-controls="home" role="tab" data-toggle="tab">Parámetros</a></li>
            </ul>
            <!-- Tab panes -->
            <div class="tab-content">
               <div role="tabpanel" class="tab-pane active" id="catalogos">
                  <div class="panel panel-default">
                     <div class="panel-heading">Catálogos</div>
                     <div class="panel-body">
                        <div class="form-group col-xs-4">
                           <span class="control-label">Catálogo:</span>
                           <asp:DropDownList ID="ddlCatalogo" runat="server" class="form-control"
                              AutoPostBack="True">
                           </asp:DropDownList>
                        </div>
                        <div class="table-custom">
                           <asp:GridView ID="gvDetalle" runat="server" AutoGenerateColumns="False" class="table table-bordered">
                              <Columns>
                                 <asp:TemplateField>
                                    <ItemTemplate>
                                       <asp:ImageButton ID="ibEditar" runat="server" ImageUrl="../../Styles/images/pencil.png" Width="16px" Height="16px" CommandName="Editar" CommandArgument="<%# Container.DataItemIndex %>" />
                                    </ItemTemplate>
                                 </asp:TemplateField>
                                 <asp:TemplateField>
                                    <ItemTemplate>
                                       <asp:ImageButton ID="ibEliminar" runat="server" ImageUrl="../../Styles/images/cross.png" Width="16px" Height="16px" CommandName="Eliminar" CommandArgument="<%# Container.DataItemIndex %>" OnClientClick="return(confirm('Desea eliminar el registro?'))" />
                                    </ItemTemplate>
                                 </asp:TemplateField>
                                 <asp:BoundField DataField="CODIGO" HeaderText="Código" />
                                 <asp:BoundField DataField="DESCRIPCION" HeaderText="Descripción" />
                                 <asp:BoundField DataField="HABILITADO" HeaderText="Habilitado" />
                                 <asp:BoundField DataField="ADICIONAL" HeaderText="Adicional" />
                              
                              </Columns>
                           </asp:GridView>
                        </div>
                        <asp:Button ID="btAgregar" runat="server" Text="Agregar" class="btn btn-danger" />
                     </div>
                  </div>
               </div>
               
               <div role="tabpanel" class="tab-pane" id="documentos">
                  <div class="panel panel-default">

                     <div class="panel-heading">Documentos Solicitud</div>
                     <div class="panel-body">

                        
                         <span class="control-label">Tipo de Producto:</span>
                           <asp:DropDownList ID="ddlTipoProducto" runat="server" class="form-control"
                              AutoPostBack="True">
                           </asp:DropDownList>
                        
                         <div class="clearfix"></div>
                        
                        <span class="control-label">Tipo de documento:</span>
                        <div class="table-custom">
                           <asp:GridView ID="gvDetalleDocumentos" runat="server" AutoGenerateColumns="False" class="table table-bordered">
                              <Columns>
                                 <asp:BoundField DataField="CODIGO" HeaderText="Código" />
                                 <asp:BoundField DataField="DESCRIPCION" HeaderText="Descripción" />
                                 <asp:TemplateField HeaderText="Deudor">
                                    <ItemTemplate>
                                       <asp:CheckBox ID="chkDeudorJuridico" runat="server" Text="Jurídico" />
                                        <asp:CheckBox ID="chkDeudorNatural" runat="server" Text="Natural" />
                                    </ItemTemplate>
                                 </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Codeudor">
                                    <ItemTemplate>
                                       <asp:CheckBox ID="chkCodeudor" runat="server" />
                                    </ItemTemplate>
                                 </asp:TemplateField>
                              </Columns>
                           </asp:GridView>
                        </div>
                        <asp:Button ID="btGuardarDocumentos" runat="server" Text="Guardar" class="btn btn-danger" />
                     </div>
                  </div>
               </div>
               <div role="tabpanel" class="tab-pane" id="parametros">
                  <div class="panel panel-default">
                     <div class="panel-heading">Parámetros</div>
                     <div class="panel-body">
                        <div class="form-group col-xs-6">
                           <span class="control-label">Habilita Centinela:</span>
                           <asp:TextBox ID="tbHabilitaCentinela" runat="server"></asp:TextBox>
                        </div>
                        <div class="clearfix"></div>
                         <div class="form-group col-xs-6">
                           <span class="control-label">Habilita T24:</span>
                           <asp:TextBox ID="tbHabilitaT24" runat="server"></asp:TextBox>
                        </div>
                         <div class="clearfix"></div>
                         <div class="form-group col-xs-6">
                           <span class="control-label">Producto Project Finance:</span>
                           <asp:TextBox ID="tbProductoPF" runat="server"></asp:TextBox>
                        </div>
                        <div class="clearfix"></div>
                        <div class="form-group col-xs-6">
                           <span class="control-label">Dias Vencimiento Aplazado:</span>
                           <asp:TextBox ID="tbDiasAplazado" runat="server"></asp:TextBox>
                        </div>
                        <div class="clearfix"></div>
                         <div class="form-group col-xs-6">
                           <span class="control-label">Tipo Identificación NIT:</span>
                           <asp:TextBox ID="tbTipoNIT" runat="server"></asp:TextBox>
                        </div>
                        <div class="clearfix"></div>
                         <div class="form-group col-xs-6">
                           <span class="control-label">Pais Defecto:</span>
                           <asp:TextBox ID="tbPaisDefecto" runat="server"></asp:TextBox>
                        </div>
                        <div class="clearfix"></div>
                        <asp:Button ID="btGuardarParametros" runat="server" Text="Guardar" class="btn btn-danger" />
                         <em>Advertencia: Al dar clic en este botón se reinicia la aplicación.</em>
                     </div>
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
                     <h4 class="modal-title">Datos del registro</h4>
                  </div>
                  <div class="modal-body container-fluid">
                     <div class="form-group col-xs-3">
                        <span class="control-label">Código:</span>
                        <asp:TextBox ID="tbCodigo" runat="server" class="form-control"></asp:TextBox>
                     </div>
                     <div class="form-group col-xs-9">
                        <span class="control-label">Descripción:</span>
                        <asp:TextBox ID="tbDescripcion" runat="server" class="form-control"></asp:TextBox>
                     </div>
                     <div class="form-group col-xs-3">
                        <span class="control-label">Habilitado:</span>
                        <asp:CheckBox ID="cbHabilitado" runat="server" />
                     </div>
                      <div class="form-group col-xs-3" runat="server" id="dvAdicional">
                        <span class="control-label">Adicional:</span>
                        <asp:CheckBox ID="cbAdicional" runat="server" />
                     </div>
                     <div class="form-group col-xs-12">
                        <span class="control-label">
                           <asp:Label ID="lbCatalogoVinculados" runat="server"></asp:Label>
                        </span>
                        <div class="table-custom">
                           <asp:GridView ID="gvDetalleVinculados" runat="server" AutoGenerateColumns="False" class="table table-bordered">
                              <Columns>
                                 <asp:BoundField DataField="CODIGO" HeaderText="Código" />
                                 <asp:BoundField DataField="DESCRIPCION" HeaderText="Descripción" />
                                 <asp:TemplateField HeaderText="Vinculado">
                                    <ItemTemplate>
                                       <asp:CheckBox ID="chkSeleccionado" runat="server" />
                                    </ItemTemplate>
                                 </asp:TemplateField>
                              </Columns>
                           </asp:GridView>
                        </div>
                     </div>
                  </div>
                  <div class="modal-footer">
                     <asp:Button ID="btGuardar" runat="server" Text="Guardar" class="btn btn-danger" />
                     <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                  </div>
               </div>
            </div>
         </div>
      </form>
   </body>
</html>