Imports Web
Imports LogWriterHelper
Imports System.Data
Imports System.Configuration.ConfigurationManager

Public Class ComplementariosHelper

    Private Const _ComplementariosData As String = "_ComplementariosData"

    Public Shared Sub InitComplementarios(ByRef form As RegistroSolicitud, ByVal isPostBack As Boolean)

        Try
            If form.UltData.IncidentNo = 0 And Not isPostBack Then
                form.Session(_ComplementariosData) = Nothing
            ElseIf Not (form.UltData.IncidentNo = 0 Or isPostBack) Then
                Dim complementarios As List(Of Complementario) = DBMethods.GetComplementarios(form.UltData.IncidentNo)
                form.Session(_ComplementariosData) = complementarios
            End If

            'FixData(form)
            ShowComplementarios(form)
            'SetShowMode(form)

        Catch ex As Exception
            LogWriter.WriteLog("ComplementariosHelper", ex)
        End Try

    End Sub

    Private Shared Sub ShowComplementarios(ByRef form As RegistroSolicitud)
        Dim data As List(Of Complementario) = GetFormData(form)
        form.gvComplementarios.DataSource = data
        form.gvComplementarios.DataBind()
        form.countComplementarios.InnerText = data.Count.ToString()
    End Sub

    Public Shared Sub DeleteComplementario(ByRef form As RegistroSolicitud, ByVal Identificacion As String)

        Dim complementarios As List(Of Complementario) = GetFormData(form)
        Dim s As Complementario = complementarios.FirstOrDefault(Function(i) i.Identificacion = Identificacion)

        If Not s Is Nothing Then
            DBMethods.DeleteComplementarios(form.UltData.IncidentNo, s.Identificacion)
            complementarios.Remove(s)
            form.Session(_ComplementariosData) = complementarios
            ShowComplementarios(form)
        End If
    End Sub

    Public Shared Sub ShowComplementario(ByRef form As RegistroSolicitud, ByVal Identificacion As String)

        Dim complementarios As List(Of Complementario) = GetFormData(form)
        Dim s As Complementario = complementarios.FirstOrDefault(Function(i) i.Identificacion = Identificacion)

        If Not s Is Nothing Then
            form.ddlComplementarioTipoComplementario.SelectedValue = s.IdTipoRelacion.ToString()
            form.ddlComplementarioTipoIdentificacion.SelectedValue = s.IdTipoIdentificacion.ToString()
            form.tbComplementarioIdentificacion.Text = s.Identificacion
            form.tbComplementarioNombre.Text = s.Nombre

            If form.ddlComplementarioPais.Items.FindByValue(s.Pais) Is Nothing Then
                form.ddlComplementarioPais.SelectedValue = Nothing
                form.ddlComplementarioCiudad.SelectedValue = Nothing
            Else
                form.ddlComplementarioPais.SelectedValue = s.Pais
                FormHelper.LoadDependentPaisCiudad(form.ddlComplementarioCiudad, Catalog.Ciudad, s.Pais)

                If form.ddlComplementarioCiudad.Items.FindByValue(s.Ciudad) Is Nothing Then
                    form.ddlComplementarioCiudad.SelectedValue = Nothing
                Else
                    form.ddlComplementarioCiudad.SelectedValue = s.Ciudad
                End If
            End If

            form.tbComplementarioCorreo.Text = s.Correo
            form.tbComplementarioDireccion.Text = s.Direccion
            form.tbComplementarioTelefono.Text = s.Telefono
            form.ddlComplementarioActividad.SelectedValue = s.IdActividad

            form.chkComplementarioParticipacion.Checked = s.Participacion

        End If
    End Sub

    Public Shared Sub InsertComplementario(ByRef form As RegistroSolicitud)

        Dim isNew As Boolean = IIf(form.hfComplementarioNew.Value = "1", True, False)
        Dim complementarios As List(Of Complementario) = GetFormData(form)
        Dim s As Complementario = Nothing

        If isNew Then
            s = New Complementario()
        Else
            Dim Identificacion As String = form.tbComplementarioIdentificacion.Text
            s = complementarios.FirstOrDefault(Function(i) i.Identificacion = Identificacion)
        End If

        GetDialogFormData(form, s)

        If isNew Then
            complementarios.Add(s)
        End If

        form.Session(_ComplementariosData) = complementarios

        ShowComplementarios(form)

    End Sub

    Private Shared Sub GetDialogFormData(ByRef form As RegistroSolicitud, ByRef s As Complementario)

        s.IdTipoRelacion = FormHelper.StringToInteger(form.ddlComplementarioTipoComplementario.SelectedValue)

        If Not form.ddlComplementarioTipoComplementario.SelectedItem Is Nothing Then
            s.TipoRelacion = form.ddlComplementarioTipoComplementario.SelectedItem.Text
        End If

        s.IdTipoIdentificacion = FormHelper.StringToInteger(form.ddlComplementarioTipoIdentificacion.SelectedValue)

        If Not form.ddlComplementarioTipoIdentificacion.SelectedItem Is Nothing Then
            s.TipoIdentificacion = form.ddlComplementarioTipoIdentificacion.SelectedItem.Text
        End If

        s.Identificacion = form.tbComplementarioIdentificacion.Text
        s.Nombre = form.tbComplementarioNombre.Text
        s.Pais = form.ddlComplementarioPais.SelectedValue
        s.Ciudad = form.ddlComplementarioCiudad.SelectedValue
        s.Correo = form.tbComplementarioCorreo.Text
        s.Direccion = form.tbComplementarioDireccion.Text
        s.Telefono = form.tbComplementarioTelefono.Text
        s.IdActividad = form.ddlComplementarioActividad.SelectedValue

        If Not form.ddlComplementarioActividad.SelectedItem Is Nothing Then
            s.Actividad = form.ddlComplementarioActividad.SelectedItem.Text
        End If

        s.Participacion = form.chkComplementarioParticipacion.Checked

        s.TipoSemejanza = FormHelper.StringToInteger(form.tbComplementarioSemejanza.Value)
        s.UrlReporte = form.tbComplementarioUrlReporte.Value

    End Sub

    Public Shared Sub SaveComplementarios(ByRef form As RegistroSolicitud)

        If form.UltData.IncidentNo = 0 Then
            Return
        End If

        Dim complementarios As List(Of Complementario) = GetFormData(form)
        For Each s As Complementario In complementarios
            DBMethods.DeleteComplementarios(form.UltData.IncidentNo, s.Identificacion)
            DBMethods.SaveComplementario(form.UltData.IncidentNo, s)
            DocumentationHelper.SaveDocumentation(form, True, False, s.Identificacion)
        Next

    End Sub

    Public Shared Function GetFormData(ByRef form As RegistroSolicitud) As List(Of Complementario)
        Dim complementarios As New List(Of Complementario)

        Dim o As Object = form.Session(_ComplementariosData)
        If Not o Is Nothing Then
            complementarios = DirectCast(o, List(Of Complementario))
        End If

        Return complementarios
    End Function


End Class
