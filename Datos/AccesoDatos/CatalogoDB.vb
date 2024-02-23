Imports Datos
Imports LogWriterHelper
Imports Modelo.AtencionTramites.Modelo.dto

Namespace AtencionTramites.AccesoDatos
    Public Class CatalogoDB

        Private Shared Function GetHelperSQL() As HelperSQL
            Return New HelperSQL("BDDefensoria")
        End Function

        ''' <summary>
        ''' Permite consultar los documentos asociados a un radicado
        ''' </summary>
        ''' <param name="codigoSolicitud">Código único de un radicado</param>
        ''' <returns>Lista con la información de los registro de un catalogo asociados a un radicado</returns>
        Public Shared Function ConsultarListaCatalogoPorCodigoSolicitud(ByVal codigoSolicitud As Integer) As List(Of CatalogoDTO)
            Dim strError As String = Nothing
            Dim listaCatalogo As New List(Of CatalogoDTO)()
            Dim dataSet As New DataSet()

            Try
                Dim sql As String = "spConsultarListaCatalogoPorCodigoSolicitud " & Utilidad.IntegerFormat(codigoSolicitud)

                If GetHelperSQL().Consulta(sql, dataSet, strError) Then
                    If dataSet.Tables.Count = 0 Or dataSet.Tables(0).Rows.Count = 0 Then
                        Dim nombreMetodo = System.Reflection.MethodBase.GetCurrentMethod().Name
                        LogWriter.WriteLog("CatalogoDB", nombreMetodo & ":" & "Query no return data.")
                    Else
                        listaCatalogo = MapearDatos(dataSet.Tables(0).Rows)
                    End If
                Else
                    Dim nombreMetodo = System.Reflection.MethodBase.GetCurrentMethod().Name
                    LogWriter.WriteLog("CatalogoDB", nombreMetodo & ":" & strError)
                End If

            Catch ex As Exception
                Dim nombreMetodo = System.Reflection.MethodBase.GetCurrentMethod().Name
                LogWriter.WriteLog("CatalogoDB -" & nombreMetodo, ex)
                Throw New Exception("CatalogoDB -" & nombreMetodo & "-" & ex.Message, ex)
            Finally
                dataSet = Nothing
            End Try

            Return listaCatalogo
        End Function

        ''' <summary>
        ''' Permite mapear los datos del catálogo consultados
        ''' </summary>
        ''' <param name="filas">Filas a mapear</param>
        ''' <returns>Lista de un catálogo mapeados</returns>
        Private Shared Function MapearDatos(ByRef filas As DataRowCollection) As List(Of CatalogoDTO)

            Dim listaCatalogo As New List(Of CatalogoDTO)()

            For i As Integer = 0 To filas.Count - 1

                Dim catalogo = New CatalogoDTO()

                catalogo.Codigo = Utilidad.ParseString(filas(i).Item("Codigo"))
                catalogo.Nombre = Utilidad.ParseInteger(filas(i).Item("Nombre"))

                listaCatalogo.Add(catalogo)
            Next

            Return listaCatalogo
        End Function

    End Class
End Namespace