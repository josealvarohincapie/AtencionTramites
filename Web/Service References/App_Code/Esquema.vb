Imports UltimusEIK

Public Class Esquema

    Public Shared EstadoSolicitud As New SchemeVariable("//Global/Solicitud/EstadoSolicitud", "EstadoSolicitud")
    Public Shared EstadoSolicitudDescripcion As New SchemeVariable("//Global/Solicitud/EstadoSolicitudDescripcion", "EstadoSolicitudDescripcion")
    Public Shared UltimaEtapa As New SchemeVariable("//Global/Solicitud/UltimaEtapa", "UltimaEtapa")
    Public Shared ActivarAnalizarOCU As New SchemeVariable("//Global/Solicitud/ActivarAnalizarOCU", "ActivarAnalizarOCU")
    Public Shared ActivarAnalizarDJU As New SchemeVariable("//Global/Solicitud/ActivarAnalizarDJU", "ActivarAnalizarDJU")
    Public Shared ActivarAnalizarDRF As New SchemeVariable("//Global/Solicitud/ActivarAnalizarDRF", "ActivarAnalizarDRF")
    Public Shared ActivarAnalizarAmbienteSocial As New SchemeVariable("//Global/Solicitud/ActivarAnalizarAmbienteSocial", "ActivarAnalizarAmbienteSocial")
    Public Shared ActivarPreviabilidadDRF As New SchemeVariable("//Global/Solicitud/ActivarPreviabilidadDRF", "ActivarPreviabilidadDRF")
    Public Shared UltimoComentario As New SchemeVariable("//Global/Solicitud/UltimoComentario", "UltimoComentario")
    Public Shared DestinatariosNotificacion As New SchemeVariable("//Global/Propiedades/DestinatariosNotificacion", "DestinatariosNotificacion")
    Public Shared TipoProducto As New SchemeVariable("//Global/Solicitud/TipoProducto", "TipoProducto")
    Public Shared TipoProductoDescripcion As New SchemeVariable("//Global/Solicitud/TipoProductoDescripcion", "TipoProductoDescripcion")
    Public Shared IdentificacionCliente As New SchemeVariable("//Global/Solicitud/IdentificacionCliente", "IdentificacionCliente")
    Public Shared NombreCliente As New SchemeVariable("//Global/Solicitud/NombreCliente", "NombreCliente")
    Public Shared EtapaDevolucion As New SchemeVariable("//Global/Solicitud/EtapaDevolucion", "EtapaDevolucion")
    Public Shared AlertaAplazado As New SchemeVariable("//Global/Propiedades/AlertaAplazado", "AlertaAplazado")
    Public Shared DiasAplazado As New SchemeVariable("//Global/Propiedades/DiasAplazado", "DiasAplazado")
    Public Shared RecipientUsuarioAnalisis As New SchemeVariable("//Global/Solicitud/RecipientUsuarioAnalisis", "RecipientUsuarioAnalisis")
End Class