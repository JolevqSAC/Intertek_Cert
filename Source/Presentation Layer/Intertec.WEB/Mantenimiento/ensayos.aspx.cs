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
    public partial class ensayos : PaginaBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CargarDropDownList();
                if (!EsNuevoRegistro())
                {
                    ObtenerDatos(Convert.ToInt32(Request["idEnsayo"].ToString()));
                }
            }
        }

        protected void btnGrabar_Click(object sender, EventArgs e)
        {
            Usuario objLogin = (Usuario)Session[Constantes.sesionUsuario];
            bool graboOK = false;
            lblMensaje.Text = "";
            string mensajeError = "";

            if (txtEnsayo.Text != "" && ddlLaboratorio.SelectedValue != "0")
            {
                if (!EsNuevoRegistro())
                {
                    //actualizar
                    Ensayo objEnsayo = EnsayoBL.Instancia.ObtenerEnsayoById(Convert.ToInt32(Request["idEnsayo"].ToString()));

                    SetearValores(ref objEnsayo);
                    objEnsayo.ENS_UsuarioModificacion = objLogin.IDUsuario.ToString();
                    objEnsayo.ENS_FechaHoraModificacion = DateTime.Now;

                    try
                    {
                        objEnsayo = EnsayoBL.Instancia.Actualizar(objEnsayo);
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
                    Ensayo objEnsayo = new Ensayo();
                    SetearValores(ref objEnsayo);
                    objEnsayo.ENS_UsuarioCreacion = objLogin.IDUsuario.ToString();
                    objEnsayo.ENS_FechaHoraCreacion = DateTime.Now;
                    try
                    {
                        objEnsayo = EnsayoBL.Instancia.Insertar(objEnsayo);
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
            Response.Redirect("ensayosBuscar.aspx");
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            if (!EsNuevoRegistro())
            {
                Response.Redirect("ensayos.aspx?idEnsayo=" + Request["idEnsayo"].ToString());
            }
            else
            {
                Response.Redirect("ensayos.aspx");
            }
        }

        private void CargarDropDownList()
        {
            ddlLaboratorio.DataSource = LaboratorioBL.Instancia.ListarTodosLaboratorios();
            ddlLaboratorio.DataValueField = "IDLaboratorio";
            ddlLaboratorio.DataTextField = "LAB_Nombre";
            ddlLaboratorio.DataBind();
            ddlLaboratorio.Items.Insert(0, new ListItem(Resources.generales.textoSeleccionar, "0"));
        }

        private void ObtenerDatos(int idEnsayo)
        {
            Ensayo objEnsayo = EnsayoBL.Instancia.ObtenerEnsayoById(idEnsayo);
            if (objEnsayo != null)
            {
                txtEnsayo.Text = objEnsayo.ENS_Nombre;
                txtDescripcion.Text = objEnsayo.ENS_Descripcion;
                ListItem oListItem = ddlLaboratorio.Items.FindByValue(objEnsayo.IDLaboratorio.ToString());
                if (oListItem != null)
                {
                    ddlLaboratorio.SelectedValue = objEnsayo.IDLaboratorio.ToString();
                }
               
            }

        }

        private void SetearValores(ref Ensayo objEnsayo)
        {
            objEnsayo.ENS_Nombre = txtEnsayo.Text;
            objEnsayo.ENS_Descripcion = txtDescripcion.Text;
            objEnsayo.IDLaboratorio = ddlLaboratorio.SelectedValue != "0" ? Convert.ToInt32(ddlLaboratorio.SelectedValue) : (int?)null;
            objEnsayo.ENS_Estado = Constantes.EstadoActivo;
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
            if (Request["idEnsayo"] != null)
            {
                return false;
            }

            return esNuevo;
        }
    }
}