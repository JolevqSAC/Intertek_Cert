<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ensayosBuscar.aspx.cs" Inherits="Intertek.WEB.Mantenimiento.ensayosBuscar"
    meta:resourcekey="PageResource1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {

            CrearDialogo('msjSatisfactorio', 162, 360, '');
            CrearDialogoEliminar('msjError', 162, 360, '');
            CrearDialogo('msjEliminarSeleccionados', 162, 360, '');



            jQuery('#' + '<%=btnEliminar0.ClientID%>').click(function (event) {
                // event.preventDefault();
                VerificarSeleccionados();

            });

        });

        var baseUrl = 'http://localhost:45649/Mantenimiento/Handlers/Ensayo.ashx';

        function eliminar() {
            jQuery.ajax({
                type: 'POST',
                dataType: 'json',
                contentType: "application/json; charset=utf-8",
                url: baseUrl,
                data: ({
                    accion: 'eliminar'
                    // id: jQuery('#hdnId').val()

                }),
                success: function (data) {
                    if (data.Id > 0) {

                        //  alert(data.Mensaje, "Producto");

                        //  jQuery('#divMantenimiento').dialog('close');
                        // buscar();
                    }
                    else {
                        //  alert(data.Mensaje, "Producto");
                    }
                },
                error: function (data) {
                    //    alert(data.Mensaje, ' Producto');

                }

            });
        }


        function VerificarSeleccionados() {
            var existenSeleccionados = false;
            $('#<%= gvBuscar.ClientID %> tr').each(function (indexF) {
                $(this).find('td').each(function (indexC) {
                    if (indexC == 4) {
                        var chk = $(this).find('input')[0];
                        if (chk != undefined) {
                            if (chk.checked == true) {
                                existenSeleccionados = true;
                            }
                        }
                    }
                });
            });

            if (existenSeleccionados == true) {
                MostrarMensajeEliminar('msjError', '');
                //  $('#btnEliminar').trigger("click");

            } else {

               MostrarMensaje('msjEliminarSeleccionados');
              //  alert("Debe seleccionar al menos un Ensayo para eliminar.");
            }
        }



        function EliminarSi() {

            CerrarDialogo('msjError');

            //  $('#btnEliminar').trigger("click");
            jQuery('#' + '<%=btnEliminar.ClientID%>').click();

        }




        function Insertar() {
            var parametros = { accion: 'insertar', nombre: "hola" };
            $.ajax(
        {
            url: "Handlers/Ensayo.ashx",
            data: parametros,
            success: function (data) {
                //                $("#txtID").get(0).value = data.id;
                //                $("#txtSentence").get(0).value = data.sentence;
            },
            error: function () {
                //                alert(arguments[2]);
            }
        });
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="prueba">
    </div>
    <div class="contentgeneral">
        <div class="titulo1">
            <div class="titulo2">
                <div class="ti1">
                    <p>
                        <asp:Label ID="lblTitulo" runat="server" Text="Buscar Ensayo" meta:resourcekey="lblTituloResource1"></asp:Label></p>
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
                        <asp:Label ID="Label1" runat="server" Text="Código:" meta:resourcekey="lblCodeResource1"></asp:Label>
                    </p>
                </div>
                <div class="line175">
                    <asp:TextBox ID="txtCodigo" runat="server" CssClass="bordeimput" MaxLength="150"
                        Width="300px" meta:resourcekey="txtCodigoResource1"></asp:TextBox></div>
            </div>
            <div class="bar2">
                <div class="line173">
                    <p>
                        <asp:Label ID="lblNombre" runat="server" Text="Nombre:" meta:resourcekey="lblNombreResource1"></asp:Label>
                    </p>
                </div>
                <div class="line175">
                    <asp:TextBox ID="txtNombre" runat="server" CssClass="bordeimput" MaxLength="100"
                        Width="300px" meta:resourcekey="txtNombreResource2"></asp:TextBox></div>
            </div>
            <div class="bar2">
                <div class="line173">
                    <p>
                        <asp:Label ID="Label2" runat="server" Text="Nombre Ingles:" meta:resourcekey="lblEnglishNameResource1"></asp:Label>
                    </p>
                </div>
                <div class="line175">
                    <asp:TextBox ID="txtNombreIngles" runat="server" CssClass="bordeimput" MaxLength="100"
                        Width="300px" meta:resourcekey="txtNombreInglesResource1"></asp:TextBox></div>
            </div>
            <div class="bar2">
                <div class="line173">
                    <p>
                        <asp:Label ID="Label3" runat="server" Text="Descripción:" meta:resourcekey="lblDescriptionResource1"></asp:Label>
                    </p>
                </div>
                <div class="line175">
                    <asp:TextBox ID="txtDescripcion" runat="server" CssClass="bordeimput" MaxLength="100"
                        Width="300px" meta:resourcekey="txtDescripcionResource1"></asp:TextBox></div>
            </div>
            <div style="clear: both">
            </div>
        </div>
        <div class="bar4">
            <div class="bar4tresili">
                <div class="btns2">
                    <asp:Button ID="btnBuscar" runat="server" CssClass="<%$ Resources:generales,imgBuscar %>"
                        OnClick="btnBuscar_Click" meta:resourcekey="btnBuscarResource1" />
                </div>
                <div class="btns3">
                    <asp:Button ID="btnLimpiar" runat="server" CssClass="<%$ Resources:generales,imgLimpiar %>"
                        meta:resourcekey="btnClearResource1" OnClick="btnLimpiar_Click" />
                </div>
                <div class="btns3">
                    <asp:Button ID="btnNuevo" runat="server" CssClass="<%$ Resources:generales,imgNuevo %>"
                        OnClick="btnNuevo_Click" meta:resourcekey="btnNuevoResource1" />
                </div>
                <div class="btns3">
                    <input type="button" runat="server" id="btnEliminar0" value="Eliminar" class="btncancelar" />
                    <asp:Button runat="server" ID="btnEliminar" Text="Eliminar" ClientIDMode="Static"
                        CssClass="ocultarBoton" OnClick="btnEliminar_Click" meta:resourcekey="btnEliminarResource3" />
                </div>
            </div>
        </div>
        <div class="separa">
            <div class="conteradio1">
                <div class="bar5">
                    <center>
                        <div>
                            <asp:Label ID="lblmensaje" runat="server" CssClass="letraError" meta:resourcekey="lblmensajeResource2"></asp:Label>
                        </div>
                        <asp:GridView ID="gvBuscar" runat="server" CssClass="gridview" AutoGenerateColumns="False"
                            AllowPaging="True" Width="800px" OnPageIndexChanging="gvBuscar_PageIndexChanging"
                            meta:resourcekey="gvBuscarResource1" OnRowCommand="gvBuscar_RowCommand" 
                            PageSize="20">
                            <Columns>
                                <asp:BoundField DataField="ENS_Codigo" HeaderText="Codigo" meta:resourcekey="BoundFieldResource1" />
                                <asp:BoundField DataField="ENS_Nombre" HeaderText="Nombre" meta:resourcekey="BoundFieldResource2" />
                                <asp:BoundField DataField="ENS_NombreIngles" HeaderText="NombreIngles" meta:resourcekey="BoundFieldResource3" />
                                <asp:BoundField DataField="ENS_Descripcion" HeaderText="Descripción" meta:resourcekey="BoundFieldResource4" />
                                <asp:TemplateField HeaderText="Selec." meta:resourcekey="TemplateFieldResource1">
                                    <ItemTemplate>
                                        <center>
                                            <asp:CheckBox ID="chkSeleccion" runat="server" meta:resourcekey="chkSeleccionResource1" />
                                            <asp:HiddenField ID="hidIdEnsayo" runat="server" Value='<%# Eval("IDEnsayo") %>' />
                                        </center>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Modificar" meta:resourcekey="TemplateFieldResource2">
                                    <ItemTemplate>
                                        <center>
                                            <asp:LinkButton ID="lkbEditar" ToolTip="Editar" runat="server" CausesValidation="False"
                                                CommandName="Editar" CommandArgument='<%# Eval("IDEnsayo") %>' meta:resourcekey="lkbEditarResource1">
                                            <img src="/img/btnmdificar.png" alt="Modificar" style="border:0" /> 
                                   </asp:LinkButton>
                                        </center>
                                    </ItemTemplate>
                                    <ItemStyle Width="90px" />
                                </asp:TemplateField>
                            </Columns>
                            <PagerSettings Mode="NumericFirstLast" PageButtonCount="2" />
                        </asp:GridView>
                    </center>
                </div>
                <div style="clear: both">
                    <asp:HiddenField runat="server" ClientIDMode="Static" ID="hdEliminarID" />
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
                            <asp:Button ID="btnNo" OnClientClick="javascript:CerrarDialogo('msjError');" runat="server"
                                CssClass="<%$ Resources:generales,imgNo %>" meta:resourcekey="btnNoResource1" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="msjSatisfactorio" style="display: none">
        <div class="contealert">
            <div class="boxtext">
                <div class="boxdentro">
                    <div class="boxletra">
                        <p>
                            <strong>
                                <asp:Label ID="lblConfirmacion" runat="server" Text="El registro se eliminó satisfactoriamente"
                                    meta:resourcekey="lblConfirmacionResource1"></asp:Label></strong></p>
                    </div>
                    <div class="boxbtn">
                        <div class="centerclose">
                            <asp:Button ID="btnCerrarConfirmacion" OnClientClick="javascript:CerrarDialogo('msjSatisfactorio');"
                                runat="server" CssClass="<%$ Resources:generales,imgCerrar %>" meta:resourcekey="btnCerrarConfirmacionResource1" />
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
                                <asp:Label ID="lbleliminarseleccionados" runat="server" Text="Debe seleccionar al menos un área para eliminar."
                                    meta:resourcekey="eliminarseleccionadosResource1"></asp:Label></strong></p>
                    </div>
                    <div class="boxbtn">
                        <div class="centerclose">
                            <asp:Button ID="btnCerrarConfirmacion2" OnClientClick="javascript:CerrarDialogo('msjEliminarSeleccionados');"
                                runat="server" CssClass="<%$ Resources:generales,imgCerrar %>" meta:resourcekey="btnCerrarConfirmacionResource1" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
