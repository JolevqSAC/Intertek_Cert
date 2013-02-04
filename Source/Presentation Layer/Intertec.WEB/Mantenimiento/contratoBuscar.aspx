<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="contratoBuscar.aspx.cs" Inherits="Intertek.WEB.Mantenimiento.contratoBuscar" meta:resourcekey="PageResource1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
<script src="../Scripts/ui.datepicker-es.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            //CrearDialogo(nombreDIV, alto, ancho, titulo);
            CrearDialogo('msjEliminacionOK', 162, 360, '');
            CrearDialogo('msjErrorEliminar', 162, 360, '');
            CrearDialogoEliminar('msjConfirmacion', 162, 360, '');
            CrearDialogo('msjEliminarSeleccionados', 162, 360, '');
           
            if ('<%=Session["ddlIdiomas"] %>' == "es-PE") {
                $.datepicker.setDefaults($.datepicker.regional["es"]);
            }
            else {
                $.datepicker.setDefaults($.datepicker.regional[""]);
            }

            $('#<%= txtInicio.ClientID %>').datepicker({ changeMonth: true, changeYear: true });
            $('#<%= txtFin.ClientID %>').datepicker({ changeMonth: true, changeYear: true });
        });

        function VerificarSeleccionados() {

            var existenSeleccionados = false;

            $('#<%= dgvbuscar.ClientID %> tr').each(function (indexF) {

                $(this).find('td').each(function (indexC) {

                    if (indexC == 8) {

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
                <div class="ti1">
                    <p>
                        <asp:Label ID="lbltitulo" runat="server" Text="Buscar Contrato" 
                            meta:resourcekey="lbltituloResource1"></asp:Label></p>
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
                        <asp:Label ID="lblcodigo" runat="server" Text="Código:" 
                            meta:resourcekey="lblcodigoResource1"></asp:Label>
                    </p>
                </div>
                <div class="line175">
                    <asp:TextBox ID="txtcodigo" runat="server" CssClass="bordeimput" Width="200px" Height="20px"
                        MaxLength="10" meta:resourcekey="txtcodigoResource1"></asp:TextBox>
                </div>
            </div>
            <div class="bar2">
                <div class="line173">
                    <p>
                        <asp:Label ID="lblDescripcion" runat="server" Text="Descripción:" 
                            meta:resourcekey="lblDescripcionResource1"></asp:Label>
                    </p>
                </div>
                <div class="line175">
                    <asp:TextBox ID="txtDescripcion" runat="server" CssClass="bordeimput" Width="700px"
                        Height="20px" MaxLength="200" meta:resourcekey="txtDescripcionResource1"></asp:TextBox>
                </div>
            </div>
            <div class="bar2">
                <div class="line173">
                    <p>
                        <asp:Label ID="lblInicio" runat="server" Text="Fecha Inicio:" 
                            meta:resourcekey="lblInicioResource1"></asp:Label></p>
                </div>
                <div class="line58">
                    <asp:TextBox ID="txtInicio" runat="server" CssClass="bordeimput" Width="200px" Height="20px"
                        MaxLength="20" meta:resourcekey="txtInicioResource1"></asp:TextBox>
                </div>
                <div class="line173">
                    <p>
                        <asp:Label ID="lblFin" runat="server" Text="Fecha Fin:" 
                            meta:resourcekey="lblFinResource1"></asp:Label>
                    </p>
                </div>
                <div class="line176">
                    <asp:TextBox ID="txtFin" runat="server" CssClass="bordeimput" MaxLength="20" Width="200px"
                        Height="20px" meta:resourcekey="txtFinResource1"></asp:TextBox>
                </div>
            </div>
            <div class="bar2">
            <div class="line173"><p>
                 <asp:Label ID="lblNumeroReferencia" runat="server" 
                 Text="Número de Referencia:" meta:resourcekey="lblNumeroReferenciaResource1" ></asp:Label> </p></div>
             <div class="line25">
                 <asp:TextBox ID="txtNumero" runat="server" CssClass="bordeimput" Width="236px" 
                        Height="20px" meta:resourcekey="txtNumeroResource1" MaxLength="15"></asp:TextBox>
             </div>
            
           </div>
            <div class="bar2">
                <div class="line173">
                    <p>
                        <asp:Label ID="lblCliente" runat="server" Text="Cliente:" 
                            meta:resourcekey="lblClienteResource1"></asp:Label></p>
                </div>
                <div class="line175">
                    <asp:DropDownList ID="ddlCliente" runat="server" CssClass="bordeimput" 
                        Width="700px" meta:resourcekey="ddlClienteResource1">
                    </asp:DropDownList>
                </div>
            </div>
            <div style="clear: both">
            </div>
        </div>
        <div class="bar4">
            <div class="bar4tresili">
                <div class="btns2">
                    <asp:Button ID="btnbuscar" runat="server" CssClass="<%$ Resources:generales,imgBuscar %>"
                        OnClick="btnbuscar_Click" meta:resourcekey="btnbuscarResource1" />
                </div>
                <div class="btns3">
                    <asp:Button ID="btnlimpiar" runat="server" CssClass="<%$ Resources:generales,imgLimpiar %>"
                        OnClick="btnlimpiar_Click" meta:resourcekey="btnlimpiarResource1" /></div>
                <div class="btns3">
                    <asp:Button ID="btnNuevo" runat="server" CssClass="<%$ Resources:generales,imgNuevo %>"
                        OnClick="btnNuevo_Click" meta:resourcekey="btnNuevoResource1" />
                </div>
                <div class="btns3">
                    <asp:Button runat="server" ID="btnEliminar" ClientIDMode="Static" CssClass="ocultarBoton"
                        OnClick="btnEliminar_Click" meta:resourcekey="btnEliminarResource1" />
                    <img id="imgeliminar" runat="server" onclick="javascript:VerificarSeleccionados();"  src="../img/btnEliminar.png"
                        alt="" class="imgCursor" />
                </div>
            </div>
        </div>
        <div class="separa">
            <div class="conteradio1">
                <div class="bar5">
                    <center> <asp:Label ID="lblmensaje" runat="server" CssClass="letraError" 
                            meta:resourcekey="lblmensajeResource1"></asp:Label> </center>
                    <asp:GridView ID="dgvbuscar" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                        Width="750px" CssClass="gridview" OnPageIndexChanging="dgvbuscar_PageIndexChanging"
                        OnRowCommand="dgvbuscar_RowCommand" meta:resourcekey="dgvbuscarResource1" 
                        PageSize="20">
                        <Columns>
                            <asp:TemplateField meta:resourcekey="TemplateFieldResource1">
                                <HeaderTemplate>
                                    <asp:Label ID="lblTitulo" runat="server" Text="Código" 
                                        meta:resourcekey="lblTituloResource2" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblCodigo" runat="server" Text='<%# Eval("CON_Codigo") %>' 
                                        width="80px" meta:resourcekey="lblCodigoResource2"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField meta:resourcekey="TemplateFieldResource2">
                                <HeaderTemplate>
                                    <asp:Label ID="lblTitulo" runat="server" Text="Descripción" 
                                        meta:resourcekey="lblTituloResource3" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div style="word-wrap: break-word; width: 200px;overflow: hidden;"><%# Eval("CON_Descripcion") %></div>
                                </ItemTemplate>
                            </asp:TemplateField>              
                            <asp:BoundField DataField="CON_FechaInico"  HeaderText="Fecha Inicio" 
                                ItemStyle-Width="100px" meta:resourcekey="BoundFieldResource1">
                                <ItemStyle Width="100px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="CON_FechaFin"  HeaderText="Fecha Fin" 
                                ItemStyle-Width="100px" meta:resourcekey="BoundFieldResource2">
                                <ItemStyle Width="100px"></ItemStyle>
                            </asp:BoundField>
                            <asp:TemplateField meta:resourcekey="TemplateFieldResource3">
                                <HeaderTemplate>
                                    <asp:Label ID="lblTitulo" runat="server" Text="Estado" 
                                        meta:resourcekey="lblTituloResource4" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label width="50px" ID="lblestadocontrato" runat="server" 
                                        Text='<%# Eval("CON_EstadoContrato") %>' 
                                        meta:resourcekey="lblestadocontratoResource1"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField meta:resourcekey="TemplateFieldResource4">
                                <HeaderTemplate>
                                    <asp:Label ID="lblTitulo" runat="server" Text="Cliente" 
                                        meta:resourcekey="lblTituloResource5" />
                                </HeaderTemplate>
                                <ItemStyle Wrap="true" Width="100px" />
                                <ItemTemplate>
                                    <asp:Label ID="lblrazonsocial" runat="server" Text='<%# Eval("CLI_RazonSocial") %>' 
                                        meta:resourcekey="lblrazonsocialResource2"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField meta:resourcekey="TemplateFieldResource5">
                                <HeaderTemplate>
                                    <asp:Label ID="lblTitulo" runat="server" Text="Número de Referencia" 
                                        meta:resourcekey="lblTituloResource7" />
                                </HeaderTemplate>
                                <ItemStyle Wrap="true" Width="100px" />
                                <ItemTemplate>
                                    <asp:Label ID="lblnumeroreferencia" runat="server" Text='<%# Eval("CON_NumReferencia") %>' 
                                        meta:resourcekey="lblnumeroreferencia"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField meta:resourcekey="TemplateFieldResource6">
                                <HeaderTemplate>
                                    <asp:Label ID="lblTitulo" runat="server" Text="Monto Máximo" 
                                        meta:resourcekey="lblTituloResource8" />
                                </HeaderTemplate>
                                <ItemStyle Wrap="true" Width="100px" />
                                <ItemTemplate>
                                    <asp:Label ID="lblmontomaximo" runat="server" Text='<%# Eval("CON_MontoMaximo") %>' 
                                        meta:resourcekey="lblmontomaximoResource2"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="60px" 
                                meta:resourcekey="TemplateFieldResource5">
                                <HeaderTemplate>
                                    <asp:Label ID="lblTitulo" runat="server" Text="Selec." 
                                        meta:resourcekey="lblTituloResource6" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkSeleccion" runat="server" 
                                        meta:resourcekey="chkSeleccionResource1" />
                                    <asp:HiddenField ID="hidIdContrato" runat="server" Value='<%# Eval("IDContrato") %>' />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" 
                                meta:resourcekey="TemplateFieldResource6">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lkbEditar" ToolTip="Modificar" runat="server" CausesValidation="False"
                                        CommandName="Editar" CommandArgument='<%# Eval("IDContrato") %>' meta:resourcekey="lkbEditarResource1">

                                <img src="../img/btnmdificar.png" alt="Modificar" style="border:0" /></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle Width="60px" />
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
        <div class="_dialog" id="msjConfirmacion" style="display: none">
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
                                <asp:Button ID="btnNo" OnClientClick="javascript:CerrarDialogo('msjConfirmacion');"
                                    runat="server" CssClass="<%$ Resources:generales,imgNo %>" 
                                    meta:resourcekey="btnNoResource1" />
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
                                <strong>
                                    <asp:Label ID="lblEliminarOk" runat="server" 
                                    Text="El registro se eliminó satisfactoriamente." 
                                    meta:resourcekey="lblEliminarOkResource1"></asp:Label></strong></p>
                        </div>
                        <div class="boxbtn">
                            <div class="centerclose">
                                <asp:Button runat="server" ID="btnCerrar1" CssClass="<%$ Resources:generales,imgCerrar %>"
                                    OnClientClick="CerrarDialogo('msjEliminacionOK');" CausesValidation="False" meta:resourcekey="btnCerrar1Resource1" />
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
                                <strong>
                                    <asp:Label ID="lblEliminarError" runat="server" 
                                    Text="Error de eliminación." meta:resourcekey="lblEliminarErrorResource1"></asp:Label></strong></p>
                        </div>
                        <div class="boxbtn">
                            <div class="centerclose">
                                <asp:Button runat="server" ID="btnCerrar2" CssClass="<%$ Resources:generales,imgCerrar %>"
                                    OnClientClick="CerrarDialogo('msjErrorEliminar');" meta:resourcekey="btnCerrar2Resource1" /></div>
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
                                Text="Debe seleccionar al menos un contrato para eliminar." 
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
