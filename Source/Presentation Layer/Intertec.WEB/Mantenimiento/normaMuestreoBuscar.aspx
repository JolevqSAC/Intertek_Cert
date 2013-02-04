<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="normaMuestreoBuscar.aspx.cs" Inherits="Intertek.WEB.Mantenimiento.normaMuestreoBuscar" meta:resourcekey="PageResource1" %>
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

        $('#<%= dgvnormasmuestreo.ClientID %> tr').each(function (indexF) {

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
            <div class="ti1"><p>
                <asp:Label ID="lbltitulo" runat="server" 
                    Text="Buscar Normas de Muestreo" meta:resourcekey="lbltituloResource1" ></asp:Label></p>
            </div>
            <div class="ti2"><p></p></div>
        </div>
    </div>
    <div class="conteradio1">

        <div class="bar1">
            <div class="line173"><p>
                <asp:Label ID="lblcodigo" runat="server" Text="Código:" meta:resourcekey="lblcodigoResource1" 
                    ></asp:Label></p></div>
            <div class="line175">
                <asp:TextBox ID="txtcodigo" runat="server" 
                    CssClass="bordeimput" Width="300px" Height="20px" MaxLength="10" 
                    meta:resourcekey="txtcodigoResource1"></asp:TextBox></div>
        </div>
        <div class="bar2">
            <div class="line173"><p>
                <asp:Label ID="lblNombre" runat="server" Text="Nombre:" meta:resourcekey="lblNombreResource1" 
                    ></asp:Label></p></div>
            <div class="line175">
                <asp:TextBox ID="txtNombre" runat="server" 
                    CssClass="bordeimput" Width="700px" Height="20px" MaxLength="100" 
                    meta:resourcekey="txtNombreResource1"></asp:TextBox></div>
        </div>
        <div class="bar2">
            <div class="line173"><p>
                <asp:Label ID="lblaño" runat="server" 
                    Text="Año:" meta:resourcekey="lblañoResource1"></asp:Label></p></div>
            <div class="line175">
                <asp:TextBox ID="txtAnio" runat="server" 
                    CssClass="bordeimput" Width="300px" Height="20px" MaxLength="4" 
                    onkeypress="javascript:return validarNro(event)" 
                    meta:resourcekey="txtAnioResource1"></asp:TextBox></div>
        </div>
        <div style="clear:both"></div>
    </div>

    <div class="bar4">
        <div class="bar4tresili">
            <div class="btns2"><asp:Button ID="btnbuscar" runat="server" 
                    CssClass="<%$ Resources:generales,imgBuscar %>" onclick="btnbuscar_Click" 
                    meta:resourcekey="btnbuscarResource1"/> 
            </div>
            <div class="btns3"><asp:Button ID="btnlimpiar" runat="server" 
                    CssClass="<%$ Resources:generales,imgLimpiar %>" 
                    onclick="btnlimpiar_Click" meta:resourcekey="btnlimpiarResource1"/>
            </div>
            <div class="btns3">
                <asp:Button ID="btnnuevo" runat="server" CssClass="<%$ Resources:generales,imgNuevo %>" 
                    onclick="btnnuevo_Click" meta:resourcekey="btnnuevoResource1"/>
            </div>

            <div class="btns3">
            <asp:Button runat="server" ID="btnEliminar" ClientIDMode="Static" 
                         CssClass="ocultarBoton" onclick="btnEliminar_Click" 
                    meta:resourcekey="btnEliminarResource1" /> 
                <img id="imgeliminar" runat="server" title="Eliminar"  style="cursor:pointer; border:0" src="../img/btnEliminar.png" alt="Eliminar"
                             onclick="javascript:VerificarSeleccionados();" />
            </div>
        </div>
    </div>

    <div class="separa">
        <div class="conteradio1">
            <div class="bar5">
                <center>
                <asp:Label ID="lblmensaje" runat="server" CssClass="letraError" 
                        meta:resourcekey="lblmensajeResource1"></asp:Label></center>
                <asp:GridView ID="dgvnormasmuestreo" runat="server" AutoGenerateColumns="False" AllowPaging="True" 
                    Width="700px" CssClass="gridview" 
                        onpageindexchanging="dgvnormasmuestreo_PageIndexChanging" 
                        onrowcommand="dgvnormasmuestreo_RowCommand" 
                        meta:resourcekey="dgvnormasmuestreoResource1" PageSize="20">
                    <Columns>
                                               
                        <asp:BoundField DataField="NOM_Codigo" HeaderText="Código" 
                            ItemStyle-Wrap="true" ItemStyle-Width="100px" meta:resourcekey="BoundFieldResource1" 
                             >
<ItemStyle Wrap="True" Width="100px"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="NOM_Nombre" HeaderText="Nombre" 
                            ItemStyle-Wrap="true" ItemStyle-Width="100px" meta:resourcekey="BoundFieldResource2"
                             >
<ItemStyle Wrap="True" Width="100px"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="NOM_Descripcion" HeaderText="Descripción" 
                            ItemStyle-Wrap="true"  ItemStyle-Width="300px" meta:resourcekey="BoundFieldResource3"
                           >
<ItemStyle Wrap="True" Width="300px"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="NOM_Acreditador" HeaderText="Acreditador" 
                            ItemStyle-Wrap="true" ItemStyle-Width="100px" meta:resourcekey="BoundFieldResource4"
                             >
<ItemStyle Wrap="True" Width="100px"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="NOM_Anio" HeaderText="Año"  ItemStyle-Width="50px" meta:resourcekey="BoundFieldResource5"
                           >                           
<ItemStyle Width="50px"></ItemStyle>
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
                                        <asp:HiddenField ID="hidIdMuestreo" runat="server" Value='<%# Eval("IDNormaMuestreo") %>' />
                                   </ItemTemplate>  

<ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                                </asp:TemplateField>                         
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" meta:resourcekey="TemplateFieldResource2" 
                            >
                            <ItemTemplate>
                               <asp:LinkButton ID="lkbEditar" ToolTip="Modificar" runat="server" CausesValidation="False"
                                    CommandName="Editar" CommandArgument='<%# Eval("IDNormaMuestreo") %>' 
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
                                Text="Debe seleccionar al menos una norma de muestreo para eliminar." 
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
