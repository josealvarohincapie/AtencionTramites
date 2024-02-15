Public Class Documento

    Private _Codigo As String
    Property Codigo As String
        Get
            Return _Codigo
        End Get
        Set(ByVal Value As String)
            _Codigo = Value
        End Set
    End Property

    Private _Registro As String
    Property Registro As String
        Get
            Return _Registro
        End Get
        Set(ByVal Value As String)
            _Registro = Value
        End Set
    End Property

    Private _Descripcion As String
    Property Descripcion As String
        Get
            Return _Descripcion
        End Get
        Set(ByVal Value As String)
            _Descripcion = Value
        End Set
    End Property

    Private _HasFile As Boolean
    Property HasFile As Boolean
        Get
            Return _HasFile
        End Get
        Set(ByVal Value As Boolean)
            _HasFile = Value
        End Set
    End Property

    Private _Archivo As String
    Property Archivo As String
        Get
            Return _Archivo
        End Get
        Set(ByVal Value As String)
            _Archivo = Value
        End Set
    End Property

    Private _Path As String
    Property Path As String
        Get
            Return _Path
        End Get
        Set(ByVal Value As String)
            _Path = Value
        End Set
    End Property

    Private _Url As String
    Property Url As String
        Get
            Return _Url
        End Get
        Set(ByVal Value As String)
            _Url = Value
        End Set
    End Property

    Private _IsExternal As Boolean
    Property IsExternal As Boolean
        Get
            Return _IsExternal
        End Get
        Set(ByVal Value As Boolean)
            _IsExternal = Value
        End Set
    End Property

    Private _FechaEmision As Date
    Property FechaEmision As Date
        Get
            Return _FechaEmision
        End Get
        Set(ByVal Value As Date)
            _FechaEmision = Value
        End Set
    End Property

    Private _DiasVencimiento As Integer
    Property DiasVencimiento As Integer
        Get
            Return _DiasVencimiento
        End Get
        Set(ByVal Value As Integer)
            _DiasVencimiento = Value
        End Set
    End Property

    Private _Observaciones As String
    Property Observaciones As String
        Get
            Return _Observaciones
        End Get
        Set(ByVal Value As String)
            _Observaciones = Value
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

    Private _RazonSocial As String
    Property RazonSocial As String
        Get
            Return _RazonSocial
        End Get
        Set(ByVal Value As String)
            _RazonSocial = Value
        End Set
    End Property

    Private _Opcional As Boolean
    Property Opcional As Boolean
        Get
            Return _Opcional
        End Get
        Set(ByVal Value As Boolean)
            _Opcional = Value
        End Set
    End Property

    Private _CanRemove As Boolean
    Property CanRemove As Boolean
        Get
            Return _CanRemove
        End Get
        Set(ByVal Value As Boolean)
            _CanRemove = Value
        End Set
    End Property

    ReadOnly Property Prefijo As String
        Get
            Return "-" & Identificacion & "-" & Codigo & "-" & Registro & "-"
        End Get
    End Property

End Class
