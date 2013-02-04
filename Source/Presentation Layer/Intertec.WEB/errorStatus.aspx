<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="errorStatus.aspx.cs" Inherits="Intertek.WEB.errorStatus" meta:resourcekey="PageResource1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link href="styles/master.css" rel="stylesheet" type="text/css" />
    <link href="styles/Alertas.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div id="header">
            <div class="logointertek">
            </div>
            <div class="conteopciones">
            </div>
        </div>
        <div class="contealertError">
        <div class="boxtext2">
            <div class="boxdentro2">
                <div class="boxletra2"><p><strong><asp:label runat="server" ID="lblMensajeError" 
                        Text="Lo Sentimos, por favor intente después de unos momentos" 
                        meta:resourcekey="lblMensajeErrorResource1"></asp:label></strong></p></div>
                <div class="boxbtn">
                    <div class="centerclose">
                        <asp:Button ID="btnRegresar" 
                            CssClass="imgcierre" runat="server" Text="Regresar" 
                            onclick="btnRegresar_Click" meta:resourcekey="btnRegresarResource1"/></div>
                </div>
            </div>
        </div>
    </div>
    </div>
    </form>
</body>
</html>
