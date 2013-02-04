<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="lugarMuestreo.aspx.cs" Inherits="Intertek.WEB.Mantenimiento.lugarMuestreo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
    $(document).ready(function () {
        //CrearDialogo(nombreDIV, alto, ancho, titulo);
        CrearDialogo('msjRegistroOK', 162, 360, '');
        CrearDialogo('msjErrorGrabar', 162, 360, '');
        CrearDialogo('msjErrorExiste', 162, 360, '');
    });

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
                   Text="Lugar de Muestreo"></asp:Label></p>
           </div>
           <div class="ti2"><p></p></div>
        </div>
    </div>

    <div class="conteradio1">
        <div class="bar1">
            <div class="line173">
                    <p><asp:Label ID="lblCliente" runat="server" 
                    Text="Cliente:" ></asp:Label></p>
            </div>
            <div class="line175">
                    <asp:DropDownList ID="ddlCliente" runat="server" CssClass="bordeimput" Width="700px">
                    </asp:DropDownList>                    
                    <asp:CompareValidator ID="cvCliente" runat="server" CssClass="letraError" ErrorMessage="*"
                        ControlToValidate="ddlCliente" ValueToCompare="0" Operator="NotEqual" Type="Integer"></asp:CompareValidator>                   
            </div>
            
        </div>    
         <div class="bar2">
            <div class="line173">
                <p>
                    <asp:Label ID="lblDireccion" runat="server" Text="Dirección:"></asp:Label>
                </p>
            </div>
            <div class="line175">
                <asp:TextBox ID="txtDireccion" runat="server" 
                    CssClass="bordeimput" Width="700px" Height="20px" MaxLength="150"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvDireccion" runat="server" 
                        ErrorMessage="*" ControlToValidate="txtDireccion" CssClass="letraError"></asp:RequiredFieldValidator>
            </div>
        </div>
        
        <div class="bar2">
            <div class="line173"><p><asp:Label ID="lblTelefono" runat="server" 
                    Text="Teléfono:" ></asp:Label></p></div>
            <div class="line175">
                <div style="width:170px;float:left"><asp:TextBox ID="txtTelefono" runat="server" 
                    CssClass="bordeimput" Width="150px" Height="20px" MaxLength="20" onkeypress="javascript:return validarNro(event)"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvTelefono" runat="server" 
                        ErrorMessage="*" ControlToValidate="txtTelefono" CssClass="letraError"></asp:RequiredFieldValidator>
                    <%--<asp:RegularExpressionValidator ID="rxvTelefono" runat="server" CssClass="letraError" ControlToValidate="txtTelefono" 
                    ErrorMessage="Sólo Dígitos" Font-Size="8" ValidationExpression="\d+"></asp:RegularExpressionValidator> --%>   
                    </div>
                    
                                     
                <div style="width:100px;float:left" class="line173"> <p>
                    <asp:Label ID="lblContacto" runat="server" Text="Contacto:"></asp:Label></p></div>
                     <div style="width:460px;float:left">
                         <asp:TextBox ID="txtContacto" runat="server" 
                             CssClass="bordeimput" MaxLength="150" Width="430px"  Height="20px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvContacto" runat="server" 
                        ErrorMessage="*" ControlToValidate="txtContacto" CssClass="letraError" ></asp:RequiredFieldValidator> 
                      <asp:RegularExpressionValidator ID="rxvContacto" runat="server" CssClass="letraError" ControlToValidate="txtContacto" 
                    ErrorMessage="Sólo letras" Font-Size="8" ValidationExpression="^[A-Z a-zñÑáéíóúÁÉÍÓÚ]*$"></asp:RegularExpressionValidator>
                       
                        <%-- <asp:FilteredTextBoxExtender ID="ftecontacto" runat="server" TargetControlID="txtContacto"
                                    Enabled="True" ValidChars="ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyzüÜáéíóúÁÉÍÓ">
                         </asp:FilteredTextBoxExtender>--%>
                       </div>   
                                                    
            
            </div>
        </div>

        <div class="bar5option">
            <div class="line182">
                 <p>
                    <asp:Label ID="lblObservaciones" runat="server" Text="Observaciones: "></asp:Label>
                 </p>
            </div>
            <div class="line93">
                <asp:TextBox ID="txtObservaciones" MaxLength="200" TextMode="MultiLine" CssClass="bordeimput" Width="639px" Height="76px" runat="server"></asp:TextBox>
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
