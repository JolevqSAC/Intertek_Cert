<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="personal.aspx.cs" Inherits="Intertek.WEB.Mantenimiento.personal" meta:resourcekey="PageResource1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            //CrearDialogo(nombreDIV, alto, ancho, titulo);
            CrearDialogo('msjSatisfactorio', 162, 360, '');
            CrearDialogo('msjCamposObligatorios', 162, 360, '');
        });

        function ValidaCampos() {
            var nombre = document.getElementById('<%=txtNombre.ClientID %>').value;
            var apellidos = document.getElementById('<%=txtApellidos.ClientID %>').value;
            var dni = document.getElementById('<%=txtDNI.ClientID %>').value;
            if (vacio(nombre) || vacio(apellidos) || vacio(dni))
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
                        <asp:Label ID="lblTitulo" runat="server" meta:resourcekey="lblTituloResource1" Text="Registrar Trabajador"></asp:Label></p>
                </div>
                <div class="ti2">
                    <p>
                    </p>
                </div>
            </div>
        </div>
        <div class="conteradio1">
            <div class="bar1">
                <div class="line173">
                    <p>
                        <asp:Label ID="lblNombre" runat="server" Text="Nombres: " meta:resourcekey="lblNombreResource1"></asp:Label></p>
                </div>
                <div class="line172">
                    <asp:TextBox ID="txtNombre" runat="server" CssClass="bordeimput" meta:resourcekey="txtNombreResource1"
                        MaxLength="50" Width="700px" Height="20px" 
                        onkeypress="javascript:return validarChr(event)" ></asp:TextBox>                   
                </div>
                <div class="line178"><p><asp:Label ID="Label1" runat="server" Text="*" class="letraError"></asp:Label></p></div>
            </div>
            <div class="bar2">
                <div class="line173">
                <p>
                    <asp:Label ID="lblApellidos" runat="server" Text="Apellidos: " 
                        meta:resourcekey="lblApellidosResource1"></asp:Label>
                </p>
                </div>
                <div class="line172">
                <asp:TextBox ID="txtApellidos" runat="server" CssClass="bordeimput" MaxLength="150"
                    Width="700px" Height="20px" meta:resourcekey="txtApellidosResource1" 
                        onkeypress="javascript:return validarChr(event)" ></asp:TextBox>
                </div>
                <div class="line178"><p><asp:Label ID="Label2" runat="server" Text="*" class="letraError"></asp:Label></p></div>
             </div>

            <div class="bar2">
                <div class="line173">
                    <p>
                        <asp:Label ID="lblDireccion" runat="server" Text="Dirección: " 
                            meta:resourcekey="lblDireccionResource1"></asp:Label></p>
                </div>
                <div class="line175">
                <asp:TextBox ID="txtDireccion" runat="server" CssClass="bordeimput" MaxLength="150" 
                        Width="700px" Height="20px" meta:resourcekey="txtDireccionResource1"></asp:TextBox>
               </div>
             </div>
              <div class="bar2">
                 <div class="line173">
                    <p>
                        <asp:Label ID="lblDNI" runat="server" Text="DNI: " 
                            meta:resourcekey="lblDNIResource1"></asp:Label></p>
                 </div>
                 <div class="line65">
                    <asp:TextBox ID="txtDNI" runat="server" CssClass="bordeimput" MaxLength="8" 
                         Width="200px" Height="20px" 
                         onkeypress="javascript:return validarNro(event)" 
                         meta:resourcekey="txtDNIResource1"></asp:TextBox>
                 </div>
                 <div class="line178"><p><asp:Label ID="Label3" runat="server" Text="*" class="letraError"></asp:Label></p></div>
                 <div class="line173">
                    <p>
                    <asp:Label ID="lblCargo" runat="server" Text="Cargo: " 
                            meta:resourcekey="lblCargoResource1"></asp:Label></p>
                    </div>
                    <div class="line176">
                        <asp:DropDownList runat="server" ID="ddlCargo" CssClass="bordeimput" 
                            Width="200px" meta:resourcekey="ddlCargoResource1"></asp:DropDownList>
                    </div>
                </div>
         <div class="bar2">
         <div class="line173">
                <p>
                    <asp:Label ID="lblPais" runat="server" Text="País: " 
                        meta:resourcekey="lblPaisResource1"></asp:Label></p>
            </div>
            <div class="line58">
                <asp:DropDownList runat="server" ID="ddlPais" Width="200px" 
                    CssClass="bordeimput" AutoPostBack="True" 
                    onselectedindexchanged="ddlPais_SelectedIndexChanged" Height="20px" 
                    meta:resourcekey="ddlPaisResource1" ></asp:DropDownList>
               </div>

               <div class="line173">
                <p>
                    <asp:Label ID="lblDepartamento" runat="server" Text="Departamento: " 
                        meta:resourcekey="lblDepartamentoResource1"></asp:Label></p>
            </div>
            <div class="line176">
                <asp:DropDownList runat="server" ID="ddlDepartamento" Width="200px" 
                    CssClass="bordeimput" AutoPostBack="True" 
                    onselectedindexchanged="ddlDepartamento_SelectedIndexChanged" 
                    meta:resourcekey="ddlDepartamentoResource1" ></asp:DropDownList>
               </div>

       </div>

       <div class="bar2">
         <div class="line173">
                <p>
                    <asp:Label ID="lblProvincia" runat="server" Text="Provincia: " 
                        meta:resourcekey="lblProvinciaResource1"></asp:Label></p>
            </div>
            <div class="line58">
                <asp:DropDownList runat="server" ID="ddlProvincia" Width="200px" 
                    CssClass="bordeimput" AutoPostBack="True" 
                    onselectedindexchanged="ddlProvincia_SelectedIndexChanged" Height="20px" 
                    meta:resourcekey="ddlProvinciaResource1" ></asp:DropDownList>
               </div>

               <div class="line173">
                <p>
                    <asp:Label ID="lblDistrito" runat="server" Text="Distrito: " 
                        meta:resourcekey="lblDistritoResource1"></asp:Label></p>
            </div>
            <div class="line176">
                <asp:DropDownList runat="server" ID="ddlDistrito" Width="200px" 
                    CssClass="bordeimput" Height="20px" 
                    meta:resourcekey="ddlDistritoResource1" ></asp:DropDownList>
               </div>
</div>
        <div style="clear: both">
        </div>
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
                                Text="El registro se Grabo satisfactoriamente" 
                                meta:resourcekey="lblConfirmacionResource1"></asp:Label></strong></p>
                    </div>
                    <div class="boxbtn">
                        <div class="centerclose">
                            <asp:Button ID="btnCerrarConfirmacion" OnClientClick="javascript:CerrarDialogoC('msjSatisfactorio','personalBuscar.aspx');"
                                runat="server" CssClass="<%$ Resources:generales,imgCerrar %>" 
                               CausesValidation="false"  meta:resourcekey="btnCerrarConfirmacionResource1" />
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
                            <strong><asp:Label ID="Label4" runat="server" 
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