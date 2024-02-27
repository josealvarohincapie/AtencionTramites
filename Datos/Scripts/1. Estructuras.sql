IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Catalogo].[TipoPeticion]') AND type in (N'U'))
DROP TABLE [Catalogo].[TipoPeticion]
GO

CREATE TABLE [Catalogo].[TipoPeticion](
	[Codigo] [int] NOT NULL,
	[Nombre] [varchar](250) NOT NULL,	
	[Habilitado] [bit] NOT NULL
 CONSTRAINT [PK_TipoPeticion] PRIMARY KEY CLUSTERED 
(
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Catalogo].[AreaDerecho]') AND type in (N'U'))
DROP TABLE [Catalogo].[AreaDerecho]
GO

CREATE TABLE [Catalogo].[AreaDerecho](
	[Codigo] [int] NOT NULL,
	[Nombre] [varchar](250) NOT NULL,	
	[Habilitado] [bit] NOT NULL
 CONSTRAINT [PK_AreaDerecho] PRIMARY KEY CLUSTERED 
(
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Catalogo].Derecho') AND type in (N'U'))
DROP TABLE [Catalogo].Derecho
GO

CREATE TABLE [Catalogo].Derecho(
	[Codigo] [int] NOT NULL,
	[Nombre] [varchar](250) NOT NULL,	
	[Habilitado] [bit] NOT NULL
 CONSTRAINT [PK_Derecho] PRIMARY KEY CLUSTERED 
(
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Catalogo].ConclusionAsesoria') AND type in (N'U'))
DROP TABLE [Catalogo].ConclusionAsesoria
GO

CREATE TABLE [Catalogo].ConclusionAsesoria(
	[Codigo] [int] NOT NULL,
	[Nombre] [varchar](250) NOT NULL,	
	[Habilitado] [bit] NOT NULL
 CONSTRAINT [PK_ConclusionAsesoria] PRIMARY KEY CLUSTERED 
(
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ClasificacionPeticion]') AND type in (N'U'))
DROP TABLE [dbo].[ClasificacionPeticion]
GO

CREATE TABLE [dbo].[ClasificacionPeticion] (
    [CodigoSolicitud]          BIGINT         NOT NULL,
    [CodigoTipoPeticion]       INT            NOT NULL,
    [CodigoAreaDerecho]        INT            NULL,
    [DescripcionAsesoria]      VARCHAR (2000) NOT NULL,
    [Observaciones]            VARCHAR (2000) NOT NULL,
    [RespuestaEscrito]         BIT            NOT NULL,
    [CodigoConclusionAsesoria] INT            NULL,
    [FechaCreacion]            DATETIME       NOT NULL,
    [NombreUsuarioCreacion]    VARCHAR (250)  NOT NULL,
    [IDUsuarioCreacion]        VARCHAR (250)  NULL,
    [FechaUsuarioModifica]     DATETIME       NOT NULL,
    [NombreUsuarioModifica]    VARCHAR (250)  NOT NULL,
    CONSTRAINT [PK_ClasificacionPeticion] PRIMARY KEY CLUSTERED ([CodigoSolicitud] ASC),
    CONSTRAINT [FK_ClasificacionPeticion_AreaDerecho] FOREIGN KEY ([CodigoAreaDerecho]) REFERENCES [Catalogo].[AreaDerecho] ([Codigo]),
    CONSTRAINT [FK_ClasificacionPeticion_ConclusionAsesoria] FOREIGN KEY ([CodigoConclusionAsesoria]) REFERENCES [Catalogo].[ConclusionAsesoria] ([Codigo]),
    CONSTRAINT [FK_ClasificacionPeticion_Radicado] FOREIGN KEY ([CodigoSolicitud]) REFERENCES [dbo].[Radicado] ([CodigoSolicitud]),
    CONSTRAINT [FK_ClasificacionPeticion_TipoPeticion] FOREIGN KEY ([CodigoTipoPeticion]) REFERENCES [Catalogo].[TipoPeticion] ([Codigo])
);

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre usuario modificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ClasificacionPeticion', @level2type = N'COLUMN', @level2name = N'NombreUsuarioModifica';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ClasificacionPeticion', @level2type = N'COLUMN', @level2name = N'CodigoClasificacion';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID usuario creación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ClasificacionPeticion', @level2type = N'COLUMN', @level2name = N'IDUsuarioCreacion';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK - TipoPeticion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ClasificacionPeticion', @level2type = N'COLUMN', @level2name = N'CodigoTipoPeticion';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de creación del registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ClasificacionPeticion', @level2type = N'COLUMN', @level2name = N'FechaCreacion';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones adicionales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ClasificacionPeticion', @level2type = N'COLUMN', @level2name = N'Observaciones';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Conclusiones asesoría', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ClasificacionPeticion', @level2type = N'COLUMN', @level2name = N'CodigoConclusionAsesoria';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK - Radicado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ClasificacionPeticion', @level2type = N'COLUMN', @level2name = N'CodigoSolicitud';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha usuario modificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ClasificacionPeticion', @level2type = N'COLUMN', @level2name = N'FechaUsuarioModifica';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK - AreaDerecho', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ClasificacionPeticion', @level2type = N'COLUMN', @level2name = N'CodigoAreaDerecho';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre usuario creacion del registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ClasificacionPeticion', @level2type = N'COLUMN', @level2name = N'NombreUsuarioCreacion';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'¿La asesoría debe generar respuesta por escrito?', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ClasificacionPeticion', @level2type = N'COLUMN', @level2name = N'RespuestaEscrito';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción Asesoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ClasificacionPeticion', @level2type = N'COLUMN', @level2name = N'DescripcionAsesoria';

CREATE TABLE [dbo].[DerechosClasificacion] (
    [CodigoDerechoClasificacion] BIGINT        IDENTITY (1, 1) NOT NULL,
    [CodigoSolicitud]            BIGINT        NOT NULL,
    [CodigoDerecho]              INT           NOT NULL,
    [FechaCreacion]              DATETIME      NOT NULL,
    [NombreUsuarioCreacion]      VARCHAR (250) NOT NULL,
    [IDUsuarioCreacion]          VARCHAR (250) NULL,
    [FechaUsuarioModifica]       DATETIME      NOT NULL,
    [NombreUsuarioModifica]      VARCHAR (250) NOT NULL,
    [Habilitado]                 BIT           NOT NULL,
    CONSTRAINT [PK_DerechosClasificacion] PRIMARY KEY CLUSTERED ([CodigoDerechoClasificacion] ASC),
    CONSTRAINT [FK_DerechosClasificacion_Derecho] FOREIGN KEY ([CodigoDerecho]) REFERENCES [Catalogo].[Derecho] ([Codigo]),
    CONSTRAINT [FK_DerechosClasificacion_Radicado] FOREIGN KEY ([CodigoSolicitud]) REFERENCES [dbo].[Radicado] ([CodigoSolicitud])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Almacena los derechos asociados a una petición en la clasificación de una petición', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DerechosClasificacion';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de creación del registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DerechosClasificacion', @level2type = N'COLUMN', @level2name = N'FechaCreacion';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre usuario creacion del registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DerechosClasificacion', @level2type = N'COLUMN', @level2name = N'NombreUsuarioCreacion';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha usuario modificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DerechosClasificacion', @level2type = N'COLUMN', @level2name = N'FechaUsuarioModifica';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DerechosClasificacion', @level2type = N'COLUMN', @level2name = N'CodigoDerechoClasificacion';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID usuario creación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DerechosClasificacion', @level2type = N'COLUMN', @level2name = N'IDUsuarioCreacion';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Activo:1 - inactivo:0', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DerechosClasificacion', @level2type = N'COLUMN', @level2name = N'Habilitado';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK - Catálogo Derecho', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DerechosClasificacion', @level2type = N'COLUMN', @level2name = N'CodigoDerecho';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre usuario modificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DerechosClasificacion', @level2type = N'COLUMN', @level2name = N'NombreUsuarioModifica';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK - Radicado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DerechosClasificacion', @level2type = N'COLUMN', @level2name = N'CodigoSolicitud';

