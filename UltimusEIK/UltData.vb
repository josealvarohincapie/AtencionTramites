Imports System.Xml

Public Class UltData

    Private _TaskID As String
    Public Property TaskID As String
        Get
            Return _TaskID
        End Get
        Set(ByVal value As String)
            _TaskID = value
        End Set
    End Property

    Private _SessionID As String
    Public Property SessionID As String
        Get
            Return _SessionID
        End Get
        Set(ByVal value As String)
            _SessionID = value
        End Set
    End Property

    Private _UserID As String
    Public Property UserID As String
        Get
            Return _UserID
        End Get
        Set(ByVal value As String)
            _UserID = value
        End Set
    End Property

    Private _TaskDataXmlDocument As XmlDocument
    Public Property DataXML As String
        Get
            If _TaskDataXmlDocument Is Nothing Then
                Return Nothing
            Else
                Return _TaskDataXmlDocument.OuterXml()
            End If
        End Get
        Set(ByVal value As String)
            Try
                _TaskDataXmlDocument = New XmlDocument()
                _TaskDataXmlDocument.LoadXml(value)
            Catch
                _TaskDataXmlDocument = Nothing
            End Try

        End Set
    End Property

    Private _IncidentDataXmlDocument As XmlDocument
    Public Property DataXML_Incident As String
        Get
            If _IncidentDataXmlDocument Is Nothing Then
                Return Nothing
            Else
                Return _IncidentDataXmlDocument.OuterXml()
            End If
        End Get
        Set(ByVal value As String)
            Try
                _IncidentDataXmlDocument = New XmlDocument()
                _IncidentDataXmlDocument.LoadXml(value)
            Catch
                _IncidentDataXmlDocument = Nothing
            End Try

        End Set
    End Property

    Private _IncidentNo As Int32
    Public Property IncidentNo As Int32
        Get
            Return _IncidentNo
        End Get
        Set(ByVal value As Int32)
            _IncidentNo = value
        End Set
    End Property

    Private _ProcessName As String
    Public Property ProcessName As String
        Get
            Return _ProcessName
        End Get
        Set(ByVal value As String)
            _ProcessName = value
        End Set
    End Property

    Private _StepLabel As String
    Public Property StepLabel As String
        Get
            Return _StepLabel
        End Get
        Set(ByVal value As String)
            _StepLabel = value
        End Set
    End Property

    Private _StartTime As Date
    Public Property StartTime As Date
        Get
            Return _StartTime
        End Get
        Set(ByVal value As Date)
            _StartTime = value
        End Set
    End Property

    Private _IncidentStatus As Int32
    Public Property IncidentStatus As Int32
        Get
            Return _IncidentStatus
        End Get
        Set(ByVal value As Int32)
            _IncidentStatus = value
        End Set
    End Property

    Private _TaskStatus As Int32
    Public Property TaskStatus As Int32
        Get
            Return _TaskStatus
        End Get
        Set(ByVal value As Int32)
            _TaskStatus = value
        End Set
    End Property

    Private _IncidentSummary As String
    Public Property IncidentSummary() As String
        Get
            Return _IncidentSummary
        End Get
        Set(ByVal value As String)
            _IncidentSummary = value
        End Set
    End Property

    Private _UserEmail As String
    Public Property UserEmail As String
        Get
            Return _UserEmail
        End Get
        Set(ByVal value As String)
            _UserEmail = value
        End Set
    End Property

    Public Function getSchemeValueAsString(ByRef variable As SchemeVariable) As String

        Dim value As String = Nothing

        Try
            If (Not _TaskDataXmlDocument Is Nothing) Then
                Dim nodeList As XmlNodeList = _TaskDataXmlDocument.GetElementsByTagName(variable.GetSchemeVariableTagName())
                If (nodeList.Count > 0) Then
                    value = nodeList(0).InnerText
                End If
            End If
        Catch
        End Try

        Return value
    End Function

    Public Function getSchemeIncidentValueAsString(ByRef variable As SchemeVariable) As String

        Dim value As String = Nothing

        Try
            If (Not _IncidentDataXmlDocument Is Nothing) Then
                Dim nodeList As XmlNodeList = _IncidentDataXmlDocument.GetElementsByTagName(variable.GetSchemeVariableTagName())
                If (nodeList.Count > 0) Then
                    value = nodeList(0).InnerText
                End If
            End If
        Catch
        End Try

        Return value
    End Function

    Public Function getSchemeValueAsInteger(ByRef variable As SchemeVariable) As Integer

        Dim value As Integer = 0

        Try
            Dim temp As String = getSchemeValueAsString(variable)
            value = Integer.Parse(temp)
        Catch
        End Try

        Return value
    End Function

    Public Function getSchemeValueAsBoolean(ByRef variable As SchemeVariable) As Boolean

        Dim value As Boolean = 0

        Try
            Dim temp As String = getSchemeValueAsString(variable)
            value = (temp = "true")
        Catch
        End Try

        Return value
    End Function

    Public Function getSchemeIncidentValueAsBoolean(ByRef variable As SchemeVariable) As Boolean

        Dim value As Boolean = 0

        Try
            Dim temp As String = getSchemeIncidentValueAsString(variable)
            value = (temp = "true")
        Catch
        End Try

        Return value
    End Function

    Public Function setSchemeStringValue(ByRef variable As SchemeVariable, ByVal value As String) As Boolean

        Try
            If (Not _TaskDataXmlDocument Is Nothing) Then
                Dim nodeList As XmlNodeList = _TaskDataXmlDocument.GetElementsByTagName(variable.GetSchemeVariableTagName())
                If (nodeList.Count > 0) Then
                    nodeList(0).Attributes.RemoveAll()
                    nodeList(0).InnerText = value
                    Return True
                End If
            End If
        Catch
        End Try
        Return False
    End Function

    Public Function setSchemeIntegerValue(ByRef variable As SchemeVariable, ByVal value As Integer) As Boolean

        Try
            Return setSchemeStringValue(variable, value.ToString())
        Catch
            Return False
        End Try
    End Function

    Public Function setSchemeDecimalValue(ByRef variable As SchemeVariable, ByVal value As Decimal) As Boolean

        Try
            Return setSchemeStringValue(variable, value.ToString().Replace(",", "."))
        Catch
            Return False
        End Try
    End Function

    Public Function setSchemeBooleanValue(ByRef variable As SchemeVariable, ByVal value As Boolean) As Boolean

        Try
            Return setSchemeStringValue(variable, IIf(value, "true", "false"))
        Catch
            Return False
        End Try
    End Function

    Public Function setSchemeValue(ByVal variable As String, ByVal value As String) As Boolean
        If Not _TaskDataXmlDocument.GetElementsByTagName(variable).Item(0).Attributes.Item(0) Is Nothing Then
            _TaskDataXmlDocument.GetElementsByTagName(variable).Item(0).Attributes.Item(0).Value = "false"
        End If
        _TaskDataXmlDocument.GetElementsByTagName(variable).Item(0).InnerText = value
        Return True
    End Function

End Class
