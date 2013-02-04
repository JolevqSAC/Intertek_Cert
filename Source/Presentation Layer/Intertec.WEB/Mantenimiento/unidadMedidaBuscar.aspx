<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="unidadMedidaBuscar.aspx.cs" Inherits="Intertek.WEB.Mantenimiento.unidadMedidaBuscar" meta:resourcekey="PageResource1" %>
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

        $('#<%= dgvunidadmedida.ClientID %> tr').each(function (indexF) {

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
                    Text="Buscar Unidad de Medida" meta:resourcekey="lbltituloResource1"></asp:Label></p>
            </div>
            <div class="ti2"><p></p></div>
        </div>
    </div>
    <div class="conteradio1">
        <div class="bar1">
            <div class="line173"><p><asp:Label ID="lblcodigo" runat="server" Text="Codigo:" 
                    meta:resourcekey="lblcodigoResource1"></asp:Label></p></div>
            <div class="line175">
                <asp:TextBox ID="txtcodigo" runat="server" 
                    CssClass="bordeimput" Width="700px" Height="20px" MaxLength="10" 
                    meta:resourcekey="txtcodigoResource1"></asp:TextBox></div>
        </div>
        <div class="bar2">
            <div class="line173"><p><asp:Label ID="lblNombre" runat="server" Text="Nombre:" 
                    meta:resourcekey="lblNombreResource1"></asp:Label></p></div>
            <div class="line175">
                <asp:TextBox ID="txtNombre" runat="server" 
                    CssClass="bordeimput" Width="700px" Height="20px" MaxLength="50" 
                    meta:resourcekey="txtNombreResource1"></asp:TextBox></div>
        </div>
        <div class="bar2">
            <div class="line173"><p>
                <asp:Label ID="lblcorto" runat="server" 
                    Text="Nombre Corto:" meta:resourcekey="lblAbreviaturaResource1"></asp:Label></p></div>
            <div class="line175">
                <asp:TextBox ID="txtAbreviatura" runat="server" 
                    CssClass="bordeimput" Width="100px" Height="20px" 
                    meta:resourcekey="txtAbreviaturaResource1" MaxLength="5"></asp:TextBox></div>
        </div>
        <div style="clear:both"></div>
    </div>

    <div class="bar4">
        <div class="bar4tresili">
            <div class="btns2"><asp:Button ID="btnbuscar" runat="server" 
                    CssClass="<%$ Resources:generales,imgBuscar %>" onclick="btnbuscar_Click" 
                    meta:resourcekey="btnbuscarResource1"/> </div>
            <div class="btns3"><asp:Button ID="btnlimpiar" runat="server" 
                    CssClass="<%$ Resources:generales,imgLimpiar %>" 
                    onclick="btnlimpiar_Click" meta:resourcekey="btnlimpiarResource1"/></div>
            <div class="btns3">
                <asp:Button ID="btnnuevo" runat="server" CssClass="<%$ Resources:generales,imgNuevo %>" 
                    onclick="btnnuevo_Click" meta:resourcekey="btnnuevoResource1"/>                
                </div>
            <div class="btns3">
                <asp:Button runat="server" ID="btnEliminar" ClientIDMode="Static" 
                         CssClass="ocultarBoton" onclick="btnEliminar_Click" 
                    meta:resourcekey="btnEliminarResource1" />
                <img id="imgeliminar" runat="server" src="../img/btnEliminar.png" alt="" title="Eliminar"  style="cursor:pointer; border:0" 
                     onclick="javascript:VerificarSeleccionados();" />

            </div>
        </div>
    </div>

    <div class="separa">
        <div class="conteradio1">
            <div class="bar5">
                
                <center> <asp:Label ID="lblmensaje" runat="server" CssClass="letraError" 
                        meta:resourcekey="lblmensajeResource1"></asp:Label></center>
                <asp:GridView ID="dgvunidadmedida" runat="server" AutoGenerateColumns="False" AllowPaging="True" 
                    Width="600px" CssClass="gridview" 
                        onpageindexchanging="dgvunidadmedida_PageIndexChanging" 
                        meta:resourcekey="dgvunidadmedidaResource1" 
                        onrowcommand="dgvunidadmedida_RowCommand" PageSize="20">
                    <Columns>
                        <asp:BoundField DataField="UNM_Codigo" HeaderText="Código" ItemStyle-Width="100px" 
                            meta:resourcekey="BoundFieldResource1">                       
                                <ItemStyle Width="100px"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="UNM_Nombre" HeaderText="Nombre" ItemStyle-Width="250px"
                            meta:resourcekey="BoundFieldResource2">
                            <ItemStyle Width="250px"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="UNM_NombreCorto" HeaderText="Nombre Corto"  ItemStyle-Width="100px"
                            meta:resourcekey="BoundFieldResource3" >
                            <ItemStyle Width="100px"></ItemStyle>
                        </asp:BoundField>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="60" 
                            meta:resourcekey="TemplateFieldResource1">
                               <HeaderTemplate>
                                    <asp:Label ID="lblTitulo2" runat="server" Text="Selec." 
                                        meta:resourcekey="lblTitulo2Resource1"/>
                               </HeaderTemplate>               
                               <ItemTemplate>
                                    <asp:CheckBox ID="chkSeleccion" runat="server" 
                                        meta:resourcekey="chkSeleccionResource1" />
                                    <asp:HiddenField ID="hidIdUnidad" runat="server" Value='<%# Eval("IDUnidadMedida") %>' />
                               </ItemTemplate>  
                                <ItemStyle  VerticalAlign="Middle" HorizontalAlign="Center" Width="60px"></ItemStyle>
                             </asp:TemplateField>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" 
                            meta:resourcekey="TemplateFieldResource1">
                            <ItemTemplate>
                               <asp:LinkButton ID="lkbEditar" ToolTip="Modificar" runat="server" CausesValidation="False"
                                    CommandName="Editar" CommandArgument='<%# Eval("IDUnidadMedida") %>' 
                                    meta:resourcekey="lkbEditarResource1">
                                    <img src="../img/btnmdificar.png" alt="Modificar" style="border:0" /></asp:LinkButton>          
                             </ItemTemplate>
                             <ItemStyle Width="60px" />
                        </asp:TemplateField>

                    </Columns>
                    
                    <PagerSettings Mode="NumericFirstLast" PageButtonCount="2" />
                    
                </asp:GridView>
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
                                Text="Error de eliminación." meta:resourcekey="lblEliminarErrorResource1"></asp:Label></strong></p>
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
                                Text="Debe seleccionar al menos una unidad de medida para eliminar." 
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
</div>
</asp:Content>
