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
using System.Web.Services;
using Newtonsoft.Json;

namespace Intertek.WEB.Mantenimiento
{
    public partial class comentarioBuscar : PaginaBase
    {
        Nota objNota = new Nota();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                txtCodigo.Focus();
                BuscarPorFiltros();
                if (Session["ddlIdiomas"].ToString() == "en-US")
                {
                    imgeliminar.Src = Resources.generales.imgEliminarUS;
                }
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
            Response.Redirect("comentario.aspx?accion=N");
        }

        private void LlenarGridview()
        {
            objNota.NOT_Estado = Constantes.EstadoActivo;
            this.dgvcomentario.DataSource = NotaBL.Instancia.ListarTodosActivos(objNota);
            this.dgvcomentario.DataBind();
        }

        protected void dgvcomentario_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvcomentario.EditIndex = -1;
            dgvcomentario.PageIndex = e.NewPageIndex;
            BuscarPorFiltros();
        }

        private void BuscarPorFiltros()
        {

            objNota.NOT_Codigo = txtCodigo.Text.Trim();
            objNota.NOT_Nombre = txtNombre.Text.Trim();
            var entidades = NotaBL.Instancia.BuscarPorFiltros(objNota);
            if (entidades.Count != 0)
            {
                lblmensaje.Text = "";
                dgvcomentario.DataSource = entidades;
                dgvcomentario.DataBind();
            }
            else
            {
                dgvcomentario.DataSource = null;
                dgvcomentario.DataBind();
                if ((Session["ddlIdiomas"].ToString() == "en-US"))
                {
                    lblmensaje.Text = "No Data Found";
                }
                else
                {
                    lblmensaje.Text = "No Existen Datos Encontrados";
                }
            }
            dgvcomentario.PageIndex = 0;
        }

        private void LimpiarCampos()
        {
            this.txtCodigo.Text = "";
            this.txtNombre.Text = "";
            this.lblmensaje.Text = "";
            BuscarPorFiltros();
            
        }
        protected void dgvcomentario_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Editar":
                    Response.Redirect(string.Format("comentario.aspx?IDNota={0}&accion=M", Convert.ToInt32(e.CommandArgument), true));
                    break;               
            }
        }


        private void EliminarRegistros()
        {
            var listaEliminados = new List<int>();

            foreach (GridViewRow row in dgvcomentario.Rows)
            {
                var chkBox = (CheckBox)row.Cells[3].FindControl("chkSeleccion");

                if (chkBox != null)
                {
                    if (chkBox.Checked)
                    {
                        var hidIdComentario = (HiddenField)row.Cells[3].FindControl("hidIdComentario");
                        var idNota = Convert.ToInt32(hidIdComentario.Value);
                        listaEliminados.Add(idNota);
                    }
                }
            }

            Usuario objusuario = (Usuario)Session[Constantes.sesionUsuario];

            var listaNotas = new List<Nota>();

            foreach (var eliminado in listaEliminados)
            {
                var entidad = NotaBL.Instancia.ObtenerDatosPorID(eliminado);
                entidad.NOT_Estado = Constantes.EstadoEliminado;
                entidad.NOT_UsuarioModificacion = objusuario.IDUsuario.ToString();
                entidad.NOT_FechaHoraModificacion = DateTime.Now;

                listaNotas.Add(entidad);
            }

            try
            {
                foreach (var Nota in listaNotas)
                {
                    NotaBL.Instancia.Actualizar(Nota);
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
            EliminarRegistros();
        }
       


    }
}