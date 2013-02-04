<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="cargos.aspx.cs" Inherits="Intertek.WEB.Mantenimiento.cargos" meta:resourcekey="PageResource1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
<script language="javascript" type="text/javascript">
    $(document).ready(function () {
        //CrearDialogo(nombreDIV, alto, ancho, titulo);
        CrearDialogo('msjSatisfactorio', 162, 360, '');
        CrearDialogo('msjAlertaCaracteres', 162, 360, '');
        CrearDialogo('msjCamposObligatorios', 162, 360, '');
    });

    function ValidaCampos() {
        var nombre = $('#<%=txtNombre.ClientID %>').val();
        var valor = $('#<%=ddlarea.ClientID %>').val();
        if (vacio(nombre) || valor==0)
            MostrarMensaje('msjCamposObligatorios');        
        else {
            $('#btnGrabar').trigger("click");
            return true;
            }
    }  


</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="contentgeneral">
    <div class="titulo1">
        <div class="titulo2">
           <div class="ti1"><p><asp:Label ID="lblTitulo" runat="server" Text="Registrar Cargo" 
                   meta:resourcekey="lblTituloResource1"></asp:Label></p>
           </div>
           <div class="ti2"><p></p></div>
        </div>
    </div>

    <div class="conteradio1">
        <div class="bar1">
            <div class="line173"><p>
                <asp:Label ID="lblNombre" runat="server" Text="Nombre:" 
                    meta:resourcekey="lblNombreResource1"></asp:Label></p></div>
            <div class="line172">
                <asp:TextBox ID="txtNombre" runat="server" CssClass="bordeimput" MaxLength="50" 
                    Width="700px" Height="20px" onkeypress="javascript:return validarChr(event)"
                    meta:resourcekey="txtNombreResource1"></asp:TextBox> 
                <%--<asp:RequiredFieldValidator ID="rfvNombre" runat="server" ErrorMessage="*" 
                    ControlToValidate="txtNombre" CssClass="letraError" 
                    meta:resourcekey="rfvNombreResource1"></asp:RequiredFieldValidator>--%>
           </div>
           <div class="line178"><p><asp:Label ID="lbl" runat="server" Text="*" class="letraError"></asp:Label></p></div>
        </div>
        <div class="bar2">
         <div class="line173">
         <p>
            <asp:Label ID="lblArea" runat="server" Text="Área:" 
                 meta:resourcekey="lblAreaResource1"></asp:Label> 
         </p>
         </div>
            <div class="line184">
                 <asp:DropDownList runat="server" ID="ddlarea" CssClass="bordeimput" 
                     Width="250px" meta:resourcekey="ddlareaResource1" >
                 </asp:DropDownList>
                    <%--<asp:CompareValidator ID="cvarea" runat="server" CssClass="letraError" ErrorMessage="*"
                        ControlToValidate="ddlarea" ValueToCompare="0" Operator="NotEqual" 
                 Type="Integer" meta:resourcekey="cvareaResource1"></asp:CompareValidator> --%>
             
             </div>
             <div class="line178"><p><asp:Label ID="Label2" runat="server" Text="*" class="letraError"></asp:Label></p></div>
           </div>

        <div class="bar5option">
           <div class="line182"><p>
               <asp:Label ID="lblDescripcion" runat="server" Text="Descripción:" 
                   meta:resourcekey="lblDescripcionResource1"></asp:Label> </p></div>
         <div class="line93">
             <asp:TextBox ID="txtDescripcion" runat="server" CssClass="bordeimput" 
                 MaxLength="200" Width="700px" 
                 Height="76px"  TextMode="MultiLine" onKeyUp="javascript:Count(this,200);" 
                 onChange="javascript:Count(this,200);" 
                 meta:resourcekey="txtDescripcionResource1"></asp:TextBox ></div>
        </div>
         <div class="bar2">
                <%-- <div >
                    <asp:Label ID="lblCampoObligatorio" runat="server" 
                         Text="(*) Campos obligatorios" class="letraError" 
                         meta:resourcekey="lblCampoObligatorioResource1"></asp:Label>
              </div>--%>
            </div>
           <div style="clear:both"></div>
        </div>              
   

    <div class="bar4">
        <div class="bar4tres">
           <div class="btns2">
            <asp:Button ID="btnGrabar" runat="server"  CssClass="ocultarBoton" ClientIDMode="Static"
                        onclick="btnGrabar_Click" meta:resourcekey="btnGrabarResource1"/>
            <img id="imgGrabar" runat="server" src="../img/btngrabar2.png" onclick="javascript:return ValidaCampos()" alt="" class="imgCursor"/>         
           </div> 
            <div class="btns3">
                <asp:Button ID="btnCancelar" runat="server" CssClass="<%$ Resources:generales,imgCancelar %>" 
            CausesValidation="False" 
                    onclick="btnCancelar_Click" 
                    OnClientClick="javascript:DesactivarValidacion();" 
                    meta:resourcekey="btnCancelarResource1" /></div>
            <div class="btns3"> 
                <asp:Button ID="btnLimpiar" runat="server" CssClass="<%$ Resources:generales,imgLimpiar %>"  
             CausesValidation="False" 
                    onclick="btnLimpiar_Click" 
                    OnClientClick="javascript:DesactivarValidacion();" 
                    meta:resourcekey="btnLimpiarResource1"/></div>
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
                                Text="El registro se grabó satisfactoriamente" 
                                meta:resourcekey="lblConfirmacionResource1"></asp:Label></strong></p>
                    </div>
                    <div class="boxbtn">
                        <div class="centerclose">
                            <asp:Button ID="btnCerrarConfirmacion" OnClientClick="javascript:CerrarDialogoC('msjSatisfactorio', 'cargosBuscar.aspx');"
                                runat="server" CssClass="<%$ Resources:generales,imgCerrar %>" 
                                CausesValidation="False" meta:resourcekey="btnCerrarConfirmacionResource1" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="msjAlertaCaracteres" style="display: none" title="Error">
        <div class="contealertError">
            <div class="boxtext">
                <div class="boxdentro">
                    <div class="boxletra2">
                         <p>
                         <strong>
                             <asp:Label ID="lblmsj1" runat="server" Text="Ud. debe ingresar: " 
                                 meta:resourcekey="lblmsj1Resource1"></asp:Label> 
                                <span id="spanCaracteres"></span>
                            <asp:Label ID="lblmsj2" runat="server" Text="caracteres." 
                                 meta:resourcekey="lblmsj2Resource1" ></asp:Label>
                        </strong>
                         </p>
                    </div>
                    <div class="boxbtn">
                        <div class="centerclose">
                            <asp:Button runat="server" ID="btncerrarcaracteres" CssClass="<%$ Resources:generales,imgCerrar %>"
                                OnClientClick="CerrarDialogo('msjAlertaCaracteres');" 
                                CausesValidation="False" meta:resourcekey="btncerrarcaracteresResource1"/></div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="msjCamposObligatorios" style="display: none" title="Error">
        <div class="contealertError">
            <div class="boxtext">
                <div class="boxdentro">
                    <div class="boxletra2">
                         <p>
                            <strong><asp:Label ID="Label1" runat="server" 
                                Text="Debe ingresar los campos obligatorios marcados con asterisco(*)" 
                                meta:resourcekey="lblCampoObligatorioResource1"></asp:Label></strong>
                         </p>
                    </div>
                    <div class="boxbtn">
                        <div class="centerclose">
                            <asp:Button runat="server" ID="btnCerrarObligatorios" CssClass="<%$ Resources:generales,imgCerrar %>"
                                OnClientClick="CerrarDialogo('msjCamposObligatorios');" 
                                CausesValidation="False" meta:resourcekey="btncerrarcaracteresResource1"/></div>
                    </div>
                </div>
            </div>
        </div>
    </div>
  </div>  
</asp:Content>


