USE [defensoria]
GO

delete from Catalogo.TipoPeticion;
delete from Catalogo.Derecho;
delete from Catalogo.AreaDerecho;
delete from Catalogo.ConclusionAsesoria;

IF NOT EXISTS(SELECT codigo FROM [Catalogo].ConclusionAsesoria WHERE codigo = 1)
BEGIN
	INSERT INTO Catalogo.ConclusionAsesoria (Codigo,Nombre,Habilitado) VALUES (1,'Petición absuelta',1); 
END
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
	INSERT INTO [Catalogo].AreaDerecho([Codigo],[Nombre],[Habilitado]) VALUES (6,'Derecho laboral',1)
END
GO

IF NOT EXISTS(SELECT codigo FROM [Catalogo].AreaDerecho WHERE codigo = 7)
BEGIN
	INSERT INTO [Catalogo].AreaDerecho([Codigo],[Nombre],[Habilitado]) VALUES (7,'Derecho penal',1)
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


delete from Catalogo.Derecho;

INSERT INTO Catalogo.Derecho (Codigo,Nombre,Habilitado,CodigoAreaDerecho) VALUES (	96	,'ACTUACIONES ADMINISTRATIVAS',1,	1	); 
INSERT INTO Catalogo.Derecho (Codigo,Nombre,Habilitado,CodigoAreaDerecho) VALUES (	97	,'PROCEDIMIENTOS ADMINISTRATIVOS',1,	1	); 
INSERT INTO Catalogo.Derecho (Codigo,Nombre,Habilitado,CodigoAreaDerecho) VALUES (	98	,'SILENCIO ADMINISTRATIVO',1,	1	); 
INSERT INTO Catalogo.Derecho (Codigo,Nombre,Habilitado,CodigoAreaDerecho) VALUES (	99	,'RECURSOS VIA GUBERNATIVA',1,	1	); 
INSERT INTO Catalogo.Derecho (Codigo,Nombre,Habilitado,CodigoAreaDerecho) VALUES (	100	,'ACCIONES CONTENCIOSO- ADMINISTRATIVO',1,	1	); 
INSERT INTO Catalogo.Derecho (Codigo,Nombre,Habilitado,CodigoAreaDerecho) VALUES (	91	,'SOBRE LAS PERSONAS',1,	2	); 
INSERT INTO Catalogo.Derecho (Codigo,Nombre,Habilitado,CodigoAreaDerecho) VALUES (	92	,'BIENES Y SU DOMINIO, POSESION, USO, CE Y DISPOSICION',1,	2	); 
INSERT INTO Catalogo.Derecho (Codigo,Nombre,Habilitado,CodigoAreaDerecho) VALUES (	93	,'SUCESIONES',1,	2	); 
INSERT INTO Catalogo.Derecho (Codigo,Nombre,Habilitado,CodigoAreaDerecho) VALUES (	94	,'OBLIGACIONES EN GENERAL Y CONTRATOS',1,	2	); 
INSERT INTO Catalogo.Derecho (Codigo,Nombre,Habilitado,CodigoAreaDerecho) VALUES (	95	,'CODI DE PROCEDIMIENTO CIVIL',1,	2	); 
INSERT INTO Catalogo.Derecho (Codigo,Nombre,Habilitado,CodigoAreaDerecho) VALUES (	101	,'ACCIONES DE LOS COMERCIANTES',1,	3	); 
INSERT INTO Catalogo.Derecho (Codigo,Nombre,Habilitado,CodigoAreaDerecho) VALUES (	102	,'SOCIEDADES COMERCIALES',1,	3	); 
INSERT INTO Catalogo.Derecho (Codigo,Nombre,Habilitado,CodigoAreaDerecho) VALUES (	103	,'CONTRATOS COMERCIALES',1,	3	); 
INSERT INTO Catalogo.Derecho (Codigo,Nombre,Habilitado,CodigoAreaDerecho) VALUES (	104	,'PROCEDIMIENTO COMERCIAL',1,	3	); 
INSERT INTO Catalogo.Derecho (Codigo,Nombre,Habilitado,CodigoAreaDerecho) VALUES (	105	,'MATRIMONIO',1,	4	); 
INSERT INTO Catalogo.Derecho (Codigo,Nombre,Habilitado,CodigoAreaDerecho) VALUES (	106	,'DIVORCIO',1,	4	); 
INSERT INTO Catalogo.Derecho (Codigo,Nombre,Habilitado,CodigoAreaDerecho) VALUES (	107	,'ADOPCION',1,	4	); 
INSERT INTO Catalogo.Derecho (Codigo,Nombre,Habilitado,CodigoAreaDerecho) VALUES (	108	,'PATRIA POTESTAD',1,	4	); 
INSERT INTO Catalogo.Derecho (Codigo,Nombre,Habilitado,CodigoAreaDerecho) VALUES (	109	,'EMANCIPACION',1,	4	); 
INSERT INTO Catalogo.Derecho (Codigo,Nombre,Habilitado,CodigoAreaDerecho) VALUES (	110	,'ALIMENTOS',1,	4	); 
INSERT INTO Catalogo.Derecho (Codigo,Nombre,Habilitado,CodigoAreaDerecho) VALUES (	111	,'TUTELA O CURADURIA DEL MENOR',1,	4	); 
INSERT INTO Catalogo.Derecho (Codigo,Nombre,Habilitado,CodigoAreaDerecho) VALUES (	112	,'PROCEDIMIENTOS ANTE INSTANCIAS INTERNACIONALES',1,	5	); 
INSERT INTO Catalogo.Derecho (Codigo,Nombre,Habilitado,CodigoAreaDerecho) VALUES (	80	,'CONTRATO INDIVIDUAL DE TRABAJO',1,	6	); 
INSERT INTO Catalogo.Derecho (Codigo,Nombre,Habilitado,CodigoAreaDerecho) VALUES (	81	,'PERIODO DE PRUEBA Y APRENDIZAJE',1,	6	); 
INSERT INTO Catalogo.Derecho (Codigo,Nombre,Habilitado,CodigoAreaDerecho) VALUES (	82	,'REGLAMENTO DE TRABAJO',1,	6	); 
INSERT INTO Catalogo.Derecho (Codigo,Nombre,Habilitado,CodigoAreaDerecho) VALUES (	83	,'SALARIOS',1,	6	); 
INSERT INTO Catalogo.Derecho (Codigo,Nombre,Habilitado,CodigoAreaDerecho) VALUES (	84	,'JORNADA DE TRABAJO',1,	6	); 
INSERT INTO Catalogo.Derecho (Codigo,Nombre,Habilitado,CodigoAreaDerecho) VALUES (	85	,'DESCANSOS OBLIGATORIOS',1,	6	); 
INSERT INTO Catalogo.Derecho (Codigo,Nombre,Habilitado,CodigoAreaDerecho) VALUES (	86	,'PRESTACIONES PATRONALES COMUNES',1,	6	); 
INSERT INTO Catalogo.Derecho (Codigo,Nombre,Habilitado,CodigoAreaDerecho) VALUES (	87	,'DERECHO COLECTIVO DE TRABAJO',1,	6	); 
INSERT INTO Catalogo.Derecho (Codigo,Nombre,Habilitado,CodigoAreaDerecho) VALUES (	88	,'CONVENCIONES Y PACTOS COLECTIVOS',1,	6	); 
INSERT INTO Catalogo.Derecho (Codigo,Nombre,Habilitado,CodigoAreaDerecho) VALUES (	89	,'PROCEDIMIENTO LABORAL',1,	6	); 
INSERT INTO Catalogo.Derecho (Codigo,Nombre,Habilitado,CodigoAreaDerecho) VALUES (	90	,'SEGURIDAD SOCIAL',1,	6	); 
INSERT INTO Catalogo.Derecho (Codigo,Nombre,Habilitado,CodigoAreaDerecho) VALUES (	113	,'APLICACION DE LA LEY PENAL',1,	7	); 
INSERT INTO Catalogo.Derecho (Codigo,Nombre,Habilitado,CodigoAreaDerecho) VALUES (	114	,'CONDUCTA PUNIBLE',1,	7	); 
INSERT INTO Catalogo.Derecho (Codigo,Nombre,Habilitado,CodigoAreaDerecho) VALUES (	115	,'PUNIBILIDAD',1,	7	); 
INSERT INTO Catalogo.Derecho (Codigo,Nombre,Habilitado,CodigoAreaDerecho) VALUES (	116	,'MECANISMOS SUSTITUTIVOS DE LA PENA PRIVATIVA DE LA LIBERTAD',1,	7	); 
INSERT INTO Catalogo.Derecho (Codigo,Nombre,Habilitado,CodigoAreaDerecho) VALUES (	117	,'MEDIDAS DE SEGURIDAD',1,	7	); 
INSERT INTO Catalogo.Derecho (Codigo,Nombre,Habilitado,CodigoAreaDerecho) VALUES (	118	,'EXTINCION DE LA ACCION Y SANCION PENAL',1,	7	); 
INSERT INTO Catalogo.Derecho (Codigo,Nombre,Habilitado,CodigoAreaDerecho) VALUES (	119	,'RESPONSABILIDAD CIVIL DERIVADA DE LA CONDUCTA PUNIBLE',1,	7	); 
INSERT INTO Catalogo.Derecho (Codigo,Nombre,Habilitado,CodigoAreaDerecho) VALUES (	120	,'PROCEDIMIENTO PENAL',1,	7	); 
INSERT INTO Catalogo.Derecho (Codigo,Nombre,Habilitado,CodigoAreaDerecho) VALUES (	121	,'ACCIONES POLICIVAS',1,	8	); 
INSERT INTO Catalogo.Derecho (Codigo,Nombre,Habilitado,CodigoAreaDerecho) VALUES (	1	,'VIDA',1,	9	); 
INSERT INTO Catalogo.Derecho (Codigo,Nombre,Habilitado,CodigoAreaDerecho) VALUES (	2	,'INTEGRIDAD PERSONAL',1,	9	); 
INSERT INTO Catalogo.Derecho (Codigo,Nombre,Habilitado,CodigoAreaDerecho) VALUES (	3	,'NO SER SOMETIDO A NUEVAS FORMAS DE ESCLAVITUD',1,	9	); 
INSERT INTO Catalogo.Derecho (Codigo,Nombre,Habilitado,CodigoAreaDerecho) VALUES (	4	,'LIBERTAD PERSONAL',1,	9	); 
INSERT INTO Catalogo.Derecho (Codigo,Nombre,Habilitado,CodigoAreaDerecho) VALUES (	5	,'NO SER SOMETIDO A DESAPARICION FORZADA',1,	9	); 
INSERT INTO Catalogo.Derecho (Codigo,Nombre,Habilitado,CodigoAreaDerecho) VALUES (	6	,'DEBIDO PROCESO LEGAL Y A LAS GARANTIAS JUDICIALES',1,	9	); 
INSERT INTO Catalogo.Derecho (Codigo,Nombre,Habilitado,CodigoAreaDerecho) VALUES (	7	,'VICTIMAS DE DESPLAZAMIENTO FORZADO POR LA VIOLENCIA',1,	9	); 
INSERT INTO Catalogo.Derecho (Codigo,Nombre,Habilitado,CodigoAreaDerecho) VALUES (	8	,'HONRA Y AL BUEN NOMBRE',1,	9	); 
INSERT INTO Catalogo.Derecho (Codigo,Nombre,Habilitado,CodigoAreaDerecho) VALUES (	9	,'INTIMIDAD',1,	9	); 
INSERT INTO Catalogo.Derecho (Codigo,Nombre,Habilitado,CodigoAreaDerecho) VALUES (	10	,'LIBRE DESARROLLO DE LA PERSONALIDAD',1,	9	); 
INSERT INTO Catalogo.Derecho (Codigo,Nombre,Habilitado,CodigoAreaDerecho) VALUES (	11	,'LIBERTAD RELIGIOSA',1,	9	); 
INSERT INTO Catalogo.Derecho (Codigo,Nombre,Habilitado,CodigoAreaDerecho) VALUES (	12	,'LIBERTAD DE CONCIENCIA',1,	9	); 
INSERT INTO Catalogo.Derecho (Codigo,Nombre,Habilitado,CodigoAreaDerecho) VALUES (	13	,'PROPIEDAD PRIVADA',1,	9	); 
INSERT INTO Catalogo.Derecho (Codigo,Nombre,Habilitado,CodigoAreaDerecho) VALUES (	14	,'RECONOCIMIENTO DE LA PERSONALIDAD JURIDICA',1,	9	); 
INSERT INTO Catalogo.Derecho (Codigo,Nombre,Habilitado,CodigoAreaDerecho) VALUES (	15	,'LIBERTAD DE OPINION, EXPRESION Y BUSCAR INFORMACION',1,	9	); 
INSERT INTO Catalogo.Derecho (Codigo,Nombre,Habilitado,CodigoAreaDerecho) VALUES (	16	,'LIBERTAD DE CIRCULACION Y RESIDENCIA',1,	9	); 
INSERT INTO Catalogo.Derecho (Codigo,Nombre,Habilitado,CodigoAreaDerecho) VALUES (	17	,'TRABAJO EN CONDICIONES EQUITATIVAS Y SATISFACTORIAS',1,	9	); 
INSERT INTO Catalogo.Derecho (Codigo,Nombre,Habilitado,CodigoAreaDerecho) VALUES (	18	,'PETICION',1,	9	); 
INSERT INTO Catalogo.Derecho (Codigo,Nombre,Habilitado,CodigoAreaDerecho) VALUES (	19	,'LIBERTAD DE REUNION',1,	9	); 
INSERT INTO Catalogo.Derecho (Codigo,Nombre,Habilitado,CodigoAreaDerecho) VALUES (	20	,'LIBERTADES DE ASOCIACION EN MATERIA LABORAL Y SINDICAL',1,	9	); 
INSERT INTO Catalogo.Derecho (Codigo,Nombre,Habilitado,CodigoAreaDerecho) VALUES (	21	,'LIBERTAD SINDICAL',1,	9	); 
INSERT INTO Catalogo.Derecho (Codigo,Nombre,Habilitado,CodigoAreaDerecho) VALUES (	22	,'DERECHOS POLITICOS',1,	9	); 
INSERT INTO Catalogo.Derecho (Codigo,Nombre,Habilitado,CodigoAreaDerecho) VALUES (	23	,'IGUALDAD ANTE LA LEY NO DISCRIMINACION',1,	9	); 
INSERT INTO Catalogo.Derecho (Codigo,Nombre,Habilitado,CodigoAreaDerecho) VALUES (	24	,'FAMILIA',1,	9	); 
INSERT INTO Catalogo.Derecho (Codigo,Nombre,Habilitado,CodigoAreaDerecho) VALUES (	25	,'SALUD',1,	9	); 
INSERT INTO Catalogo.Derecho (Codigo,Nombre,Habilitado,CodigoAreaDerecho) VALUES (	26	,'SEGURIDAD SOCIAL',1,	9	); 
INSERT INTO Catalogo.Derecho (Codigo,Nombre,Habilitado,CodigoAreaDerecho) VALUES (	27	,'SALUD EN CONEXIDAD CON VIDA',1,	9	); 
INSERT INTO Catalogo.Derecho (Codigo,Nombre,Habilitado,CodigoAreaDerecho) VALUES (	30	,'DERECHO AL AMBIENTE SANO Y AL EQUILIBRIO ECOLOGICO',1,	9	); 
INSERT INTO Catalogo.Derecho (Codigo,Nombre,Habilitado,CodigoAreaDerecho) VALUES (	31	,'RECREACION, AL DEPORTE Y AL APROVECHAMIENTO DEL TIEMPO LIBRE',1,	9	); 
INSERT INTO Catalogo.Derecho (Codigo,Nombre,Habilitado,CodigoAreaDerecho) VALUES (	32	,'EDUCACION',1,	9	); 
INSERT INTO Catalogo.Derecho (Codigo,Nombre,Habilitado,CodigoAreaDerecho) VALUES (	33	,'EL CE DEL ESPACIO PUBLICO Y LA UTILIZACION Y DEFENSA DE LOS BIENES DE USO PUBLICO',1,	9	); 
INSERT INTO Catalogo.Derecho (Codigo,Nombre,Habilitado,CodigoAreaDerecho) VALUES (	34	,'PAZ',1,	9	); 
INSERT INTO Catalogo.Derecho (Codigo,Nombre,Habilitado,CodigoAreaDerecho) VALUES (	35	,'REFUGIO',1,	9	); 
INSERT INTO Catalogo.Derecho (Codigo,Nombre,Habilitado,CodigoAreaDerecho) VALUES (	36	,'EL ACCESO A LOS SERVICIOS PUBLICOS Y A QUE SU PRESTACION SEA EFICIENTE Y OPORTUNA',1,	9	); 
INSERT INTO Catalogo.Derecho (Codigo,Nombre,Habilitado,CodigoAreaDerecho) VALUES (	37	,'NACIONALIDAD',1,	9	); 
INSERT INTO Catalogo.Derecho (Codigo,Nombre,Habilitado,CodigoAreaDerecho) VALUES (	38	,'DERECHO INTERNACIONAL HUMANITARIO',1,	9	); 
INSERT INTO Catalogo.Derecho (Codigo,Nombre,Habilitado,CodigoAreaDerecho) VALUES (	39	,'PUEBLOS INDIGENAS Y OTROS GRUPOS ETNICOS',1,	9	); 
INSERT INTO Catalogo.Derecho (Codigo,Nombre,Habilitado,CodigoAreaDerecho) VALUES (	40	,'VICTIMAS DE VIOLACIONES MANIFIESTAS A LAS NORMAS INTERNACIONALES DE DDHH Y DE VIOLACIONES GRAVES AL DIH',1,	9	); 
INSERT INTO Catalogo.Derecho (Codigo,Nombre,Habilitado,CodigoAreaDerecho) VALUES (	41	,'PROTECCION DE LOS CONSUMIDORES Y USUARIOS',1,	9	); 
INSERT INTO Catalogo.Derecho (Codigo,Nombre,Habilitado,CodigoAreaDerecho) VALUES (	42	,'MORALIDAD ADMINISTRATIVA',1,	9	); 
INSERT INTO Catalogo.Derecho (Codigo,Nombre,Habilitado,CodigoAreaDerecho) VALUES (	43	,'NINEZ',1,	9	); 
INSERT INTO Catalogo.Derecho (Codigo,Nombre,Habilitado,CodigoAreaDerecho) VALUES (	44	,'MUJERES',1,	9	); 
INSERT INTO Catalogo.Derecho (Codigo,Nombre,Habilitado,CodigoAreaDerecho) VALUES (	45	,'PERSONAS EN SITUACION DE DISCAPACIDAD',1,	9	); 
INSERT INTO Catalogo.Derecho (Codigo,Nombre,Habilitado,CodigoAreaDerecho) VALUES (	46	,'VICTIMAS DE MINAS ANTIPERSONA Y MUNICIONES ABANDONADAS SIN EXPLOTAR',1,	9	); 
INSERT INTO Catalogo.Derecho (Codigo,Nombre,Habilitado,CodigoAreaDerecho) VALUES (	47	,'VIVIENDA ADECUADA',1,	9	); 
INSERT INTO Catalogo.Derecho (Codigo,Nombre,Habilitado,CodigoAreaDerecho) VALUES (	48	,'USUARIOS DEL SERVICIO FINANCIERO',1,	9	); 
INSERT INTO Catalogo.Derecho (Codigo,Nombre,Habilitado,CodigoAreaDerecho) VALUES (	49	,'INFORMACION, DIVULGACION Y EDUCACION DE LOS CONSUMIDORES Y USUARIOS',1,	9	); 
INSERT INTO Catalogo.Derecho (Codigo,Nombre,Habilitado,CodigoAreaDerecho) VALUES (	50	,'PROTECCION CONTRA LA PUBLICIDAD ENGANOSA Y LOS METODOS COMERCIALES ABUSIVOS Y DESLEALES',1,	9	); 
INSERT INTO Catalogo.Derecho (Codigo,Nombre,Habilitado,CodigoAreaDerecho) VALUES (	51	,'PERSONAS PRIVADAS DE LA LIBERTAD',1,	9	); 
INSERT INTO Catalogo.Derecho (Codigo,Nombre,Habilitado,CodigoAreaDerecho) VALUES (	52	,'MINIMO VITAL O SUBSISTENCIA DIGNA',1,	9	); 
INSERT INTO Catalogo.Derecho (Codigo,Nombre,Habilitado,CodigoAreaDerecho) VALUES (	145	,'DERECHOS RECONOCIDOS A LOS DEFENSORES Y DEFENSORAS DE DERECHOS HUMANOS',1,	9	); 
INSERT INTO Catalogo.Derecho (Codigo,Nombre,Habilitado,CodigoAreaDerecho) VALUES (	150	,'DERECHOS RECONOCIDOS A LOS JOVENES',1,	9	); 
INSERT INTO Catalogo.Derecho (Codigo,Nombre,Habilitado,CodigoAreaDerecho) VALUES (	155	,'DERECHOS RECONOCIDOS A LAS PERSONAS DE LA TERCERA EDAD',1,	9	); 
INSERT INTO Catalogo.Derecho (Codigo,Nombre,Habilitado,CodigoAreaDerecho) VALUES (	160	,'DERECHOS RECONOCIDOS A LOS TRABAJADORES MIGRANTES Y SUS FAMILIAS',1,	9	); 
INSERT INTO Catalogo.Derecho (Codigo,Nombre,Habilitado,CodigoAreaDerecho) VALUES (	232	,'DERECHO HUMANO AL AGUA',1,	9	); 
INSERT INTO Catalogo.Derecho (Codigo,Nombre,Habilitado,CodigoAreaDerecho) VALUES (	234	,'TIERRA Y TERRITORIO',1,	9	); 
INSERT INTO Catalogo.Derecho (Codigo,Nombre,Habilitado,CodigoAreaDerecho) VALUES (	235	,'SEGURIDAD Y PREVENCION DE DESASTRES PREVISIBLES TECNICAMENTE',1,	9	); 
INSERT INTO Catalogo.Derecho (Codigo,Nombre,Habilitado,CodigoAreaDerecho) VALUES (	236	,'DERECHO A UNA VIDA LIBRE DE VIOLENCIA CONTRA LAS MUJERES',1,	9	); 
INSERT INTO Catalogo.Derecho (Codigo,Nombre,Habilitado,CodigoAreaDerecho) VALUES (	237	,'PERSONA CON DISCAPACIDAD PCD: VALORACION DE APOYO',1,	9	); 
INSERT INTO Catalogo.Derecho (Codigo,Nombre,Habilitado,CodigoAreaDerecho) VALUES (	175	,'DERECHO A LA VERDAD, JUSTICIA Y REPARACION',1,	10	); 
INSERT INTO Catalogo.Derecho (Codigo,Nombre,Habilitado,CodigoAreaDerecho) VALUES (	180	,'DERECHO A ACUDIR A ESCENARIOS DE DIALO INSTITUCIONAL Y COMUNITARIO',1,	10	); 
INSERT INTO Catalogo.Derecho (Codigo,Nombre,Habilitado,CodigoAreaDerecho) VALUES (	190	,'DERECHO A SOLICITAR Y RECIBIR ATENCION HUMANITARIA',1,	10	); 
INSERT INTO Catalogo.Derecho (Codigo,Nombre,Habilitado,CodigoAreaDerecho) VALUES (	230	,'DERECHO DE LAS MUJERES A VIVIR LIBRES DE VIOLENCIA',1,	10	); 
INSERT INTO Catalogo.Derecho (Codigo,Nombre,Habilitado,CodigoAreaDerecho) VALUES (	210	,'DERECHO A RETORNAR A SU LUGAR DE ORIGEN O REUBICARSE EN CONDICIONES DE VOLUNTARIEDAD, SEGURIDAD Y DIGNIDAD EN EL MARCO DE LA POLITICA DE SEGURIDAD NACIONAL',1,	10	); 
INSERT INTO Catalogo.Derecho (Codigo,Nombre,Habilitado,CodigoAreaDerecho) VALUES (	185	,'DERECHO A SER BENEFICIARIO DE LAS ACCIONES AFIRMATIVAS ADELANTANDAS POR EL ESTADO PARA PROTEGER Y GARANTIZAR EL DERECHO A LA VIDA EN CONDICIONES DE DIGNIDAD',1,	10	); 
INSERT INTO Catalogo.Derecho (Codigo,Nombre,Habilitado,CodigoAreaDerecho) VALUES (	195	,'DERECHO A PARTICIPAR EN LA FORMULACION, IMPLEMENTACION Y SEGUIMIENTO DE LA POLITICA PUBLICA DE PREVENCION, ATENCION Y REPARACION INTEGRAL',1,	10	); 
INSERT INTO Catalogo.Derecho (Codigo,Nombre,Habilitado,CodigoAreaDerecho) VALUES (	200	,'DERECHO A QUE LA POLITICA PUBLICA DE QUE TRATA LA PRESENTE LEY, TENGA ENFOQUE DIFERENCIAL',1,	10	); 
INSERT INTO Catalogo.Derecho (Codigo,Nombre,Habilitado,CodigoAreaDerecho) VALUES (	205	,'DERECHO A LA REUNIFICACION FAMILIAR CUANDO POR RAZON DE SU TIPO DE VICTIMIZACION SE HAYA DIVIDIDO EL NUCLEO FAMILIAR',1,	10	); 
INSERT INTO Catalogo.Derecho (Codigo,Nombre,Habilitado,CodigoAreaDerecho) VALUES (	215	,'DERECHO A LA RESTITUCION DE LA TIERRA SI HUBIERE SIDO DESPOJADO DE ELLA, EN LOS TERMINOS ESTABLECIDOS EN LA PRESENTE LEY',1,	10	); 
INSERT INTO Catalogo.Derecho (Codigo,Nombre,Habilitado,CodigoAreaDerecho) VALUES (	220	,'DERECHO A LA INFORMACION SOBRE LAS RUTAS Y LOS MEDIOS DE ACCESO A LAS MEDIDAS QUE SE ESTABLECEN EN LA PRESENTE LEY',1,	10	); 
INSERT INTO Catalogo.Derecho (Codigo,Nombre,Habilitado,CodigoAreaDerecho) VALUES (	225	,'DERECHO A CONOCER EL ESTADO DE PROCESOS JUDICIALES Y ADMINISTRATIVOS QUE SE ESTEN ADELANTANDO, EN LOS QUE TENGAN UN INTERES COMO PARTE O INTERVINIENTES',1,	10	); 

go