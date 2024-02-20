Imports Datos
Imports LogWriterHelper
Imports Web.Modelo.dto

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
            Dim sql As String = "spConsultarDocumentosPorCodigoRadicado " & IntegerFormat(codigoSolicitud)

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

            documento.CodigoRadicadoDocumento = ParseString(filas(i).Item("CodigoRadicadoDocumento"))
            documento.CodigoSolicitud = ParseInteger(filas(i).Item("CodigoSolicitud"))
            documento.TituloArchivo = ParseString(filas(i).Item("TituloArchivo"))
            documento.FechaCreacion = ParseDate(filas(i).Item("FechaCreacion"))
            documento.NombreUsuarioCreacion = ParseString(filas(i).Item("NombreUsuarioCreacion"))
            documento.RutaFisicaArchivo = ParseString(filas(i).Item("RutaFisicaArchivo"))
            documento.RutaVirtualArchivo = ParseString(filas(i).Item("RutaVirtualArchivo"))
            documento.TamanoArchivo = ParseDecimal(filas(i).Item("TamanoArchivo"))

            listaDocumentos.Add(documento)
        Next

        Return listaDocumentos
    End Function

    Public Shared Function ParseInteger(ByVal o As Object) As Integer
        If IsDBNull(o) Or Not TypeOf o Is Integer Then
            Return 0
        Else
            Return DirectCast(o, Integer)
        End If
    End Function

    Public Shared Function ParseString(ByVal o As Object) As String
        If IsDBNull(o) Or Not TypeOf o Is String Then
            Return ""
        Else
            Return DirectCast(o, String)
        End If
    End Function

    Public Shared Function ParseBoolean(ByVal o As Object) As Boolean
        If IsDBNull(o) Or Not TypeOf o Is Boolean Then
            Return False
        Else
            Return DirectCast(o, Boolean)
        End If
    End Function

    Public Shared Function ParseDecimal(ByVal o As Object) As Decimal
        If IsDBNull(o) Or Not TypeOf o Is Decimal Then
            Return 0
        Else
            Return DirectCast(o, Decimal)
        End If
    End Function

    Public Shared Function ParseDate(ByVal o As Object) As Date
        If IsDBNull(o) Or Not TypeOf o Is Date Then
            Return Nothing
        Else
            Return DirectCast(o, Date)
        End If
    End Function

    Public Shared Function ParseDateTime(ByVal o As Object) As DateTime
        If IsDBNull(o) Or Not TypeOf o Is DateTime Then
            Return Nothing
        Else
            Return DirectCast(o, DateTime)
        End If
    End Function

    Public Shared Function ParseLong(ByVal o As Object) As Long
        If IsDBNull(o) Or Not TypeOf o Is Long Then
            Return Nothing
        Else
            Return DirectCast(o, Long)
        End If
    End Function



    Private Shared Function IntegerFormat(ByVal i As Integer) As String
        Return i.ToString()
    End Function

    Private Shared Function StringFormat(ByVal s As String) As String
        If s Is Nothing Then
            Return "null"
        Else
            Return "'" + s + "'"
        End If
    End Function

    Private Shared Function BooleanFormat(ByVal b As Boolean) As String
        Return IIf(b, "1", "0")
    End Function

    Private Shared Function DecimalFormat(ByVal d As Decimal) As String
        Return d.ToString().Replace(",", ".")
    End Function

    Private Shared Function DateFormat(ByVal d As Date) As String
        If d = Date.MinValue Then
            Return "null"
        Else
            Return "'" + d.ToString("yyyy-MM-dd") + "'"
        End If
    End Function


    Public Shared Function GetConfigDocuments(ByVal Identificacion As String, ByVal Avalista As Boolean, ByVal razonSocial As String, ByVal participacionAccionaria As Boolean, ByVal tipoProducto As Integer, ByVal tipoPersona As Integer) As List(Of Documento)
        Dim strError As String = Nothing
        Dim d As New List(Of Documento)
        Dim dataSet As New DataSet()

        Try
            Dim sql As String = "PA_AdminCatalogo_V2 9,null,null,null,null,null," & IIf(participacionAccionaria, 1, 0) & ",2,'" & tipoProducto & "'," & IIf(Avalista, "0,1", "1,0") + "," + tipoPersona.ToString()

            If GetHelperSQL().Consulta(sql, dataSet, strError) Then
                If dataSet.Tables.Count = 0 Then
                    LogWriter.WriteLog("DBMethods", "Query no return data.")
                Else
                    Dim i As Integer = 0
                    For Each row As DataRow In dataSet.Tables(0).Rows
                        i = i + 1
                        d.Add(ParseConfigDocument(row, i, Identificacion, razonSocial))
                    Next
                End If
            Else
                LogWriter.WriteLog("DBMethods", "Consulta: " & strError)
            End If

        Catch ex As Exception
            LogWriter.WriteLog("DBMethods", ex)
        Finally
            dataSet = Nothing
        End Try

        Return d
    End Function


    Private Shared Function ParseConfigDocument(ByRef row As DataRow, ByVal index As Integer, ByVal Identificacion As String, ByVal razonSocial As String) As Documento
        Dim d As New Documento()
        d.Identificacion = Identificacion
        d.Codigo = ParseString(row("CODIGO"))
        d.Registro = index.ToString()
        d.Descripcion = ParseString(row("DESCRIPCION"))
        d.HasFile = False
        d.Archivo = Nothing
        d.Path = Nothing
        d.Url = Nothing
        d.IsExternal = False
        d.FechaEmision = Nothing
        d.DiasVencimiento = 0
        d.Observaciones = String.Empty
        d.RazonSocial = razonSocial
        d.Opcional = ParseBoolean(row("OPCIONAL"))
        Return d
    End Function

    Public Shared Function GetAttachedDocuments(ByVal incident As Integer, ByVal Identificacion As String, ByVal razonSocial As String) As List(Of Documento)
        Dim strError As String = Nothing
        Dim d As New List(Of Documento)
        Dim dataSet As New DataSet()

        Try

            Dim sql As String = "PA_SelectDocumentacion " & IntegerFormat(incident)
            If Identificacion <> String.Empty Then sql &= ",'" & Identificacion & "'"

            If GetHelperSQL().Consulta(sql, dataSet, strError) Then
                If dataSet.Tables.Count = 0 Then
                    LogWriter.WriteLog("DBMethods", "Query no return data.")
                Else
                    Dim i As Integer
                    For Each row As DataRow In dataSet.Tables(0).Rows
                        i = i + 1
                        d.Add(ParseAttachedDocument(row, i, razonSocial))
                    Next
                End If
            Else
                LogWriter.WriteLog("DBMethods", "Consulta: " & strError)
            End If

        Catch ex As Exception
            LogWriter.WriteLog("DBMethods", ex)
        Finally
            dataSet = Nothing
        End Try

        Return d
    End Function

    Private Shared Function ParseAttachedDocument(ByRef row As DataRow, ByVal index As Integer, ByVal razonSocial As String) As Documento
        Dim d As New Documento()
        d.Identificacion = ParseString(row("IDENTIFICACION"))
        d.Codigo = ParseString(row("CODIGO"))
        d.Registro = index.ToString()
        d.Descripcion = ParseString(row("DESCRIPCION"))
        d.HasFile = ParseBoolean(row("HAS_FILE"))
        d.Archivo = Nothing
        d.Path = Nothing
        d.Url = ParseString(row("URL"))
        d.IsExternal = ParseBoolean(row("IS_EXTERNAL"))
        d.FechaEmision = ParseDate(row("FECHA_EMISION"))
        d.DiasVencimiento = 0
        d.Observaciones = ParseString(row("OBSERVACIONES"))
        d.RazonSocial = razonSocial 'ParseString(row("RAZON_SOCIAL"))
        d.Opcional = ParseBoolean(row("OPCIONAL"))

        Return d
    End Function

    Public Shared Function DeleteDocuments(ByVal incident As Integer, ByVal Identificacion As String) As Boolean
        Dim strError As String = Nothing
        Try
            Dim sql As String = "PA_DeleteDocumentacion " & IntegerFormat(incident) & ",'" & Identificacion & "'"

            If Not GetHelperSQL().EjecutaAccion(sql, strError) Then
                LogWriter.WriteLog("DBMethods", strError)
                Return False
            End If

            Return True
        Catch ex As Exception
            LogWriter.WriteLog("DBMethods", ex)
        End Try
    End Function

    Public Shared Function SaveDocument(ByVal incidente As Integer, ByRef d As Documento) As Boolean
        Dim strError As String = Nothing

        Try
            Dim sql As String = "PA_InsertDocument "
            sql += IntegerFormat(incidente) + ","
            sql += StringFormat(d.Identificacion) + ","
            sql += StringFormat(d.Codigo) + ","
            sql += StringFormat(d.Descripcion) + ","
            sql += BooleanFormat(d.HasFile) + ","
            sql += StringFormat(d.Url) + ","
            sql += BooleanFormat(d.IsExternal) + ","
            sql += DateFormat(d.FechaEmision) + ","
            sql += StringFormat(d.Observaciones) + ","
            sql += StringFormat(d.RazonSocial) + ","
            sql += BooleanFormat(d.Opcional)

            If Not GetHelperSQL().EjecutaAccion(sql, strError) Then
                LogWriter.WriteLog("DBMethods", strError)
                Return False
            End If

            Return True
        Catch ex As Exception
            LogWriter.WriteLog("DBMethods", ex)
            Return False
        End Try

    End Function

    Private Shared Function ParseComplementario(ByRef row As DataRow) As Complementario
        Dim d As New Complementario()

        d.IdTipoIdentificacion = ParseInteger(row("ID_TIPO_IDENTIFICACION"))
        d.TipoIdentificacion = ParseString(row("TIPO_IDENTIFICACION"))
        d.Identificacion = ParseString(row("IDENTIFICACION"))
        d.Nombre = ParseString(row("NOMBRE"))
        d.IdTipoRelacion = ParseInteger(row("ID_TIPO_RELACION"))
        d.TipoRelacion = ParseString(row("TIPO_RELACION"))
        d.Direccion = ParseString(row("DIRECCION"))
        d.Telefono = ParseString(row("TELEFONO"))
        d.Pais = ParseString(row("ID_PAIS"))
        d.Ciudad = ParseString(row("ID_CIUDAD"))
        d.Participacion = ParseBoolean(row("PARTICIPACIONACC"))
        d.IdActividad = ParseString(row("ID_ACTIVIDAD"))
        d.Correo = ParseString(row("CORREO"))
        d.TipoSemejanza = ParseInteger(row("SEMEJANZA"))
        d.UrlReporte = ParseString(row("REPORTE"))

        Return d
    End Function

    Public Shared Function DeleteComplementarios(ByVal incident As Integer, ByVal Identificacion As String) As Boolean
        Dim strError As String = Nothing
        Try
            Dim sql As String = "PA_DeleteComplementarios " & IntegerFormat(incident) & ",'" & Identificacion & "'"

            If Not GetHelperSQL().EjecutaAccion(sql, strError) Then
                LogWriter.WriteLog("DBMethods", strError)
                Return False
            End If

            Return True
        Catch ex As Exception
            LogWriter.WriteLog("DBMethods", ex)
        End Try
    End Function


    Public Shared Function SaveComplementario(ByVal incidente As Integer, ByRef r As Complementario) As Boolean
        Dim strError As String = Nothing

        Try
            Dim sql As String = "PA_InsertComplementario "
            sql += IntegerFormat(incidente) + ","
            sql += IntegerFormat(r.IdTipoIdentificacion) + ","
            sql += StringFormat(r.Identificacion) + ","
            sql += StringFormat(r.Nombre) + ","
            sql += IntegerFormat(r.IdTipoRelacion) + ","
            sql += StringFormat(r.Direccion) + ","
            sql += StringFormat(r.Telefono) + ","
            sql += StringFormat(r.IdActividad) + ","
            sql += StringFormat(r.Correo) + ","
            sql += IntegerFormat(r.TipoSemejanza) + ","
            sql += StringFormat(r.UrlReporte) + ","
            sql += StringFormat(r.Pais) + ","
            sql += StringFormat(r.Ciudad) + ","
            sql += BooleanFormat(r.Participacion)

            If Not GetHelperSQL().EjecutaAccion(sql, strError) Then
                LogWriter.WriteLog("DBMethods", strError)
                Return False
            End If

            Return True
        Catch ex As Exception
            LogWriter.WriteLog("DBMethods", ex)
            Return False
        End Try

    End Function

    Public Shared Function GetComplementarios(ByVal incident As Integer) As List(Of Complementario)
        Dim strError As String = Nothing
        Dim c As New List(Of Complementario)
        Dim dataSet As New DataSet()

        Try
            Dim sql As String = "PA_SelectComplementarios " & IntegerFormat(incident)

            If GetHelperSQL().Consulta(sql, dataSet, strError) Then
                If dataSet.Tables.Count = 0 Then
                    LogWriter.WriteLog("DBMethods", "Query no return data.")
                Else
                    For Each row As DataRow In dataSet.Tables(0).Rows
                        c.Add(ParseComplementario(row))
                    Next
                End If
            Else
                LogWriter.WriteLog("DBMethods", "Consulta: " & strError)
            End If

        Catch ex As Exception
            LogWriter.WriteLog("DBMethods", ex)
        Finally
            dataSet = Nothing
        End Try

        Return c
    End Function

    Public Shared Function GetDocumentosAdicionales(ByVal incident As Integer) As List(Of String)
        Dim strError As String = Nothing
        Dim s As New List(Of String)
        Dim dataSet As New DataSet()

        Try
            Dim sql As String = "PA_AdminAdjunto_V2 1," & IntegerFormat(incident)

            If GetHelperSQL().Consulta(sql, dataSet, strError) Then
                If dataSet.Tables.Count = 0 Then
                    LogWriter.WriteLog("DBMethods", "Query no return data.")
                Else
                    For Each row As DataRow In dataSet.Tables(0).Rows
                        s.Add(ParseString(row("ADJUNTO")))
                    Next
                End If
            Else
                LogWriter.WriteLog("DBMethods", "Consulta: " & strError)
            End If

        Catch ex As Exception
            LogWriter.WriteLog("DBMethods", ex)
        Finally
            dataSet = Nothing
        End Try

        Return s
    End Function

    Public Shared Function GetComunicacionRecibida(ByVal incident As Integer) As List(Of ComunicacionRecibida)
        Dim strError As String = Nothing
        Dim cr As New List(Of ComunicacionRecibida)
        Dim dataSet As New DataSet()

        Try
            Dim sql As String = "PA_SelectComunicacionRecibida " & IntegerFormat(incident)

            If GetHelperSQL().Consulta(sql, dataSet, strError) Then
                If dataSet.Tables.Count = 0 Or dataSet.Tables(0).Rows.Count = 0 Then
                    'LogWriter.WriteLog("DBMethods", "Query no return data.")
                Else
                    For Each row As DataRow In dataSet.Tables(0).Rows
                        cr.Add(ParseComunicacionRecibida(row))
                    Next
                End If
            Else
                LogWriter.WriteLog("DBMethods", "Consulta: " & strError)
            End If

        Catch ex As Exception
            LogWriter.WriteLog("DBMethods", ex)
        Finally
            dataSet = Nothing
        End Try

        Return cr
    End Function

    Private Shared Function ParseComunicacionRecibida(ByRef row As DataRow) As ComunicacionRecibida
        Dim cr As New ComunicacionRecibida()
        cr.Id = ParseLong(row("Id"))
        cr.Proceso = ParseString(row("Proceso"))
        cr.Incidente = ParseInteger(row("Incidente"))
        cr.Creado = ParseBoolean(row("Creado"))
        cr.FechaRadicacion = ParseDateTime(row("FechaRadicacion"))
        cr.NumeroRadicado = ParseString(row("NumeroRadicado"))
        cr.NumeroReferenciaRelacionada = ParseString(row("NumeroReferenciaRelacionada"))
        cr.TipoIdentificacion = ParseString(row("TipoIdentificacion"))
        cr.NumeroIdentificacion = ParseLong(row("NumeroIdentificacion"))
        cr.NombreRazonSocial = ParseString(row("NombreRazonSocial"))
        cr.Asunto = ParseInteger(row("Asunto"))
        cr.DescripcionAsunto = ParseString(row("DescripcionAsunto"))
        cr.FunsionarioAsignado = ParseString(row("FunsionarioAsignado"))
        cr.DocumentHandle = ParseLong(row("DocumentHandle"))
        cr.IdAnexos = ParseString(row("IdAnexos"))
        cr.NombreAnexos = ParseString(row("NombreAnexos"))
        cr.CantidadAnexos = ParseInteger(row("CantidadAnexos"))
        cr.MostrarAnexos = ParseBoolean(row("MostrarAnexos"))
        Return cr
    End Function

    Public Shared Function GetIncidenteComunicacionRecibida(ByVal incident As Integer) As IncidenteComunicacionRecibida
        Dim strError As String = Nothing
        Dim cr As New List(Of ComunicacionRecibida)
        Dim dataSet As New DataSet()

        Try
            Dim sql As String = "PA_SelectIncidenteComunicacionRecibida " & IntegerFormat(incident)

            If GetHelperSQL().Consulta(sql, dataSet, strError) Then
                If dataSet.Tables.Count = 0 Or dataSet.Tables(0).Rows.Count = 0 Then
                    Return Nothing
                Else
                    Return ParseIncidenteComunicacionRecibida(dataSet.Tables(0).Rows(0))
                End If
            Else
                LogWriter.WriteLog("DBMethods", "Consulta: " & strError)
            End If

        Catch ex As Exception
            LogWriter.WriteLog("DBMethods", ex)
        Finally
            dataSet = Nothing
        End Try
        Return Nothing
    End Function

    Private Shared Function ParseIncidenteComunicacionRecibida(ByRef row As DataRow) As IncidenteComunicacionRecibida
        Dim cr As New IncidenteComunicacionRecibida()
        cr.Proceso = ParseString(row("Proceso"))
        cr.Incidente = ParseInteger(row("Incidente"))
        cr.Resumen = ParseString(row("Resumen"))
        Return cr
    End Function

End Class
