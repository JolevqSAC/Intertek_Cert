<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BuscarCliente.aspx.cs" Inherits="Intertek.WEB.BuscaCliente" Title="Página sin título" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<div class="contentgeneral">

<div class="titulo1">
    <div class="titulo2">
       <div class="ti1"><p>Buscar Cliente</p></div>
       <div class="ti2"><p></p></div>
    </div>
</div>

<div class="conteradio1">

    <div class="bar1">
        <div class="line171"><p>Razón:</p></div>
        <div class="line172"><asp:TextBox ID="TextBox1" CssClass="bordeimput" Width="700px" Height="20px" runat="server"></asp:TextBox></div>
    </div>
    
    <div class="bar2">
        <div class="line171"><p>RUC:</p></div>
        <div class="line172"><asp:TextBox ID="TextBox2" CssClass="bordeimput" Width="700px" Height="20px" runat="server"></asp:TextBox></div>
    </div>
    
    <div class="bar2">
        <div class="line171"><p>Dirección:</p></div>
        <div class="line172"><asp:TextBox ID="TextBox3" CssClass="bordeimput" Width="700px" Height="20px" runat="server"></asp:TextBox></div>
    </div>
        
    <div style="clear:both"></div>

</div>

<div class="bar4">
    <div class="bar4tres">
        <div class="btns2"><asp:Button ID="Button1" CssClass="btnbuscar" runat="server"/></div>
        <div class="btns3"><asp:Button ID="Button2" CssClass="btnlimpiar" runat="server"/></div>
        <div class="btns3"><asp:Button ID="Button3" CssClass="btnnuevo" runat="server"/></div>
    </div>
</div>

<div class="separa">
<div class="conteradio1">
    <div class="bar5">
        <table align="center" class="bordetabla" cellpadding="6" cellspacing="0">
            <tr class="letra">
                <td style="width:50px" class="paratd">ID</td>
                <td style="width:150px" class="paratd">Razón Social</td>
                <td style="width:200px" class="paratd">Ruc</td>
                <td style="width:200px" class="paratd">Dirección</td>
                <td style="width:200px" class="paratd2">Rubro</td>
            </tr>
            
            <tr>
                <td class="paratd3"><br /></td>
                <td class="paratd3"></td>
                <td class="paratd3"></td>
                <td class="paratd3"></td>
                <td></td>
            </tr>
        </table>
    </div>
    
    <div style="clear:both"></div>
</div>
</div>

<div class="bar4">
    <div class="bar4dentro">
        <div class="btns2"><asp:Button ID="Button4" CssClass="btnmodificar" runat="server"/></div>
        <div class="btns3"><asp:Button ID="Button5" CssClass="btneliminar" runat="server"/></div>
    </div>
</div>

</div>

</asp:Content>
