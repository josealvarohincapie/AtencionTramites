IF OBJECT_ID('spInsClasificacionPeticion') IS NOT NULL
      BEGIN
            DROP Procedure spInsClasificacionPeticion
      END
GO

/*===============================================================================================================				 
* Sistema  : Defensoría del Pueblo - Ultimus
* Archivo  : spInsClasificacionPeticion.sql
* Autor	   : José Álvaro Hincapié Castillo
* 
* Fecha      Responsable        Comentarios
* ==============================================================================================================
* 22/02/2024 José A Hincapié	Permite insertar un registro de la clasificaciónde la petición
* =============================================================================================================== */
CREATE PROCEDURE [dbo].[spInsClasificacionPeticion]
 @CodigoSolicitud bigint,
 @CodigoTipoPeticion int,
 @CodigoAreaDerecho int,     
 @DescripcionAsesoria VARCHAR(40),
 @Observaciones VARCHAR(40),
 @RespuestaEscrito bit,    
 @CodigoConclusionAsesoria int,
 @FechaCreacion datetime,    
 @NombreUsuarioCreacion VARCHAR(250),
 @IDUsuarioCreacion VARCHAR(250) 
AS    
BEGIN
	BEGIN TRY    
	
		INSERT INTO ClasificacionPeticion(CodigoSolicitud
			,[CodigoTipoPeticion]
			,[CodigoAreaDerecho]
			,[DescripcionAsesoria]
			,[Observaciones]
			,[RespuestaEscrito]
			,[CodigoConclusionAsesoria]
			,[FechaCreacion]
			,[NombreUsuarioCreacion]
			,[IDUsuarioCreacion]
		) VALUES (
			@CodigoSolicitud,
			@CodigoTipoPeticion,
			@CodigoAreaDerecho,     
			@DescripcionAsesoria,
			@Observaciones,
			@RespuestaEscrito,    
			@CodigoConclusionAsesoria,
			@FechaCreacion,    
			@NombreUsuarioCreacion,
			@IDUsuarioCreacion
		)    
	END TRY    
	BEGIN CATCH    
		RAISERROR ('Ocurrio un error al insertar el registro en la tabla ClasificacionPeticion', 15, 3)    
		RETURN    
	END CATCH    
END

GO

IF OBJECT_ID('spModClasificacionPeticion') IS NOT NULL
      BEGIN
            DROP Procedure spModClasificacionPeticion
      END
GO

/*===============================================================================================================				 
* Sistema  : Defensoría del Pueblo - Ultimus
* Archivo  : spModClasificacionPeticion.sql
* Autor	   : José Álvaro Hincapié Castillo
* 
* Fecha      Responsable        Comentarios
* ==============================================================================================================
* 22/02/2024 José A Hincapié	Permite modificar un registro de la clasificaciónde la petición
* =============================================================================================================== */
CREATE PROCEDURE [dbo].[spModClasificacionPeticion]
 @CodigoClasificacion bigint,
 @CodigoSolicitud bigint,
 @CodigoTipoPeticion int,
 @CodigoAreaDerecho int,     
 @DescripcionAsesoria VARCHAR(40),
 @Observaciones VARCHAR(40),
 @RespuestaEscrito bit,    
 @CodigoConclusionAsesoria int,
 @FechaUsuarioModifica datetime,    
 @NombreUsuarioModifica VARCHAR(250) 
AS    
BEGIN
	BEGIN TRY    
	
		UPDATE [dbo].[ClasificacionPeticion]
		SET 
			[CodigoSolicitud] = @CodigoSolicitud
			,[CodigoTipoPeticion] = @CodigoTipoPeticion
			,[CodigoAreaDerecho] = @CodigoAreaDerecho
			,[DescripcionAsesoria] = @DescripcionAsesoria
			,[Observaciones] = @Observaciones
			,[RespuestaEscrito] = @RespuestaEscrito
			,[CodigoConclusionAsesoria] = @CodigoConclusionAsesoria
			,[FechaUsuarioModifica] = @FechaUsuarioModifica
			,[NombreUsuarioModifica] = @NombreUsuarioModifica		
		WHERE CodigoClasificacion = @CodigoClasificacion
	END TRY    
	BEGIN CATCH    
		RAISERROR ('Ocurrio un error al modificar el registro en la tabla ClasificacionPeticion', 15, 3)    
		RETURN    
	END CATCH    
END

GO

IF OBJECT_ID('spConClasificacionPeticion') IS NOT NULL
      BEGIN
            DROP Procedure spConClasificacionPeticion
      END
GO

/*===============================================================================================================				 
* Sistema  : Defensoría del Pueblo - Ultimus
* Archivo  : spConClasificacionPeticion.sql
* Autor	   : José Álvaro Hincapié Castillo
* 
* Fecha      Responsable        Comentarios
* ==============================================================================================================
* 22/02/2024 José A Hincapié	Permite consultar un registro de la clasificaciónde la petición por codigoClasificacion
* =============================================================================================================== */
CREATE PROCEDURE [dbo].[spConClasificacionPeticion]
 @CodigoClasificacion bigint
