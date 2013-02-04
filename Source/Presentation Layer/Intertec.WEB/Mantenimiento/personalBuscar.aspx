<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="personalBuscar.aspx.cs" Inherits="Intertek.WEB.Mantenimiento.personalBuscar" meta:resourcekey="PageResource1" %>
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

    function validarNro(e) {
        var key;
        if (window.event) // IE
        {
            key = e.keyCode;
        }
        else if (e.which) // Netscape/Firefox/Opera
        {
            key = e.which;
        }

        if (key < 48 || key > 57) {
            if (key == 8) // backspace (retroceso)
            { return true; }
            else
            { return false; }
        }
        return true;
    }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="contentgeneral">
    <div class="titulo1">
        <div class="titulo2">
           <div class="ti1"><p>
               <asp:Label ID="lblTitulo" runat="server" 
                   Text="Buscar Trabajador" meta:resourcekey="lblTituloResource1"></asp:Label></p>
           </div>
           <div class="ti2"><p></p></div>
        </div>
    </div>
     <div class="conteradio1">
        <div class="bar1">
            <div class="line173"><p><asp:Label ID="lblcodigo" runat="server" Text="Codigo:" 
                    meta:resourcekey="lblcodigoResource1" ></asp:Label></p></div>
            <div class="line175">
                <asp:TextBox ID="txtCodigo" runat="server" CssClass="bordeimput" Width="700px" 
                    Height="20px" meta:resourcekey="txtCodigoResource1" MaxLength="10"></asp:TextBox></div>
        </div>
        
        <div class="bar2">
            <div class="line173"><p><asp:Label ID="lblNombre" runat="server" Text="Nombres:" 
                    meta:resourcekey="lblNombreResource1" ></asp:Label></p></div>
            <div class="line175">
                <asp:TextBox ID="txtNombre" runat="server" CssClass="bordeimput" Width="700px" 
                    Height="20px" meta:resourcekey="txtNombreResource1" MaxLength="50"></asp:TextBox></div>
        </div>
        <div class="bar2">
         <div class="line173"><p><asp:Label ID="lblApellidos" runat="server" 
                 Text="Apellidos:" meta:resourcekey="lblApellidosResource1"  
        ></asp:Label> </p></div>
         <div class="line175">
             <asp:TextBox ID="txtApellidos" runat="server" 
                 CssClass="bordeimput" Width="700px" Height="20px" 
                 meta:resourcekey="txtApellidosResource1" MaxLength="150"
        ></asp:TextBox></div>
        </div>
        <div class="bar2">
           <div class="line173"><p>
               <asp:Label ID="lblDNI" runat="server" Text="DNI:" meta:resourcekey="lblDNIResource1"  
        ></asp:Label> </p></div>
         <div class="line158">
             <asp:TextBox ID="txtDNI" runat="server" CssClass="bordeimput" Height="20px" 
                 onkeypress="javascript:return validarNro(event)" 
                 meta:resourcekey="txtDNIResource1" MaxLength="8"
        ></asp:TextBox></div>

        <div class="line173">
         <p>
            <asp:Label ID="lblcargo" runat="server" Text="Cargo:" 
                 meta:resourcekey="lblcargoResource1"></asp:Label> 
         </p>
         </div>
         <div class="line176">
             <asp:DropDownList runat="server" ID="ddlcargo" CssClass="bordeimput" 
                 Width="200px" Height="20px" meta:resourcekey="ddlcargoResource1">
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
                CssClass="<%$ Resources:generales,imgNuevo %>" onclick="btnNuevo_Click" meta:resourcekey="btnNuevoResource1" 
         /> </div>
          <div class="btns3">
         <asp:Button runat="server" ID="btnEliminar" ClientIDMode="Static" 
                         CssClass="ocultarBoton" onclick="btnEliminar_Click" 
                  meta:resourcekey="btnEliminarResource1" /> 
          <img id="imgeliminar" runat="server" onclick="javascript:VerificarSeleccionados();"
                                         src="../img/btnEliminar.png" alt="" class="imgCursor"  /></div>
        </div>
    </div>

    <div class="separa">
<div class="conteradio1">
    <div class="bar5">

                     <center> <asp:Label ID="lblmensaje" runat="server" CssClass="letraError" 
                             meta:resourcekey="lblmensajeResource1"></asp:Label></center>
                    <asp:GridView ID="gvBuscar" runat="server" CssClass="gridview" AutoGenerateColumns="False"
                        AllowPaging="True" Width="700px" 
                        OnPageIndexChanging="gvBuscar_PageIndexChanging" 
                         onrowcommand="gvBuscar_RowCommand" meta:resourcekey="gvBuscarResource1" PageSize="20"
                        >
                        <Columns>
                            <asp:BoundField DataField="PER_Codigo" HeaderText="Código" 
                                ItemStyle-Width="100px" meta:resourcekey="BoundFieldResource1" >
                                <ItemStyle Width="100px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="PER_Nombres" HeaderText="Nombres" 
                                ItemStyle-Width="150px" meta:resourcekey="BoundFieldResource2" >
                                 <ItemStyle Width="150px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="PER_Apellidos" HeaderText="Apellidos" 
                                ItemStyle-Width="150px" meta:resourcekey="BoundFieldResource3" >
                                <ItemStyle Width="150px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="PER_DNI" HeaderText="DNI" ItemStyle-Width="90px" 
                                meta:resourcekey="BoundFieldResource4" >  
                                <ItemStyle Width="90px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="CAR_Nombre" HeaderText="Cargo" 
                                meta:resourcekey="BoundFieldResource5" />
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="60px" 
                                meta:resourcekey="TemplateFieldResource1">
                               <HeaderTemplate>
                                    <asp:Label ID="lblTitulo" runat="server" Text="Selec." 
                                        meta:resourcekey="lblTituloResource2"/>
                               </HeaderTemplate>               
                               <ItemTemplate>
                                    <asp:CheckBox ID="chkSeleccion" runat="server" 
                                        meta:resourcekey="chkSeleccionResource1" />
                                    <asp:HiddenField ID="hidIdPersonal" runat="server" Value='<%# Eval("IDPersonal") %>' />
                               </ItemTemplate>  

                                <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                             </asp:TemplateField>    
                            <asp:TemplateField  ItemStyle-HorizontalAlign="Center" 
                                meta:resourcekey="TemplateFieldResource2">
                                <ItemStyle Width="80px" />
                                <ItemTemplate>
                                 <asp:LinkButton ID="lkbEditar" ToolTip="Modificar" runat="server" CausesValidation="False"
                                    CommandName="Editar" CommandArgument='<%# Eval("IDPersonal") %>' 
                                        meta:resourcekey="lkbEditarResource1">
                                    <img src="/img/btnmdificar.png" alt="Modificar" style="border:0" /></asp:LinkButton>
                                </ItemTemplate>
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
                                Text="Debe seleccionar al menos una personal para eliminar." 
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
