Imports System
Imports LogWriterHelper
Imports AtencionTramites.Servicios.Utilidades.Mensajes.Compensar.Servicios.Utilidades.Mensajes

Namespace AtencionTramites.Servicios.Utilidades.Mensajes
    Module HelperMensaje
        Function Obtener(ByVal codigo As String) As String
            Try
                Return String.Format("{0}|{1}", codigo, CatalogoMensajes.Instancia.GetMensaje(codigo))
            Catch ex As Exception
                LogWriter.WriteLog("HelperMensaje.Obtener", "Error al obtener el mensaje del archivo mensajes.xml. Razón: {0} Excepcion:" & ex.Message)
                Return String.Format("Error al obtener el mensaje del archivo mensajes.xml. Razón: {0}", ex.Message)
            End Try
        End Function
    End Module
End Namespace
