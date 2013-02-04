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
    public partial class comentario : PaginaBase
    {
        Nota objNota = new Nota();
        string accion = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            txtNombre.Focus();
            accion = Request.QueryString.Get("accion");
            if (!Page.IsPostBack)
            {
                if (Session["ddlIdiomas"].ToString() == "en-US")
                {
                    imgGrabar.Src = Resources.generales.imgGrabarUS;
                }
                if (accion == "M")
                    CargarDatos(Request.QueryString.Get("IDNota"));
            }
        }

        protected void btnGrabar_Click(object sender, EventArgs e)
        {
            GuardarDatosNota();
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("comentarioBuscar.aspx");
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void CargarDatos(string idNota)
        {
            int id = Convert.ToInt32(idNota);
            var entidad = NotaBL.Instancia.ObtenerDatosPorID(id);
            txtNombre.Text = entidad.NOT_Nombre;
            txtDescripcion.Text = entidad.NOT_Descripcion;
            if (Session["ddlIdiomas"].ToString() == "en-US")
            {
                lblTitulo.Text = "Modify Note";
            }
            else
            {
                lblTitulo.Text = "Modificar Nota";
            }
        }

        private void GuardarDatosNota()
        {
            switch (accion)
            {
                case "N":
                    InsertarNota();
                    break;
                case "M":
                    ModificarNota();
                    break;
            }
        }
        private void InsertarNota()
        {
            
                try
                {
                    if (txtNombre.Text != "")
                    {
                        Usuario objusuario = (Usuario)Session[Constantes.sesionUsuario];
                        objNota.NOT_Nombre = txtNombre.Text.Trim();
                        objNota.NOT_Descripcion = txtDescripcion.Text.Trim();
                        objNota.NOT_Estado = Constantes.EstadoActivo;
                        objNota.NOT_UsuarioCreacion = objusuario.IDUsuario.ToString();
                        objNota.NOT_FechaHoraCreacion = DateTime.Now;
                        objNota = NotaBL.Instancia.Insertar(objNota);
                        int idNota = objNota.IDNota;
                        objNota.NOT_Codigo = "NOT" + idNota.ToString().PadLeft(7, '0');
                        NotaBL.Instancia.Actualizar(objNota);
                        LimpiarCampos();
                        ClientScript.RegisterStartupScript(this.GetType(), "Confirmacion", "$(function(){MostrarMensaje('msjRegistroOK');});", true);
                    }
                }
                catch
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Error", "$(function(){MostrarMensaje('msjErrorGrabar');});", true);
                }
            
        }


        private void ModificarNota()
        {
            Usuario objusuario = (Usuario)Session[Constantes.sesionUsuario]; 
            int id = Convert.ToInt32(Request.QueryString.Get("IDNota"));
            var entidad = NotaBL.Instancia.ObtenerDatosPorID(id);
            entidad.NOT_Nombre = txtNombre.Text.Trim();
            entidad.NOT_Descripcion = txtDescripcion.Text.Trim();
            entidad.NOT_UsuarioModificacion = objusuario.IDUsuario.ToString();
            entidad.NOT_FechaHoraModificacion = DateTime.Now;
            try
            {
                NotaBL.Instancia.Actualizar(entidad);
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
            //if (accion == "M")
            //    CargarDatos(Request.QueryString.Get("IDNota"));
            //else
            //{
                txtNombre.Text = " ";
                txtDescripcion.Text = " ";
            //}
        }
    }
}