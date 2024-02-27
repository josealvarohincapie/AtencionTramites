USE [defensoria]
GO

IF NOT EXISTS(SELECT codigo FROM [Catalogo].[TipoPeticion] WHERE codigo = 1)
BEGIN
	INSERT INTO [Catalogo].[TipoPeticion]([Codigo],[Nombre],[Habilitado]) VALUES (1,'Asesoría',1)
END
GO

IF NOT EXISTS(SELECT codigo FROM [Catalogo].AreaDerecho WHERE codigo = 1)
BEGIN
	INSERT INTO [Catalogo].AreaDerecho([Codigo],[Nombre],[Habilitado]) VALUES (1,'Derecho administrativo',1)
END
GO

IF NOT EXISTS(SELECT codigo FROM [Catalogo].AreaDerecho WHERE codigo = 2)
BEGIN
	INSERT INTO [Catalogo].AreaDerecho([Codigo],[Nombre],[Habilitado]) VALUES (2,'Derecho civil',1)
END
GO

IF NOT EXISTS(SELECT codigo FROM [Catalogo].AreaDerecho WHERE codigo = 3)
BEGIN
	INSERT INTO [Catalogo].AreaDerecho([Codigo],[Nombre],[Habilitado]) VALUES (3,'Derecho comercial',1)
END
GO

IF NOT EXISTS(SELECT codigo FROM [Catalogo].AreaDerecho WHERE codigo = 4)
BEGIN
	INSERT INTO [Catalogo].AreaDerecho([Codigo],[Nombre],[Habilitado]) VALUES (4,'Derecho de familia',1)
END
GO
IF NOT EXISTS(SELECT codigo FROM [Catalogo].AreaDerecho WHERE codigo = 5)
BEGIN
	INSERT INTO [Catalogo].AreaDerecho([Codigo],[Nombre],[Habilitado]) VALUES (5,'Derecho internacional',1)
END
GO

IF NOT EXISTS(SELECT codigo FROM [Catalogo].AreaDerecho WHERE codigo = 6)
BEGIN
	INSERT INTO [Catalogo].AreaDerecho([Codigo],[Nombre],[Habilitado]) VALUES (6,'Derecho penal',1)
END
GO

IF NOT EXISTS(SELECT codigo FROM [Catalogo].AreaDerecho WHERE codigo = 7)
BEGIN
	INSERT INTO [Catalogo].AreaDerecho([Codigo],[Nombre],[Habilitado]) VALUES (7,'Derecho laboral',1)
END
GO

IF NOT EXISTS(SELECT codigo FROM [Catalogo].AreaDerecho WHERE codigo = 8)
BEGIN
	INSERT INTO [Catalogo].AreaDerecho([Codigo],[Nombre],[Habilitado]) VALUES (8,'Derecho policivo',1)
END
GO
IF NOT EXISTS(SELECT codigo FROM [Catalogo].AreaDerecho WHERE codigo = 9)
BEGIN
	INSERT INTO [Catalogo].AreaDerecho([Codigo],[Nombre],[Habilitado]) VALUES (9,'Derechos humanos y DIH',1)
END
GO

IF NOT EXISTS(SELECT codigo FROM [Catalogo].AreaDerecho WHERE codigo = 10)
BEGIN
	INSERT INTO [Catalogo].AreaDerecho([Codigo],[Nombre],[Habilitado]) VALUES (10,'J. Transicional Ley 1448 de 2011',1)
END
GO

