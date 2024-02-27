Imports LogWriterHelper
Imports Modelo.AtencionTramites.Modelo.dto

Namespace AtencionTramites.AccesoDatos
    Public Class ClasificacionPeticionDB

        Private Shared Function GetHelperSQL() As HelperSQL
            Return New HelperSQL("BDDefensoria")
        End Function

        ''' <summary>
        ''' Permite consultar los documentos asociados a un radicado
        ''' </summary>
        ''' <param name="codigoSolicitud">Código único de un radicado</param>
        ''' <returns>Objeto con la información de la clasificación de la solicitud</returns>
        Public Shared Function ConsultarClasificacionXCodigoSolicitud(ByVal codigoSolicitud As Integer) As ClasificacionPeticionDTO
            Dim strError As String = Nothing
            Dim clasificacionPeticion As New ClasificacionPeticionDTO
            Dim dataSet As New DataSet()

            Try
                Dim sql As String = "spConsultarClasificacionPeticion " & Utilidad.IntegerFormat(codigoSolicitud)

                If GetHelperSQL().Consulta(sql, dataSet, strError) Then
                    If dataSet.Tables.Count = 0 Or dataSet.Tables(0).Rows.Count = 0 Then
                        Dim nombreMetodo = System.Reflection.MethodBase.GetCurrentMethod().Name
                        LogWriter.WriteLog("ClasificacionPeticionDB", nombreMetodo & ":" & "Query no return data.")
                    Else
                        clasificacionPeticion = MapearDatos(dataSet.Tables(0).Rows(0))
                    End If
                Else
                    Dim nombreMetodo = System.Reflection.MethodBase.GetCurrentMethod().Name
                    LogWriter.WriteLog("ClasificacionPeticionDB", nombreMetodo & ":" & strError)
                End If

            Catch ex As Exception
                Dim nombreMetodo = System.Reflection.MethodBase.GetCurrentMethod().Name
                LogWriter.WriteLog("ClasificacionPeticionDB -" & nombreMetodo, ex)
                Throw New Exception("ClasificacionPeticionDB -" & nombreMetodo & "-" & ex.Message, ex)
            Finally
                dataSet = Nothing
            End Try

            Return clasificacionPeticion
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
            Dim strError As String = Nothing

            Try
                Dim sql As String = "spInsClasificacionPeticion "
                sql += Utilidad.IntegerFormat(codigoSolicitud) + ","
                sql += Utilidad.IntegerFormat(codigoTipoPeticion) + ","
                sql += Utilidad.IntegerFormat(codigoAreaDerecho) + ","
                sql += Utilidad.StringFormat(descripcionAsesoria) + ","
                sql += Utilidad.StringFormat(observaciones) + ","
                sql += Utilidad.BooleanFormat(respuestaEscrito) + ","
                sql += Utilidad.StringFormat(codigoConclusionAsesoria) + ","
                sql += Utilidad.StringFormat(nombreUsuario) + ","
                sql += Utilidad.StringFormat(idUsuario)

                If Not GetHelperSQL().EjecutaAccion(sql, strError) Then
                    Dim nombreMetodo = System.Reflection.MethodBase.GetCurrentMethod().Name
                    LogWriter.WriteLog("ClasificacionPeticionDB -" & nombreMetodo, strError)
                    Return False
                End If

                Return True
            Catch ex As Exception
                Dim nombreMetodo = System.Reflection.MethodBase.GetCurrentMethod().Name
                LogWriter.WriteLog("ClasificacionPeticionDB -" & nombreMetodo, ex)
                Throw New Exception("ClasificacionPeticionDB -" & nombreMetodo & "-" & ex.Message, ex)
                Return False
            End Try

        End Function

        ''' <summary>
        ''' Permite mapear los datos de los documentos consultados
        ''' </summary>
        ''' <param name="fila">Filas a mapear</param>
        ''' <returns>Lista de documentos mapeados</returns>
        Private Shared Function MapearDatos(ByRef fila As DataRow) As ClasificacionPeticionDTO

            Dim clasificacionPeticion As New ClasificacionPeticionDTO

            clasificacionPeticion.CodigoSolicitud = Utilidad.ParseInteger(fila.Item("CodigoSolicitud"))

            If IsDBNull(fila("CodigoAreaDerecho")) Then
                clasificacionPeticion.AreaDerecho = Nothing
            Else
                clasificacionPeticion.AreaDerecho = New CatalogoDTO()
                clasificacionPeticion.AreaDerecho.Codigo = Utilidad.ParseString(fila("CodigoAreaDerecho"))
                clasificacionPeticion.AreaDerecho.Nombre = Utilidad.ParseString(fila("NombreAreaDerecho"))
            End If

            If IsDBNull(fila("CodigoConclusionAsesoria")) Then
                clasificacionPeticion.ConclusionAsesoria = Nothing
            Else
                clasificacionPeticion.ConclusionAsesoria = New CatalogoDTO()
                clasificacionPeticion.ConclusionAsesoria.Codigo = Utilidad.ParseString(fila("CodigoConclusionAsesoria"))
                clasificacionPeticion.ConclusionAsesoria.Nombre = Utilidad.ParseString(fila("NombreConclusionAsesoria"))
            End If

            clasificacionPeticion.DescripcionAsesoria = Utilidad.ParseString(fila("DescripcionAsesoria"))
            clasificacionPeticion.Observaciones = Utilidad.ParseString(fila("Observaciones"))
            clasificacionPeticion.RespuestaEscrita = Utilidad.ParseBoolean(fila("RespuestaEscrito"))

            If IsDBNull(fila("CodigoTipoPeticion")) Then
                clasificacionPeticion.TipoPeticion = Nothing
            Else
                clasificacionPeticion.TipoPeticion = New CatalogoDTO()
                clasificacionPeticion.TipoPeticion.Codigo = Utilidad.ParseString(fila("CodigoTipoPeticion"))
                clasificacionPeticion.TipoPeticion.Nombre = Utilidad.ParseString(fila("NombreTipoPeticion"))
            End If

            Return clasificacionPeticion
        End Function

    End Class
End Namespace