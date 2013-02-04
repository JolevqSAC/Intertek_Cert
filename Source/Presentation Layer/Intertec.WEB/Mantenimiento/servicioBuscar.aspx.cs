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
    public partial class servicioBuscar : PaginaBase
    {
        Servicio objservicio = new Servicio();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            txtcodigo.Focus();
            if (!Page.IsPostBack)
            {
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
            this.dgvbuscar.DataSource = ServicioBL.Instancia.ListarTodosServicios();
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
            BuscarPorFiltros();
        }


        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("servicio.aspx?accion=N");
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
                    Response.Redirect(string.Format("servicio.aspx?IDServicio={0}&Accion=M", Convert.ToInt32(e.CommandArgument)), true);
                    break;
            }
        }

        private void BuscarPorFiltros()
        {
            objservicio.SER_Codigo = txtcodigo.Text.Trim();
            objservicio.SER_Nombre = txtNombre.Text.Trim();
            objservicio.SER_NombreIngles = txtingles.Text.Trim();
            var entidades = ServicioBL.Instancia.ListarServicio(objservicio);
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
                var chkBox = (CheckBox)row.Cells[4].FindControl("chkSeleccion");

                if (chkBox != null)
                {
                    if (chkBox.Checked)
                    {
                        var hidIdServicio = (HiddenField)row.Cells[4].FindControl("hidIdServicio");
                        var idservicio = Convert.ToInt32(hidIdServicio.Value);
                        listaEliminados.Add(idservicio);
                    }
                }
            }

            Usuario objusuario = (Usuario)Session[Constantes.sesionUsuario];

            var listaServicio = new List<Servicio>();

            foreach (var eliminado in listaEliminados)
            {
                var entidad = ServicioBL.Instancia.ObtenerServicioByID(eliminado);
                entidad.SER_Estado = Constantes.EstadoEliminado;
                entidad.SER_UsuarioModificacion = objusuario.IDUsuario.ToString();
                entidad.SER_FechaHoraModificacion = DateTime.Now;

                listaServicio.Add(entidad);
            }

            try
            {
                foreach (var servicio in listaServicio)
                {
                    ServicioBL.Instancia.Actualizar(servicio);
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
    }
}