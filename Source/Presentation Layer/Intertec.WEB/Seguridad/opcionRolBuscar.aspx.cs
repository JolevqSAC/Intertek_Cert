using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//agregr referencias
using Intertek.Business.Entities;
using Intertek.Business.Logic;
using Intertek.Helpers;
using System.Web.Services;
using Newtonsoft.Json;

namespace Intertek.WEB.Seguridad
{
    public partial class opcionRolBuscar : PaginaBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Usuario objUsuario = (Usuario)Session[Constantes.sesionUsuario];
                hdUsuario.Value = objUsuario.IDUsuario.ToString();
                CargarDatos();
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarDatos();
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            Response.Redirect("opcionRolBuscar.aspx");
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("opcionesRol.aspx");
        }

        private void CargarDatos()
        {
            Rol objRol = new Rol();
            objRol.ROL_Codigo = txtcodigo.Text.Trim();
            objRol.ROL_Nombre = txtRol.Text.Trim();
            objRol.ROL_Descripcion = txtdescripcion.Text.Trim();
            IList<Rol> listRoles= RolBL.Instancia.ListarRol(objRol);
            if (listRoles.Count != 0)
            {
                lblmensaje.Text = "";
                gvBuscar.DataSource = listRoles;
                gvBuscar.DataBind();
            }
            
            else
            {
                gvBuscar.DataSource = null;
                gvBuscar.DataBind();
                lblmensaje.Text = "No Existen Datos Encontrados";
            }

            gvBuscar.PageIndex = 0;
        }

        protected void gvBuscar_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvBuscar.PageIndex = e.NewPageIndex;
            CargarDatos();
        }
        
        [WebMethod]
        public static string Eliminar(int idRol, int idUsuario)
        {
            int mensaje = 0;
            Rol objRol = RolBL.Instancia.ObtenerRolByID(idRol);

            if (objRol != null)
            { 
                objRol.ROL_Estado = Constantes.EstadoEliminado;
                objRol.ROL_UsuarioModificacion = idUsuario.ToString();
                objRol.ROL_FechaHoraModificacion = DateTime.Now;
                objRol = RolBL.Instancia.Actualizar(objRol);
                if (objRol.ROL_Estado == Constantes.EstadoEliminado)
                {
                    mensaje = 1;
                }
            }

            return JsonConvert.SerializeObject(mensaje);
            //Response.Write(JsonConvert.SerializeObject(mensaje));

            //ClientScript.RegisterStartupScript(this.GetType(), "miscriptError", "$(function(){MostrarMensaje('msjError');});", true);
        }

        protected void btnCargarGrilla_Click(object sender, EventArgs e)
        {
            CargarDatos();
        }

        protected void gvBuscar_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Editar":
                    Response.Redirect(string.Format("opcionesRol.aspx?idRol={0}", Convert.ToInt32(e.CommandArgument)), true);
                    break;
            }
        }
    }
}