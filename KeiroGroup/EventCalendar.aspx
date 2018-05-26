<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EventCalendar.aspx.cs" Inherits="KeiroGroup.EventCalendar" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
        .auto-style1 {
            height: 889px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="auto-style1">
            <asp:Calendar ID="CLD_EventCalendar" runat="server" Height="765px" ShowGridLines="True" Width="1013px" Font-Size="Smaller" OnDayRender="CLD_EventCalendar_DayRender" DayNameFormat="Full" Font-Bold="True" Font-Underline="True" FirstDayOfWeek="Monday">
                <DayHeaderStyle BackColor="#6699FF" BorderStyle="Double" BorderWidth="1px" Font-Size="Larger" Height="10px" HorizontalAlign="Center" />
                <DayStyle VerticalAlign="Top" Font-Size="Larger" BackColor="White" BorderColor="#FF6600" Height="100px" HorizontalAlign="Center" Wrap="False" />




                <TitleStyle Font-Size="X-Large" Height="10px" />
                <TodayDayStyle BorderColor="#FF0066" BorderStyle="Groove" BorderWidth="5px" />
                <WeekendDayStyle BackColor="#FF99FF" />




            </asp:Calendar>
        </div>
    </form>
</body>
</html>
