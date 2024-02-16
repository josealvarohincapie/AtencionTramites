Namespace Modelo.dto

    Public Class RadicadoDTO

        Private _codigoSolicitud As Int64
        Private _tipoTramite As CatalogoDTO
        Private _fuente As CatalogoDTO
        Private _incidente As Int32
        Private _numeroRadicado As String
        Private _fecha As Date
        Private _remitente As String
        Private _asunto As String
        Private _direccion As String
        Private _telefono As String
        Private _entidad As CatalogoDTO
        Private _correo As String
        Private _folios As Int32
        Private _anexos As String
        Private _esUrgente As Boolean
        Private _identificacion As String
        Private _tipoDocumento As CatalogoDTO
        Private _subTipoDocumento As CatalogoDTO
        Private _grupoEtnico As CatalogoDTO
        Private _situacionDiscapacidad As CatalogoDTO
        Private _sujetoEspecialProteccion As CatalogoDTO
        Private _estadoCivil As CatalogoDTO
        Private _nivelEstudio As CatalogoDTO
        Private _discapacidad As Boolean
        Private _grupoEtnicoReconoce As Boolean
        Private _genero As CatalogoDTO
        Private _sexo As CatalogoDTO
        Private _orientacionSexual As CatalogoDTO
        Private _procedencia As CatalogoDTO
        Private _rangoEdad As CatalogoDTO
        Private _tipoSolicitante As CatalogoDTO
        Private _esAnonimo As Boolean
        Private _tipoDocId As CatalogoDTO
        Private _pais As CatalogoDTO
        Private _dpto As CatalogoDTO
        Private _ciudad As CatalogoDTO
        Private _medioRespuesta As CatalogoDTO
        Private _tipoPqrs As CatalogoDTO
        Private _resumen As String
        Private _descripcionHechos As String
        Private _descripcionSolicitud As String
        Private _dptoHechos As CatalogoDTO
        Private _municipioHechos As CatalogoDTO
        Private _formato As CatalogoDTO
        Private _observaciones As String
        Private _cedula As String

        Public Property CodigoSolicitud As Long
            Get
                Return _codigoSolicitud
            End Get
            Set(value As Long)
                _codigoSolicitud = value
            End Set
        End Property

        Public Property TipoTramite As CatalogoDTO
            Get
                Return _tipoTramite
            End Get
            Set(value As CatalogoDTO)
                _tipoTramite = value
            End Set
        End Property

        Public Property Fuente As CatalogoDTO
            Get
                Return _fuente
            End Get
            Set(value As CatalogoDTO)
                _fuente = value
            End Set
        End Property

        Public Property Incidente As Integer
            Get
                Return _incidente
            End Get
            Set(value As Integer)
                _incidente = value
            End Set
        End Property

        Public Property NumeroRadicado As String
            Get
                Return _numeroRadicado
            End Get
            Set(value As String)
                _numeroRadicado = value
            End Set
        End Property

        Public Property Fecha As Date
            Get
                Return _fecha
            End Get
            Set(value As Date)
                _fecha = value
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

        Public Property Asunto As String
            Get
                Return _asunto
            End Get
            Set(value As String)
                _asunto = value
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

        Public Property Telefono As String
            Get
                Return _telefono
            End Get
            Set(value As String)
                _telefono = value
            End Set
        End Property

        Public Property Entidad As CatalogoDTO
            Get
                Return _entidad
            End Get
            Set(value As CatalogoDTO)
                _entidad = value
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

        Public Property Folios As Integer
            Get
                Return _folios
            End Get
            Set(value As Integer)
                _folios = value
            End Set
        End Property

        Public Property Anexos As String
            Get
                Return _anexos
            End Get
            Set(value As String)
                _anexos = value
            End Set
        End Property

        Public Property EsUrgente As Boolean
            Get
                Return _esUrgente
            End Get
            Set(value As Boolean)
                _esUrgente = value
            End Set
        End Property

        Public Property TipoDocumento As CatalogoDTO
            Get
                Return _tipoDocumento
            End Get
            Set(value As CatalogoDTO)
                _tipoDocumento = value
            End Set
        End Property

        Public Property SubTipoDocumento As CatalogoDTO
            Get
                Return _subTipoDocumento
            End Get
            Set(value As CatalogoDTO)
                _subTipoDocumento = value
            End Set
        End Property

        Public Property GrupoEtnico As CatalogoDTO
            Get
                Return _grupoEtnico
            End Get
            Set(value As CatalogoDTO)
                _grupoEtnico = value
            End Set
        End Property

        Public Property SituacionDiscapacidad As CatalogoDTO
            Get
                Return _situacionDiscapacidad
            End Get
            Set(value As CatalogoDTO)
                _situacionDiscapacidad = value
            End Set
        End Property

        Public Property SujetoEspecialProteccion As CatalogoDTO
            Get
                Return _sujetoEspecialProteccion
            End Get
            Set(value As CatalogoDTO)
                _sujetoEspecialProteccion = value
            End Set
        End Property

        Public Property EstadoCivil As CatalogoDTO
            Get
                Return _estadoCivil
            End Get
            Set(value As CatalogoDTO)
                _estadoCivil = value
            End Set
        End Property

        Public Property NivelEstudio As CatalogoDTO
            Get
                Return _nivelEstudio
            End Get
            Set(value As CatalogoDTO)
                _nivelEstudio = value
            End Set
        End Property

        Public Property Discapacidad As Boolean
            Get
                Return _discapacidad
            End Get
            Set(value As Boolean)
                _discapacidad = value
            End Set
        End Property

        Public Property GrupoEtnicoReconoce As Boolean
            Get
                Return _grupoEtnicoReconoce
            End Get
            Set(value As Boolean)
                _grupoEtnicoReconoce = value
            End Set
        End Property

        Public Property Genero As CatalogoDTO
            Get
                Return _genero
            End Get
            Set(value As CatalogoDTO)
                _genero = value
            End Set
        End Property

        Public Property Sexo As CatalogoDTO
            Get
                Return _sexo
            End Get
            Set(value As CatalogoDTO)
                _sexo = value
            End Set
        End Property

        Public Property OrientacionSexual As CatalogoDTO
            Get
                Return _orientacionSexual
            End Get
            Set(value As CatalogoDTO)
                _orientacionSexual = value
            End Set
        End Property

        Public Property Procedencia As CatalogoDTO
            Get
                Return _procedencia
            End Get
            Set(value As CatalogoDTO)
                _procedencia = value
            End Set
        End Property

        Public Property RangoEdad As CatalogoDTO
            Get
                Return _rangoEdad
            End Get
            Set(value As CatalogoDTO)
                _rangoEdad = value
            End Set
        End Property

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

        Public Property TipoDocId As CatalogoDTO
            Get
                Return _tipoDocId
            End Get
            Set(value As CatalogoDTO)
                _tipoDocId = value
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

        Public Property Dpto As CatalogoDTO
            Get
                Return _dpto
            End Get
            Set(value As CatalogoDTO)
                _dpto = value
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

        Public Property MedioRespuesta As CatalogoDTO
            Get
                Return _medioRespuesta
            End Get
            Set(value As CatalogoDTO)
                _medioRespuesta = value
            End Set
        End Property

        Public Property TipoPqrs As CatalogoDTO
            Get
                Return _tipoPqrs
            End Get
            Set(value As CatalogoDTO)
                _tipoPqrs = value
            End Set
        End Property

        Public Property Resumen As String
            Get
                Return _resumen
            End Get
            Set(value As String)
                _resumen = value
            End Set
        End Property

        Public Property DescripcionHechos As String
            Get
                Return _descripcionHechos
            End Get
            Set(value As String)
                _descripcionHechos = value
            End Set
        End Property

        Public Property DescripcionSolicitud As String
            Get
                Return _descripcionSolicitud
            End Get
            Set(value As String)
                _descripcionSolicitud = value
            End Set
        End Property

        Public Property DptoHechos As CatalogoDTO
            Get
                Return _dptoHechos
            End Get
            Set(value As CatalogoDTO)
                _dptoHechos = value
            End Set
        End Property

        Public Property MunicipioHechos As CatalogoDTO
            Get
                Return _municipioHechos
            End Get
            Set(value As CatalogoDTO)
                _municipioHechos = value
            End Set
        End Property

        Public Property Formato As CatalogoDTO
            Get
                Return _formato
            End Get
            Set(value As CatalogoDTO)
                _formato = value
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

        Public Property Cedula As String
            Get
                Return _cedula
            End Get
            Set(value As String)
                _cedula = value
            End Set
        End Property

        Public Property Identificacion As String
            Get
                Return _identificacion
            End Get
            Set(value As String)
                _identificacion = value
            End Set
        End Property
    End Class
End Namespace