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
    public partial class area : PaginaBase
    {
        Area objarea = new Area();
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
                    CargarDatos(Request.QueryString.Get("idarea"));
            }
        }

        private void CargarDatos(string idarea)
        {
            objarea.IDArea = Convert.ToInt32(idarea);
            var entidad = AreaBL.Instancia.ObtenerAreaByID(objarea.IDArea);
            txtNombre.Text = entidad.ARE_Nombre;
            txtDescripcion.Text = entidad.ARE_Descripcion;
            if (Session["ddlIdiomas"].ToString() == "en-US")
            {
                lblTitulo.Text = "Modify Area";
            }
            else
            {
                lblTitulo.Text = "Modificar Área";
            }
        }
        
        protected void btnGrabar_Click(object sender, EventArgs e)
        {
            GuardarDatos();
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("areaBuscar.aspx");
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
                    objarea.ARE_Nombre = txtNombre.Text.Trim();
                    objarea.ARE_Descripcion = txtDescripcion.Text.Trim();
                    objarea.ARE_Estado = Constantes.EstadoActivo;
                    objarea.ARE_UsuarioCreaccion = objusuario.IDUsuario.ToString();
                    objarea.ARE_FechaHoraCreacion = DateTime.Now;
                    objarea = AreaBL.Instancia.Insertar(objarea);
                    int idarea = objarea.IDArea;
                    objarea.ARE_Codigo = "ARE" + idarea.ToString().PadLeft(7, '0');
                    AreaBL.Instancia.Actualizar(objarea);
                    ClientScript.RegisterStartupScript(this.GetType(), "Confirmacion", "$(function(){MostrarMensaje('msjRegistroOK');});", true);
                   
                }
                //else
                //{
                //    ClientScript.RegisterStartupScript(this.GetType(), "Alerta", "$(function(){MostrarMensaje('msjCamposObligatorios');});", true);
                //}
            }
            catch
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Error", "$(function(){MostrarMensaje('msjErrorGrabar');});", true);
            }

        }


        private void Modificar()
        {                       
            
            try
            {
                if (txtNombre.Text != "")
                {
                    Usuario objusuario = (Usuario)Session[Constantes.sesionUsuario];
                    objarea.IDArea = Convert.ToInt32(Request.QueryString.Get("idarea"));
                    var entidad = AreaBL.Instancia.ObtenerAreaByID(objarea.IDArea);
                    entidad.ARE_Nombre = txtNombre.Text;
                    entidad.ARE_Descripcion = txtDescripcion.Text;
                    entidad.ARE_UsuarioModificacion = objusuario.IDUsuario.ToString();
                    entidad.ARE_FechaHoraModificacion = DateTime.Now;
                    AreaBL.Instancia.Actualizar(entidad);
                    ClientScript.RegisterStartupScript(this.GetType(), "Confirmacion", "$(function(){MostrarMensaje('msjRegistroOK');});", true);
                  
                }

                //else {
                //    ClientScript.RegisterStartupScript(this.GetType(), "Alerta", "$(function(){MostrarMensaje('msjCamposObligatorios');});", true);
                //}
            }
            catch
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Error", "$(function(){MostrarMensaje('msjErrorGrabar');});", true);
            }
        }


        private void LimpiarCampos()
        {
                this.txtNombre.Text = " ";
                this.txtDescripcion.Text = " ";
          
        }
    }
}