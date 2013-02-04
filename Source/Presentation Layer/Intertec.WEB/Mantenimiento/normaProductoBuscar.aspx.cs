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

namespace Intertek.WEB.Mantenimiento
{
    public partial class normaProductoBuscar : PaginaBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CargarDropDownList();
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarDatos();
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            Response.Redirect("normaProductoBuscar.aspx");
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("normaProductos.aspx");
        }

        protected void gvBuscar_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvBuscar.PageIndex = e.NewPageIndex;

            var datos = Session["cargarNormaProductos"];
            gvBuscar.DataSource = datos;
            gvBuscar.DataBind();
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            Usuario objUsuario = (Usuario)Session[Constantes.sesionUsuario];

            NormaProducto objEliminar = NormaProductoBL.Instancia.ObtenerNormaProductoById(Convert.ToInt32(hdEliminarID.Value));

            objEliminar.NOR_Estado = Constantes.EstadoEliminado;
            objEliminar.NOR_UsuarioModificacion = objUsuario.IDUsuario.ToString();
            objEliminar.NOR_FechaHoraModificacion = DateTime.Now;
            try
            {
                NormaProductoBL.Instancia.Eliminar(objEliminar);
                CargarDatos();
                ClientScript.RegisterStartupScript(this.GetType(), "miscriptAlerta", "$(function(){MostrarMensaje('msjSatisfactorio');});", true);
            }
            catch
            {

            }
        }

        private void CargarDatos()
        {
            NormaProducto objNormaProducto = new NormaProducto();
            objNormaProducto.NOR_Nombre = txtNombre.Text;
            objNormaProducto.NOR_Acreditador = txtAcreditador.Text;
            objNormaProducto.IDProducto = ddlProducto.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlProducto.SelectedValue);

            IList<NormaProducto> lstNormaProductos = NormaProductoBL.Instancia.ListarNormaProductos(objNormaProducto);

            var datos = (from p in lstNormaProductos
                         select new
                         {   p.NOR_Nombre,
                             p.NOR_Acreditador,
                             PRO_Nombre = p.Producto==null?"":p.Producto.PRO_Nombre,
                             p.NOR_Anio,
                             p.IDNorma
                         }).ToList();

            gvBuscar.DataSource = datos;
            gvBuscar.DataBind();

            Session["cargarNormaProductos"] = lstNormaProductos;
        }

        private void CargarDropDownList()
        {
            ddlProducto.DataSource = ProductoBL.Instancia.ListarProductosTodos();
            ddlProducto.DataValueField = "IDProducto";
            ddlProducto.DataTextField = "PRO_Nombre";
            ddlProducto.DataBind();
            ddlProducto.Items.Insert(0, new ListItem(Resources.generales.textoTodos, "0"));

        }
    }
}