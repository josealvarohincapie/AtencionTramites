Imports LogWriterHelper

Public Class BISWS

    Function GetTaskInfo(ByVal UltData As UltData, ByRef strErr As String) As Boolean
        Dim strXML As String = String.Empty
        Dim strXQry As String = String.Empty
        Dim oWS As New UltimusBIS.UltimusBISSoapClient
        If UltData.TaskID <> String.Empty Then
            strXQry = "for $a in /Incidents/Incident/Tasks/Task where $a/tTaskID='" & UltData.TaskID & "' return $a"
        Else
            strXQry = "for $a in /Incidents/Incident/Tasks/Task where $a/tProcessName='" & UltData.ProcessName & "' and $a/tStepLabel='" & UltData.StepLabel & "' and $a/tIncidentNo=" & UltData.IncidentNo & " return $a"
        End If
        Dim xDoc As XElement = oWS.RunBPMQuery(strXQry, False)
        UltData.ProcessName = xDoc.Element("Task").Element("tProcessName").Value
        UltData.StepLabel = xDoc.Element("Task").Element("tStepLabel").Value
        UltData.IncidentNo = xDoc.Element("Task").Element("tIncidentNo").Value
        UltData.UserID = xDoc.Element("Task").Element("tAssignedToUser").Value
        UltData.StartTime = xDoc.Element("Task").Element("tStartTime").Value
        UltData.TaskID = xDoc.Element("Task").Element("tTaskID").Value
        xDoc = Nothing
        Return True
    End Function

    Function GetIncidentInfo(ByVal UltData As UltData, ByRef strErr As String) As Boolean
        Dim strXML As String = String.Empty
        Dim strXQry As String = String.Empty
        Dim oWS As New UltimusBIS.UltimusBISSoapClient()

        strXQry = "for $a in /Incidents/Incident where $a/iProcessName='" & UltData.ProcessName & "' and $a/iIncidentNo=" & UltData.IncidentNo & " return $a"
        Dim xDoc As XElement = oWS.RunProcessQuery(UltData.ProcessName, 0, strXQry, False)
        If Not xDoc.Element("Incident") Is Nothing Then
            UltData.IncidentStatus = xDoc.Element("Incident").Element("iStatus").Value
            UltData.DataXML = xDoc.Element("Incident").Element("IncidentData").Element("Global").ToString()
        End If
        xDoc = Nothing
        Return True
    End Function

    Function GetActiveTaskInfo(ByVal UltData As UltData, ByRef strErr As String) As Boolean
        Dim strXML As String = String.Empty
        Dim strXQry As String = String.Empty
        Dim oWS As New UltimusBIS.UltimusBISSoapClient

        strXQry = "for $a in /Incidents/Incident/Tasks/Task where $a/tProcessName='" & UltData.ProcessName & "' and $a/tIncidentNo=" & UltData.IncidentNo & " and $a/tStatus=1 return $a"

        Dim xDoc As XElement = oWS.RunBPMQuery(strXQry, False)

        If xDoc.Element("Task") Is Nothing Then
            Return False
        End If

        UltData.ProcessName = xDoc.Element("Task").Element("tProcessName").Value
        UltData.StepLabel = xDoc.Element("Task").Element("tStepLabel").Value
        UltData.IncidentNo = xDoc.Element("Task").Element("tIncidentNo").Value
        UltData.UserID = xDoc.Element("Task").Element("tAssignedToUser").Value
        UltData.StartTime = xDoc.Element("Task").Element("tStartTime").Value
        UltData.TaskID = xDoc.Element("Task").Element("tTaskID").Value
        xDoc = Nothing
        Return True
    End Function

End Class