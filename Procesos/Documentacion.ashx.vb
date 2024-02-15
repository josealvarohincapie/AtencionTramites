Imports System.Web
Imports System.Web.Services
Imports System.IO
Imports LogWriterHelper

Public Class Documentacion
    Implements System.Web.IHttpHandler

    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        Try
            Dim url As String = context.Request.QueryString.Get("u")
            url = Path.Combine(ConfigurationManager.AppSettings("DocumentacionPath"), url)
            Dim data() As Byte = My.Computer.FileSystem.ReadAllBytes(url)
            context.Response.Clear()
            context.Response.ContentType = "application/" + Path.GetExtension(url).Substring(1)
            context.Response.AddHeader("Content-Disposition", "filename=""" & Path.GetFileName(url) & """")
            context.Response.BinaryWrite(data)
            context.Response.Flush()
            context.Response.End()
        Catch ex As Exception
            LogWriter.WriteLog("RegistroSolicitud", ex)
        End Try
    End Sub

    ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class