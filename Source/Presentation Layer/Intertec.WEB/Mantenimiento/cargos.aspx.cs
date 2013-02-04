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
    public partial class cargos : PaginaBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CargarArea();
                if (Session["ddlIdiomas"].ToString() == "en-US")
                {
                    imgGrabar.Src = Resources.generales.imgGrabarUS;
                }
                if (!EsNuevoRegistro())
                {
                    ObtenerDatos(Convert.ToInt32(Request["idCargo"].ToString()));
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
                ddlarea.Items.Insert(0, new ListItem(Resources.generales.textoSelect, "0"));
            }
            else
            {
                ddlarea.Items.Insert(0, new ListItem(Resources.generales.textoSeleccionar, "0"));
            }
        }
        
        
        protected void btnGrabar_Click(object sender, EventArgs e)
        {
            Usuario objUsuario = (Usuario)Session[Constantes.sesionUsuario];
            bool graboOK = false;
            if (txtNombre.Text != "")
            {
                if (!EsNuevoRegistro())
                {
                    //actualizar
                    Cargo objCargo = CargosBL.Instancia.ObtenerCargoByID(Convert.ToInt32(Request["idCargo"].ToString()));
                    objCargo.CAR_Nombre = txtNombre.Text.Trim();
                    objCargo.CAR_Descripcion = txtDescripcion.Text.Trim();
                    objCargo.IDArea = ddlarea.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlarea.SelectedValue); 
                    objCargo.CAR_UsuarioModificacion = objUsuario.IDUsuario.ToString();
                    objCargo.CAR_FechaHoraModificacion = DateTime.Now;
                    objCargo.CAR_Estado = Constantes.EstadoActivo;
                    try
                    {
                        objCargo = CargosBL.Instancia.Actualizar(objCargo);
                        graboOK = true;
                        txtNombre.Text = "";
                        txtDescripcion.Text = "";
                        ddlarea.SelectedIndex = -1;
                    }
                    catch
                    {
                        graboOK = false;
                    }
                }
                else 
                {
                    //insertar
                    Cargo objCargo = new Cargo();
                    objCargo.CAR_Nombre = txtNombre.Text.Trim();
                    objCargo.CAR_Descripcion = txtDescripcion.Text.Trim();
                    objCargo.IDArea = ddlarea.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlarea.SelectedValue); 
                    objCargo.CAR_UsuarioCreacion = objUsuario.IDUsuario.ToString();
                    objCargo.CAR_FechaHoraCreacion = DateTime.Now;
                    objCargo.CAR_Estado = Constantes.EstadoActivo;
                    try
                    {
                        objCargo = CargosBL.Instancia.Insertar(objCargo);
                        int idcargo = objCargo.IDCargo;
                        objCargo.CAR_Codigo = "CAR" + idcargo.ToString().PadLeft(7, '0');
                        CargosBL.Instancia.Actualizar(objCargo);
                        graboOK = true;
                        txtNombre.Text = "";
                        txtDescripcion.Text = "";
                        ddlarea.SelectedIndex = -1;
                    }
                    catch
                    {
                        graboOK = false;
                    }
                }
              
            }

            if (graboOK)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "miscriptError", "$(function(){MostrarMensaje('msjSatisfactorio');});", true);
            }

        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("cargosBuscar.aspx");
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {            
                Response.Redirect("cargos.aspx");
                ddlarea.SelectedIndex = -1;
          
        }

        private bool EsNuevoRegistro()
        {
            bool esNuevo = true;
            if (Request["idCargo"] != null)
            {
                return false;
            }

            return esNuevo;
        }

        private void ObtenerDatos(int idCargo)
        {

            Cargo objCargo = CargosBL.Instancia.ObtenerCargoByID(idCargo);

            if (objCargo != null)
            {
                txtNombre.Text = objCargo.CAR_Nombre;
                txtDescripcion.Text = objCargo.CAR_Descripcion;
                ListItem oListItem = ddlarea.Items.FindByValue(objCargo.IDArea.ToString());
                if (oListItem != null)
                {
                    ddlarea.SelectedValue = objCargo.IDArea.ToString();
                }
            }
            if (Session["ddlIdiomas"].ToString() == "en-US")
            {
                lblTitulo.Text = "Modify Charge";
            }
            else
            {
                lblTitulo.Text = "Modificar Cargo";
            }
        }
    }
}