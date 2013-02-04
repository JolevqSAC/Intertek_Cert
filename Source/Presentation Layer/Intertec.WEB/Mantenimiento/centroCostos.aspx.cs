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
    public partial class centroCostos : PaginaBase
    {
        CentroCosto objCentroCosto = new CentroCosto();
        string accion = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            accion = Request.QueryString.Get("accion");
            if (!Page.IsPostBack)
            {
                CargarArea();
                if (Session["ddlIdiomas"].ToString() == "en-US")
                {
                    imgGrabar.Src = Resources.generales.imgGrabarUS;
                }
                if(accion == "M")
                    CargarDatos(Request.QueryString.Get("IDCentroCosto"));
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
                ddlarea.Items.Insert(0, new ListItem(Resources.generales.textoSelect, "0"));
            }
            else
            {
                ddlarea.Items.Insert(0, new ListItem(Resources.generales.textoSeleccionar, "0"));
            }
        }

        private void CargarDatos(string idcentrocosto)
        {
            var entidad = CentroCostoBL.Instancia.ObtenerDatosPorID(Convert.ToInt32(idcentrocosto));
            txtNumero.Text = entidad.CCO_Numero;
            ddlarea.SelectedValue = entidad.IDArea.ToString();
            if (Session["ddlIdiomas"].ToString() == "en-US")
            {
                lblTitulo.Text = "Modify Cost Center";
            }
            else
            {
                lblTitulo.Text = "Modificar Centro de Costo";
            }
        }

        protected void btnGrabar_Click(object sender, EventArgs e)
        {
            GuardarDatosCentroCosto();
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("centroCostosBuscar.aspx");
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void GuardarDatosCentroCosto()
        {
            switch (accion)
            {
                case "N":
                    InsertarCentroCosto();
                    break;
                case "M":
                    ModificarCentroCosto();
                    break;
            }
        }

        private void InsertarCentroCosto()
        {
                try
                {
                    if (txtNumero.Text != "")
                    {
                        Usuario objusuario = (Usuario)Session[Constantes.sesionUsuario];
                        objCentroCosto.CCO_Numero = txtNumero.Text.Trim();
                        objCentroCosto.IDArea = ddlarea.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlarea.SelectedValue); 
                        objCentroCosto.CCO_Estado = Constantes.EstadoActivo;
                        objCentroCosto.CCO_UsuarioCreacion = objusuario.IDUsuario.ToString();
                        objCentroCosto.CCO_FechaHoraCreacion = DateTime.Now;
                        objCentroCosto = CentroCostoBL.Instancia.Insertar(objCentroCosto);
                        int idcentrocosto = objCentroCosto.IDCentroCosto;
                        objCentroCosto.CCO_Codigo = "CCO" + idcentrocosto.ToString().PadLeft(7, '0');
                        CentroCostoBL.Instancia.Actualizar(objCentroCosto);
                        LimpiarCampos();
                        ClientScript.RegisterStartupScript(this.GetType(), "Confirmacion", "$(function(){MostrarMensaje('msjRegistroOK');});", true);
                    }
                }
                catch
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Error", "$(function(){MostrarMensaje('msjErrorGrabar');});", true);
                }
              
        }


        private void ModificarCentroCosto()
        {
            Usuario objusuario = (Usuario)Session[Constantes.sesionUsuario];
            
            var entidad = CentroCostoBL.Instancia.ObtenerDatosPorID(Convert.ToInt32( Request.QueryString.Get("idcentrocosto")));
            entidad.CCO_Numero = txtNumero.Text;
            entidad.IDArea = ddlarea.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlarea.SelectedValue); 
            entidad.CCO_UsuarioModificacion = objusuario.IDUsuario.ToString();
            entidad.CCO_FechaHoraModificacion = DateTime.Now;
            try
            {
                CentroCostoBL.Instancia.Actualizar(entidad);
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
                this.txtNumero.Text = " ";
                this.ddlarea.SelectedIndex = -1;
        }
        
    }
}