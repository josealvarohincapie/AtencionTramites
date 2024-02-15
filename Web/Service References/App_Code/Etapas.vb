Public Class Etapas

    Public Shared SolicitarCredito As New Etapa("Solicitar Credito")
    Public Shared CorrespondenciaRecibida As New Etapa("Correspondencia Recibida")
    Public Shared AnalizarRiesgos As New Etapa("Analizar OCU")
    Public Shared AnalizarCapacidad As New Etapa("Analizar DJU")
    Public Shared PreviabilidadDRF As New Etapa("Previabilidad DRF")
    Public Shared AnalizarAmbientalSocial As New Etapa("Analizar Ambiental Social")
    Public Shared ValidarAnalisisOCU As New Etapa("Validar Analisis OCU")
    Public Shared EmitirConceptoGOC As New Etapa("Emitir Concepto GOC")
    Public Shared ValidarAnalisisDJU As New Etapa("Validar Analisis DJU")
    Public Shared EmitirConceptoVJU As New Etapa("Emitir Concepto VJU")
    Public Shared AnalizarDRF As New Etapa("Analizar DRF")
    Public Shared ValidarAnalisisDRF As New Etapa("Validar Analisis DRF")
    Public Shared EmitirConceptoVRI As New Etapa("Emitir Concepto VRI")
    Public Shared PrepararPresentacion As New Etapa("Preparar Presentacion")
    Public Shared RecomendarComiteExterno As New Etapa("Recomendar Comite Externo")
    Public Shared RecomendarComiteInterno As New Etapa("Recomendar Comite Interno")
    Public Shared AprobarJuntaDirectiva As New Etapa("Aprobar Junta Directiva")
    Public Shared AsignacionDRC As New Etapa("Asignacion DRC")
    Public Shared PreviabilidadDRC As New Etapa("Previabilidad DRC")
    Public Shared AnalizarDRC As New Etapa("Analizar DRC")
    Public Shared ValidarAnalisisDRC As New Etapa("Validar Analisis DRC")
End Class

Public Class Etapa

    Private _name As String

    Friend Sub New(ByVal name As String)
        _name = name
    End Sub

    ReadOnly Property Name As String
        Get
            Return _name
        End Get
    End Property

End Class