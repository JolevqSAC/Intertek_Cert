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
    public partial class productosBuscar : PaginaBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                txtCodigo.Focus();
                CargarDropDownList();
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
            Response.Redirect("productosBuscar.aspx");
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("productos.aspx");
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            var listaEliminados = new List<int>();

            foreach (GridViewRow row in gvBuscar.Rows)
            {
                var chkBox = (CheckBox)row.Cells[6].FindControl("chkSeleccion");

                if (chkBox != null)
                {
                    if (chkBox.Checked)
                    {
                        var hidIdProducto = (HiddenField)row.Cells[6].FindControl("hidIdProducto");
                        var idcargo = Convert.ToInt32(hidIdProducto.Value);
                        listaEliminados.Add(idcargo);
                    }
                }
            }

            Usuario objusuario = (Usuario)Session[Constantes.sesionUsuario];

            var listaproducto = new List<Producto>();

            foreach (var eliminado in listaEliminados)
            {
                var entidad = ProductoBL.Instancia.ObtenerProductoById(eliminado);
                entidad.PRO_Estado = Constantes.EstadoEliminado;
                entidad.PRO_UsuarioModificacion = objusuario.IDUsuario.ToString();
                entidad.PRO_FechaHoraModificacion = DateTime.Now;

                listaproducto.Add(entidad);
            }

            try
            {
                foreach (var producto in listaproducto)
                {
                    ProductoBL.Instancia.Actualizar(producto);
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
            var datos = Session["cargarProductos"];
            gvBuscar.DataSource = datos;
            gvBuscar.DataBind();
        }

        private void CargarDatos()
        {
            Producto objProducto = new Producto();
            objProducto.PRO_Codigo = txtCodigo.Text;
            objProducto.PRO_Nombre = txtNombre.Text;
            objProducto.PRO_NombreIngles = txtIngles.Text;
            objProducto.IDCategoria = ddlCategoria.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlCategoria.SelectedValue);
           // objProducto.IDUnidadMedida = ddlUnidadMedida.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlUnidadMedida.SelectedValue);
            
            var lstProductos = ProductoBL.Instancia.ListarProductos(objProducto);
            if (lstProductos.Count != 0)
            {
                var datos = (from p in lstProductos
                             select new
                             {
                                 p.PRO_Nombre,
                                 p.PRO_Codigo,
                                 p.PRO_Descripcion,
                                 p.PRO_NombreIngles,
                                 CAT_Nombre = p.CategoriaProducto == null ? "" : p.CategoriaProducto.CAT_Nombre,
                                 UNM_Nombre = p.UnidadMedida == null ? "" : p.UnidadMedida.UNM_Nombre,
                                 p.IDProducto
                             }).ToList();

                gvBuscar.DataSource = datos;
                gvBuscar.DataBind();
                //Session["cargarProductos"] = lstProductos;
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

        private void CargarDropDownList()
        {
            ddlCategoria.DataSource = CategoriaProductoBL.Instancia.ListarTodosCategoriaProductos();
            ddlCategoria.DataValueField = "IDCategoria";
            ddlCategoria.DataTextField = "CAT_Nombre";
            ddlCategoria.DataBind();
            if ((Session["ddlIdiomas"].ToString() == "en-US"))
            {
                ddlCategoria.Items.Insert(0, new ListItem(Resources.generales.testoAll, "0"));
            }
            else
            {
                ddlCategoria.Items.Insert(0, new ListItem(Resources.generales.textoTodos, "0"));
            }
                        
        }

        protected void gvBuscar_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Editar":
                    Response.Redirect(string.Format("productos.aspx?idProducto={0}", Convert.ToInt32(e.CommandArgument)), true);
                    break;
            }
        }

    }
}