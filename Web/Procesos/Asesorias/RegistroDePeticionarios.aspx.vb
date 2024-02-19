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

    '''<summary>
    '''Control TxtCodigoSolicitud.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Public WithEvents TxtCodigoSolicitud As Global.System.Web.UI.WebControls.HiddenField

    '''<summary>
    '''Control txtNumeroRadicado.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Public WithEvents TxtNumeroRadicado As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control hddCodigoCanalAtencion.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Public WithEvents hddCodigoCanalAtencion As Global.System.Web.UI.WebControls.HiddenField

    '''<summary>
    '''Control txtCanalAtencion.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
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

    '''<summary>
    '''Control chkEsAnonimo.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
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
    '''Control txtCedula.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtCedula As Global.System.Web.UI.WebControls.TextBox

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
            txtCedula.Text = radicado.Identificacion
            txtRemitente.Text = radicado.Remitente
            'rblGrupoEtnico.Select
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
            Response.Write("<script language=""javascript"">alert('Congratulations!');</script>")
        End Try
    End Sub

End Class