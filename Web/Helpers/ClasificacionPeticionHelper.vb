Imports Datos.AtencionTramites.AccesoDatos
Imports LogWriterHelper
Imports Modelo.AtencionTramites.Modelo.dto

Namespace AtencionTramites.Helpers
    Public Class ClasificacionPeticionHelper

        Public Sub New()

        End Sub

        Public Shared Sub InicializacionClasificacion(codigoSolicitud As Int64)

            Try
                Dim clasificacion As ClasificacionPeticionDTO = ConsultarClasificacionXCodigoSolicitud(codigoSolicitud)

                clasificacion.Derechos = CatalogoDB.ConsultarListaCatalogoPorCodigoSolicitud(codigoSolicitud)

            Catch ex As Exception
                Dim nombreMetodo = System.Reflection.MethodBase.GetCurrentMethod().Name
                LogWriter.WriteLog("ClasificacionPeticionHelper - " & nombreMetodo, ex)
            End Try

        End Sub

        Public Shared Function ConsultarClasificacionXCodigoSolicitud(codigoSolicitud As Int64) As ClasificacionPeticionDTO
            Dim clasificacion As ClasificacionPeticionDTO = Nothing
            Try

                clasificacion = ClasificacionPeticionDB.ConsultarClasificacionXCodigoSolicitud(codigoSolicitud)
            Catch ex As Exception
                Dim nombreMetodo = System.Reflection.MethodBase.GetCurrentMethod().Name
                Dim cadenaLog As String = "ClasificacionPeticionHelper -" & nombreMetodo
                LogWriter.WriteLog(cadenaLog, ex)
                Throw New Exception(cadenaLog & "-" & ex.Message, ex)
            End Try

            Return clasificacion
        End Function

        ''' <summary>
        ''' Permite guardar o modificar la clasificación de la petición
        ''' </summary>
        ''' <param name="codigoSolicitud">Código único de la solicitud</param>
        ''' <param name="codigoTipoPeticion">Código único del catálogo tipo de peticion</param>
        ''' <param name="codigoAreaDerecho">Código único del catálogo Área de Derecho</param>
        ''' <param name="descripcionAsesoria">Descripción de la asesoría</param>
        ''' <param name="observaciones">Observaciones de la clasificación</param>
        ''' <param name="respuestaEscrito">¿La asesoría debe generar respuesta por escrito? 1:si - 0:No</param>
        ''' <param name="codigoConclusionAsesoria">Código único del catálogo Conclusión asesoría</param>
        ''' <param name="nombreUsuario">Nombre del usuario que crea o modifica el registro</param>
        ''' <param name="idUsuario">Is del usuario que crea o modifica el registro</param>
        ''' <returns>Variable lógica que define si la transacción fue exitosa o generó error</returns>
        Public Shared Function GuardarRadicarClasificacion(codigoSolicitud As Int64,
            codigoTipoPeticion As Int32, codigoAreaDerecho As Int32,
            descripcionAsesoria As String, observaciones As String, respuestaEscrito As Boolean,
            codigoConclusionAsesoria As Int32, nombreUsuario As String, idUsuario As String) As Boolean

            Dim respuesta As Boolean = True
            Try

                respuesta = ClasificacionPeticionDB.GuardarRadicarClasificacion(
                    codigoSolicitud, codigoTipoPeticion, codigoAreaDerecho,
                    descripcionAsesoria, observaciones, respuestaEscrito,
                    codigoConclusionAsesoria, nombreUsuario, idUsuario)

            Catch ex As Exception
                Dim nombreMetodo = System.Reflection.MethodBase.GetCurrentMethod().Name
                Dim cadenaLog As String = "ClasificacionPeticionHelper -" & nombreMetodo
                LogWriter.WriteLog(cadenaLog, ex)
                Throw New Exception(cadenaLog & "-" & ex.Message, ex)
            End Try

            Return respuesta
        End Function

    End Class
End Namespace