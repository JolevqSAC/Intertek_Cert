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
using System.Web.Services;

namespace Intertek.WEB.Mantenimiento
{
    public partial class contrato : PaginaBase
    {
        Contrato objcontrato = new Contrato();
        string accion = "";
        
        protected void Page_Load(object sender, EventArgs e)
        {
                      
            accion = Request.QueryString.Get("accion");
            if (!Page.IsPostBack)
            {
                CargarCliente();
                //CargarEstado();
                if (accion == "M")
                    CargarDatos(Request.QueryString.Get("IDContrato"));
                
            }
            if (Session["ddlIdiomas"].ToString() == "en-US")
            {
                imgGrabar.Src = Resources.generales.imgGrabarUS;
            }  
        }

        protected void btnGrabar_Click(object sender, EventArgs e)
        {
            GuardarDatos();
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("contratoBuscar.aspx");
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void CargarDatos(string idcontrato)
        {
            objcontrato.IDContrato  = Convert.ToInt32(idcontrato);
            var entidad = ContratoBL.Instancia.ObtenerDatosPorID(objcontrato.IDContrato);
            txtDescripcion.Text = entidad.CON_Descripcion;
            txtNumero.Text = entidad.CON_NumReferencia;
            txtMontoMaximo.Text =entidad.CON_MontoMaximo.ToString();
            if (Session["ddlIdiomas"].ToString() == "en-US")
            {
                txtInicio.Text = entidad.CON_FechaInico.Value.ToString("MM/dd/yyyy");
                txtFin.Text = entidad.CON_FechaFin.Value.ToString("MM/dd/yyyy");
            }
            else {
                txtInicio.Text = entidad.CON_FechaInico.Value.ToString("dd/MM/yyyy");
                txtFin.Text = entidad.CON_FechaFin.Value.ToString("dd/MM/yyyy");
            }
           
            //ListItem oListItem = ddltipo.Items.FindByValue(entidad.CON_EstadoContrato);
            //if (oListItem != null)
            //{
            //    ddltipo.SelectedValue = entidad.CON_EstadoContrato;
            //}

            ListItem oListcliente = ddlCliente.Items.FindByValue(entidad.IDCliente.ToString());
            if (oListcliente != null)
            {
                ddlCliente.SelectedValue = entidad.IDCliente.ToString();
            }
            if (Session["ddlIdiomas"].ToString() == "en-US")
            {
                lblTitulo.Text = "Modify Contract";
            }
            else
            {
                lblTitulo.Text = "Modificar Contrato";
            }

        }

        private void GuardarDatos()
        {
            switch (accion)
            {
                case "N":
                    Insertar();
                    break;
                case "M":
                    Modificar();
                    break;
            }
        }

        private void Insertar()
        {
            try
            {
                if (txtInicio.Text != "")
                {
                    Usuario objusuario = (Usuario)Session[Constantes.sesionUsuario];
                    objcontrato.CON_Descripcion = txtDescripcion.Text.Trim();
                    objcontrato.CON_FechaInico = Convert.ToDateTime(txtInicio.Text.Trim());
                    objcontrato.CON_FechaFin = Convert.ToDateTime(txtFin.Text.Trim());
                    objcontrato.CON_EstadoContrato = Resources.generales.estadoContrato;
                    objcontrato.CON_NumReferencia = txtNumero.Text.Trim();
                    objcontrato.CON_MontoMaximo = Convert.ToDecimal(txtMontoMaximo.Text.Trim());
                    objcontrato.IDCliente = ddlCliente.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlCliente.SelectedValue);    
                    objcontrato.CON_Estado = Constantes.EstadoActivo;
                    objcontrato.CON_UsuarioCreacion = objusuario.IDUsuario.ToString();
                    objcontrato.CON_FechaHoraCreacion = DateTime.Now;
                    objcontrato = ContratoBL.Instancia.Insertar(objcontrato);
                    int idcontrato = objcontrato.IDContrato;
                    objcontrato.CON_Codigo = "CON" + idcontrato.ToString().PadLeft(7, '0');
                    ContratoBL.Instancia.Actualizar(objcontrato);
                    LimpiarCampos();
                    ClientScript.RegisterStartupScript(this.GetType(), "Confirmacion", "$(function(){MostrarMensaje('msjRegistroOK');});", true);
                }
            }
            catch
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Error", "$(function(){MostrarMensaje('msjErrorGrabar');});", true);
            }

        }


        private void Modificar()
        {
            try
            {
                Usuario objusuario = (Usuario)Session[Constantes.sesionUsuario];
                objcontrato.IDContrato = Convert.ToInt32(Request.QueryString.Get("IDContrato"));
                var entidad = ContratoBL.Instancia.ObtenerDatosPorID(objcontrato.IDContrato);
                entidad.CON_Descripcion = txtDescripcion.Text.Trim();
                entidad.CON_FechaInico = Convert.ToDateTime(txtInicio.Text.Trim());
                entidad.CON_FechaFin = Convert.ToDateTime(txtFin.Text.Trim());
                entidad.CON_EstadoContrato = Resources.generales.estadoContrato;
                entidad.CON_NumReferencia = txtNumero.Text.Trim();
                entidad.CON_MontoMaximo = Convert.ToDecimal(txtMontoMaximo.Text.Trim());
                entidad.IDCliente = ddlCliente.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlCliente.SelectedValue);
                entidad.CON_UsuaroModificacion = objusuario.IDUsuario.ToString();
                entidad.CON_FechaHoraModificacion = DateTime.Now;
                ContratoBL.Instancia.Actualizar(entidad);
                LimpiarCampos();
                ClientScript.RegisterStartupScript(this.GetType(), "Confirmacion", "$(function(){MostrarMensaje('msjRegistroOK');});", true);
            }
            catch
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Error", "$(function(){MostrarMensaje('msjErrorGrabar');});", true);
            }

        }


        private void LimpiarCampos()
        {
            txtDescripcion.Text = string.Empty;
            txtInicio.Text = string.Empty;
            txtFin.Text = string.Empty;
            txtNumero.Text = string.Empty;
            txtMontoMaximo.Text = string.Empty;
            ddlCliente.SelectedIndex = -1;
        }

        //private void CargarEstado()
        //{
        //    if (Session["ddlIdiomas"].ToString() == "en-US")
        //    {
        //        ddltipo.Items.Insert(0, new ListItem(Resources.generales.textoSelect, "0"));
        //    }
        //    else
        //    {
        //        ddltipo.Items.Insert(0, new ListItem(Resources.generales.textoSeleccionar, "0"));
        //    }
        //    ddltipo.Items.Insert(1, "Activo");
        //    ddltipo.Items.Insert(2, "Inactivo");

        //}

        //[WebMethod]
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
                ddlCliente.Items.Insert(0, new ListItem(Resources.generales.textoSelect, "0"));
            }
            else
            {
                ddlCliente.Items.Insert(0, new ListItem(Resources.generales.textoSeleccionar, "0"));
            }
            //cliente = cliente.ToLower();
            //Cliente objcliente = new Cliente();
            //objcliente.CLI_Estado = Constantes.EstadoActivo;
            //var listcliente = ClienteBL.Instancia.ListarPorRazonSocial(objcliente)
            //.Where(c=>c.CLI_RazonSocial.ToLower().Contains(cliente));
            //return listcliente.ToList();

        }
    }
}