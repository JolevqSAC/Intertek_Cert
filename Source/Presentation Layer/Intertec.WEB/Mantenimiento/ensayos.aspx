<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ensayos.aspx.cs" Inherits="Intertek.WEB.Mantenimiento.ensayos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            //CrearDialogo(nombreDIV, alto, ancho, titulo);
            CrearDialogo('msjSatisfactorio', 162, 360, '');
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="contentgeneral">
        <div class="titulo1">
            <div class="titulo2">
                <div class="ti1">
                    <p>
                        <asp:Label ID="lblTitulo" runat="server" meta:resourcekey="lblTituloResource1" Text="Ensayos"></asp:Label></p>
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
                        <asp:Label ID="lblEnsayo" runat="server" Text="Ensayo: "></asp:Label></p>
                </div>
                <div class="line175">
                    <asp:TextBox ID="txtEnsayo" CssClass="bordeimput" Width="500px" Height="20px" runat="server"
                        MaxLength="100" onkeypress="javascript:return validarChr(event)" ></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvEnsayo" runat="server" ErrorMessage="*" ControlToValidate="txtEnsayo"
                        CssClass="letraError"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="bar5option">
                <div class="line182">
                    <p>
                        <asp:Label ID="lblDescripcion" runat="server" Text="Descripción: "></asp:Label></p>
                </div>
                <div class="line93">
                    <asp:TextBox ID="txtDescripcion" CssClass="bordeimput" TextMode="MultiLine" Width="600px"
                        Height="76px" runat="server" MaxLength="400"></asp:TextBox>
                </div>
            </div>
            <div class="bar2">
                <div class="line173">
                    <p>
                        <asp:Label ID="lblLaboratorio" runat="server" Text="Laboratorio: "></asp:Label></p>
                </div>
                <div class="line175">
                    <asp:DropDownList runat="server" ID="ddlLaboratorio" CssClass="bordeimput" Width="300px">
                    </asp:DropDownList>
                    <asp:CompareValidator ID="cvLaboratorio" runat="server" CssClass="letraError" ErrorMessage="*"
                        ControlToValidate="ddlLaboratorio" ValueToCompare="0" Operator="NotEqual" Type="Integer"></asp:CompareValidator>
                </div>
            </div>
             <div class="bar2">
                 <div >
                    <asp:Label ID="lblCampoObligatorio" runat="server" Text="(*) Campos obligatorios" class="letraError"></asp:Label>
              </div>
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
                        OnClientClick="javascript:DesactivarValidacion();" /></div>
                <div class="btns3">
                    <asp:Button ID="btnLimpiar" runat="server" Text="" CssClass="<%$ Resources:generales,imgLimpiar %>"
                        meta:resourcekey="btnLimpiarResource1" CausesValidation="False" OnClick="btnLimpiar_Click"
                        OnClientClick="javascript:DesactivarValidacion();" /></div>
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
</asp:Content>