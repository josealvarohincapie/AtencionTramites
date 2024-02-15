Public Class Catalogo
    Private _id As Integer
    Private _idCatalogoRelacion As Integer

    Public Sub New(ByVal id As Integer)
        _id = id
        _idCatalogoRelacion = 0
    End Sub

    Public Sub New(ByVal id As Integer, ByVal idCatalogoRelacion As Integer)
        _id = id
        _idCatalogoRelacion = idCatalogoRelacion
    End Sub

    Friend Function GetCatalogoId() As String
        Return _id
    End Function

    Friend Function GetCatalogoIdCatalogoRelacion() As String
        Return _idCatalogoRelacion
    End Function

End Class