AS    
BEGIN
	SELECT [CodigoClasificacion]
      ,[CodigoSolicitud]
      ,[CodigoTipoPeticion]
      ,[CodigoAreaDerecho]
	  , ad.Nombre NombreAreaDerecho
      ,[DescripcionAsesoria]
      ,[Observaciones]
      ,[RespuestaEscrito]
      ,[CodigoConclusionAsesoria]
      ,[FechaCreacion]
      ,[NombreUsuarioCreacion]
      ,[IDUsuarioCreacion]
      ,[FechaUsuarioModifica]
      ,[NombreUsuarioModifica]
	FROM [dbo].[ClasificacionPeticion]		
	INNER JOIN Catalogo.AreaDerecho ad on ad.Codigo = CodigoAreaDerecho and ad.Habilitado = 1
	INNER JOIN Catalogo.TipoPeticion tp on tp.Codigo = CodigoTipoPeticion and tp.Habilitado = 1
	INNER JOIN Catalogo.ConclusionAsesoria ca on ca.Codigo = CodigoConclusionAsesoria and ca.Habilitado = 1
	WHERE CodigoClasificacion = @CodigoClasificacion	   
END

GO


IF OBJECT_ID('spConsultarDatosRadicadoPorCodigo') IS NOT NULL
      BEGIN
            DROP Procedure [spConsultarDatosRadicadoPorCodigo]
      END
GO

