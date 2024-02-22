Imports System.Xml
Imports System.Xml.Serialization
Imports System.IO
Imports UltimusEIK.ClientServicesWS
Imports UltimusEIK.Tramite_Asesorias
Imports UltimusEIK.Tramite_Quejas
Imports UltimusEIK.Tramite_Solicitudes
Imports AtencionTramites.Servicios.Utilidades.Excepciones.AtencionTramites.Servicios.Utilidades.Excepciones
Imports AtencionTramites.Enumeracion.AtencionTramites.Enumeracion
Imports AtencionTramites.Servicios.Utilidades.Mensajes.AtencionTramites.Servicios.Utilidades.Mensajes
Imports Modelo.AtencionTramites.Modelos
Imports Modelo.AtencionTramites.UltimusSOAP.Modelo

Namespace AtencionTramites.UltimusEIK.UltimusSOAP

    ''' <summary>
    ''' Clase que representa la integración con Ultimus BPM
    ''' </summary>
    ''' <CreatedBy>EALVAREZT</CreatedBy>
    ''' <CreationDate>18/02/2024</CreationDate>
    ''' <ModifiedBy></ModifiedBy>
    ''' <ModificationDate></ModificationDate>
    Partial Public Class UltimusOperacion

#Region "Variables"

        ''' <summary>
        ''' Variables que almacenan la conexión cliente con el servicio
        ''' </summary>
        ''' <CreatedBy>EALVAREZT</CreatedBy>
        ''' <CreationDate>18/02/2024</CreationDate>
        ''' <ModifiedBy></ModifiedBy>
        ''' <ModificationDate></ModificationDate>
        Private clienteAsesorias As AtencionTramiteAseSoapClient
        Private clienteQuejas As AtencionTramiteQueSoapClient
        Private clienteSolicitudes As AtencionTramiteSolSoapClient

#End Region

#Region "Constantes"

        Private Const incident As String = "Incident"
        Private Const taskID As String = "tTaskID"
        Private Const IncidentNo As String = "iIncidentNo"
        Private Const AssignedToUser As String = "tAssignedToUser"
        Private Const StepLabel As String = "tStepLabel"
        Private Const StartTime As String = "tStartTime"
        Private Const UrgentDueTime As String = "tUrgentDueTime"
        Private Const OverDueTime As String = "tOverDueTime"

#End Region

