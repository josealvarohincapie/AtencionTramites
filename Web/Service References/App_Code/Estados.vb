Public Class Estados

    Public Shared Viable As New Estado(1, "Viable", "label-success")
    Public Shared NoViable As New Estado(2, "No Viable", "label-danger")
    Public Shared Continuar As New Estado(3, "Continuar", "label-primary")
    Public Shared Recomendado As New Estado(4, "Recomendado", "label-success")
    Public Shared NoRecomendado As New Estado(5, "No Recomendado", "label-danger")
    Public Shared Rechazado As New Estado(6, "Rechazado", "label-danger")
    Public Shared Devuelto As New Estado(7, "Devuelto", "label-warning")
    Public Shared ContinuarPendientes As New Estado(8, "Continuar Pendientes SARLAFT", "label-primary")
    Public Shared Aprobado As New Estado(9, "Aprobado", "label-success")
    Public Shared Aplazado As New Estado(10, "Aplazado", "label-warning")
    Public Shared DevueltoMonto As New Estado(11, "Devuelto por Monto", "label-warning")
    Public Shared AnalisisOCU As New Estado(12, "Análisis OCU", "label-info")
    Public Shared AnalisisDJU As New Estado(13, "Análisis DJU", "label-info")
    Public Shared AnalisisDRF As New Estado(14, "Análisis DRF", "label-info")
    Public Shared ContinuarComiteInterno As New Estado(15, "Continuar", "label-primary")

    Private Shared _Estados() As Estado = New Estado() {Viable, NoViable, Continuar, Recomendado, NoRecomendado, Rechazado, Devuelto, ContinuarPendientes, Aprobado, Aplazado, DevueltoMonto, AnalisisOCU, AnalisisDJU, AnalisisDRF}
    Public Shared Function GetFromName(ByVal Name As String) As Estado
        Return _Estados.FirstOrDefault(Function(e) e.Name = Name)
    End Function

End Class

Public Class Estado

    Private _id As Integer
    Private _name As String
    Private _css As String

    Friend Sub New(ByVal id As Integer, ByVal name As String, ByVal css As String)
        _id = id
        _name = name
        _css = css
    End Sub

    ReadOnly Property Id As Integer
        Get
            Return _id
        End Get
    End Property

    ReadOnly Property Name As String
        Get
            Return _name
        End Get
    End Property

    ReadOnly Property Css As String
        Get
            Return _css
        End Get
    End Property

End Class