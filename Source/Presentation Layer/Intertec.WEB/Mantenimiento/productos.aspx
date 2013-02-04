<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="productos.aspx.cs" Inherits="Intertek.WEB.Mantenimiento.productos" meta:resourcekey="PageResource1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            //CrearDialogo(nombreDIV, alto, ancho, titulo);
            CrearDialogo('msjSatisfactorio', 162, 360, '');
            CrearDialogo('msjAlertaCaracteres', 162, 360, '');
            CrearDialogo('msjCamposObligatorios', 162, 360, '');
        });

        function ValidaCampos() {
            var nombre = document.getElementById('<%=txtNombre.ClientID %>').value;
            var ingles = document.getElementById('<%=txtingles.ClientID %>').value;
            var unidmedida = document.getElementById('<%=ddlUnidadMedida.ClientID %>').value;
            var categoria = document.getElementById('<%=ddlCategoria.ClientID %>').value;
            if (vacio(nombre) || vacio(ingles) || unidmedida==0 ||categoria==0)
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
                        <asp:Label ID="lblTitulo" runat="server" meta:resourcekey="lblTituloResource1" Text="Registrar Producto"></asp:Label></p>
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
                        <asp:Label ID="lblNombre" runat="server" Text="Nombre:" 
                            meta:resourcekey="lblNombreResource1"></asp:Label></p>
                </div>
                <div class="line26">
                    <asp:TextBox ID="txtNombre" CssClass="bordeimput" Width="470px"
                        Height="20px" runat="server" MaxLength="100" 
                        meta:resourcekey="txtNombreResource1"></asp:TextBox>
                </div>
                <div class="line8"><p><asp:Label ID="Label1" runat="server" Text="*" class="letraError"></asp:Label></p></div>
            </div>
             <div class="bar2">
                <div class="line173">
                    <p>
                        <asp:Label ID="lblIngles" runat="server" Text="Nombre en Inglés: " 
                            meta:resourcekey="lblInglesResource1"></asp:Label></p>
                </div>
                <div class="line26">
                    <asp:TextBox ID="txtingles" CssClass="bordeimput" Width="470px"
                        Height="20px" runat="server" MaxLength="100" 
                        meta:resourcekey="txtinglesResource1"></asp:TextBox>                    
                </div>
                <div class="line8"><p><asp:Label ID="Label2" runat="server" Text="*" class="letraError"></asp:Label></p></div>
            </div>
          
            <div class="bar5option">
                <div class="line182">
                     <p>
                        <asp:Label ID="lblDescripcion" runat="server" Text="Descripción: " 
                             meta:resourcekey="lblDescripcionResource1"></asp:Label>
                     </p>
                </div>
                <div class="line93">
                    <asp:TextBox ID="txtDescripcion" MaxLength="200" TextMode="MultiLine" 
                    CssClass="bordeimput" Width="639px" Height="76px" runat="server" 
                        onKeyUp="javascript:Count(this,200);" onChange="javascript:Count(this,200);" 
                        meta:resourcekey="txtDescripcionResource1"></asp:TextBox>
                </div>
            </div>
            
            <div class="bar2">
                <div class="line173">
                    <p>
                        <asp:Label ID="lblUnidadMedida" runat="server" Text="Unidad de Medida:" 
                            meta:resourcekey="lblUnidadMedidaResource1"></asp:Label>
                    </p>
                </div>
                <div class="line158">
                    <asp:DropDownList runat="server" ID="ddlUnidadMedida" CssClass="bordeimput" 
                        Width="250px" meta:resourcekey="ddlUnidadMedidaResource1">
                    </asp:DropDownList>                    
                </div>
                <div class="line8"><p><asp:Label ID="Label4" runat="server" Text="*" class="letraError"></asp:Label></p></div>
            </div>
            <div class="bar2">
                <div class="line173">
                    <p>
                        <asp:Label ID="lblCategoria" runat="server" Text="Categoría de Producto:" 
                            meta:resourcekey="lblCategoriaResource1"></asp:Label>
                    </p>
                </div>
                <div class="line158">
                    <asp:DropDownList runat="server" ID="ddlCategoria" CssClass="bordeimput" 
                        Width="250px" meta:resourcekey="ddlCategoriaResource1">
                    </asp:DropDownList>                   
                </div>
                <div class="line8"><p><asp:Label ID="Label5" runat="server" Text="*" class="letraError"></asp:Label></p></div>
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
                        meta:resourcekey="btnCancelarResource1" CausesValidation="False" OnClick="btnCancelar_Click"
                         /></div>
                <div class="btns3">
                    <asp:Button ID="btnLimpiar" runat="server" CssClass="<%$ Resources:generales,imgLimpiar %>"
                        meta:resourcekey="btnLimpiarResource1" CausesValidation="False" OnClick="btnLimpiar_Click"
                         /></div>
            </div>
        </div>
        <div class="bar4">
            <asp:Label ID="lblMensaje" runat="server" CssClass="letraError" 
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
                            <asp:Button ID="btnCerrarConfirmacion" OnClientClick="javascript:CerrarDialogoC('msjSatisfactorio','productosBuscar.aspx');"
                                runat="server" CssClass="<%$ Resources:generales,imgCerrar %>" 
                                CausesValidation="False" meta:resourcekey="btnCerrarConfirmacionResource1" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="msjAlertaCaracteres" style="display: none" title="Error">
        <div class="contealertError">
            <div class="boxtext">
                <div class="boxdentro">
                    <div class="boxletra2">
                         <p>
                         <strong>
                             <asp:Label ID="lblmsj1" runat="server" Text="Ud. debe ingresar: " 
                                 meta:resourcekey="lblmsj1Resource1"></asp:Label> 
                                <span id="spanCaracteres"></span>
                            <asp:Label ID="lblmsj2" runat="server" Text="caracteres." 
                                 meta:resourcekey="lblmsj2Resource1"></asp:Label>
                        </strong>
                         </p>
                    </div>
                    <div class="boxbtn">
                        <div class="centerclose">
                            <asp:Button runat="server" ID="btncerrarcaracteres" CssClass="<%$ Resources:generales,imgCerrar %>"
                                OnClientClick="CerrarDialogo('msjAlertaCaracteres');" Text="Cerrar" 
                                CausesValidation="False" meta:resourcekey="btncerrarcaracteresResource1"/>
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
                            <strong><asp:Label ID="Label3" runat="server" 
                                Text="Debe ingresar los campos obligatorios marcados con asterisco(*)" 
                                meta:resourcekey="lblCampoObligatorioResource1"></asp:Label></strong>
                         </p>
                    </div>
                    <div class="boxbtn">
                        <div class="centerclose">
                            <asp:Button runat="server" ID="btnCerrarObligatorios" CssClass="<%$ Resources:generales,imgCerrar %>"
                                OnClientClick="CerrarDialogo('msjCamposObligatorios');" 
                                CausesValidation="False" meta:resourcekey="btncerrarcaracteresResource1"/></div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