#Region "Dispose"

        ''' <summary>
        ''' Método público que permite liberar recursos no administrados de la clase .
        ''' </summary>
        ''' <CreatedBy>EALVAREZT</CreatedBy>
        ''' <CreationDate>18/02/2024</CreationDate>
        ''' <ModifiedBy></ModifiedBy>
        ''' <ModificationDate></ModificationDate>
        Public Sub Dispose()
            GC.SuppressFinalize(Me)
        End Sub

#End Region

#Region "Métodos"

        ''' <summary>
        ''' Crear el cliente de la conexión con el servicio de ultimus
        ''' </summary>
        ''' <CreatedBy>EALVAREZT</CreatedBy>
        ''' <CreationDate>22/02/2024</CreationDate>
        ''' <ModifiedBy></ModifiedBy>
        ''' <ModificationDate></ModificationDate>
        Private Sub CrearRequestAsesoria()
            Try
                clienteAsesorias = New AtencionTramiteAseSoapClient()
            Catch ex As Exception
                Throw New ExcepcionOperacion(HelperMensaje.Obtener(HelperCodigoMensaje.BPM_0001), CInt(CodigosHTTP.[Error]), ex.InnerException)
            End Try
        End Sub

        ''' <summary>
        ''' Crear el cliente de la conexión con el servicio de ultimus
        ''' </summary>
        ''' <CreatedBy>EALVAREZT</CreatedBy>
        ''' <CreationDate>21/02/2024</CreationDate>
        ''' <ModifiedBy></ModifiedBy>
        ''' <ModificationDate></ModificationDate>
        Private Sub CrearRequestSolicitud()
            Try
                clienteSolicitudes = New AtencionTramiteSolSoapClient()
            Catch ex As Exception
                Throw New ExcepcionOperacion(HelperMensaje.Obtener(HelperCodigoMensaje.BPM_0001), CInt(CodigosHTTP.[Error]), ex.InnerException)
            End Try
        End Sub

        ''' <summary>
        ''' Crear el cliente de la conexión con el servicio de ultimus
        ''' </summary>
        ''' <CreatedBy>EALVAREZT</CreatedBy>
        ''' <CreationDate>21/02/2024</CreationDate>
        ''' <ModifiedBy></ModifiedBy>
        ''' <ModificationDate></ModificationDate>
        Private Sub CrearRequestQueja()
            Try
                clienteQuejas = New AtencionTramiteQueSoapClient()
            Catch ex As Exception
                Throw New ExcepcionOperacion(HelperMensaje.Obtener(HelperCodigoMensaje.BPM_0001), CInt(CodigosHTTP.[Error]), ex.InnerException)
            End Try
        End Sub

        ''' <summary>
        ''' Iniciar un incidente sobre el BPMS, de acuerdo a las validaciones realiza incidente para Asesorías, Quejas o Solicitudes
        ''' </summary>
        ''' <param name="dtoUltimus">BpmDTO--Objeto con los datos solicitados por ultimus</param>
        ''' <param name="dtoDesembolsoUltimus">BpmDesembolsoDTO--Objeto con los datos solicitados por ultimus para desembolso</param>
        ''' <returns>Numero del incidente</returns>
        ''' <CreatedBy>EALVAREZT</CreatedBy>
        ''' <CreationDate>22/04/2017</CreationDate>
        ''' <ModifiedBy></ModifiedBy>
        ''' <ModificationDate></ModificationDate>

        Public Function IniciarIncidente(ByVal BpmAsesoriaDTO As BpmDTO, ByVal BpmSolicitudDTO As BpmDTO, ByVal BpmQuejaDTO As BpmDTO) As Long
            Dim numeroIncidente = 0
            Dim strXml = ""
            Dim strError = ""
            Dim inValueAse As Tramite_Asesorias.LaunchIncidentRequest = Nothing
            Dim inValueSol As Tramite_Solicitudes.LaunchIncidentRequest = Nothing
            Dim inValueQue As Tramite_Quejas.LaunchIncidentRequest = Nothing

            Try
                If inValueAse IsNot Nothing Then
                    'Valida si es proceso de seguro desempleo
                    If BpmAsesoriaDTO.NombreProceso.Equals("AtencionTramiteAse") Then ' Asesorías
                        CrearRequestAsesoria()

                        inValueAse = New Tramite_Asesorias.LaunchIncidentRequest()
                        inValueAse.Body = New Tramite_Asesorias.LaunchIncidentRequestBody()
                        inValueAse.Body.strUserName = String.Concat(BpmAsesoriaDTO.DominioRed, "/", BpmAsesoriaDTO.UsuarioBPM)
                        inValueAse.Body.strSummary = BpmAsesoriaDTO.NumeroRadicacionPostulacion.ToString()
                        inValueAse.Body.strMemo = String.Empty
                        inValueAse.Body.bDisableAbort = False
                        inValueAse.Body.nPriority = 9
                        inValueAse.Body.strXML = String.Empty
                        inValueAse.Body.bValidateXML = False
                        Dim esquemas As Tramite_Asesorias.SchemaFile() = Nothing

                        If Not clienteAsesorias.GetLaunchInformation(BpmAsesoriaDTO.DominioRed.ToString() & "/" + BpmAsesoriaDTO.UsuarioBPM, esquemas, strXml, strError) Then Throw New ExcepcionOperacion(HelperMensaje.Obtener(HelperCodigoMensaje.BPM_0002), CInt(CodigosHTTP.[Error]), Nothing)

                        inValueAse.Body.strXML = strXml
                        strError = String.Empty

                        If Not clienteAsesorias.LaunchIncident(BpmAsesoriaDTO.DominioRed.ToString() & "/" + BpmAsesoriaDTO.UsuarioBPM, BpmAsesoriaDTO.NumeroRadicacionPostulacion.ToString(), String.Empty, False, 9, inValueAse.Body.strXML, False, numeroIncidente, strError) Then
                            Throw New ExcepcionOperacion(String.Concat(HelperMensaje.Obtener(HelperCodigoMensaje.BPM_0003), strError), CInt(CodigosHTTP.[Error]), Nothing)
                        End If
                    ElseIf BpmSolicitudDTO.NombreProceso.Equals("AtencionTramiteSol") Then ' Solicitudes
                        CrearRequestSolicitud()
#If DEBUG Then
                        BpmSolicitudDTO.UsuarioBPM = "SVCULTIMUSPRUE"
