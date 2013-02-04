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
    public partial class unidadMedidaBuscar : PaginaBase
    {
        UnidadMedida objUnidadMedida = new UnidadMedida();

        protected void Page_Load(object sender, EventArgs e)        
        {
            if (!Page.IsPostBack)
            {
                txtcodigo.Focus();
                if (Session["ddlIdiomas"].ToString() == "en-US")
                {
                    imgeliminar.Src = Resources.generales.imgEliminarUS;
                }                
                BuscarPorFiltros();
            }
        }

        protected void btnbuscar_Click(object sender, EventArgs e)
        {
            BuscarPorFiltros();
        }

        protected void btnlimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        protected void btnnuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("unidadMedida.aspx?accion=N");
        }

        private void LlenarGridview()
        {
            objUnidadMedida.UNM_Estado = Constantes.EstadoActivo;
            this.dgvunidadmedida.DataSource = UnidadMedidaBL.Instancia.ListarTodosActivos(objUnidadMedida);
            this.dgvunidadmedida.DataBind();
        }

        protected void dgvunidadmedida_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.dgvunidadmedida.EditIndex = -1;
            this.dgvunidadmedida.PageIndex = e.NewPageIndex;
            BuscarPorFiltros();
        }

        private void BuscarPorFiltros()
        {
            objUnidadMedida.UNM_Codigo =  txtcodigo.Text.Trim();
            objUnidadMedida.UNM_Nombre = txtNombre.Text.Trim();
            objUnidadMedida.UNM_NombreCorto = txtAbreviatura.Text.Trim();
            objUnidadMedida.UNM_Estado = Constantes.EstadoActivo;
            var entidades = UnidadMedidaBL.Instancia.BuscarPorFiltros(objUnidadMedida);
            if (entidades.Count!=0)
            {
                lblmensaje.Text = "";
                dgvunidadmedida.DataSource = entidades;
                dgvunidadmedida.DataBind();
            }
            else
            {
                dgvunidadmedida.DataSource = null;
                dgvunidadmedida.DataBind();
                if ((Session["ddlIdiomas"].ToString() == "en-US"))
                {
                    lblmensaje.Text = "No Data Found";
                }
                else
                {
                    lblmensaje.Text = "No Existen Datos Encontrados";
                }
            }
            dgvunidadmedida.PageIndex = 0;
        }

        private void LimpiarCampos()
        {
            this.txtcodigo.Text = "";
            this.txtNombre.Text = "";
            this.txtAbreviatura.Text = "";
            this.lblmensaje.Text = "";
            BuscarPorFiltros();
        }

        protected void dgvunidadmedida_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Editar":
                     Response.Redirect(string.Format("unidadMedida.aspx?IDUnidadMedida={0}&accion=M", Convert.ToInt32(e.CommandArgument),true));
                    break;             
            }
        }

        private void EliminarUnidadMedida()
        {
            var listaEliminados = new List<int>();

            foreach (GridViewRow row in dgvunidadmedida.Rows)
            {
                var chkBox = (CheckBox)row.Cells[3].FindControl("chkSeleccion");

                if (chkBox != null)
                {
                    if (chkBox.Checked)
                    {
                        var hidIdUnidad = (HiddenField)row.Cells[3].FindControl("hidIdUnidad");
                        var idunidad = Convert.ToInt32(hidIdUnidad.Value);
                        listaEliminados.Add(idunidad);
                    }
                }
            }

            Usuario objusuario = (Usuario)Session[Constantes.sesionUsuario];

            var listaUnidad = new List<UnidadMedida>();

            foreach (var eliminado in listaEliminados)
            {
                var entidad = UnidadMedidaBL.Instancia.ObtenerDatosPorID(eliminado);
                entidad.UNM_Estado = Constantes.EstadoEliminado;
                entidad.UNM_UsuarioModificacion = objusuario.IDUsuario.ToString();
                entidad.UNM_FechaHoraModificacion = DateTime.Now;

                listaUnidad.Add(entidad);
            }

            try
            {
                foreach (var categoria in listaUnidad)
                {
                    UnidadMedidaBL.Instancia.Actualizar(categoria);
                }

                ClientScript.RegisterStartupScript(this.GetType(), "Confirmacion", "$(function(){MostrarMensaje('msjEliminacionOK');});", true);
            }
            catch
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Error", "$(function(){MostrarMensaje('msjErrorEliminar');});", true);
            }
            BuscarPorFiltros();
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            EliminarUnidadMedida();
        }
    }
}