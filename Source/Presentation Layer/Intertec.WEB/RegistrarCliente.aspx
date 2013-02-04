<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RegistrarCliente.aspx.cs" Inherits="Intertek.WEB.Registrar_Cliente" Title="Página sin título" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<div class="contentgeneral">
    <div class="titulo1">
    <div class="titulo2">
       <div class="ti1"><p>Registrar Cliente</p></div>
       <div class="ti2"><p></p></div>
    </div>
</div>

<div class="conteradio1">
    <div class="bar1">
        <div class="line173"><p>Razón Social:</p></div>
        <div class="line175"><asp:TextBox ID="TextBox1" CssClass="bordeimput" Width="765px" Height="20px" runat="server"></asp:TextBox></div>
    </div>
    
    <div class="bar2">
        <div class="line173"><p>RUC:</p></div>
        <div class="line176"><asp:TextBox ID="TextBox2" CssClass="bordeimput" Width="105px" Height="20px" runat="server"></asp:TextBox></div>
        <div class="line177"><p>Teléfono:</p></div>
        <div class="line176"><asp:TextBox ID="TextBox3" CssClass="bordeimput" Width="105px" Height="20px" runat="server"></asp:TextBox></div>
        <div class="line178"><p>Fax:</p></div>
        <div class="line176"><asp:TextBox ID="TextBox4" CssClass="bordeimput" Width="105px" Height="20px" runat="server"></asp:TextBox></div>
    </div>
    
    <div class="bar2">
        <div class="line173"><p>E-mail:</p></div>
        <div class="line175"><asp:TextBox ID="TextBox5" CssClass="bordeimput" Width="455px" Height="20px" runat="server"></asp:TextBox></div>
    </div>
    
    <div class="bar2">
        <div class="line173"><p>Contacto:</p></div>
        <div class="line175"><asp:TextBox ID="TextBox6" CssClass="bordeimput" Width="455px" Height="20px" runat="server"></asp:TextBox></div>
    </div>
    
    <div class="bar2">
        <div class="line173"><p>Dirección:</p></div>
        <div class="line175"><asp:TextBox ID="TextBox7" CssClass="bordeimput" Width="455px" Height="20px" runat="server"></asp:TextBox></div>
    </div>
    
    <div class="bar2">
        <div class="line173"><p>Urbanización:</p></div>
        <div class="line179"><asp:DropDownList ID="DropDownList1" CssClass="bordeimput" Width="162px" Height="24px" runat="server"></asp:DropDownList></div>
        <div class="line180"><p>Departamentos:</p></div>
        <div class="line179"><asp:DropDownList ID="DropDownList2" CssClass="bordeimput" Width="162px" Height="24px" runat="server"></asp:DropDownList></div>
    </div>
    
    <div class="bar2">
        <div class="line173"><p>Distrito:</p></div>
        <div class="line179"><asp:DropDownList ID="DropDownList3" CssClass="bordeimput" Width="162px" Height="24px" runat="server"></asp:DropDownList></div>
        <div class="line180"><p>Provincias:</p></div>
        <div class="line179"><asp:DropDownList ID="DropDownList4" CssClass="bordeimput" Width="162px" Height="24px" runat="server"></asp:DropDownList></div>
        <div class="line181"><p>País:</p></div>
        <div class="line176"><asp:DropDownList ID="DropDownList5" CssClass="bordeimput" Width="105px" Height="20px" runat="server"></asp:DropDownList></div>
    </div>
    
    <div class="bar5option">
        <div class="line182"><p>Observaciones:</p></div>
        <div class="line93"><asp:TextBox ID="TextBox8" TextMode="MultiLine" CssClass="bordeimput" Width="639px" Height="76px" runat="server"></asp:TextBox></div>
    </div>
    
    <div style="clear:both"></div>
</div>

<div class="bar4">
    <div class="bar4tres">
        <div class="btns2"><asp:Button ID="Button1" CssClass="btngrabar" runat="server"/></div>
        <div class="btns3"><asp:Button ID="Button2" CssClass="btncancelar" runat="server"/></div>
        <div class="btns3"><asp:Button ID="Button3" CssClass="btnlimpiar" runat="server"/></div>
    </div>
</div>

</div>

</asp:Content>
