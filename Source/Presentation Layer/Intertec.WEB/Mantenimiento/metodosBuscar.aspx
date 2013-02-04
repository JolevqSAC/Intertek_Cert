<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="metodosBuscar.aspx.cs" Inherits="Intertek.WEB.Mantenimiento.metodosBuscar" meta:resourcekey="PageResource1" %>
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
                        <asp:Label ID="lblTitulo" runat="server" Text="Buscar Métodos" 
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
            <div class="line173"><p><asp:Label ID="lblcodigo" runat="server" Text="Código:" 
                    meta:resourcekey="lblcodigoResource1"></asp:Label></p></div>
            <div class="line175">
                <asp:TextBox ID="txtCodigo" runat="server" CssClass="bordeimput" MaxLength="10" 
                    Width="700px" Height="20px" meta:resourcekey="txtCodigoResource1"></asp:TextBox></div>
        </div>

        <div class="bar2">
            <div class="line173"><p>
                <asp:Label ID="lblnombre" runat="server" Text="Nombre:" 
                    meta:resourcekey="lblnombreResource1" ></asp:Label></p></div>
            <div class="line175">
                <asp:TextBox ID="txtnombre" runat="server" CssClass="bordeimput" MaxLength="100" 
                    Width="700px" Height="20px" meta:resourcekey="txtnombreResource1"></asp:TextBox></div>
        </div>

        <div class="bar2">
             <div class="line173">
             <p>
                <asp:Label ID="lblingles" runat="server" Text="Nombre en Inglés:" 
                     meta:resourcekey="lblinglesResource1"></asp:Label> 
             </p>
             </div>
             <div class="line175">
              <asp:TextBox ID="txtingles" runat="server" CssClass="bordeimput" MaxLength="100" 
                  Width="700px" Height="20px" meta:resourcekey="txtinglesResource1"></asp:TextBox></div>
           </div>

           <div class="bar2">
            <div class="line173"><p>
                <asp:Label ID="lbldescripcion" runat="server" Text="Descripción:" 
                    meta:resourcekey="lbldescripcionResource1" ></asp:Label></p></div>
            <div class="line175">
                <asp:TextBox ID="txtdescripcion" runat="server" CssClass="bordeimput" MaxLength="200" 
                    Width="700px" Height="20px" meta:resourcekey="txtdescripcionResource1"></asp:TextBox></div>
        </div>
        <div style="clear: both"></div>
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
                     <asp:Button runat="server" ID="btnEliminar" ClientIDMode="Static" 
                                 CssClass="ocultarBoton" onclick="btnEliminar_Click" 
                    meta:resourcekey="btnEliminarResource1" /> 
                    <img id="imgeliminar" runat="server" onclick="javascript:VerificarSeleccionados();"
                                          src="../img/btneliminar.png" alt=""     class="imgCursor"/>
                 </div>
            </div>
        </div>
        <div class="separa">
            <div class="conteradio1">
                <div class="bar5">
                    <center>
                        <asp:Label ID="lblmensaje" runat="server" CssClass="letraError" 
                            meta:resourcekey="lblmensajeResource1"></asp:Label>
                        <asp:GridView ID="gvBuscar" runat="server" CssClass="gridview" AutoGenerateColumns="False"
                            AllowPaging="True" Width="700px" 
                            OnPageIndexChanging="gvBuscar_PageIndexChanging" 
                            onrowcommand="gvBuscar_RowCommand" meta:resourcekey="gvBuscarResource1" 
                            PageSize="20">
                            <Columns>
                                <asp:BoundField DataField="MET_Codigo" HeaderText="Código" 
                                    ItemStyle-Width="100px" meta:resourcekey="BoundFieldResource1">
<ItemStyle Width="100px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="MET_Nombre" HeaderText="Nombre" 
                                    ItemStyle-Width="150px" meta:resourcekey="BoundFieldResource2"  >
<ItemStyle Width="150px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="MET_NombreIngles" HeaderText="Nombre Inglés" 
                                    ItemStyle-Width="150px" meta:resourcekey="BoundFieldResource3" >
<ItemStyle Width="150px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="MET_Descripcion" HeaderText="Descripción" 
                                    ItemStyle-Width="400px" meta:resourcekey="BoundFieldResource4" >
<ItemStyle Width="400px"></ItemStyle>
                                </asp:BoundField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="60px" 
                                    meta:resourcekey="TemplateFieldResource1">
                                   <HeaderTemplate>
                                        <asp:Label ID="lblTitulo" runat="server" Text="Selec." 
                                            meta:resourcekey="lblTituloResource2"/>
                                   </HeaderTemplate>               
                                   <ItemTemplate>
                                        <asp:CheckBox ID="chkSeleccion" runat="server" 
                                            meta:resourcekey="chkSeleccionResource1" />
                                        <asp:HiddenField ID="hidIdMetodo" runat="server" Value='<%# Eval("IDMetodo") %>' />
                                   </ItemTemplate>  

<ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                                </asp:TemplateField>  
                                <asp:TemplateField  ItemStyle-HorizontalAlign="Center" 
                                    meta:resourcekey="TemplateFieldResource2">
                                    <ItemStyle Width="80px" />
                                    <ItemTemplate>
                                       <asp:LinkButton ID="lkbEditar" ToolTip="Modificar" runat="server" CausesValidation="False"
                                    CommandName="Editar" CommandArgument='<%# Eval("IDMetodo") %>' 
                                    meta:resourcekey="lkbEditarResource1">
                                    <img src="../img/btnmdificar.png" alt="Modificar" style="border:0" /></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <PagerSettings Mode="NumericFirstLast" PageButtonCount="2" />
                        </asp:GridView>
                    </center>
                </div>
                <div style="clear: both">
                    <asp:HiddenField runat="server" ClientIDMode="Static" ID="hdEliminarID" />
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
                                Text="Debe seleccionar al menos un método para eliminar." 
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
    </div>
    </asp:Content>
