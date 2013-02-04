<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Pruebas.aspx.cs" Inherits="Intertek.WEB.Pruebas" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="Origen" runat="server" Text="Origen"></asp:Label><asp:TextBox ID="txtOrigen"
            runat="server"></asp:TextBox><br />
            <asp:Label ID="Label2" runat="server" Text="Encriptado"></asp:Label>
        <asp:TextBox ID="txtResultado"
            runat="server" Width="460px"></asp:TextBox>
            <br />
        <asp:Button ID="btnResultado" runat="server" Text="Generar" 
            onclick="btnResultado_Click" />
            
    <asp:GridView ID="GridView1"  runat="server" Font-Names="Verdana, Geneva, sans-serif" Font-Size="10px" ForeColor="#333333" BorderStyle="Solid" BorderWidth="1px" BorderColor="#003976" >
    <HeaderStyle  Font-Names="Verdana, Geneva, sans-serif" Font-Size="10px" ForeColor="#333333" BackColor="Crimson" />
    <RowStyle Font-Names="Verdana, Geneva, sans-serif" Font-Size="10px" ForeColor="#333333"/>
    <AlternatingRowStyle Font-Names="Verdana, Geneva, sans-serif" Font-Size="10px" ForeColor="Blue"/>
    </asp:GridView>

    </div>
    </form>
</body>
</html>
