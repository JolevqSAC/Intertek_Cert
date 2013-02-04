<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="tipoEnvase.aspx.cs" Inherits="Intertek.WEB.Mantenimiento.tipoEnvase" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
<script type="text/javascript">
    $(document).ready(function () {
        //CrearDialogo(nombreDIV, alto, ancho, titulo);
        CrearDialogo('msjRegistroOK', 162, 360, '');
        CrearDialogo('msjErrorGrabar', 162, 360, '');
        CrearDialogo('msjErrorExiste', 162, 360, '');
    });

  </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="contentgeneral">
 <div class="titulo1">
        <div class="titulo2">
           <div class="ti1"><p>
               <asp:Label ID="lblTitulo" runat="server" 
                   Text="Tipo Envase"></asp:Label></p>
           </div>
           <div class="ti2"><p></p></div>
        </div>
    </div>

    <div class="conteradio1">
        <div class="bar1">
            <div class="line173">
                    <p><asp:Label ID="lblDescripcion" runat="server" Text="Descripción:" 
                           ></asp:Label></p>
            </div>
            <div class="line175">
                    <asp:TextBox ID="txtDescripcion" runat="server" CssClass="bordeimput" Width="700px" 
                        Height="20px" MaxLength="50" ></asp:TextBox>                    
                    <asp:RequiredFieldValidator ID="rfvDescripcion" runat="server" 
                    ErrorMessage="*" ControlToValidate="txtDescripcion" Font-Size="10pt" 
                        ForeColor="Red"></asp:RequiredFieldValidator>
             </div>
            
        </div>   
         <div class="bar2">
                 <div >
                    <asp:Label ID="lblCampoObligatorio" runat="server" Text="(*) Campos obligatorios" class="letraError"></asp:Label>
              </div>
            </div>     
        <div style="clear:both"></div>
    </div>

   <div class="bar4">
    <div class="bar4tres">
        <div class="btns2">
            <asp:Button ID="btnGrabar" runat="server"  CssClass="<%$ Resources:generales,imgGrabar %>" 
        onclick="btnGrabar_Click"  /></div>
        <div class="btns3">
            <asp:Button ID="btnCancelar" runat="server" CssClass="<%$ Resources:generales,imgCancelar %>" 
        onclick="btnCancelar_Click" CausesValidation="False" 
                 />                        
        </div>
        <div class="btns3"> 
            <asp:Button ID="btnLimpiar" runat="server" CssClass="<%$ Resources:generales,imgLimpiar %>"  
        onclick="btnLimpiar_Click" CausesValidation="False" 
                 /></div>
    </div>
   </div>

   <div class="_dialog" id="msjRegistroOK" style="display: none">
        <div class="contealert">
            <div class="boxtext">
                <div class="boxdentro">
                    <div class="boxletra">
                        <p>
                            <strong> <asp:Label ID="lblConfirmacion" runat="server" Text="El registro se Grabó satisfactoriamente"></asp:Label></strong></p>
                    </div>
                    <div class="boxbtn">
                        <div class="centerclose">
                            <asp:Button runat="server" ID="btnCerrar1" CssClass="<%$ Resources:generales,imgCerrar %>"
                            OnClientClick="CerrarDialogo('msjRegistroOK');" CausesValidation="False" 
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
                            <strong> <asp:Label ID="lblErrorGrabar" runat="server" Text="Error al grabar el registro."></asp:Label></strong></p>
                    </div>
                    <div class="boxbtn">
                        <div class="centerclose">
                            <asp:Button runat="server" ID="btnCerrar2" CssClass="<%$ Resources:generales,imgCerrar %>"
                                OnClientClick="CerrarDialogo('msjErrorGrabar');" 
                                meta:resourcekey="btnCerrar2Resource1" /></div>
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
                            <strong> <asp:Label ID="lblErrorExiste" runat="server" Text="Ya existe el registro ingresado"></asp:Label></strong></p>
                    </div>
                    <div class="boxbtn">
                        <div class="centerclose">
                            <asp:Button runat="server" ID="btncerrar3" CssClass="<%$ Resources:generales,imgCerrar %>"
                                OnClientClick="CerrarDialogo('msjErrorExiste');" 
                                meta:resourcekey="btncerrar3Resource1" /></div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
</asp:Content>
