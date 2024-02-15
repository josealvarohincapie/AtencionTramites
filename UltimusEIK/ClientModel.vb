Imports Ultimus.WFServer
Imports Ultimus.OC
Imports Ultimus.ClientServices
Imports System.Configuration.ConfigurationManager
Imports System.Xml
Imports System.Text
Imports System.Runtime.Remoting
Imports System.Runtime.Remoting.RemotingConfiguration
Imports LogWriterHelper

Public Class ClientModel

    Public Sub New()
        ' Activa servidor BPM Remoto
        ' Versión Referencias 8.3 SP2 (UltEik, UltEikClientServices, UltEikStructures y UltManagedStructs)
        If AppSettings("RemoteBPM") <> "" Then
            Dim oActCliEnt As ActivatedClientTypeEntry = IsRemotelyActivatedClientType(GetType(OrgChart))
            If oActCliEnt Is Nothing Then
                Dim sSrvBPM As String = "http://" & AppSettings("RemoteBPM") & "/UltWeb"
                RegisterActivatedClientType(GetType(OrgChart), sSrvBPM)
                RegisterActivatedClientType(GetType(User), sSrvBPM)
                RegisterActivatedClientType(GetType(Group), sSrvBPM)
                RegisterActivatedClientType(GetType(Task), sSrvBPM)
                RegisterActivatedClientType(GetType(Tasklist), sSrvBPM)
                RegisterActivatedClientType(GetType(TasklistFilter), sSrvBPM)
                RegisterActivatedClientType(GetType(Incident), sSrvBPM)
            End If
        End If
    End Sub

    Function UserData(ByVal strUser As String, ByRef strUserName As String, ByRef strXmlAssociates As String, ByRef strJFG As String, ByRef strErr As String) As Boolean
        Dim bRes As Boolean
        Try
            Dim oOC As New OrgChart
            Dim oUser As New User
            Dim oGroupsF() As Group = Nothing
            Dim oGroupsG() As Group = Nothing
            Dim oUsers() As User = Nothing
            oOC.FindUser(strUser, strUserName, Nothing, oUser)
            ' FullName
            strUserName = oUser.strUserFullName

            Dim lstAsc() As UserAssociate = Nothing
            ' Asociados
            strXmlAssociates = "<NewDataSet>"
            If oUser.GetAssociates(lstAsc) Then
                For n As Integer = 0 To lstAsc.Length - 1
                    strXmlAssociates &= "<Table>"
                    strXmlAssociates &= "<User>" & lstAsc(n).Associate & "</User>"
                    strXmlAssociates &= "<UserName>" & lstAsc(n).AssociateFullName & "</UserName>"
                    strXmlAssociates &= "</Table>"
                Next
            End If
            strXmlAssociates &= "</NewDataSet>"
            ' Funciones de Trabajo / Grupos (Colas)
            If oUser.GetUserJFGs(oGroupsF) And oUser.GetUserGroups(oGroupsG) Then
                If oGroupsF.Length > 0 Or oGroupsG.Length > 0 Then
                    strJFG = "("
                    For n As Integer = 0 To oGroupsF.Length - 1
                        strJFG &= "$a/tRecipient='" & oGroupsF(n).strGroupName & "' or "
                    Next
                    For n As Integer = 0 To oGroupsG.Length - 1
                        strJFG &= "$a/tRecipient='" & oGroupsG(n).strGroupName & "' or "
                    Next
                    strJFG = Mid(strJFG, 1, strJFG.Length - 4) & ")"
                End If
            End If

            bRes = True
        Catch err As Exception
            bRes = False
        End Try
        Return bRes
    End Function

    Function TasksUserBIS(ByVal strUser As String, ByVal strView As String, ByVal strJFG As String, ByRef strXml As String, ByRef strErr As String) As Boolean
        Dim bResultado As Boolean = False
        Try
            Dim sXmlB As New StringBuilder
            Dim xmlNodoT As Linq.XElement
            Dim oWS As New UltimusBIS.UltimusBISSoapClient
            'oWs.Url = AppSettings("wsUltBIS")
            Dim sXQry As String = ""
            Dim sDatXQry As String = ""
            Dim sFilXQry As String = ""
            Dim sTimXQry As String = "'" & Now.ToString("HH:mm:ss") & "'='" & Now.ToString("HH:mm:ss") & "'"
            Dim sDominio As String = AppSettings("DominioBPM")
            If Not (strView = "Cola de Tareas" And strJFG.Trim = "") Then
                ' XQuery
                Select Case strView
                    Case "Bandeja de Entrada"
                        sFilXQry &= "$a/tAssignedToUser='" & strUser & "' and $a/tStatus=1"
                    Case "Cola de Tareas"
                        sFilXQry &= "$a/tStatus=13 and " & strJFG
                    Case "Completado 1 Semana", "Completado 1 Mes", "Completado 2 Meses"
                        Dim nDias As Integer
                        Select Case strView
                            Case "Completado 1 Semana" : nDias = -7
                            Case "Completado 1 Mes" : nDias = -30
                            Case "Completado 2 Meses" : nDias = -60
                        End Select
                        sFilXQry &= "$a/tAssignedToUser='" & strUser & "' and $a/tStatus=3"
                        'sFilXQry &= " and $a/tEndTime>='" & Today.AddDays(nDias).ToString("yyyy-MM-dd") & "'"
                    Case "Abortado"
                        sFilXQry &= "$a/tAssignedToUser='" & strUser & "' and $a/tStatus=7"
                    Case "Reasignado"
                        sFilXQry &= "$a/tTaskUser='" & strUser
                        sFilXQry &= "' and $a/tStatus=1 and ($a/tSubStatus=4 or $a/tSubStatus=5)"
                End Select
                sDatXQry &= " return <Task>"
                sDatXQry &= "{$a/tTaskID}"
                sDatXQry &= "{$a/tProcessName}"
                sDatXQry &= "{$a/tProcessVersion}"
                sDatXQry &= "{$a/tStepLabel}"
                sDatXQry &= "{$a/tIncidentNo}"
                sDatXQry &= "{$a/tStatus}"
                sDatXQry &= "{$a/tSubStatus}"
                sDatXQry &= "{$a/tOverDueTime}"
                sDatXQry &= "{$a/tAssignedToUser}"
                sDatXQry &= "{$a/tStartTime}"
                sDatXQry &= "{$a/tEndTime}"
                'sDatXQry &= "{$a/IncidentData/Global/Nota}"
                sDatXQry &= "</Task>"
                sXmlB.Append("<NewDataSet>")
                Dim oIniEve As Date = Now
                sXQry = "for $a in /Incidents/Incident/Tasks/Task where " & sTimXQry & " and " & sFilXQry & sDatXQry
                xmlNodoT = oWS.RunBPMQuery(sXQry, False)
                Dim nTks As Long = xmlNodoT.Nodes.Count
                'Fn.GrabarLogEvento("Mensaje", "BPM", "TareasUsuarioBPM(" & strUser.Replace(sDominio, "") & "," & strView & "," & sProceso & "," & nTks & "): " & DateDiff(DateInterval.Second, oIniEve, Now))
                nTks = IIf(nTks > 0, nTks - 1, nTks)
                If Not xmlNodoT Is Nothing Then
                    Dim nSts, nSubSts As Integer
                    Dim sTskID, sPro, sStp, sAsgUsr, sImg, sDes As String
                    Dim sDueTim As String = ""
                    Dim sStrTim As String = ""
                    Dim sEndTim As String = ""
                    Dim sNot As String = ""
                    Dim sRad As String = ""
                    Dim sIde As String = ""
                    Dim sMon As String = ""
                    Dim sSed As String = ""
                    Dim sEst As String = ""
                    Dim nInc, nVer As Long
                    For Each xmlChild As Linq.XElement In xmlNodoT.Elements
                        If xmlChild.Value <> "" Then
                            sTskID = xmlChild.Element("tTaskID").Value
                            sPro = xmlChild.Element("tProcessName").Value
                            nVer = xmlChild.Element("tProcessVersion").Value
                            sStp = xmlChild.Element("tStepLabel").Value
                            nInc = xmlChild.Element("tIncidentNo").Value
                            nSts = xmlChild.Element("tStatus").Value
                            nSubSts = xmlChild.Element("tSubStatus").Value
                            sAsgUsr = xmlChild.Element("tAssignedToUser").Value
                            If Not xmlChild.Element("Nota") Is Nothing Then
                                sNot = xmlChild.Element("Nota").Value
                            End If
                            sImg = IIf(strView = "Iniciar", "Initiate16", ImageStatus(nSts, nSubSts))
                            sDes = IIf(strView = "Iniciar", "Iniciar", DescriptionStatus(nSts, nSubSts))
                            If Not xmlChild.Element("tOverDueTime") Is Nothing Then
                                sDueTim = xmlChild.Element("tOverDueTime").Value.Replace("T", " ")
                            End If
                            If Not xmlChild.Element("tStartTime") Is Nothing Then
                                sStrTim = xmlChild.Element("tStartTime").Value.Replace("T", " ")
                            End If
                            If Not xmlChild.Element("tEndTime") Is Nothing Then
                                sEndTim = xmlChild.Element("tEndTime").Value.Replace("T", " ")
                            End If
                            sXmlB.Append("<Table>")
                            sXmlB.Append("<TaskData>")
                            sXmlB.Append(sImg)
                            sXmlB.Append(",")
                            sXmlB.Append(sTskID)
                            sXmlB.Append(",")
                            sXmlB.Append(sPro)
                            sXmlB.Append(",")
                            sXmlB.Append(sStp)
                            sXmlB.Append(",")
                            sXmlB.Append(nInc)
                            sXmlB.Append(",")
                            sXmlB.Append(sDes)
                            sXmlB.Append("</TaskData>")
                            sXmlB.Append("<Proceso>")
                            sXmlB.Append(sPro)
                            sXmlB.Append("</Proceso>")
                            sXmlB.Append("<Etapa>")
                            sXmlB.Append(sStp)
                            sXmlB.Append("</Etapa>")
                            sXmlB.Append("<Incidente>")
                            sXmlB.Append(nInc)
                            sXmlB.Append("</Incidente>")
                            If strView = "Reasignado" Then
                                sXmlB.Append("<Usuario>")
                                sXmlB.Append(sAsgUsr.Replace(sDominio, ""))
                                sXmlB.Append("</Usuario>")
                            End If
                            If strView.IndexOf("Completado") <> -1 Then
                                sXmlB.Append("<Fin>")
                                sXmlB.Append(sEndTim)
                                sXmlB.Append("</Fin>")
                            Else
                                sXmlB.Append("<Vencimiento>")
                                sXmlB.Append(sDueTim)
                                sXmlB.Append("</Vencimiento>")
                            End If
                            sXmlB.Append("<Inicio>")
                            sXmlB.Append(sStrTim)
                            sXmlB.Append("</Inicio>")
                            sXmlB.Append("<Estado>")
                            sXmlB.Append(sDes)
                            sXmlB.Append("</Estado>")
                            sXmlB.Append("</Table>")
                        End If
                    Next
                End If
                sXmlB.Append("</NewDataSet>")
                strXml = sXmlB.ToString
                sXmlB = Nothing
                xmlNodoT = Nothing
                oWS = Nothing
                bResultado = True
            Else
                strXml = "<NewDataSet/>"
            End If
        Catch err As Exception
            bResultado = False
        End Try
        Return bResultado
    End Function

    Function TasksInitiateEIK(ByVal strUser As String, ByVal strView As String, ByRef strXml As String, ByRef strErr As String) As Boolean
        Dim bResultado As Boolean = False
        Dim oIniEve As Date = Now
        Try
            Dim oTaskList As New Tasklist
            Dim oFilter As New TasklistFilter
            oFilter.strArrUserName = New String() {strUser}
            oFilter.nFiltersMask = Filters.nFilter_Initiate
            strXml = "<NewDataSet>"
            If oTaskList.LoadFilteredTasks(oFilter, strErr) Then
                Dim sTskID, sPro, sStp, sImg, sDes As String
                Dim nInc, nVer As Long
                For n As Integer = 0 To oTaskList.GetTasksCount() - 1
                    sTskID = oTaskList.GetAt(n).strTaskId
                    sPro = oTaskList.GetAt(n).strProcessName
                    sStp = oTaskList.GetAt(n).strStepName
                    nVer = oTaskList.GetAt(n).nProcessVersion
                    sImg = "Initiate16"
                    sDes = "Iniciar"
                    strXml &= "<Table>"
                    strXml &= "<TaskData>" & sImg & "," & sTskID & "," & sPro & "," & sStp & "," & nInc & "," & sDes & "</TaskData>"
                    strXml &= "<Proceso>" & sPro & "</Proceso>"
                    strXml &= "<Version>" & nVer & "</Version>"
                    strXml &= "<Etapa>" & sStp & "</Etapa>"
                    strXml &= "</Table>"
                Next
            End If
            strXml &= "</NewDataSet>"
            bResultado = True
        Catch err As Exception
            bResultado = False
        End Try
        Return bResultado
    End Function

    Private Function DescriptionStatus(ByVal nStatus As Integer, ByVal nSubStatus As Integer) As String
        Dim sResultado As String = Nothing
        Select Case nStatus
            Case TaskStatuses.TASK_STATUS_INQUEUE
                Select Case nSubStatus
                    Case TaskSubStatuses.TASK_STATUS_ACTIVE_LATE, TaskSubStatuses.TASK_STATUS_ACTIVE_OVERDUE
                        sResultado = "En Cola (Urgente)"
                    Case Else
                        sResultado = "En Cola"
                End Select
            Case TaskStatuses.TASK_STATUS_ACTIVE, TaskStatuses.TASK_STATUS_DELAYED
                Select Case nSubStatus
                    Case TaskSubStatuses.TASK_STATUS_ACTIVE_LATE, TaskSubStatuses.TASK_STATUS_ACTIVE_OVERDUE
                        sResultado = "Urgente"
                    Case TaskSubStatuses.TASK_STATUS_ACTIVE_REASSIGNED
                        sResultado = "Reasignado"
                    Case 5
                        sResultado = "Reasignado (Urgente)"
                    Case Else
                        sResultado = "Activo"
                End Select
            Case TaskStatuses.TASK_STATUS_ABORTED
                sResultado = "Abortado"
            Case TaskStatuses.TASK_STATUS_COMPLETED
                Select Case nSubStatus
                    Case TaskSubStatuses.TASK_STATUS_COMPLETE_INCIDENT_COMPLETE
                        sResultado = "Incidente Completado"
                    Case Else
                        sResultado = "Completado"
                End Select
        End Select
        Return sResultado
    End Function

    Private Function ImageStatus(ByVal nStatus As Integer, ByVal nSubStatus As Integer) As String
        Dim sResultado As String = Nothing
        Select Case nStatus
            Case TaskStatuses.TASK_STATUS_INQUEUE
                Select Case nSubStatus
                    Case TaskSubStatuses.TASK_STATUS_ACTIVE_LATE, TaskSubStatuses.TASK_STATUS_ACTIVE_OVERDUE
                        sResultado = "Urgent16"
                    Case Else
                        sResultado = "InQueue16"
                End Select
            Case TaskStatuses.TASK_STATUS_ACTIVE
                Select Case nSubStatus
                    Case TaskSubStatuses.TASK_STATUS_ACTIVE_LATE, TaskSubStatuses.TASK_STATUS_ACTIVE_OVERDUE
                        sResultado = "Urgent16"
                    Case TaskSubStatuses.TASK_STATUS_ACTIVE_REASSIGNED
                        sResultado = "Reassign16"
                    Case 5 ' Reasignado (Urgente)
                        sResultado = "Urgent16"
                    Case Else
                        sResultado = "Active16"
                End Select
            Case TaskStatuses.TASK_STATUS_ABORTED
                sResultado = "Aborted16"
            Case TaskStatuses.TASK_STATUS_COMPLETED
                Select Case nSubStatus
                    Case TaskSubStatuses.TASK_STATUS_COMPLETE_INCIDENT_COMPLETE
                        sResultado = "Completed16"
                    Case Else
                        sResultado = "Completedhalf16"
                End Select
        End Select
        Return sResultado
    End Function

    Function AssignTask(ByVal strTaskID As String, ByVal strUser As String, ByRef strErr As String) As Boolean
        Dim bResultado As Boolean
        Dim oTask As New Task
        oTask.InitializeFromTaskId(strTaskID)
        If oTask.strAssignedToUser <> strUser Then
            bResultado = oTask.AssignTask(strUser)
            If bResultado Then
                Dim sDominio As String = AppSettings("DominioBPM")
                'Dim Fn As New cDatosBD(AppSettings("ProjectID"))
                'Fn.GrabarLogEvento("Mensaje", "BPM", "AsignacionTarea(" & oTask.strStepName & "," & oTask.nIncidentNo & "): " & sUser.Replace(sDominio, ""))
            End If
        End If
        Return bResultado
    End Function

    Function TakeBack(ByVal strTaskID As String, ByRef strErr As String) As Boolean
        Dim bResultado As Boolean
        Dim oTask As New Task
        oTask.InitializeFromTaskId(strTaskID)
        If oTask.strUser <> oTask.strAssignedToUser Then
            bResultado = oTask.TakeBack(oTask.strUser)
            If bResultado Then
                Dim sDominio As String = AppSettings("DominioBPM")
                'Dim Fn As New cDatosBD(AppSettings("ProjectID"))
                'Fn.GrabarLogEvento("Mensaje", "BPM", "AsignacionTarea(" & oTask.strStepName & "," & oTask.nIncidentNo & "): " & oTask.strUser.Replace(sDominio, ""))
            End If
        End If
        Return bResultado
    End Function

    Function SendTask(ByVal strTaskID As String, ByVal strProcess As String, ByVal strStep As String, ByVal strUser As String, ByRef nIncident As Long, ByVal strSummary As String, ByVal bSetXml As Boolean, ByVal strXmlTask As String, ByRef strErr As String) As Boolean
        Dim oIniEve As Date = Now
        Dim bResultado As Boolean = False
        Dim oTask As New Task
        If strTaskID.StartsWith("S") Then
            bResultado = oTask.InitializeFromInitiateTaskId(strUser, strTaskID)
        Else
            bResultado = oTask.InitializeFromTaskId(strTaskID)
        End If
        ' Envio de la tarea, reintenta si hay error
        For n As Integer = 1 To 3
            Try
                If bSetXml Then
                    bResultado = oTask.SetTaskXML(strXmlTask, False, strErr)
                End If
                If bResultado Then
                    strStep = oTask.strStepName
                    nIncident = oTask.nIncidentNo
                    If TaskActive(oTask.nTaskStatus) Then
                        ' Cola de Tareas
                        If oTask.nTaskStatus = TaskStatuses.TASK_STATUS_INQUEUE Then
                            Dim sUserAssigned As String = ""
                            bResultado = oTask.AssignQueueTask(strUser, sUserAssigned, strErr)
                            If bResultado Then bResultado = oTask.InitializeFromTaskId(oTask.strTaskId)
                        End If
                        ' Tarea Compartida
                        If oTask.strAssignedToUser <> strUser Then
                            bResultado = oTask.AssignTask(strUser)
                            If bResultado Then bResultado = oTask.InitializeFromTaskId(strTaskID)
                        End If
                        ' Envio de Tarea
                        If bResultado Then
                            bResultado = oTask.Send(Nothing, strSummary, False, nIncident, strErr)
                        End If
                        ' Historico
                        'If bResultado Then HistoricoTarea(oTask.strTaskId, oTask.strProcessName, nIncident, oTask.strSummary, oTask.strStepName, _
                        'oTask.strAssignedToUser, DateTime.FromOADate(oTask.dStartTime).ToString("yyyy/MM/dd HH:mm:ss"), sXml, sError)
                    End If
                End If
            Catch err As Exception
                bResultado = False
                strErr = err.Message
            End Try
            If bResultado Then
                Exit For
            Else
                System.Threading.Thread.Sleep(3000)
            End If
        Next
        'Fn.GrabarLogEvento("Mensaje", "BPM", "EnviarTareaBPM(" & sStep & "," & nIncident & "): " & DateDiff(DateInterval.Second, oIniEve, Now))
        If Not bResultado Then

        End If
        oTask = Nothing
        strXmlTask = Nothing
        Return bResultado
    End Function

    Function TaskData(ByVal strTaskID As String, ByVal strProcess As String, ByVal strStep As String, ByVal strUser As String, _
    ByRef nIncident As Long, ByRef bTaskActive As Boolean, ByRef bIncidentActive As Boolean, ByVal bGetXml As Boolean, _
    ByRef strXmlTask As String, ByRef sError As String) As Boolean
        Dim oIniEve As Date = Now
        Dim bResultado As Boolean = False
        Dim oTask As New Task
        If strTaskID.StartsWith("S") Then
            bResultado = oTask.InitializeFromInitiateTaskId(strUser, strTaskID)
        Else
            bResultado = oTask.InitializeFromTaskId(strTaskID)
        End If
        If bResultado Then
            bTaskActive = TaskActive(oTask.nTaskStatus)
            bIncidentActive = TaskIncidentActive(oTask.nTaskStatus, oTask.nTaskSubStatus)
            strProcess = oTask.strProcessName
            strStep = oTask.strStepName
            nIncident = oTask.nIncidentNo
            If bGetXml Then bResultado = oTask.GetTaskXML(strXmlTask, sError)
            oTask = Nothing
        Else
            If sError = Nothing Then sError = "No se pudo inicializar la tarea"
        End If
        'Fn.GrabarLogEvento("Mensaje", "BPM", "DatosTareaBPM(" & sStep & "," & nIncident & "): " & DateDiff(DateInterval.Second, oIniEve, Now))
        If Not bResultado Then

        End If
        Return bResultado
    End Function

    Function TaskActive(ByVal nTaskStatus As Integer) As Boolean
        Dim bResultado As Boolean
        If nTaskStatus = 0 Or nTaskStatus = TaskStatuses.TASK_STATUS_ACTIVE Or _
        nTaskStatus = TaskStatuses.TASK_STATUS_DELAYED Or _
        nTaskStatus = TaskStatuses.TASK_STATUS_INQUEUE Then
            bResultado = True
        Else
            bResultado = False
        End If
        Return bResultado
    End Function

    Function TaskIncidentActive(ByVal nTaskStatus As Integer, ByVal nTaskSubStatus As Integer) As Boolean
        Dim bResultado As Boolean = False
        If Not (nTaskStatus = TaskStatuses.TASK_STATUS_COMPLETED And _
        nTaskSubStatus = TaskSubStatuses.TASK_STATUS_COMPLETE_INCIDENT_COMPLETE) Then
            bResultado = True
        Else
            bResultado = False
        End If
        Return bResultado
    End Function

    Function IsMemberOfGroup(ByVal strGroup As String, ByVal strUser As String, ByRef bMember As Boolean, ByRef strErr As String) As Boolean
        Try
            Dim oOC As New OrgChart
            Dim oGroup As New Group

            bMember = False
            oOC.GetGroup(strGroup, oGroup)
            If (oGroup Is Nothing) Then
                Return True
            End If

            bMember = oGroup.IsMemberOfGroup(strUser)
            Return True
        Catch err As Exception
            strErr = err.Message
            Return False
        End Try
    End Function

    Function IsMemberOfDepartment(ByVal strDepartment As String, ByVal strUser As String, ByRef bMember As Boolean, ByRef strErr As String) As Boolean
        Try
            bMember = False
            strUser = strUser.ToLower().Trim()

            Dim oOC As New OrgChart
            Dim oDepartment As New Department

            Dim oSubDepartments() As Department = Nothing
            Dim oUsers() As User = Nothing

            oOC.FindDepartment(strDepartment, Nothing, oDepartment)
            If (oDepartment Is Nothing) Then
                Return True
            End If
            oDepartment.GetDepartmentMembers(oUsers)
            For Each user As User In oUsers
                If strUser = user.strUserName.ToLower().Trim() Then
                    bMember = True
                    Return True
                End If
            Next

            oDepartment.GetSubDepartments(oSubDepartments)
            For Each subDepartment As Department In oSubDepartments
                IsMemberOfDepartment(subDepartment.strDepartmentName, strUser, bMember, strErr)
                If bMember Then
                    Return True
                End If
            Next

            Return True
        Catch err As Exception
            strErr = err.Message
            Return False
        End Try
    End Function

    Function IsMemberOfJobFunction(ByVal strJobFunction As String, ByVal strUser As String, ByRef bMember As Boolean, ByRef strErr As String) As Boolean
        Try
            strJobFunction = strJobFunction.ToLower()
            bMember = False

            Dim regex As System.Text.RegularExpressions.Regex = New System.Text.RegularExpressions.Regex("-\d+$")
            Dim oOC As New OrgChart
            Dim jobFunctions() As String = oOC.GetUserJobFunctions(strUser, strErr)

            For Each jobFunction As String In jobFunctions
                jobFunction = jobFunction.ToLower()
                Dim match As System.Text.RegularExpressions.Match = regex.Match(jobFunction)
                If match.Success Then
                    jobFunction = jobFunction.Substring(0, Len(jobFunction) - Len(match.Value))
                End If

                If jobFunction = strJobFunction Then
                    bMember = True
                    Return True
                End If
            Next

            Return True
        Catch err As Exception
            strErr = err.Message
            Return False
        End Try
    End Function

    Function SetNodeValue(ByVal strProcess As String, ByVal nIncidente As Long, ByVal strNodeName As String, ByVal strNodeValue As String, ByRef strErr As String) As Boolean
        Dim bResultado As Boolean = True
        Dim oIncident As New Incident
        bResultado = oIncident.LoadIncident(strProcess, nIncidente)
        If bResultado Then
            bResultado = oIncident.SetNodeValue(strNodeName, strNodeValue, strErr)
            If bResultado Then
                bResultado = oIncident.SaveVariables()
                If bResultado Then

                Else
                    If strErr = Nothing OrElse strErr = "" Then bResultado = True
                End If
            End If
        Else
            If strErr = Nothing OrElse strErr = "" Then bResultado = True
        End If
        oIncident = Nothing
        If Not bResultado Then

        End If
        Return bResultado
    End Function

    Function GetNodeValue(ByVal strProcess As String, ByVal nIncidente As Long, ByVal strNodeName As String, ByRef strNodeValue As String, ByRef strErr As String) As Boolean
        Dim bResultado As Boolean = True
        Dim oIncident As New Incident
        bResultado = oIncident.LoadIncident(strProcess, nIncidente)
        If bResultado Then
            bResultado = oIncident.GetNodeValue(strNodeName, strNodeValue, strErr)
        Else
            If strErr = Nothing OrElse strErr = "" Then bResultado = True
        End If
        oIncident = Nothing
        If Not bResultado Then

        End If
        Return bResultado
    End Function

    Function GetGroup(ByRef strGroup As String, ByRef strMembers() As String, ByRef strErr As String) As Boolean
        Dim bResultado As Boolean
        Try
            Dim oOC As New OrgChart
            Dim oGroup As New Group
            oOC.GetGroup(strGroup, oGroup)
            ReDim strMembers(oGroup.GroupMembers.Length - 1)
            For i As Integer = 0 To oGroup.GroupMembers.Length - 1
                strMembers(i) = oGroup.GroupMembers(i).strMemberName
            Next
            bResultado = True
            oOC = Nothing
        Catch err As Exception
            bResultado = False
            strErr = err.Message
        End Try
        If Not bResultado Then

        End If
        Return bResultado
    End Function

    Function GroupWorkload(ByRef strGroup As String, ByRef strUser As String, ByRef strErr As String) As Boolean
        Dim bResultado As Boolean
        Try
            Dim oIniEve As Date = Now
            Dim oOC As New OrgChart
            Dim oGroup As New Group
            Dim nTareas, nTareasU, nPeso As Integer
            Dim sMiembro As String
            oOC.GetGroup(strGroup, oGroup)
            If oGroup.GroupMembers.Length = 1 Then
                sMiembro = oGroup.GroupMembers(0).strMemberName
                strUser = "USER:org=Business Organization,user=" & sMiembro
            Else
                For i As Integer = 0 To oGroup.GroupMembers.Length - 1
                    nTareas = 0
                    sMiembro = oGroup.GroupMembers(i).strMemberName
                    nPeso = oGroup.GroupMembers(i).nWeight
                    Dim oTaskList As New Tasklist
                    Dim oFilter As New TasklistFilter
                    oFilter.strArrUserName = New String() {sMiembro}
                    oFilter.nFiltersMask = Filters.nFilter_Current Or Filters.nFilter_Overdue Or Filters.nFilter_Urgent
                    If oTaskList.LoadFilteredTasks(oFilter, strErr) Then
                        ' Tareas pendientes
                        nTareas = oTaskList.GetTasksCount()
                    Else
                        nTareas = 0
                    End If
                    nTareas = IIf(nPeso = 0, nTareas, nTareas * nPeso)
                    ' Usuario con menos carga
                    If i = 0 Or nTareas <= nTareasU Then
                        strUser = "USER:org=Business Organization,user=" & sMiembro
                        nTareasU = nTareas
                    End If
                Next
            End If
            bResultado = True
            'Fn.GrabarLogEvento("Mensaje", "BPM", "CargaGrupo(" & sGrupo & "): " & DateDiff(DateInterval.Second, oIniEve, Now))
            oOC = Nothing
            oGroup = Nothing
        Catch err As Exception
            bResultado = False
            strErr = err.Message
        End Try
        If Not bResultado Then

        End If
        Return bResultado
    End Function

    Function GetDepartments(ByRef strDepartments() As String, ByRef strErr As String) As Boolean
        Dim bResultado As Boolean = False
        Try
            Dim oOC As New OrgChart
            Dim oDepartments() As Department = Nothing

            bResultado = oOC.GetDepartments(oDepartments)
            If bResultado Then
                ReDim strDepartments(oDepartments.Length - 1)
                For i As Integer = 0 To oDepartments.Length - 1
                    strDepartments(i) = oDepartments(i).strDepartmentName
                Next
            End If
            oOC = Nothing
        Catch err As Exception
            bResultado = False
            strErr = err.Message
        End Try
        Return bResultado
    End Function

    Public Class Usuario
        Private name As String
        Private id As String

        Public Property Nombre() As String
            Get
                Return Me.name
            End Get
            Set(ByVal Value As String)
                Me.name = Value
            End Set
        End Property

        Public Property Login() As String
            Get
                Return Me.id
            End Get
            Set(ByVal Value As String)
                Me.id = Value
            End Set
        End Property

    End Class

    Function GetUsers(ByRef listUser As List(Of Usuario), ByRef strErr As String) As Boolean
        Dim bResultado As Boolean = False
        Try
            listUser = New List(Of Usuario)
            Dim oOC As New OrgChart
            Dim oDepartments() As Department = Nothing

            bResultado = oOC.GetDepartments(oDepartments)
            If bResultado Then
                Dim strDepartments(oDepartments.Length - 1) As String
                For i As Integer = 0 To oDepartments.Length - 1
                    Dim oUsers() As User = Nothing
                    If oDepartments(i).GetDepartmentMembers(oUsers) Then
                        listUser = listUser.Union(From x In oUsers Select New Usuario With {.Login = x.strUserName, .Nombre = x.strUserFullName}).ToList()
                    End If
                Next
            End If
            oOC = Nothing
        Catch err As Exception
            bResultado = False
            strErr = err.Message
        End Try
        Return bResultado
    End Function

    Function GetUserDepartment(ByVal user As String, ByRef department As String, ByRef strErr As String) As Boolean
        Dim bResultado As Boolean = False
        Try
            Dim oOC As New OrgChart
            Dim oUser As New User

            bResultado = oOC.FindUser(user, Nothing, Nothing, oUser)
            If bResultado Then
                department = oUser.strDepartmentName
            End If
            oOC = Nothing
            oUser = Nothing
        Catch err As Exception
            bResultado = False
            strErr = err.Message
        End Try
        Return bResultado
    End Function

    Function GetDepartmentUsers(ByVal department As String, ByRef listUser As List(Of Usuario), ByRef strErr As String) As Boolean
        Dim bResultado As Boolean = False
        Try
            listUser = New List(Of Usuario)
            Dim oOC As New OrgChart
            Dim oDepartment As Department = Nothing

            bResultado = oOC.FindDepartment(department, Nothing, oDepartment)
            If bResultado Then
                Dim oUsers() As User = Nothing
                bResultado = oDepartment.GetDepartmentMembers(oUsers)
                If bResultado Then
                    listUser = oUsers.Select(Function(x) New Usuario With {.Login = x.strUserName, .Nombre = x.strUserFullName}).ToList()
                End If
            End If
            oOC = Nothing
            oDepartment = Nothing
        Catch err As Exception
            bResultado = False
            strErr = err.Message
        End Try
        Return bResultado
    End Function

    Function GetTaskInfo(ByVal UltData As UltData, ByRef strErr As String) As Boolean

        Dim orgChart As New OrgChart()
        Dim task As New Task()
        Dim orgChartUser As User = Nothing
        Dim userPreferences As UserPreferences = Nothing
        Dim strError As String = String.Empty
        Dim taskXML As String = String.Empty

        If Not task.InitializeFromTaskId(UltData.TaskID) Then
            If Not task.InitializeFromInitiateTaskId(UltData.UserID, UltData.TaskID) Then
                strErr = "GetTaskInfo(" + UltData.UserID + ", " + UltData.TaskID + "): Task initialize error"
                Return False
            End If
        End If

        If Not task.GetTaskXML(taskXML, strError) Then
            strErr = "GetTaskInfo(" + UltData.UserID + ", " + UltData.TaskID + ") / GetTaskXML: " + strError
        End If

        If orgChart.FindUser(UltData.UserID, Nothing, Nothing, orgChartUser) Then
            orgChartUser.GetUserPrefs(userPreferences)
        End If

        UltData.UserID = IIf(String.IsNullOrEmpty(task.strAssignedToUser), task.strUser, task.strAssignedToUser)
        UltData.IncidentNo = task.nIncidentNo
        UltData.ProcessName = task.strProcessName
        UltData.StepLabel = task.strStepName
        UltData.StartTime = DateTime.FromOADate(task.dStartTime)
        UltData.TaskStatus = task.nTaskStatus
        UltData.DataXML = taskXML
        UltData.IncidentSummary = task.strSummary
        If Not userPreferences Is Nothing Then
            UltData.UserEmail = userPreferences.EmailAddress
        End If
        Return True

    End Function

    Public Function GetUsersIncident(ByVal strProcess As String, ByVal nIncident As Long, ByRef strUsers As String, ByRef strError As String) As String
        Dim bResultado As Boolean
        Try
            strUsers = String.Empty
            Dim oTaskList As New Tasklist
            Dim oFilter As New TasklistFilter
            Dim oOC As New OrgChart
            oFilter.strProcessNameFilter = strProcess
            oFilter.nIncidentNo = nIncident
            If oTaskList.LoadFilteredTasks(oFilter, strError) Then
                For n As Integer = 0 To oTaskList.GetTasksCount() - 1
                    strUsers &= oOC.GetEmailAddress(oTaskList.GetAt(n).strAssignedToUser, strError) & ";"
                Next
            End If
        Catch err As Exception
            bResultado = False
            strError = err.Message
        End Try
        Return bResultado
    End Function

    Public Function GetIncidentData(ByVal strProcess As String, ByVal nIncident As Long, ByRef strXMLIncident As String, ByRef strError As String) As Boolean
        Dim bResultado As Boolean
        strXMLIncident = String.Empty
        Try
            Dim incident As New Incident

            If (incident.LoadIncident(strProcess, nIncident)) Then
                If (incident.GetIncidentXML(strXMLIncident, strError)) Then
                    bResultado = True
                End If
            End If

        Catch err As Exception
            bResultado = False
            strError = err.Message
        End Try
        Return bResultado
    End Function

    Public Function GetIncident(ByVal strProcess As String, ByVal nIncident As Long, ByRef strError As String) As String
        Dim bResultado As Boolean
        Try
            Dim incident As New Incident
            Dim strXML As String = String.Empty
            strError = String.Empty

            incident.LoadIncident(strProcess, nIncident)
            incident.GetIncidentXML(strXML, strError)

            Dim xml As New XmlDocument
            xml.LoadXml(strXML)

            setSchemeValue(xml, "ActivarAnalizarDJU", 0)
            setSchemeValue(xml, "ActivarAnalizarOCU", 0)
            setSchemeValue(xml, "ActivarAnalizarDRF", 0)
            setSchemeValue(xml, "ActivarAnalizarAmbienteSocial", 0)
            setSchemeValue(xml, "ActivarPreviabilidadDRF", 0)

            If (incident.SetIncidentXML(xml.OuterXml, False, strError)) Then
                incident.SaveVariables()
            End If

        Catch err As Exception
            bResultado = False
            strError = err.Message
        End Try
        Return bResultado
    End Function

    Public Function setSchemeValue(ByRef strXML As XmlDocument, ByVal variable As String, ByVal value As String) As Boolean
        If Not strXML.GetElementsByTagName(variable).Item(0).Attributes.Item(0) Is Nothing Then
            strXML.GetElementsByTagName(variable).Item(0).Attributes.Item(0).Value = "false"
        End If
        strXML.GetElementsByTagName(variable).Item(0).InnerText = value
        Return True
    End Function

End Class