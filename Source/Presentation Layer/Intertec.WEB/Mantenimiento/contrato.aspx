<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="contrato.aspx.cs" Inherits="Intertek.WEB.Mantenimiento.contrato" meta:resourcekey="PageResource1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="../Scripts/ui.datepicker-es.js" type="text/javascript"></script>
<script type="text/javascript">
    $(document).ready(function () {
        //CrearDialogo(nombreDIV, alto, ancho, titulo);
        CrearDialogo('msjRegistroOK', 162, 360, '');
        CrearDialogo('msjErrorGrabar', 162, 360, '');
        CrearDialogo('msjErrorExiste', 162, 360, '');
        CrearDialogoEliminar('msjFechasIncorrectas', 162, 360, '');
        CrearDialogoEliminar('msjFechasValidar', 162, 360, '');
        CrearDialogo('msjAlertaCaracteres', 162, 360, '');
        CrearDialogo('msjCamposObligatorios', 162, 360, '');

        if ('<%=Session["ddlIdiomas"] %>' == "es-PE") {
            $.datepicker.setDefaults($.datepicker.regional["es"]);
        }
        else {
            $.datepicker.setDefaults($.datepicker.regional[""]);
        }

        $('#<%= txtInicio.ClientID %>').datepicker(
                                    { changeMonth: true,
                                        changeYear: true,
                                        onClose: function (selectedDate) {
                                            $('#<%= txtFin.ClientID %>').datepicker("option", "minDate", selectedDate);
                                        }
                                    });

        $('#<%= txtFin.ClientID %>').datepicker(
                            { changeMonth: true,
                                changeYear: true,
                                onClose: function (selectedDate) {
                                    $('#<%= txtInicio.ClientID %>').datepicker("option", "maxDate", selectedDate);
                                }
                            });
    });

    function compareDates() {
        var d1 = "";
        var d2 = "";
        var idioma = '<%=Session["ddlIdiomas"] %>';
        var numero = document.getElementById('<%=txtNumero.ClientID %>').value;
        var montomax = document.getElementById('<%=txtMontoMaximo.ClientID %>').value;
        var listcliente = document.getElementById('<%=ddlCliente.ClientID %>').value;

        if (idioma != "") {

            if (idioma == "es-PE") {
                d1 = getDateFromFormat($('#<%= txtInicio.ClientID %>').val(), "dd/MM/yyyy");
                d2 = getDateFromFormat($('#<%= txtFin.ClientID %>').val(), "dd/MM/yyyy");
            } else {
                d1 = getDateFromFormat($('#<%= txtInicio.ClientID %>').val(), "MM/dd/yyyy");
                d2 = getDateFromFormat($('#<%= txtFin.ClientID %>').val(), "MM/dd/yyyy");
            }

            if (d1 == 0 || d2 == 0 || vacio(numero) || vacio(montomax) || listcliente==0) {
                MostrarMensaje('msjCamposObligatorios');
                return false;
            }
            else if (d1 > d2) {
                $("#spanFechaInicio1").text($('#<%= txtInicio.ClientID %>').val());
                $("#spanFechaFin1").text($('#<%= txtFin.ClientID %>').val());
                MostrarMensaje('msjFechasValidar');
                return false;
            }
            else {
                $('#btnGrabar').trigger("click");
                return true; 
            }
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
                   Text="Registrar Contrato" meta:resourcekey="lblTituloResource1"></asp:Label></p>
           </div>
           <div class="ti2"><p></p></div>
        </div>
    </div>

    <div class="conteradio1">
        
        <div class="bar1">
            <div class="line173"><p>
                 <asp:Label ID="lbltipo" runat="server" 
                 Text="Estado:" meta:resourcekey="lbltipoResource1" ></asp:Label> </p></div>
             <div class="line25">
                 <asp:TextBox ID="txtestado" runat="server" CssClass="bordeimput" Width="100px" Text="Vigente"
                        Height="20px" meta:resourcekey="txtestadoResource1" MaxLength="50" Enabled="false"></asp:TextBox>
             </div>
            
           </div>

        <div class="bar5option">
            <div class="line182">
                 <p>
                    <asp:Label ID="lblDescripcion" runat="server" Text="Descripcion: " 
                         meta:resourcekey="lblDescripcionResource1"></asp:Label>
                 </p>
            </div>
            <div class="line93">
                <asp:TextBox ID="txtDescripcion" MaxLength="200" 
                TextMode="MultiLine" CssClass="bordeimput" 
                Width="700px" Height="76px" runat="server" 
                    onKeyUp="javascript:Count(this,200);" onChange="javascript:Count(this,200);" 
                    meta:resourcekey="txtDescripcionResource1" ></asp:TextBox>
            </div>
        </div>
            
       <div class="bar2">
            <div class="line173"><p>
                <asp:Label ID="lblInicio" runat="server" 
                    Text="Fecha Inicio:" meta:resourcekey="lblInicioResource1" ></asp:Label></p></div>
            <div class="line25">
                <asp:TextBox ID="txtInicio" runat="server" CssClass="bordeimput" Width="236px" 
                    Height="20px" MaxLength="20"  meta:resourcekey="txtInicioResource1"></asp:TextBox>                                                   
            </div>
            <div class="line8"><p><asp:Label ID="Label3" runat="server" Text="*" class="letraError"></asp:Label></p></div>
            
            <div class="line180"> 
                <p>
                    <asp:Label ID="lblFin" runat="server" Text="Fecha Fin:" 
                        meta:resourcekey="lblFinResource1"></asp:Label>
                </p>
            </div>
                <div class="line25">
                   <asp:TextBox ID="txtFin" runat="server" ClientIDMode="Static" 
                    CssClass="bordeimput" MaxLength="20" Width="236px" Height="20px" 
                        meta:resourcekey="txtFinResource1"></asp:TextBox>                   
            </div>
            <div class="line8"><p><asp:Label ID="Label1" runat="server" Text="*" class="letraError"></asp:Label></p></div>
        </div>

        

        <div class="bar2">
            <div class="line173"><p>
                 <asp:Label ID="lblNumeroReferencia" runat="server" 
                 Text="Número de Referencia:" meta:resourcekey="lblNumeroReferenciaResource1" ></asp:Label> </p></div>
             <div class="line25">
                 <asp:TextBox ID="txtNumero" runat="server" CssClass="bordeimput" Width="236px" 
                        Height="20px" meta:resourcekey="txtNumeroResource1" MaxLength="15"></asp:TextBox>
             </div>
             <div class="line8"><p><asp:Label ID="Label2" runat="server" Text="*" class="letraError"></asp:Label></p></div>
           </div>
         <div class="bar2">
            <div class="line173"><p>
                 <asp:Label ID="lblMontoMaximo" runat="server" 
                 Text="Monto Máximo de Contrato:" meta:resourcekey="lblMontoMaximoResource1" ></asp:Label> </p></div>
             <div class="line25">
                 <asp:TextBox ID="txtMontoMaximo" runat="server" CssClass="bordeimput" Width="236px" 
                        Height="20px" meta:resourcekey="txtMontoMaximoResource1" MaxLength="15" 
                        onKeypress="javascript:return jsEsNumeroDecimal(event);" 
                      onChange="javascript:return formatearNumeroWithTope(this,2,1000000000000);"
                        ></asp:TextBox>
             </div>
             <div class="line8"><p><asp:Label ID="Label6" runat="server" Text="*" class="letraError"></asp:Label></p></div>
           </div>

        <div class="bar2">
            <div class="line173">
                    <p><asp:Label ID="lblCliente" runat="server" 
                    Text="Cliente:" meta:resourcekey="lblClienteResource1" ></asp:Label></p>
            </div>
            <div class="line172">
                    <asp:DropDownList ID="ddlCliente" runat="server" CssClass="bordeimput" 
                                      Width="700px" meta:resourcekey="ddlClienteResource1" 
                        Height="20px">
                    </asp:DropDownList>  
            </div>
            <div class="line8"><p><asp:Label ID="Label4" runat="server" Text="*" class="letraError"></asp:Label></p></div>
        </div>  
          
        <div style="clear:both">           
        </div>
    </div>


   <div class="bar4">
    <div class="bar4tres">
        <div class="btns2">
            <asp:Button ID="btnGrabar" runat="server" CssClass="ocultarBoton" ClientIDMode="Static"
        onclick="btnGrabar_Click" meta:resourcekey="btnGrabarResource1"  />
        <img id="imgGrabar" runat="server" src="../img/btngrabar2.png"
             onclick="javascript:compareDates();"  alt=""
             class="imgCursor" />
        </div>

        <div class="btns3">
            <asp:Button ID="btnCancelar" runat="server" CssClass="<%$ Resources:generales,imgCancelar %>" 
        onclick="btnCancelar_Click" CausesValidation="False" meta:resourcekey="btnCancelarResource1" 
                 />                        
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
                            OnClientClick="CerrarDialogoC('msjRegistroOK','contratoBuscar.aspx');" CausesValidation="False" 
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
                            <strong> <asp:Label ID="lblErrorGrabar" runat="server" 
                                Text="Error al grabar el registro." meta:resourcekey="lblErrorGrabarResource1"></asp:Label></strong></p>
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
                            <strong> <asp:Label ID="lblErrorExiste" runat="server" 
                                Text="Ya existe el registro ingresado" 
                                meta:resourcekey="lblErrorExisteResource1"></asp:Label></strong></p>
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

     <div class="_dialog" id="msjFechasIncorrectas" style="display: none" title="Error">
        <div class="contealertError">
            <div class="boxtext">
                <div class="boxdentro">
                    <div class="boxletra2">
                        <p>
                             Fechas Incorrectas: 
                            <strong><span id="spanFechaInicio"></span></strong>
                            o
                            <strong><span id="spanFechaFin"></span></strong>
                            </p>
                    </div>
                    <div class="boxbtn">
                        <div class="centerclose">
                            <asp:Button runat="server" ID="btncerrarfecha" CssClass="<%$ Resources:generales,imgCerrar %>"
                                OnClientClick="CerrarDialogo('msjFechasIncorrectas');" 
                                meta:resourcekey="btncerrar3Resource1" /></div>
                    </div>
                </div>
            </div>
        </div>
    </div>
       <div class="_dialog" id="msjFechasValidar" style="display: none" title="Error">
        <div class="contealertError">
            <div class="boxtext">
                <div class="boxdentro">
                    <div class="boxletra2">
                        <p>
                            Fecha Inicio: 
                            <strong> <span id="spanFechaInicio1"></span></strong>
                             debe ser menor a Fecha Fin: 
                            <strong><span id="spanFechaFin1"></span></strong>
                        </p>
                    </div>
                    <div class="boxbtn">
                        <div class="centerclose">
                            <asp:Button runat="server" ID="btnfechavalidar" CssClass="<%$ Resources:generales,imgCerrar %>"
                                OnClientClick="CerrarDialogo('msjFechasValidar');" 
                                meta:resourcekey="btncerrar3Resource1" /></div>
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
                            <strong><asp:Label ID="Label9" runat="server" 
                                Text="Debe ingresar los campos obligatorios marcados con asterisco(*)" 
                                meta:resourcekey="lblCampoObligatorioResource1"></asp:Label></strong>
                         </p>
                    </div>
                    <div class="boxbtn">
                        <div class="centerclose">
                            <asp:Button runat="server" ID="btnCerrarObligatorios" CssClass="<%$ Resources:generales,imgCerrar %>"
                                OnClientClick="CerrarDialogo('msjCamposObligatorios');" 
                                CausesValidation="False" meta:resourcekey="btncerrar3Resource1"/></div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</div>
</asp:Content>
