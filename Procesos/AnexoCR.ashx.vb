Imports System.Web
Imports System.Web.Services
Imports System.IO
Imports LogWriterHelper
Imports Integracion

Public Class AnexoCR
    Implements System.Web.IHttpHandler

    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        Try
            Dim temp As String = New Simple3Des("NkSN3.5sAAlsp").DecryptData(context.Request.QueryString.Get("t"))
            If Not temp.StartsWith("Credito Directo Empresarial|") Then
                Return
            End If

            Dim values As String() = temp.Split("|")
            Dim documento As Document = OnBaseHelper.ConsultarData(values(2))
            context.Response.Clear()
            context.Response.ContentType = "application/" + documento.File.FileExtension
            context.Response.AddHeader("Content-Disposition", "inline;filename=" + documento.name + "." + documento.File.FileExtension)
            context.Response.BinaryWrite(documento.File.Data)
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