#End If
                        inValueSol = New Tramite_Solicitudes.LaunchIncidentRequest()
                        inValueSol.Body = New Tramite_Solicitudes.LaunchIncidentRequestBody()
                        inValueSol.Body.strUserName = String.Concat(BpmSolicitudDTO.DominioRed, "/", BpmSolicitudDTO.UsuarioBPM)
                        inValueSol.Body.strSummary = BpmSolicitudDTO.NumeroRadicacionPostulacion.ToString()
                        inValueSol.Body.strMemo = String.Empty
                        inValueSol.Body.bDisableAbort = False
                        inValueSol.Body.nPriority = 9
                        inValueSol.Body.strXML = String.Empty
                        inValueSol.Body.bValidateXML = False
                        Dim esquemaDesembolso As Tramite_Solicitudes.SchemaFile() = Nothing

                        If Not clienteSolicitudes.GetLaunchInformation(BpmSolicitudDTO.DominioRed.ToString() & "/" + BpmSolicitudDTO.UsuarioBPM, esquemaDesembolso, strXml, strError) Then Throw New ExcepcionOperacion(HelperMensaje.Obtener(HelperCodigoMensaje.BPM_0002), CInt(CodigosHTTP.[Error]), Nothing)

                        BpmSolicitudDTO.strTaskXml = strXml
                        'Me.CompletarDatosDesembolso(BpmSolicitudDTO, BpmSolicitudDTO)
                        inValueSol.Body.strXML = BpmSolicitudDTO.strTaskXml

                        strError = String.Empty

                        If Not clienteSolicitudes.LaunchIncident(BpmSolicitudDTO.DominioRed.ToString() & "/" + BpmSolicitudDTO.UsuarioBPM, BpmSolicitudDTO.NumeroRadicacionPostulacion.ToString(), String.Empty, False, 9, BpmSolicitudDTO.strTaskXml, False, numeroIncidente, strError) Then
                            Throw New ExcepcionOperacion(String.Concat(HelperMensaje.Obtener(HelperCodigoMensaje.BPM_0003), strError), CInt(CodigosHTTP.[Error]), Nothing)
                        End If
                    ElseIf BpmQuejaDTO.NombreProceso.Equals("AtencionTramiteQue") Then 'Quejas
                        CrearRequestQueja()
#If DEBUG Then
                        BpmQuejaDTO.UsuarioBPM = "SVCULTIMUSPRUE"
