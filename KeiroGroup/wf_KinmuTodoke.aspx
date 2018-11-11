<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="wf_KinmuTodoke.aspx.cs" Inherits="KeiroGroup.Kintai.wf_KinmuTodoke" %>

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
            <asp:Label ID="la_employee_name" runat="server" BorderStyle="Solid" Text="Label"></asp:Label>
            <br />
            <br />
            <asp:Table ID="tbl_KinmuList" runat="server" GridLines="Both">
            </asp:Table>
            <br />
            <asp:Button ID="Cmd_checkAllYotei" runat="server" OnClick="Cmd_checkAllYotei_Click" Text="全てチェック" />
            <br />
            <asp:Button ID="Cmd_inputYotei" runat="server" OnClick="Cmd_inputYotei_Click" Text="予定を登録" Width="128px" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="Cmd_updateYotei" runat="server" OnClick="Cmd_updateZisseki_Click" Text="勤務時間登録" />
            <br />
            <br />
            <br />
            勤務変更届出一欄<br />
            <asp:GridView ID="Gv_todokedeinfo" runat="server">
            </asp:GridView>
            <br />
            <br />
            <br />
        </div>
    </form>
    <p>
        &nbsp;</p>
</body>
</html>
