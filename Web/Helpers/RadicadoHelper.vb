﻿Imports Web
Imports System.Data
Imports System.IO
Imports System.Configuration.ConfigurationManager
Imports LogWriterHelper
''Imports Modelo.
Imports System.Globalization
Imports Web.Modelo.dto

Public Class RadicadoHelper

    Private Const _RadicadoData As String = "_RadicadoData"

    Public Sub New()

    End Sub

    Public Shared Sub InitRadicado(ByRef form As RegistroDePeticionarios)

        Try
            Dim radicado As RadicadoDTO = RadicadoHelper.ConsultarDatosRadicadoPorCodigo(form.TxtCodigoSolicitud.Value)
            form.TxtNumeroRadicado.Text = radicado.NumeroRadicado
        Catch ex As Exception
            Dim nombreMetodo = System.Reflection.MethodBase.GetCurrentMethod().Name
            LogWriter.WriteLog("RadicadoHelper - " & nombreMetodo, ex)
        End Try

    End Sub

    Private Shared Function ConsultarDatosRadicadoPorCodigo(codigoSolicitud As Int64) As RadicadoDTO
        Dim radicado As RadicadoDTO = RadicadoDB.ConsultarDatosRadicadoPorCodigo(codigoSolicitud)
        Return radicado
    End Function

End Class