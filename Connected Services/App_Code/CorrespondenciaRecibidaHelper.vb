Imports Web
Imports System.Data
Imports System.IO
Imports System.Configuration.ConfigurationManager
Imports LogWriterHelper
Imports System.Globalization

Public Class CorrespondenciaRecibidaHelper

    Public Shared Sub ShowComunicacionRecibida(ByRef form As RegistroSolicitud)

        Try
            If form.UltData.IncidentNo > 0 Then
                Dim list As List(Of ComunicacionRecibida) = DBMethods.GetComunicacionRecibida(form.UltData.IncidentNo)
                If list.Count > 0 Then
                    form.gvCorrespondenciaRecibida.DataSource = list
                    form.gvCorrespondenciaRecibida.DataBind()
                    form.ibCorrespondenciaRecibida.Visible = True
                Else
                    form.ibCorrespondenciaRecibida.Visible = False
                End If
            End If
        Catch ex As Exception
            LogWriter.WriteLog("CorrespondenciaRecibidaHelper", ex)
        End Try

    End Sub

End Class
