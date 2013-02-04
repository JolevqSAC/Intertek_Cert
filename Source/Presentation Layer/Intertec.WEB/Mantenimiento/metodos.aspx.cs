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


namespace Intertek.WEB.Mantenimiento
{
    public partial class metodos : PaginaBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session["ddlIdiomas"].ToString() == "en-US")
                {
                    imgGrabar.Src = Resources.generales.imgGrabarUS;
                }
                if (!EsNuevoRegistro())
                {
                    ObtenerDatos(Convert.ToInt32(Request["idMetodo"].ToString()));
                }
            }
        }

        protected void btnGrabar_Click(object sender, EventArgs e)
        {
            Usuario objLogin = (Usuario)Session[Constantes.sesionUsuario];
            bool graboOK = false;
            lblMensaje.Text = "";
            string mensajeError = "";

            if (txtNombre.Text != "" )
            {
                if (!EsNuevoRegistro())
                {
                    //actualizar
                    Metodo objMetodo = MetodoBL.Instancia.ObtenerMetodoById(Convert.ToInt32(Request["idMetodo"].ToString()));

                    SetearValores(ref objMetodo);
                    objMetodo.MET_UsuarioModificacion = objLogin.IDUsuario.ToString();
                    objMetodo.MET_FechaHoraModificacion = DateTime.Now;

                    try
                    {
                        objMetodo = MetodoBL.Instancia.Actualizar(objMetodo);
                        LimpiarFormulario();
                        graboOK = true;
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
                    Metodo objMetodo = new Metodo();
                    SetearValores(ref objMetodo);
                    objMetodo.MET_UsuarioCreacion = objLogin.IDUsuario.ToString();
                    objMetodo.MET_FechaHoraCreacion = DateTime.Now;
                    try
                    {
                        objMetodo = MetodoBL.Instancia.Insertar(objMetodo);
                        int idcargo = objMetodo.IDMetodo;
                        objMetodo.MET_Codigo = "MET" + idcargo.ToString().PadLeft(7, '0');
                        MetodoBL.Instancia.Actualizar(objMetodo);
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
                //lblMensaje.Text = mensajeError;
                ClientScript.RegisterStartupScript(this.GetType(), "miscriptError", "$(function(){MostrarMensaje(' msjErrorGrabar');});", true);
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("metodosBuscar.aspx");
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtDescripcion.Text = "";
            txtingles.Text = "";
            txtNombre.Text = "";            
        }
        private void ObtenerDatos(int idMetodo)
        {
            Metodo objMetodo = MetodoBL.Instancia.ObtenerMetodoById(idMetodo);
            if (objMetodo != null)
            {
                txtNombre.Text = objMetodo.MET_Nombre;
                txtingles.Text = objMetodo.MET_NombreIngles;
                txtDescripcion.Text = objMetodo.MET_Descripcion;
            }
            if (Session["ddlIdiomas"].ToString() == "en-US")
            {
                lblTitulo.Text = "Modify Method";
            }
            else
            {
                lblTitulo.Text = "Modificar Método";
            }
        }

        private void SetearValores(ref Metodo objMetodo)
        {
            objMetodo.MET_Nombre = txtNombre.Text;
            objMetodo.MET_NombreIngles = txtingles.Text;
            objMetodo.MET_Descripcion = txtDescripcion.Text;           
            objMetodo.MET_Estado = Constantes.EstadoActivo;
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
            }
        }
        private bool EsNuevoRegistro()
        {
            bool esNuevo = true;
            if (Request["idMetodo"] != null)
            {
                return false;
            }

            return esNuevo;
        }
    }
}