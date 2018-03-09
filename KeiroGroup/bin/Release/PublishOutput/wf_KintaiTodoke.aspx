<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="wf_KintaiTodoke.aspx.cs" Inherits="KeiroGroup.wf_KintaiTodoke" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body style="height: 741px">
    <form id="form1" runat="server">
        社員名：<asp:Label ID="lbl_worker_name" runat="server" Text="Label"></asp:Label>
        <asp:GridView ID="gv_TodokedeDetail" runat="server">
        </asp:GridView>
        <br />
        <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="Larger" Font-Underline="True" Text="届出一欄"></asp:Label>
        <br />
        <br />
        <asp:Label ID="La_Tikoku" runat="server" Font-Underline="True" Text="遅刻届" Visible="False"></asp:Label>
        <br />
        <asp:Label ID="La_TikokuInfo" runat="server" BorderStyle="Groove" Text="Label" Visible="False"></asp:Label>
        <br />
        <asp:Label ID="La_Soutai" runat="server" Font-Underline="True" Text="早退届" Visible="False"></asp:Label>
        <br />
        <asp:Label ID="La_SoutaiInfo" runat="server" BorderStyle="Groove" Text="Label" Visible="False"></asp:Label>
        <br />
        <asp:Label ID="La_Henkou" runat="server" Font-Underline="True" Text="変更届" Visible="False"></asp:Label>
        <br />
        <asp:Label ID="La_HenkouInfo" runat="server" BorderStyle="Groove" Text="Label" Visible="False"></asp:Label>
        <br />
        <asp:Label ID="La_Kekkin" runat="server" Font-Underline="True" Text="欠勤届" Visible="False"></asp:Label>
        <br />
        <asp:Label ID="La_KekkinInfo" runat="server" BorderStyle="Groove" Text="Label" Visible="False"></asp:Label>
        <asp:Label ID="la_Yukyu" runat="server" Font-Bold="True" ForeColor="Red" Text="有給" Visible="False"></asp:Label>
        <br />
        <asp:Label ID="La_Zangyou" runat="server" Font-Underline="True" Text="残業届" Visible="False"></asp:Label>
        <br />
        <asp:Label ID="La_ZangyouInfo" runat="server" BorderStyle="Groove" Text="Label" Visible="False"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="Btn_ResetKintaiTodoke" runat="server" Text="削除" Height="23px" OnClick="Btn_ResetKintaiTodoke_Click" style="margin-top: 2px" Width="53px" />
        <br />
        <br />
        <asp:Panel ID="Panel1" runat="server" BackColor="#FF6600" BorderColor="Black" BorderStyle="Double" style="margin-right: 0px" Width="603px">
            　　　　　　　　　　　　　　　　　　　&nbsp;&nbsp; 届出入力<br /> 
            <br />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <br />
            &nbsp;&nbsp;
            <asp:Label ID="La_SinseiDay" runat="server" Text="申請日："></asp:Label>
            <br />
            　　　<br /> 　　
            <br />
            　　①<asp:Button ID="Btn_InputKekkinInfo" runat="server" Text="欠勤" OnClick="Btn_InputKekkinInfo_Click" />
            <asp:CheckBox ID="cb_Yukyu" runat="server" Text="有給届" />
            <br />
            <br />
            　　②<asp:TextBox ID="Tb_AdjustHour" runat="server" Width="34px"></asp:TextBox>
            &nbsp;<asp:Label ID="La_AdjustTime" runat="server" Text="時間"></asp:Label>
            &nbsp;&nbsp;<asp:TextBox ID="Tb_AdjustMinute" runat="server" Width="34px"></asp:TextBox>
            分&nbsp; &nbsp;
            <asp:Button ID="Btn_InputTikokuInfo" runat="server" Text="遅刻" OnClick="Btn_InputTikokuInfo_Click" style="height: 21px" />
            　<asp:Button ID="Btn_InputSoutaiInfo" runat="server" Text="早退" OnClick="Btn_InputSoutaiInfo_Click" />
            　<asp:Button ID="Btn_InputZangyouInfo" runat="server" Text="残業" OnClick="Btn_InputZangyouInfo_Click" />
            <br />
            <br />
            　　③<asp:TextBox ID="Tb_AfterStartHour" runat="server" Width="34px"></asp:TextBox>
            &nbsp;<asp:Label ID="La_AdjustTime1" runat="server" Text="時間"></asp:Label>
&nbsp;&nbsp;<asp:TextBox ID="Tb_AfterStartMinute" runat="server" Width="34px"></asp:TextBox>
            分 &nbsp;&nbsp;<asp:Label ID="La_ChangeTime" runat="server" Text="～"></asp:Label>
            &nbsp;&nbsp;
            <asp:TextBox ID="Tb_AfterEndHour" runat="server" Width="32px"></asp:TextBox>
            時間<asp:TextBox ID="Tb_AfterEndMinute" runat="server" Width="34px"></asp:TextBox>
            分に&nbsp;<asp:Button ID="Btn_InputHenkouInfo" runat="server" Height="23px" OnClick="Btn_InputHenkouInfo_Click" style="margin-top: 2px" Text="変更" Width="53px" />
            <br />
&nbsp;
            <br />
            &nbsp;&nbsp; 理由<br /> &nbsp; &nbsp;<asp:TextBox ID="Tb_HenkouRiyuu" runat="server" Width="508px"></asp:TextBox>
            &nbsp;

            <br />
        </asp:Panel>
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
    </form>
</body>
</html>
