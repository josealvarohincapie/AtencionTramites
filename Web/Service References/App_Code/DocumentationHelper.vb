Imports Web
Imports System.Data
Imports System.IO
Imports System.Configuration.ConfigurationManager
Imports LogWriterHelper
Imports System.Globalization

Public Class DocumentationHelper

    Private Const _DocumentationData As String = "_DocumentationData"

    Public Sub New()

    End Sub

    Public Shared Sub InitDocumentation(ByRef form As RadicadoSolicitud, ByVal isPostBack As Boolean, ByVal Avalista As Boolean, ByVal editar As Boolean, ByVal Identificacion As String, ByVal razonSocial As String, ByVal participacionAccionaria As Boolean)

        Try

            form.hfFilesToken.Value = form.UltData.UserID.Substring(form.UltData.UserID.IndexOf("/") + 1) & Now.ToString("yyyyMMddHHmm")

            Dim s As Solicitud = FormHelper.GetFormData(form)
            Dim documentos As List(Of Documento) = Nothing

            If editar Then ' Opcion ver información de lista de codeudores
                documentos = form.Session(_DocumentationData & Identificacion)
                If documentos Is Nothing Then
                    documentos = GetAttachedDocuments(s, Identificacion, razonSocial)
                    form.Session(_DocumentationData & Identificacion) = documentos
                End If
            ElseIf form.UltData.IncidentNo = 0 Or isPostBack Then
                documentos = GetConfigDocuments(s, Identificacion, Avalista, razonSocial, participacionAccionaria)
                For Each documento As Documento In documentos
                    documento.Identificacion = Identificacion
                Next
                form.Session(_DocumentationData & Identificacion) = documentos
            Else
                documentos = GetAttachedDocuments(s, Identificacion, razonSocial)
                form.Session(_DocumentationData & Identificacion) = documentos
            End If

            ShowDocumentation(form, documentos, Avalista, Identificacion)

        Catch ex As Exception
            LogWriter.WriteLog("DocumentationHelper", ex)
        End Try

    End Sub

    Private Shared Function GetConfigDocuments(ByVal s As Solicitud, ByVal Identificacion As String, ByVal Avalista As Boolean, ByVal razonSocial As String, participacionAccionaria As Boolean) As List(Of Documento)
        Dim documentos As List(Of Documento) = DBMethods.GetConfigDocuments(Identificacion, Avalista, razonSocial, participacionAccionaria, s.TipoProducto, s.TipoPersona)
        Return documentos
    End Function

    Private Shared Function GetAttachedDocuments(ByVal s As Solicitud, ByVal Identificacion As String, razonSocial As String) As List(Of Documento)
        Dim documentos As List(Of Documento) = DBMethods.GetAttachedDocuments(s.Incidente, Identificacion, razonSocial)
        Return documentos
    End Function

    Private Shared Function CompleteFormData(ByRef form As RadicadoSolicitud, ByVal documentos As List(Of Documento), ByVal Avalista As Boolean, ByVal Identificacion As String) As List(Of Documento)
        Dim gv As GridView
        If Avalista Then
            gv = form.gvDocumentacionComplementario
        Else
            gv = form.gvDocumentacion
        End If

        For i As Integer = 0 To gv.Rows.Count - 1
            Dim txtObservaciones As HtmlTextArea = DirectCast(gv.Rows(i).FindControl("txtObservaciones"), HtmlTextArea)
            Dim txtFechaEmision As HtmlInputText = DirectCast(gv.Rows(i).FindControl("txtFechaEmision"), HtmlInputText)

            documentos(i).Observaciones = txtObservaciones.Value
            If Not Date.TryParseExact(txtFechaEmision.Value, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, documentos(i).FechaEmision) Then
                txtFechaEmision.Value = Nothing
            End If

            If Not Avalista Then
                documentos(i).RazonSocial = form.tbNombre.Text
            Else
                documentos(i).RazonSocial = form.tbComplementarioNombre.Text
            End If
        Next

        form.Session(_DocumentationData & Identificacion) = documentos
        Return documentos
    End Function

    Private Shared Function GetFormData(ByRef form As RadicadoSolicitud, ByVal Avalista As Boolean, ByVal completar As Boolean, ByVal Identificacion As String) As List(Of Documento)

        Dim documentos As New List(Of Documento)
        Dim o As Object = form.Session(_DocumentationData & Identificacion)
        If o Is Nothing Then
            documentos = New List(Of Documento)
        Else
            documentos = DirectCast(o, List(Of Documento))
        End If

        If completar Then
            documentos = CompleteFormData(form, documentos, Avalista, Identificacion)
        End If

        Return documentos

    End Function

    Private Shared Sub ShowDocumentation(ByRef form As RadicadoSolicitud, ByRef documentation As List(Of Documento), ByVal Avalista As Boolean, ByVal Identificacion As String)
        If Not Avalista Then
            form.gvDocumentacion.DataSource = documentation
            form.gvDocumentacion.DataBind()
            form.countDocumentacion.InnerText = documentation.Count.ToString()
        Else
            form.gvDocumentacionComplementario.DataSource = documentation
            form.gvDocumentacionComplementario.DataBind()
            form.countDocumentacionComplementario.InnerText = documentation.Count.ToString()
        End If
        form.Session(_DocumentationData & Identificacion) = documentation
    End Sub

    Public Shared Sub AddDocument(ByRef form As RadicadoSolicitud, ByVal Avalista As Boolean, ByVal Identificacion As String, ByVal codigo As String)
        Try

            Dim documentos As List(Of Documento) = GetFormData(form, Avalista, True, Identificacion)
            Dim temp As Documento = Nothing

            'Se comenta la siguiente línea para permitir que se puedan adicionar documentos con el mismo tipo documental 07.04.2018 (cuando ya existe en los doc obligatorios)
            'temp = documentos.FirstOrDefault(Function(t) t.Codigo = codigo And t.Identificacion = Identificacion)

            If temp Is Nothing Then
                temp = New Documento()
            Else
                Return
            End If

            temp.Identificacion = Identificacion
            If Not Avalista Then
                temp.Codigo = form.ddlTipoDocumental.SelectedValue
                temp.Registro = form.gvDocumentacion.Rows.Count + 1
                temp.Descripcion = form.ddlTipoDocumental.SelectedItem.Text
                temp.RazonSocial = form.tbNombre.Text
                temp.CanRemove = True
            Else
                temp.Codigo = form.ddlTipoDocComplementario.SelectedValue
                temp.Registro = form.gvDocumentacionComplementario.Rows.Count + 1
                temp.Descripcion = form.ddlTipoDocComplementario.SelectedItem.Text
                temp.RazonSocial = form.tbComplementarioNombre.Text
                temp.CanRemove = True
            End If

            documentos.Add(temp)

            ShowDocumentation(form, documentos, Avalista, Identificacion)

        Catch ex As Exception
            LogWriter.WriteLog("DocumentationHelper", ex)
        End Try
    End Sub

    Public Shared Sub RemoveDocument(ByRef form As RadicadoSolicitud, ByVal Avalista As Boolean, ByVal Identificacion As String, ByVal codigo As String, ByVal registro As String)
        Try

            Dim documentos As List(Of Documento) = GetFormData(form, Avalista, True, Identificacion)
            Dim temp As Documento = documentos.FirstOrDefault(Function(t) t.Codigo = codigo And t.Identificacion = Identificacion And t.Registro = registro)
            If temp Is Nothing Then
                Return
            End If

            documentos.Remove(temp)

            ' Renumerar
            Dim i As Integer = 0
            For Each doc As Documento In documentos
                i = i + 1
                doc.Registro = i
            Next

            ShowDocumentation(form, documentos, Avalista, Identificacion)

        Catch ex As Exception
            LogWriter.WriteLog("DocumentationHelper", ex)
        End Try
    End Sub

    Public Shared Sub UploadDocument(ByRef form As RadicadoSolicitud, ByVal Avalista As Boolean, ByVal Identificacion As String, ByVal codigo As String, ByVal registro As String)
        Try

            Dim documentos As List(Of Documento) = GetFormData(form, Avalista, True, Identificacion)

            Dim temp As Documento = documentos.FirstOrDefault(Function(t) t.Codigo = codigo And t.Identificacion = Identificacion And t.Registro = registro)
            If temp Is Nothing Then
                Return
            End If

            Dim strFile As String = form.hfFilesToken.Value & temp.Prefijo & form.fuDocumentacion.FileName
            Dim filePath As String = Path.Combine(AppSettings("DocumentacionPath"), strFile)
            form.fuDocumentacion.SaveAs(filePath)
            ' Copia OnBase
            'filePath = Path.Combine(AppSettings("OnBaseTransferPath"), strFile)
            'form.fuDocumentacion.SaveAs(filePath)

            temp.HasFile = True
            temp.Url = strFile

            ShowDocumentation(form, documentos, Avalista, Identificacion)

        Catch ex As Exception
            LogWriter.WriteLog("DocumentationHelper", ex)
        End Try
    End Sub

    Public Shared Sub CompleteDocuments(ByRef form As RadicadoSolicitud, ByVal Avalista As Boolean, ByVal Identificacion As String)
        Try

            Dim documentos As List(Of Documento) = GetFormData(form, Avalista, True, Identificacion)
            documentos = CompleteFormData(form, documentos, Avalista, Identificacion)

        Catch ex As Exception
            LogWriter.WriteLog("DocumentationHelper", ex)
        End Try
    End Sub

    Public Shared Sub SaveDocumentation(ByRef form As RadicadoSolicitud, ByVal Avalista As Boolean, ByVal completar As Boolean, ByVal Identificacion As String)

        Dim documentos As List(Of Documento) = GetFormData(form, Avalista, completar, Identificacion)

        If documentos.Count > 0 Then
            DBMethods.DeleteDocuments(form.UltData.IncidentNo, Identificacion)
            For Each d As Documento In documentos
                d.Identificacion = Identificacion
                DBMethods.SaveDocument(form.UltData.IncidentNo, d)
            Next
        End If

    End Sub

    Public Shared Sub CreateFileONBASE(ByRef form As RadicadoSolicitud)
        Try
            Dim transferPath As String = AppSettings("OnBaseTransferPath")
            Dim documentPath As String = AppSettings("DocumentacionPath")

            Dim metadata As String = ""
            Dim documentos As List(Of Documento) = DBMethods.GetAttachedDocuments(Val(form.lbIncidente.Text), String.Empty, String.Empty)

            Dim index As Integer = 1
            For Each documento As Documento In documentos.Where(Function(d) String.IsNullOrEmpty(d.Url) = False And IO.File.Exists(Path.Combine(documentPath, d.Url)))

                Dim ArchivoTransferencia As String = Path.GetFileNameWithoutExtension(documento.Url)
                ArchivoTransferencia = ArchivoTransferencia.Substring(ArchivoTransferencia.IndexOf("-") + 1)
                ArchivoTransferencia = ArchivoTransferencia.Substring(ArchivoTransferencia.IndexOf("-") + 1)
                ArchivoTransferencia = ArchivoTransferencia.Substring(ArchivoTransferencia.IndexOf("-") + 1)
                ArchivoTransferencia = ArchivoTransferencia.Substring(ArchivoTransferencia.IndexOf("-") + 1)
                ArchivoTransferencia = form.UltData.IncidentNo.ToString() & "_" & index.ToString() & "_" & documento.Codigo & "_" & OnBaseFileName(ArchivoTransferencia) & Path.GetExtension(documento.Url)

                metadata &= documento.Codigo & ";"
                metadata &= documento.FechaEmision.ToString("dd/MM/yyyy") & ";"
                metadata &= form.UltData.IncidentNo.ToString() & ";"
                metadata &= documento.Identificacion & ";"
                metadata &= form.tbNombre.Text & ";"
                metadata &= CodigoExtension(Path.GetExtension(documento.Url).Substring(1)) & ";"
                metadata &= ArchivoTransferencia & ";0;0;" & vbNewLine
                File.Copy(Path.Combine(documentPath, documento.Url), Path.Combine(transferPath, ArchivoTransferencia))

                index += 1
            Next

            Dim streamWriter As StreamWriter = File.CreateText(Path.Combine(transferPath, form.UltData.IncidentNo.ToString() & ".txt"))
            streamWriter.Write(metadata)
            streamWriter.Close()

        Catch ex As Exception
            LogWriter.WriteLog("DocumentationHelper", ex)
        End Try

    End Sub

    Public Shared Function CodigoExtension(ByVal ext As String) As String
        Dim codigo As String = String.Empty
        Dim dataTable As DataTable = DBMethods.GetCatalog(Catalog.ExtensionArchivos)
        If Not dataTable Is Nothing Then
            For Each row As DataRow In dataTable.Rows
                If row(1).ToString().Contains(ext) Then
                    codigo = row(0)
                    Exit For
                End If
            Next
        End If
        Return codigo
    End Function

    Private Const CaracteresValidos As String = "_abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"
    Private Shared Function OnBaseFileName(ByVal file As String) As String
        file = file.Replace("á", "a").Replace("é", "e").Replace("í", "i").Replace("ó", "o").Replace("ú", "u").Replace("ñ", "n")
        file = file.Replace("Á", "A").Replace("É", "E").Replace("Í", "I").Replace("Ó", "O").Replace("Ú", "U").Replace("Ñ", "N")
        file = file.Replace(" ", "_")
        While file.Contains("__")
            file = file.Replace("__", "_")
        End While

        Dim result As String = ""
        For Each c As Char In file.ToCharArray()
            If CaracteresValidos.Contains(c) Then
                result &= c
            End If
        Next

        If result.Length > 50 Then
            result = result.Substring(0, 50)
        End If
        Return result
    End Function

End Class
