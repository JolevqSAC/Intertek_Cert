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
    public partial class estado : PaginaBase
    {
        Estado objestado = new Estado();
        string accion = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            txtDescripcion.Focus();
            accion = Request.QueryString.Get("accion");
            if (!Page.IsPostBack)
            {
                if (accion == "M")
                    CargarDatos(Request.QueryString.Get("IDEstado"));
            }
        }

        protected void btnGrabar_Click(object sender, EventArgs e)
        {
            GuardarDatos();
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("estadoBuscar.aspx");
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void CargarDatos(string idestado)
        {
            objestado.IDEstado = Convert.ToInt32(idestado);
            var entidad = EstadoBL.Instancia.ObtenerDatosPorID(objestado);
            txtDescripcion.Text = entidad.EST_Descripcion;
            txtTipo.Text = entidad.EST_Tipo;

        }

        private void GuardarDatos()
        {
            switch (accion)
            {
                case "N":
                    InsertarEstado();
                    break;
                case "M":
                    ModificarEstado();
                    break;
            }
        }
        private void InsertarEstado()
        {                                
                try
                {
                    if (txtDescripcion.Text != "")
                    {
                        Usuario objusuario = (Usuario)Session[Constantes.sesionUsuario];
                        objestado.EST_Descripcion = txtDescripcion.Text.Trim();
                        objestado.EST_Tipo = txtTipo.Text.Trim();
                        objestado.EST_Estado = Constantes.EstadoActivo;
                        objestado.EST_UsuarioCreacion = objusuario.IDUsuario.ToString();
                        objestado.EST_FechaHoraCreacion = DateTime.Now;
                        objestado = EstadoBL.Instancia.Insertar(objestado);
                        LimpiarCampos();
                        ClientScript.RegisterStartupScript(this.GetType(), "Confirmacion", "$(function(){MostrarMensaje('msjRegistroOK');});", true);
                    }
                }
                catch
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Error", "$(function(){MostrarMensaje('msjErrorGrabar');});", true);
                }
            
        }


        private void ModificarEstado()
        {
            Usuario objusuario = (Usuario)Session[Constantes.sesionUsuario];
            objestado.IDEstado = Convert.ToInt32(Request.QueryString.Get("IDEstado"));
            var entidad = EstadoBL.Instancia.ObtenerDatosPorID(objestado);
            entidad.EST_Descripcion = txtDescripcion.Text.Trim();
            entidad.EST_Tipo = txtTipo.Text.Trim();
            entidad.EST_UsuarioModificacion = objusuario.IDUsuario.ToString();
            entidad.EST_FechaHoraModificacion = DateTime.Now;
            try
            {
                EstadoBL.Instancia.Actualizar(entidad);
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
            if (accion == "M")
                CargarDatos(Request.QueryString.Get("IDEstado"));
            else
            {
                txtDescripcion.Text = " ";
                txtTipo.Text = " ";
            }
        }
    }
}