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
    public partial class metodosBuscar : PaginaBase
    {
        Metodo objmetodo = new Metodo();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtCodigo.Focus();
                BuscarPorFiltros();
                if (Session["ddlIdiomas"].ToString() == "en-US")
                {
                    imgeliminar.Src = Resources.generales.imgEliminarUS;
                } 
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            Eliminar(); 
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            BuscarPorFiltros();
        }


        private void BuscarPorFiltros()
        {
            objmetodo.MET_Codigo = txtCodigo.Text.Trim();
            objmetodo.MET_Nombre = txtnombre.Text.Trim();
            objmetodo.MET_NombreIngles = txtingles.Text.Trim();
            objmetodo.MET_Descripcion = txtdescripcion.Text.Trim();
            var entidades = MetodoBL.Instancia.ListarMetodos(objmetodo);
            if (entidades.Count != 0)
            {
                lblmensaje.Text = "";
                gvBuscar.DataSource = entidades;
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

        }
        
        
        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            Response.Redirect("metodosBuscar.aspx");
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("metodos.aspx");
        }

        protected void gvBuscar_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvBuscar.PageIndex = e.NewPageIndex;
            var datos = Session["cargarMetodos"];
            gvBuscar.DataSource = datos;
            gvBuscar.DataBind();
        }

        private void LlenarGridview()
        {
            this.gvBuscar.DataSource = MetodoBL.Instancia.ListarMetodosTodos();
            this.gvBuscar.DataBind();
        }

        private void LimpiarCampos()
        {
            this.txtCodigo.Text = "";
            this.txtnombre.Text = "";
            this.txtingles.Text = "";
            this.txtdescripcion.Text = "";
            this.lblmensaje.Text = "";
            BuscarPorFiltros();
        }

        private void Eliminar()
        {
            var listaEliminados = new List<int>();

            foreach (GridViewRow row in gvBuscar.Rows)
            {
                var chkBox = (CheckBox)row.Cells[3].FindControl("chkSeleccion");

                if (chkBox != null)
                {
                    if (chkBox.Checked)
                    {
                        var hidIdMetodo = (HiddenField)row.Cells[3].FindControl("hidIdMetodo");
                        var idmetodo = Convert.ToInt32(hidIdMetodo.Value);
                        listaEliminados.Add(idmetodo);
                    }
                }
            }

            Usuario objusuario = (Usuario)Session[Constantes.sesionUsuario];

            var listametodos = new List<Metodo>();

            foreach (var eliminado in listaEliminados)
            {
                var entidad = MetodoBL.Instancia.ObtenerMetodoById(eliminado);
                entidad.MET_Estado = Constantes.EstadoEliminado;
                entidad.MET_UsuarioModificacion = objusuario.IDUsuario.ToString();
                entidad.MET_FechaHoraModificacion = DateTime.Now;

                listametodos.Add(entidad);
            }

            try
            {
                foreach (var metodo in listametodos)
                {
                    MetodoBL.Instancia.Actualizar(metodo);
                }

                ClientScript.RegisterStartupScript(this.GetType(), "Confirmacion", "$(function(){MostrarMensaje('msjSatisfactorio');});", true);
            }
            catch
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Error", "$(function(){MostrarMensaje('msjErrorEliminar');});", true);
            }
            BuscarPorFiltros();
        }

        protected void gvBuscar_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Editar":
                    Response.Redirect(string.Format("metodos.aspx?IDMetodo={0}&Accion=M", Convert.ToInt32(e.CommandArgument)), true);
                    break;
            }
        }
    }
}