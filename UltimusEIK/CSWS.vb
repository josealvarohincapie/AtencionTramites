Imports System.Xml.Linq
Imports System.Xml
Imports LogWriterHelper

Public Class CSWS

    Function LoginUser(ByVal strDomain As String, ByVal strUser As String, ByVal strPassword As String, ByRef strSessionID As String, ByRef strErr As String) As Boolean
        Dim oWS As New ClientServicesWS.ServicesSoapClient
        Return oWS.LoginUser(strDomain, strUser, strPassword, strSessionID, strErr)
    End Function

    Function IsValidSessionID(ByVal strSessionID As String, ByVal strUser As String) As Boolean
        Dim oWS As New ClientServicesWS.ServicesSoapClient
        Return oWS.IsValidSessionID(strSessionID, strUser)
    End Function

    Function SendTask(ByVal UltData As UltData, ByRef strErr As String, ByVal prioridad As Integer) As Boolean
        Dim oWS As New ClientServicesWS.ServicesSoapClient
        oWS.CheckOutTask(UltData.SessionID, UltData.TaskID, strErr)
        If oWS.SendTask(UltData.SessionID, UltData.TaskID, UltData.IncidentSummary, Nothing, UltData.DataXML, True, False, prioridad, UltData.IncidentNo, strErr) Then
            Dim FnCH As New CustomHistory
            FnCH.TaskHistory(UltData, strErr)
            Return True
        Else
            LogWriter.WriteLog("CSWS", "SendTask('" + UltData.SessionID + "', '" + UltData.TaskID + "', '" + UltData.IncidentSummary + "', Nothing, '" + UltData.DataXML + "', True, False, " + prioridad.ToString() + ", " + UltData.IncidentNo.ToString() + ", strErr)" + vbNewLine + " - strErr: " + strErr)
            Return False
        End If
    End Function

    Function GetUserEmailAddress(ByVal UltData As UltData, ByRef strErr As String) As Boolean
        Dim oWS As New ClientServicesWS.ServicesSoapClient
        Return oWS.GetUserEmailAddress(UltData.UserID, UltData.UserEmail, strErr)
    End Function

    Public Sub SetVarValue(ByVal xdoc As XmlDocument, ByVal strName As String, ByVal strValue As String)
        If Not xdoc.GetElementsByTagName(strName).Item(0).Attributes.Item(0) Is Nothing Then
            xdoc.GetElementsByTagName(strName).Item(0).Attributes.Item(0).Value = "false"
        End If
        xdoc.GetElementsByTagName(strName).Item(0).InnerText = strValue
    End Sub

End Class
