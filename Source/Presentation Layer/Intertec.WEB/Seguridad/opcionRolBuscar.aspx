<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="opcionRolBuscar.aspx.cs" Inherits="Intertek.WEB.Seguridad.opcionRolBuscar"
    meta:resourcekey="PageResource1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {

            //CrearDialogo(nombreDIV, alto, ancho, titulo);
            CrearDialogo('msjSatisfactorio', 162, 360, '');
            CrearDialogoEliminar('msjError', 162, 360, '');

        });
        function EliminarSi() {

            var idRolEliminar = $('#hdEliminarID').val();
            var idUsuarioLog = $('#hdUsuario').val();
            //alert('idUsuarioLog:' + idUsuarioLog);
            var parametros = { idRol: idRolEliminar, idUsuario: idUsuarioLog };

            var rsp = EliminarRegistro('../Seguridad/opcionRolBuscar.aspx/Eliminar', parametros, false);

            CerrarDialogo('msjError');
         
            if (jQuery.trim(rsp) == '1') {
                //alert('ingreso a cargar resultado: ' + rsp);
                MostrarMensaje('msjSatisfactorio');
                $('#btnCargarGrilla').trigger("click");
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
                        <asp:Label ID="lblTitulo" runat="server" Text="Buscar Roles" meta:resourcekey="lblTituloResource1"></asp:Label></p>
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
                            meta:resourcekey="lblcodigoResource1"></asp:Label></p>
                </div>
                <div class="line175">
                    <asp:TextBox ID="txtcodigo" runat="server" CssClass="bordeimput" Width="250px" 
                        MaxLength="10" Height="20px" meta:resourcekey="txtcodigoResource1"></asp:TextBox></div>
            </div>

            <div class="bar2">
                <div class="line173">
                    <p>
                        <asp:Label ID="lblNombre" runat="server" Text="Nombre:" 
                            meta:resourcekey="lblNombreResource1"></asp:Label></p>
                </div>
                <div class="line175">
                    <asp:TextBox ID="txtRol" runat="server" CssClass="bordeimput" Width="700px" 
                        Height="20px" MaxLength="100" meta:resourcekey="txtRolResource2"></asp:TextBox></div>
            </div>

             <div class="bar2">
                <div class="line173">
                    <p>
                        <asp:Label ID="lbldescripcion" runat="server" Text="Descripción:" 
                            meta:resourcekey="lbldescripcionResource1"></asp:Label></p>
                </div>
                <div class="line175">
                    <asp:TextBox ID="txtdescripcion" runat="server" CssClass="bordeimput" 
                        Width="700px" Height="20px" MaxLength="200" 
                        meta:resourcekey="txtdescripcionResource1"></asp:TextBox></div>
            </div>
            
            <div style="clear: both">
            </div>
        </div>
        <div class="bar4">
            <div class="bar4tres">
                <div class="btns2">
                    <asp:Button ID="btnBuscar" runat="server" CssClass="<%$ Resources:generales,imgBuscar %>"
                        OnClick="btnBuscar_Click" meta:resourcekey="btnBuscarResource1" /></div>
                <div class="btns3">
                    <asp:Button ID="btnLimpiar" runat="server" CssClass="<%$ Resources:generales,imgLimpiar %>"
                        OnClick="btnLimpiar_Click" meta:resourcekey="btnLimpiarResource1" /></div>
                <div class="btns3">
                    <asp:Button ID="btnNuevo" runat="server" CssClass="<%$ Resources:generales,imgNuevo %>"
                        OnClick="btnNuevo_Click" meta:resourcekey="btnNuevoResource1" />
                    <asp:Button runat="server" ID="btnCargarGrilla" ClientIDMode="Static" 
                        onclick="btnCargarGrilla_Click" CssClass="ocultarBoton" 
                        meta:resourcekey="btnCargarGrillaResource1" />        
                </div>
            </div>
        </div>
        <div class="separa">
            <div class="conteradio1">
                <div class="bar5">
                 
                    <center> <asp:Label ID="lblmensaje" runat="server" CssClass="letraError" 
                            meta:resourcekey="lblmensajeResource1"></asp:Label></center>
                    <asp:GridView ID="gvBuscar" runat="server" CssClass="gridview" AutoGenerateColumns="False"
                        AllowPaging="True" Width="500px" OnPageIndexChanging="gvBuscar_PageIndexChanging"
                        meta:resourcekey="gvBuscarResource1" onrowcommand="gvBuscar_RowCommand" 
                        PageSize="20">
                        <Columns>
                           <%-- <asp:HyperLinkField HeaderText="Modificar" DataNavigateUrlFields="IDRol" Text="<img alt='' src='../img/editar.png' border='0' />"
                                DataNavigateUrlFormatString="opcionesRol.aspx?idRol={0}" meta:resourcekey="HyperLinkFieldResource1" />--%>
                           
                        <asp:BoundField DataField="ROL_Codigo" HeaderText="Código" meta:resourcekey="BoundFieldResource2">
                            <ItemStyle Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ROL_Nombre" HeaderText="Nombre" 
                                meta:resourcekey="BoundFieldResource3" >
                            <ItemStyle Width="150px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ROL_Descripcion" HeaderText="Descripción" 
                                meta:resourcekey="BoundFieldResource1" >
                            <ItemStyle Width="300px" />
                            </asp:BoundField>
                           
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" 
                                meta:resourcekey="TemplateFieldResource2">
                                <ItemStyle Width="80px" />
                                <ItemTemplate>
                                
                                   <asp:LinkButton ID="LinkButton1" ToolTip="Modificar" runat="server" CausesValidation="False"
                                          CommandName="Editar" CommandArgument='<%# Eval("IDRol") %>' 
                                        meta:resourcekey="hypActualizarResource1">
                                        <img src="/img/btnmdificar.png" alt="Modificar" style="border:0" />                                  
                                  </asp:LinkButton>

                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <PagerSettings Mode="NumericFirstLast" PageButtonCount="2" />
                    </asp:GridView>
                    
                </div>
                <div style="clear: both">
                    <input type="hidden" id="hdEliminarID" value="" />
                    <asp:HiddenField runat="server"
                        ID="hdUsuario" ClientIDMode="Static" />
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
</asp:Content>