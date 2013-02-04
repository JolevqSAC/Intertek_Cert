<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="proveedoresBuscar.aspx.cs" Inherits="Intertek.WEB.Mantenimiento.proveedoresBuscar" meta:resourcekey="PageResource1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            //CrearDialogo(nombreDIV, alto, ancho, titulo);
            CrearDialogo('msjSatisfactorio', 162, 360, '');
            CrearDialogoEliminar('msjError', 162, 360, '');
            CrearDialogo('msjEliminarSeleccionados', 162, 360, '');

            jQuery('#' + '<%=btnEliminar0.ClientID%>').click(function (event) {
                // event.preventDefault();
                VerificarSeleccionados();

            });

        });

        function VerificarSeleccionados() {

            var existenSeleccionados = false;

            $('#<%= gvBuscar.ClientID %> tr').each(function (indexF) {

                $(this).find('td').each(function (indexC) {

                    if (indexC == 4) {
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
                //  alert("Debe seleccionar al menos un producto para eliminar.");
                MostrarMensaje('msjEliminarSeleccionados');
            }
        }



        function EliminarSi() {
            var idRolEliminar = $('#hdEliminarID').val();
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
                        <asp:Label ID="lblTitulo" runat="server" Text="Buscar Proveedores" 
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
                        <asp:Label ID="Label1" runat="server" Text="Código:" 
                            meta:resourcekey="Label1Resource1"></asp:Label>
                    </p>
                </div>
                <div class="line175">
                    <asp:TextBox ID="txtCodigo" runat="server" CssClass="bordeimput" MaxLength="10"
                        Width="300px" meta:resourcekey="txtCodigoResource1"></asp:TextBox></div>
            </div>
            <div class="bar2">
                <div class="line173">
                    <p>
                        <asp:Label ID="lblRazonSocial" runat="server" Text="Razón Social:" 
                            meta:resourcekey="lblRazonSocialResource1"></asp:Label>
                    </p>
                </div>
                <div class="line175">
                    <asp:TextBox ID="txtRazonSocial" runat="server" CssClass="bordeimput" MaxLength="150"
                        Width="300px" meta:resourcekey="txtRazonSocialResource1"></asp:TextBox></div>
            </div>
            <div class="bar2">
                <div class="line173">
                    <p>
                        <asp:Label ID="lblRuc" runat="server" Text="RUC:" 
                            meta:resourcekey="lblRucResource1"></asp:Label>
                    </p>
                </div>
                <div class="line175">
                    <asp:TextBox ID="txtRuc" runat="server" CssClass="bordeimput" MaxLength="11" 
                        Width="300px" meta:resourcekey="txtRucResource1"></asp:TextBox></div>
            </div>

            <div class="bar2">
                <div class="line173"><p>
                    <asp:Label ID="lblIndicadorArea" runat="server" 
                     Text="Indicador Area:" meta:resourcekey="lblIndicadorAreaResource1"></asp:Label> </p></div>
                <div class="line175">
                    <asp:DropDownList runat="server" ID="ddlIndicadorArea" CssClass="bordeimput" 
                     Width="250px" >
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
                        OnClick="btnNuevo_Click" meta:resourcekey="btnNuevoResource1" /></div>
                <div class="btns3">
                  <%--  <asp:Button runat="server" ID="btnEliminar" ClientIDMode="Static" CssClass="ocultarBoton"
                        OnClick="btnEliminar_Click" meta:resourcekey="btnEliminarResource1" />
                    <img onclick="javascript:VerificarSeleccionados();" src="../img/btneliminar.png"
                        alt="" class="imgCursor" />--%>
                     <input type="button" runat="server" id="btnEliminar0" value="Eliminar" class="btncancelar" />
                    <asp:Button runat="server" ID="btnEliminar" Text="Eliminar" ClientIDMode="Static"
                        CssClass="ocultarBoton" OnClick="btnEliminar_Click" meta:resourcekey="btnEliminarResource1" />
                </div>
            </div>
        </div>
        <div class="separa">
            <div class="conteradio1">
                <div class="bar5">
                    <center> <asp:Label ID="lblmensaje" runat="server" CssClass="letraError"></asp:Label></center>
                        <asp:GridView ID="gvBuscar" runat="server" CssClass="gridview" AutoGenerateColumns="False"
                            AllowPaging="True" Width="600px" 
                            OnPageIndexChanging="gvBuscar_PageIndexChanging" 
                            meta:resourcekey="gvBuscarResource1" 
                        onrowcommand="gvBuscar_RowCommand" PageSize="20">
                            <Columns>
                                <asp:BoundField DataField="PRV_Codigo" HeaderText="Código" 
                                    meta:resourcekey="BoundFieldResource1" />
                                <asp:BoundField DataField="PRV_RazonSocial" HeaderText="Razón Social" 
                                    meta:resourcekey="BoundFieldResource2" />
                                <asp:BoundField DataField="PRV_Ruc" HeaderText="RUC" 
                                    meta:resourcekey="BoundFieldResource3" />
                                <asp:BoundField DataField="PRV_IndicadorArea" HeaderText="Indicador Área" 
                                ItemStyle-Width="90" meta:resourcekey="BoundFieldResource4" >
                                 <ItemStyle Width="90px"></ItemStyle>
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Selec." 
                                    meta:resourcekey="TemplateFieldResource1">
                                    <ItemTemplate>
                                        <center>
                                            <asp:CheckBox ID="chkSeleccion" runat="server" 
                                                meta:resourcekey="chkSeleccionResource1" />
                                            <asp:HiddenField ID="hidIdProveedor" runat="server" Value='<%# Eval("IDProveedor") %>' />
                                        </center>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Modificar" 
                                    meta:resourcekey="TemplateFieldResource2">
                                    <ItemTemplate>
                                        <center>
                                    
                                            <asp:LinkButton ID="hypActualizar" ToolTip="Editar" runat="server" CausesValidation="False"
                                          CommandName="Editar" CommandArgument='<%# Eval("IDProveedor") %>' 
                                        meta:resourcekey="hypActualizarResource1">
                                    <img src="/img/btnmdificar.png" alt="Editar" style="border:0" /></asp:LinkButton>
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
                                Text="Debe seleccionar al menos un proveedor para eliminar." 
                                meta:resourcekey="eliminarseleccionadosResource1"></asp:Label></strong></p>
                    </div>
                    <div class="boxbtn">
                        <div class="centerclose">
                            <asp:Button ID="Button1" OnClientClick="javascript:CerrarDialogo('msjEliminarSeleccionados');"
                                runat="server" CssClass="<%$ Resources:generales,imgCerrar %>" 
                                meta:resourcekey="btnCerrarConfirmacionResource1" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
