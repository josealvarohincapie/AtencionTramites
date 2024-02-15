Public Class Solicitud

    Private _Incidente As Integer
    Property Incidente As Integer
        Get
            Return _Incidente
        End Get
        Set(ByVal Value As Integer)
            _Incidente = Value
        End Set
    End Property

    Private _Area As String
    Property Area As String
        Get
            Return _Area
        End Get
        Set(ByVal Value As String)
            _Area = Value
        End Set
    End Property

    Private _TipoProducto As String
    Property TipoProducto As String
        Get
            Return _TipoProducto
        End Get
        Set(ByVal Value As String)
            _TipoProducto = Value
        End Set
    End Property

    Private _Participacion As Boolean
    Property Participacion As Boolean
        Get
            Return _Participacion
        End Get
        Set(ByVal Value As Boolean)
            _Participacion = Value
        End Set
    End Property

    Private _TipoIdentificacion As Integer
    Property TipoIdentificacion As Integer
        Get
            Return _TipoIdentificacion
        End Get
        Set(ByVal Value As Integer)
            _TipoIdentificacion = Value
        End Set
    End Property

    Private _TipoIdentificacionDescripcion As String
    Property TipoIdentificacionDescripcion As String
        Get
            Return _TipoIdentificacionDescripcion
        End Get
        Set(ByVal Value As String)
            _TipoIdentificacionDescripcion = Value
        End Set
    End Property

    Private _Identificacion As String
    Property Identificacion As String
        Get
            Return _Identificacion
        End Get
        Set(ByVal Value As String)
            _Identificacion = Value
        End Set
    End Property

    Private _Nombre As String
    Property Nombre As String
        Get
            Return _Nombre
        End Get
        Set(ByVal Value As String)
            _Nombre = Value
        End Set
    End Property

    Private _Pais As String
    Property Pais As String
        Get
            Return _Pais
        End Get
        Set(ByVal Value As String)
            _Pais = Value
        End Set
    End Property

    Private _Ciudad As String
    Property Ciudad As String
        Get
            Return _Ciudad
        End Get
        Set(ByVal Value As String)
            _Ciudad = Value
        End Set
    End Property

    Private _Correo As String
    Property Correo As String
        Get
            Return _Correo
        End Get
        Set(ByVal Value As String)
            _Correo = Value
        End Set
    End Property

    Private _Direccion As String
    Property Direccion As String
        Get
            Return _Direccion
        End Get
        Set(ByVal Value As String)
            _Direccion = Value
        End Set
    End Property

    Private _Telefono As String
    Property Telefono As String
        Get
            Return _Telefono
        End Get
        Set(ByVal Value As String)
            _Telefono = Value
        End Set
    End Property

    Private _Actividad As String
    Property Actividad As String
        Get
            Return _Actividad
        End Get
        Set(ByVal Value As String)
            _Actividad = Value
        End Set
    End Property

    Private _ActividadDescripcion As String
    Property ActividadDescripcion As String
        Get
            Return _ActividadDescripcion
        End Get
        Set(ByVal Value As String)
            _ActividadDescripcion = Value
        End Set
    End Property

    Private _TipoCoincidencia As String()
    Property TipoCoincidencia As String()
        Get
            Return _TipoCoincidencia
        End Get
        Set(ByVal Value As String())
            _TipoCoincidencia = Value
        End Set
    End Property

    Private _UrlReporte As String
    Property UrlReporte As String
        Get
            Return _UrlReporte
        End Get
        Set(ByVal Value As String)
            _UrlReporte = Value
        End Set
    End Property

    Private _TipoSemejanza As Integer
    Property TipoSemejanza As Integer
        Get
            Return _TipoSemejanza
        End Get
        Set(ByVal Value As Integer)
            _TipoSemejanza = Value
        End Set
    End Property

    ReadOnly Property DescripcionSemejanza
        Get
            Return IIf(TipoSemejanza = 1, "TOTAL", IIf(TipoSemejanza = 2, "PARCIAL", "SIN C."))
        End Get
    End Property

    Private _ResponsableCliente As String
    Property ResponsableCliente As String
        Get
            Return _ResponsableCliente
        End Get
        Set(ByVal Value As String)
            _ResponsableCliente = Value
        End Set
    End Property

    Private _ResponsableSolicitud As String
    Property ResponsableSolicitud As String
        Get
            Return _ResponsableSolicitud
        End Get
        Set(ByVal Value As String)
            _ResponsableSolicitud = Value
        End Set
    End Property

    Private _Divisa As Integer
    Property Divisa As Integer
        Get
            Return _Divisa
        End Get
        Set(ByVal Value As Integer)
            _Divisa = Value
        End Set
    End Property

    Private _DivisaDescripcion As String
    Property DivisaDescripcion As String
        Get
            Return _DivisaDescripcion
        End Get
        Set(ByVal Value As String)
            _DivisaDescripcion = Value
        End Set
    End Property

    Private _ValorDivisa As Double
    Property ValorDivisa As Double
        Get
            Return _ValorDivisa
        End Get
        Set(ByVal Value As Double)
            _ValorDivisa = Value
        End Set
    End Property

    Private _MontoRecomendado As Double?
    Property MontoRecomendado As Double?
        Get
            Return _MontoRecomendado
        End Get
        Set(ByVal Value As Double?)
            _MontoRecomendado = Value
        End Set
    End Property

    Private _FechaSolicitud As Date
    Property FechaSolicitud As Date
        Get
            Return _FechaSolicitud
        End Get
        Set(ByVal Value As Date)
            _FechaSolicitud = Value
        End Set
    End Property

    Private _TipoPersona As Integer
    Property TipoPersona As Integer
        Get
            Return _TipoPersona
        End Get
        Set(ByVal Value As Integer)
            _TipoPersona = Value
        End Set
    End Property

    Private _EstadoSolicitud As Integer
    Property EstadoSolicitud As Integer
        Get
            Return _EstadoSolicitud
        End Get
        Set(ByVal Value As Integer)
            _EstadoSolicitud = Value
        End Set
    End Property

    Public Sub New()

    End Sub

    Public Complementarios As List(Of Complementario)

End Class
