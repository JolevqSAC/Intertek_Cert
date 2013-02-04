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

namespace Intertek.WEB.Seguridad
{
    public partial class usuariosBuscar : PaginaBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CargarRoles();
                CargarIndicadorSignatario();
                CargarDatos();
                
                if (Session["ddlIdiomas"].ToString() == "en-US")
                {
                    imgeliminar.Src = Resources.generales.imgEliminarUS;
                }
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarDatos();
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            Response.Redirect("usuariosBuscar.aspx");
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("usuarios.aspx");
        }

        protected void gvBuscar_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvBuscar.PageIndex = e.NewPageIndex;
            
            CargarDatos();
        }

      
        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            //Usuario objUsuario = (Usuario)Session[Constantes.sesionUsuario];
            ////hdEliminarID.Value
            //Usuario objEliminar = UsuarioBL.Instancia.ObtenerUsuarioById(Convert.ToInt32(hdEliminarID.Value));

            //objEliminar.USU_Estado = Constantes.EstadoEliminado;
            //objEliminar.USU_UsuarioModificacion = objUsuario.IDUsuario.ToString();
            //objEliminar.USU_FechaHoraModificacion = DateTime.Now;
            //try
            //{

            //    UsuarioBL.Instancia.Eliminar(objEliminar);
            //    CargarDatos();
            //    ClientScript.RegisterStartupScript(this.GetType(), "miscriptAlerta", "$(function(){MostrarMensaje('msjSatisfactorio');});", true);
            //}
            //catch
            //{

            //}
            Usuario objUsuarioSesion = (Usuario)Session[Constantes.sesionUsuario];

            try
            {
                foreach (GridViewRow row in gvBuscar.Rows)
                {
                    CheckBox chbx = (CheckBox)row.FindControl("chkSeleccion");

                    if (chbx != null && chbx.Checked)
                    {
                        HiddenField hidIdUsuario = (HiddenField)row.FindControl("hidIdUsuario");
                        int idUsuario = Convert.ToInt32(hidIdUsuario.Value);
                        Usuario objUsuario = UsuarioBL.Instancia.ObtenerUsuarioById(idUsuario);
                        objUsuario.USU_Estado = Constantes.EstadoEliminado;
                        objUsuario.USU_UsuarioModificacion = objUsuarioSesion.IDUsuario.ToString();
                        objUsuario.USU_FechaHoraModificacion = DateTime.Now;
                        UsuarioBL.Instancia.Actualizar(objUsuario);
                    }
                }

                ClientScript.RegisterStartupScript(this.GetType(), "Confirmacion", "$(function(){MostrarMensaje('msjSatisfactorio');});", true);
            }
            catch
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Error", "$(function(){MostrarMensaje('msjErrorEliminar');});", true);
            }

            CargarDatos();
        }

        private void CargarDatos()
        {
            Usuario objUsuario = new Usuario();
            objUsuario.IDRol = ddlRol.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlRol.SelectedValue);
            objUsuario.USU_Login = txtUsuario.Text;
            objUsuario.USU_IndicadorSignatario = ddlIndicadorSignatario.SelectedValue.ToString();
            IList<Usuario> listUsuarios = UsuarioBL.Instancia.ListarUsuarios(objUsuario);

            if (listUsuarios.Count != 0)
            {
                var datos = (from p in listUsuarios
                         select new
                         {
                             ROL_Nombre = p.Rol==null?"":p.Rol.ROL_Nombre,
                             p.IDUsuario,
                             p.USU_Login,
                             p.USU_IndicadorSignatario
                         }).ToList();

                lblmensaje.Text = "";
                gvBuscar.DataSource = datos;
                gvBuscar.DataBind();
            }
            else
            {
                gvBuscar.DataSource = null;
                gvBuscar.DataBind();
                if ((Session["ddlIdiomas"].ToString() == "en-US"))
                {
                    lblmensaje.Text = "No Data Found";
                }
                else
                {
                    lblmensaje.Text = "No Existen Datos Encontrados";
                }
            }
            gvBuscar.PageIndex = 0;


            //Session["cargarRol"]=listRoles;
        }

        private void CargarRoles()
        {
            ddlRol.DataSource = RolBL.Instancia.ListarRolTodos();
            ddlRol.DataValueField = "IDRol";
            ddlRol.DataTextField = "ROL_Nombre";
            ddlRol.DataBind();

            if (Session["ddlIdiomas"].ToString() == "en-US")
            {
                ddlRol.Items.Insert(0, new ListItem(Resources.generales.testoAll, "0"));
            }
            else
            {
                ddlRol.Items.Insert(0, new ListItem(Resources.generales.textoTodos, "0"));
            }
            

        }

        private void CargarIndicadorSignatario()
        {
            if (Session["ddlIdiomas"].ToString() == "en-US")
            {
                ddlIndicadorSignatario.Items.Insert(0, new ListItem(Resources.generales.testoAll, "0"));
            }
            else
            {
                ddlIndicadorSignatario.Items.Insert(0, new ListItem(Resources.generales.textoTodos, "0"));
            }
            ddlIndicadorSignatario.Items.Insert(1, "Si");
            ddlIndicadorSignatario.Items.Insert(2, "No");
        }

        protected void gvBuscar_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Editar":
                    Response.Redirect(string.Format("usuarios.aspx?idUsuario={0}&Accion=M", Convert.ToInt32(e.CommandArgument)), true);
                    break;
            }
        }
    }
}