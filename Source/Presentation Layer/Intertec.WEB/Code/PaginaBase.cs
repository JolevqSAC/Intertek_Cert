using System;
using System.Globalization;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.Security;
using Intertek.Business.Entities;
using Intertek.Helpers;

public class PaginaBase:System.Web.UI.Page
{
    /// <summary>
    /// Cambia de cultura
    /// </summary>
    protected override void InitializeCulture()
    {
        ValidarSesionActiva();

        if (Session["ddlIdiomas"] != null)
        {
            String selectedLanguage = Session["ddlIdiomas"].ToString();//Request.Form["ListBox1"];
           
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(selectedLanguage);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(selectedLanguage);
        }
         base.InitializeCulture();
    }
   
    /// <summary>
    /// Verifica si la session ha sido cerrada
    /// </summary>
    private void ValidarSesionActiva()
    {

        if (Session[Constantes.sesionUsuario] == null)
        {
            Session[Constantes.sesionUsuario] = null;
            Request.Cookies.Clear();
            Session.Clear();
            Response.Redirect(Utils.RelativeWebRoot + "expiroSession.aspx");
        }

    }
}