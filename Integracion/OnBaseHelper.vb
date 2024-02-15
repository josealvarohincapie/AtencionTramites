Imports System.Configuration.ConfigurationManager
Imports System.Xml
Imports LogWriterHelper
Imports Microsoft.Win32

Public Class OnBaseHelper

    Private Shared Function ObtenerLlaveRegistro(ByVal Nombre As String) As String
        Dim localMachineRegistry As RegistryKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, IIf(Environment.Is64BitOperatingSystem, RegistryView.Registry64, RegistryView.Registry32))
        Dim rk As RegistryKey = localMachineRegistry.OpenSubKey("SOFTWARE\\UltimusParameters\\BDXOnBaseHelper", False)
        Return rk.GetValue(Nombre).ToString()
    End Function

    Private Shared ReadOnly Property UrlService As String
        Get
            Return ObtenerLlaveRegistro("UrlService")
        End Get
    End Property

    Private Shared ReadOnly Property Timeout As Integer
        Get
            Return Integer.Parse(ObtenerLlaveRegistro("Timeout"))
        End Get
    End Property

    Private Shared ReadOnly Property RequestConsultarDocumento As String
        Get
            Return ObtenerLlaveRegistro("RequestConsultarDocumento")
        End Get
    End Property

    Private Shared ReadOnly Property RequestConsultarData As String
        Get
            Return ObtenerLlaveRegistro("RequestConsultarData")
        End Get
    End Property

    Public Shared Function ConsultarDocumento(ByVal Id As String, ByVal Keywords As Boolean) As Document

        Dim request As String = RequestConsultarDocumento.Replace("%Id%", Id).Replace("%LoadKeywords%", IIf(Keywords, "True", "False"))
        Dim wsEcm As New WSECM.service()
        wsEcm.Url = UrlService
        wsEcm.Timeout = Timeout

        Dim response As String = wsEcm.Execute(request)
        Dim document As New XmlDocument()
        document.LoadXml(response)

        If document.SelectSingleNode("//xml/Response/Code").InnerText <> "00" Then
            Throw New Exception("ECM: " + document.SelectSingleNode("//xml/Response/Description").InnerText)
        End If

        Dim xmlDocument As XmlNode = document.SelectSingleNode("//xml/Response/Result/Document")
        Dim result As New Document With {
            .id = xmlDocument.Attributes("id").InnerText,
            .name = xmlDocument.Attributes("name").InnerText,
            .LatestRevision = Integer.Parse(xmlDocument("LatestRevision").InnerText),
            .DocumentType = Integer.Parse(xmlDocument("DocumentType").Attributes("id").InnerText),
            .DocumentTypeName = xmlDocument("DocumentType").InnerText,
            .DateStored = DateTime.Parse(xmlDocument("DateStored").InnerText)
        }

        If Keywords Then
            result.Keywords = xmlDocument("Keywords").ChildNodes.Cast(Of XmlNode)().ToDictionary(Function(t) t.Attributes("name").InnerText, Function(t) t.InnerText)
        End If

        Return result

    End Function

    Public Shared Function ConsultarData(ByVal Id As String) As Document

        Dim request As String = RequestConsultarData.Replace("%Id%", Id)
        Dim wsEcm As New WSECM.service()
        wsEcm.Url = UrlService
        wsEcm.Timeout = Timeout

        Dim response As String = wsEcm.Execute(request)
        Dim document As New XmlDocument()
        document.LoadXml(response)
        If document.SelectSingleNode("//xml/Response/Code").InnerText <> "00" Then
            Throw New Exception("ECM: " + document.SelectSingleNode("//xml/Response/Description").InnerText)
        End If

        Dim xmlDocument As XmlNode = document.SelectSingleNode("//xml/Response/Result/Document")
        Dim xmlPage As XmlNode = xmlDocument("Pages").FirstChild

        Return New Document With {
            .id = xmlDocument.Attributes("id").InnerText,
            .name = xmlDocument.Attributes("name").InnerText,
            .LatestRevision = Integer.Parse(xmlDocument("LatestRevision").InnerText),
            .DocumentType = Integer.Parse(xmlDocument("DocumentType").Attributes("id").InnerText),
            .DocumentTypeName = xmlDocument("DocumentType").InnerText,
            .DateStored = DateTime.Parse(xmlDocument("DateStored").InnerText),
            .File = New File With {
                .FileExtension = xmlPage.Attributes("extension").InnerText,
                .Data = Convert.FromBase64String(xmlPage.InnerText)
            }
        }

    End Function

End Class

Public Class Document
    Public id As String
    Public name As String
    Public LatestRevision As Integer
    Public DocumentType As Integer
    Public DocumentTypeName As String
    Public DateStored As DateTime
    Public Keywords As Dictionary(Of String, String)
    Public File As File
End Class

Public Class File
    Public FileExtension As String
    Public Keywords As List(Of Keyword)
    Public KTGKeywords As List(Of Keyword)
    Public Data As Byte()
End Class

Public Class Keyword
    Public Key As String
    Public Value As String
End Class
