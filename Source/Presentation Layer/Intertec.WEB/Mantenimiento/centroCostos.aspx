<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="centroCostos.aspx.cs" Inherits="Intertek.WEB.Mantenimiento.centroCostos" meta:resourcekey="PageResource1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
  
   <script type="text/javascript">
       $(document).ready(function () {
           //CrearDialogo(nombreDIV, alto, ancho, titulo);
           CrearDialogo('msjRegistroOK', 162, 360, '');
           CrearDialogo('msjErrorGrabar', 162, 360, '');
           CrearDialogo('msjErrorExiste', 162, 360, '');
           CrearDialogo('msjAlertaCaracteres', 162, 360, '');
           CrearDialogo('msjCamposObligatorios', 162, 360, '');
       });

       function ValidaCampos() {
           var numero = document.getElementById('<%=txtNumero.ClientID %>').value;
           var valor = document.getElementById('<%=ddlarea.ClientID %>').value;
           if (vacio(numero) || valor == 0)
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
           <div class="ti1"><p>
               <asp:Label ID="lblTitulo" runat="server" 
                   Text="Registrar Centros de Costo" meta:resourcekey="lblTituloResource1" ></asp:Label></p>
           </div>
           <div class="ti2"><p></p></div>
        </div>
    </div>

    <div class="conteradio1">
        <div class="bar1">
            <div class="line173">
                    <p><asp:Label ID="lblNumero" runat="server" Text="Número:" 
                            meta:resourcekey="lblNumeroResource1"  ></asp:Label></p>
            </div>
            <div class="line184">
                    <asp:TextBox ID="txtNumero" runat="server" CssClass="bordeimput"  Width="275px" 
                        Height="20px" 
                        meta:resourcekey="txtNombreResource1" MaxLength="20"></asp:TextBox>  
             </div>
            <div class="line8"><p><asp:Label ID="Label1" runat="server" Text="*" class="letraError"></asp:Label></p></div>
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
                     Width="275px" meta:resourcekey="ddlareaResource1" >
                 </asp:DropDownList>
             </div>
             <div class="line8"><p><asp:Label ID="Label2" runat="server" Text="*" class="letraError"></asp:Label></p></div>
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
         onclick="btnCancelar_Click" CausesValidation="False" 
                meta:resourcekey="btnCancelarResource1" />                        
        </div>
        <div class="btns3"> 
            <asp:Button ID="btnLimpiar" runat="server" CssClass="<%$ Resources:generales,imgLimpiar %>"  
        onclick="btnLimpiar_Click" CausesValidation="False" 
                meta:resourcekey="btnLimpiarResource1" /></div>
    </div>
   </div>

   <div class="_dialog" id="msjRegistroOK" style="display: none">
        <div class="contealert">
            <div class="boxtext">
                <div class="boxdentro">
                    <div class="boxletra">
                        <p>
                            <strong> <asp:Label ID="lblConfirmacion" runat="server" 
                                Text="El registro se Grabó satisfactoriamente" 
                                meta:resourcekey="lblConfirmacionResource1"></asp:Label></strong></p>
                    </div>
                    <div class="boxbtn">
                        <div class="centerclose">
                            <asp:Button runat="server" ID="btnCerrar1" CssClass="<%$ Resources:generales,imgCerrar %>"
                            OnClientClick="CerrarDialogoC('msjRegistroOK','centroCostosBuscar.aspx');" CausesValidation="False" 
                                meta:resourcekey="btnCerrar1Resource1"/>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

   <div class="_dialog" id="msjErrorGrabar" style="display: none" title="Error">
        <div class="contealertError">
            <div class="boxtext">
                <div class="boxdentro">
                    <div class="boxletra">
                        <p>
                            <strong><asp:Label ID="lblErrorGrabar" runat="server" 
                                Text="Error al grabar el registro." meta:resourcekey="lblErrorGrabarResource1"></asp:Label></strong></p>
                    </div>
                    <div class="boxbtn">
                        <div class="centerclose">
                            <asp:Button runat="server" ID="btnCerrar" CssClass="<%$ Resources:generales,imgCerrar %>"
                                OnClientClick="CerrarDialogo('msjErrorGrabar');" CausesValidation="False"
                                meta:resourcekey="btnCerrarResource1" /></div>
                    </div>
                </div>
            </div>
        </div>
    </div>
     <div class="_dialog" id="msjErrorExiste" style="display: none" title="Error">
        <div class="contealertError">
            <div class="boxtext">
                <div class="boxdentro">
                    <div class="boxletra">
                        <p>
                            <strong><asp:Label ID="lblErrorExiste" runat="server" 
                                Text="Ya existe el registro ingresado" 
                                meta:resourcekey="lblErrorExisteResource1"></asp:Label></strong></p>
                    </div>
                    <div class="boxbtn">
                        <div class="centerclose">
                            <asp:Button runat="server" ID="Button1" CssClass="<%$ Resources:generales,imgCerrar %>"
                                OnClientClick="CerrarDialogo('msjErrorExiste');" CausesValidation="False" 
                                meta:resourcekey="Button1Resource1" /></div>
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
                                 meta:resourcekey="lblmsj2Resource1"></asp:Label>
                        </strong>
                         </p>
                    </div>
                    <div class="boxbtn">
                        <div class="centerclose">
                            <asp:Button runat="server" ID="btncerrarcaracteres" CssClass="<%$ Resources:generales,imgCerrar %>"
                                OnClientClick="CerrarDialogo('msjAlertaCaracteres');" Text="Cerrar" 
                                CausesValidation="False" meta:resourcekey="btncerrarcaracteresResource1"/>
                            </div>
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
                            <strong><asp:Label ID="Label3" runat="server" 
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
