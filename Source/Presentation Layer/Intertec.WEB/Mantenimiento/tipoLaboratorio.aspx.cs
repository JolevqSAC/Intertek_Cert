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
    public partial class tipoLaboratorio : PaginaBase
    {
        TipoLaboratorio objtipolaboratorio = new TipoLaboratorio();
        string accion = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            txtNombre.Focus();
            accion = Request.QueryString.Get("accion");
            if (!Page.IsPostBack)
            {
                if (accion == "M")
                    CargarDatos(Request.QueryString.Get("IDTipoLaboratorio"));
            }
        }

        protected void btnGrabar_Click(object sender, EventArgs e)
        {
            GuardarDatos();
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("tipoLaboratorioBuscar.aspx");
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void CargarDatos(string idtipolaboratorio)
        {
            objtipolaboratorio.IDTipoLaboratorio = Convert.ToInt32(idtipolaboratorio);
            var entidad = TipoLaboratorioBL.Instancia.ObtenerDatosPorID(objtipolaboratorio);
            txtNombre.Text = entidad.TLA_Nombre;

        }

        private void GuardarDatos()
        {
            switch (accion)
            {
                case "N":
                    InsertarTipoLaboratorio();
                    break;
                case "M":
                    ModificarTipoLaboratorio();
                    break;
            }
        }
        private void InsertarTipoLaboratorio()
        {
            try
            {
                if (txtNombre.Text != "")
                {
                    Usuario objusuario = (Usuario)Session[Constantes.sesionUsuario];
                    objtipolaboratorio.TLA_Nombre = txtNombre.Text.Trim();
                    objtipolaboratorio.TLA_Estado = Constantes.EstadoActivo;
                    objtipolaboratorio.TLA_UsuarioCreacion = objusuario.IDUsuario.ToString();
                    objtipolaboratorio.TLA_FechaHoraCreacion = DateTime.Now;
                    objtipolaboratorio = TipoLaboratorioBL.Instancia.Insertar(objtipolaboratorio);
                    LimpiarCampos();
                    ClientScript.RegisterStartupScript(this.GetType(), "Confirmacion", "$(function(){MostrarMensaje('msjRegistroOK');});", true);
                }
            }
            catch
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Error", "$(function(){MostrarMensaje('msjErrorGrabar');});", true);
            }

        }


        private void ModificarTipoLaboratorio()
        {
            try
            {
                Usuario objusuario = (Usuario)Session[Constantes.sesionUsuario];
                objtipolaboratorio.IDTipoLaboratorio = Convert.ToInt32(Request.QueryString.Get("IDTipoLaboratorio"));
                var entidadtipolab = TipoLaboratorioBL.Instancia.ObtenerDatosPorID(objtipolaboratorio);
                entidadtipolab.TLA_Nombre = txtNombre.Text.Trim();
                entidadtipolab.TLA_UsuarioModificacion = objusuario.IDUsuario.ToString();
                entidadtipolab.TLA_FechaHoraModificacion = DateTime.Now;
                TipoLaboratorioBL.Instancia.Actualizar(entidadtipolab);
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
                CargarDatos(Request.QueryString.Get("IDTipoLaboratorio"));
            else
                txtNombre.Text = " ";
        }
    }
}