<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="wf_HenkouTodoke.aspx.cs" Inherits="KeiroGroup.wf_HenkouTodoke" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="height: 528px; margin-left: 0px; margin-top: 0px">
            <asp:Label ID="la_employee_name" runat="server" Font-Size="X-Large" Text="Label"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <br />
            <br />
            <br />
            出勤日：　 
             
            <asp:Label ID="lbl_ThisMonth" runat="server" Text="Label"></asp:Label>
            <br />
            届出理由：<asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
            <br />
            <br />
            種　類　　：<asp:DropDownList ID="ddl_TodokeType" runat="server" AutoPostBack="True" OnTextChanged="ddl_TodokeType_TextChanged">
                <asp:ListItem>欠勤</asp:ListItem>
                <asp:ListItem>早退</asp:ListItem>
                <asp:ListItem>遅刻</asp:ListItem>
                <asp:ListItem>残業</asp:ListItem>
                <asp:ListItem>変更</asp:ListItem>
            </asp:DropDownList>
            <asp:Button ID="cmb_InputTodokede" runat="server" Text="届出" />
            <br />
            <asp:TextBox ID="txt_TodokeHour" runat="server" Visible="False" Width="43px"></asp:TextBox>
            &nbsp;
            <asp:Label ID="lbl_TodokeHour" runat="server" Text="h" Visible="False"></asp:Label>
&nbsp;<asp:TextBox ID="txt_TodokeMinute" runat="server" Visible="False" Width="43px"></asp:TextBox>
            &nbsp;<asp:Label ID="lbl_TodokeMinute" runat="server" Text="mm" Visible="False"></asp:Label>
            <br />
            <asp:TextBox ID="txt_StartTodokeTime" runat="server" Width="47px" Visible="False"></asp:TextBox>
            <asp:Label ID="la_Kara" runat="server" Text="～"></asp:Label>
            <asp:TextBox ID="txt_EndTodokeTime" runat="server" Width="69px" Visible="False"></asp:TextBox>
            <br />
            <br />
            <br />
            <br />
            <br />
        </div>
    </form>
</body>
</html>
