﻿IF OBJECT_ID('spInsClasificacionPeticion') IS NOT NULL
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
* 22/02/2024 José A Hincapié	Permite insertar o modificar un registro de la clasificaciónde la petición
* =============================================================================================================== */
create or alter PROCEDURE [dbo].[spInsClasificacionPeticion]
 @CodigoSolicitud bigint,
 @CodigoTipoPeticion int,
 @CodigoAreaDerecho int,     
 @DescripcionAsesoria VARCHAR(2048),
 @Observaciones VARCHAR(2048),
 @RespuestaEscrito bit,    
 @CodigoConclusionAsesoria int,
 @NombreUsuarioCreacion VARCHAR(250),
 @IDUsuarioCreacion VARCHAR(250) 
AS    
BEGIN

	Declare @FechaCreacion datetime = GetDate()

    If exists(Select 1 from ClasificacionPeticion 
				where CodigoSolicitud = @CodigoSolicitud)
	Begin
		Update ClasificacionPeticion 
	    Set CodigoTipoPeticion = @CodigoTipoPeticion
            ,[CodigoAreaDerecho] = @CodigoAreaDerecho
            ,[DescripcionAsesoria] = @DescripcionAsesoria
            ,[Observaciones] = @Observaciones
            ,[RespuestaEscrito] = @RespuestaEscrito
            ,[CodigoConclusionAsesoria] = @CodigoConclusionAsesoria
            ,[FechaUsuarioModifica] = @FechaCreacion
            ,[NombreUsuarioModifica] = @NombreUsuarioCreacion		
		Where CodigoSolicitud = @CodigoSolicitud 
	End
	Else
	Begin
		INSERT INTO ClasificacionPeticion(
            CodigoSolicitud, CodigoTipoPeticion, CodigoAreaDerecho
			, [DescripcionAsesoria] ,[Observaciones] ,[RespuestaEscrito]
			,[CodigoConclusionAsesoria] ,[FechaCreacion] ,[NombreUsuarioCreacion]
			,[IDUsuarioCreacion]
		) VALUES (
			@CodigoSolicitud, @CodigoTipoPeticion, @CodigoAreaDerecho,
			@DescripcionAsesoria, @Observaciones, @RespuestaEscrito,    
			@CodigoConclusionAsesoria, @FechaCreacion, @NombreUsuarioCreacion,
			@IDUsuarioCreacion
		)
	End
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
create or alter PROCEDURE [dbo].[spConClasificacionPeticion]
 @CodigoSolicitud bigint
AS    
BEGIN
	SELECT 
      [CodigoSolicitud]
      ,CodigoTipoPeticion
      ,tp.nombre NombreTipoPeticion
      ,[CodigoAreaDerecho]
	  , ad.Nombre NombreAreaDerecho
      ,[DescripcionAsesoria]
      ,[Observaciones]
      ,[RespuestaEscrito]
      ,[CodigoConclusionAsesoria]
      ,ca.nombre NombreConclusionAsesoria
      ,[FechaCreacion]
      ,[NombreUsuarioCreacion]
      ,[IDUsuarioCreacion]
      ,[FechaUsuarioModifica]
      ,[NombreUsuarioModifica]
	FROM [dbo].[ClasificacionPeticion]		
	INNER JOIN Catalogo.AreaDerecho ad on ad.Codigo = CodigoAreaDerecho and ad.Habilitado = 1
	INNER JOIN Catalogo.TipoPeticion tp on tp.Codigo = CodigoTipoPeticion and tp.Habilitado = 1
	INNER JOIN Catalogo.ConclusionAsesoria ca on ca.Codigo = CodigoConclusionAsesoria and ca.Habilitado = 1
	WHERE CodigoSolicitud = @CodigoSolicitud
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
create or alter PROCEDURE [dbo].[spConsultarDatosRadicadoPorCodigo]
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
            , rad.Discapacidad, IsNull(rad.NumeroDocumentoIdentificacion, 1105871453) NumeroDocumentoIdentificacion
            , rad.GrupoEtnicoReconoce
            , IsNull(gen.Codigo,1) CodigoGenero, IsNull(gen.Nombre,'HOMBRE CISGÉNERO') NombreGenero 
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
			, ca.Codigo CodigoCanalAtencion, 
            'PRESENCIAL' NombreCanalAtencion            
			, 1 CodigoExpresionGenero,
            'MASCULINO' NombreExpresionGenero
            ,1 CodigoIdentidadGenero,
            'MASCULINO' NombreIdentidadGenero
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
create or alter PROCEDURE [dbo].[spConsultarDocumentosPorCodigoRadicado]
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
USE [defensoria]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*===============================================================================================================				 
* Sistema  : Defensoría del Pueblo - Ultimus
* Archivo  : spConDerechosClasificacionPorSolicitud.sql
* Autor	   : Mateo Polanco Rodríguez
* 
* Fecha      Responsable        Comentarios
* ==============================================================================================================
* 29/02/2024 Mateo Polanco	Permite consultar los derechos de la clasificación  por codigoSolicitud
* =============================================================================================================== */

