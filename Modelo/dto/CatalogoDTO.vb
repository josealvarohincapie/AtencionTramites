Namespace Modelo.dto

    Public Class CatalogoDTO

        Private _codigo As String
        Private _nombre As String

        Public Sub New()

        End Sub

        Public Sub New(codigo As String, nombre As String)
            If codigo Is Nothing Then
                Throw New ArgumentNullException(NameOf(codigo))
            End If

            If nombre Is Nothing Then
                Throw New ArgumentNullException(NameOf(nombre))
            End If

            Me.Codigo = codigo
            Me.Nombre = nombre
        End Sub

        Public Property Codigo As String
            Get
                Return _codigo
            End Get
            Set(value As String)
                _codigo = value
            End Set
        End Property

        Public Property Nombre As String
            Get
                Return _nombre
            End Get
            Set(value As String)
                _nombre = value
            End Set
        End Property
    End Class
End Namespace