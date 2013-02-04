<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="usuariosBuscar.aspx.cs" Inherits="Intertek.WEB.Seguridad.usuariosBuscar" meta:resourcekey="PageResource1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            //CrearDialogo(nombreDIV, alto, ancho, titulo);
            CrearDialogo('msjSatisfactorio', 162, 360, '');
            CrearDialogoEliminar('msjError', 162, 360, '');
            CrearDialogo('msjEliminarSeleccionados', 162, 360, '');
        });


        function VerificarSeleccionados() {

            var existenSeleccionados = false;

            $('#<%= gvBuscar.ClientID %> tr').each(function (indexF) {

                $(this).find('td').each(function (indexC) {

                    if (indexC == 3) {

                        var chk = $(this).find('input')[0];
                        if (chk != undefined) {
                            if (chk.checked == true) {
                                existenSeleccionados = true;
                            }
                        }
                    }
                });
            });

            if (existenSeleccionados == true) {
                MostrarMensajeEliminar('msjError', '');
            } else {
                MostrarMensaje('msjEliminarSeleccionados');
            }
        }

        function EliminarSi() {

            var idEliminar = $('#hdEliminarID').val();
            CerrarDialogo('msjError');
            $('#btnEliminar').trigger("click");
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="contentgeneral">
        <div class="titulo1">
            <div class="titulo2">
                <div class="ti1">
                    <p>
                        <asp:Label ID="lblTitulo" runat="server" Text="Buscar Usuarios" 
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
                        <asp:Label ID="lblRol" runat="server" Text="Rol:" 
                            meta:resourcekey="lblRolResource1"></asp:Label></p>
                </div>
                <div class="line175">
                    <asp:DropDownList runat="server" ID="ddlRol" CssClass="bordeimput" 
                        Width="300px" meta:resourcekey="ddlRolResource1">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="bar2">
                <div class="line173">
                    <p>
                        <asp:Label ID="lblUsuario" runat="server" Text="Usuario:" 
                            meta:resourcekey="lblUsuarioResource1"></asp:Label>
                    </p>
                </div>
                <div class="line175">
                    <asp:TextBox ID="txtUsuario" runat="server" CssClass="bordeimput" MaxLength="50"
                        Width="250px" meta:resourcekey="txtUsuarioResource1"></asp:TextBox></div>
            </div>
            <div class="bar2">
                 <div class="line173"><p>
                     <asp:Label ID="lblIndicadorSignatario" runat="server" 
                         Text="Indicador Signatario:" meta:resourcekey="lblIndicadorSignatarioResource1"></asp:Label> </p></div>
                 <div class="line175">
                      <asp:DropDownList runat="server" ID="ddlIndicadorSignatario" CssClass="bordeimput" 
                         Width="250px" meta:resourcekey="ddlIndicadorSignatarioResource1" >
                     </asp:DropDownList>
             </div>
        </div>
            
            <div style="clear: both">
            </div>
        </div>
        <div class="bar4">
            <div class="bar4tresili">
                <div class="btns2">
                    <asp:Button ID="btnBuscar" runat="server" CssClass="<%$ Resources:generales,imgBuscar %>"
                        OnClick="btnBuscar_Click" meta:resourcekey="btnBuscarResource1" /></div>
                <div class="btns3">
                    <asp:Button ID="btnLimpiar" runat="server" CssClass="<%$ Resources:generales,imgLimpiar %>"
                        OnClick="btnLimpiar_Click" meta:resourcekey="btnLimpiarResource1" /></div>
                <div class="btns3">
                    <asp:Button ID="btnNuevo" runat="server" CssClass="<%$ Resources:generales,imgNuevo %>"
                        OnClick="btnNuevo_Click" meta:resourcekey="btnNuevoResource1" />
                </div>
                <div class="btns3">
                    <asp:Button runat="server" ID="btnEliminar" ClientIDMode="Static" CssClass="ocultarBoton"
                        OnClick="btnEliminar_Click" meta:resourcekey="btnEliminarResource1" />
                    <img id="imgeliminar" runat="server" onclick="javascript:VerificarSeleccionados();" src="../img/btneliminar.png" alt="" class="imgCursor"/>
                </div>
            </div>
        </div>
        <div class="separa">
            <div class="conteradio1">
                <div class="bar5">
                    <center>
                    <asp:Label ID="lblmensaje" runat="server" CssClass="letraError" 
                             meta:resourcekey="lblmensajeResource1"></asp:Label> </center>
                        <asp:GridView ID="gvBuscar" runat="server" CssClass="gridview" AutoGenerateColumns="False"
                            AllowPaging="True" Width="500px" 
                        OnPageIndexChanging="gvBuscar_PageIndexChanging" 
                        meta:resourcekey="gvBuscarResource1" onrowcommand="gvBuscar_RowCommand" 
                        PageSize="20">
                            <Columns>
                                <asp:BoundField DataField="ROL_Nombre" HeaderText="Rol" 
                                    meta:resourcekey="BoundFieldResource1" />
                                <asp:BoundField DataField="USU_Login" HeaderText="Usuario" 
                                    meta:resourcekey="BoundFieldResource2" />
                                <asp:BoundField DataField="USU_IndicadorSignatario" HeaderText="Indicador Signatario" 
                                    ItemStyle-Width="90" meta:resourcekey="BoundFieldResource4" >
                                    <ItemStyle Width="90px"></ItemStyle>
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Selec." 
                                    meta:resourcekey="TemplateFieldResource1">
                                    <ItemTemplate>
                                        <center>
                                            <asp:CheckBox ID="chkSeleccion" runat="server" 
                                                meta:resourcekey="chkSeleccionResource1" />
                                            <asp:HiddenField ID="hidIdUsuario" runat="server" Value='<%# Eval("IDUsuario") %>' />
                                        </center>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField  
                                    meta:resourcekey="TemplateFieldResource2">
                                    <ItemTemplate>
                                        <center>
                                        <asp:LinkButton ID="LinkButton1" ToolTip="Modifcar" runat="server" CausesValidation="False"
                                          CommandName="Editar" CommandArgument='<%# Eval("IDUsuario") %>' 
                                        meta:resourcekey="hypActualizarResource1">
                                                <img src="/img/btnmdificar.png" alt="Modificar" style="border:0" /></asp:LinkButton>
                                        </center>
                                    </ItemTemplate>
                                    <ItemStyle Width="90px" />
                                </asp:TemplateField>
                            </Columns>
                            <PagerSettings Mode="NumericFirstLast" PageButtonCount="2" />
                        </asp:GridView>
                   
                </div>
                <div style="clear: both">
                    <asp:HiddenField runat="server" ClientIDMode="Static" ID="hdEliminarID" />
                </div>
            </div>
        </div>
    </div>
    <div id="msjError" style="display: none">
        <div class="contealertPregunta">
            <div class="boxtext">
                <div class="boxdentro">
                    <div class="boxletra">
                        <p>
                            <strong>
                                <asp:Label ID="lblEliminar" runat="server" 
                                Text="Desea eliminar el registro?" meta:resourcekey="lblEliminarResource1"></asp:Label></strong></p>
                    </div>
                    <div class="boxbtn">
                        <div class="centerclose">
                            <asp:Button ID="btnAceptar" OnClientClick="javascript:EliminarSi();" 
                                runat="server" CssClass="<%$ Resources:generales,imgSi %>" 
                                meta:resourcekey="btnAceptarResource1" />
                            <asp:Button ID="btnNo" OnClientClick="javascript:CerrarDialogo('msjError');" 
                                runat="server" CssClass="<%$ Resources:generales,imgNo %>" 
                                meta:resourcekey="btnNoResource1" />
                        </div>
                    </div>
                </div>
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
                                Text="El registro se eliminó satisfactoriamente" 
                                meta:resourcekey="lblConfirmacionResource1"></asp:Label></strong></p>
                    </div>
                    <div class="boxbtn">
                        <div class="centerclose">
                            <asp:Button ID="btnCerrarConfirmacion" OnClientClick="javascript:CerrarDialogo('msjSatisfactorio');"
                                runat="server" CssClass="<%$ Resources:generales,imgCerrar %>" 
                                meta:resourcekey="btnCerrarConfirmacionResource1" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="msjEliminarSeleccionados" style="display: none">
        <div class="contealert">
            <div class="boxtext">
                <div class="boxdentro">
                    <div class="boxletra2">
                        <p>
                            <strong>
                                <asp:Label ID="lbleliminarseleccionados" runat="server" 
                                Text="Debe seleccionar al menos un usuario para eliminar." 
                                meta:resourcekey="lbleliminarseleccionadosResource1"></asp:Label></strong></p>
                    </div>
                    <div class="boxbtn">
                        <div class="centerclose">
                            <asp:Button ID="btncerrarseleccionados" OnClientClick="javascript:CerrarDialogo('msjEliminarSeleccionados');"
                                runat="server" CssClass="<%$ Resources:generales,imgCerrar %>" 
                                meta:resourcekey="btncerrarseleccionadosResource1" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
