Imports System.Web.Script.Serialization

Public Class Complementario

    Private _IdTipoIdentificacion As Integer
    Property IdTipoIdentificacion As Integer
        Get
            Return _IdTipoIdentificacion
        End Get
        Set(ByVal Value As Integer)
            _IdTipoIdentificacion = Value
        End Set
    End Property

    Private _TipoIdentificacion As String
    Property TipoIdentificacion As String
        Get
            Return _TipoIdentificacion
        End Get
        Set(ByVal Value As String)
            _TipoIdentificacion = Value
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

    Private _IdTipoRelacion As Integer
    Property IdTipoRelacion As Integer
        Get
            Return _IdTipoRelacion
        End Get
        Set(ByVal Value As Integer)
            _IdTipoRelacion = Value
        End Set
    End Property

    Private _TipoRelacion As String
    Property TipoRelacion As String
        Get
            Return _TipoRelacion
        End Get
        Set(ByVal Value As String)
            _TipoRelacion = Value
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

    Private _IdActividad As String
    Property IdActividad As String
        Get
            Return _IdActividad
        End Get
        Set(ByVal Value As String)
            _IdActividad = Value
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

    Private _Participacion As Boolean
    Property Participacion As Boolean
        Get
            Return _Participacion
        End Get
        Set(ByVal Value As Boolean)
            _Participacion = Value
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

    Private _TipoSemejanza As Integer
    Property TipoSemejanza As Integer
        Get
            Return _TipoSemejanza
        End Get
        Set(ByVal Value As Integer)
            _TipoSemejanza = Value
        End Set
    End Property

    ReadOnly Property DescripcionSemejanza As String
        Get
            Return IIf(TipoSemejanza = 1, "TOTAL", IIf(TipoSemejanza = 2, "PARCIAL", "SIN C."))
        End Get
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

    Public Sub New()

    End Sub

End Class

