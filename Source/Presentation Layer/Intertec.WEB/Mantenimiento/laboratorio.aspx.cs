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
    public partial class laboratorio : PaginaBase
    {
        Laboratorio objlaboratorio = new Laboratorio();
        string accion = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            txtNombre.Focus();
            accion = Request.QueryString.Get("accion");
            if (!Page.IsPostBack)
            {
                CargarTipoLaboratorio();
                if (Session["ddlIdiomas"].ToString() == "en-US")
                {
                    imgGrabar.Src = Resources.generales.imgGrabarUS;
                }
                if (accion == "M")
                    CargarDatos(Request.QueryString.Get("IDLaboratorio"));
            }
        }

        protected void btnGrabar_Click(object sender, EventArgs e)
        {
            GuardarDatos();
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("laboratorioBuscar.aspx");
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void CargarTipoLaboratorio()
        {
            TipoLaboratorio objtipolaboratorio = new TipoLaboratorio();
            objtipolaboratorio.TLA_Estado = Constantes.EstadoActivo;
            ddlTipoLaboratorio.DataSource = TipoLaboratorioBL.Instancia.ListarPorNombre(objtipolaboratorio);
            ddlTipoLaboratorio.DataValueField = "IDTipoLaboratorio";
            ddlTipoLaboratorio.DataTextField = "TLA_Nombre";
            ddlTipoLaboratorio.DataBind();
            if (Session["ddlIdiomas"].ToString() == "en-US")
            {
                ddlTipoLaboratorio.Items.Insert(0, new ListItem(Resources.generales.textoSelect, "0"));
            }
            else
            {
                ddlTipoLaboratorio.Items.Insert(0, new ListItem(Resources.generales.textoSeleccionar, "0"));
            }
        }
        
        private void CargarDatos(string idlaboratorio)
        {
            objlaboratorio.IDLaboratorio = Convert.ToInt32(idlaboratorio);
            var entidad = LaboratorioBL.Instancia.ObtenerDatosPorID(objlaboratorio.IDLaboratorio);
            txtNombre.Text = entidad.LAB_Nombre;
            ListItem oListItem = ddlTipoLaboratorio.Items.FindByValue(entidad.IDTipoLaboratorio.ToString());
            if (oListItem != null)
            {
                ddlTipoLaboratorio.SelectedValue = entidad.IDTipoLaboratorio.ToString();
            }
            if (Session["ddlIdiomas"].ToString() == "en-US")
            {
                lblTitulo.Text = "Modify Laboratory";
            }
            else
            {
                lblTitulo.Text = "Modificar Laboratorio";
            }
        }
        
        private void GuardarDatos()
        {
            switch (accion)
            {
                case "N":
                    InsertarLaboratorio();
                    break;
                case "M":
                    ModificarLaboratorio();
                    break;
            }
        }
        private void InsertarLaboratorio()
        {
            try
            {
                if (txtNombre.Text != "")
                {
                    Usuario objusuario = (Usuario)Session[Constantes.sesionUsuario];
                    objlaboratorio.LAB_Nombre = txtNombre.Text.Trim();
                    objlaboratorio.IDTipoLaboratorio = ddlTipoLaboratorio.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlTipoLaboratorio.SelectedValue); 
                    objlaboratorio.LAB_Estado = Constantes.EstadoActivo;
                    objlaboratorio.LAB_UsuarioCreacion = objusuario.IDUsuario.ToString();
                    objlaboratorio.LAB_FechaHoraCreacion = DateTime.Now;
                    objlaboratorio = LaboratorioBL.Instancia.Insertar(objlaboratorio);
                    LimpiarCampos();
                    ClientScript.RegisterStartupScript(this.GetType(), "Confirmacion", "$(function(){MostrarMensaje('msjRegistroOK');});", true);
                }
            }
            catch
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Error", "$(function(){MostrarMensaje('msjErrorGrabar');});", true);
            }

        }


        private void ModificarLaboratorio()
        {
            try
            {
                Usuario objusuario = (Usuario)Session[Constantes.sesionUsuario];
                objlaboratorio.IDLaboratorio = Convert.ToInt32(Request.QueryString.Get("IDLaboratorio"));
                var entidadlab = LaboratorioBL.Instancia.ObtenerDatosPorID(objlaboratorio.IDLaboratorio);
                entidadlab.LAB_Nombre = txtNombre.Text.Trim();
                entidadlab.IDTipoLaboratorio = ddlTipoLaboratorio.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlTipoLaboratorio.SelectedValue); 
                entidadlab.LAB_UsuarioModificacion = objusuario.IDUsuario.ToString();
                entidadlab.LAB_FechaHoraModificacion = DateTime.Now;
                LaboratorioBL.Instancia.Actualizar(entidadlab);
                LimpiarCampos();
                ClientScript.RegisterStartupScript(this.GetType(), "Confirmacion", "$(function(){MostrarMensaje('msjRegistroOK');});", true);
            }
            catch
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Error", "$(function(){MostrarMensaje('msjErrorGrabar');});", true);
            }

        }


        private void LimpiarCampos()
        {            
                txtNombre.Text = " ";
                ddlTipoLaboratorio.SelectedIndex = -1;
            
        }
    }
}