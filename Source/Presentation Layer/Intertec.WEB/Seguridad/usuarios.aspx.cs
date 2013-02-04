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
    public partial class usuarios : PaginaBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CargarDropDownList();
                 if (!EsNuevoRegistro())
                {
                    ObtenerDatos(Convert.ToInt32(Request["idUsuario"].ToString()));
                }
                
            }
        }

        protected void btnGrabar_Click(object sender, EventArgs e)
        {
             Usuario objLogin = (Usuario)Session[Constantes.sesionUsuario];
            bool graboOK = false;
            lblMensaje.Text = "";
            string mensajeError = "";

            if (txtUsuario.Text != "" && txtPassword.Text != "" && ddlRol.SelectedValue != "0" && ddlPersonal.SelectedValue != "0")
            {
                if (!EsNuevoRegistro())
                {
                    //actualizar
                    Usuario objUsuario = UsuarioBL.Instancia.ObtenerUsuarioById(Convert.ToInt32(Request["idUsuario"].ToString()));

                    SetearValores(ref objUsuario);
                    objUsuario.USU_UsuarioModificacion = objLogin.IDUsuario.ToString();
                    objUsuario.USU_FechaHoraModificacion = DateTime.Now;

                    try
                    {
                        objUsuario = UsuarioBL.Instancia.Actualizar(objUsuario);
                        graboOK = true;
                        LimpiarFormulario();
                    }
                    catch (Exception ex)
                    {
                        graboOK = false;
                        mensajeError = "Actualizar" + ex.Message;
                    }
                }
                else
                {
                    //insertar
                    Usuario objUsuario = new Usuario();
                    SetearValores(ref objUsuario);
                    objUsuario.USU_UsuarioCreacion = objLogin.IDUsuario.ToString();
                    objUsuario.USU_FechaHoraCreacion = DateTime.Now;
                    try
                    {
                        objUsuario = UsuarioBL.Instancia.Insertar(objUsuario);
                        graboOK = true;
                        LimpiarFormulario();
                    }
                    catch (Exception ex)
                    {
                        graboOK = false;
                        mensajeError = "Insertar" + ex.Message;
                    }
                }
            }

            if (graboOK)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "miscriptError", "$(function(){MostrarMensaje('msjSatisfactorio');});", true);
            }
            else
            {
                lblMensaje.Text = mensajeError;
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            
            Response.Redirect("usuariosBuscar.aspx");
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
          
                Response.Redirect("usuarios.aspx");            
        }

        private void CargarDropDownList()
        {

            ddlRol.DataSource = RolBL.Instancia.ListarRolTodos();
            ddlRol.DataValueField = "IDRol";
            ddlRol.DataTextField = "ROL_Nombre";
            ddlRol.DataBind();
            ddlRol.Items.Insert(0, new ListItem(Resources.generales.textoSeleccionar, "0"));

            IList<Trabajador> listPersonal = PersonalBL.Instancia.ListarPersonalTodos();
            var datos = (from p in listPersonal
                         select new
                         {
                             Nombres = p.PER_Apellidos.Trim() + " " + p.PER_Nombres.Trim(),
                             p.IDPersonal
                         }).ToList();

            ddlPersonal.DataSource = datos;
            ddlPersonal.DataValueField = "IDPersonal";
            ddlPersonal.DataTextField = "Nombres";
            ddlPersonal.DataBind();
            ddlPersonal.Items.Insert(0, new ListItem(Resources.generales.textoSeleccionar, "0"));

         

        }

        private void ObtenerDatos(int idUsuario)
        {
            Usuario objUsuario = UsuarioBL.Instancia.ObtenerUsuarioById(idUsuario);
            if (objUsuario != null)
            {
                txtUsuario.Text=objUsuario.USU_Login;
                txtPassword.Text = objUsuario.USU_Clave;
                rbIndicadorSignatario.SelectedValue = objUsuario.USU_IndicadorSignatario;
                ListItem oListItem = ddlRol.Items.FindByValue(objUsuario.IDRol.ToString());
                if (oListItem != null)
                {
                    ddlRol.SelectedValue = objUsuario.IDRol.ToString();
                }
                oListItem = ddlPersonal.Items.FindByValue(objUsuario.IDPersonal.ToString());
                if (oListItem != null)
                {
                    ddlPersonal.SelectedValue = objUsuario.IDPersonal.ToString();
                }              

            }

            if (Session["ddlIdiomas"].ToString() == "en-US")
            {
                lblTitulo.Text = "Modify User";
            }
            else
            {
                lblTitulo.Text = "Modificar Usuario";
            }

            
        }

        private void SetearValores(ref Usuario objUsuario)
        {
            objUsuario.USU_Login = txtUsuario.Text;
            objUsuario.USU_Clave = Utils.Encriptar(txtPassword.Text);
            objUsuario.IDRol = ddlRol.SelectedValue != "0" ? Convert.ToInt32(ddlRol.SelectedValue) : (int?)null;
            objUsuario.IDPersonal = ddlPersonal.SelectedValue != "0" ? Convert.ToInt32(ddlPersonal.SelectedValue) : (int?)null;
            objUsuario.USU_Estado = Constantes.EstadoActivo;
            objUsuario.USU_IndicadorSignatario = rbIndicadorSignatario.SelectedValue;

        }

        private void LimpiarFormulario()
        {
            ContentPlaceHolder mpContentPlaceHolder;
            mpContentPlaceHolder = (ContentPlaceHolder)Master.FindControl("MainContent");
            if (mpContentPlaceHolder != null)
            {
                foreach (var obj in mpContentPlaceHolder.Controls.OfType<TextBox>())
                {
                    obj.Text = "";
                }

                foreach (var obj in mpContentPlaceHolder.Controls.OfType<DropDownList>())
                {
                    obj.SelectedIndex = -1;
                }
            }
        }
        private bool EsNuevoRegistro()
        {
            bool esNuevo = true;
            if (Request["idUsuario"] != null)
            {
                return false;
            }

            return esNuevo;
        }
    }
}