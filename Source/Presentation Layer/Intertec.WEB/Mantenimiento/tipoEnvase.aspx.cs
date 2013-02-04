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
    public partial class tipoEnvase : PaginaBase
    {
        TipoEnvase objtipoenvase = new TipoEnvase();        
        string accion = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            txtDescripcion.Focus();
            accion = Request.QueryString.Get("accion");
            if (!Page.IsPostBack)
            {
                if (accion == "M")
                    CargarDatos(Request.QueryString.Get("IDTipoEnvase"));
            }
        }

        protected void btnGrabar_Click(object sender, EventArgs e)
        {
            GuardarDatos();
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("tipoEnvaseBuscar.aspx");
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void CargarDatos(string idtipoenvase)
        {
            objtipoenvase.IDTiposEnvase = Convert.ToInt32(idtipoenvase);
            var entidad = TipoEnvaseBL.Instancia.ObtenerDatosPorID(objtipoenvase);
            txtDescripcion.Text = entidad.TIE_Descripcion;           

        }

        private void GuardarDatos()
        {
            switch (accion)
            {
                case "N":
                    InsertarTipoEnvase();
                    break;
                case "M":
                    ModificarTipoEnvase();
                    break;
            }
        }
        private void InsertarTipoEnvase()
        {            
                try
                {
                    if (txtDescripcion.Text != "")
                    {
                        Usuario objusuario = (Usuario)Session[Constantes.sesionUsuario];
                        objtipoenvase.TIE_Descripcion = txtDescripcion.Text.Trim();
                        objtipoenvase.TIE_Estado = Constantes.EstadoActivo;
                        objtipoenvase.TIE_UsuarioCreacion = objusuario.IDUsuario.ToString();
                        objtipoenvase.TIE_FechaHoraCreacion = DateTime.Now;
                        objtipoenvase = TipoEnvaseBL.Instancia.Insertar(objtipoenvase);
                        LimpiarCampos();
                        ClientScript.RegisterStartupScript(this.GetType(), "Confirmacion", "$(function(){MostrarMensaje('msjRegistroOK');});", true);
                    }
                }
                catch
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Error", "$(function(){MostrarMensaje('msjErrorGrabar');});", true);
                }
            
        }


        private void ModificarTipoEnvase()
        {            
                try
                {
                    Usuario objusuario = (Usuario)Session[Constantes.sesionUsuario];
                    objtipoenvase.IDTiposEnvase = Convert.ToInt32(Request.QueryString.Get("IDTipoEnvase"));
                    var entidadtipoenvase = TipoEnvaseBL.Instancia.ObtenerDatosPorID(objtipoenvase);
                    entidadtipoenvase.TIE_Descripcion = txtDescripcion.Text.Trim();
                    entidadtipoenvase.TIE_UsuarioModificacion = objusuario.IDUsuario.ToString();
                    entidadtipoenvase.TIE_FechaHoraModificacion = DateTime.Now;
                    TipoEnvaseBL.Instancia.Actualizar(entidadtipoenvase);
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
                CargarDatos(Request.QueryString.Get("IDTipoEnvase"));
            else
                txtDescripcion.Text = " ";
        }
    }
}