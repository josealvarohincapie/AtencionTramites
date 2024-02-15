Imports Web
Imports UltimusEIK
Imports System.Xml
Imports LogWriterHelper
Imports System.Data
Imports Datos
Imports System.Web.Script.Serialization
Imports System.Configuration.ConfigurationManager

Public Class SendHelper

    Public Shared Sub SaveAndSend(ByRef form As RegistroSolicitud, ByRef message As String)

        Dim FnUlt As New CSWS()
        Dim strError As String = String.Empty

        SetSchemeSolicitud(form)

        SetSummary(form)

        Dim bNewInc As Boolean = IIf(form.UltData.IncidentNo = 0, True, False)

        If FnUlt.SendTask(form.UltData, strError, 9) Then
            form.UltData.TaskStatus = FormHelper._TaskStatusCompleted
            form.lbIncidente.Text = form.UltData.IncidentNo
            form.tbObservacion.ReadOnly = False
            form.btGuardarNota.Enabled = True
            form.btCerrarNota.Enabled = True
            form.btGuardarAdjunto.Enabled = True
            form.fuAdjunto.Enabled = True
            Save(form)
        Else
            LogWriter.WriteLog("SendHelper", strError)
            message = "Ocurrió un error al completar la tarea"
            Return
        End If

        message = IIf(form.UltData.StepLabel = Etapas.SolicitarCredito.Name And bNewInc, "Se ha creado la solicitud número " & form.UltData.IncidentNo & ".", "La tarea fue completada exitosamente.")

    End Sub

    Public Shared Sub Save(ByRef form As RegistroSolicitud)
        FormHelper.SaveForm(form)
        DocumentationHelper.CompleteDocuments(form, False, form.tbIdentificacion.Text)
        DocumentationHelper.SaveDocumentation(form, False, True, form.tbIdentificacion.Text)
        ComplementariosHelper.SaveComplementarios(form)
        DocumentationHelper.CreateFileONBASE(form)
    End Sub

    Private Shared Sub SetSummary(ByRef form As RegistroSolicitud)
        If form.tbIdentificacion.Text <> String.Empty And form.tbNombre.Text <> String.Empty Then
            form.UltData.IncidentSummary = form.tbIdentificacion.Text & " - " & form.tbNombre.Text
        End If
    End Sub

    Private Shared Sub SetSchemeSolicitud(ByRef form As RegistroSolicitud)

        Dim FnUlt As New CSWS()
        Dim strError As String = String.Empty
        Dim StrActivarAnalizarOCU As Boolean = False
        Dim StrActivarAnalizarDJU As Boolean = False
        Dim StrActivarAnalizarDRF As Boolean = False
        Dim StrActivarAnalizarAmbienteSocial As Boolean = False
        Dim StrActivarPreviabilidadDRF As Boolean = False
        Dim strUsers As String = String.Empty
        Dim strXMLIncident As String = String.Empty
        Dim FnBPM As New UltimusEIK.ClientModel
        FnBPM.GetUsersIncident(AppSettings("UltimusProceso"), form.UltData.IncidentNo, strUsers, strError)

        If (FnBPM.GetIncidentData(AppSettings("UltimusProceso"), form.UltData.IncidentNo, strXMLIncident, strError)) Then
            form.UltData.DataXML_Incident = strXMLIncident
        End If


        Dim s As Solicitud = FormHelper.GetFormData(form)

        form.UltData.setSchemeIntegerValue(Esquema.EstadoSolicitud, s.EstadoSolicitud)
        If form.rbDesicion.SelectedIndex <> -1 Then
            form.UltData.setSchemeStringValue(Esquema.EstadoSolicitudDescripcion, form.rbDesicion.SelectedItem.Text)
        End If
        If form.UltData.StepLabel <> Etapas.SolicitarCredito.Name And form.UltData.StepLabel <> Etapas.EmitirConceptoVRI.Name Then
            form.UltData.setSchemeStringValue(Esquema.UltimaEtapa, form.UltData.StepLabel)
        End If
        form.UltData.setSchemeStringValue(Esquema.UltimoComentario, form.tbUltimaObservacion.Value)

        If strUsers <> String.Empty Then
            strUsers = strUsers.Replace(";;", ";")
        End If

        form.UltData.setSchemeStringValue(Esquema.DestinatariosNotificacion, strUsers)
        form.UltData.setSchemeStringValue(Esquema.TipoProducto, s.TipoProducto)
        form.UltData.setSchemeStringValue(Esquema.TipoProductoDescripcion, form.ddlTipoProducto.SelectedItem.Text)
        form.UltData.setSchemeStringValue(Esquema.IdentificacionCliente, s.Identificacion)
        form.UltData.setSchemeStringValue(Esquema.NombreCliente, s.Nombre)
        If form.ddlEtapaDevolucion.SelectedIndex <> -1 Then
            form.UltData.setSchemeStringValue(Esquema.EtapaDevolucion, form.ddlEtapaDevolucion.SelectedValue)
        End If
        form.UltData.setSchemeIntegerValue(Esquema.DiasAplazado, AppSettings("DiasVencimientoAplazado"))

        'Se consultan los valores de las variables
        StrActivarAnalizarOCU = form.UltData.getSchemeIncidentValueAsBoolean(Esquema.ActivarAnalizarOCU)
        StrActivarAnalizarDJU = form.UltData.getSchemeIncidentValueAsBoolean(Esquema.ActivarAnalizarDJU)
        StrActivarAnalizarDRF = form.UltData.getSchemeIncidentValueAsBoolean(Esquema.ActivarAnalizarDRF)
        StrActivarAnalizarAmbienteSocial = form.UltData.getSchemeIncidentValueAsBoolean(Esquema.ActivarAnalizarAmbienteSocial)
        StrActivarPreviabilidadDRF = form.UltData.getSchemeIncidentValueAsBoolean(Esquema.ActivarPreviabilidadDRF)

        'Se mantienen los valores de las variables en la devolución de las etapas:
        form.UltData.setSchemeBooleanValue(Esquema.ActivarAnalizarDJU, StrActivarAnalizarDJU)
        form.UltData.setSchemeBooleanValue(Esquema.ActivarAnalizarOCU, StrActivarAnalizarOCU)
        form.UltData.setSchemeBooleanValue(Esquema.ActivarAnalizarDRF, StrActivarAnalizarDRF)
        form.UltData.setSchemeBooleanValue(Esquema.ActivarAnalizarAmbienteSocial, StrActivarAnalizarAmbienteSocial)
        form.UltData.setSchemeBooleanValue(Esquema.ActivarPreviabilidadDRF, StrActivarPreviabilidadDRF)

        ''Se mantienen los valores de las variables en la devolución de las etapas diferentes a:
        ''AnalizarOCU, AnalizarDJU, AnalizarDRF, ActivarAnalizarAmbienteSocial y ActivarPreviabilidadDRF
        'If form.UltData.StepLabel <> Etapas.AnalizarRiesgos.Name And form.UltData.StepLabel <> Etapas.AnalizarCapacidad.Name And form.UltData.StepLabel <> Etapas.AnalizarDRF.Name And form.UltData.StepLabel <> Etapas.AnalizarAmbientalSocial.Name And form.UltData.StepLabel <> Etapas.PreviabilidadDRF.Name Then
        '    form.UltData.setSchemeBooleanValue(Esquema.ActivarAnalizarDJU, StrActivarAnalizarDJU)
        '    form.UltData.setSchemeBooleanValue(Esquema.ActivarAnalizarOCU, StrActivarAnalizarOCU)
        '    form.UltData.setSchemeBooleanValue(Esquema.ActivarAnalizarDRF, StrActivarAnalizarDRF)
        '    form.UltData.setSchemeBooleanValue(Esquema.ActivarAnalizarAmbienteSocial, StrActivarAnalizarAmbienteSocial)
        '    form.UltData.setSchemeBooleanValue(Esquema.ActivarPreviabilidadDRF, StrActivarPreviabilidadDRF)
        'End If

        ''Se mantienen los valores de las variables // AnalizarOCU se setea en true
        'If form.UltData.StepLabel = Etapas.AnalizarRiesgos.Name Then
        '    form.UltData.setSchemeBooleanValue(Esquema.ActivarAnalizarDJU, StrActivarAnalizarDJU)
        '    form.UltData.setSchemeBooleanValue(Esquema.ActivarAnalizarOCU, 1)
        '    form.UltData.setSchemeBooleanValue(Esquema.ActivarAnalizarDRF, StrActivarAnalizarDRF)
        '    form.UltData.setSchemeBooleanValue(Esquema.ActivarAnalizarAmbienteSocial, StrActivarAnalizarAmbienteSocial)
        '    form.UltData.setSchemeBooleanValue(Esquema.ActivarPreviabilidadDRF, StrActivarPreviabilidadDRF)
        'End If
        ''Se mantienen los valores de las variables // AnalizarDJU se setea en true
        'If form.UltData.StepLabel = Etapas.AnalizarCapacidad.Name Then
        '    form.UltData.setSchemeBooleanValue(Esquema.ActivarAnalizarDJU, 1)
        '    form.UltData.setSchemeBooleanValue(Esquema.ActivarAnalizarOCU, StrActivarAnalizarOCU)
        '    form.UltData.setSchemeBooleanValue(Esquema.ActivarAnalizarDRF, StrActivarAnalizarDRF)
        '    form.UltData.setSchemeBooleanValue(Esquema.ActivarAnalizarAmbienteSocial, StrActivarAnalizarAmbienteSocial)
        '    form.UltData.setSchemeBooleanValue(Esquema.ActivarPreviabilidadDRF, StrActivarPreviabilidadDRF)
        'End If
        ''Se mantienen los valores de las variables // AnalizarDRF se setea en true
        'If form.UltData.StepLabel = Etapas.AnalizarDRF.Name Then
        '    form.UltData.setSchemeBooleanValue(Esquema.ActivarAnalizarDJU, StrActivarAnalizarDJU)
        '    form.UltData.setSchemeBooleanValue(Esquema.ActivarAnalizarOCU, StrActivarAnalizarOCU)
        '    form.UltData.setSchemeBooleanValue(Esquema.ActivarAnalizarDRF, 1)
        '    form.UltData.setSchemeBooleanValue(Esquema.ActivarAnalizarAmbienteSocial, StrActivarAnalizarAmbienteSocial)
        '    form.UltData.setSchemeBooleanValue(Esquema.ActivarPreviabilidadDRF, StrActivarPreviabilidadDRF)
        'End If
        ''Se mantienen los valores de las variables // ActivarAnalizarAmbienteSocial se setea en true
        'If form.UltData.StepLabel = Etapas.AnalizarAmbientalSocial.Name Then
        '    form.UltData.setSchemeBooleanValue(Esquema.ActivarAnalizarDJU, StrActivarAnalizarDJU)
        '    form.UltData.setSchemeBooleanValue(Esquema.ActivarAnalizarOCU, StrActivarAnalizarOCU)
        '    form.UltData.setSchemeBooleanValue(Esquema.ActivarAnalizarDRF, StrActivarAnalizarDRF)
        '    form.UltData.setSchemeBooleanValue(Esquema.ActivarAnalizarAmbienteSocial, 1)
        '    form.UltData.setSchemeBooleanValue(Esquema.ActivarPreviabilidadDRF, StrActivarPreviabilidadDRF)
        'End If
        ''Se mantienen los valores de las variables // ActivarPreviabilidadDRF se setea en true
        'If form.UltData.StepLabel = Etapas.PreviabilidadDRF.Name Then
        '    form.UltData.setSchemeBooleanValue(Esquema.ActivarAnalizarDJU, StrActivarAnalizarDJU)
        '    form.UltData.setSchemeBooleanValue(Esquema.ActivarAnalizarOCU, StrActivarAnalizarOCU)
        '    form.UltData.setSchemeBooleanValue(Esquema.ActivarAnalizarDRF, StrActivarAnalizarDRF)
        '    form.UltData.setSchemeBooleanValue(Esquema.ActivarAnalizarAmbienteSocial, StrActivarAnalizarAmbienteSocial)
        '    form.UltData.setSchemeBooleanValue(Esquema.ActivarPreviabilidadDRF, 1)
        'End If

    End Sub

End Class