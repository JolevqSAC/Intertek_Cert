<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="neoclientes.aspx.cs" Inherits="Intertek.WEB.Mantenimiento.neoclientes"
    meta:resourcekey="PageResource1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../Styles/tabs.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {

            CrearDialogo('msjSatisfactorio', 170, 390, '');
            CrearDialogoEliminar('msjError', 170, 390, '');
            CrearDialogo('msjCamposObligatorios', 170, 390, '');
            CrearDialogoEliminar('msjError2', 200, 360, '');
            CrearDialogoEliminar('msjError3', 200, 360, '');
            CrearDialogo('msjEliminarSeleccionados', 162, 360, '');
            $("#tabs").tabs();

            jQuery('#' + '<%=btnEliminar0.ClientID%>').live('click', function (event) {
                 event.preventDefault();

                VerificarSeleccionados();

            });


            jQuery('#' + '<%=btnEliminar3.ClientID%>').live('click', function (event) {
                 event.preventDefault();

                VerificarSeleccionados3();

            });



           // EliminarSi2();
           // EliminarSi3();

        });

        function EliminarSi() {
            var idRolEliminar = $('#hdEliminarID').val();
            CerrarDialogo('msjError');
            $('#btnEliminar').trigger("click");
        }

      
        function EliminarSi2() {
            //   var idRolEliminar = $('#hdEliminarID').val();
            CerrarDialogo('msjError2');
            // $('#btnQuitarContacto').trigger("click");
            jQuery('#' + '<%=btnQuitarContacto.ClientID%>').click();
        }


        function EliminarSi3() {
            //   var idRolEliminar = $('#hdEliminarID').val();
            CerrarDialogo('msjError3');
            // $('#btnQuitarContacto').trigger("click");
            jQuery('#' + '<%=btnQuitarDireccion.ClientID%>').click();
        }

        function CerrarDialogoC(nombreDIV) {
            $('#' + nombreDIV).dialog("close");
            $(location).attr('href', 'clientesBuscar.aspx');
        }

        function validate(event) {
            var rspt = true
            var tNombre = document.getElementById('<%= txtNombre.ClientID %>');
            var tApellido = document.getElementById('<%= txtApellido.ClientID %>');
            var tTipo = document.getElementById('<%= drdwlsTipo.ClientID %>');
            var tDireccion = document.getElementById('<%= txtDireccioSecundar.ClientID %>');
            var tRazonSocial = document.getElementById('<%= txtRazonSocial.ClientID %>');

            var tRUC = document.getElementById('<%= txtRUC.ClientID %>');
            if (tRazonSocial.value == "") {
                tRazonSocial.setAttribute('style', 'width: 600px; height: 20px;border-color:#FF0000');
                rspt = false;
            }


        }

        function ValidaCampos() {
            var razonsocial = document.getElementById('<%=txtRazonSocial.ClientID %>').value;
            var radiolistbtn = document.getElementById('<%=rbIndicadorArea.ClientID%>');
            //            var nombre = document.getElementById('<%=txtNombre.ClientID %>').value;
            //            var apellido = document.getElementById('<%=txtApellido.ClientID %>').value;
            //            var telefono = document.getElementById('<%=txtTelefonoContacto1.ClientID %>').value;
            //            var tipo = document.getElementById('<%=drdwlsTipo.ClientID %>').value;
            //            var direccion = document.getElementById('<%=txtDireccioSecundar.ClientID %>').value;

            //            if (vacio(razonsocial) || rblSelectedValue(radiolistbtn) || vacio(nombre) || vacio(apellido) || vacio(telefono) || tipo == 0 || vacio(direccion))
            if (vacio(razonsocial) || rblSelectedValue(radiolistbtn))
                MostrarMensaje('msjCamposObligatorios');
            else {
                $('#btnGrabar').trigger("click");
                return true;
            }
        }

        function VerificarSeleccionados() {
            var existenSeleccionados = false;
            $('#<%= grvwContacto.ClientID %> tr').each(function (indexF) {
                $(this).find('td').each(function (indexC) {
                    if (indexC == 5) {
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



        function VerificarSeleccionados3() {
            var existenSeleccionados = false;


            $('#<%= grvwDireccion.ClientID %> tr').each(function (indexF) {
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
                MostrarMensajeEliminar('msjError3', '');
            } else {
                //  alert("Debe seleccionar al menos un Cliente para eliminar.");
                MostrarMensaje('msjEliminarSeleccionados');
            }
        }
    
    </script>
    <style type="text/css">
        div#err
        {
            color: Red;
            font-family: Arial;
            font-size: 12px;
            font-variant: normal;
            font-weight: bold;
            margin-left: 10px;
            width: auto;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="contentgeneral" id="divContenedorGeneral">
        <div class="titulo1">
            <div class="titulo2">
                <div class="ti1">
                    <p>
                        <asp:Label ID="lblTitulo" runat="server" meta:resourcekey="lblTituloResource1" Text="Cliente"></asp:Label></p>
                </div>
                <div class="ti2">
                    <p>
                    </p>
                </div>
            </div>
        </div>
        <div id="tabs">
            <ul>
                <li><a href="#tabs-1">
                    <asp:Label ID="lblDato" runat="server" Text="Dato General" meta:resourcekey="lblDatoGneralTabResource1"></asp:Label></a></li>
                <li><a href="#tabs-2">
                    <asp:Label ID="lblDetalle" runat="server" Text="Contacto" meta:resourcekey="lblContactoTabResource1"></asp:Label></a></li>
                <li><a href="#tabs-3">
                    <asp:Label ID="lblDireccion2" runat="server" Text="Dirección" meta:resourcekey="lblDireccionTabResource1"></asp:Label></a></li>
            </ul>
            <div id="tabs-1">
                <div class="bar1">
                    <div class="line173">
                        <p>
                            <asp:Label ID="lblRazonSocial" runat="server" Text="Razón Social: " meta:resourcekey="lblRazonSocialResource1"></asp:Label></p>
                    </div>
                    <div class="line172">
                        <asp:TextBox ID="txtRazonSocial" MaxLength="150" CssClass="bordeimput" Width="700px"
                            Height="20px" runat="server" meta:resourcekey="txtRazonSocialResource1"></asp:TextBox>
                        
                        <asp:HiddenField ID="hdflIDCliente" runat="server" />
                    </div>
                    <div class="line8"><p><asp:Label ID="Label11" runat="server" Text="*" class="letraError"></asp:Label></p></div>
                </div>
                <div class="bar2">
                    <div class="line173">
                        <p>
                            <asp:Label ID="lblRUC" runat="server" Text="Ruc: " meta:resourcekey="lblRUCResource1"></asp:Label></p>
                    </div>
                    <div class="line174">
                        <asp:TextBox ID="txtRUC" MaxLength="11" CssClass="bordeimput" Width="100px" Height="20px"
                            runat="server" onkeypress="javascript:return validarNro(event)" meta:resourcekey="txtRUCResource1"></asp:TextBox>
                    </div>
                    <div class="line177_2">
                        <p>
                            <asp:Label ID="lblTelefono1" runat="server" Text="Telf. Oficina: " meta:resourcekey="lblTelefono1Resource1"></asp:Label></p>
                    </div>
                    <div class="line176">
                        <asp:TextBox ID="txtTelefono1" MaxLength="20" CssClass="bordeimput" Width="105px"
                            Height="20px" runat="server" onkeypress="javascript:return validarNro(event)"
                            meta:resourcekey="txtTelefono1Resource1"></asp:TextBox></div>
                    <div class="line177_2">
                        <p>
                            <asp:Label ID="lblTelefonoPlanta" runat="server" Text="Telf. Planta: " meta:resourcekey="lblTelefonoPlantaResource1"></asp:Label></p>
                    </div>
                    <div class="line176">
                        <asp:TextBox ID="txtTelefono2" MaxLength="20" CssClass="bordeimput" Width="105px"
                            Height="20px" runat="server" onkeypress="javascript:return validarNro(event)"
                            meta:resourcekey="txtTelefono2Resource1"></asp:TextBox></div>
                    <div class="line178">
                        <p>
                            <asp:Label ID="lblFax" runat="server" Text="Fax: " meta:resourcekey="lblFaxResource1"></asp:Label></p>
                    </div>
                    <div class="line176">
                        <asp:TextBox ID="txtFax" MaxLength="20" CssClass="bordeimput" Width="105px" Height="20px"
                            runat="server" onkeypress="javascript:return validarNro(event)" meta:resourcekey="txtFaxResource1"></asp:TextBox></div>
                </div>
                <div class="bar2">
                    <div class="line173">
                        <p>
                            <asp:Label ID="lblEmail" runat="server" Text="E-mail: " meta:resourcekey="lblEmailResource1"></asp:Label></p>
                    </div>
                    <div class="line175">
                        <asp:TextBox ID="txtEmail" MaxLength="50" CssClass="bordeimput" Width="455px" Height="20px"
                            runat="server" meta:resourcekey="txtEmailResource1"></asp:TextBox>
                        <asp:Label ID="lblerrorEmail" runat="server" Text="formato no válido en Email" class="letraError"
                            Visible="False" meta:resourcekey="lblerrorEmailResource1"></asp:Label>
                    </div>
                </div>
                <div class="bar2">
                    <div class="line173">
                        <p>
                            <asp:Label ID="lblIndicadorArea" runat="server" Text="Indicador Área:" meta:resourcekey="lblIndicadorAreaResource1"></asp:Label>
                        </p>
                    </div>
                    <div class="line52">
                        <asp:RadioButtonList ID="rbIndicadorArea" runat="server" RepeatDirection="Horizontal"
                            CssClass="letra2">
                            <asp:ListItem runat="server" Text="Agri" Value="A"></asp:ListItem>
                            <asp:ListItem runat="server" Text="Hidro" Value="H"></asp:ListItem>
                            <asp:ListItem runat="server" Text="Mixto" Value="M"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div> 
                    <div class="line8"><p><asp:Label ID="Label12" runat="server" Text="*" class="letraError"></asp:Label></p></div>  
                </div>
                <div class="bar2">
                    <div class="line173">
                        <p>
                            <asp:Label ID="lblPais" runat="server" Text="País: " meta:resourcekey="lblPaisResource1"></asp:Label></p>
                    </div>
                    <div class="line179">
                        <asp:DropDownList ID="ddlPais" CssClass="bordeimput" Width="162px" Height="24px"
                            runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlPais_SelectedIndexChanged"
                            meta:resourcekey="ddlPaisResource1">
                        </asp:DropDownList>
                    </div>
                    <div class="line180">
                        <p>
                            <asp:Label ID="lblDepartamento" runat="server" Text="Departamento: " meta:resourcekey="lblDepartamentoResource1"></asp:Label></p>
                    </div>
                    <div class="line179">
                        <asp:DropDownList ID="ddlDepartamento" CssClass="bordeimput" Width="162px" Height="24px"
                            runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlDepartamento_SelectedIndexChanged"
                            meta:resourcekey="ddlDepartamentoResource1">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="bar2">
                    <div class="line173">
                        <p>
                            <asp:Label ID="lblProvincia" runat="server" Text="Provincia: " meta:resourcekey="lblProvinciaResource1"></asp:Label></p>
                    </div>
                    <div class="line179">
                        <asp:DropDownList ID="ddlProvincia" CssClass="bordeimput" Width="162px" Height="24px"
                            runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlProvincia_SelectedIndexChanged"
                            meta:resourcekey="ddlProvinciaResource1">
                        </asp:DropDownList>
                    </div>
                    <div class="line180">
                        <p>
                            <asp:Label ID="lblDistrito" runat="server" Text="Distrito: " meta:resourcekey="lblDistritoResource1"></asp:Label></p>
                    </div>
                    <div class="line179">
                        <asp:DropDownList ID="ddlDistrito" CssClass="bordeimput" Width="162px" Height="24px"
                            runat="server" meta:resourcekey="ddlDistritoResource1">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="bar2">
                    <div class="line173">
                        <p>
                            <asp:Label ID="Label6" runat="server" Text="Dirección: " meta:resourcekey="Label6Resource1"></asp:Label></p>
                    </div>
                    <div class="line175">
                        <asp:TextBox ID="txtDireccion" MaxLength="150" CssClass="bordeimput" Width="455px"
                            Height="20px" runat="server" meta:resourcekey="txtDireccionResource1"></asp:TextBox></div>
                </div>
                <div class="bar5option">
                    <div class="line182">
                        <p>
                            <asp:Label ID="lblObservaciones" runat="server" Text="Observación: " meta:resourcekey="lblObservacionesResource1"></asp:Label></p>
                    </div>
                    <div class="line93">
                        <asp:TextBox ID="txtObservaciones" MaxLength="200" TextMode="MultiLine" CssClass="bordeimput"
                            Width="639px" Height="76px" runat="server" onKeyUp="javascript:Count(this,200);"
                            onChange="javascript:Count(this,200);" meta:resourcekey="txtObservacionesResource1"></asp:TextBox>
                    </div>
                </div>
               
                <div style="clear: both">
                </div>
            </div>
            <div id="tabs-2">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div class="bar2">
                            <div class="line173">
                                <p>
                                    <asp:Label ID="lblContacto" runat="server" Text="Nombre: " meta:resourcekey="lblContactoResource1"></asp:Label>
                                    <asp:HiddenField runat="server" ClientIDMode="Static" ID="hdflIdContacto" />
                                </p>
                            </div>
                            <div class="line179">
                                <asp:TextBox ID="txtNombre" MaxLength="150" CssClass="bordeimput" Width="200px" Height="20px"
                                    runat="server" onkeypress="javascript:return validarChr(event)" meta:resourcekey="txtNombreResource1"></asp:TextBox>
                            </div>
                            <div class="line8"><p><asp:Label ID="Label13" runat="server" Text="*" class="letraError"></asp:Label></p></div>
                            <div class="line180">
                                <p>
                                    <asp:Label ID="Label1" runat="server" Text="Apellido: " meta:resourcekey="Label1Resource1"></asp:Label></p>
                            </div>
                            <div class="line179">
                                <asp:TextBox ID="txtApellido" MaxLength="150" CssClass="bordeimput" Width="200px"
                                    Height="20px" runat="server" onkeypress="javascript:return validarChr(event)"
                                    meta:resourcekey="txtApellidoResource1"></asp:TextBox>
                            </div>
                            
                        </div>
                        <div class="line8"><p><asp:Label ID="Label14" runat="server" Text="*" class="letraError"></asp:Label></p></div>
                        <div class="bar2">
                            <div class="line173">
                                <p>
                                    <asp:Label ID="Label2" runat="server" Text="Cargo: " meta:resourcekey="Label2Resource1"></asp:Label></p>
                            </div>
                            <div class="line179">
                                <asp:TextBox ID="txtCargo" MaxLength="150" CssClass="bordeimput" Width="200px" Height="20px"
                                    runat="server" onkeypress="javascript:return validarChr(event)" meta:resourcekey="txtCargoResource1"></asp:TextBox></div>
                            <div class="line8"><p><asp:Label ID="Label17" runat="server" Text="" class="letraError">&nbsp;</asp:Label></p></div>
                            <div class="line180">
                                <p>
                                    <asp:Label ID="Label3" runat="server" Text="Teléfono: " meta:resourcekey="Label3Resource1"></asp:Label></p>
                            </div>
                            
                            <div class="line179">
                                <asp:TextBox ID="txtTelefonoContacto1" MaxLength="150" CssClass="bordeimput" Width="200px"
                                    Height="20px" runat="server" onkeypress="javascript:return validarNro(event)"
                                    meta:resourcekey="txtTelefonoContacto1Resource1"></asp:TextBox>                                
                            </div>
                            <div class="line8"><p><asp:Label ID="Label15" runat="server" Text="*" class="letraError"></asp:Label></p></div>
                        </div>
                        <div class="bar2">
                            <div class="line173">
                                <p>
                                    <asp:Label ID="Label4" runat="server" Text="Celular: " meta:resourcekey="Label4Resource1"></asp:Label></p>
                            </div>
                            <div class="line179">
                                <asp:TextBox ID="txtTelefonoContacto2" MaxLength="150" CssClass="bordeimput" Width="200px"
                                    Height="20px" runat="server" onkeypress="javascript:return validarNro(event)"
                                    meta:resourcekey="txtTelefonoContacto2Resource1"></asp:TextBox></div>
                            <div class="line173">
                                <p>
                                    <asp:Label ID="Label7" runat="server" meta:resourcekey="Label7Resource1"></asp:Label></p>
                            </div>
                            <div class="line179">
                                <div class="btns2" id="divbtnAgregarContacto" runat="server">
                                    <asp:Button ID="btnAgregarContacto" runat="server" CssClass="<%$ Resources:generales,imgAgregar %>"
                                        meta:resourcekey="btnAgregarContactoResource1" ValidationGroup="validarCliente2"
                                        UseSubmitBehavior="False" OnClick="btnAgregarContacto_Click" />
                                </div>
                                <div class="btns3" id="divbtnQuitarContacto" runat="server">
                                    <input type="button" runat="server" id="btnEliminar0" value="Quitar" class="btnquitar" />
                                    <asp:Button ID="btnQuitarContacto" runat="server" meta:resourcekey="btnQuitarContactoResource1"
                                        CausesValidation="False" UseSubmitBehavior="False" CssClass="ocultarBoton" OnClick="btnQuitarContacto_Click" />
                                </div>
                                <div class="btns2" id="divbntActualizarContacto" runat="server" visible="False">
                                    <asp:Button ID="bntActualizarContacto" runat="server" CssClass="<%$ Resources:generales,imgGrabar %>" ValidationGroup="validarCliente2"
                                        UseSubmitBehavior="False" meta:resourcekey="bntActualizarContactoResource1" OnClick="bntActualizarContacto_Click" />
                                </div>
                                <div class="btns3" id="divbntCancelarContacto" runat="server" visible="False">
                                    <asp:Button ID="bntCancelarContacto" runat="server" CssClass="<%$ Resources:generales,imgCancelar %>"
                                        meta:resourcekey="bntCancelarContactoResource1" CausesValidation="False" OnClick="bntCancelarContacto_Click" />
                                </div>
                            </div>
                        </div>
                        <div class="bar11">
                            <div class="line225">
                                <asp:GridView ID="grvwContacto" runat="server" CssClass="gridview" AutoGenerateColumns="False"
                                    AllowPaging="True" Width="800px" OnPageIndexChanging="grvwContacto_PageIndexChanging"
                                    PageSize="5" meta:resourcekey="grvwContactoResource1" OnRowCommand="grvwContacto_RowCommand">
                                    <Columns>
                                        <asp:BoundField DataField="COC_Nombres" HeaderText="Nombres" meta:resourcekey="BoundFieldResource1" />
                                        <asp:BoundField DataField="COC_Apellidos" HeaderText="Apellidos" meta:resourcekey="BoundFieldResource2" />
                                        <asp:BoundField DataField="COC_Cargo" HeaderText="Cargo" meta:resourcekey="BoundFieldResource3" />
                                        <asp:BoundField DataField="COC_Telefono1" HeaderText="Teléfono" meta:resourcekey="BoundFieldResource4" />
                                        <asp:BoundField DataField="COC_Telefono2" HeaderText="Celular" meta:resourcekey="BoundFieldResource5" />
                                        <asp:TemplateField HeaderText="Selec." meta:resourcekey="TemplateFieldResource1">
                                            <ItemTemplate>
                                                <center>
                                                    <asp:CheckBox ID="chkSeleccionContacto" runat="server" meta:resourcekey="chkSeleccionContactoResource1" />
                                                    <asp:HiddenField ID="hidIdContacto" runat="server" Value='<%# Eval("IDContactoCliente") %>' />
                                                </center>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField meta:resourcekey="TemplateFieldResource2">
                                            <ItemTemplate>
                                                <center>
                                                    <asp:LinkButton ID="lkbEditar" ToolTip="Modificar" runat="server" CausesValidation="False"
                                                        CommandName="Editar" CommandArgument='<%# Eval("IDContactoCliente") %>' meta:resourcekey="lkbEditarResource1">
                                                    <img src="/img/btnmdificar.png" alt="Modificar" style="border:0" /></asp:LinkButton>
                                                </center>
                                            </ItemTemplate>
                                            <ItemStyle BorderStyle="None" Width="90px" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <div style="clear: both">
                            </div>
                        </div>
                        
                        <div style="clear: both">
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div id="tabs-3">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <div class="bar2">
                            <div class="line173">
                                <p>
                                    <asp:Label ID="Label5" runat="server" Text="Tipo: " meta:resourcekey="Label5Resource1"></asp:Label>
                                    <asp:HiddenField runat="server" ClientIDMode="Static" ID="hdflIdDireccion" />
                                </p>
                            </div>
                            <div class="line179">
                                <asp:DropDownList ID="drdwlsTipo" MaxLength="150" CssClass="bordeimput" Width="200px"
                                    Height="20px" runat="server" meta:resourcekey="drdwlsTipoResource1">
                                    <asp:ListItem meta:resourcekey="ListItemResource1">Seleccionar</asp:ListItem>
                                    <asp:ListItem Value="Tienda" meta:resourcekey="ListItemResource2">Tienda</asp:ListItem>
                                    <asp:ListItem Value="Almacen" meta:resourcekey="ListItemResource3">Almacen</asp:ListItem>
                                    <asp:ListItem Value="Planta" meta:resourcekey="ListItemResource4">Planta</asp:ListItem>
                                    <asp:ListItem Value="Otros" meta:resourcekey="ListItemResource5">Otros</asp:ListItem>
                                </asp:DropDownList>
                                
                            </div>
                            <div class="line8"><p><asp:Label ID="Label16" runat="server" Text="*" class="letraError"></asp:Label></p></div>
                            <div class="line173">
                                <p>
                                    <asp:Label ID="Label8" runat="server" meta:resourcekey="Label8Resource1"></asp:Label></p>
                            </div>
                            <div class="line179">
                                <div class="btns2" id="divbtnAgregarDireccion" runat="server">
                                    <asp:Button ID="btnAgregarDireccion" runat="server" CssClass="<%$ Resources:generales,imgAgregar %>"
                                        meta:resourcekey="btnAgregarDireccionResource1" UseSubmitBehavior="False" ValidationGroup="validarCliente3"
                                        OnClick="btnAgregarDireccion_Click" />
                                </div>
                                <div class="btns3" id="divbtnQuitarDireccion" runat="server">
                                    <input type="button" runat="server" id="btnEliminar3" value="Quitar" class="btnquitar" />
                                    <asp:Button ID="btnQuitarDireccion" runat="server" UseSubmitBehavior="False" CssClass="ocultarBoton"
                                        meta:resourcekey="btnQuitarContactoResource1" OnClick="btnQuitarDireccion_Click" />
                                </div>
                                <div class="btns2" id="divbntActualizarDireccion" runat="server" visible="False">
                                    <asp:Button ID="bntActualizarDireccion" runat="server" CssClass="<%$ Resources:generales,imgGrabar %>" ValidationGroup="validarCliente3"
                                        UseSubmitBehavior="False" meta:resourcekey="bntActualizarDireccionResource1"
                                        OnClick="bntActualizarDireccion_Click" />
                                </div>
                                <div class="btns3" id="divbntCancelarDireccion" runat="server" visible="False">
                                    <asp:Button ID="bntCancelarDireccion" runat="server" CssClass="<%$ Resources:generales,imgCancelar %>"
                                        meta:resourcekey="bntCancelarDireccionResource1" CausesValidation="False" OnClick="bntCancelarDireccion_Click" />
                                </div>
                            </div>
                        </div>
                        <div class="bar2">
                            <div class="line173">
                                <p>
                                    <asp:Label ID="lblDireccion" runat="server" Text="Dirección: " meta:resourcekey="lblDireccionResource1"></asp:Label></p>
                            </div>
                            <div class="line175">
                                <asp:TextBox ID="txtDireccioSecundar" MaxLength="150" CssClass="bordeimput" Width="400px"
                                    Height="20px" runat="server" meta:resourcekey="txtDireccioSecundarResource1"></asp:TextBox>
                               
                            </div>
                            <div class="line8"><p><asp:Label ID="Label18" runat="server" Text="*" class="letraError"></asp:Label></p></div>
                        </div>
                        <div class="bar11">
                            <div class="line225">
                                <asp:GridView ID="grvwDireccion" runat="server" CssClass="gridview" AutoGenerateColumns="False"
                                    AllowPaging="True" Width="400px" OnPageIndexChanging="grvwDireccion_PageIndexChanging"
                                    OnRowCommand="grvwDireccion_RowCommand" PageSize="5" meta:resourcekey="grvwDireccionResource1">
                                    <Columns>
                                        <asp:BoundField DataField="DIC_Tipo" HeaderText="Tipo" meta:resourcekey="BoundFieldResource6" />
                                        <asp:BoundField DataField="DIC_Descripcion" HeaderText="Dirección" meta:resourcekey="BoundFieldResource7" />
                                        <asp:TemplateField HeaderText="Selec." meta:resourcekey="TemplateFieldResource3">
                                            <ItemTemplate>
                                                <center>
                                                    <asp:CheckBox ID="chkSeleccionDireccion" runat="server" meta:resourcekey="chkSeleccionDireccionResource1" />
                                                    <asp:HiddenField ID="hidIdDireccion" runat="server" Value='<%# Eval("IDDireccionCliente") %>' />
                                                </center>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField meta:resourcekey="TemplateFieldResource4">
                                            <ItemTemplate>
                                                <center>
                                                    <%--  <asp:Button ID="btnModificarDireccion" runat="server" CssClass="btnmodificar" meta:resourcekey="btnModificarDireccionResource1"
                                                        CausesValidation="False" OnClick="btnModificarDireccion_Click" />--%>
                                                    <asp:LinkButton ID="lkbEditar" ToolTip="Modificar" runat="server" CausesValidation="False"
                                                        CommandName="Editar" CommandArgument='<%# Eval("IDDireccionCliente") %>' meta:resourcekey="lkbEditarResource1">
                                                    <img src="/img/btnmdificar.png" alt="Modificar" style="border:0" /></asp:LinkButton>
                                                </center>
                                            </ItemTemplate>
                                            <ItemStyle BorderStyle="None" Width="90px" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>                        
                        <div style="clear: both">
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
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
                        meta:resourcekey="btnCancelarResource1" CausesValidation="False" OnClick="btnCancelar_Click"
                        OnClientClick="javascript:DesactivarValidacion();" /></div>
                <div class="btns3">
                    <asp:Button ID="btnLimpiar" runat="server" CssClass="<%$ Resources:generales,imgLimpiar %>"
                        meta:resourcekey="btnLimpiarResource1" CausesValidation="False" OnClick="btnLimpiar_Click"
                        OnClientClick="javascript:DesactivarValidacion();" />
                    <asp:Button runat="server" ID="btnEliminar" ClientIDMode="Static" CssClass="ocultarBoton"
                        OnClick="btnEliminar_Click" meta:resourcekey="btnEliminarResource1" />
                </div>
            </div>
        </div>
        <div class="bar4">
            <asp:Label ID="lblMensaje" runat="server" CssClass="letraError" meta:resourcekey="lblMensajeResource1"></asp:Label>
        </div>
    </div>
    <div id="msjSatisfactorio" style="display: none">
        <div class="contealert">
            <div class="boxtext">
                <div class="boxdentro">
                    <div class="boxletra">
                        <p>
                            <strong>
                                <asp:Label ID="lblConfirmacion" runat="server" Text="El registro se Grabo satisfactoriamente"
                                    meta:resourcekey="lblConfirmacionResource1"></asp:Label></strong></p>
                    </div>
                    <div class="boxbtn">
                        <div class="centerclose">
                            <asp:Button ID="btnCerrarConfirmacion" OnClientClick="javascript:CerrarDialogoC('msjSatisfactorio','clientesBuscar.aspx');"
                                runat="server" CssClass="<%$ Resources:generales,imgCerrar %>" CausesValidation="False"
                                meta:resourcekey="btnCerrarConfirmacionResource1" />
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
                                <asp:Label ID="lblEliminar" runat="server" Text="Desea eliminar el registro?" meta:resourcekey="lblEliminarResource1"></asp:Label></strong></p>
                    </div>
                    <div class="boxbtn">
                        <div class="centerclose">
                            <asp:Button ID="btnAceptar" OnClientClick="javascript:EliminarSi();" runat="server"
                                CssClass="<%$ Resources:generales,imgSi %>" meta:resourcekey="btnAceptarResource1" />
                            <asp:Button ID="btnNo" OnClientClick="javascript:CerrarDialogoC('msjError');" runat="server"
                                CssClass="<%$ Resources:generales,imgNo %>" meta:resourcekey="btnNoResource1" />
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
    <div id="msjError3" style="display: none">
        <div class="contealertPregunta">
            <div class="boxtext">
                <div class="boxdentro">
                    <div class="boxletra">
                        <p>
                            <strong>
                                <asp:Label ID="lblEliminar3" runat="server" Text="Desea eliminar el registro?" meta:resourcekey="lblEliminarResource1"></asp:Label></strong></p>
                    </div>
                    <div class="boxbtn">
                        <div class="centerclose">
                            <asp:Button ID="btnEliminar_3" OnClientClick="javascript:EliminarSi3();" runat="server"
                                CssClass="<%$ Resources:generales,imgSi %>" meta:resourcekey="btnAceptarResource1" />
                            <asp:Button ID="btnEliminar33" OnClientClick="javascript:CerrarDialogoC('msjError');" runat="server"
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
                                    meta:resourcekey="eliminarseleccionadosResource1"></asp:Label></strong></p>
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
    <div id="msjCamposObligatorios" style="display: none" title="Error">
        <div class="contealertError">
            <div class="boxtext">
                <div class="boxdentro">
                    <div class="boxletra2">
                         <p>
                            <strong><asp:Label ID="Label10" runat="server" 
                                Text="Debe ingresar los campos obligatorios marcados con asterisco(*)" 
                                meta:resourcekey="lblCampoObligatorioResource1"></asp:Label></strong>
                         </p>
                    </div>
                    <div class="boxbtn">
                        <div class="centerclose">
                            <asp:Button runat="server" ID="btnCerrarObligatorios" CssClass="<%$ Resources:generales,imgCerrar %>"
                                OnClientClick="CerrarDialogo('msjCamposObligatorios');" 
                                CausesValidation="False" meta:resourcekey="btnCerrarConfirmacionResource1"/></div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
