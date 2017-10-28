<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="wf_EventDetail.aspx.cs" Inherits="KeiroGroup.wf_EventDetail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        </div>
        <asp:Label ID="lb_EventTytle" runat="server" Font-Bold="True" Font-Size="XX-Large" Text="Label"></asp:Label>
        <br />
        <br />
        開始日：<asp:Label ID="lb_EventStartDt" runat="server" Text="Label"></asp:Label>
        ～終了日：<asp:Label ID="lb_EventEndDt" runat="server" Text="Label"></asp:Label>
        <br />
        <br />
        <asp:Table ID="tbl_EventDetail" runat="server" BackColor="#999966" BorderColor="#660066" BorderStyle="Double" GridLines="Both">
        </asp:Table>
        <br />
        <br />
        <br />
        <br />
    </form>
</body>
</html>
