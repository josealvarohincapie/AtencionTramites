Imports System.IO
Imports System.Configuration.ConfigurationManager
Imports System.Text

Public Class LogWriter

    Public Shared Sub WriteLog(ByVal fileName As String, ByVal text As String)
        Try
            Dim now As Date = Date.Now()
            Dim file As New StreamWriter(Path.Combine(AppSettings("LogPath"), fileName & " " & now.ToString("yyyy-MM-dd") & ".txt"), True)
            file.WriteLine(now & Environment.NewLine & text & Environment.NewLine)
            file.Close()
        Catch ex As Exception
        End Try
    End Sub

    Public Shared Sub WriteLog(ByVal fileName As String, ByVal ex As Exception)
        Try
            WriteLog(fileName, ExceptionToString(ex, 0))
        Catch e As Exception
        End Try
    End Sub

    Private Shared Function ExceptionToString(ByVal ex As Exception, ByVal level As Integer)
        Dim indent1 As String = New String(" ", level * 5 + 2)
        Dim indent2 As String = ""
        If level <> 0 Then
            indent2 = indent1.Substring(5)
        End If

        Dim text As New StringBuilder()
        text.AppendLine("{")

        If String.IsNullOrEmpty(ex.HelpLink) = False Then
            text.AppendLine(indent1 & """HelpLink"": """ & ex.HelpLink.Replace("""", "\""") & """,")
        End If

        If Not ex.InnerException Is Nothing Then
            text.AppendLine(indent1 & """InnerException"": " & ExceptionToString(ex.InnerException, level + 1) & ",")
        End If

        If String.IsNullOrEmpty(ex.Message) = False Then
            text.AppendLine(indent1 & """Message"": """ & ex.Message.Replace("""", "\""") & """,")
        End If

        If String.IsNullOrEmpty(ex.Source) = False Then
            text.AppendLine(indent1 & """Source"": """ & ex.Source.Replace("""", "\""") & """,")
        End If

        If String.IsNullOrEmpty(ex.StackTrace) = False Then
            text.AppendLine(indent1 & """StackTrace"": """ & ex.StackTrace.Replace("""", "\""") & """,")
        End If

        If (Not ex.TargetSite Is Nothing) AndAlso String.IsNullOrEmpty(ex.TargetSite.Name) = False Then
            text.AppendLine(indent1 & """TargetSite"": """ & ex.TargetSite.Name.Replace("""", "\""") & """,")
        End If

        text.Append(indent2 & "}")

        Return text.ToString()
    End Function

End Class
