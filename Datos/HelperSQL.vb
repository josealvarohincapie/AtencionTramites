Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationManager
Imports System.Web.UI.WebControls
Imports LogWriterHelper

Public Class HelperSQL

    Private sCnBD As String

    Public Sub New(ByVal sCnBD As String)
        MyBase.New()
        Me.sCnBD = sCnBD
    End Sub

    ''' <summary>
    ''' Función para consulta en base de datos
    ''' </summary>
    ''' <param name="sSql">comando de sql</param>
    ''' <param name="oDs">dataset con el resultado de la consulta</param>
    ''' <param name="sErr">descripción del error si lo hay</param>
    ''' <returns>True si la consulta es exitosa y False si ocurre un error</returns>
    ''' <remarks></remarks>
    Public Function Consulta(ByVal sSql As String, ByRef oDs As DataSet, ByRef sErr As String) As Boolean
        Dim bRes As Boolean
        Dim cnn As New SqlConnection(AppSettings(sCnBD))
        Dim cmd As SqlCommand
        Dim da As SqlDataAdapter
        Try
            cnn.Open()
            cmd = New SqlCommand(sSql, cnn)
            da = New SqlDataAdapter(cmd)
            da.Fill(oDs)
            bRes = True
        Catch ex As Exception
            sErr = ex.Message & " " & sSql
            LogWriter.WriteLog("HelperSQL", sSql)
            LogWriter.WriteLog("HelperSQL", ex)
        Finally
            cmd = Nothing
            da = Nothing
            cnn.Close()
            cnn = Nothing
        End Try
        Return bRes
    End Function

    ''' <summary>
    ''' Función que ejecuta un comando en la base de datos
    ''' </summary>
    ''' <param name="sSql">comando de sql</param>
    ''' <param name="sErr">descripción del error si lo hay</param>
    ''' <returns>True si la consulta es exitosa y False si ocurre un error</returns>
    ''' <remarks></remarks>
    Public Function EjecutaAccion(ByVal sSql As String, ByRef sErr As String) As Boolean
        Dim bRes As Boolean
        Dim cnn As New SqlConnection(AppSettings(sCnBD))
        Dim cmd As SqlCommand
        Try
            cnn.Open()
            cmd = New SqlCommand(sSql, cnn)
            If cmd.ExecuteNonQuery() > 0 Then
                bRes = True
            End If
            bRes = True
        Catch ex As SqlException
            'sErr = e.Message & " " & sSql
            If ex.Number = 2627 Then
                sErr = "El registro ya existe."
            ElseIf ex.Number = 547 Then
                sErr = "Existen registros asociados."
            Else
                sErr = ex.Message
            End If

            sErr = ex.Message & " " & sSql
            LogWriter.WriteLog("HelperSQL", sSql)
            LogWriter.WriteLog("HelperSQL", ex)
        Catch ex As Exception
            sErr = ex.Message & " " & sSql
            LogWriter.WriteLog("HelperSQL", sSql)
            LogWriter.WriteLog("HelperSQL", ex)
        Finally
            cmd = Nothing
            cnn.Close()
            cnn = Nothing
        End Try
        Return bRes
    End Function

    ''' <summary>
    ''' Función para la consulta de un dato en la base de datos
    ''' </summary>
    ''' <param name="sSql">comando de sql</param>
    ''' <param name="sVal">dato resultado de la consulta</param>
    ''' <param name="sErr">descripción del error si lo hay</param>
    ''' <returns>True si la consulta es exitosa y False si ocurre un error</returns>
    ''' <remarks></remarks>
    Public Function ConsultaValor(ByVal sSql As String, ByRef sVal As String, ByRef sErr As String) As Boolean
        Dim bRes As Boolean
        Dim ds As New DataSet
        If Consulta(sSql, ds, sErr) Then
            If ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                sVal = ds.Tables(0).Rows(0).Item(0)
            End If
            bRes = True
        End If
        Return bRes
    End Function

    ''' <summary>
    ''' Función que ejecuta una consulta y carga un control grilla con los resultados
    ''' </summary>
    ''' <param name="oCtrl">grilla a cargar</param>
    ''' <param name="sSql">comando de sql</param>
    ''' <param name="sErr">descripción del error si lo hay</param>
    ''' <returns>True si la consulta es exitosa y False si ocurre un error</returns>
    ''' <remarks></remarks>
    Function ConsultaGrilla(ByVal oCtrl As GridView, ByVal sSql As String, ByRef sErr As String) As Boolean
        Dim bRes As Boolean
        Dim ds As New DataSet
        If Consulta(sSql, ds, sErr) Then
            If ds.Tables.Count <> 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                oCtrl.DataSource = ds
            Else
                oCtrl.DataSource = Nothing
            End If
            oCtrl.DataBind()
            bRes = True
            ds = Nothing
        End If
        Return bRes
    End Function

    ''' <summary>
    ''' Función que ejecuta una consulta y carga un control combo con los resultados
    ''' </summary>
    ''' <param name="oCtrl">combo a cargar</param>
    ''' <param name="bSel">True si muestra un elemento "Seleccione" y False de lo contrario</param>
    ''' <param name="sSql">comando de sql</param>
    ''' <param name="sErr">descripción del error si lo hay</param>
    ''' <returns>True si la consulta es exitosa y False si ocurre un error</returns>
    ''' <remarks></remarks>
    Function ConsultaCombo(ByVal oCtrl As DropDownList, ByVal bSel As Boolean, ByVal sSql As String, ByRef sErr As String) As Boolean
        Dim bRes As Boolean
        Dim ds As New DataSet
        oCtrl.Items.Clear()
        If Consulta(sSql, ds, sErr) Then
            If ds.Tables.Count > 0 Then
                oCtrl.DataSource = ds.Tables(0)
                If ds.Tables(0).Columns.Count = 1 Then
                    oCtrl.DataTextField = ds.Tables(0).Columns(0).ColumnName
                Else
                    oCtrl.DataTextField = ds.Tables(0).Columns(1).ColumnName
                End If
                oCtrl.DataValueField = ds.Tables(0).Columns(0).ColumnName
                oCtrl.DataBind()
                If bSel Then oCtrl.Items.Insert(0, New ListItem(String.Empty, 0))
                bRes = True
            End If
            ds = Nothing
        End If
        Return bRes
    End Function

    ''' <summary>
    ''' Función que ejecuta una consulta y carga dos control combo con los resultados
    ''' </summary>
    ''' <param name="oCtrl1">Primer combo a cargar</param>
    ''' <param name="oCtrl2">Segundo combo a cargar</param>
    ''' <param name="bSel">True si muestra un elemento "Seleccione" y False de lo contrario</param>
    ''' <param name="sSql">comando de sql</param>
    ''' <param name="sErr">descripción del error si lo hay</param>
    ''' <returns>True si la consulta es exitosa y False si ocurre un error</returns>
    ''' <remarks></remarks>
    Function ConsultaCombo2(ByVal oCtrl1 As DropDownList, ByVal oCtrl2 As DropDownList, ByVal bSel As Boolean, ByVal sSql As String, ByRef sErr As String) As Boolean
        Dim bRes As Boolean
        Dim ds As New DataSet
        oCtrl1.Items.Clear()
        oCtrl2.Items.Clear()
        If Consulta(sSql, ds, sErr) Then
            If ds.Tables.Count > 0 Then
                oCtrl1.DataSource = ds.Tables(0)
                oCtrl2.DataSource = ds.Tables(0)
                If ds.Tables(0).Columns.Count = 1 Then
                    oCtrl1.DataTextField = ds.Tables(0).Columns(0).ColumnName
                    oCtrl2.DataTextField = ds.Tables(0).Columns(0).ColumnName
                Else
                    oCtrl1.DataTextField = ds.Tables(0).Columns(1).ColumnName
                    oCtrl2.DataTextField = ds.Tables(0).Columns(1).ColumnName
                End If
                oCtrl1.DataValueField = ds.Tables(0).Columns(0).ColumnName
                oCtrl1.DataBind()

                oCtrl2.DataValueField = ds.Tables(0).Columns(0).ColumnName
                oCtrl2.DataBind()

                If bSel Then
                    oCtrl1.Items.Insert(0, New ListItem(String.Empty, 0))
                    oCtrl2.Items.Insert(0, New ListItem(String.Empty, 0))
                End If

                bRes = True
            End If
            ds = Nothing
        End If
        Return bRes
    End Function

    ''' <summary>
    ''' Función que ejecuta una consulta y carga un control lista con los resultados
    ''' </summary>
    ''' <param name="oCtrl">lista a cargar</param>
    ''' <param name="bSel">True si muestra un elemento "Seleccione" y False de lo contrario</param>
    ''' <param name="sSql">comando de sql</param>
    ''' <param name="sErr">descripción del error si lo hay</param>
    ''' <returns>True si la consulta es exitosa y False si ocurre un error</returns>
    ''' <remarks></remarks>
    Function ConsultaLista(ByVal oCtrl As ListBox, ByVal bSel As Boolean, ByVal sSql As String, ByRef sErr As String) As Boolean
        Dim bRes As Boolean
        Dim ds As New DataSet
        oCtrl.Items.Clear()
        If Consulta(sSql, ds, sErr) Then
            If ds.Tables.Count > 0 Then
                oCtrl.DataSource = ds.Tables(0)
                If ds.Tables(0).Columns.Count = 1 Then
                    oCtrl.DataTextField = ds.Tables(0).Columns(0).ColumnName
                Else
                    oCtrl.DataTextField = ds.Tables(0).Columns(1).ColumnName
                End If
                oCtrl.DataValueField = ds.Tables(0).Columns(0).ColumnName
                oCtrl.DataBind()
                If bSel Then oCtrl.Items.Insert(0, New ListItem("[Seleccione]", ""))
                bRes = True
            End If
            ds = Nothing
        End If
        Return bRes
    End Function

    ''' <summary>
    ''' Función que ejecuta una consulta y carga un control lista con los resultados
    ''' </summary>
    ''' <param name="oCtrl">lista a cargar</param>
    ''' <param name="bSel">True si muestra un elemento "Seleccione" y False de lo contrario</param>
    ''' <param name="sSql">comando de sql</param>
    ''' <param name="sErr">descripción del error si lo hay</param>
    ''' <returns>True si la consulta es exitosa y False si ocurre un error</returns>
    ''' <remarks></remarks>
    Function ConsultaListaChequeo(ByVal oCtrl As CheckBoxList, ByVal bSel As Boolean, ByVal sSql As String, ByRef sErr As String) As Boolean
        Dim bRes As Boolean
        Dim ds As New DataSet
        oCtrl.Items.Clear()
        If Consulta(sSql, ds, sErr) Then
            If ds.Tables.Count > 0 Then
                oCtrl.DataSource = ds.Tables(0)
                If ds.Tables(0).Columns.Count = 1 Then
                    oCtrl.DataTextField = ds.Tables(0).Columns(0).ColumnName
                Else
                    oCtrl.DataTextField = ds.Tables(0).Columns(1).ColumnName
                End If
                oCtrl.DataValueField = ds.Tables(0).Columns(0).ColumnName
                oCtrl.DataBind()
                If bSel Then oCtrl.Items.Insert(0, New ListItem("[Seleccione]", ""))
                bRes = True
            End If
            ds = Nothing
        End If
        Return bRes
    End Function

End Class