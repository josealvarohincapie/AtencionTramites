Imports Datos
Imports LogWriterHelper
Imports Modelo.Modelo.dto

Namespace DB

    Public Class RadicadoDB

        Private Shared Function GetHelperSQL() As HelperSQL
            Return New HelperSQL("BDDefensoria")
        End Function

        Public Shared Function ConsultarDatosRadicadoPorCodigo(ByVal codigoSolicitud As Integer) As RadicadoDTO
            Dim strError As String = Nothing
            Dim radicado As RadicadoDTO = Nothing
            Dim dataSet As New DataSet()

            Try
                Dim sql As String = "spConsultarDatosRadicadoPorCodigo " & Utilidad.IntegerFormat(codigoSolicitud)

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
            radicado.CodigoSolicitud = Utilidad.ParseInteger(row("CodigoSolicitud"))

            If IsDBNull(row("CodigoTipoTramite")) Then
                radicado.TipoTramite = Nothing
            Else
                radicado.TipoTramite = New CatalogoDTO()
                radicado.TipoTramite.Codigo = Utilidad.ParseString(row("CodigoTipoTramite"))
                radicado.TipoTramite.Nombre = Utilidad.ParseString(row("NombreTipoTramite"))
            End If

            If IsDBNull(row("CodigoFuente")) Then
                radicado.Fuente = Nothing
            Else
                radicado.Fuente = New CatalogoDTO()
                radicado.Fuente.Codigo = Utilidad.ParseString(row("CodigoFuente"))
                radicado.Fuente.Nombre = Utilidad.ParseString(row("NombreFuente"))
            End If

            radicado.Incidente = Utilidad.ParseInteger(row("Incidente"))

            If IsDBNull(row("NumeroRadicado")) Then
                radicado.NumeroRadicado = ""
            Else
                radicado.NumeroRadicado = Utilidad.ParseString(row("NumeroRadicado"))
            End If

            If IsDBNull(row("Fecha")) Then
                radicado.Fecha = Nothing
            Else
                radicado.Fecha = Utilidad.ParseDate(row("Fecha"))
            End If

            radicado.Remitente = Utilidad.ParseString(row("Remitente"))
            radicado.Asunto = Utilidad.ParseString(row("Asunto"))
            radicado.Direccion = Utilidad.ParseString(row("Direccion"))
            radicado.Telefono = Utilidad.ParseString(row("Telefono"))
            radicado.Identificacion = Utilidad.ParseString(row("NumeroDocumentoIdentificacion"))

            If IsDBNull(row("CodigoEntidad")) Then
                radicado.Entidad = Nothing
            Else
                radicado.Entidad = New CatalogoDTO()
                radicado.Entidad.Codigo = Utilidad.ParseString(row("CodigoEntidad"))
                radicado.Entidad.Nombre = Utilidad.ParseString(row("NombreEntidad"))
            End If

            radicado.Correo = Utilidad.ParseString(row("Correo"))
            radicado.Folios = Utilidad.ParseInteger(row("Folios"))
            radicado.Anexos = Utilidad.ParseString(row("Anexos"))
            radicado.EsUrgente = Utilidad.ParseBoolean(row("EsUrgente"))

            If IsDBNull(row("CodigoTipoDocumento")) Then
                radicado.TipoDocumento = Nothing
            Else
                radicado.TipoDocumento = New CatalogoDTO()
                radicado.TipoDocumento.Codigo = Utilidad.ParseString(row("CodigoTipoDocumento"))
                radicado.TipoDocumento.Nombre = Utilidad.ParseString(row("NombreTipoDocumento"))
            End If

            If IsDBNull(row("CodigoSubTipoDocumento")) Then
                radicado.SubTipoDocumento = Nothing
            Else
                radicado.SubTipoDocumento = New CatalogoDTO()
                radicado.SubTipoDocumento.Codigo = Utilidad.ParseString(row("CodigoSubTipoDocumento"))
                radicado.SubTipoDocumento.Nombre = Utilidad.ParseString(row("NombreSubTipoDocumento"))
            End If

            If IsDBNull(row("CodigoGrupoEtnico")) Then
                radicado.GrupoEtnico = Nothing
            Else
                radicado.GrupoEtnico = New CatalogoDTO()
                radicado.GrupoEtnico.Codigo = Utilidad.ParseString(row("CodigoGrupoEtnico"))
                radicado.GrupoEtnico.Nombre = Utilidad.ParseString(row("NombreGrupoEtnico"))
            End If

            If IsDBNull(row("CodigoSituacionDiscapacidad")) Then
                radicado.SituacionDiscapacidad = Nothing
            Else
                radicado.SituacionDiscapacidad = New CatalogoDTO()
                radicado.SituacionDiscapacidad.Codigo = Utilidad.ParseString(row("CodigoSituacionDiscapacidad"))
                radicado.SituacionDiscapacidad.Nombre = Utilidad.ParseString(row("NombreSituacionDiscapacidad"))
            End If

            If IsDBNull(row("CodigoSujetoEspecialProteccion")) Then
                radicado.SujetoEspecialProteccion = Nothing
            Else
                radicado.SujetoEspecialProteccion = New CatalogoDTO()
                radicado.SujetoEspecialProteccion.Codigo = Utilidad.ParseString(row("CodigoSujetoEspecialProteccion"))
                radicado.SujetoEspecialProteccion.Nombre = Utilidad.ParseString(row("NombreSujetoEspecialProteccion"))
            End If

            If IsDBNull(row("CodigoEstadoCivil")) Then
                radicado.EstadoCivil = Nothing
            Else
                radicado.EstadoCivil = New CatalogoDTO()
                radicado.EstadoCivil.Codigo = Utilidad.ParseString(row("CodigoEstadoCivil"))
                radicado.EstadoCivil.Nombre = Utilidad.ParseString(row("NombreEstadoCivil"))
            End If

            If IsDBNull(row("CodigoNivelEstudio")) Then
                radicado.NivelEstudio = Nothing
            Else
                radicado.NivelEstudio = New CatalogoDTO()
                radicado.NivelEstudio.Codigo = Utilidad.ParseString(row("CodigoNivelEstudio"))
                radicado.NivelEstudio.Nombre = Utilidad.ParseString(row("NombreNivelEstudio"))
            End If

            radicado.Discapacidad = Utilidad.ParseBoolean(row("Discapacidad"))
            radicado.GrupoEtnicoReconoce = Utilidad.ParseBoolean(row("GrupoEtnicoReconoce"))

            If IsDBNull(row("CodigoGenero")) Then
                radicado.Genero = Nothing
            Else
                radicado.Genero = New CatalogoDTO()
                radicado.Genero.Codigo = Utilidad.ParseString(row("CodigoGenero"))
                radicado.Genero.Nombre = Utilidad.ParseString(row("NombreGenero"))
            End If

            If IsDBNull(row("CodigoSexo")) Then
                radicado.Sexo = Nothing
            Else
                radicado.Sexo = New CatalogoDTO()
                radicado.Sexo.Codigo = Utilidad.ParseString(row("CodigoSexo"))
                radicado.Sexo.Nombre = Utilidad.ParseString(row("NombreSexo"))
            End If

            If IsDBNull(row("CodigoOrientacionSexual")) Then
                radicado.OrientacionSexual = Nothing
            Else
                radicado.OrientacionSexual = New CatalogoDTO()
                radicado.OrientacionSexual.Codigo = Utilidad.ParseString(row("CodigoOrientacionSexual"))
                radicado.OrientacionSexual.Nombre = Utilidad.ParseString(row("NombreOrientacionSexual"))
            End If

            If IsDBNull(row("CodigoProcedencia")) Then
                radicado.Procedencia = Nothing
            Else
                radicado.Procedencia = New CatalogoDTO()
                radicado.Procedencia.Codigo = Utilidad.ParseString(row("CodigoProcedencia"))
                radicado.Procedencia.Nombre = Utilidad.ParseString(row("NombreProcedencia"))
            End If

            If IsDBNull(row("CodigoRangoEdad")) Then
                radicado.RangoEdad = Nothing
            Else
                radicado.RangoEdad = New CatalogoDTO()
                radicado.RangoEdad.Codigo = Utilidad.ParseString(row("CodigoRangoEdad"))
                radicado.RangoEdad.Nombre = Utilidad.ParseString(row("NombreRangoEdad"))
            End If

            If IsDBNull(row("CodigoTipoSolicitante")) Then
                radicado.TipoSolicitante = Nothing
            Else
                radicado.TipoSolicitante = New CatalogoDTO()
                radicado.TipoSolicitante.Codigo = Utilidad.ParseString(row("CodigoTipoSolicitante"))
                radicado.TipoSolicitante.Nombre = Utilidad.ParseString(row("NombreTipoSolicitante"))
            End If

            radicado.EsAnonimo = Utilidad.ParseBoolean(row("EsAnonimo"))

            If IsDBNull(row("CodigoTipoDocId")) Then
                radicado.TipoDocId = Nothing
            Else
                radicado.TipoDocId = New CatalogoDTO()
                radicado.TipoDocId.Codigo = Utilidad.ParseString(row("CodigoTipoDocId"))
                radicado.TipoDocId.Nombre = Utilidad.ParseString(row("NombreTipoDocId"))
            End If

            If IsDBNull(row("CodigoPais")) Then
                radicado.Pais = Nothing
            Else
                radicado.Pais = New CatalogoDTO()
                radicado.Pais.Codigo = Utilidad.ParseString(row("CodigoPais"))
                radicado.Pais.Nombre = Utilidad.ParseString(row("NombrePais"))
            End If

            If IsDBNull(row("CodigoDpto")) Then
                radicado.TipoTramite = Nothing
            Else
                radicado.Dpto = New CatalogoDTO()
                radicado.Dpto.Codigo = Utilidad.ParseString(row("CodigoDpto"))
                radicado.Dpto.Nombre = Utilidad.ParseString(row("NombreDpto"))
            End If

            If IsDBNull(row("CodigoCiudad")) Then
                radicado.Ciudad = Nothing
            Else
                radicado.Ciudad = New CatalogoDTO()
                radicado.Ciudad.Codigo = Utilidad.ParseString(row("CodigoCiudad"))
                radicado.Ciudad.Nombre = Utilidad.ParseString(row("NombreCiudad"))
            End If

            If IsDBNull(row("CodigoMedioRespuesta")) Then
                radicado.MedioRespuesta = Nothing
            Else
                radicado.MedioRespuesta = New CatalogoDTO()
                radicado.MedioRespuesta.Codigo = Utilidad.ParseString(row("CodigoMedioRespuesta"))
                radicado.MedioRespuesta.Nombre = Utilidad.ParseString(row("NombreMedioRespuesta"))
            End If

            If IsDBNull(row("CodigoTipoPqrs")) Then
                radicado.TipoPqrs = Nothing
            Else
                radicado.TipoPqrs = New CatalogoDTO()
                radicado.TipoPqrs.Codigo = Utilidad.ParseString(row("CodigoTipoPqrs"))
                radicado.TipoPqrs.Nombre = Utilidad.ParseString(row("NombreTipoPqrs"))
            End If

            radicado.Resumen = Utilidad.ParseString(row("Resumen"))
            radicado.DescripcionHechos = Utilidad.ParseString(row("DescripcionHechos"))

            If IsDBNull(row("CodigoDptoHechos")) Then
                radicado.DptoHechos = Nothing
            Else
                radicado.DptoHechos = New CatalogoDTO()
                radicado.DptoHechos.Codigo = Utilidad.ParseString(row("CodigoDptoHechos"))
                radicado.DptoHechos.Nombre = Utilidad.ParseString(row("NombreDptoHechos"))
            End If

            If IsDBNull(row("CodigoMunicipioHechos")) Then
                radicado.MunicipioHechos = Nothing
            Else
                radicado.MunicipioHechos = New CatalogoDTO()
                radicado.MunicipioHechos.Codigo = Utilidad.ParseString(row("CodigoMunicipioHechos"))
                radicado.MunicipioHechos.Nombre = Utilidad.ParseString(row("NombreMunicipioHechos"))
            End If

            If IsDBNull(row("CodigoFormato")) Then
                radicado.Formato = Nothing
            Else
                radicado.Formato = New CatalogoDTO()
                radicado.Formato.Codigo = Utilidad.ParseString(row("CodigoFormato"))
                radicado.Formato.Nombre = Utilidad.ParseString(row("NombreFormato"))
            End If

            radicado.Observaciones = Utilidad.ParseString(row("Observaciones"))

            If IsDBNull(row("CodigoCanalAtencion")) Then
                radicado.CanalAtencion = Nothing
            Else
                radicado.CanalAtencion = New CatalogoDTO()
                radicado.CanalAtencion.Codigo = Utilidad.ParseString(row("CodigoCanalAtencion"))
                radicado.CanalAtencion.Nombre = Utilidad.ParseString(row("NombreCanalAtencion"))
            End If

            If IsDBNull(row("CodigoExpresionGenero")) Then
                radicado.ExpresionGenero = Nothing
            Else
                radicado.ExpresionGenero = New CatalogoDTO()
                radicado.ExpresionGenero.Codigo = Utilidad.ParseString(row("CodigoExpresionGenero"))
                radicado.ExpresionGenero.Nombre = Utilidad.ParseString(row("NombreExpresionGenero"))
            End If

            Return radicado
        End Function

    End Class
End Namespace