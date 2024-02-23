Imports Modelo.Modelo.dto

Public Class PeticionarioDTO
    Private _tipoSolicitante As CatalogoDTO
    Private _esAnonimo As Boolean
    Private _tipoDocumentacion As CatalogoDTO
    Private _numeroIdentificacion As String
    Private _remitente As String
    Private _confidencial As Boolean
    Private _contacto As Boolean
    Private _correo As String
    Private _telefono As String
    Private _direccion As String
    Private _nacionalidad As CatalogoDTO
    Private _pais As CatalogoDTO
    Private _departamento As CatalogoDTO
    Private _ciudad As CatalogoDTO
    Private _centroPoblado As CatalogoDTO
    Private _grupo As CatalogoDTO
    Private _subGrupo As CatalogoDTO
    Private _comunidad As String

    Public Property TipoSolicitante As CatalogoDTO
        Get
            Return _tipoSolicitante
        End Get
        Set(value As CatalogoDTO)
            _tipoSolicitante = value
        End Set
    End Property

    Public Property EsAnonimo As Boolean
        Get
            Return _esAnonimo
        End Get
        Set(value As Boolean)
            _esAnonimo = value
        End Set
    End Property

    Public Property TipoDocumentacion As CatalogoDTO
        Get
            Return _tipoDocumentacion
        End Get
        Set(value As CatalogoDTO)
            _tipoDocumentacion = value
        End Set
    End Property

    Public Property NumeroIdentificacion As String
        Get
            Return _numeroIdentificacion
        End Get
        Set(value As String)
            _numeroIdentificacion = value
        End Set
    End Property

    Public Property Remitente As String
        Get
            Return _remitente
        End Get
        Set(value As String)
            _remitente = value
        End Set
    End Property

    Public Property Confidencial As Boolean
        Get
            Return _confidencial
        End Get
        Set(value As Boolean)
            _confidencial = value
        End Set
    End Property

    Public Property Contacto As Boolean
        Get
            Return _contacto
        End Get
        Set(value As Boolean)
            _contacto = value
        End Set
    End Property

    Public Property Correo As String
        Get
            Return _correo
        End Get
        Set(value As String)
            _correo = value
        End Set
    End Property

    Public Property Telefono As String
        Get
            Return _telefono
        End Get
        Set(value As String)
            _telefono = value
        End Set
    End Property

    Public Property Direccion As String
        Get
            Return _direccion
        End Get
        Set(value As String)
            _direccion = value
        End Set
    End Property

    Public Property Nacionalidad As CatalogoDTO
        Get
            Return _nacionalidad
        End Get
        Set(value As CatalogoDTO)
            _nacionalidad = value
        End Set
    End Property

    Public Property Pais As CatalogoDTO
        Get
            Return _pais
        End Get
        Set(value As CatalogoDTO)
            _pais = value
        End Set
    End Property

    Public Property Departamento As CatalogoDTO
        Get
            Return _departamento
        End Get
        Set(value As CatalogoDTO)
            _departamento = value
        End Set
    End Property

    Public Property Ciudad As CatalogoDTO
        Get
            Return _ciudad
        End Get
        Set(value As CatalogoDTO)
            _ciudad = value
        End Set
    End Property

    Public Property CentroPoblado As CatalogoDTO
        Get
            Return _centroPoblado
        End Get
        Set(value As CatalogoDTO)
            _centroPoblado = value
        End Set
    End Property

    Public Property Grupo As CatalogoDTO
        Get
            Return _grupo
        End Get
        Set(value As CatalogoDTO)
            _grupo = value
        End Set
    End Property

    Public Property SubGrupo As CatalogoDTO
        Get
            Return _subGrupo
        End Get
        Set(value As CatalogoDTO)
            _subGrupo = value
        End Set
    End Property

    Public Property Comunidad As String
        Get
            Return _comunidad
        End Get
        Set(value As String)
            _comunidad = value
        End Set
    End Property
End Class
