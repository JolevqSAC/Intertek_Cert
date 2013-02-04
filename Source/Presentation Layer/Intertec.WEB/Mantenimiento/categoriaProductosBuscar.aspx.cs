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
    public partial class categoriaProductosBuscar : PaginaBase
    {
        CategoriaProducto objcategoria = new CategoriaProducto();
        

        protected void Page_Load(object sender, EventArgs e)
        {
            txtcodigo.Focus();
            if (!IsPostBack)
            {
                CargarIndicadorArea();
                BuscarPorFiltros();
                if (Session["ddlIdiomas"].ToString() == "en-US")
                {
                    imgeliminar.Src = Resources.generales.imgEliminarUS;
                }
            }

        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
           BuscarPorFiltros();
            
        }

        private void LlenarGridview()
        {
            this.dgvbuscar.DataSource = CategoriaProductoBL.Instancia.ListarTodosCategoriaProductos();
            this.dgvbuscar.DataBind();
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();            
        }

        private void LimpiarCampos()
        {
            this.txtcodigo.Text = "";
            this.txtNombre.Text = "";
            this.txtingles.Text = "";
            this.lblmensaje.Text = "";
            this.ddlIndicadorArea.SelectedIndex = -1;
            BuscarPorFiltros();
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("categoriaProductos.aspx?accion=N");
        }

        protected void dgvbuscar_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.dgvbuscar.EditIndex = -1;
            this.dgvbuscar.PageIndex = e.NewPageIndex;
            BuscarPorFiltros();
        }

        protected void dgvbuscar_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Editar":
                    Response.Redirect(string.Format("categoriaProductos.aspx?IDCategoria={0}&Accion=M", Convert.ToInt32(e.CommandArgument)), true);
                    break;
            }
        }

        private void BuscarPorFiltros()
        {

            string indarea = ddlIndicadorArea.SelectedValue.ToString();
            objcategoria.CAT_Codigo= txtcodigo.Text.Trim();
            objcategoria.CAT_Nombre = txtNombre.Text.Trim();
            objcategoria.CAT_NombreIngles = txtingles.Text.Trim();
            objcategoria.CAT_IndicadorArea = indarea=="0"? "0": indarea;            
            var entidades = CategoriaProductoBL.Instancia.ListarCategoriaProductos(objcategoria);
            if (entidades.Count != 0)
            {
                lblmensaje.Text = "";
                dgvbuscar.DataSource = entidades;
                dgvbuscar.DataBind();
            }
            else
            {
                dgvbuscar.DataSource = null;
                dgvbuscar.DataBind();
                if ((Session["ddlIdiomas"].ToString() == "en-US"))
                {
                    lblmensaje.Text = "No Data Found";
                }
                else
                {
                    lblmensaje.Text = "No Existen Datos Encontrados";
                }
            }
            dgvbuscar.PageIndex = 0;

        }

        private void EliminarRegistros()
        {
            var listaEliminados = new List<int>();

            foreach (GridViewRow row in dgvbuscar.Rows)
            {
                var chkBox = (CheckBox)row.Cells[5].FindControl("chkSeleccion");

                if (chkBox != null)
                {
                    if (chkBox.Checked)
                    {
                        var hidIdCategoria = (HiddenField)row.Cells[5].FindControl("hidIdCategoria");
                        var idcategoria = Convert.ToInt32(hidIdCategoria.Value);
                        listaEliminados.Add(idcategoria);
                    }
                }
            }

            Usuario objusuario = (Usuario)Session[Constantes.sesionUsuario];

            var listaCategorias = new List<CategoriaProducto>();

            foreach (var eliminado in listaEliminados)
            {
                var entidad = CategoriaProductoBL.Instancia.ObtenerCategoriaProductoByID(eliminado);
                entidad.CAT_Estado = Constantes.EstadoEliminado;
                entidad.CAT_UsuarioModificacion = objusuario.IDUsuario.ToString();
                entidad.CAT_FechaHoraModificacion = DateTime.Now;

                listaCategorias.Add(entidad);
            }

            try
            {
                foreach (var categoria in listaCategorias)
                {
                    CategoriaProductoBL.Instancia.Actualizar(categoria);
                }

                ClientScript.RegisterStartupScript(this.GetType(), "Confirmacion", "$(function(){MostrarMensaje('msjSatisfactorio');});", true);
            }
            catch
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Error", "$(function(){MostrarMensaje('msjErrorEliminar');});", true);
            }
            BuscarPorFiltros();
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            EliminarRegistros();
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
            ddlIndicadorArea.Items.Insert(1, new ListItem(Resources.generales.txtAgri,"A"));
            ddlIndicadorArea.Items.Insert(2, new ListItem(Resources.generales.txtHidro,"H"));
            ddlIndicadorArea.Items.Insert(3, new ListItem(Resources.generales.txtMixto,"M"));
        }
    }
}