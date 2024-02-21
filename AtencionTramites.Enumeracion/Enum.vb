Imports System
Imports System.ComponentModel.DataAnnotations

Namespace AtencionTramites.Enumeracion

    ''' <summary>
    ''' Clase que contiene los valores de la aplicación
    ''' </summary>
    ''' <paramname=""></param>
    ''' <returns></returns>
    ''' <CreatedBy>EALVAREZT</CreatedBy>
    ''' <CreationDate>21/02/2024</CreationDate>
    ''' <ModifiedBy></ModifiedBy>
    ''' <ModificationDate></ModificationDate>
    Public Module [Enum]

        ''' <summary>
        ''' Enumerador que contiene los valores de los codigos HTTP
        ''' </summary>
        ''' <CreatedBy>EALVAREZT</CreatedBy>
        ''' <CreationDate>21/02/2024</CreationDate>
        ''' <ModifiedBy></ModifiedBy>
        ''' <ModificationDate></ModificationDate>
        <Flags>
        Public Enum CodigosHTTP As Integer
            Ok = 200
            No_Autorizado = 401
            NoContent = 204
            [Error] = 400
            Error_Servicio = 500
        End Enum

        ''' <summary>
        ''' Enumerador que contiene los valores de los estados
        ''' </summary>
        ''' <CreatedBy>EALVAREZT</CreatedBy>
        ''' <CreationDate>21/02/2024</CreationDate>
        ''' <ModifiedBy></ModifiedBy>
        ''' <ModificationDate></ModificationDate>
        Public Enum Estado
            <Display(Name:="Radicado")>
            RAD
            <Display(Name:="Negado Definitivo")>
            NGD
            <Display(Name:="Cancelado")>
            CAN
            <Display(Name:="Pre Negado")>
            PND
            <Display(Name:="Pre Aprobado")>
            PAP
            <Display(Name:="Negado")>
            NEG
            <Display(Name:="En Espera Presupuesto")>
            EEP
            <Display(Name:="En Espera")>
            ESP
            <Display(Name:="Adjudicado")>
            ADJ
            <Display(Name:="Suspendido")>
            SUS
            <Display(Name:="En Giro")>
            EGI
            <Display(Name:="Re Activada")>
            RAC
        End Enum

        ''' <summary>
        ''' Enumerador Estados Desembolsos
        ''' </summary>
        ''' <CreatedBy>EALVAREZT</CreatedBy>
        ''' <CreationDate>21/02/2024</CreationDate>
        ''' <ModifiedBy></ModifiedBy>
        ''' <ModificationDate></ModificationDate>
        Public Enum EstadosDesembolso
            <Display(Name:="D. Registrado")>
            DRE
            <Display(Name:="D. Suspendido")>
            DSU
            <Display(Name:="D. Negado")>
            DNE
            <Display(Name:="D. No Aprobado")>
            DNA
            <Display(Name:="D. Aprobado")>
            DAP
            <Display(Name:="D. Pre Aprobado")>
            DPA
            <Display(Name:="D. Fallido")>
            DFA
        End Enum

        ''' <summary>
        ''' Enumerador que contiene 
        ''' </summary>
        ''' <CreatedBy>EALVAREZT</CreatedBy>
        ''' <CreationDate>21/02/2024</CreationDate>
        ''' <ModifiedBy></ModifiedBy>
        ''' <ModificationDate></ModificationDate>
        Public Enum EnumParametros As Integer
            None = 0
            UsuarioBPM = 16
            PasoCompletarTareaPostulacion = 17
            PasoValidarPostulacion = 7
            PasoValidarAdjudicacion = 12
            DominioRed = 18
            FactorIndependienteMR = 20
            TotalTiempoCajaMR = 21
            RutaGeneracionReporte = 8
            RutaRepoteSSRS = 3
            RutaDestinoOnBase = 15
            CorreoNoReplay = 23
            ContrasenaBPM = 24
            ProcesoUltimus = 19
            PasoBPM = 200004
            NombreGrupoUltimus = 46
            CantidadHilosDesembolso = 48
            NombreprocesoUltimusDesempleo = 19
            NombreProcesoUltimusDesembolso = 50
            VersionBPM = 51
            NombreUsuarioDesembolso = 52
            NombrePasoDesembolso = 55
            NombreRutaXsdIncidenteTibco = 56
            NombreRutaXsdCierreTareasTibco = 57
            Taskid_CancelarPostulacion = 414
            Taskid_CompletarPostulacion = 415
            Taskid_NegarPostulacion = 416
            Taskid_PendienteAclarar = 417
        End Enum

        ''' <summary>
        ''' Enumerador que contiene los estados por los que pasa el BPM
        ''' </summary>
        ''' <CreatedBy>EALVAREZT</CreatedBy>
        ''' <CreationDate>21/02/2024</CreationDate>
        ''' <ModifiedBy></ModifiedBy>
        ''' <ModificationDate></ModificationDate>
        Public Enum PasoBPM As Integer
            None = 0
            PendienteAclarar = 1
            PendienteAclararAlterno = 2
            CompletarPostulacion = 3
            RadicarPostulacion = 4
            DejarPostulacionEspera = 5
            CancelarPostulacion = 6
            GestionarEstados = 7
            NegarPostulacion = 8
            NegarPostulacionAlterno = 9
            CargarArchivos = 10
        End Enum

    End Module
End Namespace