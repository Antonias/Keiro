<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="wf_Schedule.aspx.cs" Inherits="KeiroGroup.wf_Schedule" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <br />
            <asp:TextBox ID="tb_TargetYear" runat="server" Width="73px"></asp:TextBox>
            　年　<asp:DropDownList ID="ddl_TargetMonth" runat="server">
                <asp:ListItem Enabled="False"></asp:ListItem>
                <asp:ListItem>1</asp:ListItem>
                <asp:ListItem>2</asp:ListItem>
                <asp:ListItem>3</asp:ListItem>
                <asp:ListItem>4</asp:ListItem>
                <asp:ListItem>5</asp:ListItem>
                <asp:ListItem>6</asp:ListItem>
                <asp:ListItem>7</asp:ListItem>
                <asp:ListItem>8</asp:ListItem>
                <asp:ListItem>9</asp:ListItem>
                <asp:ListItem>10</asp:ListItem>
                <asp:ListItem>11</asp:ListItem>
                <asp:ListItem>12</asp:ListItem>
            </asp:DropDownList>
            月<asp:Button ID="Button2" runat="server" OnClick="Button1_Click" Text="月予定表示" />
        </div>
        <br />
        <asp:Label ID="lbl_ActiveCalender" runat="server"></asp:Label>
        <br />
        <asp:GridView ID="drv_MonthlySchedule" runat="server">
        </asp:GridView>
    </form>
</body>
</html>
