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
    public partial class estadoBuscar : PaginaBase
    {
        Estado objestado = new Estado();
        protected void Page_Load(object sender, EventArgs e)
        {
            txtDescripcion.Focus();
            if (!IsPostBack)
                LlenarGridview();
        }

        protected void btnbuscar_Click(object sender, EventArgs e)
        {
            BuscarPorFiltros(txtDescripcion.Text, txtTipo.Text);
        }

        protected void btnlimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
            LlenarGridview();
        }

        protected void btnnuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("estado.aspx?accion=N");
        }

             
        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            EliminarEstado(Convert.ToInt32(hdEliminarID.Value));
        }

        private void LlenarGridview()
        {
            objestado.EST_Estado = Constantes.EstadoActivo;
            this.dgvestado.DataSource = EstadoBL.Instancia.ListarTodosActivos(objestado);
            this.dgvestado.DataBind();
        }

        protected void dgvestado_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.dgvestado.EditIndex = -1;
            this.dgvestado.PageIndex = e.NewPageIndex;
            LlenarGridview();
        }

        private void BuscarPorFiltros(string descripcion, string tipo)
        {
            objestado.EST_Descripcion =  descripcion.Trim();
            objestado.EST_Tipo = tipo.Trim();
            objestado.EST_Estado = Constantes.EstadoActivo;
            var entidades = EstadoBL.Instancia.BuscarPorFiltros(objestado);
            if (entidades.Count != 0)
            {
                lblmensaje.Text = "";
                this.dgvestado.DataSource = entidades;
                this.dgvestado.DataBind();
            }
            else
            {
                this.dgvestado.DataSource = null;
                this.dgvestado.DataBind();
                lblmensaje.Text = "No Existen Datos Encontrados";
            }
            this.dgvestado.PageIndex = 0;
            txtDescripcion.Focus();
        }

        private void LimpiarCampos()
        {
            this.txtDescripcion.Text = "";
            this.txtTipo.Text = "";
            this.lblmensaje.Text = "";
        }

        protected void dgvestado_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Editar":
                    Response.Redirect(string.Format("estado.aspx?IDEstado={0}&accion=M", Convert.ToInt32(e.CommandArgument), true));
                    break;
            }
        }

        private void EliminarEstado(int idestado)
        {
            Usuario objusuario = (Usuario)Session[Constantes.sesionUsuario];
            objestado.IDEstado = idestado;
            var entidad = EstadoBL.Instancia.ObtenerDatosPorID(objestado);
            entidad.EST_Estado = Constantes.EstadoEliminado;
            entidad.EST_UsuarioModificacion = objusuario.IDUsuario.ToString();
            entidad.EST_FechaHoraModificacion = DateTime.Now;
            try
            {

                EstadoBL.Instancia.Actualizar(entidad);
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