#End If
                        inValueQue = New Tramite_Quejas.LaunchIncidentRequest()
                        inValueQue.Body = New Tramite_Quejas.LaunchIncidentRequestBody()
                        inValueQue.Body.strUserName = String.Concat(BpmQuejaDTO.DominioRed, "/", BpmQuejaDTO.UsuarioBPM)
                        inValueQue.Body.strSummary = BpmQuejaDTO.NumeroRadicacionPostulacion.ToString()
                        inValueQue.Body.strMemo = String.Empty
                        inValueQue.Body.bDisableAbort = False
                        inValueQue.Body.nPriority = 9
                        inValueQue.Body.strXML = String.Empty
                        inValueQue.Body.bValidateXML = False
                        Dim esquemaDesembolso As Tramite_Quejas.SchemaFile() = Nothing

                        If Not clienteQuejas.GetLaunchInformation(BpmQuejaDTO.DominioRed.ToString() & "/" + BpmQuejaDTO.UsuarioBPM, esquemaDesembolso, strXml, strError) Then Throw New ExcepcionOperacion(HelperMensaje.Obtener(HelperCodigoMensaje.BPM_0002), CInt(CodigosHTTP.[Error]), Nothing)

                        BpmQuejaDTO.strTaskXml = strXml
                        'Me.CompletarDatosDesembolso(BpmQuejaDTO, BpmQuejaDTO)
                        inValueQue.Body.strXML = BpmQuejaDTO.strTaskXml

                        strError = String.Empty

                        If Not clienteQuejas.LaunchIncident(BpmQuejaDTO.DominioRed.ToString() & "/" + BpmQuejaDTO.UsuarioBPM, BpmQuejaDTO.NumeroRadicacionPostulacion.ToString(), String.Empty, False, 9, BpmQuejaDTO.strTaskXml, False, numeroIncidente, strError) Then
                            Throw New ExcepcionOperacion(String.Concat(HelperMensaje.Obtener(HelperCodigoMensaje.BPM_0003), strError), CInt(CodigosHTTP.[Error]), Nothing)
                        End If
                    End If
                End If
                Return numeroIncidente
            Catch exo As ExcepcionOperacion
                'Convert to xml
                Dim request = UltimusOperacion.SetRequestXml(inValueAse, inValueSol, inValueQue)
                Throw New ExcepcionOperacion(String.Concat(exo.Message, "Request:  ", request), CInt(CodigosHTTP.[Error]), Nothing)
            Catch ex As Exception
                'Convert to xml
                Dim request = UltimusOperacion.SetRequestXml(inValueAse, inValueSol, inValueQue)

                If ex.InnerException.Message.Contains("Error404") Then
                    Throw New ExcepcionOperacion(String.Concat(HelperMensaje.Obtener(HelperCodigoMensaje.BPM_0001), "Request: ", request), CInt(CodigosHTTP.[Error]), ex.InnerException)
                Else
                    Throw New ExcepcionOperacion(String.Concat(HelperMensaje.Obtener(HelperCodigoMensaje.BPM_0006), "Request: ", request), CInt(CodigosHTTP.[Error]), ex.InnerException)
                End If
            End Try
        End Function

        ''' <summary>
        ''' realiza solicitud xml
        ''' </summary>
        ''' <paramname="inValueAse">Tramite_Asesorias.LaunchIncidentRequest</param>
        ''' <paramname="inValueSol"> Tramite_Solicitudes.LaunchIncidentRequest</param>
        ''' <paramname="inValueQue"> Tramite_Quejas.LaunchIncidentRequest</param>
        ''' <returns>string</returns>
        ''' <CreatedBy>EALVAREZT</CreatedBy>
        ''' <CreationDate>22/02/2024</CreationDate>
        ''' <ModifiedBy></ModifiedBy>
        ''' <ModificationDate></ModificationDate>
        Private Shared Function SetRequestXml(inValueAse As Tramite_Asesorias.LaunchIncidentRequest, inValueSol As Tramite_Solicitudes.LaunchIncidentRequest, inValueQue As Tramite_Quejas.LaunchIncidentRequest) As String
            Dim request = String.Empty

            If inValueAse IsNot Nothing Then
                Using stringwriter As StringWriter = New StringWriter()
                    Dim serializer = New XmlSerializer(inValueAse.[GetType]())
                    serializer.Serialize(stringwriter, inValueAse)
                    request = stringwriter.ToString()
                    stringwriter.Dispose()
                End Using
            ElseIf inValueSol IsNot Nothing Then
                Using stringwriter As StringWriter = New StringWriter()
                    Dim serializer = New XmlSerializer(inValueSol.[GetType]())
                    serializer.Serialize(stringwriter, inValueSol)
                    request = stringwriter.ToString()
                    stringwriter.Dispose()
                End Using
            ElseIf inValueQue IsNot Nothing Then
                Using stringwriter As StringWriter = New StringWriter()
                    Dim serializer = New XmlSerializer(inValueQue.[GetType]())
                    serializer.Serialize(stringwriter, inValueQue)
                    request = stringwriter.ToString()
                    stringwriter.Dispose()
                End Using
            End If
            Return request
        End Function

        ''' <summary>
        ''' Finaliza una tarea asignada a un usuario
        ''' </summary>
        ''' <paramname="dtoUltimusAse">BpmDTO--Objeto con los valores para cerrar la tarea</param>
        ''' <CreatedBy>EALVAREZT</CreatedBy>
        ''' <CreationDate>22/04/2017</CreationDate>
        ''' <ModifiedBy></ModifiedBy>
        ''' <ModificationDate></ModificationDate>
        Public Sub FinalizarTareaAsesorias(dtoUltimusAse As BpmDTO)
            Dim esquemas As Tramite_Asesorias.SchemaFile() = Nothing
            Dim strTaskXml = ""
            Dim strError = ""
            Dim oldChar As String = "<UsuarioOC xsi:nil=""true"" xmlns=""http://schema.ultimus.com/AtencionTramiteAse/" & dtoUltimusAse.NumeroVersion.ToString().ToString() & "/Types"" />"
            Dim newChar As String = "<UsuarioOC xsi:nil=""false"" xmlns=""http://schema.ultimus.com/AtencionTramiteAse/" & dtoUltimusAse.NumeroVersion.ToString().ToString() & "/Types"" />"

            Dim oldCharEstadonegado As String = "<EstadoNegado xsi:nil=""true"" xmlns=""http://schema.ultimus.com/AtencionTramiteAse/" & dtoUltimusAse.NumeroVersion.ToString().ToString() & "/Types"" />"
            Dim newCharEstadonegado As String = "<EstadoNegado xsi:nil=""false"" xmlns=""http://schema.ultimus.com/AtencionTramiteAse/" & dtoUltimusAse.NumeroVersion.ToString().ToString() & "/Types"" />"

            Try
                If dtoUltimusAse IsNot Nothing Then
                    Dim numeroIncidente = CInt(dtoUltimusAse.NumeroIncidente)

                    CrearRequestAsesoria()

                    If Not clienteAsesorias.GetTaskInformation(dtoUltimusAse.DominioRed.ToString() & "/" + dtoUltimusAse.UsuarioBPM, CInt(dtoUltimusAse.NumeroIncidente), dtoUltimusAse.[Step], esquemas, strTaskXml, strError) Then Throw New ExcepcionOperacion(HelperMensaje.Obtener(HelperCodigoMensaje.BPM_0004), CInt(CodigosHTTP.[Error]), strError)

                    dtoUltimusAse.strTaskXml = strTaskXml.Replace(oldChar, newChar)

                    If Not String.IsNullOrEmpty(dtoUltimusAse.EstadoNegado) Then
                        If dtoUltimusAse.EstadoNegado.Equals("true") Then
                            dtoUltimusAse.strTaskXml = strTaskXml.Replace(oldCharEstadonegado, newCharEstadonegado)
                        End If
                    End If

                    Me.CompletarDatosAclarar(dtoUltimusAse)

                    If Not clienteAsesorias.CompleteStep(dtoUltimusAse.DominioRed.ToString() & "/" + dtoUltimusAse.UsuarioBPM, numeroIncidente, dtoUltimusAse.[Step], dtoUltimusAse.NumeroRadicacionPostulacion.ToString(), String.Empty, False, 9, dtoUltimusAse.strTaskXml, False, strError) Then Throw New ExcepcionOperacion(HelperMensaje.Obtener(HelperCodigoMensaje.BPM_0005), CInt(CodigosHTTP.[Error]), strError)
                End If
            Catch exo As ExcepcionOperacion
                Throw exo
            Catch ex As Exception
                If ex.InnerException.Message.Contains("Error404") Then
                    Throw New ExcepcionOperacion(HelperMensaje.Obtener(HelperCodigoMensaje.BPM_0001), CInt(CodigosHTTP.[Error]), ex.InnerException)
                Else
                    Throw New ExcepcionOperacion(HelperMensaje.Obtener(HelperCodigoMensaje.BPM_0007), CInt(CodigosHTTP.[Error]), ex.InnerException)
                End If
            End Try
        End Sub

        'Public Sub FinalizarTareaDesembolso(ByVal dtoUltimus As BpmDTO, ByVal dtoDesembolsoUltimus As BpmDesembolsoDTO)
        '    Dim esquemas As wsProcesoDesembolso.SchemaFile() = Nothing
        '    Dim strTaskXml As String = ""
        '    Dim strError As String = ""

        '    Try

        '        If dtoUltimus IsNot Nothing Then
        '            Dim numeroIncidente As Integer = CInt(dtoUltimus.NumeroIncidente)
        '            CrearRequestDesembolso()
        '            If Not clienteDesembolso.GetTaskInformation(dtoUltimus.DominioRed & "/" + dtoUltimus.UsuarioBPM, CInt(dtoUltimus.NumeroIncidente), dtoUltimus.[Step], esquemas, strTaskXml, strError) Then Throw New ExcepcionOperacion(HelperMensaje.Obtener(HelperCodigoMensaje.BPM_0004), CInt(CodigosHTTP.[Error]), strError)
        '            dtoUltimus.strTaskXml = strTaskXml
        '            CompletarDatosDesembolsoCierreTarea(dtoUltimus, dtoDesembolsoUltimus)
        '            If Not clienteDesembolso.CompleteStep(dtoUltimus.DominioRed & "/" + dtoUltimus.UsuarioBPM, numeroIncidente, dtoUltimus.[Step], dtoUltimus.NumeroRadicacionPostulacion.ToString(), String.Empty, False, 9, dtoUltimus.strTaskXml, False, strError) Then Throw New ExcepcionOperacion(HelperMensaje.Obtener(HelperCodigoMensaje.BPM_0005), CInt(CodigosHTTP.[Error]), strError)
        '        End If

        '    Catch ex As Exception

        '        If ex.InnerException.Message.Contains(ErrorHTML.Error404) Then
        '            Throw New ExcepcionOperacion(HelperMensaje.Obtener(HelperCodigoMensaje.BPM_0020), CInt(CodigosHTTP.[Error]), ex.InnerException)
        '        Else
        '            Throw New ExcepcionOperacion(HelperMensaje.Obtener(HelperCodigoMensaje.BPM_0021), CInt(CodigosHTTP.[Error]), ex.InnerException)
        '        End If
        '    End Try
        'End Sub

        ''' <summary>
        ''' Buscar contador del XML de la tarea
        ''' </summary>
        ''' <paramname="dtoUltimus">BpmDTO--Objeto con los valores para cerrar la tarea</param>
        ''' <returns>string</returns>
        ''' <CreatedBy>EALVAREZT</CreatedBy>
        ''' <CreationDate>23/05/2017</CreationDate>
        ''' <ModifiedBy></ModifiedBy>
        ''' <ModificationDate></ModificationDate>
        'Public Function BuscarPasoAclarar(dtoUltimus As BpmDTO) As String
        '    Dim esquemas As wsProcesoDesempleo.SchemaFile() = Nothing
        '    Dim strTaskXml = String.Empty
        '    Dim strError = String.Empty
        '    Dim tareaTemp As TareaBPMDTO = Nothing
        '    Dim pasoRetorno As String = PasoBPM.PendienteAclarar.ToString()

        '    Try
        '        tareaTemp = Me.ConsultaInfoTareaPorPasoUsuarioyNombreProcesoNoEstado(dtoUltimus).Where(Function(x) x.iIncidente Is dtoUltimus.NumeroIncidente AndAlso x.NombreEtapa Is dtoUltimus.[Step]).FirstOrDefault()

        '        CrearRequest()

        '        If Not cliente.GetTaskInformation(tareaTemp.UsuarioAsignado, CInt(dtoUltimus.NumeroIncidente), dtoUltimus.[Step], esquemas, strTaskXml, strError) Then
        '            pasoRetorno = PasoBPM.PendienteAclararAlterno.ToString()
        '        End If

        '        Return pasoRetorno
        '    Catch exo As ExcepcionOperacion
        '        Throw exo
        '    Catch ex As Exception
        '        If ex.InnerException.Message.Contains(ErrorHTML.Error404) Then
        '            Throw New ExcepcionOperacion(HelperMensaje.Obtener(HelperCodigoMensaje.BPM_0001), CInt(CodigosHTTP.[Error]), ex.InnerException)
        '        Else
        '            Throw New ExcepcionOperacion(HelperMensaje.Obtener(HelperCodigoMensaje.BPM_0007), CInt(CodigosHTTP.[Error]), ex.InnerException)
        '        End If
        '    End Try
        'End Function

        ''' <summary>
        ''' Método que realiza el proceso de reclamación de tarea.
        ''' logueo - ObtieneTaskId y AsignaciónTarea
        ''' </summary>
        ''' <paramname="ParametrosBPM">BpmDTO</param>
        ''' <returns>BpmDTO</returns>
        ''' <CreatedBy>EALVAREZT</CreatedBy>
        ''' <CreationDate>14/11/2017</CreationDate>
        ''' <ModifiedBy></ModifiedBy>
        ''' <ModificationDate></ModificationDate>
        Public Function ReclamarTarea(ParametrosBPM As BpmDTO, sessionId As String) As BpmDTO
            Dim NombreUsuario = String.Empty
            Dim ErrorAsignandoTarea = String.Empty
            Dim ErrorCerrandoSesion = String.Empty
            Dim response = False

            Try
                If ParametrosBPM Is Nothing OrElse String.IsNullOrEmpty(ParametrosBPM.DominioRed) OrElse String.IsNullOrEmpty(ParametrosBPM.UsuarioBPM) OrElse String.IsNullOrEmpty(ParametrosBPM.PassBPM) Then Throw New ExcepcionOperacion(HelperMensaje.Obtener(HelperCodigoMensaje.BPM_0011), CInt(CodigosHTTP.[Error]), String.Empty, Nothing)

                If String.IsNullOrEmpty(ParametrosBPM.TareaId) Then
                    NombreUsuario = ParametrosBPM.UsuarioBPM
                    ParametrosBPM.UsuarioBPM = ParametrosBPM.UsuarioDefaultBPM
                    ParametrosBPM.TareaId = Me.ConsultarTareayFechas(ParametrosBPM).TareaId
                    ParametrosBPM.UsuarioBPM = NombreUsuario
                End If

                Dim DominioUsuario = String.Concat(ParametrosBPM.DominioRed, "/", ParametrosBPM.UsuarioBPM)

                Using wsClient As ServicesSoapClient = New ServicesSoapClient()
                    response = wsClient.AssignTask(sessionId, ParametrosBPM.DominioRed, ParametrosBPM.TareaId, DominioUsuario, ErrorAsignandoTarea)
                    If Not response Then Throw New ExcepcionOperacion(HelperMensaje.Obtener(HelperCodigoMensaje.BPM_0013), CInt(CodigosHTTP.[Error]), ErrorAsignandoTarea, Nothing)
                End Using

                Return ParametrosBPM
            Catch exo As ExcepcionOperacion
                Throw exo
            Catch ex As Exception
                Throw New ExcepcionOperacion(HelperMensaje.Obtener(HelperCodigoMensaje.BPM_0014), CInt(CodigosHTTP.[Error]), ex.Message, ex.InnerException)
            End Try
        End Function

        ''' <summary>
        ''' Método que consulta la información de la tarea 
        ''' y retorna un DTO con la información de las
        ''' fechas y la tarea.
        ''' </summary>
        ''' <param name="dtoBpm">BpmDTO-- Objeto Bpm</param>
        ''' <returns>BpmDTO</returns>
        ''' <CreatedBy>EALVAREZT</CreatedBy>
        ''' <CreationDate>22/02/2024</CreationDate>
        ''' <ModifiedBy></ModifiedBy>
        ''' <ModificationDate></ModificationDate>
        Public Function ConsultarTareayFechas(dtoBpm As BpmDTO) As BpmDTO
            Dim listaRespuesta As List(Of TareaBPMDTO) = Nothing
            listaRespuesta = ConsultaInfoTareaPorPasoUsuarioyNombreProceso(dtoBpm)
            Dim task As TareaBPMDTO = listaRespuesta.Where(Function(x) x.iIncidente = dtoBpm.NumeroIncidente AndAlso x.NombreEtapa.Equals(dtoBpm.[Step])).FirstOrDefault()

            If task Is Nothing Then Throw New ExcepcionOperacion(HelperMensaje.Obtener(HelperCodigoMensaje.BPM_0016), CInt(CodigosHTTP.[Error]), String.Empty, Nothing)

            dtoBpm.FechaExtendidaBPM = task.FechaExtendidaBPM
            dtoBpm.FechaVencimientoBPM = task.FechaTerminacion
            dtoBpm.TareaId = task.IdTarea
            Return dtoBpm
        End Function

        ''' <summary>
        ''' Método que realiza la consulta de información de 
        ''' una tarea por paso y nombre de proceso
        ''' </summary>
        ''' <param name="dtoBPM">BpmDTO-- Objeto Bpm</param>
        ''' <returns>List<TareaBPMDTO></returns>
        ''' <CreatedBy>emeza</CreatedBy>
        ''' <CreationDate>23/05/2017</CreationDate>
        ''' <ModifiedBy></ModifiedBy>
        ''' <ModificationDate></ModificationDate>       
        Public Function ConsultaInfoTareaPorPasoUsuarioyNombreProceso(dtoBPM As BpmDTO) As List(Of TareaBPMDTO)
            Dim strXFil = String.Empty
            Dim strXQry = String.Empty

            Dim ListaRespuesta As List(Of TareaBPMDTO) = New List(Of TareaBPMDTO)()

            If dtoBPM IsNot Nothing Then
                Dim sProcessName As String = dtoBPM.NombreProceso
                Dim sNombrePaso As String = dtoBPM.[Step]
                Dim xDoc As XmlNode = Nothing

                If dtoBPM.NumeroIncidente = 0 OrElse String.IsNullOrEmpty(dtoBPM.[Step]) Then Throw New ExcepcionOperacion(HelperMensaje.Obtener(HelperCodigoMensaje.BPM_0012), CInt(CodigosHTTP.[Error]), String.Empty, Nothing)

                Dim DominioUsuario = String.Concat(dtoBPM.DominioRed, "/", dtoBPM.UsuarioBPM)

                If Equals(sNombrePaso, "PendienteAclarar") Then
                    sNombrePaso = dtoBPM.Taskid_PendienteAclarar

                    strXQry = "<Incident>{$a/iIncidentNo}{$a/Tasks/Task/tTaskID}{$a/Tasks/Task/tAssignedToUser}{$a/Tasks/Task/tStepLabel}{$a/Tasks/Task/tStartTime}{$a/Tasks/Task/tOverDueTime}{$a/Tasks/Task/tUrgentDueTime}</Incident>"
                    strXFil = String.Format("for $a in /Incidents/Incident where $a/iProcessName='{0}' and $a/Tasks/Task/tStatus=1 and $a/Tasks/Task/tStepID='{3}' and $a/Tasks/Task/tProcessVersion={4} and $a/Tasks/Task/tAssignedToUser= '{2}' return {1}", sProcessName, strXQry, DominioUsuario, sNombrePaso, dtoBPM.NumeroVersion)
                Else
                    sNombrePaso = dtoBPM.[Step]

                    strXQry = "<Incident>{$a/iIncidentNo}{$a/Tasks/Task/tTaskID}{$a/Tasks/Task/tAssignedToUser}{$a/Tasks/Task/tStepLabel}{$a/Tasks/Task/tStartTime}{$a/Tasks/Task/tOverDueTime}{$a/Tasks/Task/tUrgentDueTime}</Incident>"
                    strXFil = String.Format("for $a in /Incidents/Incident where $a/iProcessName='{0}' and $a/Tasks/Task/tStatus=1 and $a/Tasks/Task/tStepLabel='{3}' and $a/Tasks/Task/tProcessVersion={4} and $a/Tasks/Task/tAssignedToUser= '{2}' return {1}", sProcessName, strXQry, DominioUsuario, sNombrePaso, dtoBPM.NumeroVersion)
                End If

                Using wsClient As UltimusBIS.UltimusBISSoapClient = New UltimusBIS.UltimusBISSoapClient()
                    'xDoc = wsClient.RunProcessQuery(sProcessName, Convert.ToInt32(dtoBPM.NumeroVersion), strXFil, False)
                End Using


                Dim fechaExtendida, fechaTerminacion As Date?
                For Each xItem As XmlNode In xDoc.ChildNodes
                    If xItem.LocalName Is "Incident" Then
                        fechaExtendida = Nothing
                        fechaTerminacion = Nothing
                        For Each element As XmlElement In xItem.ChildNodes
                            If element.LocalName Is "tUrgentDueTime" Then fechaExtendida = Convert.ToDateTime(xItem.SelectSingleNode(CStr("tUrgentDueTime")).InnerText)
                            If element.LocalName Is "tOverDueTime" Then fechaTerminacion = Convert.ToDateTime(xItem.SelectSingleNode(CStr("tOverDueTime")).InnerText)
                        Next

                        ListaRespuesta.Add(New TareaBPMDTO() With {
                                           .IdTarea = xItem.SelectSingleNode(CStr("tTaskID")).InnerText,
                                           .iIncidente = Convert.ToInt64(xItem.SelectSingleNode(CStr("iIncidentNo")).InnerText),
                                           .UsuarioAsignado = xItem.SelectSingleNode(CStr("tAssignedToUser")).InnerText,
                                           .NombreEtapa = xItem.SelectSingleNode(CStr("tStepLabel")).InnerText,
                                           .FechaInicio = Convert.ToDateTime(xItem.SelectSingleNode(CStr("tStartTime")).InnerText),
                                           .FechaExtendidaBPM = fechaExtendida,
                                           .FechaTerminacion = fechaTerminacion
                                           })
                    End If
                Next
            End If
            Return ListaRespuesta
        End Function


        ''' <summary>
        ''' Método que realiza el logueo al servicio de ultimus
        ''' </summary>
        ''' <paramname="DatosLogueoBPM">BpmDTO-- Objeto Bpm</param>
        ''' <returns>string</returns>
        ''' <CreatedBy>EALVAREZT</CreatedBy>
        ''' <CreationDate>23/05/2017</CreationDate>
        ''' <ModifiedBy></ModifiedBy>
        ''' <ModificationDate></ModificationDate>
        Public Function ObtenerIdSesion(DatosLogueoBPM As BpmDTO) As String
            Dim sessionId = ""
            Dim [error] = ""
            Dim response = False

            Using wsClient As ServicesSoapClient = New ServicesSoapClient()
                response = wsClient.LoginUser(DatosLogueoBPM.DominioRed, DatosLogueoBPM.UsuarioDefaultBPM, DatosLogueoBPM.PassBPM, sessionId, [error])
            End Using
            If Not response Then Throw New ExcepcionOperacion(HelperMensaje.Obtener(HelperCodigoMensaje.BPM_0010), CInt(CodigosHTTP.[Error]), [error], Nothing)

            Return sessionId
        End Function

        ''' <summary>
        ''' Libera la sessión en el BPM
        ''' </summary>
        ''' <paramname="sessionId">Parametro con el valor de la sessión</param>
        ''' <CreatedBy>EALVAREZT</CreatedBy>
        ''' <CreationDate>23/05/2017</CreationDate>
        ''' <ModifiedBy></ModifiedBy>
        ''' <ModificationDate></ModificationDate>
        Public Sub LiberarIdSession(sessionId As String)
            Dim ErrorCerrandoSesion As String

            Using wsClient As ServicesSoapClient = New ServicesSoapClient()
                Dim logoutResponse As Boolean = wsClient.LogoutUser(sessionId, ErrorCerrandoSesion)
                If Not logoutResponse Then Throw New ExcepcionOperacion(HelperMensaje.Obtener(HelperCodigoMensaje.BPM_0015), CInt(CodigosHTTP.[Error]), ErrorCerrandoSesion, Nothing)
            End Using
        End Sub

        ''' <summary>
        ''' Método que setea el nombre del proceso dependiendo del flag (bandera) proveniente del BPM.
        ''' </summary>
        ''' <param name="dtoBpm">BpmDTO--Objeto Bpm</param>
        ''' <CreatedBy>EALVAREZT</CreatedBy>
        ''' <CreationDate>22/02/2024</CreationDate>
        ''' <ModifiedBy></ModifiedBy>
        ''' <ModificationDate></ModificationDate>
        Private Sub CompletarDatosAclarar(ByRef dtoBpm As BpmDTO)
            If dtoBpm Is Nothing Then
                Throw New ExcepcionOperacion(HelperMensaje.Obtener(HelperCodigoMensaje.GEN_0010), CInt(CodigosHTTP.[Error]), String.Empty, Nothing)
            End If

            Dim document As XmlDocument = New XmlDocument()
            document.LoadXml(dtoBpm.strTaskXml)
            Dim item As XmlNode = document.LastChild

            For Each itemData As XmlNode In item.ChildNodes

                If itemData.LocalName = "Global" Then

                    For Each element As XmlElement In itemData.ChildNodes

                        If element.LocalName = "PasosId1" Then

                            For Each element2 As XmlElement In element.ChildNodes

                                If element2.LocalName = "CambioEst" Then
                                    element2.InnerText = dtoBpm.NombreEstado
                                End If
                            Next
                        End If

                        If element.LocalName = "EstadoNegado" Then

                            If Not String.IsNullOrEmpty(dtoBpm.EstadoNegado) Then
                                element.InnerText = dtoBpm.EstadoNegado
                            End If
                        End If

                        If element.LocalName = "UsuarioOC" Then
                            element.IsEmpty = False
                            element.InnerText = dtoBpm.UsuarioOc
                        End If
                    Next
                End If
            Next

            dtoBpm.strTaskXml = document.InnerXml
        End Sub

#End Region

    End Class

End Namespace