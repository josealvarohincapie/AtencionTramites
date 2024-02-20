/*===============================================================================================================				 
* Sistema  : Defensoría del Pueblo - Ultimus
* Archivo  : [spConsultarDocumentosPorCodigoRadicado].sql
* Autor	   : José Álvaro Hincapié Castillo
* 
* Fecha      Responsable        Comentarios
* ==============================================================================================================
* 13/02/2024 José A Hincapié	Permite consultar los documentos asociados a un radicado
* =============================================================================================================== */
create or ALTER PROCEDURE [dbo].[spConsultarDocumentosPorCodigoRadicado]
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
