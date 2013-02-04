﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="area.aspx.cs" Inherits="Intertek.WEB.Mantenimiento.area" meta:resourcekey="PageResource1" %>
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
          
           var nombre = $('#<%=txtNombre.ClientID %>').val();          
           
           if (vacio(nombre))
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
                   Text="Registrar Área" meta:resourcekey="lblTituloResource1"  ></asp:Label></p>
           </div>
           <div class="ti2"><p></p></div>
        </div>
    </div>

    <div class="conteradio1">
        <div class="bar1">
            <div class="line173">
                    <p><asp:Label ID="lblNombre" runat="server" Text="Nombre:" 
                            meta:resourcekey="lblNombreResource1" ></asp:Label></p>
            </div>
            <div class="line172">
                    <asp:TextBox ID="txtNombre" ClientIDMode="Static" runat="server" CssClass="bordeimput"  Width="700px" 
                        Height="20px" MaxLength="50" meta:resourcekey="txtNombreResource1" 
                        onkeypress="javascript:return validarChr(event)"></asp:TextBox>                                                          
                    <%--<asp:RequiredFieldValidator ID="rfvNombre" runat="server" 
                    ErrorMessage="*" ControlToValidate="txtNombre" CssClass="letraError"
                    meta:resourcekey="rfvNombreResource1" ></asp:RequiredFieldValidator>--%>
             </div>
             <div class="line178"><p><asp:Label ID="lbl" runat="server" Text="*" class="letraError"></asp:Label></p></div>
        </div>
        <div class="bar5option">
             <div class="line182"><p>
                 <asp:Label ID="lblDescripcion" runat="server" Text="Descripción:" 
                     meta:resourcekey="lblDescripcionResource1" ></asp:Label> </p></div>
            <div class="line93">
                <asp:TextBox ID="txtDescripcion" runat="server" 
                 CssClass="bordeimput"  Width="700px" Height="76px" 
                     MaxLength="150" TextMode="MultiLine" onKeyUp="javascript:Count(this,150)" 
                    onChange="javascript:Count(this,150)" 
                    meta:resourcekey="txtDescripcionResource1"></asp:TextBox>
            </div>
                       
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
                meta:resourcekey="btnCancelarResource1"/>                        
        </div>
        <div class="btns3"> 
            <asp:Button ID="btnLimpiar" runat="server" CssClass="<%$ Resources:generales,imgLimpiar %>"  
        onclick="btnLimpiar_Click" CausesValidation="False" 
                meta:resourcekey="btnLimpiarResource1"/></div>
    </div>
   </div>

   <div id="msjRegistroOK" style="display: none">
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
                            OnClientClick="CerrarDialogoC('msjRegistroOK', 'areaBuscar.aspx');" CausesValidation="False" 
                                meta:resourcekey="btnCerrar1Resource1"/>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

   <div id="msjErrorGrabar" style="display: none" title="Error">
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
                                meta:resourcekey="btnCerrarResource1"/></div>
                    </div>
                </div>
            </div>
        </div>
    </div>
     <div id="msjErrorExiste" style="display: none" title="Error">
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
                                meta:resourcekey="Button1Resource1"/></div>
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
                            <asp:Button runat="server" ID="Button2" CssClass="<%$ Resources:generales,imgCerrar %>"
                                OnClientClick="CerrarDialogo('msjAlertaCaracteres');" 
                                CausesValidation="False" meta:resourcekey="Button2Resource1"/></div>
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
                            <strong><asp:Label ID="lblCampoObligatorio" runat="server" 
                                Text="Debe ingresar los campos obligatorios marcados con asterisco(*)" 
                                meta:resourcekey="lblCampoObligatorioResource1"></asp:Label></strong>
                         </p>
                    </div>
                    <div class="boxbtn">
                        <div class="centerclose">
                            <asp:Button runat="server" ID="btnCerrarObligatorios" CssClass="<%$ Resources:generales,imgCerrar %>"
                                OnClientClick="CerrarDialogo('msjCamposObligatorios');" 
                                CausesValidation="False" meta:resourcekey="Button2Resource1"/></div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
</asp:Content>
