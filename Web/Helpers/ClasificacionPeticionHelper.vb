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
                LogWriter.WriteLog("ClasificacionPeticionHelper -" & nombreMetodo, ex)
            End Try

            Return radicado
        End Function

    End Class
End Namespace