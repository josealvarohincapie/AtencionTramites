Imports Datos
Imports LogWriterHelper
Imports Modelo.AtencionTramites.Modelo.dto

Namespace AtencionTramites.AccesoDatos
    Public Class DocumentoDB

        Private Shared Function GetHelperSQL() As HelperSQL
            Return New HelperSQL("BDDefensoria")
        End Function

        ''' <summary>
        ''' Permite consultar los documentos asociados a un radicado
        ''' </summary>
        ''' <param name="codigoSolicitud">Código único de un radicado</param>
        ''' <returns>Lista con la información de los documentos asociados a un radicado</returns>
        Public Shared Function ConsultarDocumentosPorCodigoRadicado(ByVal codigoSolicitud As Integer) As List(Of DocumentoDTO)
            Dim strError As String = Nothing
            Dim listaDocumentos As New List(Of DocumentoDTO)()
            Dim dataSet As New DataSet()

            Try
                Dim sql As String = "spConsultarDocumentosPorCodigoRadicado " & Utilidad.IntegerFormat(codigoSolicitud)

                If GetHelperSQL().Consulta(sql, dataSet, strError) Then
                    If dataSet.Tables.Count = 0 Or dataSet.Tables(0).Rows.Count = 0 Then
                        Dim nombreMetodo = System.Reflection.MethodBase.GetCurrentMethod().Name
                        LogWriter.WriteLog("DocumentoDB", nombreMetodo & ":" & "Query no return data.")
                    Else
                        listaDocumentos = MapearDatos(dataSet.Tables(0).Rows)
                    End If
                Else
                    Dim nombreMetodo = System.Reflection.MethodBase.GetCurrentMethod().Name
                    LogWriter.WriteLog("DocumentoDB", nombreMetodo & ":" & strError)
                End If

            Catch ex As Exception
                Dim nombreMetodo = System.Reflection.MethodBase.GetCurrentMethod().Name
                LogWriter.WriteLog("DocumentoDB -" & nombreMetodo, ex)
                Throw New Exception("DocumentoDB -" & nombreMetodo & "-" & ex.Message, ex)
            Finally
                dataSet = Nothing
            End Try

            Return listaDocumentos
        End Function

        ''' <summary>
        ''' Permite mapear los datos de los documentos consultados
        ''' </summary>
        ''' <param name="filas">Filas a mapear</param>
        ''' <returns>Lista de documentos mapeados</returns>
        Private Shared Function MapearDatos(ByRef filas As DataRowCollection) As List(Of DocumentoDTO)

            Dim listaDocumentos As New List(Of DocumentoDTO)()

            For i As Integer = 0 To filas.Count - 1

                Dim documento = New DocumentoDTO()

                documento.CodigoRadicadoDocumento = Utilidad.ParseString(filas(i).Item("CodigoRadicadoDocumento"))
                documento.CodigoSolicitud = Utilidad.ParseInteger(filas(i).Item("CodigoSolicitud"))
                documento.TituloArchivo = Utilidad.ParseString(filas(i).Item("TituloArchivo"))
                documento.FechaCreacion = Utilidad.ParseDate(filas(i).Item("FechaCreacion"))
                documento.NombreUsuarioCreacion = Utilidad.ParseString(filas(i).Item("NombreUsuarioCreacion"))
                documento.RutaFisicaArchivo = Utilidad.ParseString(filas(i).Item("RutaFisicaArchivo"))
                documento.RutaVirtualArchivo = Utilidad.ParseString(filas(i).Item("RutaVirtualArchivo"))
                documento.TamanoArchivo = Utilidad.ParseDecimal(filas(i).Item("TamanoArchivo"))

                listaDocumentos.Add(documento)
            Next

            Return listaDocumentos
        End Function

    End Class
End Namespace