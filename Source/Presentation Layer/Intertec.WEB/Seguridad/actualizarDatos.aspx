<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="actualizarDatos.aspx.cs" Inherits="Intertek.WEB.Seguridad.actualizarDatos" meta:resourcekey="PageResource1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
<script language="javascript" type="text/javascript">
    $(document).ready(function () {

        //CrearDialogo(nombreDIV, alto, ancho, titulo);
        CrearDialogo('msjSatisfactorio', 162, 360, '');
        CrearDialogo('msjErrorGrabar', 162, 360, '');
        CrearDialogo('msjCamposObligatorios', 162, 360, '');

    });

    function ValidaCampos() {
        var usuario = document.getElementById('<%=txtUsuario.ClientID %>').value;
        var pass = document.getElementById('<%=txtPassword.ClientID%>').value;

        if (vacio(usuario) || vacio(pass))
            MostrarMensaje('msjCamposObligatorios');
        else {
            $('#btnGrabar').trigger("click");
            return true;
        }
    }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="contentgeneral">
        <div class="titulo1">
            <div class="titulo2">
                <div class="ti1">
                    <p>
                        <asp:Label ID="lblTitulo" runat="server" meta:resourcekey="lblTituloResource1" 
                            Text="Actualización de Datos"></asp:Label></p>
                </div>
                <div class="ti2">
                    <p>
                    </p>
                </div>
            </div>
        </div>
         <div class="conteradio1">
              <div class="bar1">
                <div class="line173"><p><asp:Label ID="lblUsuario" runat="server" Text="Usuario: " 
                        meta:resourcekey="lblUsuarioResource1"></asp:Label></p></div>
                <div class="line58"><asp:TextBox ID="txtUsuario" CssClass="bordeimput" 
                        Width="250px" Height="20px" runat="server" MaxLength="50" 
                        meta:resourcekey="txtUsuarioResource1"></asp:TextBox>
                </div>
                <div class="line8"><p><asp:Label ID="Label12" runat="server" Text="*" class="letraError"></asp:Label></p></div>  
            </div>
              <div class="bar2">
                <div class="line173"><p><asp:Label ID="lblPassword" runat="server" 
                        Text="Password: " meta:resourcekey="lblPasswordResource1"></asp:Label></p></div>
                <div class="line58"><asp:TextBox ID="txtPassword" CssClass="bordeimput" 
                        Width="250px" Height="20px" runat="server" TextMode="Password" 
                        MaxLength="10" meta:resourcekey="txtPasswordResource1"></asp:TextBox>
                </div>
                <div class="line8"><p><asp:Label ID="Label1" runat="server" Text="*" class="letraError"></asp:Label></p></div>  
            </div>
              
     <div style="clear:both"></div>

         </div>

             <div class="bar4">
        <div class="bar4tres">
            <div class="btns2">
                    <asp:Button ID="btnGrabar" runat="server"  CssClass="ocultarBoton" ClientIDMode="Static"
                        onclick="btnGrabar_Click" meta:resourcekey="btnGrabarResource1"/>
                    <img id="imgGrabar" runat="server" src="../img/btngrabar2.png" onclick="javascript:return ValidaCampos()" alt="" class="imgCursor"/>         
                </div> 
            <div class="btns3">
                <asp:Button ID="btnCancelar" runat="server" CssClass="<%$ Resources:generales,imgCancelar %>"
                    meta:resourcekey="btnCancelarResource1" CausesValidation="False" 
                    OnClick="btnCancelar_Click" OnClientClick="javascript:DesactivarValidacion();"/></div>
        </div>
    </div>

        </div>

        <div id="msjSatisfactorio" style="display: none">
        <div class="contealert">
            <div class="boxtext">
                <div class="boxdentro">
                    <div class="boxletra">
                        <p>
                            <strong>
                                <asp:Label ID="lblConfirmacion" runat="server" 
                                Text="El registro se Grabo satisfactoriamente" 
                                meta:resourcekey="lblConfirmacionResource1"></asp:Label></strong></p>
                    </div>
                    <div class="boxbtn">
                        <div class="centerclose">
                            <asp:Button ID="btnCerrarConfirmacion" 
                                OnClientClick="javascript:CerrarDialogoC('msjSatisfactorio','../inicio.aspx');" CausesValidation="False"
                                runat="server" CssClass="<%$ Resources:generales,imgCerrar %>" 
                                meta:resourcekey="btnCerrarConfirmacionResource1" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="_dialog" id="msjErrorGrabar" style="display: none" title="Error">
        <div class="contealertError">
            <div class="boxtext">
                <div class="boxdentro">
                    <div class="boxletra">
                        <p>
                            <strong><asp:Label ID="lblErrorGrabar" runat="server" 
                                Text="Error al grabar el registro." meta:resourcekey="lblErrorGrabarResource1"></asp:Label></strong></p>
                    </div>
                    <div class="boxbtn">
                        <div class="centerclose">
                            <asp:Button runat="server" ID="btnCerrar" CssClass="<%$ Resources:generales,imgCerrar %>"
                                OnClientClick="CerrarDialogo('msjErrorGrabar');" CausesValidation="False"
                                meta:resourcekey="btnCerrarResource1" /></div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="msjCamposObligatorios" style="display: none" title="Error">
        <div class="contealertError">
            <div class="boxtext">
                <div class="boxdentro">
                    <div class="boxletra2">
                         <p>
                            <strong><asp:Label ID="Label10" runat="server" 
                                Text="Debe ingresar los campos obligatorios marcados con asterisco(*)" 
                                meta:resourcekey="lblCampoObligatorioResource1"></asp:Label></strong>
                         </p>
                    </div>
                    <div class="boxbtn">
                        <div class="centerclose">
                            <asp:Button runat="server" ID="btnCerrarObligatorios" CssClass="<%$ Resources:generales,imgCerrar %>"
                                OnClientClick="CerrarDialogo('msjCamposObligatorios');" 
                                CausesValidation="False" meta:resourcekey="btnCerrarResource1"/></div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
