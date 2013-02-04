using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//referencia a la capa logica
using Intertek.Business.Entities;
using Intertek.Business.Logic;
using Intertek.Helpers;

namespace Intertek.WEB.Mantenimiento
{
    public partial class servicio : PaginaBase
    {

        Servicio objservicio = new Servicio();
        string accion = "";
        
        protected void Page_Load(object sender, EventArgs e)
        {
            accion = Request.QueryString.Get("accion");
            if (!Page.IsPostBack)
            {
                if (Session["ddlIdiomas"].ToString() == "en-US")
                {
                    imgGrabar.Src = Resources.generales.imgGrabarUS;
                }
                if (accion == "M")
                    CargarDatos(Request.QueryString.Get("idservicio"));
            }
        }

        private void CargarDatos(string idservicio)
        {
            objservicio.IDServicio = Convert.ToInt32(idservicio);
            var entidad = ServicioBL.Instancia.ObtenerServicioByID(objservicio.IDServicio);
            txtNombre.Text = entidad.SER_Nombre;
            txtingles.Text = entidad.SER_NombreIngles;
            txtDescripcion.Text = entidad.SER_Descripcion;
            if (Session["ddlIdiomas"].ToString() == "en-US")
            {
                lblTitulo.Text = "Modify Service";
            }
            else
            {
                lblTitulo.Text = "Modificar Servicio";
            }
        }

        protected void btnGrabar_Click(object sender, EventArgs e)
        {
            GuardarDatos();
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("servicioBuscar.aspx");
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void GuardarDatos()
        {
            switch (accion)
            {
                case "N":
                    Insertar();
                    break;
                case "M":
                    Modificar();
                    break;
            }
        }

        private void Insertar()
        {
            try
            {
                if (txtNombre.Text != "")
                {
                    Usuario objusuario = (Usuario)Session[Constantes.sesionUsuario];
                    objservicio.SER_Nombre = txtNombre.Text.Trim();
                    objservicio.SER_NombreIngles = txtingles.Text.Trim();
                    objservicio.SER_Descripcion = txtDescripcion.Text.Trim();
                    objservicio.SER_Estado = Constantes.EstadoActivo;
                    objservicio.SER_UsuarioCreacion = objusuario.IDUsuario.ToString();
                    objservicio.SER_FechaHoraCreacion = DateTime.Now;
                    objservicio = ServicioBL.Instancia.Insertar(objservicio);
                    int idcategoria = objservicio.IDServicio;
                    objservicio.SER_Codigo = "SER" + idcategoria.ToString().PadLeft(7, '0');
                    ServicioBL.Instancia.Actualizar(objservicio);
                    LimpiarCampos();
                    ClientScript.RegisterStartupScript(this.GetType(), "Confirmacion", "$(function(){MostrarMensaje('msjRegistroOK');});", true);
                }
            }
            catch
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Error", "$(function(){MostrarMensaje('msjErrorGrabar');});", true);
            }

        }


        private void Modificar()
        {
            Usuario objusuario = (Usuario)Session[Constantes.sesionUsuario];
            objservicio.IDServicio = Convert.ToInt32(Request.QueryString.Get("idservicio"));
            var entidad = ServicioBL.Instancia.ObtenerServicioByID(objservicio.IDServicio);
            entidad.SER_Nombre = txtNombre.Text;
            entidad.SER_NombreIngles = txtingles.Text;
            entidad.SER_Descripcion = txtDescripcion.Text;
            entidad.SER_UsuarioModificacion = objusuario.IDUsuario.ToString();
            entidad.SER_FechaHoraModificacion = DateTime.Now;
            try
            {
                ServicioBL.Instancia.Actualizar(entidad);
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
            this.txtNombre.Text = " ";
            this.txtingles.Text = " ";
            this.txtDescripcion.Text = " ";
        }
    }
}