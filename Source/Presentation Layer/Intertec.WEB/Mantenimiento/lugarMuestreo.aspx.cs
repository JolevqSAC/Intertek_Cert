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

namespace Intertek.WEB.Mantenimiento
{
    public partial class lugarMuestreo : PaginaBase
    {
        LugarMuestreo objlugarmuestro = new LugarMuestreo();
        string accion = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            accion = Request.QueryString.Get("accion");
            if (!Page.IsPostBack)
            {
                CargarCliente();
                if (accion == "M")
                    CargarDatos(Request.QueryString.Get("IDLugarMuestreo"));
            }
        }

        protected void btnGrabar_Click(object sender, EventArgs e)
        {
            GuardarDatos();
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("lugarMuestreoBuscar.aspx");
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void CargarCliente()
        {
            Cliente objcliente = new Cliente();
            objcliente.CLI_Estado = Constantes.EstadoActivo;
            ddlCliente.DataSource = ClienteBL.Instancia.ListarPorRazonSocial(objcliente);
            ddlCliente.DataValueField = "IDCliente";
            ddlCliente.DataTextField = "CLI_RazonSocial";
            ddlCliente.DataBind();
            ddlCliente.Items.Insert(0, new ListItem(Resources.generales.textoSeleccionar, "0"));
        }

        private void CargarDatos(string idlugarmuestreo)
        {
            objlugarmuestro.IDLugarMuestreo = Convert.ToInt32(idlugarmuestreo);
            var entidad = LugarMuestreoBL.Instancia.ObtenerDatosPorID(objlugarmuestro);
            txtContacto.Text = entidad.LUM_Contacto;
            txtDireccion.Text = entidad.LUM_Direccion;
            txtTelefono.Text = entidad.LUM_Telefono;
            txtObservaciones.Text = entidad.LUM_Observaciones;
            ListItem oListItem = ddlCliente.Items.FindByValue(entidad.IDCliente.ToString());
            if (oListItem != null)
            {
                ddlCliente.SelectedValue = entidad.IDCliente.ToString();
            }

        }

        private void GuardarDatos()
        {
            switch (accion)
            {
                case "N":
                    InsertarLugarMuestreo();
                    break;
                case "M":
                    ModificarLugarMuestreo();
                    break;
            }
        }
        private void InsertarLugarMuestreo()
        {
            try
            {                
                    Usuario objusuario = (Usuario)Session[Constantes.sesionUsuario];
                    objlugarmuestro.IDCliente = ddlCliente.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlCliente.SelectedValue);    
                    objlugarmuestro.LUM_Contacto = txtContacto.Text.Trim();                    
                    objlugarmuestro.LUM_Direccion = txtDireccion.Text.Trim();
                    objlugarmuestro.LUM_Telefono = txtTelefono.Text.Trim();
                    objlugarmuestro.LUM_Observaciones = txtObservaciones.Text.Trim();
                    objlugarmuestro.LUM_Estado = Constantes.EstadoActivo;
                    objlugarmuestro.LUM_UsuarioCreacion = objusuario.IDUsuario.ToString();
                    objlugarmuestro.LUM_FechaHoraCreacion = DateTime.Now;
                    objlugarmuestro=LugarMuestreoBL.Instancia.Insertar(objlugarmuestro);
                    LimpiarCampos();
                    ClientScript.RegisterStartupScript(this.GetType(), "Confirmacion", "$(function(){MostrarMensaje('msjRegistroOK');});", true);
                
            }
            catch
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Error", "$(function(){MostrarMensaje('msjErrorGrabar');});", true);
            }

        }


        private void ModificarLugarMuestreo()
        {
            try
            {
                Usuario objusuario = (Usuario)Session[Constantes.sesionUsuario];
                objlugarmuestro.IDLugarMuestreo = Convert.ToInt32(Request.QueryString.Get("IDLugarMuestreo"));
                var entidad = LugarMuestreoBL.Instancia.ObtenerDatosPorID(objlugarmuestro);
                entidad.IDCliente = ddlCliente.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlCliente.SelectedValue);
                entidad.LUM_Contacto = txtContacto.Text.Trim();
                entidad.LUM_Direccion = txtDireccion.Text.Trim();
                entidad.LUM_Telefono = txtTelefono.Text.Trim();
                entidad.LUM_Observaciones = txtObservaciones.Text.Trim();                
                entidad.LUM_UsuarioModificacion = objusuario.IDUsuario.ToString();
                entidad.LUM_FechaHoraModificacion = DateTime.Now;
                entidad=LugarMuestreoBL.Instancia.Actualizar(entidad);
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
            if (accion == "M")
                CargarDatos(Request.QueryString.Get("IDLugarMuestreo"));
            else
            {
                txtContacto.Text = " ";
                txtDireccion.Text = " ";
                txtTelefono.Text = " ";
                txtObservaciones.Text = " ";
                ddlCliente.SelectedIndex = -1;
            }
        }
    }
}