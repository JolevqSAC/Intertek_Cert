<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="estadoBuscar.aspx.cs" Inherits="Intertek.WEB.Mantenimiento.estadoBuscar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
<script type="text/javascript">
    $(document).ready(function () {
        //CrearDialogo(nombreDIV, alto, ancho, titulo);
        CrearDialogo('msjEliminacionOK', 162, 360, '');
        CrearDialogo('msjErrorEliminar', 162, 360, '');
        CrearDialogoEliminar('msjConfirmacion', 162, 360, '');
    });

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
                    Text="Buscar Estado" ></asp:Label></p>
            </div>
            <div class="ti2"><p></p></div>
        </div>
    </div>
    <div class="conteradio1">
        <div class="bar1">
            <div class="line173"><p><asp:Label ID="lblDescripcion" runat="server" Text="Descripción:" 
                    ></asp:Label></p></div>
            <div class="line175">
                <asp:TextBox ID="txtDescripcion" runat="server" 
                    CssClass="bordeimput" Width="700px" Height="20px" MaxLength="20" 
                    ></asp:TextBox></div>
        </div>
        <div class="bar2">
            <div class="line173"><p><asp:Label ID="lblTipo" runat="server" 
                    Text="Tipo:"></asp:Label></p></div>
            <div class="line175">
                <asp:TextBox ID="txtTipo" runat="server" 
                    CssClass="bordeimput" Width="100px" Height="20px" MaxLength="3" 
                    ></asp:TextBox></div>
        </div>
        <div style="clear:both"></div>
    </div>

    <div class="bar4">
        <div class="bar4tres">
            <div class="btns2"><asp:Button ID="btnbuscar" runat="server" 
                    CssClass="<%$ Resources:generales,imgBuscar %>" onclick="btnbuscar_Click" 
                    meta:resourcekey="btnbuscarResource1"/> </div>
            <div class="btns3"><asp:Button ID="btnlimpiar" runat="server" 
                    CssClass="<%$ Resources:generales,imgLimpiar %>" 
                    onclick="btnlimpiar_Click" /></div>
            <div class="btns3">
                <asp:Button ID="btnnuevo" runat="server" CssClass="<%$ Resources:generales,imgNuevo %>" 
                    onclick="btnnuevo_Click"/>
                <asp:Button runat="server" ID="btnEliminar" ClientIDMode="Static" 
                         CssClass="ocultarBoton" onclick="btnEliminar_Click" />
                </div>
        </div>
    </div>

    <div class="separa">
        <div class="conteradio1">
            <div class="bar5">
                <center>
                <asp:Label ID="lblmensaje" runat="server" CssClass="letraError"></asp:Label>
                <asp:GridView ID="dgvestado" runat="server" AutoGenerateColumns="False" AllowPaging="True" 
                    Width="350px" CssClass="gridview" 
                        onpageindexchanging="dgvestado_PageIndexChanging" 
                        onrowcommand="dgvestado_RowCommand">
                    <Columns>
                                               
                        <asp:BoundField DataField="EST_Descripcion" HeaderText="Descripción" 
                            />
                        <asp:BoundField DataField="EST_Tipo" HeaderText="Tipo" 
                            />
                        <asp:TemplateField HeaderText="Opciones" ItemStyle-HorizontalAlign="Center" 
                            >
                            <ItemTemplate>
                               <asp:LinkButton ID="lkbEditar" ToolTip="Editar" runat="server" CausesValidation="False"
                                    CommandName="Editar" CommandArgument='<%# Eval("IDEstado") %>' 
                                    meta:resourcekey="lkbEditarResource1">
                                    <img src="../img/editar.png" alt="Editar" style="border:0" /></asp:LinkButton>
                                                              
                               <img  src="../img/quitar.png" alt="" title="Eliminar"  style="cursor:pointer; border:0" 
                                     onclick="javascript:MostrarMensajeEliminar('msjConfirmacion','<%# Eval("IDEstado") %>');"/>
                                                                      
                             </ItemTemplate>
                             <ItemStyle Width="60px" />
                        </asp:TemplateField>

                    </Columns>
                    
                </asp:GridView></center>
            </div>

            <div style="clear:both">
              <asp:HiddenField runat="server" ClientIDMode="Static" ID="hdEliminarID" Value="" />
            </div>
        </div>
    </div>
    
    <div class="_dialog" id="msjConfirmacion" style="display: none">
        <div class="contealertPregunta">
            <div class="boxtext">
                <div class="boxdentro">
                    <div class="boxletra">
                        <p>
                            <strong><asp:Label ID="lblEliminar" runat="server" Text="Desea eliminar el registro?"></asp:Label></strong></p>
                    </div>
                     <div class="boxbtn">
                        <div class="centerclose">
                            <asp:Button ID="btnAceptar" OnClientClick="javascript:EliminarSi();" runat="server"
                                Text="" CssClass="<%$ Resources:generales,imgSi %>" />
                            <asp:Button ID="btnNo" OnClientClick="javascript:CerrarDialogo('msjConfirmacion');" runat="server"
                                Text="" CssClass="<%$ Resources:generales,imgNo %>" />
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
                            <strong><asp:Label ID="lblEliminarOk" runat="server" Text="El registro se eliminó satisfactoriamente."></asp:Label></strong></p>
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
                            <strong><asp:Label ID="lblEliminarError" runat="server" Text="Error de eliminación."></asp:Label></strong></p>
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

</div>
</asp:Content>
