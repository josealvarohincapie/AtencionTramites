Imports System.Data
Imports Web
Imports UltimusEIK
Imports UltimusEIK.ClientModel
Imports LogWriterHelper
Imports System.Configuration.ConfigurationManager
Imports Datos
Imports System.Linq
Imports System.Net
Imports System.IO
Imports Integracion
Imports Datos.Enums
Imports System.Globalization

Public Class FormHelper

    Public Const _TaskStatusCompleted As Integer = 3

    Private Shared Function GetHelperSQL() As HelperSQL
        Return New HelperSQL("BDCreditoDirecto")
    End Function

    Public Shared Sub LoadDropDownList(ByVal dropDownList As DropDownList, catalogo As Catalogo)
        dropDownList.Items.Clear()
        Dim dataTable As DataTable = DBMethods.GetCatalog(catalogo)
        If Not dataTable Is Nothing Then
            dropDownList.DataSource = dataTable
            dropDownList.DataValueField = dataTable.Columns(0).ColumnName
            dropDownList.DataTextField = dataTable.Columns(IIf(dataTable.Columns.Count > 1, 1, 0)).ColumnName
            dropDownList.DataBind()
        End If
        dropDownList.Items.Insert(0, New ListItem(String.Empty, 0))
    End Sub

    Public Shared Sub LoadCheckList(ByVal checkBoxList As CheckBoxList, catalogo As Catalogo)
        checkBoxList.Items.Clear()
        Dim dataTable As DataTable = DBMethods.GetCatalog(catalogo)
        If Not dataTable Is Nothing Then
            checkBoxList.DataSource = dataTable
            checkBoxList.DataValueField = dataTable.Columns(0).ColumnName
            checkBoxList.DataTextField = dataTable.Columns(IIf(dataTable.Columns.Count > 1, 1, 0)).ColumnName
            checkBoxList.DataBind()
        End If
    End Sub

    Public Shared Sub LoadDependentPaisCiudad(ByVal ddlCiudad As DropDownList, catalogo As Catalogo, ByVal value As String)
        ddlCiudad.Items.Clear()
        Dim dataTable As DataTable = DBMethods.GetCatalog(catalogo)
        If Not dataTable Is Nothing Then
            For Each row As DataRow In dataTable.Rows
                If (row(0).ToString().StartsWith(value)) Then
                    ddlCiudad.Items.Add(New ListItem(row(1), row(0)))
                End If
            Next
        End If
        ddlCiudad.Items.Insert(0, New ListItem(String.Empty, 0))
    End Sub

    Public Shared Sub LoadDropDownListWithDepartments(ByVal dropDownList As DropDownList)
        Dim strError As String = Nothing
        Try
            Dim FnEik As New ClientModel()
            Dim departments() As String = Nothing
            If Not FnEik.GetDepartments(departments, strError) Then
                LogWriter.WriteLog("GetDepartments", "GetUsers: " & strError)
            End If

            dropDownList.Items.Clear()
            dropDownList.Items.Insert(0, New ListItem(String.Empty, 0))
            For i = 0 To departments.Length - 1
                dropDownList.Items.Add(departments(i))
            Next
        Catch ex As Exception
            LogWriter.WriteLog("FormHelper", ex)
        End Try
    End Sub

    Public Shared Sub LoadListActividades(ByVal dropDownList As DropDownList, ByVal literal As Literal)
        Dim textHtml As String = String.Empty
        For Each item As ListItem In dropDownList.Items
            If Not String.IsNullOrEmpty(item.Text) Then
                textHtml += "<a href='#' data-value='" & item.Value & "' class='list-group-item'>" & item.Value & " - " & item.Text & "</a>"
            End If
        Next
        literal.Text = textHtml
    End Sub

    Public Shared Sub LoadExtensionArchivos(ByVal field As HiddenField)
        field.Value = String.Empty
        Dim dataTable As DataTable = DBMethods.GetCatalog(Catalog.ExtensionArchivos)
        If Not dataTable Is Nothing Then
            field.Value = String.Join(",", dataTable.Rows.Cast(Of DataRow).Select(Function(i) i.Item(1).ToString()))
        End If
    End Sub

    Private Shared Sub LoadInitialsDropDownList(ByRef form As RegistroSolicitud)
        LoadDropDownList(form.ddlTipoProducto, Catalog.TipoDeProducto)
        LoadDropDownList(form.ddlDivisa, Catalog.Moneda)
        LoadDropDownList(form.ddlTipoIdentificacion, Catalog.TipoDeIdentificacion)
        LoadDropDownList(form.ddlPais, Catalog.Pais)
        LoadDropDownList(form.ddlTipoDocumental, Catalog.TipoDeDocumento)
        LoadDropDownList(form.ddlTipoDocComplementario, Catalog.TipoDeDocumento)
        LoadDropDownList(form.ddlComplementarioPais, Catalog.Pais)
        LoadDropDownList(form.ddlComplementarioActividad, Catalog.ActividadEconomica)
        LoadListActividades(form.ddlComplementarioActividad, form.listActividades)
        LoadDropDownList(form.ddlComplementarioTipoIdentificacion, Catalog.TipoDeIdentificacion)
        LoadDropDownList(form.ddlComplementarioTipoComplementario, Catalog.TipoCodeudorAvalista)
        LoadCheckList(form.cblTipoCoincidencia, Catalog.TipoDeCoincidencia)
        LoadDropDownListWithDepartments(form.ddlArea)
        LoadExtensionArchivos(form.hfExtensionesValidas)
        For Each item As ListItem In form.ddlComplementarioActividad.Items
            If item.Value <> "0" Then
                item.Text = item.Value & " - " & item.Text
            End If
        Next
    End Sub

    Public Shared Sub ShowNotas(ByRef form As RegistroSolicitud)
        Dim strError As String = Nothing
        If Not GetHelperSQL().ConsultaGrilla(form.gvNotas, "PA_AdminObservacion_V2 1," & form.UltData.IncidentNo, strError) Then
            LogWriter.WriteLog("FormHelper", "ShowNotas: " & strError)
        End If
    End Sub

    Public Shared Sub ShowAdjuntos(ByRef form As RegistroSolicitud)
        Dim strError As String = Nothing
        If Not GetHelperSQL().ConsultaGrilla(form.gvAdjuntos, "PA_AdminAdjunto_V2 1," & form.UltData.IncidentNo, strError) Then
            LogWriter.WriteLog("FormHelper", "ShowAdjuntos: " & strError)
        End If
    End Sub

    Public Shared Sub InitForm(ByRef form As RegistroSolicitud, ByVal isPostBack As Boolean)

        Dim strError As String = Nothing
        Dim s As Solicitud

        If Not isPostBack Then
            LoadInitialsDropDownList(form)

            If form.UltData.IncidentNo > 0 Then
                ShowNotas(form)
                ShowAdjuntos(form)
                form.divReportesCentinela.Visible = True
            End If
        End If

        If isPostBack Then
            s = GetFormData(form)
        Else
            If form.UltData.IncidentNo = 0 Then
                s = GetInitialData(form.UltData.UserID)
            Else
                s = DBMethods.GetSolicitud(form.UltData.IncidentNo)
            End If
        End If

        SetFormData(form, s)

        If form.UltData.TaskStatus = _TaskStatusCompleted Then
            ShowFormDisabled(form)
        ElseIf form.UltData.IncidentNo = 0 Then
            ShowFormInitialMode(form)
        Else
            If form.UltData.StepLabel = Etapas.SolicitarCredito.Name Or form.UltData.StepLabel = Etapas.CorrespondenciaRecibida.Name Then
                ShowFormEditMode(form)
            Else
                ShowFormReadOnly(form)
            End If
        End If

        ShowDecisions(form)

        If form.dvDesicion.Visible = True AndAlso s.MontoRecomendado.HasValue = True AndAlso s.MontoRecomendado.Value > 0 Then
            form.lblMontoRecomendado.Visible = True
            form.tbMontoRecomendado.Visible = True
        End If

    End Sub

    Public Shared Sub ShowFormInitialMode(ByRef form As RegistroSolicitud)

        SetEnabledShowControls(form, True)
        form.SoloLectura.Value = "0"
        form.lbEnviar.Enabled = True
        form.lbGuardar.Visible = False
        form.ibNotas.Visible = False
        form.ibAdjuntos.Visible = False
        form.ibCorrespondenciaRecibida.Visible = False

    End Sub

    Public Shared Sub ShowFormEditMode(ByRef form As RegistroSolicitud)

        SetEnabledShowControls(form, True)
        form.SoloLectura.Value = "0"
        form.lbEnviar.Enabled = True
        form.lbGuardar.Visible = True

    End Sub

    Public Shared Sub ShowFormReadOnly(ByRef form As RegistroSolicitud)

        SetEnabledShowControls(form, False)
        form.SoloLectura.Value = "1"
        form.lbEnviar.Enabled = True
        form.lbGuardar.Visible = False
        form.tbObservacion.Enabled = True
        form.tbObservacion.ReadOnly = False
        form.btGuardarNota.Enabled = True
        form.btCerrarNota.Enabled = True
        form.fuAdjunto.Enabled = True
        form.btGuardarAdjunto.Enabled = True
        form.btLimpiar.Visible = False
        SetEnabledShowControls(form.gvAdjuntos, True)
        form.btComplementarioCerrar.Enabled = True
        ' Analisis
        form.btObservacion.Enabled = True
        form.dvDesicion.Disabled = False
        form.rbDesicion.Enabled = True
        form.ddlTipoDocumental.Enabled = True
        form.ddlTipoDocComplementario.Enabled = True
        form.btAgregarDocumento.Enabled = True
        form.btAgregarDocComplementario.Enabled = True
        SetEnabledShowControls(form.gvDocumentacion, True)
        SetEnabledShowControls(form.gvComplementarios, True)
        SetEnabledShowControls(form.gvDocumentacionComplementario, True)
        form.fuDocumentacion.Enabled = True
        form.btEliminarDocumento.Enabled = True
        form.dvUnidadCumplimiento.Disabled = False
        form.ddlEtapaDevolucion.Enabled = True
        form.ddlUsuarioAnalisis.Enabled = True
        form.lblMontoRecomendado.Disabled = False
        form.tbMontoRecomendado.ReadOnly = False
        form.panelDesicion.Disabled = False
        form.panelDevolucion.Disabled = False

    End Sub

    Public Shared Sub ShowFormDisabled(ByRef form As RegistroSolicitud)

        SetEnabledShowControls(form, False)
        form.lbEnviar.Enabled = False
        form.SoloLectura.Value = "1"

    End Sub

    Public Shared Sub ShowDecisions(ByRef form As RegistroSolicitud)

        If form.UltData.IncidentNo > 0 Then

            If form.UltData.StepLabel = Etapas.PrepararPresentacion.Name Then
                form.ddlEtapaDevolucion.Visible = True
                form.lblEtapaDevolucion.Visible = True
                form.ddlEtapaDevolucion.Items.Clear()
                form.ddlEtapaDevolucion.Items.Add(New ListItem(Etapas.EmitirConceptoGOC.Name))
                form.ddlEtapaDevolucion.Items.Add(New ListItem(Etapas.EmitirConceptoVJU.Name))
                form.ddlEtapaDevolucion.Items.Add(New ListItem(Etapas.EmitirConceptoVRI.Name))
            ElseIf form.UltData.StepLabel = Etapas.EmitirConceptoVRI.Name Then
                form.ddlEtapaDevolucion.Visible = True
                form.lblEtapaDevolucion.Visible = True
                form.ddlEtapaDevolucion.Items.Clear()
                form.ddlEtapaDevolucion.Items.Add(New ListItem(Etapas.ValidarAnalisisDRF.Name))
                form.ddlEtapaDevolucion.Items.Add(New ListItem(Etapas.AnalizarAmbientalSocial.Name))
            Else
                form.ddlEtapaDevolucion.Visible = False
                form.lblEtapaDevolucion.Visible = False
            End If

            form.dvDesicion.Visible = form.UltData.StepLabel <> Etapas.SolicitarCredito.Name And form.UltData.StepLabel <> Etapas.CorrespondenciaRecibida.Name
            form.rbDesicion.Items.Clear()
            form.lblUsuarioAnalisis.Visible = False
            form.ddlUsuarioAnalisis.Visible = False
            form.lblMontoRecomendado.Visible = False
            form.tbMontoRecomendado.Visible = False
            form.tbMontoRecomendado.ReadOnly = True

            Select Case form.UltData.StepLabel
                Case Etapas.SolicitarCredito.Name, Etapas.CorrespondenciaRecibida.Name
                    form.rbDesicion.Items.Add(New ListItem(Estados.Continuar.Name, Estados.Continuar.Id))
                    form.rbDesicion.Items.Add(New ListItem(Estados.Rechazado.Name, Estados.Rechazado.Id))
                Case Etapas.AnalizarRiesgos.Name, Etapas.ValidarAnalisisOCU.Name, Etapas.PreviabilidadDRF.Name, Etapas.AnalizarDRF.Name, Etapas.ValidarAnalisisDRF.Name, Etapas.AnalizarAmbientalSocial.Name, Etapas.PrepararPresentacion.Name
                    form.rbDesicion.Items.Add(New ListItem(Estados.Continuar.Name, Estados.Continuar.Id))
                    form.rbDesicion.Items.Add(New ListItem(Estados.Devuelto.Name, Estados.Devuelto.Id))
                Case Etapas.EmitirConceptoGOC.Name
                    form.rbDesicion.Items.Add(New ListItem(Estados.Continuar.Name, Estados.Continuar.Id))
                    form.rbDesicion.Items.Add(New ListItem(Estados.ContinuarPendientes.Name, Estados.ContinuarPendientes.Id))
                    form.rbDesicion.Items.Add(New ListItem(Estados.Devuelto.Name, Estados.Devuelto.Id))
                    form.rbDesicion.Items.Add(New ListItem(Estados.Rechazado.Name, Estados.Rechazado.Id))
                Case Etapas.AnalizarCapacidad.Name, Etapas.ValidarAnalisisDJU.Name, Etapas.EmitirConceptoVJU.Name
                    form.rbDesicion.Items.Add(New ListItem(Estados.Viable.Name, Estados.Viable.Id))
                    form.rbDesicion.Items.Add(New ListItem(Estados.NoViable.Name, Estados.NoViable.Id))
                    form.rbDesicion.Items.Add(New ListItem(Estados.Devuelto.Name, Estados.Devuelto.Id))
                Case Etapas.EmitirConceptoVRI.Name
                    form.rbDesicion.Items.Add(New ListItem(Estados.Recomendado.Name, Estados.Recomendado.Id))
                    form.rbDesicion.Items.Add(New ListItem(Estados.NoRecomendado.Name, Estados.NoRecomendado.Id))
                    form.rbDesicion.Items.Add(New ListItem(Estados.Rechazado.Name, Estados.Rechazado.Id))
                    form.rbDesicion.Items.Add(New ListItem(Estados.Devuelto.Name, Estados.Devuelto.Id))
                Case Etapas.RecomendarComiteExterno.Name
                    form.rbDesicion.Items.Add(New ListItem(Estados.Continuar.Name, Estados.Continuar.Id))
                    form.rbDesicion.Items.Add(New ListItem(Estados.Devuelto.Name, Estados.Devuelto.Id))
                    form.rbDesicion.Items.Add(New ListItem(Estados.Rechazado.Name, Estados.Rechazado.Id))
                Case Etapas.RecomendarComiteInterno.Name
                    form.rbDesicion.Items.Add(New ListItem(Estados.Aprobado.Name, Estados.Aprobado.Id))
                    form.rbDesicion.Items.Add(New ListItem(Estados.Devuelto.Name, Estados.Devuelto.Id))
                    form.rbDesicion.Items.Add(New ListItem(Estados.Rechazado.Name, Estados.Rechazado.Id))
                Case Etapas.AprobarJuntaDirectiva.Name
                    form.rbDesicion.Items.Add(New ListItem(Estados.Aprobado.Name, Estados.Aprobado.Id))
                    form.rbDesicion.Items.Add(New ListItem(Estados.Aplazado.Name, Estados.Aplazado.Id))
                    form.rbDesicion.Items.Add(New ListItem(Estados.Rechazado.Name, Estados.Rechazado.Id))
                Case Etapas.AsignacionDRC.Name
                    form.rbDesicion.Items.Add(New ListItem(Estados.Continuar.Name, Estados.Continuar.Id))
                    form.rbDesicion.Items.Add(New ListItem(Estados.Devuelto.Name, Estados.Devuelto.Id))
                    form.lblUsuarioAnalisis.Visible = True
                    form.ddlUsuarioAnalisis.Visible = True
                    LoadDropDownList(form.ddlUsuarioAnalisis, Catalog.UsuariosDRC)
                Case Etapas.PreviabilidadDRC.Name
                    form.rbDesicion.Items.Add(New ListItem(Estados.Continuar.Name, Estados.Continuar.Id))
                    form.rbDesicion.Items.Add(New ListItem(Estados.Devuelto.Name, Estados.Devuelto.Id))
                    form.rbDesicion.Items.Add(New ListItem(Estados.DevueltoMonto.Name, Estados.DevueltoMonto.Id))
                    form.lblMontoRecomendado.Visible = True
                    form.tbMontoRecomendado.Visible = True
                    form.tbMontoRecomendado.ReadOnly = False
                Case Etapas.AnalizarDRC.Name
                    form.rbDesicion.Items.Add(New ListItem(Estados.Continuar.Name, Estados.Continuar.Id))
                    form.rbDesicion.Items.Add(New ListItem(Estados.Devuelto.Name, Estados.Devuelto.Id))
                    form.rbDesicion.Items.Add(New ListItem(Estados.DevueltoMonto.Name, Estados.DevueltoMonto.Id))
                    form.rbDesicion.Items.Add(New ListItem(Estados.AnalisisOCU.Name, Estados.AnalisisOCU.Id))
                    form.rbDesicion.Items.Add(New ListItem(Estados.AnalisisDJU.Name, Estados.AnalisisDJU.Id))
                    form.rbDesicion.Items.Add(New ListItem(Estados.AnalisisDRF.Name, Estados.AnalisisDRF.Id))
                    form.lblMontoRecomendado.Visible = True
                    form.tbMontoRecomendado.Visible = True
                    form.tbMontoRecomendado.ReadOnly = False
                    form.panelDesicion.Attributes("class") = "form-group col-xs-12"
                    form.panelDevolucion.Visible = False
                Case Etapas.ValidarAnalisisDRC.Name
                    form.rbDesicion.Items.Add(New ListItem(Estados.Continuar.Name, Estados.Continuar.Id))
                    form.rbDesicion.Items.Add(New ListItem(Estados.Devuelto.Name, Estados.Devuelto.Id))
                    form.rbDesicion.Items.Add(New ListItem(Estados.DevueltoMonto.Name, Estados.DevueltoMonto.Id))
                    form.lblMontoRecomendado.Visible = True
                    form.tbMontoRecomendado.Visible = True
                    form.tbMontoRecomendado.ReadOnly = False
            End Select

            For Each item As ListItem In form.rbDesicion.Items
                If item.Value = Estados.Rechazado.Id Or item.Value = Estados.NoViable.Id Then
                    item.Attributes.Add("onclick", "this.checked=confirm('¿Realmente desea terminar el proceso?');")
                End If
            Next

        Else
            form.dvDesicion.Visible = False
        End If

    End Sub

    Public Shared Sub SetEnabledShowControls(ByRef o As Control, ByVal enabled As Boolean)

        If (o.GetType() Is GetType(TextBox)) Then
            Dim c As TextBox = CType(o, TextBox)
            c.ReadOnly = Not enabled
        ElseIf (o.GetType() Is GetType(DropDownList)) Then
            Dim c As DropDownList = CType(o, DropDownList)
            c.Enabled = enabled
        ElseIf (o.GetType() Is GetType(CheckBoxList)) Then
            Dim c As CheckBoxList = CType(o, CheckBoxList)
            c.Enabled = enabled
        ElseIf (o.GetType() Is GetType(CheckBox)) Then
            Dim c As CheckBox = CType(o, CheckBox)
            c.Enabled = enabled
        ElseIf (o.GetType() Is GetType(RadioButton)) Then
            Dim c As RadioButton = CType(o, RadioButton)
            c.Enabled = enabled
        ElseIf (o.GetType() Is GetType(RadioButtonList)) Then
            Dim c As RadioButtonList = CType(o, RadioButtonList)
            c.Enabled = enabled
        ElseIf (o.GetType() Is GetType(Button)) Then
            Dim c As Button = CType(o, Button)
            c.Enabled = enabled
        ElseIf (o.GetType() Is GetType(FileUpload)) Then
            Dim c As FileUpload = CType(o, FileUpload)
            c.Enabled = enabled
        ElseIf (o.GetType() Is GetType(GridView)) Then
            Dim c As GridView = CType(o, GridView)
            c.Enabled = enabled
        ElseIf (o.GetType() Is GetType(HyperLink)) Then
            Dim c As HyperLink = CType(o, HyperLink)
            c.Enabled = enabled
        ElseIf (o.GetType() Is GetType(HtmlInputText)) Then
            Dim c As HtmlInputText = CType(o, HtmlInputText)
            c.Disabled = Not enabled
        ElseIf (o.GetType() Is GetType(HtmlGenericControl)) Then
            Dim c As HtmlGenericControl = CType(o, HtmlGenericControl)
            c.Disabled = Not enabled
        End If

        For Each c As Control In o.Controls
            SetEnabledShowControls(c, enabled)
        Next

    End Sub

    Private Shared Function GetInitialData(ByVal userID As String) As Solicitud
        Dim strError As String = Nothing
        Dim FnEik As New ClientModel()

        Dim s As New Solicitud

        Dim strDepartment As String = String.Empty
        If FnEik.GetUserDepartment(userID, strDepartment, strError) Then
            s.Area = strDepartment
        End If

        s.Incidente = 0
        s.TipoProducto = String.Empty
        s.TipoCoincidencia = New String() {}

        Return s
    End Function

    Private Shared Sub SetFormData(ByRef form As RegistroSolicitud, ByVal s As Solicitud)

        form.lbProceso.Text = form.UltData.ProcessName
        form.lbIncidente.Text = form.UltData.IncidentNo.ToString()
        form.lbUsuario.Text = form.UltData.UserID
        form.lbFecha.Text = Now.ToString("dd/MM/yyyy")
        form.lbEtapa.Text = form.UltData.StepLabel

        If form.ddlArea.Items.FindByValue(s.Area) Is Nothing Then
            form.ddlArea.SelectedValue = Nothing
        Else
            form.ddlArea.SelectedValue = s.Area
        End If

        If form.ddlTipoProducto.Items.FindByValue(s.TipoProducto.ToString()) Is Nothing Then
            form.ddlTipoProducto.SelectedValue = Nothing
        Else
            form.ddlTipoProducto.SelectedValue = s.TipoProducto.ToString()
            form.ProyectFinance.Visible = IIf(form.ddlTipoProducto.SelectedValue = AppSettings("ProductoProjectFinance"), True, False)
        End If

        If form.ddlTipoIdentificacion.Items.FindByValue(s.TipoIdentificacion.ToString()) Is Nothing Then
            form.ddlTipoIdentificacion.SelectedValue = Nothing
        Else
            form.ddlTipoIdentificacion.SelectedValue = s.TipoIdentificacion.ToString()
        End If

        If form.UltData.IncidentNo > 0 Then
            If form.UltData.StepLabel <> Etapas.SolicitarCredito.Name And form.UltData.StepLabel <> Etapas.CorrespondenciaRecibida.Name Then
                form.tbFuncionarioRespCli.Text = s.ResponsableCliente
            End If
            form.tbFuncionarioRespCli.Enabled = False
            form.tbFechaSolicitud.Text = s.FechaSolicitud.ToString("dd/MM/yyyy")
            form.tbFechaSolicitud.Enabled = False
        Else
            form.tbFuncionarioRespCli.Enabled = False
            form.tbFechaSolicitud.Enabled = False
        End If

        If s.TipoPersona <> 0 Then
            form.rblTipoPersona.SelectedValue = s.TipoPersona
        Else
            form.rblTipoPersona.SelectedValue = Nothing
        End If

        form.tbFuncionarioRespSol.Text = s.ResponsableSolicitud

        form.tbSemejanza.Value = s.TipoSemejanza
        form.lbTipoCoincidencia.Text = s.DescripcionSemejanza
        form.tbUrlReporte.Value = s.UrlReporte

        For Each item As ListItem In form.cblTipoCoincidencia.Items
            item.Selected = s.TipoCoincidencia.Contains(item.Value)
        Next

        If form.ddlDivisa.Items.FindByValue(s.Divisa.ToString()) Is Nothing Then
            form.ddlDivisa.SelectedValue = Nothing
        Else
            form.ddlDivisa.SelectedValue = s.Divisa.ToString()
        End If

        form.tbValorDivisa.Text = s.ValorDivisa.ToString("#,##0").Replace(".", ",")
        If s.MontoRecomendado.HasValue Then
            form.tbMontoRecomendado.Text = s.MontoRecomendado.Value.ToString("#,##0").Replace(".", ",")
        Else
            form.tbMontoRecomendado.Text = ""
        End If

        form.tbIdentificacion.Text = s.Identificacion
        form.tbNombre.Text = s.Nombre

        If form.ddlPais.Items.FindByValue(s.Pais) Is Nothing Then
            form.ddlPais.SelectedValue = Nothing
            form.ddlCiudad.SelectedValue = Nothing
        Else
            form.ddlPais.SelectedValue = s.Pais
            LoadDependentPaisCiudad(form.ddlCiudad, Catalog.Ciudad, s.Pais)

            If form.ddlCiudad.Items.FindByValue(s.Ciudad) Is Nothing Then
                form.ddlCiudad.SelectedValue = Nothing
            Else
                form.ddlCiudad.SelectedValue = s.Ciudad
            End If
        End If

        form.tbCorreo.Text = s.Correo
        form.tbDireccion.Text = s.Direccion
        form.tbTelefono.Text = s.Telefono

        form.chkParticipacion.Checked = s.Participacion

        form.lblEstado.Text = IIf(form.ClienteExiste.Value = "1", "Cliente existente", IIf(form.ClienteExiste.Value = "0", "Cliente NO existente", ""))

        form.rblClienteDefinido.SelectedValue = IIf(form.UltData.IncidentNo <> 0 And s.Identificacion = String.Empty, 0, 1)

    End Sub

    Public Shared Function GetFormData(ByRef form As RegistroSolicitud) As Solicitud
        Dim s As New Solicitud
        s.Incidente = form.UltData.IncidentNo
        s.Area = form.ddlArea.SelectedValue
        s.TipoProducto = form.ddlTipoProducto.SelectedValue
        s.TipoIdentificacion = StringToInteger(form.ddlTipoIdentificacion.SelectedValue)
        s.Identificacion = form.tbIdentificacion.Text
        s.Nombre = form.tbNombre.Text
        s.Pais = form.ddlPais.SelectedValue
        s.Ciudad = form.ddlCiudad.SelectedValue
        s.Correo = form.tbCorreo.Text
        s.Direccion = form.tbDireccion.Text
        s.Telefono = form.tbTelefono.Text
        s.ResponsableCliente = form.tbFuncionarioRespCli.Text
        s.ResponsableSolicitud = form.tbFuncionarioRespSol.Text
        s.Divisa = form.ddlDivisa.SelectedValue
        s.ValorDivisa = Val(form.tbValorDivisa.Text.Replace(",", String.Empty))
        s.MontoRecomendado = IIf(form.tbMontoRecomendado.Visible, Val(form.tbMontoRecomendado.Text.Replace(",", String.Empty)), Nothing)
        s.TipoCoincidencia = form.cblTipoCoincidencia.Items.OfType(Of ListItem).Where(Function(i) i.Selected).Select(Function(i) i.Value).ToArray()
        s.TipoSemejanza = StringToInteger(form.tbSemejanza.Value)
        s.UrlReporte = form.tbUrlReporte.Value
        s.FechaSolicitud = Date.ParseExact(form.tbFechaSolicitud.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture)
        s.TipoPersona = IIf(form.rblTipoPersona.SelectedValue = "1", 1, IIf(form.rblTipoPersona.SelectedValue = "2", 2, Nothing))

        If form.rbDesicion.SelectedValue <> Nothing Then
            s.EstadoSolicitud = form.rbDesicion.SelectedValue
        End If
        s.Participacion = form.chkParticipacion.Checked

        Return s
    End Function

    Public Shared Function IntegerToString(ByVal i As Integer) As String
        Return IIf(i = 0, "", i.ToString())
    End Function

    Public Shared Function StringToInteger(ByVal s As String) As Integer
        Dim i As Integer = 0
        Integer.TryParse(s, i)
        Return i
    End Function

    Public Shared Function DecimalToString(ByVal d As Decimal) As String
        Return IIf(d = 0, "", d.ToString().Replace(",", "."))
    End Function

    Public Shared Function StringToDecimal(ByVal s As String) As Decimal
        Dim d As Decimal = 0
        Decimal.TryParse(s, d)
        Return d
    End Function

    Public Shared Function SanitizeString(ByVal s As String) As String
        Return s.Trim().ToLower().Replace("á", "a").Replace("é", "e").Replace("í", "i").Replace("ó", "o").Replace("ú", "u")
    End Function

    Public Shared Function SaveForm(ByRef form As RegistroSolicitud) As Boolean
        Dim s As Solicitud = GetFormData(form)
        Dim result As Boolean = True

        result = DBMethods.SaveSolicitud(form.UltData.IncidentNo, s)

        Return result
    End Function

    Public Shared Function ConsultCentinela(ByVal login As String, ByVal strNombre As String, ByVal numIdentificacion As String, ByRef numLista As String, ByRef strSemejanza As String, ByRef strUrl As String, ByRef strError As String) As Boolean
        Dim result As Boolean = True
        Try

            If AppSettings("HabilitaCentinela") = "1" Then

                If String.IsNullOrWhiteSpace(numIdentificacion) = True Or String.IsNullOrWhiteSpace(strNombre) = True Then
                    Return True
                End If

                Dim FnInt As New InterfacesWS()
                result = FnInt.ConsultaVIGIA(login, strNombre, numIdentificacion, numLista, strUrl, strError)

                strSemejanza = IIf(numLista = 1, "TOTAL", IIf(numLista = 2, "PARCIAL", "SIN C."))

            End If

        Catch ex As Exception
            LogWriter.WriteLog("FormHelper", ex)
            Return False
        Finally
            'FixForm(form)
        End Try

        Return result
    End Function

    Public Shared Function ConsultIsExistsIncident(ByVal Avalista As Boolean, ByRef form As RegistroSolicitud, ByRef incidentData As UltData) As Boolean
        Dim result As Boolean = False
        Dim strError As String = String.Empty
        Dim strSQL As String = String.Empty
        Dim nIncidente As Long

        Try
            If Avalista Then
                strSQL = "PA_AdminSolicitud_V2 3,null,'" & form.ddlComplementarioTipoIdentificacion.SelectedValue & "','" & form.tbComplementarioIdentificacion.Text & "'"
            Else
                strSQL = "PA_AdminSolicitud_V2 3,null,'" & form.ddlTipoIdentificacion.SelectedValue & "','" & form.tbIdentificacion.Text & "'"
            End If

            If GetHelperSQL().ConsultaValor(strSQL, nIncidente, strError) AndAlso nIncidente > 0 Then

                Dim UltDataVal As New UltData
                UltDataVal.ProcessName = form.UltData.ProcessName
                UltDataVal.IncidentNo = nIncidente

                Dim ws As New BISWS()
                If ws.GetIncidentInfo(UltDataVal, strError) Then

                    If (UltDataVal.IncidentStatus = 1) Then
                        Dim cm As New ClientModel()
                        Dim strUserName As String = String.Empty
                        Dim strXML As String = String.Empty
                        Dim strJFG As String = String.Empty
                        If ws.GetActiveTaskInfo(UltDataVal, strError) Then
                            If Not cm.UserData(UltDataVal.UserID, strUserName, strXML, strJFG, strError) Then
                                LogWriter.WriteLog("FormHelper", "UserData: " & strError)
                            End If

                            incidentData.StepLabel = UltDataVal.StepLabel
                            incidentData.IncidentNo = UltDataVal.IncidentNo
                            incidentData.UserID = IIf(String.IsNullOrEmpty(strUserName), UltDataVal.UserID, strUserName)
                            Return True
                        Else
                            LogWriter.WriteLog("FormHelper", "GetActiveTaskInfo: " & strError)
                        End If
                    End If
                Else
                    LogWriter.WriteLog("FormHelper", "GetIncidentInfo: " & strError)
                End If
            End If

        Catch ex As Exception
            LogWriter.WriteLog("FormHelper", ex)
            Return False
        End Try

        Return result
    End Function

    Public Shared Function ConsultT24(ByRef form As RegistroSolicitud, ByVal numDocumento As String, ByVal Avalista As Boolean, ByRef bActivo As Boolean, ByRef strNombre As String, ByRef strPais As String, ByRef strCiudad As String, ByRef strEmail As String, ByRef strError As String) As Boolean

        Dim result As Boolean = True
        Dim itemPais As New ListItem
        Try
            If AppSettings("HabilitaT24") = "1" Then

                Dim FnInt As New InterfacesWS()

                result = FnInt.ConsultaClienteT24(numDocumento, bActivo, strNombre, strPais, strCiudad, strEmail, strError)

                If result Then
                    form.ClienteExiste.Value = IIf(bActivo, "1", "0")
                    If bActivo Then

                        If Avalista Then
                            form.tbComplementarioNombre.Text = strNombre
                            form.tbComplementarioCorreo.Text = strEmail
                            itemPais = form.ddlComplementarioPais.Items.FindByText(strPais)
                        Else
                            form.tbNombre.Text = strNombre
                            form.tbCorreo.Text = strEmail
                            itemPais = form.ddlPais.Items.FindByText(strPais)
                        End If

                        If Not itemPais Is Nothing Then
                            If Avalista Then
                                form.ddlComplementarioPais.SelectedValue = itemPais.Value
                                LoadDependentPaisCiudad(form.ddlComplementarioCiudad, Catalog.Ciudad, itemPais.Value)

                                Dim itemCiudad As ListItem = form.ddlComplementarioCiudad.Items.FindByText(strCiudad)
                                If Not itemCiudad Is Nothing Then
                                    form.ddlComplementarioCiudad.SelectedValue = itemCiudad.Value
                                End If
                            Else
                                form.ddlPais.SelectedValue = itemPais.Value
                                LoadDependentPaisCiudad(form.ddlCiudad, Catalog.Ciudad, itemPais.Value)

                                Dim itemCiudad As ListItem = form.ddlCiudad.Items.FindByText(strCiudad)
                                If Not itemCiudad Is Nothing Then
                                    form.ddlCiudad.SelectedValue = itemCiudad.Value
                                End If
                            End If
                        End If
                    End If
                Else
                    form.ClienteExiste.Value = ""
                    If Avalista Then
                        form.lblComplementarioEstado.Text = "Ocurrió un error en la consulta de T24. Contacte al administrador."
                    Else
                        form.lblEstado.Text = "Ocurrió un error en la consulta de T24. Contacte al administrador."
                    End If
                    LogWriter.WriteLog("FormHelper", "ConsultaClienteT24: " & strError)
                End If

                If Avalista Then
                    form.lblComplementarioEstado.Text = IIf(form.ClienteExiste.Value = "1", "Cliente existente", IIf(form.ClienteExiste.Value = "0", "Cliente NO existente", ""))
                    FormHelper.ConsultCentinela(form.UltData.UserID, form.tbComplementarioNombre.Text, form.tbComplementarioIdentificacion.Text, form.tbComplementarioSemejanza.Value, Nothing, form.tbComplementarioUrlReporte.Value, strError)
                Else
                    form.lblEstado.Text = IIf(form.ClienteExiste.Value = "1", "Cliente existente", IIf(form.ClienteExiste.Value = "0", "Cliente NO existente", ""))
                    FormHelper.ConsultCentinela(form.UltData.UserID, form.tbNombre.Text, form.tbIdentificacion.Text, form.tbSemejanza.Value, form.lbTipoCoincidencia.Text, form.tbUrlReporte.Value, strError)
                End If

            End If

        Catch ex As Exception
            LogWriter.WriteLog("FormHelper", ex)
            Return False
        Finally
            'FixForm(form)
        End Try

        Return result
    End Function

    Public Shared Function DigitoNIT(ByVal sNit As String) As String
        Dim Temp As String, nDigito As Integer, i As Integer, Residuo As Integer = 0, Chequeo As Integer = 0
        Dim ArregloPA As String() = Split("3,7,13,17,19,23,29,37,41,43,47,53,59,67,71", ",")
        For i = 0 To Len(sNit) - 1
            Temp = Mid(sNit, Len(sNit) - i, 1)
            Chequeo = Chequeo + Val(Temp) * ArregloPA(i)
        Next
        Residuo = Chequeo Mod 11
        If Residuo > 1 Then nDigito = 11 - Residuo Else nDigito = Residuo
        Return nDigito
    End Function

End Class