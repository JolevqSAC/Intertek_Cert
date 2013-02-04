using System;
using System.Collections;
using System.Collections.Generic;
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
//referencias
using Intertek.Business.Entities;
using Intertek.Business.Logic;
using Intertek.Helpers;

namespace Intertek.WEB
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            if (txtUsuario.Text != "")
            {
                ValidarUsuario();
            }
           
               
        }
        /// <summary>
        /// Valida la autenticidad del usuario
        /// </summary>
        private void ValidarUsuario()
        {
            Usuario objUsuario = new Usuario();
            objUsuario.USU_Login = txtUsuario.Text;
            objUsuario.USU_Clave =Utils.Encriptar(txtPassword.Text);
            objUsuario.USU_Estado = Constantes.EstadoActivo;

            IList<Usuario> lstUsuario= UsuarioBL.Instancia.obtenerDatos(objUsuario);

            if (lstUsuario != null && lstUsuario.Count > 0)
            {
                FormsAuthentication.Initialize();

                //FormsAuthentication.FormsCookieName
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, lstUsuario[0].USU_Login, DateTime.Now, DateTime.Now.AddMinutes(50), true, "datos de usuario", FormsAuthentication.FormsCookiePath);
                // Encrypt the cookie using the machine key for secure transport
                string hash = FormsAuthentication.Encrypt(ticket);
                HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, hash);
                if (ticket.IsPersistent)
                { cookie.Expires = ticket.Expiration; }

                // Add the cookie to the list for outgoing response
                Response.Cookies.Add(cookie);

                Session["ddlIdiomas"] = ConfigurationManager.AppSettings["CulturaDefecto"].ToString();
                HttpCookie cookieCultura = new HttpCookie("cookieCultura", ConfigurationManager.AppSettings["CulturaDefecto"].ToString());
                Response.Cookies.Add(cookieCultura);
                FormsAuthentication.SetAuthCookie(lstUsuario[0].USU_Login, false);

                Session[Constantes.sesionUsuario] = lstUsuario[0];
                
                RolOpcionSistema objOpciones=new RolOpcionSistema();
                objOpciones.IDRol = lstUsuario[0].IDRol;
                //IList<RolOpcionSistema> lstOpciones2 = RolOpcionSistemaBL.Instancia.ObtenerOpciones_ByRol2(objOpciones);
                //IEnumerable<string> lstModulos = lstOpciones.Select(aux=> aux.OpcionSistema.OSI_Modulo).Distinct();
                List<RolOpcionSistema> lstOpciones = RolOpcionSistemaBL.Instancia.ObtenerOpcionesSistema(objOpciones);

                //var varModulos = lstOpciones.GroupBy(modulo => new {modulo.OpcionSistema.OSI_Modulo, modulo.OpcionSistema.OSI_Modulo_en_US}).OrderBy(modulo => modulo.Key);
                var varModulos = lstOpciones.GroupBy(modulo => new { modulo.OpcionSistema.OSI_Modulo, modulo.OpcionSistema.OSI_Modulo_en_US }).ToList();
                List<OpcionSistema> lstModulos = new List<OpcionSistema>();
                foreach (var obj in varModulos)
                {
                    OpcionSistema objModulo = new OpcionSistema();
                    objModulo.OSI_Modulo = obj.Key.OSI_Modulo;
                    objModulo.OSI_Modulo_en_US = obj.Key.OSI_Modulo_en_US;
                    
                    lstModulos.Add(objModulo);
                }

                Session[Constantes.opcionesSistema] = lstOpciones;
                Session[Constantes.modulosSistema] = lstModulos;
                Response.Redirect("inicio.aspx");
            }
            else 
            {
                ClientScript.RegisterStartupScript(this.GetType(), "miscriptError", "$(function(){MostrarMensaje('msjError');});", true);
            }


            
            
        }


    }
}
