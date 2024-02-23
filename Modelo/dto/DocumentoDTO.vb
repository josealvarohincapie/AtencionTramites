Namespace AtencionTramites.Modelo.dto

    Public Class DocumentoDTO

        Private _codigoSolicitud As Int64
        Private _codigoRadicadoDocumento As String
        Private _tituloArchivo As String
        Private _fechaCreacion As Date
        Private _nombreUsuarioCreacion As String
        Private _rutaFisicaArchivo As String
        Private _rutaVirtualArchivo As String
        Private _tamanoArchivo As Decimal

        Public Property CodigoSolicitud As Long
            Get
                Return _codigoSolicitud
            End Get
            Set(value As Long)
                _codigoSolicitud = value
            End Set
        End Property

        Public Property CodigoRadicadoDocumento As String
            Get
                Return _codigoRadicadoDocumento
            End Get
            Set(value As String)
                _codigoRadicadoDocumento = value
            End Set
        End Property

        Public Property TituloArchivo As String
            Get
                Return _tituloArchivo
            End Get
            Set(value As String)
                _tituloArchivo = value
            End Set
        End Property

        Public Property FechaCreacion As Date
            Get
                Return _fechaCreacion
            End Get
            Set(value As Date)
                _fechaCreacion = value
            End Set
        End Property

        Public Property NombreUsuarioCreacion As String
            Get
                Return _nombreUsuarioCreacion
            End Get
            Set(value As String)
                _nombreUsuarioCreacion = value
            End Set
        End Property

        Public Property RutaFisicaArchivo As String
            Get
                Return _rutaFisicaArchivo
            End Get
            Set(value As String)
                _rutaFisicaArchivo = value
            End Set
        End Property

        Public Property RutaVirtualArchivo As String
            Get
                Return _rutaVirtualArchivo
            End Get
            Set(value As String)
                _rutaVirtualArchivo = value
            End Set
        End Property

        Public Property TamanoArchivo As Decimal
            Get
                Return _tamanoArchivo
            End Get
            Set(value As Decimal)
                _tamanoArchivo = value
            End Set
        End Property
    End Class
End Namespace