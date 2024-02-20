Imports System.Xml

Public Class CustomHistory

    Function TaskHistory(ByVal UltData As UltData, ByRef strErr As String) As Boolean
        Dim bResultado As Boolean = False
        Try
            Dim strSql As String = "PA_AdminHistorico_V2 @Opcion=1"
            Dim FnBD As New Datos.HelperSQL("Correspondencia")
            strSql &= ",@Proceso='" & UltData.ProcessName & "',@Incidente=" & UltData.IncidentNo
            strSql &= ",@Usuario='" & UltData.UserID & "',@Etapa='" & UltData.StepLabel & "'"
            strSql &= ",@Inicio='" & UltData.StartTime.ToString("yyyy/MM/dd HH:mm:ss") & "'"
            strSql &= ",@Fin='" & Now.ToString("yyyy/MM/dd HH:mm:ss") & "'"
            strSql &= ",@Datos='" & UltData.DataXML & "'"
            FnBD.EjecutaAccion(strSql, strErr)
        Catch err As Exception
            strErr = err.Message
        End Try
        Return bResultado
    End Function

End Class
