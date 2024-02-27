USE [defensoria]
GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DerechosClasificacion', @level2type=N'COLUMN',@level2name=N'Habilitado'
GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DerechosClasificacion', @level2type=N'COLUMN',@level2name=N'NombreUsuarioModifica'
GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DerechosClasificacion', @level2type=N'COLUMN',@level2name=N'FechaUsuarioModifica'
GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DerechosClasificacion', @level2type=N'COLUMN',@level2name=N'IDUsuarioCreacion'
GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DerechosClasificacion', @level2type=N'COLUMN',@level2name=N'NombreUsuarioCreacion'
GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DerechosClasificacion', @level2type=N'COLUMN',@level2name=N'FechaCreacion'
GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DerechosClasificacion', @level2type=N'COLUMN',@level2name=N'CodigoDerecho'
GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DerechosClasificacion', @level2type=N'COLUMN',@level2name=N'CodigoSolicitud'
GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DerechosClasificacion', @level2type=N'COLUMN',@level2name=N'CodigoDerechoClasificacion'
GO

ALTER TABLE [dbo].[DerechosClasificacion] DROP CONSTRAINT [FK_DerechosClasificacion_Radicado]
GO

ALTER TABLE [dbo].[DerechosClasificacion] DROP CONSTRAINT [FK_DerechosClasificacion_Derecho]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DerechosClasificacion]') AND type in (N'U'))
DROP TABLE [dbo].[DerechosClasificacion]
GO


-- ClasificacionPeticion



EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ClasificacionPeticion', @level2type=N'COLUMN',@level2name=N'NombreUsuarioModifica'
GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ClasificacionPeticion', @level2type=N'COLUMN',@level2name=N'FechaUsuarioModifica'
GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ClasificacionPeticion', @level2type=N'COLUMN',@level2name=N'IDUsuarioCreacion'
GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ClasificacionPeticion', @level2type=N'COLUMN',@level2name=N'NombreUsuarioCreacion'
GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ClasificacionPeticion', @level2type=N'COLUMN',@level2name=N'FechaCreacion'
GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ClasificacionPeticion', @level2type=N'COLUMN',@level2name=N'CodigoConclusionAsesoria'
GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ClasificacionPeticion', @level2type=N'COLUMN',@level2name=N'RespuestaEscrito'
GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ClasificacionPeticion', @level2type=N'COLUMN',@level2name=N'Observaciones'
GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ClasificacionPeticion', @level2type=N'COLUMN',@level2name=N'DescripcionAsesoria'
GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ClasificacionPeticion', @level2type=N'COLUMN',@level2name=N'CodigoAreaDerecho'
GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ClasificacionPeticion', @level2type=N'COLUMN',@level2name=N'CodigoTipoPeticion'
GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ClasificacionPeticion', @level2type=N'COLUMN',@level2name=N'CodigoSolicitud'
GO

ALTER TABLE [dbo].[ClasificacionPeticion] DROP CONSTRAINT [FK_ClasificacionPeticion_TipoPeticion]
GO

ALTER TABLE [dbo].[ClasificacionPeticion] DROP CONSTRAINT [FK_ClasificacionPeticion_Radicado]
GO

ALTER TABLE [dbo].[ClasificacionPeticion] DROP CONSTRAINT [FK_ClasificacionPeticion_ConclusionAsesoria]
GO

ALTER TABLE [dbo].[ClasificacionPeticion] DROP CONSTRAINT [FK_ClasificacionPeticion_AreaDerecho]
GO

/****** Object:  Table [dbo].[ClasificacionPeticion]    Script Date: 27/02/2024 2:24:47 p. m. ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ClasificacionPeticion]') AND type in (N'U'))
DROP TABLE [dbo].[ClasificacionPeticion]
GO

-- Catalogo.Derecho

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'Catalogo', @level1type=N'TABLE',@level1name=N'Derecho', @level2type=N'CONSTRAINT',@level2name=N'FK_Derecho_AreaDerecho'
GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'Catalogo', @level1type=N'TABLE',@level1name=N'Derecho', @level2type=N'COLUMN',@level2name=N'CodigoAreaDerecho'
GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'Catalogo', @level1type=N'TABLE',@level1name=N'Derecho', @level2type=N'COLUMN',@level2name=N'Habilitado'
GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'Catalogo', @level1type=N'TABLE',@level1name=N'Derecho', @level2type=N'COLUMN',@level2name=N'Nombre'
GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'Catalogo', @level1type=N'TABLE',@level1name=N'Derecho', @level2type=N'COLUMN',@level2name=N'Codigo'
GO

ALTER TABLE [Catalogo].[Derecho] DROP CONSTRAINT [FK_Derecho_AreaDerecho]
GO

/****** Object:  Table [Catalogo].[Derecho]    Script Date: 27/02/2024 2:34:04 p. m. ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Catalogo].[Derecho]') AND type in (N'U'))
DROP TABLE [Catalogo].[Derecho]
GO