<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="categoriaProductosBuscar.aspx.cs" Inherits="Intertek.WEB.Mantenimiento.categoriaProductosBuscar" meta:resourcekey="PageResource1" %>
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

            $('#<%= dgvbuscar.ClientID %> tr').each(function (indexF) {

                $(this).find('td').each(function (indexC) {

                    if (indexC == 5) {

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
           <div class="ti1"><p><asp:Label ID="lblTitulo" runat="server" 
                   Text="Buscar Categoría de Producto" meta:resourcekey="lblTituloResource1"></asp:Label></p>
           </div>
           <div class="ti2"><p></p></div>
        </div>
    </div>
     <div class="conteradio1">
        <div class="bar1">
            <div class="line173"><p>
                <asp:Label ID="lblcodigo" runat="server" Text="Código:" 
                    meta:resourcekey="lblcodigoResource1"></asp:Label></p></div>
            <div class="line175">
                <asp:TextBox ID="txtcodigo" runat="server" CssClass="bordeimput" MaxLength="50" 
                    Width="250px" meta:resourcekey="txtcodigoResource1"></asp:TextBox></div>
        </div>
        
        <div class="bar2">
            <div class="line173"><p>
                <asp:Label ID="lblNombre" runat="server" Text="Nombre:" 
                    meta:resourcekey="lblNombreResource1"></asp:Label></p></div>
            <div class="line175">
                <asp:TextBox ID="txtNombre" runat="server" CssClass="bordeimput" 
                    MaxLength="100" Width="700px" meta:resourcekey="txtNombreResource1"></asp:TextBox></div>
        </div>

        <div class="bar2">
         <div class="line173"><p>
             <asp:Label ID="lblingles" runat="server" 
                 Text="Nombre en Inglés:" meta:resourcekey="lblinglesResource1"></asp:Label> </p></div>
         <div class="line175">
             <asp:TextBox ID="txtingles" runat="server" CssClass="bordeimput" 
                 MaxLength="100" Width="700px" meta:resourcekey="txtinglesResource1"></asp:TextBox></div>
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

        <div style="clear:both"></div>
    </div>

    <div class="bar4">
    <div class="bar4tresili">
        <div class="btns2">
            <asp:Button ID="btnBuscar" runat="server" Text="Buscar"  
                CssClass="<%$ Resources:generales,imgBuscar %>" onclick="btnBuscar_Click" meta:resourcekey="btnBuscarResource1" 
          /></div>
        <div class="btns3"><asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" 
                CssClass="<%$ Resources:generales,imgLimpiar %>" 
                onclick="btnLimpiar_Click" meta:resourcekey="btnLimpiarResource1"  
         /></div>
        <div class="btns3">
            <asp:Button ID="btnNuevo" runat="server" Text="Nuevo" 
                CssClass="<%$ Resources:generales,imgNuevo %>" onclick="btnNuevo_Click" 
                meta:resourcekey="btnNuevoResource1"/>
        </div>

        <div class="btns3">
            <asp:Button runat="server" ID="btnEliminar" ClientIDMode="Static"  Text="Eliminar"
                             CssClass="ocultarBoton" onclick="btnEliminar_Click" 
                meta:resourcekey="btnEliminarResource1" /> 

            <img id="imgeliminar" runat="server" onclick="javascript:VerificarSeleccionados();" src="../img/btneliminar.png" alt="" class="imgCursor"/>
        </div>

    </div>
    </div>

    <div class="separa">
<div class="conteradio1">
    <div class="bar5">

                     <center> <asp:Label ID="lblmensaje" runat="server" CssClass="letraError" 
                             meta:resourcekey="lblmensajeResource1"></asp:Label></center>
                    <asp:GridView ID="dgvbuscar" runat="server" CssClass="gridview" AutoGenerateColumns="False"
                        AllowPaging="True" Width="800px" 
                        onpageindexchanging="dgvbuscar_PageIndexChanging" 
                        onrowcommand="dgvbuscar_RowCommand" meta:resourcekey="dgvbuscarResource1" 
                         PageSize="20">
                        <Columns>                                                    
                            <asp:BoundField DataField="CAT_Codigo" HeaderText="Código" 
                                meta:resourcekey="BoundFieldResource1"> 
                                 <ItemStyle Width="100px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="CAT_Nombre" HeaderText="Nombre" 
                                ItemStyle-Width="250" meta:resourcekey="BoundFieldResource2" >
                                <ItemStyle Width="250px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="CAT_NombreIngles" HeaderText="Nombre en Inglés" 
                                ItemStyle-Width="250"  meta:resourcekey="BoundFieldResource3" >
                                 <ItemStyle Width="250px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="CAT_IndicadorArea" HeaderText="Indicador Área" 
                                ItemStyle-Width="90" meta:resourcekey="BoundFieldResource4" >
                                 <ItemStyle Width="90px"></ItemStyle>
                            </asp:BoundField>
                             <asp:TemplateField meta:resourcekey="TemplateFieldResource1">
                                <HeaderTemplate>
                                    <asp:Label ID="lblTitulo" runat="server" Text="Descripción" 
                                        meta:resourcekey="lblTituloResource2" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div style="word-wrap: break-word; width: 300px;overflow: hidden;"><%# Eval("CAT_Descripcion")%></div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="60" 
                                meta:resourcekey="TemplateFieldResource2">
                               <HeaderTemplate>
                                    <asp:Label ID="lblTitulo" runat="server" Text="Selec." 
                                        meta:resourcekey="lblTituloResource3"/>
                               </HeaderTemplate>               
                               <ItemTemplate>
                                    <asp:CheckBox ID="chkSeleccion" runat="server" 
                                        meta:resourcekey="chkSeleccionResource1" />
                                    <asp:HiddenField ID="hidIdCategoria" runat="server" Value='<%# Eval("IDCategoria") %>' />
                               </ItemTemplate>  
                                <ItemStyle  VerticalAlign="Middle" HorizontalAlign="Center" Width="60px"></ItemStyle>
                             </asp:TemplateField>  
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" 
                                meta:resourcekey="TemplateFieldResource3">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lkbEditar" ToolTip="Modificar" runat="server" CausesValidation="False"
                                    CommandName="Editar" CommandArgument='<%# Eval("IDCategoria") %>' 
                                    meta:resourcekey="lkbEditarResource1">
                                    <img src="../img/btnmdificar.png" alt="Modificar" style="border:0" /></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateField>
                        </Columns>
                        <PagerSettings Mode="NumericFirstLast" PageButtonCount="2" />
                    </asp:GridView>
             
        
    </div>
    
    <div style="clear:both"> <asp:HiddenField runat="server" ClientIDMode="Static" 
            ID="hdEliminarID" /> </div>
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
                            <asp:Button ID="btnSi" OnClientClick="javascript:EliminarSi();" runat="server"
                                Text="Si" CssClass="<%$ Resources:generales,imgSi %>" 
                                meta:resourcekey="btnSiResource1" />
                            <asp:Button ID="btnNo" OnClientClick="javascript:CerrarDialogo('msjError');" runat="server"
                                Text="No" CssClass="<%$ Resources:generales,imgNo %>" 
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
                                runat="server" Text="Cerrar" 
                                CssClass="<%$ Resources:generales,imgCerrar %>" 
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
                                Text="Debe seleccionar al menos un categoria para eliminar." 
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
