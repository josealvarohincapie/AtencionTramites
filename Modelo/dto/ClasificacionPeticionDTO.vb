Namespace Modelo.dto

    Public Class ClasificacionPeticionDTO

        Private _tipoPeticion As CatalogoDTO
        Private _areaDerecho As CatalogoDTO
        Private _derechos As List(Of CatalogoDTO)
        Private _descripcionAsesoria As String
        Private _observaciones As String
        Private _respuestaEscrita As Boolean
        Private _conclusionAsesoria As String

        Public Property TipoPeticion As CatalogoDTO
            Get
                Return _tipoPeticion
            End Get
            Set(value As CatalogoDTO)
                _tipoPeticion = value
            End Set
        End Property

        Public Property AreaDerecho As CatalogoDTO
            Get
                Return _areaDerecho
            End Get
            Set(value As CatalogoDTO)
                _areaDerecho = value
            End Set
        End Property

        Public Property Derechos As List(Of CatalogoDTO)
            Get
                Return _derechos
            End Get
            Set(value As List(Of CatalogoDTO))
                _derechos = value
            End Set
        End Property

        Public Property DescripcionAsesoria As String
            Get
                Return _descripcionAsesoria
            End Get
            Set(value As String)
                _descripcionAsesoria = value
            End Set
        End Property

        Public Property Observaciones As String
            Get
                Return _observaciones
            End Get
            Set(value As String)
                _observaciones = value
            End Set
        End Property

        Public Property RespuestaEscrita As Boolean
            Get
                Return _respuestaEscrita
            End Get
            Set(value As Boolean)
                _respuestaEscrita = value
            End Set
        End Property

        Public Property ConclusionAsesoria As String
            Get
                Return _conclusionAsesoria
            End Get
            Set(value As String)
                _conclusionAsesoria = value
            End Set
        End Property
    End Class
End Namespace