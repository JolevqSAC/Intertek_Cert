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
    public partial class areaBuscar : PaginaBase
    {
        Area objarea = new Area();

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
            this.dgvarea.DataSource = AreaBL.Instancia.ListarTodosAreas();
            this.dgvarea.DataBind();
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();           
        }

        private void LimpiarCampos()
        {
            this.txtcodigo.Text = "";
            this.txtNombre.Text = "";
            this.txtDescripcion.Text = "";
            this.lblmensaje.Text = "";
            BuscarPorFiltros();
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("area.aspx?accion=N");
        }

        protected void dgvarea_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.dgvarea.EditIndex = -1;
            this.dgvarea.PageIndex = e.NewPageIndex;
            BuscarPorFiltros();
        }

        private void BuscarPorFiltros()
        {
            objarea.ARE_Codigo =  txtcodigo.Text.Trim();
            objarea.ARE_Nombre = txtNombre.Text.Trim();
            objarea.ARE_Descripcion = txtDescripcion.Text.Trim();            
            var entidades = AreaBL.Instancia.ListarAreas(objarea);
            if (entidades.Count != 0)
            {
                lblmensaje.Text = "";
                dgvarea.DataSource = entidades;
                dgvarea.DataBind();
            }
            else
            {
                dgvarea.DataSource = null;
                dgvarea.DataBind();
                if ((Session["ddlIdiomas"].ToString() == "en-US"))
                {
                    lblmensaje.Text = "No Data Found";
                }
                else
                {
                    lblmensaje.Text = "No Existen Datos Encontrados";
                }
            }
            dgvarea.PageIndex = 0;
            txtcodigo.Focus();

        }

        protected void dgvarea_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Editar":
                    Response.Redirect(string.Format("area.aspx?IDArea={0}&Accion=M", Convert.ToInt32(e.CommandArgument)), true);
                    break;
            }
        }

        private void EliminarArea()
        {
            Cargo objcargo = new Cargo();
            
            var listaEliminados = new List<int>();

            foreach (GridViewRow row in dgvarea.Rows)
            {
                var chkBox = (CheckBox)row.Cells[3].FindControl("chkSeleccion");

                if (chkBox != null)
                {
                    if (chkBox.Checked)
                    {
                        var hidIdArea = (HiddenField)row.Cells[3].FindControl("hidIdArea");
                        var idArea = Convert.ToInt32(hidIdArea.Value);
                        //var ListaCargoArea = CargosBL.Instancia.ListarxIdArea(idArea);
                        //if (ListaCargoArea.Count != 0)
                        //{
                        //    ClientScript.RegisterStartupScript(this.GetType(), "Error", "$(function(){MostrarMensaje('msjErrorEliminar');});", true);
                        //}
                        //else
                        //{
                            listaEliminados.Add(idArea);
                        //    ClientScript.RegisterStartupScript(this.GetType(), "Confirmacion", "$(function(){MostrarMensaje('msjSatisfactorio');});", true);
                           
                        //}
                    }
                }
            }

            Usuario objusuario = (Usuario)Session[Constantes.sesionUsuario];

            var listaAreas = new List<Area>();

            foreach (var eliminado in listaEliminados)
            {
                var entidad = AreaBL.Instancia.ObtenerAreaByID(eliminado);
                entidad.ARE_Estado = Constantes.EstadoEliminado;
                entidad.ARE_UsuarioModificacion = objusuario.IDUsuario.ToString();
                entidad.ARE_FechaHoraModificacion = DateTime.Now;

                listaAreas.Add(entidad);
            }

            try
            {
                foreach (var area in listaAreas)
                {
                    AreaBL.Instancia.Actualizar(area);
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
            EliminarArea();
        }
    }
}