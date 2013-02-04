<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Intertek.WEB.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Intertek</title>
    <link href="Styles/master.css" rel="stylesheet" type="text/css" />
    <link href="Styles/Alertas.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-1.8.2.js" type="text/javascript" language="javascript"></script>
    <script src="Scripts/jquery-ui-1.9.0.custom.js" type="text/javascript" language="javascript"></script>
    <script src="Scripts/funcGlobales.js" type="text/javascript" language="javascript"></script>
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {

            //CrearDialogo(nombreDIV, alto, ancho, titulo);
            CrearDialogo('msjError', 200, 200, '');

        });

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="contelogin">
            <div class="loginbase">
                <div class="conte1">
                    <div class="conte2">
                        <div class="bar1">
                            <div class="log1">
                                <p>
                                    <strong>Usuario:</strong></p>
                            </div>
                            <div class="log2">
                                <asp:TextBox ID="txtUsuario" CssClass="bordeimputlogin" Width="240px" Height="25px"
                                    runat="server" Text="Administrador"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvUsuario" runat="server" ErrorMessage="Ingrese su Usuario"
                                    ControlToValidate="txtUsuario" ForeColor="Red" Font-Size="10" ></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="bar1">
                            <div class="log1">
                                <p>
                                    <strong>Password:</strong></p>
                            </div>
                            <div class="log2">
                                <asp:TextBox ID="txtPassword" CssClass="bordeimputlogin" TextMode="Password" Width="240px"
                                    Height="25px" runat="server" Text="1234"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvClave" runat="server" ErrorMessage="Ingrese su Password"
                                    ControlToValidate="txtPassword"  ForeColor="Red" Font-Size="10" ></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="bar2">
                            <div class="cuadrologin">
                            
                                <asp:Button ID="btnLogin" CssClass="btnlogin" runat="server" Text="Login" OnClick="Button1_Click" /></div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="msjError" style="display: none">
        <div class="contealertError">
            <div class="boxtext">
                <div class="boxdentro">
                    <div class="boxletra">
                        <p>
                            <strong><asp:Label ID="lblError" runat="server" Text="Usuario o Password Incorrecto"></asp:Label></strong></p>
                    </div>
                    <div class="boxbtn">
                        <div class="centerclose">
                            <asp:Button runat="server" ID="btnCerrar" Text="Cerrar" CssClass="<%$ Resources:generales,imgCerrar %>"
                                OnClientClick="CerrarDialogo('msjError');" /></div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>