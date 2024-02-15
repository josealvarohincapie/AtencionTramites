Imports System.Configuration
Imports System.Web.Configuration
Imports System.Configuration.ConfigurationManager
Imports Datos
Imports LogWriterHelper

Public Class AdminCatalogos
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            Dim strError As String = String.Empty
            Dim FnBD As New Datos.HelperSQL("BDCreditoDirecto")

            If FnBD.ConsultaCombo(ddlCatalogo, False, "PA_AdminCatalogo_V2 1", strError) Then
                ddlCatalogo_SelectedIndexChanged(Nothing, Nothing)
            Else

            End If

            If FnBD.ConsultaCombo(ddlTipoProducto, True, "PA_AdminCatalogo_V2 2,2", strError) Then
                ddlTipoProducto_SelectedIndexChanged(Nothing, Nothing)
            Else

            End If

            'Se cargan los documentos de la solicitud (Pestaña Documentos).

            If FnBD.ConsultaGrilla(gvDetalleDocumentos, "PA_AdminCatalogo_V2 2,9", strError) Then
                'Dim ds As New DataSet
                'For Each row As GridViewRow In gvDetalleDocumentos.Rows
                '    Dim temp() As DataRow = ds.Tables(0).Select("TIPODOCUMENTO=" & row.Cells(0).Text)
                '    If temp.Length > 0 Then
                '        CType(row.Cells(2).Controls(1), CheckBox).Checked = True
                '        CType(row.Cells(3).Controls(1), CheckBox).Checked = (temp(0)("OPCIONAL") = True)
                '    End If
                'Next
            Else

            End If

            If IsPostBack Then
                Script("$('#tab-documentos').click()")
            End If

            tbHabilitaCentinela.Text = AppSettings("HabilitaCentinela")
            tbHabilitaT24.Text = AppSettings("HabilitaT24")
            tbProductoPF.Text = AppSettings("ProductoProjectFinance")
            tbDiasAplazado.Text = AppSettings("DiasVencimientoAplazado")
            tbTipoNIT.Text = AppSettings("TipoNIT")
            tbPaisDefecto.Text = AppSettings("PaisDefecto")

        End If
    End Sub

    Protected Sub ddlCatalogo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCatalogo.SelectedIndexChanged
        Dim strError As String = String.Empty
        Dim nCat As Integer = ddlCatalogo.SelectedValue
        Dim FnBD As New Datos.HelperSQL("BDCreditoDirecto")
        If FnBD.ConsultaGrilla(gvDetalle, "PA_AdminCatalogo_V2 2," & nCat, strError) Then

        End If
    End Sub


    Private Sub ddlTipoProducto_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlTipoProducto.SelectedIndexChanged
        Dim strError As String = String.Empty
        Dim FnBD As New Datos.HelperSQL("BDCreditoDirecto")

        If FnBD.ConsultaGrilla(gvDetalleDocumentos, "PA_AdminCatalogo_V2 2,9", strError) Then

        Else

        End If

        Dim ds As New DataSet
        If FnBD.Consulta("PA_AdminCatalogo_V2 8,null,null,null,null,null,null,2,'" & ddlTipoProducto.SelectedValue & "'", ds, strError) Then
            For Each row As GridViewRow In gvDetalleDocumentos.Rows
                Dim temp() As DataRow = ds.Tables(0).Select("CODIGO='" & row.Cells(0).Text & "'")
                If temp.Length > 0 Then
                    CType(row.Cells(2).FindControl("chkDeudorJuridico"), CheckBox).Checked = (temp(0)("DEUDOR") = True And (temp(0)("TIPOPERSONA") = 0 Or temp(0)("TIPOPERSONA") = 1))
                    CType(row.Cells(2).FindControl("chkDeudorNatural"), CheckBox).Checked = (temp(0)("DEUDOR") = True And (temp(0)("TIPOPERSONA") = 0 Or temp(0)("TIPOPERSONA") = 2))
                    CType(row.Cells(3).Controls(1), CheckBox).Checked = (temp(0)("CODEUDOR") = True)
                End If
            Next
        Else

        End If

        If IsPostBack Then
            Script("$('#tab-documentos').click()")
        End If

    End Sub


    Protected Sub gvDetalle_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvDetalle.RowCommand
        Dim strSql As String = String.Empty
        Dim strError As String = String.Empty
        Dim row As GridViewRow = gvDetalle.Rows(Convert.ToInt32(e.CommandArgument))
        Dim numCat As Integer = ddlCatalogo.SelectedValue
        Dim FnBD As New Datos.HelperSQL("BDCreditoDirecto")

        If e.CommandName = "Eliminar" Then

            strSql = "PA_AdminCatalogo_V2 5," & numCat & ",'" & row.Cells(2).Text & "'"
            If FnBD.EjecutaAccion(strSql, strError) Then
                If Not FnBD.ConsultaGrilla(gvDetalle, "PA_AdminCatalogo_V2 2," & numCat, strError) Then

                End If
            Else

            End If

        ElseIf e.CommandName = "Editar" Then
            If numCat = 9 Then
                dvAdicional.Visible = True
            Else
                dvAdicional.Visible = False
            End If
            tbCodigo.Text = row.Cells(2).Text
            tbCodigo.Enabled = False
            tbDescripcion.Text = HttpUtility.HtmlDecode(row.Cells(3).Text)
            cbHabilitado.Checked = IIf(row.Cells(4).Text = "Si", True, False)
            cbAdicional.Checked = IIf(row.Cells(5).Text = "Si", True, False)
            Script("$('#modal-detalle').modal('show')")

        End If
    End Sub

    Private Sub gvDetalle_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvDetalle.RowDataBound
        If e.Row.RowIndex <> -1 Then
            e.Row.Cells(4).Text = IIf(e.Row.Cells(4).Text = "True", "Si", "No")
            e.Row.Cells(5).Text = IIf(e.Row.Cells(5).Text = "True", "Si", "No")
            If ddlCatalogo.SelectedValue = 9 Then
                gvDetalle.Columns(5).Visible = True
            Else
                gvDetalle.Columns(5).Visible = False
            End If
        End If
    End Sub

    Protected Sub btAgregar_Click(sender As Object, e As EventArgs) Handles btAgregar.Click
        Dim strError As String = String.Empty
        Dim numCat As Integer = ddlCatalogo.SelectedValue
        tbCodigo.Enabled = True
        tbCodigo.Text = String.Empty
        tbDescripcion.Text = String.Empty
        cbHabilitado.Checked = False
        If ddlCatalogo.SelectedValue = 9 Then
            dvAdicional.Visible = True
        Else
            dvAdicional.Visible = False
        End If
        Script("$('#modal-detalle').modal('show')")
    End Sub

    Protected Sub btGuardar_Click(sender As Object, e As EventArgs) Handles btGuardar.Click
        Dim strSql As String
        Dim strError As String = String.Empty
        Dim numCat As Integer = ddlCatalogo.SelectedValue
        Dim FnBD As New Datos.HelperSQL("BDCreditoDirecto")
        If tbCodigo.Enabled Then
            strSql = "PA_AdminCatalogo_V2 3," & numCat & ",'" & tbCodigo.Text & "','" & tbDescripcion.Text & "'," & IIf(cbHabilitado.Checked, 1, 0) & ",null," & IIf(cbAdicional.Checked, 1, 0)
        Else
            strSql = "PA_AdminCatalogo_V2 4," & numCat & ",'" & tbCodigo.Text & "','" & tbDescripcion.Text & "'," & IIf(cbHabilitado.Checked, 1, 0) & ",null," & IIf(cbAdicional.Checked, 1, 0)
        End If
        If FnBD.EjecutaAccion(strSql, strError) Then
            tbCodigo.Text = String.Empty
            tbDescripcion.Text = String.Empty
            cbHabilitado.Checked = False
            If Not FnBD.ConsultaGrilla(gvDetalle, "PA_AdminCatalogo_V2 2," & numCat, strError) Then

            End If
        Else
            Script("alert('" & strError & "')")
        End If
    End Sub

    Private Sub Script(ByVal strJS As String)
        strJS = "<script type='text/javascript'>$(document).ready(function(){" & strJS & "});</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Script", strJS)
    End Sub

    Private Sub gvDetalleDocumentos_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvDetalleDocumentos.RowDataBound
        If e.Row.RowIndex <> -1 Then
            e.Row.Cells(1).ToolTip = HttpUtility.HtmlDecode(e.Row.Cells(1).Text)
            If HttpUtility.HtmlDecode(e.Row.Cells(1).Text).Length > 70 Then
                e.Row.Cells(1).Text = HttpUtility.HtmlDecode(e.Row.Cells(1).Text).Substring(0, 70) & "..."
            End If
        End If
    End Sub

    Protected Sub btGuardarDocumentos_Click(sender As Object, e As EventArgs) Handles btGuardarDocumentos.Click
        Dim bolRes As Boolean
        Dim strError As String = String.Empty
        Dim FnBD As New Datos.HelperSQL("BDCreditoDirecto")
        bolRes = FnBD.EjecutaAccion("PA_AdminCatalogo_V2 6,null,null,null,null,null,null,2,'" & ddlTipoProducto.SelectedValue & "'", strError)
        If bolRes Then
            For Each row As GridViewRow In gvDetalleDocumentos.Rows
                Dim bolDeudorJuridico As Boolean = CType(row.Cells(2).FindControl("chkDeudorJuridico"), CheckBox).Checked
                Dim bolDeudorNatural As Boolean = CType(row.Cells(2).FindControl("chkDeudorNatural"), CheckBox).Checked
                Dim bolCodeudor As Boolean = CType(row.Cells(3).Controls(1), CheckBox).Checked
                If bolDeudorJuridico Or bolDeudorNatural Or bolCodeudor Then
                    Dim strSql As String
                    strSql = "PA_AdminCatalogo_V2 7,9,'" & row.Cells(0).Text & "',null,null,null,null,2,'" & ddlTipoProducto.SelectedValue & "'," & IIf(bolDeudorJuridico Or bolDeudorNatural, 1, 0) & "," & IIf(bolCodeudor, 1, 0) & "," & IIf(bolDeudorJuridico And Not bolDeudorNatural, 1, IIf(Not bolDeudorJuridico And bolDeudorNatural, 2, 0))
                    bolRes = FnBD.EjecutaAccion(strSql, strError)
                End If
            Next
        End If
        Script("$('#tab-documentos').click()")
    End Sub

    Protected Sub btGuardarParametros_Click(sender As Object, e As EventArgs) Handles btGuardarParametros.Click
        Dim myConfiguration As Configuration = WebConfigurationManager.OpenWebConfiguration("~")
        myConfiguration.AppSettings.Settings.Item("HabilitaCentinela").Value = tbHabilitaCentinela.Text
        myConfiguration.AppSettings.Settings.Item("HabilitaT24").Value = tbHabilitaT24.Text
        myConfiguration.AppSettings.Settings.Item("ProductoProjectFinance").Value = tbProductoPF.Text
        myConfiguration.AppSettings.Settings.Item("DiasVencimientoAplazado").Value = tbDiasAplazado.Text
        myConfiguration.AppSettings.Settings.Item("TipoNIT").Value = tbTipoNIT.Text
        myConfiguration.AppSettings.Settings.Item("PaisDefecto").Value = tbPaisDefecto.Text
        myConfiguration.Save()
        Script("$('#tab-parametros').click()")
    End Sub

End Class