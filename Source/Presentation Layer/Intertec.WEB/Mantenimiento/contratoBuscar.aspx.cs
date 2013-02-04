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
    public partial class contratoBuscar : PaginaBase
    {
        Contrato objcontrato = new Contrato();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //CargarEstado();
                CargarCliente();
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

        //private void CargarEstado()
        //{
        //    if (Session["ddlIdiomas"].ToString() == "en-US")
        //    {
        //        ddltipo.Items.Insert(0, new ListItem(Resources.generales.testoAll, "0"));
        //    }
        //    else
        //    {
        //        ddltipo.Items.Insert(0, new ListItem(Resources.generales.textoTodos, "0"));
        //    }
        //    ddltipo.Items.Insert(1, "Activo");
        //    ddltipo.Items.Insert(2, "Inactivo");

        //}

        private void CargarCliente()
        {
            Cliente objcliente = new Cliente();
            objcliente.CLI_Estado = Constantes.EstadoActivo;
            ddlCliente.DataSource = ClienteBL.Instancia.ListarPorRazonSocial(objcliente);
            ddlCliente.DataValueField = "IDCliente";
            ddlCliente.DataTextField = "CLI_RazonSocial";
            ddlCliente.DataBind();
            if (Session["ddlIdiomas"].ToString() == "en-US")
            {
                ddlCliente.Items.Insert(0, new ListItem(Resources.generales.testoAll, "0"));
            }
            else
            {
                ddlCliente.Items.Insert(0, new ListItem(Resources.generales.textoTodos, "0"));
            }
        }

        protected void btnlimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("contrato.aspx?accion=N");
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            Eliminar();
        }

        private void LlenarGridview()
        {
            objcontrato.CON_Estado = Constantes.EstadoActivo;
            var entidades = ContratoBL.Instancia.ListarTodosActivos(objcontrato);
            var datos = (from x in entidades
                         select new
                         {
                             x.IDContrato,
                             CON_Codigo = x.CON_Codigo,
                             CON_Descripcion = x.CON_Descripcion,
                             CON_FechaInico = x.CON_FechaFin == null ? "" : Convert.ToString(x.CON_FechaInico.Value.ToString("dd/MM/yyyy")),
                             CON_FechaFin = x.CON_FechaFin == null ? "" : Convert.ToString(x.CON_FechaFin.Value.ToString("dd/MM/yyyy")),
                             CON_EstadoContrato = x.CON_EstadoContrato,
                             CLI_RazonSocial = x.Cliente == null ? "" : x.Cliente.CLI_RazonSocial
                         }).ToList();
            dgvbuscar.DataSource = datos;
            dgvbuscar.DataBind();
        }

        protected void dgvbuscar_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvbuscar.EditIndex = -1;
            dgvbuscar.PageIndex = e.NewPageIndex;
            BuscarPorFiltros();
        }

        protected void dgvbuscar_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Editar":
                    Response.Redirect(string.Format("contrato.aspx?IDContrato={0}&Accion=M", Convert.ToInt32(e.CommandArgument)), true);
                    break;
            }
        }

        private void LimpiarCampos()
        {
            this.txtcodigo.Text = string.Empty;
            this.txtDescripcion.Text = string.Empty;
            this.txtInicio.Text = string.Empty;
            this.txtFin.Text = string.Empty;
            this.lblmensaje.Text = string.Empty;
            this.txtNumero.Text = string.Empty;
            //ddltipo.SelectedIndex = -1;
            ddlCliente.SelectedIndex = -1;
            BuscarPorFiltros();
        }

        private void BuscarPorFiltros()
        {           
                objcontrato.CON_Codigo = txtcodigo.Text.Trim();
                objcontrato.CON_Descripcion = txtDescripcion.Text.Trim();
                objcontrato.CON_FechaInico = txtInicio.Text == "" ? (DateTime?)null : Convert.ToDateTime(txtInicio.Text.Trim());
                objcontrato.CON_FechaFin = txtFin.Text== "" ?(DateTime?)null: Convert.ToDateTime(txtFin.Text.Trim());
                
                objcontrato.CON_NumReferencia = txtNumero.Text.Trim();
                objcontrato.IDCliente = ddlCliente.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlCliente.SelectedValue);

                var entidades = ContratoBL.Instancia.BuscarPorFiltros(objcontrato);
                if (entidades.Count != 0)
                {
                    lblmensaje.Text = "";
                    var datos = (from x in entidades
                                 select new
                                 {
                                     x.IDContrato,
                                     CON_Codigo = x.CON_Codigo,
                                     CON_Descripcion = x.CON_Descripcion,
                                     CON_FechaInico = x.CON_FechaFin == null ? "" : Convert.ToString(x.CON_FechaInico.Value.ToString("dd/MM/yyyy")),
                                     CON_FechaFin = x.CON_FechaFin == null ? "" : Convert.ToString(x.CON_FechaFin.Value.ToString("dd/MM/yyyy")),
                                     CON_EstadoContrato = x.CON_EstadoContrato,
                                     CON_NumReferencia=x.CON_NumReferencia,
                                     CON_MontoMaximo = x.CON_MontoMaximo,
                                     CLI_RazonSocial = x.Cliente == null ? "" : x.Cliente.CLI_RazonSocial
                                 }).ToList();
                    dgvbuscar.DataSource = datos;
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

                  

        private void Eliminar()
        {
            var listaEliminados = new List<int>();

            foreach (GridViewRow row in dgvbuscar.Rows)
            {
                var chkBox = (CheckBox)row.FindControl("chkSeleccion");

                if (chkBox != null)
                {
                    if (chkBox.Checked)
                    {
                        var hidIdContrato = (HiddenField)row.FindControl("hidIdContrato");
                        var idmuestreo = Convert.ToInt32(hidIdContrato.Value);
                        listaEliminados.Add(idmuestreo);
                    }
                }
            }

            Usuario objusuario = (Usuario)Session[Constantes.sesionUsuario];

            var listacontrato = new List<Contrato>();

            foreach (var eliminado in listaEliminados)
            {
                var entidad = ContratoBL.Instancia.ObtenerDatosPorID(eliminado);
                entidad.CON_Estado = Constantes.EstadoEliminado;
                entidad.CON_UsuaroModificacion = objusuario.IDUsuario.ToString();
                entidad.CON_FechaHoraModificacion = DateTime.Now;
                listacontrato.Add(entidad);
            }

            try
            {
                foreach (var muestreo in listacontrato)
                {
                    ContratoBL.Instancia.Actualizar(muestreo);
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