using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
//referencia para cambiar de recursos
using System.Globalization;
using System.Threading;
using Intertek.Business.Entities;
using Intertek.Helpers;
using System.Collections.Generic;
//using Intertek.WEB.Code;

namespace Intertek.WEB
{
    public partial class Site : MasterPage
    {
        public string nombreUsuarioLogeado;
        public string absolutePath;
        protected void Page_Load(object sender, EventArgs e)
        {
            absolutePath = Utils.AbsoluteWebRoot.AbsolutePath;
            if (!Page.IsPostBack)
            {
                Usuario objUsuario=(Usuario)Session[Constantes.sesionUsuario];
                ddlIdiomas.Items.FindByValue(Request.Cookies["cookieCultura"].Value).Selected = true;
                lblnombreusuario.Text = objUsuario.USU_Login;                
                
            }
            CargarMenuPrincipal();
        }

        

        protected void ddlIdiomas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlIdiomas.SelectedValue != "")
            {
                //crear la cookie para mantener el idioma seleccionado
                Session["ddlIdiomas"] = ddlIdiomas.SelectedValue;
                HttpCookie cookieCultura = Request.Cookies.Get("cookieCultura");
                cookieCultura.Value = ddlIdiomas.SelectedValue;
                Response.Cookies.Set(cookieCultura);
                
                string urlActual = Request.RawUrl;
                Response.Redirect(urlActual);
               
            }

           
        }

        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            Session[Constantes.sesionUsuario] = null;
            Request.Cookies.Clear();
            Session.Clear();
            
            Response.Redirect(Utils.RelativeWebRoot+"expiroSession.aspx");
            
        }

        private void CargarMenuPrincipal()
        {
             List<RolOpcionSistema> lstOpciones=(List<RolOpcionSistema>)Session[Constantes.opcionesSistema];
             List<OpcionSistema> lstModulos = (List<OpcionSistema>)Session[Constantes.modulosSistema];

             if (Session["ddlIdiomas"].ToString() == "en-US")
             {
                 for (int i = 0; i < lstModulos.Count; i++)
                 {

                     MenuItem item = new MenuItem(lstModulos[i].OSI_Modulo_en_US);
                     mnuPrincipal.Items.Add(item);
                     List<RolOpcionSistema> lstSubOpciones = lstOpciones.FindAll(delegate(RolOpcionSistema aux) { return aux.OpcionSistema.OSI_Modulo_en_US == lstModulos[i].OSI_Modulo_en_US; });
                     for (int f = 0; f < lstSubOpciones.Count; f++)
                     {
                         MenuItem subItem = new MenuItem();
                         subItem.Text = lstSubOpciones[f].OpcionSistema.OSI_Nombre_en_US;
                         subItem.NavigateUrl = lstSubOpciones[f].OpcionSistema.OSI_RutaPagina;
                         mnuPrincipal.Items[i].ChildItems.Add(subItem);
                     }

                 }
             }
             else
             {
                 for (int i = 0; i < lstModulos.Count; i++)
                 {

                     MenuItem item = new MenuItem(lstModulos[i].OSI_Modulo);
                     mnuPrincipal.Items.Add(item);
                     List<RolOpcionSistema> lstSubOpciones = lstOpciones.FindAll(delegate(RolOpcionSistema aux) { return aux.OpcionSistema.OSI_Modulo == lstModulos[i].OSI_Modulo; });
                     for (int f = 0; f < lstSubOpciones.Count; f++)
                     {
                         MenuItem subItem = new MenuItem();
                         subItem.Text = lstSubOpciones[f].OpcionSistema.OSI_Nombre;
                         subItem.NavigateUrl = lstSubOpciones[f].OpcionSistema.OSI_RutaPagina;
                         mnuPrincipal.Items[i].ChildItems.Add(subItem);
                     }

                 }
             }
            
        }

   

       
    }
}
