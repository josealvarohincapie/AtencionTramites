<%@ Page Language="VB" %>
<script runat="server">

    Sub Button1_OnClick(sender As Object, e As EventArgs)
        Me.lblText1.Text = Request.Form("txtInput1")
        Me.lblText2.Text = Me.txtInput2.Value
    End Sub

</script>
<html>
<head>
    <title>ShotDev.Com Tutorial</title>
</head>
<body>
    <form runat="server">
        Text 1 = <input type="text" name="txtInput1" value="<%=Request.Form("txtInput1")%>" />
        <br />
        Text 2 = <input type="text" id="txtInput2" runat="server"/>
		<button id="Button1" runat="server" OnServerClick="Button1_OnClick">Click Here</button>
        <hr>
        <asp:Label id="lblText1" runat="server"></asp:Label>
        <br>
        <asp:Label id="lblText2" runat="server"></asp:Label>
    </form>
</body>
</html>
<!--- This file download from www.shotdev.com -->