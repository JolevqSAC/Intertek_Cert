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
    public partial class tipoEnvaseBuscar : PaginaBase
    {
        TipoEnvase objtipoenvase = new TipoEnvase();

        protected void Page_Load(object sender, EventArgs e)
        {
            txtDescripcion.Focus();
            if (!IsPostBack)
                LlenarGridview();
        }

        protected void btnbuscar_Click(object sender, EventArgs e)
        {
            BuscarPorFiltros(txtDescripcion.Text);
        }

        protected void btnlimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
            LlenarGridview();
        }

        protected void btnnuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("tipoEnvase.aspx?accion=N");
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            EliminarTipoEnvase(Convert.ToInt32(hdEliminarID.Value));
        }

        private void LlenarGridview()
        {
            objtipoenvase.TIE_Estado = Constantes.EstadoActivo;
            this.dgvtipoenvase.DataSource = TipoEnvaseBL.Instancia.ListarTodosActivos(objtipoenvase);
            this.dgvtipoenvase.DataBind();
        }

        protected void dgvtipoenvase_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.dgvtipoenvase.EditIndex = -1;
            this.dgvtipoenvase.PageIndex = e.NewPageIndex;
            LlenarGridview();
        }

        private void BuscarPorFiltros(string descripcion)
        {
            objtipoenvase.TIE_Descripcion = descripcion.Trim();            
            objtipoenvase.TIE_Estado = Constantes.EstadoActivo;
            var entidades = TipoEnvaseBL.Instancia.BuscarPorFiltros(objtipoenvase);
            if (entidades.Count != 0)
            {
                lblmensaje.Text = "";
                dgvtipoenvase.DataSource = entidades;
                dgvtipoenvase.DataBind();
            }
            else
            {
                dgvtipoenvase.DataSource = null;
                dgvtipoenvase.DataBind();
                lblmensaje.Text = "No Existen Datos Encontrados";
            }
            this.dgvtipoenvase.PageIndex = 0;
            txtDescripcion.Focus();
        }

        private void LimpiarCampos()
        {
            this.txtDescripcion.Text = "";
            this.lblmensaje.Text = "";
        }

        protected void dgvtipoenvase_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Editar":
                    Response.Redirect(string.Format("tipoEnvase.aspx?IDTipoEnvase={0}&accion=M", Convert.ToInt32(e.CommandArgument), true));
                    break;
            }
        }

        private void EliminarTipoEnvase(int idtipoenvase)
        {
            Usuario objusuario = (Usuario)Session[Constantes.sesionUsuario];
            objtipoenvase.IDTiposEnvase = idtipoenvase;
            var entidad = TipoEnvaseBL.Instancia.ObtenerDatosPorID(objtipoenvase);
            entidad.TIE_Estado = Constantes.EstadoEliminado;
            entidad.TIE_UsuarioModificacion = objusuario.IDUsuario.ToString();
            entidad.TIE_FechaHoraModificacion = DateTime.Now;
            try
            {

                TipoEnvaseBL.Instancia.Actualizar(entidad);
                ClientScript.RegisterStartupScript(this.GetType(), "Confirmacion", "$(function(){MostrarMensaje('msjEliminacionOK');});", true);
            }
            catch
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Error", "$(function(){MostrarMensaje('msjErrorEliminar');});", true);
            }
            LlenarGridview();
        }

    }
}