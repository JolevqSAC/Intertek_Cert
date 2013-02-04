using System;
using System.Collections.Generic;
using System.Data;
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
    public partial class centroCostosBuscar : PaginaBase
    {
        CentroCosto objCentroCosto = new CentroCosto();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                txtCodigo.Focus();
                CargarArea();
                BuscarPorFiltros();
                if (Session["ddlIdiomas"].ToString() == "en-US")
                {
                    imgeliminar.Src = Resources.generales.imgEliminarUS;
                }
            }
        }

        private void CargarArea()
        {
            Area objarea = new Area();
            ddlarea.DataSource = AreaBL.Instancia.ListarTodosAreas();
            ddlarea.DataValueField = "IDArea";
            ddlarea.DataTextField = "ARE_Nombre";
            ddlarea.DataBind();
            if (Session["ddlIdiomas"].ToString() == "en-US")
            {
                ddlarea.Items.Insert(0, new ListItem(Resources.generales.testoAll, "0"));
            }
            else
            {
                ddlarea.Items.Insert(0, new ListItem(Resources.generales.textoTodos, "0"));
            }
        }
        private void LlenarGridview()
        {
            objCentroCosto.CCO_Estado=Constantes.EstadoActivo;
            dgvcentrocostos.DataSource = CentroCostoBL.Instancia.ListarTodosActivos(objCentroCosto);
            dgvcentrocostos.DataBind();
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            BuscarPorFiltros();
        }
       
        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();   
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("centroCostos.aspx?accion=N");
        }

        private void LimpiarCampos()
        {
            this.txtNumero.Text = "";
            this.txtCodigo.Text = "";
            this.lblmensaje.Text = "";
            this.ddlarea.SelectedIndex = -1;
            BuscarPorFiltros();
        }

        protected void dgvcentrocostos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.dgvcentrocostos.EditIndex = -1;
            this.dgvcentrocostos.PageIndex = e.NewPageIndex;
            BuscarPorFiltros();
        }

        private void BuscarPorFiltros()
        {
            objCentroCosto.CCO_Codigo = txtCodigo.Text.Trim();
            objCentroCosto.CCO_Numero = txtNumero.Text.Trim();
            objCentroCosto.IDArea = ddlarea.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlarea.SelectedValue);
            objCentroCosto.CCO_Estado = Constantes.EstadoActivo;
            var entidades = CentroCostoBL.Instancia.BuscarPorFiltros(objCentroCosto);
            if (entidades.Count!= 0)
            {
                lblmensaje.Text = "";
                var datos = (from x in entidades
                             select new
                             {
                                 x.IDCentroCosto,
                                 CCO_Codigo = x.CCO_Codigo == null ? "" : x.CCO_Codigo,
                                 CCO_Numero = x.CCO_Numero == null ? "" : x.CCO_Numero,
                                 ARE_Nombre = x.Area == null ? "" : x.Area.ARE_Nombre
                             }).ToList();
                dgvcentrocostos.DataSource = datos;
                dgvcentrocostos.DataBind();                             
            }
            else
            {
                dgvcentrocostos.DataSource = null;
                dgvcentrocostos.DataBind();
                if ((Session["ddlIdiomas"].ToString() == "en-US"))
                {
                    lblmensaje.Text = "No Data Found";
                }
                else
                {
                    lblmensaje.Text = "No Existen Datos Encontrados";
                }
            }
            dgvcentrocostos.PageIndex = 0;
        }
        
        
        protected void dgvcentrocostos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Editar":
                    Response.Redirect(string.Format("centroCostos.aspx?IDCentroCosto={0}&Accion=M", Convert.ToInt32(e.CommandArgument)), true);
                    break;                
            }
        }

        private void EliminarRegistros()
        {
            var listaEliminados = new List<int>();

            foreach (GridViewRow row in dgvcentrocostos.Rows)
            {
                var chkBox = (CheckBox)row.Cells[3].FindControl("chkSeleccion");

                if (chkBox != null)
                {
                    if (chkBox.Checked)
                    {
                        var hidIdCentro = (HiddenField)row.Cells[3].FindControl("hidIdCentroCosto");
                        var idCentro = Convert.ToInt32(hidIdCentro.Value);
                        listaEliminados.Add(idCentro);
                    }
                }
            }

            Usuario objusuario = (Usuario)Session[Constantes.sesionUsuario];

            var listaCentros = new List<CentroCosto>();

            foreach (var eliminado in listaEliminados)
            {
                var entidad = CentroCostoBL.Instancia.ObtenerDatosPorID(eliminado);
                entidad.CCO_Estado = Constantes.EstadoEliminado;
                entidad.CCO_UsuarioModificacion = objusuario.IDUsuario.ToString();
                entidad.CCO_FechaHoraModificacion = DateTime.Now;

                listaCentros.Add(entidad);
            }

            try
            {
                foreach (var centro in listaCentros)
                {
                    CentroCostoBL.Instancia.Actualizar(centro);
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