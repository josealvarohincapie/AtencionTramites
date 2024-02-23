Imports Datos.AtencionTramites.AccesoDatos
Imports LogWriterHelper
Imports Modelo.AtencionTramites.Modelo.dto

Namespace AtencionTramites.Helpers
    Public Class RadicadoHelper

        Public Sub New()

        End Sub

        Public Shared Function ConsultarDatosRadicadoPorCodigo(codigoSolicitud As Int64) As RadicadoDTO
            Dim radicado As RadicadoDTO = Nothing
            Try
                radicado = RadicadoDB.ConsultarDatosRadicadoPorCodigo(codigoSolicitud)
            Catch ex As Exception
                Dim nombreMetodo = System.Reflection.MethodBase.GetCurrentMethod().Name
                LogWriter.WriteLog("RadicadoHelper -" & nombreMetodo, ex)
            End Try

            Return radicado
        End Function

    End Class
End Namespace