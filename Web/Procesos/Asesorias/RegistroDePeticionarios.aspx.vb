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

    Private Sub InitUltDataFromRequest()

        Dim data As New UltData()
        data.TaskID = Request.Cookies.Get("TaskID").Value
        data.SessionID = Request.Cookies.Get("SessionID").Value
        data.UserID = Request.Cookies.Get("UserID").Value

        Dim strError As String = String.Empty
        Dim cm As New ClientModel()
        If Not cm.GetTaskInfo(data, strError) Then
            LogWriter.WriteLog("RegistroSolicitud", "GetTaskInfo: " + strError)
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
            RadicadoHelper.InitRadicado(Me)
            If Not IsPostBack Then

                'Session.Clear()
                'InitUltDataFromRequest()



            End If

        Catch ex As Exception
            LogWriter.WriteLog("RegistroSolicitud", ex)
        End Try
    End Sub


End Class