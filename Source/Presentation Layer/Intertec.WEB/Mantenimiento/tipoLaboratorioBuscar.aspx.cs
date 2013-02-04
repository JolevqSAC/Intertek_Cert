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
    public partial class tipoLaboratorioBuscar : PaginaBase
    {
        TipoLaboratorio objtipolaboratorio = new TipoLaboratorio();
        protected void Page_Load(object sender, EventArgs e)
        {
            txtNombre.Focus();
            if (!IsPostBack)
                LlenarGridview();
        }

        protected void btnbuscar_Click(object sender, EventArgs e)
        {
            BuscarPorFiltros(txtNombre.Text);
        }

        protected void btnlimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
            LlenarGridview();
        }

        protected void btnnuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("tipoLaboratorio.aspx?accion=N");
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            EliminarTipoLaboratorio(Convert.ToInt32(hdEliminarID.Value));
        }

        private void LlenarGridview()
        {
            objtipolaboratorio.TLA_Estado = Constantes.EstadoActivo;
            this.dgvtipolaboratorio.DataSource = TipoLaboratorioBL.Instancia.ListarTodosActivos(objtipolaboratorio);
            this.dgvtipolaboratorio.DataBind();
        }

        protected void dgvtipolaboratorio_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.dgvtipolaboratorio.EditIndex = -1;
            this.dgvtipolaboratorio.PageIndex = e.NewPageIndex;
            LlenarGridview();
        }


        private void BuscarPorFiltros(string nombre)
        {
            objtipolaboratorio.TLA_Nombre = nombre.Trim();
            objtipolaboratorio.TLA_Estado = Constantes.EstadoActivo;
            var entidades = TipoLaboratorioBL.Instancia.BuscarPorFiltros(objtipolaboratorio);
            if (entidades.Count != 0)
            {
                lblmensaje.Text = "";
                dgvtipolaboratorio.DataSource = entidades;
                dgvtipolaboratorio.DataBind();
            }
            else
            {
                dgvtipolaboratorio.DataSource = null;
                dgvtipolaboratorio.DataBind();
                lblmensaje.Text = "No Existen Datos Encontrados";
            }
            dgvtipolaboratorio.PageIndex = 0;
            txtNombre.Focus();
        }

        private void LimpiarCampos()
        {
            this.txtNombre.Text = "";
            this.lblmensaje.Text = "";
        }

        protected void dgvtipolaboratorio_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Editar":
                    Response.Redirect(string.Format("tipoLaboratorio.aspx?IDTipoLaboratorio={0}&accion=M", Convert.ToInt32(e.CommandArgument), true));
                    break;
            }
        }

        private void EliminarTipoLaboratorio(int idtipolaboratorio)
        {
            Usuario objusuario = (Usuario)Session[Constantes.sesionUsuario];
            objtipolaboratorio.IDTipoLaboratorio = idtipolaboratorio;
            var entidad = TipoLaboratorioBL.Instancia.ObtenerDatosPorID(objtipolaboratorio);
            entidad.TLA_Estado = Constantes.EstadoEliminado;
            entidad.TLA_UsuarioModificacion = objusuario.IDUsuario.ToString();
            entidad.TLA_FechaHoraModificacion = DateTime.Now;
            try
            {

                TipoLaboratorioBL.Instancia.Actualizar(entidad);
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