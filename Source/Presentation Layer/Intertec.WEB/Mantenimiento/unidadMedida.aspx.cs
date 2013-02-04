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
    public partial class unidadMedida : PaginaBase
    {
        UnidadMedida objUnidadMedida = new UnidadMedida();
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
                if(accion == "M")
                    CargarDatos(Request.QueryString.Get("IDUnidadMedida"));
            }
        }

        protected void btnGrabar_Click(object sender, EventArgs e)
        {
            GuardarDatosUnidadMedida();
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("unidadMedidaBuscar.aspx");
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void CargarDatos(string idunidadmedida)
        {
            int id = Convert.ToInt32(idunidadmedida);
            var entidad = UnidadMedidaBL.Instancia.ObtenerDatosPorID(id);
            txtNombre.Text = entidad.UNM_Nombre;
            txtAbreviatura.Text = entidad.UNM_NombreCorto;
            if (Session["ddlIdiomas"].ToString() == "en-US")
            {
                lblTitulo.Text = "Modify Measure Unit";
            }
            else
            {
                lblTitulo.Text = "Modificar Unidad de Medida";
            }
            
        }

        private void GuardarDatosUnidadMedida()
        {
            switch (accion)
            {
                case "N":
                    InsertarUnidadMedida();
                    break;
                case "M":
                    ModificarUnidadMedida();
                    break;
            }
        }
        private void InsertarUnidadMedida()
        {
            
                try
                {
                    if (txtNombre.Text != "")
                    {
                        Usuario objusuario = (Usuario)Session[Constantes.sesionUsuario];
                        objUnidadMedida.UNM_Nombre = txtNombre.Text.Trim();
                        objUnidadMedida.UNM_NombreCorto = txtAbreviatura.Text.Trim();
                        objUnidadMedida.UNM_Estado = Constantes.EstadoActivo;
                        objUnidadMedida.UNM_UsuarioCreacion = objusuario.IDUsuario.ToString();
                        objUnidadMedida.UNM_FechaHoraCreacion = DateTime.Now;
                        objUnidadMedida = UnidadMedidaBL.Instancia.Insertar(objUnidadMedida);
                        int idunidad = objUnidadMedida.IDUnidadMedida;
                        objUnidadMedida.UNM_Codigo = "UNM" + idunidad.ToString().PadLeft(7, '0');
                        UnidadMedidaBL.Instancia.Actualizar(objUnidadMedida);
                        LimpiarCampos();
                        ClientScript.RegisterStartupScript(this.GetType(), "Confirmacion", "$(function(){MostrarMensaje('msjRegistroOK');});", true);
                    }
                }
                catch
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Error", "$(function(){MostrarMensaje('msjErrorGrabar');});", true);
                }
            
        }


        private void ModificarUnidadMedida()
        {
            Usuario objusuario = (Usuario)Session[Constantes.sesionUsuario];
            objUnidadMedida.IDUnidadMedida = Convert.ToInt32(Request.QueryString.Get("IDUnidadMedida"));
            var entidad = UnidadMedidaBL.Instancia.ObtenerDatosPorID(objUnidadMedida.IDUnidadMedida);
            entidad.UNM_Nombre = txtNombre.Text.Trim();
            entidad.UNM_NombreCorto = txtAbreviatura.Text.Trim();
            entidad.UNM_UsuarioModificacion = objusuario.IDUsuario.ToString();
            entidad.UNM_FechaHoraModificacion = DateTime.Now;
            try
            {
                UnidadMedidaBL.Instancia.Actualizar(entidad);
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
                txtAbreviatura.Text = " ";
           
        }
    }
}