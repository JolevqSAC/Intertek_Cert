using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//agregar referencias
using Intertek.Business.Entities;
using Intertek.Business.Logic;
using Intertek.Helpers;

namespace Intertek.WEB.Seguridad
{
    public partial class actualizarDatos : PaginaBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Usuario objLogin = (Usuario)Session[Constantes.sesionUsuario];
                ObtenerDatos(objLogin.IDUsuario);
                if (Session["ddlIdiomas"].ToString() == "en-US")
                {
                    imgGrabar.Src = Resources.generales.imgGrabarUS;
                }
            }
        }

        private void ObtenerDatos(int idUsuario)
        {
            Usuario objUsuario = UsuarioBL.Instancia.ObtenerUsuarioById(idUsuario);
            if (objUsuario != null)
            {
                txtUsuario.Text = objUsuario.USU_Login;
                //  txtPassword.Text = objUsuario.USU_Clave;               
            }

        }

        protected void btnGrabar_Click(object sender, EventArgs e)
        {
            bool graboOK = false;
            Usuario objLogin = (Usuario)Session[Constantes.sesionUsuario];
            Usuario objUsuario = UsuarioBL.Instancia.ObtenerUsuarioById(objLogin.IDUsuario);
            objUsuario.USU_Login = txtUsuario.Text;
            objUsuario.USU_Clave = Utils.Encriptar(txtPassword.Text);
            objUsuario.USU_UsuarioModificacion = objLogin.IDUsuario.ToString();
            objUsuario.USU_FechaHoraModificacion = DateTime.Now;
            objUsuario.USU_Estado = Constantes.EstadoActivo;

            try
            {
                objUsuario = UsuarioBL.Instancia.Actualizar(objUsuario);
                graboOK = true;
            }
            catch (Exception ex)
            {
                graboOK = false;
            }

            if (graboOK)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "miscriptError", "$(function(){MostrarMensaje('msjSatisfactorio');});", true);
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "miscriptError", "$(function(){MostrarMensaje('msjErrorGrabar');});", true);
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("../inicio.aspx");
        }
    }
}