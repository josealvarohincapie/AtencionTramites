Imports System.Configuration.ConfigurationManager

Public Class ReporteSolicitudes
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim strError As String = String.Empty
            Dim FnBD As New Datos.HelperSQL("BDCreditoDirecto")
            If Not FnBD.ConsultaCombo(ddlTipoProducto, True, "PA_AdminCatalogo_V2 2,4", strError) Then

            End If

        End If
    End Sub

    Protected Sub btConsultar_Click(sender As Object, e As EventArgs) Handles btConsultar.Click
        Dim strError As String = String.Empty
        Dim strSql As String
        Dim FnBD As New Datos.HelperSQL("BDCreditoDirecto")
        strSql = "PA_ReporteSolicitudes_V2 " & Val(tbIncidente.Text)
        FnBD.ConsultaGrilla(gvDetalle, strSql, strError)
    End Sub

    Protected Sub gvDetalle_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvDetalle.RowCommand
        Dim strSql As String = String.Empty
        Dim strError As String = String.Empty
        Dim FnBD As New Datos.HelperSQL("BDCreditoDirecto")
        Dim row As GridViewRow = gvDetalle.Rows(Convert.ToInt32(e.CommandArgument))
        Dim numIncidente As Long = row.Cells(3).Text
        If e.CommandName = "Detalle" Then
            strSql = "PA_AdminObservacion_V2 1," & numIncidente
            If Not FnBD.ConsultaGrilla(gvObservaciones, strSql, strError) Then

            End If
            strSql = "PA_SelectReporteDocumentos " & numIncidente
            If Not FnBD.ConsultaGrilla(gvDocumentos, strSql, strError) Then

            End If
            strSql = "PA_AdminComplementarios_V2 1," & numIncidente
            If Not FnBD.ConsultaGrilla(gvComplementarios, strSql, strError) Then

            End If
            strSql = "PA_AdminHistorico_V2 2,'" & AppSettings("UltimusProceso") & "',null," & numIncidente
            If Not FnBD.ConsultaGrilla(gvTareas, strSql, strError) Then

            End If
            Script("$('#modal-detalle').modal('show')")
        End If
    End Sub

    Private Sub gvDetalle_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvDetalle.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            CType(e.Row.Cells(1).Controls(1), ImageButton).OnClientClick = "window.open('" & AppSettings("UltimusMapa") & "?title&procName=" & AppSettings("UltimusProceso") & "&ProcVersion=" & AppSettings("UltimusVersion") & "&IncNo=" & e.Row.Cells(3).Text & "');return(false);"
        End If
    End Sub

    Private Sub gvComplementarios_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvComplementarios.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(5).Text = HttpUtility.HtmlDecode(e.Row.Cells(5).Text)
            e.Row.Cells(6).Text = HttpUtility.HtmlDecode(e.Row.Cells(6).Text)
            e.Row.Cells(7).Text = HttpUtility.HtmlDecode(e.Row.Cells(7).Text)
        End If
    End Sub

    Private Sub Script(ByVal strJS As String)
        strJS = "<script type='text/javascript'>$(document).ready(function(){" & strJS & "});</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Script", strJS)
    End Sub

    Private Sub gvObservaciones_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvObservaciones.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            If HttpUtility.HtmlDecode(e.Row.Cells(3).Text).Trim <> String.Empty Then
                Dim strCss As String = Estados.GetFromName(e.Row.Cells(3).Text).Css
                e.Row.Cells(3).Text = "&nbsp;<span class='label " & strCss & "'>&nbsp;</span>&nbsp;" & e.Row.Cells(3).Text
            End If
        End If
    End Sub

End Class