<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="expiroSession.aspx.cs"
    Inherits="Intertek.WEB.expiroSession" meta:resourcekey="PageResource1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Styles/master.css" rel="stylesheet" type="text/css" />
    <link href="Styles/Alertas.css" rel="stylesheet" type="text/css" />
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
        <div class="contealert">
        <div class="boxtext2">
            <div class="boxdentro2">
                <div class="boxletra2"><p><strong><asp:label runat="server" ID="lblFinSession" 
                        meta:resourcekey="lblFinSessionResource1"></asp:label></strong></p></div>
                <div class="boxbtn">
                    <div class="centerclose">
                        <asp:Button ID="btnFinSession" 
                            CssClass="btncerraralert" runat="server" onclick="btnFinSession_Click" meta:resourcekey="btnFinSessionResource1"/></div>
                </div>
            </div>
        </div>
    </div>
    </div>
    </form>
</body>
</html>