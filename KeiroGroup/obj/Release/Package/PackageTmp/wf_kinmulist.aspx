<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="wf_kinmulist.aspx.cs" Inherits="KeiroGroup.wf_kinmulist" %>

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
            職種：<asp:DropDownList ID="dl_KinmuListJa" runat="server">
            </asp:DropDownList>
            <asp:Button ID="bt_ViewKinmuList" runat="server" OnClick="bt_ViewKinmuList_Click" Text="勤務表表示" />
&nbsp;<br />
            <br />
            表示月：<asp:TextBox ID="tb_TargetYear" runat="server" Width="50px"></asp:TextBox>
            年<asp:DropDownList ID="ddl_TargetMonth" runat="server">
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
            月&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
            <br />
            <br />
            <asp:HyperLink ID="HL_EmployeeSchedule" runat="server">従業員予定</asp:HyperLink>
            <br />
        </div>
        <asp:Table ID="tbl_KinmuList" runat="server" BackColor="White" BorderColor="#000066" BorderStyle="Double" GridLines="Both">
        </asp:Table>
        <br />
        <br />
        白：実績未入力<br />
        <br />
        青：実績正常登録<br />
        <br />
        黄：勤務変更届出提出済み<br />
        <br />
        赤：勤務変更届未提出</form>
</body>
</html>
