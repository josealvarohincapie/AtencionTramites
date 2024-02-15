Imports System.Configuration.ConfigurationManager
Imports UltimusEIK
Imports UltimusEIK.ClientModel
Imports Integracion
Imports System.Xml
Imports Datos
Imports System.IO
Imports LogWriterHelper
Imports System.Net
Imports System.Data.OleDb
Imports System.Threading
Imports System.Web.Services
Imports Web.WcfCorrespondenciaRecibida

Public Class RegistroSolicitud
    Inherits System.Web.UI.Page

    Public WithEvents form1 As Global.System.Web.UI.HtmlControls.HtmlForm
    Public WithEvents ScriptManager1 As Global.System.Web.UI.ScriptManager
    Public WithEvents FechaInicioEtapa As Global.System.Web.UI.WebControls.HiddenField
    Public WithEvents SoloLectura As Global.System.Web.UI.WebControls.HiddenField
    Public WithEvents ClienteExiste As Global.System.Web.UI.WebControls.HiddenField
    Public WithEvents lbEnviar As Global.System.Web.UI.WebControls.LinkButton
    Public WithEvents lbGuardar As Global.System.Web.UI.WebControls.LinkButton
    Public WithEvents ibNotas As Global.System.Web.UI.HtmlControls.HtmlAnchor
    Public WithEvents ibAdjuntos As Global.System.Web.UI.HtmlControls.HtmlAnchor
    Public WithEvents ibCorrespondenciaRecibida As Global.System.Web.UI.HtmlControls.HtmlAnchor
    Public WithEvents lbProceso As Global.System.Web.UI.WebControls.Label
    Public WithEvents lbEtapa As Global.System.Web.UI.WebControls.Label
    Public WithEvents lbIncidente As Global.System.Web.UI.WebControls.Label
    Public WithEvents lbFecha As Global.System.Web.UI.WebControls.Label
    Public WithEvents lbUsuario As Global.System.Web.UI.WebControls.Label
    Public WithEvents UpdatePanelPrincipal As Global.System.Web.UI.UpdatePanel
    Public WithEvents ddlArea As Global.System.Web.UI.WebControls.DropDownList
    Public WithEvents ddlTipoProducto As Global.System.Web.UI.WebControls.DropDownList
    Public WithEvents chkParticipacion As Global.System.Web.UI.WebControls.CheckBox
    Public WithEvents lblEstado As Global.System.Web.UI.WebControls.Label
    Public WithEvents lblComplementarioEstado As Global.System.Web.UI.WebControls.Label
    Public WithEvents rblTipoPersona As Global.System.Web.UI.WebControls.RadioButtonList
    Public WithEvents ddlTipoIdentificacion As Global.System.Web.UI.WebControls.DropDownList
    Public WithEvents tbIdentificacion As Global.System.Web.UI.WebControls.TextBox
    Public WithEvents tbNombre As Global.System.Web.UI.WebControls.TextBox
    Public WithEvents ddlPais As Global.System.Web.UI.WebControls.DropDownList
    Public WithEvents ddlCiudad As Global.System.Web.UI.WebControls.DropDownList
    Public WithEvents tbCorreo As Global.System.Web.UI.WebControls.TextBox
    Public WithEvents tbDireccion As Global.System.Web.UI.WebControls.TextBox
    Public WithEvents tbTelefono As Global.System.Web.UI.WebControls.TextBox
    Public WithEvents ddlActividad As Global.System.Web.UI.WebControls.DropDownList
    Public WithEvents tbFechaVigencia As Global.System.Web.UI.WebControls.TextBox

    Public WithEvents dvUnidadCumplimiento As Global.System.Web.UI.HtmlControls.HtmlGenericControl
    Public WithEvents tbSemejanza As Global.System.Web.UI.WebControls.HiddenField
    Public WithEvents tbUrlReporte As Global.System.Web.UI.WebControls.HiddenField
    Public WithEvents lbTipoCoincidencia As Global.System.Web.UI.WebControls.Label
    Public WithEvents cblTipoCoincidencia As Global.System.Web.UI.WebControls.CheckBoxList
    Public WithEvents divReportesCentinela As Global.System.Web.UI.HtmlControls.HtmlGenericControl
    Public WithEvents tbComplementarioSemejanza As Global.System.Web.UI.WebControls.HiddenField
    Public WithEvents tbComplementarioUrlReporte As Global.System.Web.UI.WebControls.HiddenField
    Public WithEvents divTipoCoincidencia As Global.System.Web.UI.HtmlControls.HtmlGenericControl

    Public WithEvents dvDesicion As Global.System.Web.UI.HtmlControls.HtmlGenericControl
    Public WithEvents lbDevolucion As Global.System.Web.UI.WebControls.Label
    Public WithEvents rbDesicion As Global.System.Web.UI.WebControls.RadioButtonList
    Public WithEvents btObservacion As Global.System.Web.UI.WebControls.Button
    Public WithEvents gvNotas As Global.System.Web.UI.WebControls.GridView
    Public WithEvents tbObservacion As Global.System.Web.UI.WebControls.TextBox
    Public WithEvents lblValidacionNota As Global.System.Web.UI.WebControls.Label
    Public WithEvents btGuardarNota As Global.System.Web.UI.WebControls.Button
    Public WithEvents btCerrarNota As Global.System.Web.UI.WebControls.Button
    Public WithEvents gvAdjuntos As Global.System.Web.UI.WebControls.GridView
    Public WithEvents gvCorrespondenciaRecibida As Global.System.Web.UI.WebControls.GridView
    Public WithEvents fuAdjunto As Global.System.Web.UI.WebControls.FileUpload
    Public WithEvents btGuardarAdjunto As Global.System.Web.UI.WebControls.Button

    Public WithEvents listActividades As Global.System.Web.UI.WebControls.Literal
    Public WithEvents hfComplementarioNew As Global.System.Web.UI.WebControls.HiddenField
    Public WithEvents ddlComplementarioTipoComplementario As Global.System.Web.UI.WebControls.DropDownList
    Public WithEvents ddlComplementarioTipoIdentificacion As Global.System.Web.UI.WebControls.DropDownList
    Public WithEvents tbComplementarioIdentificacion As Global.System.Web.UI.WebControls.TextBox
    Public WithEvents tbComplementarioNombre As Global.System.Web.UI.WebControls.TextBox
    Public WithEvents ddlComplementarioPais As Global.System.Web.UI.WebControls.DropDownList
    Public WithEvents ddlComplementarioCiudad As Global.System.Web.UI.WebControls.DropDownList
    Public WithEvents tbComplementarioCorreo As Global.System.Web.UI.WebControls.TextBox
    Public WithEvents tbComplementarioDireccion As Global.System.Web.UI.WebControls.TextBox
    Public WithEvents tbComplementarioTelefono As Global.System.Web.UI.WebControls.TextBox
    Public WithEvents ddlComplementarioActividad As Global.System.Web.UI.WebControls.DropDownList
    Public WithEvents chkComplementarioParticipacion As Global.System.Web.UI.WebControls.CheckBox
    Public WithEvents btComplementarioGuardar As Global.System.Web.UI.WebControls.Button
    Public WithEvents btComplementarioCerrar As Global.System.Web.UI.WebControls.Button

    Public WithEvents hfFilesToken As Global.System.Web.UI.WebControls.HiddenField
    Public WithEvents hfCtrlFecha As Global.System.Web.UI.WebControls.HiddenField
    Public WithEvents gvDocumentacion As Global.System.Web.UI.WebControls.GridView
    Public WithEvents countDocumentacion As Global.System.Web.UI.HtmlControls.HtmlGenericControl
    Public WithEvents fuDocumentacion As Global.System.Web.UI.WebControls.FileUpload
    Public WithEvents hfDocumentacionCodigo As Global.System.Web.UI.WebControls.HiddenField
    Public WithEvents hfDocumentacionCodigoComplementario As Global.System.Web.UI.WebControls.HiddenField
    Public WithEvents btDocumentacion As Global.System.Web.UI.WebControls.Button
    Public WithEvents btEliminarDocumento As Global.System.Web.UI.WebControls.Button
    Public WithEvents gvDocumentacionComplementario As Global.System.Web.UI.WebControls.GridView
    Public WithEvents countDocumentacionComplementario As Global.System.Web.UI.HtmlControls.HtmlGenericControl
    Public WithEvents btAgregarComplementario As Global.System.Web.UI.WebControls.Button
    Public WithEvents hfDocumentacionRegistro As Global.System.Web.UI.WebControls.HiddenField
    Public WithEvents hfDocumentacionRegistroComplementario As Global.System.Web.UI.WebControls.HiddenField

    Public WithEvents countComplementarios As Global.System.Web.UI.HtmlControls.HtmlGenericControl
    Public WithEvents gvComplementarios As Global.System.Web.UI.WebControls.GridView

    Public WithEvents hfScrollbarPosition As Global.System.Web.UI.WebControls.HiddenField
    Public WithEvents hfSelectedTab As Global.System.Web.UI.WebControls.HiddenField
    Public WithEvents hfExtensionesValidas As Global.System.Web.UI.WebControls.HiddenField
    Public WithEvents btLimpiar As Global.System.Web.UI.WebControls.Button
    Public WithEvents dvDivisa As Global.System.Web.UI.HtmlControls.HtmlGenericControl
    Public WithEvents ddlDivisa As Global.System.Web.UI.WebControls.DropDownList
    Public WithEvents tbValorDivisa As Global.System.Web.UI.WebControls.TextBox
    Public WithEvents tbFuncionarioRespCli As Global.System.Web.UI.WebControls.TextBox
    Public WithEvents tbFuncionarioRespSol As Global.System.Web.UI.WebControls.TextBox
    Public WithEvents tbFechaSolicitud As Global.System.Web.UI.WebControls.TextBox

    Public WithEvents ddlTipoDocumental As Global.System.Web.UI.WebControls.DropDownList
    Public WithEvents ddlTipoDocComplementario As Global.System.Web.UI.WebControls.DropDownList
    Public WithEvents btAgregarDocumento As Global.System.Web.UI.WebControls.Button
    Public WithEvents btAgregarDocComplementario As Global.System.Web.UI.WebControls.Button
    Public WithEvents ProyectFinance As Global.System.Web.UI.HtmlControls.HtmlGenericControl
    Public WithEvents rblClienteDefinido As Global.System.Web.UI.WebControls.RadioButtonList
    Public WithEvents tbUltimaObservacion As Global.System.Web.UI.WebControls.HiddenField
    Public WithEvents ddlEtapaDevolucion As Global.System.Web.UI.WebControls.DropDownList
    Public WithEvents lblEtapaDevolucion As Global.System.Web.UI.HtmlControls.HtmlGenericControl
    Public WithEvents lblUsuarioAnalisis As Global.System.Web.UI.HtmlControls.HtmlGenericControl
    Public WithEvents ddlUsuarioAnalisis As Global.System.Web.UI.WebControls.DropDownList
    Public WithEvents lblMontoRecomendado As Global.System.Web.UI.HtmlControls.HtmlGenericControl
    Public WithEvents tbMontoRecomendado As Global.System.Web.UI.WebControls.TextBox
    Public WithEvents panelDesicion As Global.System.Web.UI.HtmlControls.HtmlGenericControl
    Public WithEvents panelDevolucion As Global.System.Web.UI.HtmlControls.HtmlGenericControl

    Public Property UltData() As UltData
        Get
            Return Session("UltData")
        End Get
        Set(ByVal value As UltData)
            Session("UltData") = value
        End Set
    End Property

    Private Sub InitUltDataFromRequest()

        Dim data As New UltData()
        data.TaskID = Request.Cookies.Get("TaskID").Value
        data.SessionID = Request.Cookies.Get("SessionID").Value
        data.UserID = Request.Cookies.Get("UserID").Value

        Dim strError As String = String.Empty
        Dim cm As New ClientModel()
        If Not cm.GetTaskInfo(data, strError) Then
            LogWriter.WriteLog("RegistroSolicitud", "GetTaskInfo: " + strError)
        End If

        UltData = data

    End Sub

    Protected Sub btLimpiar_Click(sender As Object, e As EventArgs) Handles btLimpiar.Click

        lblEstado.Text = String.Empty
        ClienteExiste.Value = String.Empty

        ddlTipoIdentificacion.SelectedValue = Nothing
        tbIdentificacion.Text = Nothing
        tbNombre.Text = Nothing
        'ddlPais.SelectedValue = Nothing
        'ddlCiudad.SelectedValue = Nothing
        tbCorreo.Text = Nothing
        tbDireccion.Text = Nothing
        tbTelefono.Text = Nothing

        cblTipoCoincidencia.SelectedValue = Nothing
        tbSemejanza.Value = String.Empty
        tbUrlReporte.Value = String.Empty

        chkParticipacion.Checked = False
        divReportesCentinela.Visible = False

        'FormHelper.FixForm(Me)
        ScriptSM("setSelectedTab('tab-solicitud'); setScrollbarPosition(); ScriptInicial();")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If Not IsPostBack Then

                Session.Clear()
                InitUltDataFromRequest()

                tbFechaSolicitud.Text = Now.ToString("dd/MM/yyyy")
                tbFuncionarioRespCli.Text = UltData.UserID

                FormHelper.InitForm(Me, False)
                DocumentationHelper.InitDocumentation(Me, False, False, False, tbIdentificacion.Text, tbNombre.Text, chkParticipacion.Checked)
                ComplementariosHelper.InitComplementarios(Me, False)
                CorrespondenciaRecibidaHelper.ShowComunicacionRecibida(Me)

                If UltData.IncidentNo = 0 Then
                    If Not ddlPais.Items.FindByValue(AppSettings("PaisDefecto")) Is Nothing Then
                        ddlPais.SelectedValue = AppSettings("PaisDefecto")
                        ddlPais_SelectedIndexChanged(Nothing, Nothing)
                    End If
                End If

                If UltData.StepLabel = Etapas.CorrespondenciaRecibida.Name And UltData.TaskStatus = 1 Then
                    Dim strError As String = String.Empty
                    If Not FormHelper.ConsultCentinela(UltData.UserID, tbNombre.Text, tbIdentificacion.Text, tbSemejanza.Value, lbTipoCoincidencia.Text, tbUrlReporte.Value, strError) Then
                        ScriptSM("alert('" + strError + "')")
                    Else
                        imgPDF.Src = "../../Styles/images/pdf_icon1.png"
                    End If
                End If

            Else

                If UltData Is Nothing Then
                    ScriptSM("alert('La sesión ha expirado.'); window.close();")
                End If

            End If

        Catch ex As Exception
            LogWriter.WriteLog("RegistroSolicitud", ex)
        End Try
    End Sub

    Private Sub Script(ByVal strJS As String)
        Try
            strJS = "<script type='text/javascript'>$(document).ready(function(){" & strJS & "});</script>"
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Script", strJS)
        Catch ex As Exception
            LogWriter.WriteLog("RegistroSolicitud", ex)
        End Try
    End Sub

    Private Sub ScriptSM(ByVal strJS As String)
        ScriptManager.RegisterClientScriptBlock(Me, GetType(Page), UniqueID, "javascript:" & strJS, True)
    End Sub

    Protected Sub lbEnviar_Click(sender As Object, e As EventArgs) Handles lbEnviar.Click
        Try
            If rbDesicion.Items.Count > 0 And UltData.StepLabel <> Etapas.SolicitarCredito.Name And UltData.StepLabel <> Etapas.CorrespondenciaRecibida.Name And rbDesicion.SelectedIndex = -1 Then
                ScriptSM("alert('Por favor seleccione una opción.');")
                Return
            End If

            If UltData.StepLabel = Etapas.AsignacionDRC.Name AndAlso rbDesicion.SelectedValue = Estados.Continuar.Id AndAlso ddlUsuarioAnalisis.SelectedIndex <= 0 Then
                ScriptSM("alert('Por favor seleccione al usuario a quien asignar la solicitud.');")
                Return
            End If

            If tbMontoRecomendado.Visible AndAlso (rbDesicion.SelectedValue = Estados.Continuar.Id Or rbDesicion.SelectedValue = Estados.DevueltoMonto.Id) AndAlso (tbMontoRecomendado.Text = "" Or tbMontoRecomendado.Text = "0") Then
                ScriptSM("alert('Por favor ingrese el monto recomendado.');")
                Return
            End If

            Dim message As String = String.Empty
            SendHelper.SaveAndSend(Me, message)

            'Setear variables de activación de etapas // Segundo plano
            If UltData.StepLabel = Etapas.SolicitarCredito.Name Or UltData.StepLabel = Etapas.CorrespondenciaRecibida.Name Then
                Dim mThreadFic As Thread
                mThreadFic = New Threading.Thread(AddressOf SaveEnd)
                mThreadFic.IsBackground = True
                mThreadFic.Start()
                Thread.Sleep(10000)
            End If

            Dim js As String = String.Empty
            If Not String.IsNullOrEmpty(message) Then
                js &= "alert('" & message & "'); window.close();"
            End If
            ScriptSM(js)

        Catch ex As Exception
            LogWriter.WriteLog("RegistroSolicitud", ex)
            ScriptSM("alert('Ocurrió un error al enviar la tarea.');")
        End Try
    End Sub

    Private Sub SaveEnd()
        Dim cm As New ClientModel()
        Dim strError As String = String.Empty

        cm.GetIncident(UltData.ProcessName, UltData.IncidentNo, strError)
    End Sub

    Protected Sub tbIdentificacion_TextChanged(sender As Object, e As EventArgs) Handles tbIdentificacion.TextChanged
        Try
            If Not String.IsNullOrWhiteSpace(tbIdentificacion.Text) Then

                Dim bValido As Boolean = True
                If ddlTipoIdentificacion.SelectedValue = AppSettings("TipoNIT") Then
                    If tbIdentificacion.Text.Length <> 10 Or Not tbIdentificacion.Text.EndsWith(FormHelper.DigitoNIT(tbIdentificacion.Text.Substring(0, tbIdentificacion.Text.Length - 1))) Then
                        ScriptSM("alert('Ingrese la identificación con el digito de verificación valido.')")
                        tbIdentificacion.Text = String.Empty
                        bValido = False
                    End If
                End If

                If bValido Then
                    DocumentationHelper.InitDocumentation(Me, True, False, False, tbIdentificacion.Text, tbNombre.Text, chkParticipacion.Checked)
                    'Dim incidentData As New UltData
                    'If FormHelper.ConsultIsExistsIncident(False, Me, incidentData) Then
                    '    ScriptSM("alert('El cliente tiene una solicitud en proceso en la etapa " & incidentData.StepLabel & ", con número de incidente " & incidentData.IncidentNo & " y asignado al usuario " & incidentData.UserID & "')")
                    '    tbIdentificacion.Text = String.Empty
                    '    Return
                    'End If
                    Dim bActivo As Boolean
                    Dim strPais As String = String.Empty
                    Dim strCiudad As String = String.Empty
                    Dim strError As String = String.Empty
                    If Not FormHelper.ConsultT24(Me, tbIdentificacion.Text, False, bActivo, tbNombre.Text, strPais, strCiudad, tbCorreo.Text, strError) Then
                        ScriptSM("alert('" + strError + "')")
                    End If
                    divReportesCentinela.Visible = True
                End If

            End If
        Catch ex As Exception
            LogWriter.WriteLog("RegistroSolicitud", ex)
        End Try
    End Sub

    Protected Sub tbNombre_TextChanged(sender As Object, e As EventArgs) Handles tbNombre.TextChanged
        Try
            If String.IsNullOrWhiteSpace(tbIdentificacion.Text) = False And String.IsNullOrWhiteSpace(tbNombre.Text) = False Then
                Dim strError As String = String.Empty
                If Not FormHelper.ConsultCentinela(UltData.UserID, tbNombre.Text, tbIdentificacion.Text, tbSemejanza.Value, lbTipoCoincidencia.Text, tbUrlReporte.Value, strError) Then
                    ScriptSM("alert('" + strError + "')")
                Else
                    imgPDF.Src = "../../Styles/images/pdf_icon1.png"
                End If
            End If
        Catch ex As Exception
            LogWriter.WriteLog("RegistroSolicitud", ex)
        End Try
    End Sub

    Protected Sub ddlPais_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlPais.SelectedIndexChanged
        Try
            FormHelper.LoadDependentPaisCiudad(ddlCiudad, Catalog.Ciudad, ddlPais.SelectedValue)
        Catch ex As Exception
            LogWriter.WriteLog("RegistroSolicitud", ex)
        End Try
    End Sub

    Protected Sub ddlComplementarioPais_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlComplementarioPais.SelectedIndexChanged
        Try
            FormHelper.LoadDependentPaisCiudad(ddlComplementarioCiudad, Catalog.Ciudad, ddlComplementarioPais.SelectedValue)
            ScriptSM("$('#modal-complementario').modal('show');")
        Catch ex As Exception
            LogWriter.WriteLog("RegistroSolicitud", ex)
        End Try
    End Sub

    Protected Sub tbComplementarioIdentificacion_TextChanged(sender As Object, e As EventArgs) Handles tbComplementarioIdentificacion.TextChanged
        Try
            If Not String.IsNullOrWhiteSpace(tbComplementarioIdentificacion.Text) Then

                Dim bValido As Boolean = True
                If ddlComplementarioTipoIdentificacion.SelectedValue = AppSettings("TipoNIT") Then
                    If tbComplementarioIdentificacion.Text.Length <> 10 Or Not tbComplementarioIdentificacion.Text.EndsWith(FormHelper.DigitoNIT(tbComplementarioIdentificacion.Text.Substring(0, tbComplementarioIdentificacion.Text.Length - 1))) Then
                        ScriptSM("alert('Ingrese la identificación con el digito de verificación valido.')")
                        tbComplementarioIdentificacion.Text = String.Empty
                        bValido = False
                    End If
                End If

                If tbIdentificacion.Text.Trim = tbComplementarioIdentificacion.Text.Trim Then
                    ScriptSM("alert('El codeudor No puede ser igual al deudor.');$('#modal-complementario').modal('show');")
                    tbComplementarioIdentificacion.Text = String.Empty
                    bValido = False
                End If

                If bValido Then
                    DocumentationHelper.InitDocumentation(Me, True, True, False, tbComplementarioIdentificacion.Text, tbComplementarioNombre.Text, chkComplementarioParticipacion.Checked)
                    Dim bActivo As Boolean
                    Dim strPais As String = String.Empty
                    Dim strCiudad As String = String.Empty
                    Dim strError As String = String.Empty
                    'Dim incidentData As New UltData
                    'If FormHelper.ConsultIsExistsIncident(Avalista, Me, incidentData) Then
                    '    ScriptSM("alert('El cliente tiene una solicitud en proceso en la etapa " & incidentData.StepLabel & ", con número de incidente " & incidentData.IncidentNo & " y asignado al usuario " & incidentData.UserID & "');$('#modal-complementario').modal('show');")
                    '    tbComplementarioIdentificacion.Text = String.Empty
                    '    Return
                    'End If
                    If Not FormHelper.ConsultT24(Me, tbComplementarioIdentificacion.Text, True, bActivo, tbComplementarioNombre.Text, strPais, strCiudad, tbComplementarioCorreo.Text, strError) Then
                        ScriptSM("alert('" + strError + "');$('#modal-complementario').modal('show');")
                    Else
                        ScriptSM("$('#modal-complementario').modal('show');")
                    End If
                End If

            End If

        Catch ex As Exception
            LogWriter.WriteLog("RegistroSolicitud", ex)
            ScriptSM("$('#modal-complementario').modal('show');")
        End Try
    End Sub

    Protected Sub tbComplementarioNombre_TextChanged(sender As Object, e As EventArgs) Handles tbComplementarioNombre.TextChanged
        Try
            If String.IsNullOrWhiteSpace(tbComplementarioIdentificacion.Text) = False And String.IsNullOrWhiteSpace(tbComplementarioNombre.Text) = False Then
                Dim strError As String = String.Empty
                If Not FormHelper.ConsultCentinela(UltData.UserID, tbComplementarioNombre.Text, tbComplementarioIdentificacion.Text, tbComplementarioSemejanza.Value, Nothing, tbComplementarioUrlReporte.Value, strError) Then
                    ScriptSM("alert('" + strError + "');$('#modal-complementario').modal('show');")
                Else
                    ScriptSM("$('#modal-complementario').modal('show');")
                End If
            Else
                ScriptSM("$('#modal-complementario').modal('show');")
            End If
        Catch ex As Exception
            LogWriter.WriteLog("RegistroSolicitud", ex)
            ScriptSM(";$('#modal-complementario').modal('show');")
        End Try
    End Sub

    Protected Sub btComplementarioGuardar_Click(sender As Object, e As EventArgs) Handles btComplementarioGuardar.Click

        Try

            DocumentationHelper.CompleteDocuments(Me, True, tbComplementarioIdentificacion.Text)
            ComplementariosHelper.InsertComplementario(Me)
            ScriptSM("setSelectedTab('tab-complementarios'); setScrollbarPosition();")
            hfComplementarioNew.Value = String.Empty

        Catch ex As Exception
            LogWriter.WriteLog("RegistroSolicitud", ex)
        End Try
    End Sub

    Protected Sub btComplementarioCerrar_Click(sender As Object, e As EventArgs) Handles btComplementarioCerrar.Click

        Try

            DocumentationHelper.CompleteDocuments(Me, True, tbComplementarioIdentificacion.Text)

        Catch ex As Exception
            LogWriter.WriteLog("RegistroSolicitud", ex)
        End Try
    End Sub

    Protected Sub btGuardarNota_Click(sender As Object, e As EventArgs) Handles btGuardarNota.Click
        Try
            If rbDesicion.Items.Count > 0 And rbDesicion.SelectedIndex = -1 Then
                ScriptSM("alert('Por favor seleccione una opción para la casilla Decisión'); $('#modal-notas').modal('show');")
            Else
                Dim strSql As String
                Dim strError As String = String.Empty
                Dim FnBD As New HelperSQL("BDCreditoDirecto")
                Dim strEstado As String = String.Empty
                If rbDesicion.SelectedIndex <> -1 Then strEstado = rbDesicion.SelectedItem.Text
                strSql = "PA_AdminObservacion_V2 2," & UltData.IncidentNo & ",'" & UltData.StepLabel & "','" & UltData.UserID.Substring(UltData.UserID.IndexOf("/") + 1) & "','" & tbObservacion.Text & "','" & strEstado & "'"
                FnBD.EjecutaAccion(strSql, strError)
                tbUltimaObservacion.Value = tbObservacion.Text
                tbObservacion.Text = String.Empty
                FormHelper.ShowNotas(Me)
            End If
        Catch ex As Exception
            LogWriter.WriteLog("RegistroSolicitud", ex)
            ScriptSM("alert('Ocurrió un error al guardar la nota.');")
        Finally
            ScriptSM("limpiarNota();$('#modal-notas').modal('show');")
        End Try
    End Sub

    Protected Sub btGuardarAdjunto_Click(sender As Object, e As EventArgs) Handles btGuardarAdjunto.Click
        Try
            Dim strSql As String
            Dim strError As String = String.Empty
            Dim FnBD As New HelperSQL("BDCreditoDirecto")
            If fuAdjunto.HasFile Then
                fuAdjunto.SaveAs(AppSettings("ArchivosCredito") & UltData.IncidentNo & "-" & fuAdjunto.FileName)
                strSql = "PA_AdminAdjunto_V2 2," & UltData.IncidentNo & ",'" & UltData.UserID & "','" & fuAdjunto.FileName & "'"
                If FnBD.EjecutaAccion(strSql, strError) Then
                    FnBD.ConsultaGrilla(gvAdjuntos, "PA_AdminAdjunto_V2 1," & UltData.IncidentNo, strError)
                End If
            End If
        Catch ex As Exception
            LogWriter.WriteLog("RegistroSolicitud", ex)
            Script("alert('Ocurrió un error al guardar el adjunto.');")
        Finally
            Script("$('#modal-adjuntos').modal('show');")
        End Try
    End Sub

    Private Sub gvAdjuntos_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvAdjuntos.RowDataBound
        Try
            e.Row.Cells(1).Attributes.Add("style", "display:none")
            If e.Row.RowIndex <> -1 Then
                CType(e.Row.Cells(2).Controls(1), LinkButton).Text = e.Row.Cells(1).Text
            End If
        Catch ex As Exception
            LogWriter.WriteLog("RegistroSolicitud", ex)
        End Try
    End Sub

    Protected Sub gvAdjuntos_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvAdjuntos.RowCommand
        Try
            Dim strSql As String
            Dim strError As String = String.Empty
            Dim row As GridViewRow = gvAdjuntos.Rows(Convert.ToInt32(e.CommandArgument))
            Dim FnBD As New HelperSQL("BDCreditoDirecto")
            If e.CommandName = "Eliminar" Then
                strSql = "PA_AdminAdjunto_V2 3," & UltData.IncidentNo & ",null,'" & row.Cells(1).Text & "'"
                If FnBD.EjecutaAccion(strSql, strError) Then
                    IO.File.Delete(AppSettings("ArchivosCredito") & UltData.IncidentNo & "-" & row.Cells(1).Text)
                    FnBD.ConsultaGrilla(gvAdjuntos, "PA_AdminAdjunto_V2 1," & UltData.IncidentNo, strError)
                End If
                Script("$('#modal-adjuntos').modal('show');")
            ElseIf e.CommandName = "Descargar" Then
                Try
                    Response.ContentType = "application/octet-stream"
                    Response.AddHeader("Content-Disposition", "attachment;filename=" & row.Cells(1).Text)
                    Response.TransmitFile(AppSettings("ArchivosCredito") & UltData.IncidentNo & "-" & row.Cells(1).Text)
                    Response.End()
                Catch
                End Try
            End If
        Catch ex As Exception
            LogWriter.WriteLog("RegistroSolicitud", ex)
        End Try
    End Sub

    Protected Sub gvComplementarios_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvComplementarios.RowCommand
        Try
            If e.CommandName = "Eliminar" Then
                ComplementariosHelper.DeleteComplementario(Me, e.CommandArgument)
            ElseIf e.CommandName = "Ver" Then
                ComplementariosHelper.ShowComplementario(Me, e.CommandArgument)
                DocumentationHelper.InitDocumentation(Me, False, True, True, tbComplementarioIdentificacion.Text, tbComplementarioNombre.Text, chkComplementarioParticipacion.Checked)
                ScriptSM("$('#modal-complementario').modal('show');")
            End If
        Catch ex As Exception
            LogWriter.WriteLog("RegistroSolicitud", ex)
        End Try
    End Sub

    Protected Sub lbGuardar_Click(sender As Object, e As EventArgs) Handles lbGuardar.Click
        Try
            SendHelper.Save(Me)
        Catch ex As Exception
            LogWriter.WriteLog("RegistroSolicitud", ex)
        End Try
    End Sub

    Protected Sub btDocumentacion_Click(sender As Object, e As EventArgs) Handles btDocumentacion.Click
        Try
            If fuDocumentacion.HasFile Then

                ' Si tiene la fecha en el nombre del archivo
                Dim strFecha As String = String.Empty
                Dim js As String = String.Empty
                If fuDocumentacion.FileName.Length > 10 Then
                    strFecha = fuDocumentacion.FileName.Substring(0, 10)
                    If IsDate(strFecha) Then
                        strFecha = CDate(strFecha).ToString("dd/MM/yyyy")
                        js = "$('#" & hfCtrlFecha.Value & "').val('" & strFecha & "');"
                    Else
                        strFecha = String.Empty
                    End If
                End If

                If (hfDocumentacionCodigo.Value <> String.Empty) Then
                    DocumentationHelper.UploadDocument(Me, False, tbIdentificacion.Text, hfDocumentacionCodigo.Value, hfDocumentacionRegistro.Value)
                    Script(js)
                ElseIf (hfDocumentacionCodigoComplementario.Value <> String.Empty) Then
                    DocumentationHelper.UploadDocument(Me, True, tbComplementarioIdentificacion.Text, hfDocumentacionCodigoComplementario.Value, hfDocumentacionRegistroComplementario.Value)
                    ' For Triggers use Script function instead ScriptSM
                    Script("setSelectedTab('tab-complementarios'); $('#modal-complementario').modal('show'); setSelectedTab('tab-complementario-documentacion');" & js)
                End If

            End If
        Catch ex As Exception
            LogWriter.WriteLog("RegistroSolicitud", ex)
        End Try
    End Sub

    Protected Sub rblTipoPersona_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rblTipoPersona.SelectedIndexChanged
        Try
            DocumentationHelper.InitDocumentation(Me, True, False, False, tbIdentificacion.Text, tbNombre.Text, chkParticipacion.Checked)
        Catch ex As Exception
            LogWriter.WriteLog("RegistroSolicitud", ex)
        End Try
    End Sub

    Protected Sub ddlTipoProducto_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTipoProducto.SelectedIndexChanged
        Try
            DocumentationHelper.InitDocumentation(Me, True, False, False, tbIdentificacion.Text, tbNombre.Text, chkParticipacion.Checked)
            ProyectFinance.Visible = IIf(ddlTipoProducto.SelectedValue = AppSettings("ProductoProjectFinance"), True, False)
            ScriptSM("ValidarClienteDefinido();")
        Catch ex As Exception
            LogWriter.WriteLog("RegistroSolicitud", ex)
        End Try
    End Sub

    Private Sub gvNotas_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvNotas.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            If HttpUtility.HtmlDecode(e.Row.Cells(3).Text).Trim <> String.Empty Then
                Dim strCss As String = Estados.GetFromName(Server.HtmlDecode(e.Row.Cells(3).Text)).Css
                e.Row.Cells(3).Text = "&nbsp;<span class='label " & strCss & "'>&nbsp;</span>&nbsp;" & e.Row.Cells(3).Text
            End If
        End If
    End Sub

    Private Sub btAgregarComplementario_Click(sender As Object, e As EventArgs) Handles btAgregarComplementario.Click
        If tbIdentificacion.Text.Trim = String.Empty Then
            ScriptSM("alert('Debe ingresar la identificación del deudor');")
        Else
            hfComplementarioNew.Value = "1"
            ddlComplementarioTipoComplementario.SelectedValue = 0
            ddlComplementarioTipoIdentificacion.SelectedValue = 0
            tbComplementarioIdentificacion.Text = String.Empty
            tbComplementarioNombre.Text = String.Empty
            ddlComplementarioPais.SelectedValue = 0
            ddlComplementarioCiudad.SelectedValue = 0
            tbComplementarioCorreo.Text = String.Empty
            tbComplementarioDireccion.Text = String.Empty
            tbComplementarioTelefono.Text = String.Empty
            ddlComplementarioActividad.SelectedValue = 0
            gvDocumentacionComplementario.DataSource = Nothing
            gvDocumentacionComplementario.DataBind()
            countDocumentacionComplementario.InnerText = String.Empty
            lblComplementarioEstado.Text = String.Empty
            chkComplementarioParticipacion.Checked = False
            If Not ddlComplementarioPais.Items.FindByValue(AppSettings("PaisDefecto")) Is Nothing Then
                ddlComplementarioPais.SelectedValue = AppSettings("PaisDefecto")
                ddlComplementarioPais_SelectedIndexChanged(Nothing, Nothing)
            End If
            ScriptSM("setSelectedTab('tab-complementarios'); $('#modal-complementario').modal('show');")
        End If
    End Sub

    Private Sub btAgregarDocumento_Click(sender As Object, e As EventArgs) Handles btAgregarDocumento.Click
        If ddlTipoDocumental.SelectedIndex = 0 Then
            ScriptSM("alert('Seleccione un tipo documental');ShowDocumentacion();setSelectedTab('tab-documentacion');")
        Else
            DocumentationHelper.AddDocument(Me, False, tbIdentificacion.Text, ddlTipoDocumental.SelectedValue)
            ScriptSM("ShowDocumentacion();setSelectedTab('tab-documentacion');")
        End If
    End Sub

    Private Sub btAgregarDocComplementario_Click(sender As Object, e As EventArgs) Handles btAgregarDocComplementario.Click
        If ddlTipoDocComplementario.SelectedIndex = 0 Then
            ScriptSM("alert('Seleccione un tipo documental');ShowDocumentacion();setSelectedTab('tab-complementarios'); $('#modal-complementario').modal('show'); setSelectedTab('tab-complementario-documentacion');")
        Else
            DocumentationHelper.AddDocument(Me, True, tbComplementarioIdentificacion.Text, ddlTipoDocComplementario.SelectedValue)
            ScriptSM("ShowDocumentacion();setSelectedTab('tab-complementarios'); $('#modal-complementario').modal('show'); setSelectedTab('tab-complementario-documentacion');")
        End If
    End Sub

    Private Sub btEliminarDocumento_Click(sender As Object, e As EventArgs) Handles btEliminarDocumento.Click
        If (hfDocumentacionCodigo.Value <> String.Empty) Then
            DocumentationHelper.RemoveDocument(Me, False, tbIdentificacion.Text, hfDocumentacionCodigo.Value, hfDocumentacionRegistro.Value)
            ScriptSM("ShowDocumentacion();setSelectedTab('tab-documentacion');")
        ElseIf (hfDocumentacionCodigoComplementario.Value <> String.Empty) Then
            DocumentationHelper.RemoveDocument(Me, True, tbComplementarioIdentificacion.Text, hfDocumentacionCodigoComplementario.Value, hfDocumentacionRegistroComplementario.Value)
            ScriptSM("ShowDocumentacion();setSelectedTab('tab-complementarios'); $('#modal-complementario').modal('show'); setSelectedTab('tab-complementario-documentacion');")
        End If
    End Sub

    Private Sub gvComplementarios_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvComplementarios.RowDataBound
        If SoloLectura.Value = "1" Then
            e.Row.Cells(6).Visible = False
        End If
    End Sub

    Private Sub gvDocumentacion_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvDocumentacion.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            If SoloLectura.Value = "1" And e.Row.Cells(1).Text = "True" Then
                CType(e.Row.FindControl("txtFechaEmision"), HtmlInputText).Disabled = True
                CType(e.Row.FindControl("txtObservaciones"), HtmlTextArea).Disabled = True
                CType(e.Row.FindControl("btAdjuntar"), HtmlInputButton).Visible = False
            End If
            If e.Row.Cells(2).Text = "False" Then
                CType(e.Row.FindControl("btEliminar"), HtmlInputButton).Visible = False
            End If
            If e.Row.Cells(2).Text = "True" Then ' Validar
                CType(e.Row.FindControl("txtFechaEmision"), HtmlInputText).Disabled = False
                CType(e.Row.FindControl("txtObservaciones"), HtmlTextArea).Disabled = False
            End If
        End If
    End Sub

    Private Sub gvDocumentacionComplementario_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvDocumentacionComplementario.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            If SoloLectura.Value = "1" And e.Row.Cells(1).Text = "True" Then
                CType(e.Row.FindControl("txtFechaEmision"), HtmlInputText).Disabled = True
                CType(e.Row.FindControl("txtObservaciones"), HtmlTextArea).Disabled = True
                CType(e.Row.FindControl("btAdjuntar"), HtmlInputButton).Visible = False
            End If
            If e.Row.Cells(2).Text = "False" Then
                CType(e.Row.FindControl("btEliminar"), HtmlInputButton).Visible = False
            End If
            If e.Row.Cells(2).Text = "True" Then ' Validar
                CType(e.Row.FindControl("txtFechaEmision"), HtmlInputText).Disabled = False
                CType(e.Row.FindControl("txtObservaciones"), HtmlTextArea).Disabled = False
            End If
        End If
    End Sub

    Private Sub chkParticipacion_CheckedChanged(sender As Object, e As EventArgs) Handles chkParticipacion.CheckedChanged
        DocumentationHelper.InitDocumentation(Me, True, False, False, tbIdentificacion.Text, tbNombre.Text, chkParticipacion.Checked)
    End Sub

    Private Sub chkComplementarioParticipacion_CheckedChanged(sender As Object, e As EventArgs) Handles chkComplementarioParticipacion.CheckedChanged
        DocumentationHelper.InitDocumentation(Me, True, True, False, tbComplementarioIdentificacion.Text, tbComplementarioNombre.Text, chkComplementarioParticipacion.Checked)
        ScriptSM("$('#modal-complementario').modal('show');")
    End Sub


    <WebMethod()> Public Shared Function DevolverCorrespondencia(ByVal ComunicacionRecibida As Long, ByVal User As String, ByVal Comentarios As String) As String
        Try
            Dim service = New ServicioIntegracionClient()
            Dim result As Boolean = service.DevolverCorrespondencia(ComunicacionRecibida, User, Comentarios)
            Return IIf(result, "", "Falló la devolución de la correspondencia")
        Catch ex As Exception
            Return ex.Message
        End Try
    End Function

    <WebMethod()> Public Shared Function BuscarIncidente(ByVal Incidente As Integer) As IncidenteComunicacionRecibida
        Try
            Return DBMethods.GetIncidenteComunicacionRecibida(Incidente)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    <WebMethod()> Public Shared Function AsociarCorrespondencia(ByVal ComunicacionRecibida As Long, ByVal Incidente As Integer) As String
        Try
            Dim service = New ServicioIntegracionClient()
            Dim result As Boolean = service.AsociarCorrespondencia(ComunicacionRecibida, "Credito Directo Empresarial", Incidente)
            Return IIf(result, "", "Falló la asociación de la correspondencia")
        Catch ex As Exception
            Return ex.Message
        End Try
    End Function

End Class