/*===============================================================================================================				 
* Sistema  : Defensoría del Pueblo - Ultimus
* Archivo  : [spConsultarDatosRadicadoPorCodigo].sql
* Autor	   : José Álvaro Hincapié Castillo
* 
* Fecha      Responsable        Comentarios
* ==============================================================================================================
* 13/02/2024 José A Hincapié	Permite consultar los datos de un radicado a partir del código del radicado
* =============================================================================================================== */
CREATE PROCEDURE [dbo].[spConsultarDatosRadicadoPorCodigo]
@codigoSolicitud bigint
AS
	BEGIN
		select 
            rad.CodigoSolicitud, tt.Codigo CodigoTipoTramite, tt.Nombre NombreTipoTramite
            , fue.Codigo CodigoFuente,fue.Nombre NombreFuente
            , rad.Incidente
            , rad.NumeroRadicado, rad.Fecha, rad.Remitente
            , rad.Asunto, rad.Direccion, rad.Telefono
            , rad.CodigoEntidad, rad.NombreEntidad, rad.Correo
            , rad.Folios, rad.Anexos, rad.EsUrgente            
            , td.Codigo CodigoTipoDocumento, td.Nombre NombreTipoDocumento
            , std.Codigo CodigoSubTipoDocumento, std.Nombre NombreSubTipoDocumento
            , ge.Codigo CodigoGrupoEtnico, tt.Nombre NombreGrupoEtnico 
            , sd.Codigo CodigoSituacionDiscapacidad, tt.Nombre NombreSituacionDiscapacidad 
            , sep.Codigo CodigoSujetoEspecialProteccion, sep.Nombre NombreSujetoEspecialProteccion
            , ec.Codigo CodigoEstadoCivil, ec.Nombre NombreEstadoCivil
            , ne.Codigo CodigoNivelEstudio, ne.Nombre NombreNivelEstudio
            , rad.Discapacidad, rad.NumeroDocumentoIdentificacion
            , rad.GrupoEtnicoReconoce
            , gen.Codigo CodigoGenero, gen.Nombre NombreGenero 
            , sex.Codigo CodigoSexo, sex.Nombre NombreSexo
            , os.Codigo CodigoOrientacionSexual, os.Nombre NombreOrientacionSexual
            , pro.Codigo CodigoProcedencia, pro.Nombre NombreProcedencia
            , re.Codigo CodigoRangoEdad, re.Nombre NombreRangoEdad 
            , ts.Codigo CodigoTipoSolicitante, ts.NombreTipoSolicitante 
            , rad.EsAnonimo
            , tdi.Codigo CodigoTipoDocId, tdi.Nombre NombreTipoDocId
            , pai.Codigo CodigoPais, pai.NombrePais NombrePais 
            , dep.Codigo CodigoDpto, dep.Nombre NombreDpto
            , ciu.Codigo CodigoCiudad, ciu.Nombre NombreCiudad
            , mr.Codigo CodigoMedioRespuesta, mr.Nombre NombreMedioRespuesta
            , tp.Codigo CodigoTipoPqrs, tp.Nombre NombreTipoPqrs
            , rad.Resumen, rad.DescripcionHechos, rad.DescripcionSolicitud
            , dh.Codigo CodigoDptoHechos, dh.Nombre NombreDptoHechos
            , mh.Codigo CodigoMunicipioHechos, mh.Nombre NombreMunicipioHechos
            , fo.Codigo CodigoFormato, fo.Nombre NombreFormato
            , rad.Observaciones
			-- Campos nuevos
			, ca.Codigo CodigoCanalAtencion, ca.Nombre NombreCanalAtencion
			, eg.Codigo CodigoExpresionGenero, ca.Nombre NombreExpresionGenero
            from Radicado rad
            LEFT JOIN Catalogo.TipoTramite tt ON rad.CodigoTipoTramite = tt.Codigo and tt.Habilitado = 1
            LEFT JOIN Catalogo.Fuente fue ON rad.CodigoFuente = fue.Codigo and fue.Habilitado = 1
            LEFT JOIN Catalogo.Genero gen ON rad.CodigoGenero = gen.Codigo and gen.Habilitado = 1
            LEFT JOIN Catalogo.TipoDocumento td ON rad.CodigoTipoDocumento = td.Codigo and td.Habilitado = 1
            LEFT JOIN Catalogo.SubTipoDocumento std ON rad.CodigoSubTipoDocumento = std.Codigo and std.Habilitado = 1
            LEFT JOIN Catalogo.GrupoEtnico ge ON rad.CodigoGrupoEtnico = ge.Codigo and ge.Habilitado = 1
            LEFT JOIN Catalogo.SituacionDiscapacidad sd ON rad.CodigoSituacionDiscapacidad = sd.Codigo and sd.Habilitado = 1
            LEFT JOIN Catalogo.SujetoEspecialProteccion sep ON rad.CodigoSujetoEspecialProteccion = fue.Codigo and fue.Habilitado = 1
            LEFT JOIN Catalogo.EstadoCivil ec ON rad.CodigoFuente = ec.Codigo and ec.Habilitado = 1
            LEFT JOIN Catalogo.NivelEstudios ne ON rad.CodigoFuente = ne.Codigo and ne.Habilitado = 1
            LEFT JOIN Catalogo.Sexo sex ON rad.CodigoFuente = sex.Codigo and sex.Habilitado = 1
            LEFT JOIN Catalogo.OrientacionSexual os ON rad.CodigoFuente = os.Codigo and os.Habilitado = 1
            LEFT JOIN Catalogo.Procedencia pro ON rad.CodigoFuente = pro.Codigo and pro.Habilitado = 1
            LEFT JOIN Catalogo.RangoEdad re ON rad.CodigoFuente = re.Codigo and re.Habilitado = 1
            LEFT JOIN Catalogo.TipoSolicitante ts ON rad.CodigoFuente = ts.Codigo and ts.Habilitado = 1
            LEFT JOIN Catalogo.TipoDocumentoIdentificacion tdi ON rad.CodigoFuente = tdi.Codigo and tdi.Habilitado = 1
            LEFT JOIN Catalogo.Pais pai ON rad.CodigoFuente = pai.Codigo and pai.Habilitado = 1
            LEFT JOIN Catalogo.Departamento dep ON rad.CodigoFuente = dep.Codigo and dep.Habilitado = 1
            LEFT JOIN Catalogo.Ciudad ciu ON rad.CodigoFuente = ciu.Codigo and ciu.Habilitado = 1
            LEFT JOIN Catalogo.MedioRespuesta mr ON rad.CodigoFuente = mr.Codigo and mr.Habilitado = 1
            LEFT JOIN Catalogo.TipoPqrs tp ON rad.CodigoFuente = tp.Codigo and tp.Habilitado = 1
            LEFT JOIN Catalogo.Departamento dh ON rad.CodigoFuente = dh.Codigo and dh.Habilitado = 1
            LEFT JOIN Catalogo.Ciudad mh ON rad.CodigoFuente = mh.Codigo and mh.Habilitado = 1
            LEFT JOIN Catalogo.Formato fo ON rad.CodigoFuente = fo.Codigo and fo.Habilitado = 1
			-- campos nuevos
			LEFT JOIN Catalogo.TipoTramite ca ON rad.CodigoTipoTramite = ca.Codigo and ca.Habilitado = 1
			LEFT JOIN Catalogo.TipoTramite eg ON rad.CodigoTipoTramite = eg.Codigo and eg.Habilitado = 1
            where CodigoSolicitud = @codigoSolicitud
	END

    GO

    IF OBJECT_ID('spConsultarDocumentosPorCodigoRadicado') IS NOT NULL
      BEGIN
            DROP Procedure spConsultarDocumentosPorCodigoRadicado
      END
GO


    /*===============================================================================================================				 
* Sistema  : Defensoría del Pueblo - Ultimus
* Archivo  : [spConsultarDocumentosPorCodigoRadicado].sql
* Autor	   : José Álvaro Hincapié Castillo
* 
* Fecha      Responsable        Comentarios
* ==============================================================================================================
* 13/02/2024 José A Hincapié	Permite consultar los documentos asociados a un radicado
* =============================================================================================================== */
create PROCEDURE [dbo].[spConsultarDocumentosPorCodigoRadicado]
@codigoSolicitud bigint
AS
BEGIN
	select 
        rad.CodigoSolicitud
        , rd.CodigoRadicadoDocumento, rd.TituloArchivo
        , rd.FechaCreacion, rd.NombreUsuarioCreacion
        , rd.RutaFisicaArchivo, rd.RutaVirtualArchivo
        , rd.TamanoArchivo            
    from Radicado rad
    INNER JOIN RadicadoDocumento rd ON rad.CodigoSolicitud = rd.CodigoSolicitud
    where rad.CodigoSolicitud = @codigoSolicitud
END

GO