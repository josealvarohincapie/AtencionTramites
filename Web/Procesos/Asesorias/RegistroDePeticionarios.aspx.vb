Imports System.Configuration.ConfigurationManager
Imports UltimusEIK
Imports UltimusEIK.ClientModel
Imports Integracion
Imports System.Xml
Imports Datos
Imports System.IO
Imports LogWriterHelper
Imports System.Net
Imports System.Data.OleDb
Imports System.Threading
Imports System.Web.Services
Imports Web.WcfCorrespondenciaRecibida
Imports Web.Modelo.dto

Public Class RegistroDePeticionarios
    Inherits System.Web.UI.Page

    Public WithEvents TxtCodigoSolicitud As Global.System.Web.UI.WebControls.HiddenField
    Public WithEvents TxtNumeroRadicado As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control hddCodigoCanalAtencion.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Public WithEvents hddCodigoCanalAtencion As Global.System.Web.UI.WebControls.HiddenField
    Public WithEvents txtCanalAtencion As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtFecha.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtFecha As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control hddCodigoTipoSolicitante.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents hddCodigoTipoSolicitante As Global.System.Web.UI.WebControls.HiddenField

    '''<summary>
    '''Control txtTipoSolicitante.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtTipoSolicitante As Global.System.Web.UI.WebControls.TextBox

    Protected WithEvents chkEsAnonimo As Global.System.Web.UI.WebControls.CheckBox

    '''<summary>
    '''Control hddCodigoTipoDocumento.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents hddCodigoTipoDocumento As Global.System.Web.UI.WebControls.HiddenField

    '''<summary>
    '''Control txtTipoDocumento.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtTipoDocumento As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtIdentificacion.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtIdentificacion As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtRemitente.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtRemitente As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control rblGrupoEtnico.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents rblGrupoEtnico As Global.System.Web.UI.WebControls.RadioButtonList

    '''<summary>
    '''Control hddCodigoSexoAsignado.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents hddCodigoSexoAsignado As Global.System.Web.UI.WebControls.HiddenField

    '''<summary>
    '''Control txtSexoAsignado.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtSexoAsignado As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control hddCodigoOrientacionSexual.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents hddCodigoOrientacionSexual As Global.System.Web.UI.WebControls.HiddenField

    '''<summary>
    '''Control txtOrientacionSexual.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtOrientacionSexual As Global.System.Web.UI.WebControls.TextBox

    Public WithEvents grdDocumentos As Global.System.Web.UI.WebControls.GridView

    Protected WithEvents txtComentarios As Global.System.Web.UI.WebControls.TextBox


    Private Sub InitUltDataFromRequest()

        Dim data As New UltData()
        data.TaskID = Request.Cookies.Get("TaskID").Value
        data.SessionID = Request.Cookies.Get("SessionID").Value
        data.UserID = Request.Cookies.Get("UserID").Value

        Dim strError As String = String.Empty
        Dim cm As New ClientModel()
        If Not cm.GetTaskInfo(data, strError) Then
            LogWriter.WriteLog("RegistroDePeticionarios", "GetTaskInfo: " + strError)
        End If

        UltData = data

    End Sub

    Public Property UltData() As UltData
        Get
            Return Session("UltData")
        End Get
        Set(ByVal value As UltData)
            Session("UltData") = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Session.Clear()
                Inicializacion()
            End If

        Catch ex As Exception
            LogWriter.WriteLog("RegistroDePeticionarios", ex)
        End Try
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    Private Sub Inicializacion()
        Try
            InicializacionRadicado()
            InicializacionDocumentos()
            'InicializacionClasificacionPeticion()
        Catch ex As Exception
            Dim nombreMetodo = System.Reflection.MethodBase.GetCurrentMethod().Name
            Response.Write("<script language='javascript'>alert('" & "RegistroDePeticionarios -" & nombreMetodo & "-" & ex.Message & "');</script>")
        End Try
    End Sub

    ''' <summary>
    ''' Permite llenar los campos de la pestaña información del radicado
    ''' </summary>
    Private Sub InicializacionRadicado()
        Try
            Dim radicado As RadicadoDTO = RadicadoHelper.ConsultarDatosRadicadoPorCodigo(TxtCodigoSolicitud.Value)
            TxtNumeroRadicado.Text = radicado.NumeroRadicado
            txtCanalAtencion.Text = radicado.CanalAtencion.Nombre
            hddCodigoCanalAtencion.Value = radicado.CanalAtencion.Codigo
            txtFecha.Text = radicado.Fecha
            txtTipoSolicitante.Text = radicado.TipoSolicitante.Nombre
            hddCodigoTipoSolicitante.Value = radicado.TipoSolicitante.Codigo
            chkEsAnonimo.Checked = radicado.EsAnonimo
            txtTipoDocumento.Text = radicado.TipoDocId.Nombre
            hddCodigoTipoDocumento.Value = radicado.TipoDocumento.Codigo
            txtIdentificacion.Text = radicado.Identificacion
            txtRemitente.Text = radicado.Remitente

            If radicado.GrupoEtnicoReconoce Then
                rblGrupoEtnico.Items.FindByValue("1").Selected = True
            Else
                rblGrupoEtnico.Items.FindByValue("0").Selected = True
            End If

            txtSexoAsignado.Text = radicado.Sexo.Nombre
            hddCodigoSexoAsignado.Value = radicado.Sexo.Codigo
            If radicado.Genero IsNot Nothing Then
                txtIdentidadGenero.Text = radicado.Genero.Nombre
                hddCodigoIdentidadGenero.Value = radicado.Genero.Codigo
            End If

            If radicado.OrientacionSexual IsNot Nothing Then
                txtOrientacionSexual.Text = radicado.OrientacionSexual.Nombre
                hddCodigoIdentidadGenero.Value = radicado.OrientacionSexual.Codigo
            End If
            If radicado.ExpresionGenero IsNot Nothing Then
                txtExpresionGenero.Text = radicado.ExpresionGenero.Nombre
                hddCodigoExpresionGenero.Value = radicado.ExpresionGenero.Codigo
            End If
            If radicado.RangoEdad IsNot Nothing Then
                txtRangoEdad.Text = radicado.RangoEdad.Nombre
                hddCodigoRangoEdad.Value = radicado.RangoEdad.Codigo
            End If
        Catch
            Response.Write("<script language=""javascript"">alert('Error cargando la información del radicado!');</script>")
        End Try
    End Sub

    ''' <summary>
    ''' Permite cargar la información de los documentos en la pestaña Anexos del radicado recibido
    ''' </summary>
    Private Sub InicializacionDocumentos()
        Try
            'Dim listaDocumentos As List(Of DocumentoDTO) = DocumentoHelper.ConsultarDocumentosPorCodigoRadicado(TxtCodigoSolicitud.Value)

            ObtenerDataGridViewDocumentos()

        Catch ex As Exception
            Dim nombreMetodo = System.Reflection.MethodBase.GetCurrentMethod().Name
            LogWriter.WriteLog("RegistroDePeticionarios -" & nombreMetodo, ex)
            Throw New Exception("RegistroDePeticionarios -" & nombreMetodo & "-" & ex.Message, ex)
        End Try
    End Sub

    Private Sub ObtenerDataGridViewDocumentos()
        Try
            Dim listaDocumentos As List(Of DocumentoDTO) = DocumentoHelper.ConsultarDocumentosPorCodigoRadicado(TxtCodigoSolicitud.Value)
            Dim tabla As New DataTable

            tabla.Columns.Add("TituloArchivo", Type.GetType("System.String"))
            tabla.Columns.Add("FechaCreacion", Type.GetType("System.String"))
            tabla.Columns.Add("NombreUsuarioCreacion", Type.GetType("System.String"))
            tabla.Columns.Add("Ver", Type.GetType("System.String"))
            tabla.Columns.Add("RutaVirtualArchivo", Type.GetType("System.String"))
            For i As Integer = 0 To listaDocumentos.Count - 1

                Dim fila As DataRow
                ' create new row
                fila = tabla.NewRow
                fila("TituloArchivo") = listaDocumentos(i).TituloArchivo
                fila("FechaCreacion") = listaDocumentos(i).FechaCreacion.ToString
                fila("NombreUsuarioCreacion") = listaDocumentos(i).NombreUsuarioCreacion
                fila("Ver") = ""
                fila("RutaVirtualArchivo") = listaDocumentos(i).RutaVirtualArchivo

                tabla.Rows.Add(fila)
            Next

            grdDocumentos.DataSource = tabla
            grdDocumentos.DataBind()
        Catch ex As Exception
            Dim nombreMetodo = System.Reflection.MethodBase.GetCurrentMethod().Name
            LogWriter.WriteLog("RegistroDePeticionarios -" & nombreMetodo, ex)
            Throw New Exception("RegistroDePeticionarios -" & nombreMetodo & "-" & ex.Message, ex)
        End Try
    End Sub

    Protected Sub BtnVerDocumento_Click(sender As Object, e As ImageClickEventArgs)

        Dim row As GridViewRow = DirectCast(DirectCast(sender, ImageButton).NamingContainer, GridViewRow)
        DirectCast(row.NamingContainer, GridView).SelectedIndex = row.RowIndex
        Dim texto As String = grdDocumentos.SelectedRow.Cells(4).Text
        Dim array = texto.Split("//")
        Dim url As String = "https:/"
        For i As Integer = 1 To array.Length - 1
            If array(i).Length > 0 Then
                url = url & "/" & array(i)
            End If
        Next
        Me.Page.ClientScript.RegisterStartupScript(Me.Page.GetType(), "", "AbrirDocumento();", True)
        'Response.Write("<script language='javascript'>AbrirDocumento('" & url & "');</script>")
    End Sub
End Class