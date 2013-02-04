<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="centroCostosBuscar.aspx.cs" Inherits="Intertek.WEB.Mantenimiento.centroCostosBuscar" meta:resourcekey="PageResource1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
   <script type="text/javascript">
       $(document).ready(function () {
           //CrearDialogo(nombreDIV, alto, ancho, titulo);
           CrearDialogo('msjEliminacionOK', 162, 360, '');
           CrearDialogo('msjErrorEliminar', 162, 360, '');
           CrearDialogoEliminar('msjConfirmacion', 162, 360, '');
           CrearDialogo('msjEliminarSeleccionados', 162, 360, '');
       });

       function VerificarSeleccionados() {

           var existenSeleccionados = false;

           $('#<%= dgvcentrocostos.ClientID %> tr').each(function (indexF) {

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
               MostrarMensajeEliminar('msjConfirmacion', '');
           } else {
               MostrarMensaje('msjEliminarSeleccionados');
           }
       }

       function EliminarSi() {
           var idEliminar = $('#hdEliminarID').val();

           CerrarDialogo('msjConfirmacion');

           $('#btnEliminar').trigger("click");
       }      
  </script>
  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="contentgeneral">
    <div class="titulo1">
        <div class="titulo2">
            <div class="ti1"><p><asp:Label ID="lbltitulo" runat="server" 
                    Text="Buscar Centros de Costos" meta:resourcekey="lbltituloResource1"></asp:Label></p>
            </div>
            <div class="ti2"><p></p></div>
        </div>
    </div>
    <div class="conteradio1">
        <div class="bar1">
            <div class="line173"><p>
                <asp:Label ID="lblCodigo" runat="server" Text="Codigo:" 
                    meta:resourcekey="lblCodigoResource1" ></asp:Label></p></div>
            <div class="line175">
                <asp:TextBox ID="txtCodigo" runat="server" 
                    CssClass="bordeimput" Width="300px" Height="20px" MaxLength="10" 
                    meta:resourcekey="txtCodigoResource1"></asp:TextBox></div>
        </div>
        <div class="bar2">
            <div class="line173"><p>
                <asp:Label ID="lblNombre" runat="server" 
                    Text="Número:" meta:resourcekey="lblNombreResource1" ></asp:Label></p></div>
            <div class="line175">
                <asp:TextBox ID="txtNumero" runat="server" 
                    CssClass="bordeimput" Width="300px" Height="20px"  MaxLength="20" 
                    meta:resourcekey="txtNombreResource1"></asp:TextBox></div>
        </div>

         <div class="bar2">
         <div class="line173">
         <p>
            <asp:Label ID="lblArea" runat="server" Text="Área:" 
                 meta:resourcekey="lblAreaResource1"></asp:Label> 
         </p>
         </div>
         <div class="line175">
             <asp:DropDownList runat="server" ID="ddlarea" CssClass="bordeimput" 
                 Width="300px" meta:resourcekey="ddlareaResource1">
             </asp:DropDownList>
           </div>
        </div>
        <div style="clear:both"></div>
    </div>

    <div class="bar4">
    <div class="bar4tresili">
        <div class="btns2">
            <asp:Button ID="btnBuscar" runat="server"  
                CssClass="<%$ Resources:generales,imgBuscar %>" onclick="btnBuscar_Click" meta:resourcekey="btnBuscarResource1" 
          /></div>
        <div class="btns3"><asp:Button ID="btnLimpiar" runat="server" 
                CssClass="<%$ Resources:generales,imgLimpiar %>" 
                onclick="btnLimpiar_Click" meta:resourcekey="btnLimpiarResource1"  
         /></div>
        <div class="btns3">
            <asp:Button ID="btnNuevo" runat="server" 
                CssClass="<%$ Resources:generales,imgNuevo %>" onclick="btnNuevo_Click" 
                meta:resourcekey="btnNuevoResource1"/>
        </div>

        <div class="btns3">
            <asp:Button runat="server" ID="btnEliminar" ClientIDMode="Static" 
                                 CssClass="ocultarBoton" onclick="btnEliminar_Click" 
                meta:resourcekey="btnEliminarResource1" /> 
            <img id="imgeliminar" runat="server" onclick="javascript:VerificarSeleccionados();" src="../img/btneliminar.png" alt="" class="imgCursor"/>
        </div>

    </div>
    </div>

    <div class="separa">
        <div class="conteradio1">
            <div class="bar5">
                <center>
                <asp:Label ID="lblmensaje" runat="server" CssClass="letraError" 
                        meta:resourcekey="lblmensajeResource1"></asp:Label>
                <asp:GridView ID="dgvcentrocostos" runat="server" AutoGenerateColumns="False" AllowPaging="True" 
                    Width="700px" CssClass="gridview"  onpageindexchanging="dgvcentrocostos_PageIndexChanging"
                        onrowcommand="dgvcentrocostos_RowCommand" 
                        meta:resourcekey="dgvcentrocostosResource1" PageSize="20">
                    <Columns>
                        <asp:BoundField DataField="CCO_Codigo" HeaderText="Código" 
                            meta:resourcekey="BoundFieldResource1">
                            <ItemStyle Width="100px"></ItemStyle>
                         </asp:BoundField>
                        <asp:BoundField DataField="CCO_Numero" HeaderText="Número" 
                            meta:resourcekey="BoundFieldResource2"> 
                            <ItemStyle Width="150px"></ItemStyle>
                        </asp:BoundField>                                            
                        <asp:BoundField DataField="ARE_Nombre" HeaderText="Área" meta:resourcekey="BoundFieldResource4"
                            ItemStyle-Width="200px" ><ItemStyle Width="200px"></ItemStyle>
                        </asp:BoundField>

                         <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="60" 
                            meta:resourcekey="TemplateFieldResource1">
                               <HeaderTemplate>
                                    <asp:Label ID="lblTitulo1" runat="server" Text="Selec." 
                                        meta:resourcekey="lblTitulo1Resource1"/>
                               </HeaderTemplate>               
                               <ItemTemplate>
                                    <asp:CheckBox ID="chkSeleccion" runat="server" 
                                        meta:resourcekey="chkSeleccionResource1" />
                                    <asp:HiddenField ID="hidIdCentroCosto" runat="server" Value='<%# Eval("IDCentroCosto") %>' />
                               </ItemTemplate>  
                                <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                             </asp:TemplateField> 

                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" 
                            meta:resourcekey="TemplateFieldResource2" >
                            <ItemTemplate>
                               <asp:LinkButton ID="lkbEditar" ToolTip="Modificar" runat="server" CausesValidation="False"
                                    CommandName="Editar" CommandArgument='<%# Eval("IDCentroCosto") %>' 
                                    meta:resourcekey="lkbEditarResource1">
                                    <img src="../img/btnmdificar.png" alt="Modificar" style="border:0" /></asp:LinkButton>                                     
                             </ItemTemplate>
                             <ItemStyle Width="60px" />
                        </asp:TemplateField>

                    </Columns>
                    
                    <PagerSettings Mode="NumericFirstLast" PageButtonCount="2" />
                    
                </asp:GridView></center>
            </div>

            <div style="clear:both">
             <asp:HiddenField runat="server" ClientIDMode="Static" ID="hdEliminarID" />
            </div>
        </div>
    </div>
    
    <div class="_dialog" id="msjConfirmacion" style="display: none">
        <div class="contealertPregunta">
            <div class="boxtext">
                <div class="boxdentro">
                    <div class="boxletra">
                        <p>
                            <strong><asp:Label ID="lblEliminar" runat="server" 
                                Text="Desea eliminar el registro?" meta:resourcekey="lblEliminarResource1"></asp:Label></strong></p>
                    </div>
                    <div class="boxbtn">
                        <div class="centerclose">
                            <asp:Button ID="btnAceptar" OnClientClick="javascript:EliminarSi();" 
                                runat="server" CssClass="<%$ Resources:generales,imgSi %>" 
                                meta:resourcekey="btnAceptarResource1" />
                            <asp:Button ID="btnNo" 
                                OnClientClick="javascript:CerrarDialogo('msjConfirmacion');" runat="server" 
                                CssClass="<%$ Resources:generales,imgNo %>" meta:resourcekey="btnNoResource1" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
      </div> 
    <div class="_dialog" id="msjEliminacionOK" style="display: none">
        <div class="contealert">
            <div class="boxtext">
                <div class="boxdentro">
                    <div class="boxletra">
                        <p>
                            <strong><asp:Label ID="lblEliminarOk" runat="server" 
                                Text="El registro se eliminó satisfactoriamente." 
                                meta:resourcekey="lblEliminarOkResource1"></asp:Label></strong></p>
                    </div>
                    <div class="boxbtn">
                        <div class="centerclose">
                            <asp:Button runat="server" ID="btnCerrar1" CssClass="<%$ Resources:generales,imgCerrar %>"
                            OnClientClick="CerrarDialogo('msjEliminacionOK');" CausesValidation="False" 
                                meta:resourcekey="btnCerrar1Resource1"/>
                        </div>
                    </div>
                </div>
            </div>
        </div>
      </div>
      <div class="_dialog" id="msjErrorEliminar" style="display: none" title="Error">
        <div class="contealertError">
            <div class="boxtext">
                <div class="boxdentro">
                    <div class="boxletra">
                        <p>
                            <strong><asp:Label ID="lblEliminarError" runat="server" 
                                Text="Error al eliminar registro." meta:resourcekey="lblEliminarErrorResource1"></asp:Label></strong></p>
                    </div>
                    <div class="boxbtn">
                        <div class="centerclose">
                            <asp:Button runat="server" ID="btnCerrar2" CssClass="<%$ Resources:generales,imgCerrar %>"
                                OnClientClick="CerrarDialogo('msjErrorEliminar');" 
                                meta:resourcekey="btnCerrar2Resource1" /></div>
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
                                Text="Debe seleccionar al menos un centro de costo para eliminar." 
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
