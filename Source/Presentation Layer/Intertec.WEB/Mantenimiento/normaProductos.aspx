<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="normaProductos.aspx.cs" Inherits="Intertek.WEB.Mantenimiento.normaProductos" %>
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
                        <asp:Label ID="lblTitulo" runat="server" meta:resourcekey="lblTituloResource1" Text="Métodos"></asp:Label></p>
                </div>
                <div class="ti2">
                    <p>
                    </p>
                </div>
            </div>
        </div>
        <div class="conteradio1">
        <div class="bar1">
            <div class="line173"><p><asp:Label ID="lblProducto" runat="server" Text="Producto:" 
        ></asp:Label></p></div>
            <div class="line175"><asp:DropDownList runat="server" ID="ddlProducto" CssClass="bordeimput" 
                    Width="300px"></asp:DropDownList>
                    <asp:CompareValidator ID="cvProducto" runat="server" CssClass="letraError" ErrorMessage="*" ControlToValidate="ddlProducto" ValueToCompare="0" Operator="NotEqual" Type="Integer" ></asp:CompareValidator>
                    </div>
        </div>

              <div class="bar2">
                    <div class="line173">
                <p>
                    <asp:Label ID="lblNombre" runat="server" Text="Nombre:"></asp:Label>
                </p>
            </div>
            <div class="line175">
                <asp:TextBox ID="txtNombre" runat="server" CssClass="bordeimput" MaxLength="100"
                    Width="300px"></asp:TextBox>
                     <asp:RequiredFieldValidator ID="rfvNombre" runat="server" ErrorMessage="*" ControlToValidate="txtNombre"
                        CssClass="letraError"></asp:RequiredFieldValidator>
                    </div>
            </div>
         <div class="bar2">
                    <div class="line173">
                <p>
                    <asp:Label ID="lblAcreditador" runat="server" Text="Acreditador:"></asp:Label>
                </p>
            </div>
            <div class="line175">
                <asp:TextBox ID="txtAcreditador" runat="server" CssClass="bordeimput" MaxLength="150"
                    Width="300px"></asp:TextBox>
                      <asp:RequiredFieldValidator ID="rfvAcreditador" runat="server" ErrorMessage="*" ControlToValidate="txtAcreditador"
                        CssClass="letraError"></asp:RequiredFieldValidator>
                    </div>
            </div>
            

            <div class="bar2">
                <div class="line173">
                    <p>
                        <asp:Label ID="lblAnio" runat="server" Text="Año: "></asp:Label></p>
                </div>
                <div class="line175">
                   <asp:TextBox ID="txtAnio" CssClass="bordeimput" Width="80px"
                        Height="20px" runat="server" MaxLength="4"></asp:TextBox>
                          <asp:RequiredFieldValidator ID="rfvAnio" runat="server" ErrorMessage="*" ControlToValidate="txtAnio"
                        CssClass="letraError"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="rexAnio" runat="server" ErrorMessage="*" 
                        ControlToValidate="txtAnio" ValidationExpression="\d{4}" CssClass="letraError"></asp:RegularExpressionValidator>
                </div>
            </div>

                <div class="bar5option">
        <div class="line182"><p><asp:Label ID="lblObservaciones" runat="server" Text="Observaciones: "></asp:Label></p></div>
        <div class="line93"><asp:TextBox ID="txtObservaciones" MaxLength="200" TextMode="MultiLine" CssClass="bordeimput" Width="639px" Height="76px" runat="server"></asp:TextBox></div>
    </div>
            <div class="bar2">
                 <div >
                    <asp:Label ID="lblCampoObligatorio" runat="server" Text="(*) Campos obligatorios" class="letraError"></asp:Label>
              </div>
            </div>

            <div style="clear: both">
            </div>
        </div>

        <div class="conteradio1">
          <div class="bar1">
                <div class="line173">
                    <p>
                        <asp:Label ID="lblLaboratorio" runat="server" Text="Laboratorio:"></asp:Label>
                    </p>
                </div>
                <div class="line58">
                    <asp:DropDownList runat="server" ID="ddlLaboratorio" CssClass="bordeimput" 
                        Width="250px" AutoPostBack="True" 
                        onselectedindexchanged="ddlLaboratorio_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
                <div class="line173">
                    <p>
                        <asp:Label ID="lblEnsayo" runat="server" Text="Ensayo:"></asp:Label>
                    </p>
                </div>
                <div class="line176">
                    <asp:DropDownList runat="server" ID="ddlEnsayo" CssClass="bordeimput" Width="250px">
                    </asp:DropDownList>
                </div>
                
            </div>
            <div class="bar2">
                <div class="line173">
                    <p>
                        <asp:Label ID="lblMetodo" runat="server" Text="Método:"></asp:Label>
                    </p>
                </div>
                <div class="line58">
                    <asp:DropDownList runat="server" ID="ddlMetodo" CssClass="bordeimput" Width="250px">
                    </asp:DropDownList>
                </div>
                <div class="line173">
                    <p>
                        <asp:Label ID="lblForma" runat="server" Text="Forma:"></asp:Label>
                    </p>
                </div>
                <div class="line176">
                     <asp:TextBox ID="txtForma" runat="server" CssClass="bordeimput" MaxLength="18"
                        Width="150px"></asp:TextBox>
                </div>
            </div>
            
            <div class="bar2">
                <div class="line173">
                    <p>
                        <asp:Label ID="lblValorMinimo" runat="server" Text="Valor Mínimo:"></asp:Label>
                    </p>
                </div>
                <div class="line58">
                    <asp:TextBox ID="txtValorMinimo" runat="server" CssClass="bordeimput" MaxLength="18"
                        Width="150px"></asp:TextBox>
                </div>
                <div class="line173">
                    <p>
                        <asp:Label ID="lblValorMaximo" runat="server" Text="Valor Máximo:"></asp:Label>
                    </p>
                </div>
                <div class="line176">
                     <asp:TextBox ID="txtValorMaximo" runat="server" CssClass="bordeimput" MaxLength="18"
                        Width="150px"></asp:TextBox>
                </div>
            </div>

            <div class="bar2">
               <div class="bar4tres">
                <div class="btns2">
                    <asp:Button ID="btnAgregarNormaLimite" runat="server" Text="" 
                        CssClass="<%$ Resources:generales,imgAgregar %>" onclick="btnAgregarNormaLimite_Click" CausesValidation="false"
                          />
                             <asp:Button runat="server" ID="btnEliminar" ClientIDMode="Static" CssClass="ocultarBoton"
                        OnClick="btnEliminar_Click" CausesValidation="false" />  

                          </div>
                
            </div>
               
            </div>

              <div class="bar5">
                    <center>
                        <asp:GridView ID="gvBuscar" runat="server" CssClass="gridview" AutoGenerateColumns="False"
                            AllowPaging="True" Width="800px" OnPageIndexChanging="gvBuscar_PageIndexChanging"
                            PageSize="10">
                            <Columns>
                                <asp:BoundField DataField="ENS_Nombre" HeaderText="Ensayo" />
                                <asp:BoundField DataField="MET_Nombre" HeaderText="Método" />
                                <asp:BoundField DataField="NOL_Minimo" HeaderText="Mínimo" ItemStyle-Width="90"/>
                                <asp:BoundField DataField="NOL_Maximo" HeaderText="Máximo" ItemStyle-Width="90" />
                                <asp:BoundField DataField="NOL_Forma" HeaderText="Forma" ItemStyle-Width="120"/>
                                <asp:TemplateField HeaderText="Opciones" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle Width="80px" />
                                    <ItemTemplate>
                                       
                                        <img onclick="javascript:MostrarMensajeEliminar('msjError','<%# Eval("IDNormaLimite") %>');"
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
            <div style="clear: both">
            </div>
        </div>

        <div class="bar4">
            <div class="bar4tres">
                <div class="btns2">
                    <asp:Button ID="btnGrabar" runat="server" Text="" CssClass="<%$ Resources:generales,imgGrabar %>"
                        meta:resourcekey="btnGrabarResource1" OnClick="btnGrabar_Click" /></div>
                <div class="btns3">
                    <asp:Button ID="btnCancelar" runat="server" Text="" CssClass="<%$ Resources:generales,imgCancelar %>"
                        meta:resourcekey="btnCancelarResource1" CausesValidation="False" OnClick="btnCancelar_Click"
                         /></div>
                <div class="btns3">
                    <asp:Button ID="btnLimpiar" runat="server" Text="" CssClass="<%$ Resources:generales,imgLimpiar %>"
                        meta:resourcekey="btnLimpiarResource1" CausesValidation="False" OnClick="btnLimpiar_Click"
                         />
                      
                  </div>
            </div>
        </div>
        <div class="bar4">
            <asp:Label ID="lblMensaje" runat="server" Text="" CssClass="letraError"></asp:Label>
        </div>
    </div>
    <div id="msjSatisfactorio" style="display: none">
        <div class="contealert">
            <div class="boxtext">
                <div class="boxdentro">
                    <div class="boxletra">
                        <p>
                            <strong>
                                <asp:Label ID="lblConfirmacion" runat="server" Text="El registro se Grabo satisfactoriamente"></asp:Label></strong></p>
                    </div>
                    <div class="boxbtn">
                        <div class="centerclose">
                            <asp:Button ID="btnCerrarConfirmacion" OnClientClick="javascript:CerrarDialogo('msjSatisfactorio');"
                                runat="server" Text="" CssClass="<%$ Resources:generales,imgCerrar %>" CausesValidation="false" />
                        </div>
                    </div>
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
</asp:Content>
