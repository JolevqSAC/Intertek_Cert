<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="opcionesRol.aspx.cs" Inherits="Intertek.WEB.Seguridad.opcionesRol" meta:resourcekey="PageResource1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
<script language="javascript" type="text/javascript">
        $(document).ready(function () {

            //CrearDialogo(nombreDIV, alto, ancho, titulo);
            CrearDialogo('msjSatisfactorio', 162, 360, '');
            CrearDialogo('msjCamposObligatorios', 162, 360, '');
        });

        function ValidaCampos() {
            var rol = document.getElementById('<%=txtRol.ClientID %>').value;
            if (vacio(rol))
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
                        <asp:Label ID="lblTitulo" runat="server" Text="Registrar Rol" 
                            meta:resourcekey="lblTituloResource1"></asp:Label></p>
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
                        <asp:Label ID="lblNombre" runat="server" Text="Rol :" 
                            meta:resourcekey="lblNombreResource1"></asp:Label></p>
                </div>
                <div class="line158">
                    <asp:TextBox ID="txtRol" runat="server" CssClass="bordeimput" 
                        meta:resourcekey="txtRolResource1" Width="250px" MaxLength="100" 
                        Height="20px"></asp:TextBox>
                  </div>
                  <div class="line8"><p><asp:Label ID="Label2" runat="server" Text="*" class="letraError"></asp:Label></p></div>
            </div>
            <div class="bar2">
                <div class="line173">
                    <p>
                        <asp:Label ID="lblDescripcion" runat="server" Text="Descripción :" meta:resourcekey="lblDescripcionResource1" 
                            ></asp:Label></p>
                </div>
                <div class="line175">
                    <asp:TextBox ID="txtDescripcion" runat="server" CssClass="bordeimput" 
                        Width="700px" MaxLength="200" TextMode="MultiLine" onKeyUp="javascript:Count(this,200);"
                            onChange="javascript:Count(this,200);"
                        meta:resourcekey="txtDescripcionResource1" 
                        ></asp:TextBox>
                 </div>
            </div>
            <div class="bar2Auto">
                <div class="line173"><p><asp:Label ID="lblOpciones" runat="server" 
                        Text="Opciones :" meta:resourcekey="lblOpcionesResource1"></asp:Label></p></div>
                <div class="line175Auto">
                    <asp:TreeView ID="tviewOpciones" runat="server" ShowCheckBoxes="Leaf" 
                        meta:resourcekey="tviewOpcionesResource1" 
                        Width="650px">
                    </asp:TreeView>
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
                        meta:resourcekey="btnCancelarResource1" onclick="btnCancelar_Click" 
                        CausesValidation="False" OnClientClick="javascript:DesactivarValidacion();" /></div>
                <div class="btns3">
                    <asp:Button ID="btnLimpiar" runat="server" CssClass="<%$ Resources:generales,imgLimpiar %>"
                        meta:resourcekey="btnLimpiarResource1" onclick="btnLimpiar_Click" 
                        CausesValidation="False" OnClientClick="javascript:DesactivarValidacion();"/></div>
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
                                Text="El registro se grabó satisfactoriamente" 
                                meta:resourcekey="lblConfirmacionResource1"></asp:Label></strong></p>
                    </div>
                    <div class="boxbtn">
                        <div class="centerclose">
                            <asp:Button ID="btnCerrarConfirmacion" OnClientClick="javascript:CerrarDialogoC('msjSatisfactorio','opcionRolBuscar.aspx');"
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