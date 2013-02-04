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
using System.Globalization;

namespace Intertek.WEB.Mantenimiento
{
    public partial class proveedoresBuscar : PaginaBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string  Cultura = (string)CultureInfo.CurrentCulture.Name;
            txtCodigo.Focus();
            if (!Page.IsPostBack)
            {
                CargarIndicadorArea();
                CargarDatos();
                
                string Eliminar = HttpContext.GetLocalResourceObject("~/Mantenimiento/proveedoresBuscar.aspx", "btnEliminarResource1", new System.Globalization.CultureInfo(Cultura)).ToString();
              
                btnEliminar0.Value = Eliminar;
                
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarDatos();
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
          //  Response.Redirect("proveedoresBuscar.aspx");
            txtCodigo.Text = string.Empty;
            txtRazonSocial.Text = string.Empty;
            txtRuc.Text = string.Empty;
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
           // Response.Redirect("neoproveedores.aspx");

            Response.Redirect("neoproveedores.aspx?accion=N");
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            
            Usuario objusuario = (Usuario)Session[Constantes.sesionUsuario];

            try
            {
                foreach (GridViewRow row in gvBuscar.Rows)
                {
                    CheckBox chbx = (CheckBox)row.FindControl("chkSeleccion");

                    if (chbx != null && chbx.Checked)
                    {
                        HiddenField hidIdProveedor = (HiddenField)row.FindControl("hidIdProveedor");
                        int idProveedor = Convert.ToInt32(hidIdProveedor.Value);
                        Proveedor objProveedor = ProveedorBL.Instancia.ObtenerProveedorById(idProveedor);
                        objProveedor.PRV_Estado = Constantes.EstadoEliminado;
                        objProveedor.PRV_UsuarioModificacion = objusuario.IDUsuario.ToString();
                        objProveedor.PRV_FechaHoraModificacion = DateTime.Now;
                        ProveedorBL.Instancia.Actualizar(objProveedor);
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

        protected void gvBuscar_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvBuscar.PageIndex = e.NewPageIndex;
            var datos = Session["cargarProveedors"];
            gvBuscar.DataSource = datos;
            gvBuscar.DataBind();
        }
        private void CargarDatos()
        {
            Proveedor objProveedor = new Proveedor();
            string indarea = ddlIndicadorArea.SelectedValue.ToString();
            objProveedor.PRV_Codigo = txtCodigo.Text;
            objProveedor.PRV_RazonSocial = txtRazonSocial.Text;
            objProveedor.PRV_RUC = txtRuc.Text;
            objProveedor.PRV_IndicadorArea = indarea =="0" ? "0" : indarea;
            IList<Proveedor> lstProveedors = ProveedorBL.Instancia.ListarProveedores(objProveedor);
            if (lstProveedors.Count != 0)
            {
                lblmensaje.Text = "";
                gvBuscar.DataSource = lstProveedors;
                gvBuscar.DataBind();
                //Session["cargarProveedores"] = lstProveedors;
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
        }

        protected void gvBuscar_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Editar":
                    Response.Redirect(string.Format("neoproveedores.aspx?IDProveedor={0}&Accion=M", Convert.ToInt32(e.CommandArgument)), true);
                    break;
            }
        }

        private void CargarIndicadorArea()
        {
            if (Session["ddlIdiomas"].ToString() == "en-US")
            {
                ddlIndicadorArea.Items.Insert(0, new ListItem(Resources.generales.testoAll, "0"));
            }
            else
            {
                ddlIndicadorArea.Items.Insert(0, new ListItem(Resources.generales.textoTodos, "0"));
            }
            ddlIndicadorArea.Items.Insert(1, new ListItem(Resources.generales.txtAgri, "A"));
            ddlIndicadorArea.Items.Insert(2, new ListItem(Resources.generales.txtHidro, "H"));
            ddlIndicadorArea.Items.Insert(3, new ListItem(Resources.generales.txtMixto, "M"));
        }
    }
}