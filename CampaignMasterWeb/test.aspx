﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="test.aspx.cs" Inherits="CampaignMasterWeb.CampaignMasterClientTest" %>

<%@ Register src="FieldControl.ascx" tagname="FieldControl" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <table>
    <tr>
    <td>
        <b>Player 1</b>
        <br />
        <asp:Button ID="Button1" runat="server" Text="Show Field" 
            onclick="Button1_Click" />
    </td>
    <td>
    
    <div>
        <uc1:FieldControl ID="FieldControl1" runat="server" />
    </div>

    </td>
    <td>
        <b>Player 2</b>
        <br />
        <asp:Button ID="Button2" runat="server" Text="Show Field" />
    </td>
    </tr>
    </table>
    </form>
    <p>
        &nbsp;</p>
</body>
</html>
