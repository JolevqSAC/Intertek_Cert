<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="neoensayos.aspx.cs" Inherits="Intertec.WEB.Mantenimiento.neoensayos" meta:resourcekey="PageResource1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../Styles/tabs.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            //CrearDialogo(nombreDIV, alto, ancho, titulo);
            CrearDialogo('msjSatisfactorio', 162, 360, '');
            CrearDialogoEliminar('msjError', 200, 360, '');
            CrearDialogoEliminar('msjError2', 200, 360, '');
            CrearDialogo('msjEliminarSeleccionados', 162, 360, '');

            $("#tabs").tabs();

            jQuery('#' + '<%=btnEliminar2.ClientID%>').live('click', function (event) {
                event.preventDefault();

                VerificarSeleccionados();

            });
        });
        function EliminarSi2() {          
            CerrarDialogo('msjError2');        
            jQuery('#' + '<%=btnQuitarSubEnsayo.ClientID%>').click();
        }

        function CerrarDialogoC(nombreDIV) {
            $('#' + nombreDIV).dialog("close");
            $(location).attr('href', 'ensayosBuscar.aspx');
        }



        function VerificarSeleccionados() {
            var existenSeleccionados = false;
            $('#<%= grvwSubEnsayo.ClientID %> tr').each(function (indexF) {
                $(this).find('td').each(function (indexC) {
                    if (indexC == 2) {
                        var chk = $(this).find('input')[0];
                        if (chk != null) {
                            if (chk.checked == true) {
                                existenSeleccionados = true;
                            }
                        }
                    }
                });
            });

            if (existenSeleccionados == true) {
                MostrarMensajeEliminar('msjError2', '');
            } else {
                //  alert("Debe seleccionar al menos un Cliente para eliminar.");
                MostrarMensaje('msjEliminarSeleccionados');
            }
        }

        function validate(event) {
            var rspt = true
            var rNombre = document.getElementById('<%= txtNombre.ClientID %>');
            if (rNombre.value == "") {
                rNombre.setAttribute('style', 'width: 100px; height: 20px;border-color:#FF0000');
                rspt = false;
            }
            else {
                rNombre.setAttribute('style', '');
                rspt = true;
            }

        }

    </script>
     <style type="text/css">
     .formato
     {
      font-weight: bold;
     	 
    }
     
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="contentgeneral">
        <div class="titulo1">
            <div class="titulo2">
                <div class="ti1">
                    <p>
                        <asp:Label ID="lblTitulo" runat="server" meta:resourcekey="lblTituloResource1" Text="Ensayo"></asp:Label></p>
                </div>
                <div class="ti2">
                    <p>
                    </p>
                </div>
            </div>
        </div>
       
        <div id="tabs">
            <ul>
            
                <li><a href="#tabs-1"><asp:Label ID="lblDato" runat="server" Text="Datos" meta:resourcekey="lblDatoResource1"></asp:Label></a></li>
                <li><a href="#tabs-2"><asp:Label ID="lblDetalle" runat="server" Text="Sub Ensayo" meta:resourcekey="lblDetalleResource1"></asp:Label></a></li>
            </ul>
            <div id="tabs-1">
                <div class="bar1">
                    <div class="line173">
                        <p>
                            <asp:Label ID="lblNombre" runat="server" Text="Nombre: " 
                                meta:resourcekey="lblNombreResource1"></asp:Label></p>
                    </div>
                    <div class="line175">
                        <asp:TextBox ID="txtNombre" CssClass="bordeimput" Width="500px" Height="20px" runat="server"
                            MaxLength="100" meta:resourcekey="txtNombreResource1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvNombre" runat="server" ErrorMessage="*"  ControlToValidate="txtNombre"
                            CssClass="letraError" meta:resourcekey="rfvNombreResource1"></asp:RequiredFieldValidator>
                        <asp:HiddenField ID="hdflIDEnsayo" runat="server" />
                    </div>
                </div>
                <div class="bar2">
                    <div class="line173">
                        <p>
                            <asp:Label ID="Label1" runat="server" Text="Nombre Ingles: " 
                                meta:resourcekey="Label1Resource1"></asp:Label></p>
                    </div>
                    <div class="line175">
                        <asp:TextBox ID="txtNombreIngles" CssClass="bordeimput" Width="500px" Height="20px"
                            runat="server" MaxLength="100" meta:resourcekey="txtNombreInglesResource1"></asp:TextBox>
                    </div>
                </div>
                <div class="bar5option">
                    <div class="line182">
                        <p>
                            <asp:Label ID="lblDescripcion" runat="server" Text="Descripción: " 
                                meta:resourcekey="lblDescripcionResource1"></asp:Label></p>
                    </div>
                    <div class="line93">
                        <asp:TextBox ID="txtDescripcion" CssClass="bordeimput" TextMode="MultiLine" Width="600px"
                            Height="76px" runat="server" MaxLength="400" 
                            meta:resourcekey="txtDescripcionResource1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvDescripcion" runat="server" ErrorMessage="*" 
                            ControlToValidate="txtDescripcion"
                            CssClass="letraError" meta:resourcekey="rfvDescripcionResource1"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="bar2">
                    <div>
                        <asp:Label ID="lblCampoObligatorio" runat="server" Text="(*) Campos obligatorios"
                            class="letraError" meta:resourcekey="lblCampoObligatorioResource1"></asp:Label>
                    </div>
                </div>
                <div style="clear: both">
                </div>
            </div>
            <div id="tabs-2">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div class="bar1">
                            <div class="line173">
                                <p>
                                    <asp:Label ID="Label2" runat="server" Text="Nombre: " 
                                        meta:resourcekey="Label2Resource1"></asp:Label>
                                    <asp:HiddenField runat="server" ClientIDMode="Static" ID="hdflIdSubEnsayo" /></p>
                            </div>
                            <div class="line175">
                                    <asp:TextBox ID="txtNombreSubensayo" CssClass="bordeimput" Width="500px" 
                                    Height="20px" runat="server"
                                    MaxLength="100" meta:resourcekey="txtNombreSubensayoResource1"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvNombre2" runat="server" ErrorMessage="*" ControlToValidate="txtNombreSubensayo"
                            CssClass="letraError" ValidationGroup="validarCliente3" 
                                    meta:resourcekey="rfvNombre2Resource1"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="bar2">
                            <div class="line173">
                                <p>
                                    <asp:Label ID="Label3" runat="server" Text="Nombre Ingles: " 
                                        meta:resourcekey="Label3Resource1"></asp:Label></p>
                            </div>
                            <div class="line175">
                                <asp:TextBox ID="txtNombreInglesSubensayo" CssClass="bordeimput" Width="500px" 
                                    Height="20px" runat="server"
                                    MaxLength="100" meta:resourcekey="txtNombreInglesSubensayoResource1"></asp:TextBox>
                            </div>
                        </div>
                        <div class="bar2">
                            <div class="line173">
                                <p>
                                    <asp:Label ID="Label7" runat="server" meta:resourcekey="Label7Resource1"></asp:Label></p>
                            </div>
                            <div class="line179">
                                <div class="btns2" id="divbtnAgregarSubEnsayo" runat="server">
                                    <asp:Button ID="btnAgregarSubEnsayo" runat="server" UseSubmitBehavior="False"  
                                        ValidationGroup="validarCliente3" CssClass="<%$ Resources:generales,imgAgregar %>"
                                        meta:resourcekey="btnAgregarSubEnsayoResource1" 
                                        OnClick="btnAgregarSubEnsayo_Click" />
                                </div>

                                  
                                <div class="btns3" id="divbtnQuitarSubEnsayo" runat="server">
                                  <input type="button" runat="server" id="btnEliminar2" value="Quitar" class="btnquitar" />
                                    <asp:Button ID="btnQuitarSubEnsayo" runat="server" CssClass="ocultarBoton"  meta:resourcekey="btnQuitarSubEnsayoResource1"
                                        CausesValidation="False" UseSubmitBehavior="false"  OnClick="btnQuitarSubEnsayo_Click" />
                                </div>
                                <div class="btns2" id="divbntActualizarSubEnsayo" runat="server" visible="False">
                                    <asp:Button ID="bntActualizarSubEnsayo" runat="server" CssClass="<%$ Resources:generales,imgGrabar %>"
                                        meta:resourcekey="btnGrabar"   ValidationGroup="validarCliente3"
                                        OnClick="bntActualizarSubEnsayo_Click" />
                                </div>
                                <div class="btns3" id="divbntCancelarSubEnsayo" runat="server" visible="False">
                                    <asp:Button ID="bntCancelarSubEnsayo" runat="server" CssClass="<%$ Resources:generales,imgCancelar %>"
                                        meta:resourcekey="bntCancelarSubEnsayoResource1" CausesValidation="False" 
                                        OnClick="bntCancelarSubEnsayo_Click" />
                                </div>
                            </div>
                        </div>
                        <div class="bar11">
                            <div class="line225">
                                <asp:GridView ID="grvwSubEnsayo" runat="server" CssClass="gridview" AutoGenerateColumns="False"
                                    AllowPaging="True" Width="600px" OnPageIndexChanging="grvwSubEnsayo_PageIndexChanging"
                                    PageSize="5" meta:resourcekey="grvwSubEnsayoResource1" 
                                    onrowcommand="grvwSubEnsayo_RowCommand">
                                    <Columns>
                                        <asp:BoundField DataField="SEN_Nombre" HeaderText="Nombre" 
                                            meta:resourcekey="BoundFieldResource1" />
                                        <asp:BoundField DataField="SEN_NombreIngles" HeaderText="NombreIngles" 
                                            meta:resourcekey="BoundFieldResource2" />
                                        <asp:TemplateField HeaderText="Selec." 
                                            meta:resourcekey="TemplateFieldResource1">
                                            <ItemTemplate>
                                                <center>
                                                    <asp:CheckBox ID="chkSeleccionSubEnsayo" runat="server" 
                                                        meta:resourcekey="chkSeleccionSubEnsayoResource1" />
                                                    <asp:HiddenField ID="hidIdSubEnsayo" runat="server" Value='<%# Eval("IDSubEnsayo") %>' />
                                                </center>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Modificar" 
                                            meta:resourcekey="TemplateFieldResource2">
                                            <ItemTemplate>
                                                <center>
                                                   <%-- <asp:Button ID="btnModificarSubEnsayo" runat="server" CssClass="btnmodificar"
                                                        meta:resourcekey="btnModificarSubEnsayoResource1" CausesValidation="False" 
                                                        OnClick="btnModificarSubEnsayo_Click" />--%>
                                                          <asp:LinkButton ID="lkbEditar" ToolTip="Modificar" runat="server" CausesValidation="False"
                                                        CommandName="Editar"  CommandArgument='<%# Eval("IDSubEnsayo") %>' meta:resourcekey="lkbEditarResource1">
                                                    <img src="/img/btnmdificar.png" alt="Modificar" style="border:0" /></asp:LinkButton>
                                                </center>
                                            </ItemTemplate>
                                            <ItemStyle BorderStyle="None" Width="90px" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                              <div class="bar2">
                    <div>
                        <asp:Label ID="Label4" runat="server" Text="(*) Campos obligatorios"
                            class="letraError" meta:resourcekey="Label4Resource1"></asp:Label>
                    </div>
                </div>
                            <div style="clear: both">
                            </div>
                        </div>
                        <div style="clear: both">
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
       <%-- <div class="conteradio1">
        </div>--%>
        <div class="bar4">
            <div class="bar4tres">
                <div class="btns2">
                    <asp:Button ID="btnGrabar" runat="server" CssClass="<%$ Resources:generales,imgGrabar %>"
                        meta:resourcekey="btnGrabarResource1" OnClick="btnGrabar_Click" /></div>
                <div class="btns3">
                    <asp:Button ID="btnCancelar" runat="server" CssClass="<%$ Resources:generales,imgCancelar %>"
                        meta:resourcekey="btnCancelarResource1" CausesValidation="False" OnClick="btnCancelar_Click"
                        OnClientClick="javascript:DesactivarValidacion();" /></div>
                <div class="btns3">
                    <asp:Button ID="btnLimpiar" runat="server" CssClass="<%$ Resources:generales,imgLimpiar %>"
                        meta:resourcekey="btnLimpiarResource1" CausesValidation="False" OnClick="btnLimpiar_Click"
                        OnClientClick="javascript:DesactivarValidacion();" /></div>

                       
            </div>
        </div>
        <div class="bar4">
            <asp:Label ID="lblMensaje" runat="server" CssClass="letraError" 
                meta:resourcekey="lblMensajeResource1"></asp:Label>
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
                                Text="El registro se Grabo satisfactoriamente" 
                                meta:resourcekey="lblConfirmacionResource1"></asp:Label></strong></p>
                    </div>
                    <div class="boxbtn">
                        <div class="centerclose">
                            <asp:Button ID="btnCerrarConfirmacion" OnClientClick="javascript:CerrarDialogoC('msjSatisfactorio');"
                                runat="server" CssClass="<%$ Resources:generales,imgCerrar %>" 
                                CausesValidation="False" meta:resourcekey="btnCerrarConfirmacionResource1" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

        <div id="msjError2" style="display: none">
        <div class="contealertPregunta">
            <div class="boxtext">
                <div class="boxdentro">
                    <div class="boxletra">
                        <p>
                            <strong>
                                <asp:Label ID="lblEliminar2" runat="server" Text="Desea eliminar el registro?" meta:resourcekey="lblEliminarResource1"></asp:Label></strong></p>
                    </div>
                    <div class="boxbtn">
                        <div class="centerclose">
                            <asp:Button ID="btnAceptar2" OnClientClick="javascript:EliminarSi2();" runat="server"
                                CssClass="<%$ Resources:generales,imgSi %>" meta:resourcekey="btnAceptarResource1" />
                            <asp:Button ID="btnNo2" OnClientClick="javascript:CerrarDialogoC('msjError');" runat="server"
                                CssClass="<%$ Resources:generales,imgNo %>" meta:resourcekey="btnNoResource1" />
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
                                <asp:Label ID="eliminarseleccionados" runat="server" Text="Debe seleccionar al menos un área para eliminar."
                                    meta:resourcekey="eliminarseleccionados2Resource1"></asp:Label></strong></p>
                    </div>
                    <div class="boxbtn">
                        <div class="centerclose">
                            <asp:Button ID="Button1" OnClientClick="javascript:CerrarDialogo('msjEliminarSeleccionados');"
                                runat="server" CssClass="<%$ Resources:generales,imgCerrar %>" meta:resourcekey="btnCerrarConfirmacionResource1" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
