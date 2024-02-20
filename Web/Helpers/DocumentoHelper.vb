﻿Imports System.Configuration.ConfigurationManager
Imports LogWriterHelper
Imports Web.Modelo.dto

Public Class DocumentoHelper

    Public Sub New()

    End Sub

    ''' <summary>
    ''' Permite consultar los documentos asociados a un radicado
    ''' </summary>
    ''' <param name="codigoSolicitud">Código único de un radicado</param>
    ''' <returns>Lista con la información de los documentos asociados a un radicado</returns>
    Public Shared Function ConsultarDocumentosPorCodigoRadicado(codigoSolicitud As Int64) As List(Of DocumentoDTO)
        Dim listaDocumentos As New List(Of DocumentoDTO)()
        Try
            listaDocumentos = DocumentoDB.ConsultarDocumentosPorCodigoRadicado(codigoSolicitud)
        Catch ex As Exception
            Dim nombreMetodo = System.Reflection.MethodBase.GetCurrentMethod().Name
            LogWriter.WriteLog("DocumentoHelper -" & nombreMetodo, ex)
            Throw New Exception("DocumentoHelper -" & nombreMetodo & "-" & ex.Message, ex)
        End Try

        Return listaDocumentos
    End Function
End Class