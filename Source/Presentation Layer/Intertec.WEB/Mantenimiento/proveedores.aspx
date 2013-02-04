<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="proveedores.aspx.cs" Inherits="Intertek.WEB.Mantenimiento.proveedores" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            //CrearDialogo(nombreDIV, alto, ancho, titulo);
            CrearDialogo('msjSatisfactorio', 162, 360, '');
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="contentgeneral">
        <div class="titulo1">
            <div class="titulo2">
                <div class="ti1">
                    <p>
                        <asp:Label ID="lblTitulo" runat="server" meta:resourcekey="lblTituloResource1" Text="Proveedor"></asp:Label></p>
                </div>
                <div class="ti2">
                    <p>
                    </p>
                </div>
            </div>
        </div>
<div class="conteradio1">
    <div class="bar1">
        <div class="line173"><p> <asp:Label ID="lblRazonSocial" runat="server" Text="Razón Social: "></asp:Label></p></div>
        <div class="line175"><asp:TextBox ID="txtRazonSocial" MaxLength="150" 
                CssClass="bordeimput" Width="750px" Height="20px" runat="server"></asp:TextBox>
                 <asp:RequiredFieldValidator ID="rfvRazonSocial" runat="server" ErrorMessage="*" ControlToValidate="txtRazonSocial"
                        CssClass="letraError"></asp:RequiredFieldValidator>
                </div>
    </div>
    
    <div class="bar2">
        <div class="line173"><p><asp:Label ID="lblRUC" runat="server" Text="RUC: "></asp:Label></p></div>
        <div class="line174"><asp:TextBox ID="txtRUC" MaxLength="11" CssClass="bordeimput" Width="100px" Height="20px" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvRUC" runat="server" ErrorMessage="*" ControlToValidate="txtRUC"   CssClass="letraError"></asp:RequiredFieldValidator>
        </div>
        <div class="line177_2"><p><asp:Label ID="lblTelefono1" runat="server" Text="Telf. Oficina: "></asp:Label></p></div>
        <div class="line176"><asp:TextBox ID="txtTelefono1" MaxLength="20" CssClass="bordeimput" Width="105px" Height="20px" runat="server"></asp:TextBox></div>
         <div class="line177_2"><p><asp:Label ID="lblTelefonoPlanta" runat="server" Text="Telf. Planta: "></asp:Label></p></div>
        <div class="line176"><asp:TextBox ID="txtTelefono2" MaxLength="20" CssClass="bordeimput" Width="105px" Height="20px" runat="server"></asp:TextBox></div>
        <div class="line178"><p><asp:Label ID="lblFax" runat="server" Text="Fax: "></asp:Label></p></div>
        <div class="line176"><asp:TextBox ID="txtFax" MaxLength="20" CssClass="bordeimput" Width="105px" Height="20px" runat="server"></asp:TextBox></div>
    </div>
    
    <div class="bar2">
        <div class="line173"><p><asp:Label ID="lblEmail" runat="server" Text="E-mail: "></asp:Label></p></div>
        <div class="line175"><asp:TextBox ID="txtEmail" MaxLength="50" CssClass="bordeimput" Width="455px" Height="20px" runat="server"></asp:TextBox></div>
    </div>
    
    <div class="bar2">
        <div class="line173"><p><asp:Label ID="lblContacto" runat="server" Text="Contacto: "></asp:Label></p></div>
        <div class="line175"><asp:TextBox ID="txtContacto"  MaxLength="150"  CssClass="bordeimput" Width="455px" Height="20px" runat="server"></asp:TextBox></div>
    </div>
    
    <div class="bar2">
        <div class="line173"><p><asp:Label ID="lblDireccion" runat="server" Text="Dirección: "></asp:Label></p></div>
        <div class="line175"><asp:TextBox ID="txtDireccion" MaxLength="150" CssClass="bordeimput" Width="455px" Height="20px" runat="server"></asp:TextBox></div>
    </div>
    
    <div class="bar2">
        <div class="line173"><p><asp:Label ID="lblPais" runat="server" Text="País: "></asp:Label></p></div>
        <div class="line179"><asp:DropDownList ID="ddlPais" CssClass="bordeimput" 
                Width="162px" Height="24px" runat="server" AutoPostBack="True" 
                onselectedindexchanged="ddlPais_SelectedIndexChanged"></asp:DropDownList></div>
        <div class="line180"><p><asp:Label ID="lblDepartamento" runat="server" Text="Departamentos: "></asp:Label></p></div>
        <div class="line179"><asp:DropDownList ID="ddlDepartamento" CssClass="bordeimput" 
                Width="162px" Height="24px" runat="server" AutoPostBack="True" 
                onselectedindexchanged="ddlDepartamento_SelectedIndexChanged"></asp:DropDownList></div>
    </div>
    
    <div class="bar2">
        <div class="line173"><p><asp:Label ID="lblProvincia" runat="server" Text="Provincias: "></asp:Label></p></div>
        <div class="line179"><asp:DropDownList ID="ddlProvincia" CssClass="bordeimput" 
                Width="162px" Height="24px" runat="server" AutoPostBack="True" 
                onselectedindexchanged="ddlProvincia_SelectedIndexChanged"></asp:DropDownList></div>
        <div class="line180"><p><asp:Label ID="lblDistrito" runat="server" Text="Distrito: "></asp:Label></p></div>
        <div class="line179"><asp:DropDownList ID="ddlDistrito" CssClass="bordeimput" Width="162px" Height="24px" runat="server"></asp:DropDownList></div>
        
    </div>
    
    <div class="bar5option">
        <div class="line182"><p><asp:Label ID="lblObservaciones" runat="server" Text="Observaciones: "></asp:Label></p></div>
        <div class="line93"><asp:TextBox ID="txtObservaciones" MaxLength="200" TextMode="MultiLine" CssClass="bordeimput" Width="639px" Height="76px" runat="server"></asp:TextBox></div>
    </div>
    <div class="bar2">
         <div >
            <asp:Label ID="lblCampoObligatorio" runat="server" Text="(*) Campos obligatorios" class="letraError"></asp:Label>
      </div>
    </div>
    
    <div style="clear:both"></div>
