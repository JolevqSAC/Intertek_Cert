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
    public partial class normaRequisitoBuscar : PaginaBase
    {
        NormaRequisito objnorma = new NormaRequisito();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
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
            Response.Redirect("normaRequisito.aspx?accion=N");
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            Eliminar();
        }

        private void LlenarGridview()
        {
            objnorma.NRE_Estado = Constantes.EstadoActivo;
            this.dgvnormasrequisito.DataSource = NormaRequisitoBL.Instancia.ListarTodosActivos(objnorma);
            this.dgvnormasrequisito.DataBind();
        }

        protected void dgvnormasrequisito_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.dgvnormasrequisito.EditIndex = -1;
            this.dgvnormasrequisito.PageIndex = e.NewPageIndex;
            BuscarPorFiltros();
        }

        protected void dgvnormasrequisito_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Editar":
                    Response.Redirect(string.Format("normaRequisito.aspx?IDNormaRequisito={0}&Accion=M", Convert.ToInt32(e.CommandArgument)), true);
                    break;
            }
        }

        private void LimpiarCampos()
        {
            this.txtNombre.Text = " ";
            this.txtcodigo.Text = " ";
            this.txtAnio.Text = "";
            this.lblmensaje.Text = "";
            BuscarPorFiltros();
        }

        private void BuscarPorFiltros()
        {
            if (txtAnio.Text == "")
            {
                objnorma.NRE_Codigo = txtcodigo.Text.Trim();
                objnorma.NRE_Nombre = txtNombre.Text.Trim();
                var entidades = NormaRequisitoBL.Instancia.BuscarPorNombreCodigo(objnorma);
                if (entidades.Count != 0)
                {
                    lblmensaje.Text = "";
                    dgvnormasrequisito.DataSource = entidades;
                    dgvnormasrequisito.DataBind();
                }
                else
                {
                    dgvnormasrequisito.DataSource = null;
                    dgvnormasrequisito.DataBind();
                    if ((Session["ddlIdiomas"].ToString() == "en-US"))
                    {
                        lblmensaje.Text = "No Data Found";
                    }
                    else
                    {
                        lblmensaje.Text = "No Existen Datos Encontrados";
                    }
                }
            }
            else
            {
                objnorma.NRE_Codigo = txtcodigo.Text.Trim();
                objnorma.NRE_Nombre = txtNombre.Text.Trim();
                objnorma.NRE_Anio = Convert.ToInt32(txtAnio.Text);
                var entidades = NormaRequisitoBL.Instancia.BuscarPorFiltros(objnorma);
                if (entidades.Count != 0)
                {
                    lblmensaje.Text = "";
                    dgvnormasrequisito.DataSource = entidades;
                    dgvnormasrequisito.DataBind();
                }
                else
                {
                    dgvnormasrequisito.DataSource = null;
                    dgvnormasrequisito.DataBind();
                    if ((Session["ddlIdiomas"].ToString() == "en-US"))
                    {
                        lblmensaje.Text = "No Data Found";
                    }
                    else
                    {
                        lblmensaje.Text = "No Existen Datos Encontrados";
                    }
                }
            }

            dgvnormasrequisito.PageIndex = 0;
            txtcodigo.Focus();

        }

        private void Eliminar()
        {
            var listaEliminados = new List<int>();

            foreach (GridViewRow row in dgvnormasrequisito.Rows)
            {
                var chkBox = (CheckBox)row.Cells[5].FindControl("chkSeleccion");

                if (chkBox != null)
                {
                    if (chkBox.Checked)
                    {
                        var hidIdRequisito = (HiddenField)row.Cells[5].FindControl("hidIdRequisito");
                        var idmuestreo = Convert.ToInt32(hidIdRequisito.Value);
                        listaEliminados.Add(idmuestreo);
                    }
                }
            }

            Usuario objusuario = (Usuario)Session[Constantes.sesionUsuario];

            var listarequisito = new List<NormaRequisito>();

            foreach (var eliminado in listaEliminados)
            {
                var entidad = NormaRequisitoBL.Instancia.ObtenerDatosPorID(eliminado);
                entidad.NRE_Estado = Constantes.EstadoEliminado;
                entidad.NRE_UsuarioModificacion = objusuario.IDUsuario.ToString();
                entidad.NRE_FechaHoraModificacion = DateTime.Now;

                listarequisito.Add(entidad);
            }

            try
            {
                foreach (var requisito in listarequisito)
                {
                    NormaRequisitoBL.Instancia.Actualizar(requisito);
                }

                ClientScript.RegisterStartupScript(this.GetType(), "Confirmacion", "$(function(){MostrarMensaje('msjSatisfactorio');});", true);
            }
            catch
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Error", "$(function(){MostrarMensaje('msjErrorEliminar');});", true);
            }
            BuscarPorFiltros(); 
        }      
    }
}