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
    public partial class normaMuestreoBuscar : PaginaBase
    {
        NormaMuestreo objnorma = new NormaMuestreo();

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
            Response.Redirect("normaMuestreo.aspx?accion=N");
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            Eliminar();
        }

        private void LlenarGridview()
        {
            objnorma.NOM_Estado = Constantes.EstadoActivo;
            this.dgvnormasmuestreo.DataSource = NormaMuestreoBL.Instancia.ListarTodosActivos(objnorma);
            this.dgvnormasmuestreo.DataBind();
        }

        protected void dgvnormasmuestreo_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.dgvnormasmuestreo.EditIndex = -1;
            this.dgvnormasmuestreo.PageIndex = e.NewPageIndex;
            BuscarPorFiltros();
        }

        protected void dgvnormasmuestreo_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Editar":
                    Response.Redirect(string.Format("normaMuestreo.aspx?IDNormaMuestreo={0}&Accion=M", Convert.ToInt32(e.CommandArgument)), true);
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
                objnorma.NOM_Codigo = txtcodigo.Text.Trim();
                objnorma.NOM_Nombre = txtNombre.Text.Trim();
                var entidades = NormaMuestreoBL.Instancia.BuscarPorNombreCodigo(objnorma);
                if (entidades.Count != 0)
                {
                    lblmensaje.Text = "";
                    dgvnormasmuestreo.DataSource = entidades;
                    dgvnormasmuestreo.DataBind();
                }
                else
                {
                    dgvnormasmuestreo.DataSource = null;
                    dgvnormasmuestreo.DataBind();
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
                objnorma.NOM_Codigo = txtcodigo.Text.Trim();
                objnorma.NOM_Nombre = txtNombre.Text.Trim();
                objnorma.NOM_Anio = Convert.ToInt32(txtAnio.Text);
                var entidades = NormaMuestreoBL.Instancia.BuscarPorFiltros(objnorma);
                if (entidades.Count != 0)
                {
                    lblmensaje.Text = "";
                    dgvnormasmuestreo.DataSource = entidades;
                    dgvnormasmuestreo.DataBind();
                }
                else
                {
                    dgvnormasmuestreo.DataSource = null;
                    dgvnormasmuestreo.DataBind();
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
            
            dgvnormasmuestreo.PageIndex = 0;
        }

        private void Eliminar()
        {
            var listaEliminados = new List<int>();

            foreach (GridViewRow row in dgvnormasmuestreo.Rows)
            {
                var chkBox = (CheckBox)row.Cells[5].FindControl("chkSeleccion");

                if (chkBox != null)
                {
                    if (chkBox.Checked)
                    {
                        var hidIdMuestreo = (HiddenField)row.Cells[5].FindControl("hidIdMuestreo");
                        var idmuestreo = Convert.ToInt32(hidIdMuestreo.Value);
                        listaEliminados.Add(idmuestreo);
                    }
                }
            }

            Usuario objusuario = (Usuario)Session[Constantes.sesionUsuario];

            var listamuestreo = new List<NormaMuestreo>();

            foreach (var eliminado in listaEliminados)
            {
                var entidad = NormaMuestreoBL.Instancia.ObtenerDatosPorID(eliminado);
                entidad.NOM_Estado = Constantes.EstadoEliminado;
                entidad.NOM_UsuarioModificacion = objusuario.IDUsuario.ToString();
                entidad.NOM_FechaHoraModificacion = DateTime.Now;

                listamuestreo.Add(entidad);
            }

            try
            {
                foreach (var muestreo in listamuestreo)
                {
                    NormaMuestreoBL.Instancia.Actualizar(muestreo);
                }

                ClientScript.RegisterStartupScript(this.GetType(), "Confirmacion", "$(function(){MostrarMensaje('msjEliminacionOK');});", true);
            }
            catch
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Error", "$(function(){MostrarMensaje('msjErrorEliminar');});", true);
            }
            BuscarPorFiltros(); 
        }      

        
    }
}