CREATE PROCEDURE [dbo].[spConDerechosClasificacionPorSolicitud]
    @CodigoSolicitud bigint
AS    
BEGIN
    SELECT 
        dc.[CodigoDerechoClasificacion]
        ,dc.[CodigoSolicitud]
        ,dc.[CodigoDerecho]
        ,dc.[FechaCreacion]
        ,dc.[NombreUsuarioCreacion]
        ,dc.[IDUsuarioCreacion]
        ,dc.[FechaUsuarioModifica]
        ,dc.[NombreUsuarioModifica]
        ,dc.[Habilitado]
    FROM [dbo].[DerechosClasificacion] dc
    INNER JOIN Catalogo.Derecho der ON der.Codigo = dc.CodigoDerecho AND der.Habilitado = 1
    WHERE dc.CodigoSolicitud = @CodigoSolicitud AND dc.Habilitado = 1 
END
USE [defensoria]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*===============================================================================================================				 
* Sistema  : Defensoría del Pueblo - Ultimus
* Archivo  : spEliminarDerechoClasificacionPorCodigoSolicitud.sql
* Autor	   : Mateo Polanco Rodríguez
* 
* Fecha      Responsable        Comentarios
* ==============================================================================================================
* 29/02/2024 Mateo Polanco	Permite eliminar el derecho de la clasificación  por codigoSolicitud
* =============================================================================================================== */

CREATE PROCEDURE [dbo].[spEliminarDerechoClasificacionPorCodigoSolicitud]
    @CodigoDerechoClasificacion bigint
AS
BEGIN
    
    UPDATE [dbo].[DerechosClasificacion]
    SET Habilitado = 0 
    WHERE CodigoDerechoClasificacion = @CodigoDerechoClasificacion
END
GO
USE [defensoria]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*===============================================================================================================				 
* Sistema  : Defensoría del Pueblo - Ultimus
* Archivo  : spInsertarDerechoClasificacion.sql
* Autor	   : Mateo Polanco Rodríguez
* 
* Fecha      Responsable        Comentarios
* ==============================================================================================================
* 29/02/2024 Mateo Polanco	Permite insertar los derechos de la clasificación  
* =============================================================================================================== */

CREATE PROCEDURE [dbo].[spInsertarDerechoClasificacion]
    @CodigoDerechoClasificacion bigint,
    @CodigoSolicitud bigint,
    @CodigoDerecho bigint,
    @NombreUsuarioCreacion nvarchar(100),
    @IDUsuarioCreacion bigint
AS
BEGIN
  
    IF NOT EXISTS (
        SELECT 1 
        FROM [dbo].[DerechosClasificacion]
        WHERE CodigoDerecho = @CodigoDerecho
        AND CodigoSolicitud = @CodigoSolicitud
        AND Habilitado = 1
    )
    BEGIN
       
        INSERT INTO [dbo].[DerechosClasificacion] (
            CodigoDerechoClasificacion,
            CodigoSolicitud,
            CodigoDerecho,
            FechaCreacion,
            NombreUsuarioCreacion,
            IDUsuarioCreacion,
            Habilitado
        ) VALUES (
            @CodigoDerechoClasificacion,
            @CodigoSolicitud,
            @CodigoDerecho,
            GETDATE(), 
            @NombreUsuarioCreacion,
            @IDUsuarioCreacion,
            1 
        )
    END
    ELSE
    BEGIN
       
        RAISERROR ('Este derecho ya se encuentra asociado a la solicitu, favor revisar', 16, 1);
    END
END
GO
