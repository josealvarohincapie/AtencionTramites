USE [defensoria]
GO

-- ConclusionAsesoria
CREATE TABLE [Catalogo].[ConclusionAsesoria](
	[Codigo] [int] NOT NULL,
	[Nombre] [varchar](250) NOT NULL,
	[Habilitado] [bit] NOT NULL,
 CONSTRAINT [PK_ConclusionAsesoria] PRIMARY KEY CLUSTERED 
(
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

-- [AreaDerecho]
CREATE TABLE [Catalogo].[AreaDerecho](
	[Codigo] [int] NOT NULL,
	[Nombre] [varchar](250) NOT NULL,
	[Habilitado] [bit] NOT NULL,
 CONSTRAINT [PK_AreaDerecho] PRIMARY KEY CLUSTERED 
(
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

-- TipoPeticion

CREATE TABLE [Catalogo].[TipoPeticion](
	[Codigo] [int] NOT NULL,
	[Nombre] [varchar](250) NOT NULL,
	[Habilitado] [bit] NOT NULL,
 CONSTRAINT [PK_TipoPeticion] PRIMARY KEY CLUSTERED 
(
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO



-- [Catalogo].[Derecho]

CREATE TABLE [Catalogo].[Derecho](
	[Codigo] [int] NOT NULL,
	[Nombre] [varchar](250) NOT NULL,
	[Habilitado] [bit] NOT NULL,
	[CodigoAreaDerecho] [int] NOT NULL,
 CONSTRAINT [PK_Derecho] PRIMARY KEY CLUSTERED 
(
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [Catalogo].[Derecho]  WITH CHECK ADD  CONSTRAINT [FK_Derecho_AreaDerecho] FOREIGN KEY([CodigoAreaDerecho])
REFERENCES [Catalogo].[AreaDerecho] ([Codigo])
GO

ALTER TABLE [Catalogo].[Derecho] CHECK CONSTRAINT [FK_Derecho_AreaDerecho]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Código único del derecho' , @level0type=N'SCHEMA',@level0name=N'Catalogo', @level1type=N'TABLE',@level1name=N'Derecho', @level2type=N'COLUMN',@level2name=N'Codigo'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Nombre del derecho' , @level0type=N'SCHEMA',@level0name=N'Catalogo', @level1type=N'TABLE',@level1name=N'Derecho', @level2type=N'COLUMN',@level2name=N'Nombre'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1: Activo - 0:Inactivo' , @level0type=N'SCHEMA',@level0name=N'Catalogo', @level1type=N'TABLE',@level1name=N'Derecho', @level2type=N'COLUMN',@level2name=N'Habilitado'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'FK - AreaDerecho' , @level0type=N'SCHEMA',@level0name=N'Catalogo', @level1type=N'TABLE',@level1name=N'Derecho', @level2type=N'COLUMN',@level2name=N'CodigoAreaDerecho'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Catálogo con los derechos' , @level0type=N'SCHEMA',@level0name=N'Catalogo', @level1type=N'TABLE',@level1name=N'Derecho', @level2type=N'CONSTRAINT',@level2name=N'FK_Derecho_AreaDerecho'
GO

-- ClasificacionPeticion

CREATE TABLE [dbo].[ClasificacionPeticion](
	[CodigoSolicitud] [bigint] NOT NULL,
	[CodigoTipoPeticion] [int] NOT NULL,
	[CodigoAreaDerecho] [int] NULL,
	[DescripcionAsesoria] [varchar](2048) NOT NULL,
	[Observaciones] [varchar](2048) NOT NULL,
	[RespuestaEscrito] [bit] NOT NULL,
	[CodigoConclusionAsesoria] [int] NULL,
	[FechaCreacion] [datetime] NOT NULL,
	[NombreUsuarioCreacion] [varchar](250) NOT NULL,
	[IDUsuarioCreacion] [varchar](250) NULL,
	[FechaUsuarioModifica] [datetime] NULL,
	[NombreUsuarioModifica] [varchar](250) NULL,
 CONSTRAINT [PK_ClasificacionPeticion] PRIMARY KEY CLUSTERED 
(
	[CodigoSolicitud] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[ClasificacionPeticion]  WITH CHECK ADD  CONSTRAINT [FK_ClasificacionPeticion_AreaDerecho] FOREIGN KEY([CodigoAreaDerecho])
REFERENCES [Catalogo].[AreaDerecho] ([Codigo])
GO

ALTER TABLE [dbo].[ClasificacionPeticion] CHECK CONSTRAINT [FK_ClasificacionPeticion_AreaDerecho]
GO

ALTER TABLE [dbo].[ClasificacionPeticion]  WITH CHECK ADD  CONSTRAINT [FK_ClasificacionPeticion_ConclusionAsesoria] FOREIGN KEY([CodigoConclusionAsesoria])
REFERENCES [Catalogo].[ConclusionAsesoria] ([Codigo])
GO

ALTER TABLE [dbo].[ClasificacionPeticion] CHECK CONSTRAINT [FK_ClasificacionPeticion_ConclusionAsesoria]
GO

ALTER TABLE [dbo].[ClasificacionPeticion]  WITH CHECK ADD  CONSTRAINT [FK_ClasificacionPeticion_Radicado] FOREIGN KEY([CodigoSolicitud])
REFERENCES [dbo].[Radicado] ([CodigoSolicitud])
GO

ALTER TABLE [dbo].[ClasificacionPeticion] CHECK CONSTRAINT [FK_ClasificacionPeticion_Radicado]
GO

ALTER TABLE [dbo].[ClasificacionPeticion]  WITH CHECK ADD  CONSTRAINT [FK_ClasificacionPeticion_TipoPeticion] FOREIGN KEY([CodigoTipoPeticion])
REFERENCES [Catalogo].[TipoPeticion] ([Codigo])
GO

ALTER TABLE [dbo].[ClasificacionPeticion] CHECK CONSTRAINT [FK_ClasificacionPeticion_TipoPeticion]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'FK - Radicado' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ClasificacionPeticion', @level2type=N'COLUMN',@level2name=N'CodigoSolicitud'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'FK - TipoPeticion' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ClasificacionPeticion', @level2type=N'COLUMN',@level2name=N'CodigoTipoPeticion'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'FK - AreaDerecho' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ClasificacionPeticion', @level2type=N'COLUMN',@level2name=N'CodigoAreaDerecho'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Descripción Asesoria' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ClasificacionPeticion', @level2type=N'COLUMN',@level2name=N'DescripcionAsesoria'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Observaciones adicionales' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ClasificacionPeticion', @level2type=N'COLUMN',@level2name=N'Observaciones'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'¿La asesoría debe generar respuesta por escrito?' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ClasificacionPeticion', @level2type=N'COLUMN',@level2name=N'RespuestaEscrito'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Conclusiones asesoría' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ClasificacionPeticion', @level2type=N'COLUMN',@level2name=N'CodigoConclusionAsesoria'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha de creación del registro' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ClasificacionPeticion', @level2type=N'COLUMN',@level2name=N'FechaCreacion'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Nombre usuario creacion del registro' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ClasificacionPeticion', @level2type=N'COLUMN',@level2name=N'NombreUsuarioCreacion'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ID usuario creación' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ClasificacionPeticion', @level2type=N'COLUMN',@level2name=N'IDUsuarioCreacion'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha usuario modificación' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ClasificacionPeticion', @level2type=N'COLUMN',@level2name=N'FechaUsuarioModifica'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Nombre usuario modificación' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ClasificacionPeticion', @level2type=N'COLUMN',@level2name=N'NombreUsuarioModifica'
GO


-- DerechosClasificacion

CREATE TABLE [dbo].[DerechosClasificacion](
	[CodigoDerechoClasificacion] [bigint] IDENTITY(1,1) NOT NULL,
	[CodigoSolicitud] [bigint] NOT NULL,
	[CodigoDerecho] [int] NOT NULL,
	[FechaCreacion] [datetime] NOT NULL,
	[NombreUsuarioCreacion] [varchar](250) NOT NULL,
	[IDUsuarioCreacion] [varchar](250) NULL,
	[FechaUsuarioModifica] [datetime] NOT NULL,
	[NombreUsuarioModifica] [varchar](250) NOT NULL,
	[Habilitado] [bit] NOT NULL,
 CONSTRAINT [PK_DerechosClasificacion] PRIMARY KEY CLUSTERED 
(
	[CodigoDerechoClasificacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[DerechosClasificacion]  WITH CHECK ADD  CONSTRAINT [FK_DerechosClasificacion_Derecho] FOREIGN KEY([CodigoDerecho])
REFERENCES [Catalogo].[Derecho] ([Codigo])
GO

ALTER TABLE [dbo].[DerechosClasificacion] CHECK CONSTRAINT [FK_DerechosClasificacion_Derecho]
GO

ALTER TABLE [dbo].[DerechosClasificacion]  WITH CHECK ADD  CONSTRAINT [FK_DerechosClasificacion_Radicado] FOREIGN KEY([CodigoSolicitud])
REFERENCES [dbo].[Radicado] ([CodigoSolicitud])
GO

ALTER TABLE [dbo].[DerechosClasificacion] CHECK CONSTRAINT [FK_DerechosClasificacion_Radicado]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Identificador único del registro' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DerechosClasificacion', @level2type=N'COLUMN',@level2name=N'CodigoDerechoClasificacion'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'FK - Radicado' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DerechosClasificacion', @level2type=N'COLUMN',@level2name=N'CodigoSolicitud'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'FK - Catálogo Derecho' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DerechosClasificacion', @level2type=N'COLUMN',@level2name=N'CodigoDerecho'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha de creación del registro' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DerechosClasificacion', @level2type=N'COLUMN',@level2name=N'FechaCreacion'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Nombre usuario creacion del registro' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DerechosClasificacion', @level2type=N'COLUMN',@level2name=N'NombreUsuarioCreacion'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ID usuario creación' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DerechosClasificacion', @level2type=N'COLUMN',@level2name=N'IDUsuarioCreacion'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha usuario modificación' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DerechosClasificacion', @level2type=N'COLUMN',@level2name=N'FechaUsuarioModifica'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Nombre usuario modificación' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DerechosClasificacion', @level2type=N'COLUMN',@level2name=N'NombreUsuarioModifica'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Activo:1 - inactivo:0' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DerechosClasificacion', @level2type=N'COLUMN',@level2name=N'Habilitado'
GO

