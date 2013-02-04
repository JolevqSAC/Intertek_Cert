<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="usuarios.aspx.cs" Inherits="Intertek.WEB.Seguridad.usuarios" meta:resourcekey="PageResource1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {

            //CrearDialogo(nombreDIV, alto, ancho, titulo);
            CrearDialogo('msjSatisfactorio', 162, 360, '');
            CrearDialogo('msjCamposObligatorios', 162, 360, '');
        });
        function ValidaCampos() {
            var usuario = document.getElementById('<%=txtUsuario.ClientID %>').value;
            var pass = document.getElementById('<%=txtPassword.ClientID %>').value;
            var personal = document.getElementById('<%=ddlPersonal.ClientID %>').value;
            var rol = document.getElementById('<%=ddlRol.ClientID %>').value;
            var radiolistbtn = document.getElementById('<%= rbIndicadorSignatario.ClientID %>');
            if (vacio(usuario) || vacio(pass) || personal == 0 || rol == 0 || rblSelectedValue(radiolistbtn))
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
                            Text="Registrar Usuario"></asp:Label></p>
                </div>
                <div class="ti2">
                    <p>
                    </p>
                </div>
            </div>
        </div>
         <div class="conteradio1">
              <div class="bar1">
                <div class="line173"><p><asp:Label ID="lblUsuario" runat="server" Text="Login: " 
                        meta:resourcekey="lblUsuarioResource1"></asp:Label></p></div>
                <div class="line158"><asp:TextBox ID="txtUsuario" CssClass="bordeimput" 
                        Width="250px" Height="20px" runat="server" MaxLength="50" 
                        meta:resourcekey="txtUsuarioResource1"></asp:TextBox>
                </div>
                <div class="line8"><p><asp:Label ID="Label1" runat="server" Text="*" class="letraError"></asp:Label></p></div>
            </div>
              <div class="bar2">
                <div class="line173"><p><asp:Label ID="lblPassword" runat="server" 
                        Text="Password: " meta:resourcekey="lblPasswordResource1"></asp:Label></p></div>
                <div class="line158"><asp:TextBox ID="txtPassword" CssClass="bordeimput" 
                        Width="250px" Height="20px" runat="server" TextMode="Password" 
                        MaxLength="10" meta:resourcekey="txtPasswordResource1"></asp:TextBox>               
                </div>
                <div class="line8"><p><asp:Label ID="Label2" runat="server" Text="*" class="letraError"></asp:Label></p></div>
            </div>
              <div class="bar2">
                <div class="line173"><p>
                    <asp:Label ID="lblPersonal" runat="server" 
                        Text="Trabajador:" meta:resourcekey="lblPersonalResource1"></asp:Label></p></div>
                <div class="line172">
                    <asp:DropDownList runat="server" ID="ddlPersonal" CssClass="bordeimput" 
                    Width="700px" meta:resourcekey="ddlPersonalResource1"></asp:DropDownList>
                    </div>
                <div class="line8"><p><asp:Label ID="Label3" runat="server" Text="*" class="letraError"></asp:Label></p></div>
            </div>
            <div class="bar2">
                <div class="line173"><p><asp:Label ID="lblRol" runat="server" Text="Rol: " 
                        meta:resourcekey="lblRolResource1"></asp:Label></p></div>
                <div class="line175">
                    <asp:DropDownList runat="server" ID="ddlRol" CssClass="bordeimput" 
                    Width="700px" meta:resourcekey="ddlRolResource1"></asp:DropDownList>
                    <asp:CompareValidator ID="cvRol" runat="server" CssClass="letraError" 
                        ErrorMessage="*" ControlToValidate="ddlRol" ValueToCompare="0" 
                        Operator="NotEqual" Type="Integer" meta:resourcekey="cvRolResource1" ></asp:CompareValidator>
                    
                    </div>
            </div>

              <div class="bar2">
                     <div class="line173"><p>
                         <asp:Label ID="lblIndicadorSignatario" runat="server" 
                         Text="Indicador Signatario:" meta:resourcekey="lblIndicadorSignatarioResource1" ></asp:Label> </p></div>
                    <div class="line57"> 
                        <asp:RadioButtonList ID="rbIndicadorSignatario" runat="server" 
                            RepeatDirection="Horizontal" CssClass="letra2" 
                            meta:resourcekey="rbIndicadorSignatarioResource1">
                            <asp:ListItem runat="server" Text="Si" Value="Si" 
                                meta:resourcekey="ListItemResource1"></asp:ListItem>
                            <asp:ListItem runat="server" Text="No" Value="No" 
                                meta:resourcekey="ListItemResource2"></asp:ListItem>
                        </asp:RadioButtonList>                
                    </div>         
                      <div class="line8"><p><asp:Label ID="Label4" runat="server" Text="*" class="letraError"></asp:Label></p></div>
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
            <div class="btns3">
                <asp:Button ID="btnLimpiar" runat="server" CssClass="<%$ Resources:generales,imgLimpiar %>"
                    meta:resourcekey="btnLimpiarResource1" CausesValidation="False" 
                    OnClick="btnLimpiar_Click" OnClientClick="javascript:DesactivarValidacion();"/></div>
        </div>
    </div>

    <div class="bar4">
        <asp:Label ID="lblMensaje" runat="server" 
            meta:resourcekey="lblMensajeResource1"></asp:Label>
        
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
                                Text="El registro se grabó satisfactoriamente" 
                                meta:resourcekey="lblConfirmacionResource1"></asp:Label></strong></p>
                    </div>
                    <div class="boxbtn">
                        <div class="centerclose">
                            <asp:Button ID="btnCerrarConfirmacion" OnClientClick="javascript:CerrarDialogoC('msjSatisfactorio','usuariosBuscar.aspx');"
                                runat="server" CausesValidation="False" 
                                CssClass="<%$ Resources:generales,imgCerrar %>" 
                                meta:resourcekey="btnCerrarConfirmacionResource1" />
                        </div>
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
                            <strong><asp:Label ID="Label9" runat="server" 
                                Text="Debe ingresar los campos obligatorios marcados con asterisco(*)" 
                                meta:resourcekey="lblCampoObligatorioResource1"></asp:Label></strong>
                         </p>
                    </div>
                    <div class="boxbtn">
                        <div class="centerclose">
                            <asp:Button runat="server" ID="btnCerrarObligatorios" CssClass="<%$ Resources:generales,imgCerrar %>"
                                OnClientClick="CerrarDialogo('msjCamposObligatorios');" 
                                CausesValidation="False" meta:resourcekey="btnCerrarConfirmacionResource1"/></div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