</div>
<div class="bar4">
            <div class="bar4tres">
                <div class="btns2">
                    <asp:Button ID="btnGrabar" runat="server" Text="" CssClass="<%$ Resources:generales,imgGrabar %>"
                        meta:resourcekey="btnGrabarResource1" OnClick="btnGrabar_Click" /></div>
                <div class="btns3">
                    <asp:Button ID="btnCancelar" runat="server" Text="" CssClass="<%$ Resources:generales,imgCancelar %>"
                        meta:resourcekey="btnCancelarResource1" CausesValidation="False" OnClick="btnCancelar_Click"
                        OnClientClick="javascript:DesactivarValidacion();" /></div>
                <div class="btns3">
                    <asp:Button ID="btnLimpiar" runat="server" Text="" CssClass="<%$ Resources:generales,imgLimpiar %>"
                        meta:resourcekey="btnLimpiarResource1" CausesValidation="False" OnClick="btnLimpiar_Click"
                        OnClientClick="javascript:DesactivarValidacion();" /></div>
            </div>
        </div>
        <div class="bar4">
            <asp:Label ID="lblMensaje" runat="server" Text="" CssClass="letraError"></asp:Label>
        </div>
        </div>
        <div id="msjSatisfactorio" style="display: none">
        <div class="contealert">
            <div class="boxtext">
                <div class="boxdentro">
                    <div class="boxletra">
                        <p>
                            <strong>
                                <asp:Label ID="lblConfirmacion" runat="server" Text="El registro se Grabo satisfactoriamente"></asp:Label></strong></p>
                    </div>
                    <div class="boxbtn">
                        <div class="centerclose">
                            <asp:Button ID="btnCerrarConfirmacion" OnClientClick="javascript:CerrarDialogo('msjSatisfactorio');"
                                runat="server" Text="" CssClass="<%$ Resources:generales,imgCerrar %>" CausesValidation="false" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
