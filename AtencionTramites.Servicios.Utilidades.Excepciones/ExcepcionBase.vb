Namespace AtencionTramites.Servicios.Utilidades.Excepciones
    Public Class ExcepcionBase
        Inherits Exception

        Private ReadOnly _referencia As Object
        Public ReadOnly Property Referencia As Object
            Get
                Return _referencia
            End Get
        End Property


        Protected Sub New(message As String, referencia As Object, Optional innerException As Exception = Nothing)
            MyBase.New(message, innerException)
            _referencia = referencia
        End Sub

        Protected Sub New(message As String)
            MyBase.New(message)

        End Sub
    End Class
End Namespace
