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
using System.Globalization;

namespace Intertek.WEB.Mantenimiento
{
    public partial class clientesBuscar : PaginaBase
    {
        public string Cultura { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            Cultura = (string)CultureInfo.CurrentCulture.Name;

            if (!IsPostBack)
            {
                CargarIndicadorArea();
                CargarDatos();
            }


            string Eliminar = HttpContext.GetLocalResourceObject("~/Mantenimiento/clientesBuscar.aspx", "btnEliminarResource1", new System.Globalization.CultureInfo(Cultura)).ToString();

            btnEliminar0.Value = Eliminar;

        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarDatos();
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
           // Response.Redirect("clientesBuscar.aspx");
            txtCodigo.Text = string.Empty;
            txtRazonSocial.Text = string.Empty;
            txtRuc.Text = string.Empty;
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("neoclientes.aspx?accion=N");
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            EliminarCliente();
        }

        protected void gvBuscar_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvBuscar.PageIndex = e.NewPageIndex;

            var datos = Session["cargarClientes"];
            gvBuscar.DataSource = datos;
            gvBuscar.DataBind();
        }

        private void CargarDatos()
        {
            Cliente objCliente = new Cliente();

            objCliente.CLI_Codigo = txtCodigo.Text;
            objCliente.CLI_RazonSocial = txtRazonSocial.Text;
            objCliente.CLI_RUC = txtRuc.Text;
            objCliente.CLI_IndicadorArea = ddlIndicadorArea.SelectedValue;
            //objCliente.CLI_Direccion = txtDireccion.Text;

            IList<Cliente> lstClientes = ClienteBL.Instancia.ListarClientes(objCliente);
            if (lstClientes.Count != 0)
            {                
                gvBuscar.DataSource = lstClientes;
                gvBuscar.DataBind();

                Session["cargarClientes"] = lstClientes;
            }            
            else
            {
                gvBuscar.DataSource = null;
                gvBuscar.DataBind();
                if ((Session["ddlIdiomas"].ToString() == "en-US"))
                {
                    lblmensaje.Text = "No Data Found";
                }
                else
                {
                    lblmensaje.Text = "No Existen Datos Encontrados";
                }

            }
            gvBuscar.PageIndex = 0;     
        }

        private void EliminarCliente()
        {
            Usuario objUsuario = (Usuario)Session[Constantes.sesionUsuario];

            try
            {
                foreach (GridViewRow row in gvBuscar.Rows)
                {
                    CheckBox chbx = (CheckBox)row.FindControl("chkSeleccion");

                    if (chbx != null && chbx.Checked)
                    {
                        HiddenField hidIdCliente = (HiddenField)row.FindControl("hidIdCliente");
                        int idCliente = Convert.ToInt32(hidIdCliente.Value);
                        Cliente objCliente = ClienteBL.Instancia.ObtenerClienteById(idCliente);
                        objCliente.CLI_Estado = Constantes.EstadoEliminado;
                        objCliente.CLI_UsuarioModificacion = objUsuario.IDUsuario.ToString();
                        objCliente.CLI_FechaHoraModificacion = DateTime.Now;
                        ClienteBL.Instancia.Actualizar(objCliente);
                    }
                }

                ClientScript.RegisterStartupScript(this.GetType(), "Confirmacion", "$(function(){MostrarMensaje('msjEliminacionOK');});", true);
            }
            catch
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Error", "$(function(){MostrarMensaje('msjErrorEliminar');});", true);
            }

            CargarDatos();
        }

        protected void gvBuscar_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            switch (e.CommandName)
            {
                case "Editar":
                    Response.Redirect(string.Format("neoclientes.aspx?idCliente={0}&Accion=M", Convert.ToInt32(e.CommandArgument)), true);
                    break;
            }
        }

        private void CargarIndicadorArea()
        {
            if (Session["ddlIdiomas"].ToString() == "en-US")
            {
                ddlIndicadorArea.Items.Insert(0, new ListItem(Resources.generales.testoAll, "0"));
            }
            else
            {
                ddlIndicadorArea.Items.Insert(0, new ListItem(Resources.generales.textoTodos, "0"));
            }
            ddlIndicadorArea.Items.Insert(1, new ListItem(Resources.generales.txtAgri, "A"));
            ddlIndicadorArea.Items.Insert(2, new ListItem(Resources.generales.txtHidro, "H"));
            ddlIndicadorArea.Items.Insert(3, new ListItem(Resources.generales.txtMixto, "M"));
        }
    }
}