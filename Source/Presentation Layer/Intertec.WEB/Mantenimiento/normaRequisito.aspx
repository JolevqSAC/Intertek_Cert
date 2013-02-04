<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="normaRequisito.aspx.cs" Inherits="Intertek.WEB.Mantenimiento.normaRequisito" meta:resourcekey="PageResource1" %>
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
        var nombre = document.getElementById('<%=txtNombre.ClientID %>').value;
        var acreditador = document.getElementById('<%=txtAcreditador.ClientID %>').value;
        var anio = document.getElementById('<%=txtanio.ClientID %>').value;
        if (vacio(nombre) || vacio(acreditador) || vacio(anio))
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
                   Text="Registrar Norma Requisito" meta:resourcekey="lblTituloResource1" ></asp:Label></p>
           </div>
           <div class="ti2"><p></p></div>
        </div>
    </div>

    <div class="conteradio1">
        <div class="bar1">
            <div class="line173">
                    <p><asp:Label ID="lblNombre" runat="server" Text="Nombre" 
                            meta:resourcekey="lblNombreResource1"></asp:Label></p>
            </div>
            <div class="line172">
                    <asp:TextBox ID="txtNombre" runat="server" CssClass="bordeimput"  Width="700px" 
                        Height="20px" MaxLength="200" onkeypress="javascript:return validarChr(event)" 
                        meta:resourcekey="txtNombreResource1"></asp:TextBox> 
             </div>
             <div class="line178"><p><asp:Label ID="Label1" runat="server" Text="*" class="letraError"></asp:Label></p></div>
        </div>

        <div class="bar5option">
                <div class="line182"><p>
                 <asp:Label ID="lblDescripcion" runat="server" 
                 Text="Descripción:" meta:resourcekey="lblDescripcionResource1" ></asp:Label> </p></div>
                <div class="line93">
                <asp:TextBox ID="txtDescripcion" runat="server" 
                 CssClass="bordeimput"  Width="700px" Height="76px" 
                     MaxLength="200" TextMode="MultiLine" onKeyUp="javascript:Count(this,200);" 
                        onChange="javascript:Count(this,200);" 
                        meta:resourcekey="txtDescripcionResource1"></asp:TextBox>
         
                </div>
                       
          </div>

        <div class="bar2">
             <div class="line173"><p>
                 <asp:Label ID="lblAcreditador" runat="server" 
                 Text="Acreditador:" meta:resourcekey="lblAcreditadorResource1" ></asp:Label> </p></div>
             <div class="line172">
                <asp:TextBox ID="txtAcreditador" runat="server" 
                 CssClass="bordeimput"  Width="700px" Height="20px" MaxLength="120" onkeypress="javascript:return validarChr(event)"
                     meta:resourcekey="txtAcreditadorResource1"></asp:TextBox>                 
              </div>
              <div class="line178"><p><asp:Label ID="Label2" runat="server" Text="*" class="letraError"></asp:Label></p></div>
        </div>
            <div class="bar2">
                 <div class="line173"><p>
                     <asp:Label ID="lblanio" runat="server" 
                     Text="Año:" meta:resourcekey="lblanioResource1" ></asp:Label> </p></div>
                 <div class="line174">
                    <asp:TextBox ID="txtanio" runat="server" 
                        CssClass="bordeimput"  Width="120px" Height="20px" MaxLength="4" 
                         onkeypress="javascript:return validarNro(event)" 
                         meta:resourcekey="txtanioResource1"></asp:TextBox>  
                 </div>
                 <div class="line178"><p><asp:Label ID="Label3" runat="server" Text="*" class="letraError"></asp:Label></p></div>
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
                meta:resourcekey="btnCancelarResource1"  />                        
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
                            <asp:Button runat="server" ID="btnCerrar" CssClass="<%$ Resources:generales,imgCerrar %>"
                            OnClientClick="CerrarDialogoC('msjRegistroOK', 'normaRequisitoBuscar.aspx');" CausesValidation="False" 
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
                            <asp:Button runat="server" ID="btnCerrar1" CssClass="<%$ Resources:generales,imgCerrar %>"
                                OnClientClick="CerrarDialogo('msjErrorGrabar');" 
                                meta:resourcekey="btnCerrar1Resource2" /></div>
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
                            <asp:Button runat="server" ID="btnCerrar2" CssClass="<%$ Resources:generales,imgCerrar %>"
                                OnClientClick="CerrarDialogo('msjErrorExiste');" 
                                meta:resourcekey="btnCerrar2Resource1"  /></div>
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
                            <strong><asp:Label ID="Label4" runat="server" 
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
