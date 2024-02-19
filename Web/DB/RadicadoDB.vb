Imports Datos
Imports LogWriterHelper
Imports Web.Modelo.dto

Public Class RadicadoDB

    Private Shared Function GetHelperSQL() As HelperSQL
        Return New HelperSQL("BDDefensoria")
    End Function



    Public Shared Function ConsultarDatosRadicadoPorCodigo(ByVal codigoSolicitud As Integer) As RadicadoDTO
        Dim strError As String = Nothing
        Dim radicado As RadicadoDTO = Nothing
        Dim dataSet As New DataSet()

        Try
            Dim sql As String = "spConsultarDatosRadicadoPorCodigo " & IntegerFormat(codigoSolicitud)

            If GetHelperSQL().Consulta(sql, dataSet, strError) Then
                If dataSet.Tables.Count = 0 Or dataSet.Tables(0).Rows.Count = 0 Then
                    Dim nombreMetodo = System.Reflection.MethodBase.GetCurrentMethod().Name
                    LogWriter.WriteLog("RadicadoDB", nombreMetodo & ":" & "Query no return data.")
                Else
                    radicado = ParseSolicitud(dataSet.Tables(0).Rows(0))
                End If
            Else
                Dim nombreMetodo = System.Reflection.MethodBase.GetCurrentMethod().Name
                LogWriter.WriteLog("RadicadoDB", nombreMetodo & ":" & strError)
            End If

        Catch ex As Exception
            Dim nombreMetodo = System.Reflection.MethodBase.GetCurrentMethod().Name
            LogWriter.WriteLog("RadicadoDB -" & nombreMetodo, ex)

        Finally
            dataSet = Nothing
        End Try

        Return radicado
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="row"></param>
    ''' <returns></returns>
    Private Shared Function ParseSolicitud(ByRef row As DataRow) As RadicadoDTO
        Dim radicado As New RadicadoDTO()
        radicado.CodigoSolicitud = ParseInteger(row("CodigoSolicitud"))

        If IsDBNull(row("CodigoTipoTramite")) Then
            radicado.TipoTramite = Nothing
        Else
            radicado.TipoTramite = New CatalogoDTO()
            radicado.TipoTramite.Codigo = ParseString(row("CodigoTipoTramite"))
            radicado.TipoTramite.Nombre = ParseString(row("NombreTipoTramite"))
        End If

        If IsDBNull(row("CodigoFuente")) Then
            radicado.Fuente = Nothing
        Else
            radicado.Fuente = New CatalogoDTO()
            radicado.Fuente.Codigo = ParseString(row("CodigoFuente"))
            radicado.Fuente.Nombre = ParseString(row("NombreFuente"))
        End If

        radicado.Incidente = ParseInteger(row("Incidente"))

        If IsDBNull(row("NumeroRadicado")) Then
            radicado.NumeroRadicado = ""
        Else
            radicado.NumeroRadicado = ParseString(row("NumeroRadicado"))
        End If

        If IsDBNull(row("Fecha")) Then
            radicado.Fecha = Nothing
        Else
            radicado.Fecha = ParseDate(row("Fecha"))
        End If

        radicado.Remitente = ParseString(row("Remitente"))
        radicado.Asunto = ParseString(row("Asunto"))
        radicado.Direccion = ParseString(row("Direccion"))
        radicado.Telefono = ParseString(row("Telefono"))
        radicado.Identificacion = ParseString(row("NumeroDocumentoIdentificacion"))

        If IsDBNull(row("CodigoEntidad")) Then
            radicado.Entidad = Nothing
        Else
            radicado.Entidad = New CatalogoDTO()
            radicado.Entidad.Codigo = ParseString(row("CodigoEntidad"))
            radicado.Entidad.Nombre = ParseString(row("NombreEntidad"))
        End If

        radicado.Correo = ParseString(row("Correo"))
        radicado.Folios = ParseInteger(row("Folios"))
        radicado.Anexos = ParseString(row("Anexos"))
        radicado.EsUrgente = ParseBoolean(row("EsUrgente"))

        If IsDBNull(row("CodigoTipoDocumento")) Then
            radicado.TipoDocumento = Nothing
        Else
            radicado.TipoDocumento = New CatalogoDTO()
            radicado.TipoDocumento.Codigo = ParseString(row("CodigoTipoDocumento"))
            radicado.TipoDocumento.Nombre = ParseString(row("NombreTipoDocumento"))
        End If

        If IsDBNull(row("CodigoSubTipoDocumento")) Then
            radicado.SubTipoDocumento = Nothing
        Else
            radicado.SubTipoDocumento = New CatalogoDTO()
            radicado.SubTipoDocumento.Codigo = ParseString(row("CodigoSubTipoDocumento"))
            radicado.SubTipoDocumento.Nombre = ParseString(row("NombreSubTipoDocumento"))
        End If

        If IsDBNull(row("CodigoGrupoEtnico")) Then
            radicado.GrupoEtnico = Nothing
        Else
            radicado.GrupoEtnico = New CatalogoDTO()
            radicado.GrupoEtnico.Codigo = ParseString(row("CodigoGrupoEtnico"))
            radicado.GrupoEtnico.Nombre = ParseString(row("NombreGrupoEtnico"))
        End If

        If IsDBNull(row("CodigoSituacionDiscapacidad")) Then
            radicado.SituacionDiscapacidad = Nothing
        Else
            radicado.SituacionDiscapacidad = New CatalogoDTO()
            radicado.SituacionDiscapacidad.Codigo = ParseString(row("CodigoSituacionDiscapacidad"))
            radicado.SituacionDiscapacidad.Nombre = ParseString(row("NombreSituacionDiscapacidad"))
        End If

        If IsDBNull(row("CodigoSujetoEspecialProteccion")) Then
            radicado.SujetoEspecialProteccion = Nothing
        Else
            radicado.SujetoEspecialProteccion = New CatalogoDTO()
            radicado.SujetoEspecialProteccion.Codigo = ParseString(row("CodigoSujetoEspecialProteccion"))
            radicado.SujetoEspecialProteccion.Nombre = ParseString(row("NombreSujetoEspecialProteccion"))
        End If

        If IsDBNull(row("CodigoEstadoCivil")) Then
            radicado.EstadoCivil = Nothing
        Else
            radicado.EstadoCivil = New CatalogoDTO()
            radicado.EstadoCivil.Codigo = ParseString(row("CodigoEstadoCivil"))
            radicado.EstadoCivil.Nombre = ParseString(row("NombreEstadoCivil"))
        End If

        If IsDBNull(row("CodigoNivelEstudio")) Then
            radicado.NivelEstudio = Nothing
        Else
            radicado.NivelEstudio = New CatalogoDTO()
            radicado.NivelEstudio.Codigo = ParseString(row("CodigoNivelEstudio"))
            radicado.NivelEstudio.Nombre = ParseString(row("NombreNivelEstudio"))
        End If

        radicado.Discapacidad = ParseBoolean(row("Discapacidad"))
        radicado.GrupoEtnicoReconoce = ParseBoolean(row("GrupoEtnicoReconoce"))

        If IsDBNull(row("CodigoGenero")) Then
            radicado.Genero = Nothing
        Else
            radicado.Genero = New CatalogoDTO()
            radicado.Genero.Codigo = ParseString(row("CodigoGenero"))
            radicado.Genero.Nombre = ParseString(row("NombreGenero"))
        End If

        If IsDBNull(row("CodigoSexo")) Then
            radicado.Sexo = Nothing
        Else
            radicado.Sexo = New CatalogoDTO()
            radicado.Sexo.Codigo = ParseString(row("CodigoSexo"))
            radicado.Sexo.Nombre = ParseString(row("NombreSexo"))
        End If

        If IsDBNull(row("CodigoOrientacionSexual")) Then
            radicado.OrientacionSexual = Nothing
        Else
            radicado.OrientacionSexual = New CatalogoDTO()
            radicado.OrientacionSexual.Codigo = ParseString(row("CodigoOrientacionSexual"))
            radicado.OrientacionSexual.Nombre = ParseString(row("NombreOrientacionSexual"))
        End If

        If IsDBNull(row("CodigoProcedencia")) Then
            radicado.Procedencia = Nothing
        Else
            radicado.Procedencia = New CatalogoDTO()
            radicado.Procedencia.Codigo = ParseString(row("CodigoProcedencia"))
            radicado.Procedencia.Nombre = ParseString(row("NombreProcedencia"))
        End If

        If IsDBNull(row("CodigoRangoEdad")) Then
            radicado.RangoEdad = Nothing
        Else
            radicado.RangoEdad = New CatalogoDTO()
            radicado.RangoEdad.Codigo = ParseString(row("CodigoRangoEdad"))
            radicado.RangoEdad.Nombre = ParseString(row("NombreRangoEdad"))
        End If

        If IsDBNull(row("CodigoTipoSolicitante")) Then
            radicado.TipoSolicitante = Nothing
        Else
            radicado.TipoSolicitante = New CatalogoDTO()
            radicado.TipoSolicitante.Codigo = ParseString(row("CodigoTipoSolicitante"))
            radicado.TipoSolicitante.Nombre = ParseString(row("NombreTipoSolicitante"))
        End If

        radicado.EsAnonimo = ParseBoolean(row("EsAnonimo"))

        If IsDBNull(row("CodigoTipoDocId")) Then
            radicado.TipoDocId = Nothing
        Else
            radicado.TipoDocId = New CatalogoDTO()
            radicado.TipoDocId.Codigo = ParseString(row("CodigoTipoDocId"))
            radicado.TipoDocId.Nombre = ParseString(row("NombreTipoDocId"))
        End If

        If IsDBNull(row("CodigoPais")) Then
            radicado.Pais = Nothing
        Else
            radicado.Pais = New CatalogoDTO()
            radicado.Pais.Codigo = ParseString(row("CodigoPais"))
            radicado.Pais.Nombre = ParseString(row("NombrePais"))
        End If

        If IsDBNull(row("CodigoDpto")) Then
            radicado.TipoTramite = Nothing
        Else
            radicado.Dpto = New CatalogoDTO()
            radicado.Dpto.Codigo = ParseString(row("CodigoDpto"))
            radicado.Dpto.Nombre = ParseString(row("NombreDpto"))
        End If

        If IsDBNull(row("CodigoCiudad")) Then
            radicado.Ciudad = Nothing
        Else
            radicado.Ciudad = New CatalogoDTO()
            radicado.Ciudad.Codigo = ParseString(row("CodigoCiudad"))
            radicado.Ciudad.Nombre = ParseString(row("NombreCiudad"))
        End If

        If IsDBNull(row("CodigoMedioRespuesta")) Then
            radicado.MedioRespuesta = Nothing
        Else
            radicado.MedioRespuesta = New CatalogoDTO()
            radicado.MedioRespuesta.Codigo = ParseString(row("CodigoMedioRespuesta"))
            radicado.MedioRespuesta.Nombre = ParseString(row("NombreMedioRespuesta"))
        End If

        If IsDBNull(row("CodigoTipoPqrs")) Then
            radicado.TipoPqrs = Nothing
        Else
            radicado.TipoPqrs = New CatalogoDTO()
            radicado.TipoPqrs.Codigo = ParseString(row("CodigoTipoPqrs"))
            radicado.TipoPqrs.Nombre = ParseString(row("NombreTipoPqrs"))
        End If

        radicado.Resumen = ParseString(row("Resumen"))
        radicado.DescripcionHechos = ParseString(row("DescripcionHechos"))

        If IsDBNull(row("CodigoDptoHechos")) Then
            radicado.DptoHechos = Nothing
        Else
            radicado.DptoHechos = New CatalogoDTO()
            radicado.DptoHechos.Codigo = ParseString(row("CodigoDptoHechos"))
            radicado.DptoHechos.Nombre = ParseString(row("NombreDptoHechos"))
        End If

        If IsDBNull(row("CodigoMunicipioHechos")) Then
            radicado.MunicipioHechos = Nothing
        Else
            radicado.MunicipioHechos = New CatalogoDTO()
            radicado.MunicipioHechos.Codigo = ParseString(row("CodigoMunicipioHechos"))
            radicado.MunicipioHechos.Nombre = ParseString(row("NombreMunicipioHechos"))
        End If

        If IsDBNull(row("CodigoFormato")) Then
            radicado.Formato = Nothing
        Else
            radicado.Formato = New CatalogoDTO()
            radicado.Formato.Codigo = ParseString(row("CodigoFormato"))
            radicado.Formato.Nombre = ParseString(row("NombreFormato"))
        End If

        radicado.Observaciones = ParseString(row("Observaciones"))

        If IsDBNull(row("CodigoCanalAtencion")) Then
            radicado.CanalAtencion = Nothing
        Else
            radicado.CanalAtencion = New CatalogoDTO()
            radicado.CanalAtencion.Codigo = ParseString(row("CodigoCanalAtencion"))
            radicado.CanalAtencion.Nombre = ParseString(row("NombreCanalAtencion"))
        End If

        If IsDBNull(row("CodigoExpresionGenero")) Then
            radicado.ExpresionGenero = Nothing
        Else
            radicado.ExpresionGenero = New CatalogoDTO()
            radicado.ExpresionGenero.Codigo = ParseString(row("CodigoExpresionGenero"))
            radicado.ExpresionGenero.Nombre = ParseString(row("NombreExpresionGenero"))
        End If

        Return radicado
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
