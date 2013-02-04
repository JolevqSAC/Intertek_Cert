<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="normaProductoBuscar.aspx.cs" Inherits="Intertek.WEB.Mantenimiento.normaProductoBuscar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
<script language="javascript" type="text/javascript">
    $(document).ready(function () {
        //CrearDialogo(nombreDIV, alto, ancho, titulo);
        CrearDialogo('msjSatisfactorio', 162, 360, '');
        CrearDialogoEliminar('msjError', 162, 360, '');
    });
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
                        <asp:Label ID="lblTitulo" runat="server" Text="Buscar Normas de Producto"></asp:Label></p>
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
                    <asp:Label ID="lblNombre" runat="server" Text="Nombre:"></asp:Label>
                </p>
            </div>
            <div class="line175">
                <asp:TextBox ID="txtNombre" runat="server" CssClass="bordeimput" MaxLength="100"
                    Width="300px"></asp:TextBox></div>
            </div>
         <div class="bar2">
                    <div class="line173">
                <p>
                    <asp:Label ID="lblAcreditador" runat="server" Text="Acreditador:"></asp:Label>
                </p>
            </div>
            <div class="line175">
                <asp:TextBox ID="txtAcreditador" runat="server" CssClass="bordeimput" MaxLength="150"
                    Width="300px"></asp:TextBox></div>
            </div>
            <div class="bar2">
            <div class="line173"><p><asp:Label ID="lblProducto" runat="server" Text="Producto:" 
        ></asp:Label></p></div>
            <div class="line175"><asp:DropDownList runat="server" ID="ddlProducto" CssClass="bordeimput" 
                    Width="300px"></asp:DropDownList></div>
        </div>
            <div style="clear: both">
            </div>
        </div>
        <div class="bar4">
            <div class="bar4tres">
                <div class="btns2">
                    <asp:Button ID="btnBuscar" runat="server" Text="" CssClass="<%$ Resources:generales,imgBuscar %>"
                        OnClick="btnBuscar_Click" /></div>
                <div class="btns3">
                    <asp:Button ID="btnLimpiar" runat="server" Text="" CssClass="<%$ Resources:generales,imgLimpiar %>"
                        OnClick="btnLimpiar_Click" /></div>
                <div class="btns3">
                    <asp:Button ID="btnNuevo" runat="server" Text="" CssClass="<%$ Resources:generales,imgNuevo %>"
                        OnClick="btnNuevo_Click" />
                   
                </div>
                 <asp:Button runat="server" ID="btnEliminar" ClientIDMode="Static" CssClass="ocultarBoton"
                        OnClick="btnEliminar_Click" />
            </div>
        </div>
        <div class="separa">
            <div class="conteradio1">
                <div class="bar5">
                    <center>
                        <asp:GridView ID="gvBuscar" runat="server" CssClass="gridview" AutoGenerateColumns="False"
                            AllowPaging="True" Width="700px" 
                            OnPageIndexChanging="gvBuscar_PageIndexChanging">
                            <Columns>
                                <asp:BoundField DataField="NOR_Nombre" HeaderText="Nombre" />
                                <asp:BoundField DataField="NOR_Acreditador" HeaderText="Acreditador" />
                                <asp:BoundField DataField="PRO_Nombre" HeaderText="Producto" />
                                <asp:BoundField DataField="NOR_Anio" HeaderText="Año" ItemStyle-Width="90" ItemStyle-HorizontalAlign="Center" />
                                <asp:TemplateField HeaderText="Opciones" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle Width="80px" />
                                    <ItemTemplate>
                                        <asp:HyperLink runat="server" ID="hypActualizar" ImageUrl="~/img/editar.png" BorderWidth="0"
                                            CssClass="imgCursor" NavigateUrl='<%# "~/Mantenimiento/normaProductos.aspx?idNormaProducto="+Eval("IDNorma") %>'></asp:HyperLink>
                                        &nbsp;
                                        <img onclick="javascript:MostrarMensajeEliminar('msjError','<%# Eval("IDNorma") %>');"
                                            src="../img/quitar.png" alt="" class="imgCursor" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </center>
                </div>
                <div style="clear: both">
                    <asp:HiddenField runat="server" ClientIDMode="Static" ID="hdEliminarID" Value="" />
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
                                <asp:Label ID="lblEliminar" runat="server" Text="Desea eliminar el registro?"></asp:Label></strong></p>
                    </div>
                    <div class="boxbtn">
                        <div class="centerclose">
                            <asp:Button ID="btnAceptar" OnClientClick="javascript:EliminarSi();" runat="server"
                                Text="" CssClass="<%$ Resources:generales,imgSi %>" />
                            <asp:Button ID="btnNo" OnClientClick="javascript:CerrarDialogo('msjError');" runat="server"
                                Text="" CssClass="<%$ Resources:generales,imgNo %>" />
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
                                <asp:Label ID="lblConfirmacion" runat="server" Text="El registro se eliminó satisfactoriamente"></asp:Label></strong></p>
                    </div>
                    <div class="boxbtn">
                        <div class="centerclose">
                            <asp:Button ID="btnCerrarConfirmacion" OnClientClick="javascript:CerrarDialogo('msjSatisfactorio');"
                                runat="server" Text="" CssClass="<%$ Resources:generales,imgCerrar %>" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
