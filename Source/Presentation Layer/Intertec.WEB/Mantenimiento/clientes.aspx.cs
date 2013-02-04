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
    public partial class clientes : PaginaBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CargarDropDownList();
                if (!EsNuevoRegistro())
                {
                    ObtenerDatos(Convert.ToInt32(Request["idCliente"].ToString()));
                }
            }
        }

        protected void btnGrabar_Click(object sender, EventArgs e)
        {
            Usuario objLogin = (Usuario)Session[Constantes.sesionUsuario];
            bool graboOK = false;
            lblMensaje.Text = "";
            string mensajeError = "";

            if (txtRUC.Text != "" && txtRazonSocial.Text!="")
            {
                if (!EsNuevoRegistro())
                {
                    //actualizar
                    Cliente objCliente = ClienteBL.Instancia.ObtenerClienteById(Convert.ToInt32(Request["idCliente"].ToString()));

                    SetearValores(ref objCliente);
                    objCliente.CLI_UsuarioModificacion = objLogin.IDUsuario.ToString();
                    objCliente.CLI_FechaHoraModificacion = DateTime.Now;

                    try
                    {
                        objCliente = ClienteBL.Instancia.Actualizar(objCliente);
                        graboOK = true;
                    }
                    catch (Exception ex)
                    {
                        graboOK = false;
                        mensajeError = "Actualizar" + ex.Message;
                    }
                }
                else
                {
                    //insertar
                    Cliente objCliente = new Cliente();
                    SetearValores(ref objCliente);
                    objCliente.CLI_UsuarioCreacion = objLogin.IDUsuario.ToString();
                    objCliente.CLI_FechaHoraCreacion = DateTime.Now;
                    try
                    {
                        objCliente = ClienteBL.Instancia.Insertar(objCliente);
                        graboOK = true;
                        LimpiarFormulario();
                    }
                    catch (Exception ex)
                    {
                        graboOK = false;
                        mensajeError = "Insertar" + ex.Message;
                    }
                }
            }

            if (graboOK)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "miscriptError", "$(function(){MostrarMensaje('msjSatisfactorio');});", true);
            }
            else
            {
                lblMensaje.Text = mensajeError;
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("clientesBuscar.aspx");
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            if (!EsNuevoRegistro())
            {
                Response.Redirect("clientes.aspx?idCliente=" + Request["idCliente"].ToString());
            }
            else
            {
                Response.Redirect("clientes.aspx");
            }
        }

  

        protected void ddlPais_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPais.SelectedValue != "0")
            {
                CargarDepartamento(Convert.ToInt32(ddlPais.SelectedValue));
            }
        }

        protected void ddlDepartamento_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlDepartamento.SelectedValue != "0")
            {
                CargarProvincias(Convert.ToInt32(ddlDepartamento.SelectedValue));
            }
        }

        protected void ddlProvincia_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlProvincia.SelectedValue != "0")
            {
                CargarDistritos(Convert.ToInt32(ddlProvincia.SelectedValue));
            }
        }

        #region cargarUbigeo
        private void CargarDropDownList()
        {
            ddlPais.DataSource = DistritoBL.Instancia.ListarTodosPaises();
            ddlPais.DataValueField = "IDPais";
            ddlPais.DataTextField = "PAI_Nombre";
            ddlPais.DataBind();
            ddlPais.Items.Insert(0, new ListItem(Resources.generales.textoSeleccionar, "0"));

            ddlDepartamento.Items.Insert(0, new ListItem(Resources.generales.textoSeleccionar, "0"));
            ddlProvincia.Items.Insert(0, new ListItem(Resources.generales.textoSeleccionar, "0"));
            ddlDistrito.Items.Insert(0, new ListItem(Resources.generales.textoSeleccionar, "0"));
        }
        private void CargarDepartamento(int idPais)
        {
            ddlDepartamento.DataSource = DistritoBL.Instancia.ListarTodosDepartamentos(idPais);
            ddlDepartamento.DataValueField = "IDDepartamento";
            ddlDepartamento.DataTextField = "DEP_Nombre";
            ddlDepartamento.DataBind();
            ddlDepartamento.Items.Insert(0, new ListItem(Resources.generales.textoSeleccionar, "0"));
        }

        private void CargarProvincias(int idDepartamento)
        {
            ddlProvincia.DataSource = DistritoBL.Instancia.ListarTodosProvincias(idDepartamento);
            ddlProvincia.DataValueField = "IDProvincia";
            ddlProvincia.DataTextField = "PRO_Nombre";
            ddlProvincia.DataBind();
            ddlProvincia.Items.Insert(0, new ListItem(Resources.generales.textoSeleccionar, "0"));
        }

        private void CargarDistritos(int idProvincia)
        {
            ddlDistrito.DataSource = DistritoBL.Instancia.ListarTodosDistritosByProvincia(idProvincia);
            ddlDistrito.DataValueField = "IDDistrito";
            ddlDistrito.DataTextField = "DIS_Nombre";
            ddlDistrito.DataBind();
            ddlDistrito.Items.Insert(0, new ListItem(Resources.generales.textoSeleccionar, "0"));
        }
        #endregion

        private void ObtenerDatos(int idCliente)
        {
            Cliente objCliente = ClienteBL.Instancia.ObtenerClienteById(idCliente);
            if (objCliente != null)
            {
                //txtContacto.Text = objCliente.CLI_Contacto;
                txtEmail.Text = objCliente.CLI_Correo;
                txtDireccion.Text = objCliente.CLI_Direccion;
                txtFax.Text = objCliente.CLI_Fax;
                txtObservaciones.Text = objCliente.CLI_Observaciones;
                txtRazonSocial.Text = objCliente.CLI_RazonSocial;
                txtRUC.Text = objCliente.CLI_RUC;
                txtTelefono1.Text = objCliente.CLI_Telefono1;
                txtTelefono2.Text = objCliente.CLI_Telefono2;

                //verificamos si tiene dirección
                if (objCliente.IDDistrito != null)
                {
                    Distrito objDistrito = DistritoBL.Instancia.ObtenerDistritoByID(objCliente.IDDistrito.Value);
                    Provincia objProvincia = DistritoBL.Instancia.ObtenerProvinciaByID(objDistrito.IDProvincia.Value);

                    ListItem oListItem = ddlPais.Items.FindByValue(objProvincia.Departamento.IDPais.ToString());
                    if (oListItem != null)
                    {
                        ddlPais.SelectedValue = objProvincia.Departamento.IDPais.ToString();
                        CargarDepartamento(objProvincia.Departamento.IDPais.Value);

                        oListItem = ddlDepartamento.Items.FindByValue(objProvincia.Departamento.IDDepartamento.ToString());
                        if (oListItem != null)
                        {
                            ddlDepartamento.SelectedValue = objProvincia.Departamento.IDDepartamento.ToString();
                            CargarProvincias(objProvincia.Departamento.IDDepartamento);

                            oListItem = ddlProvincia.Items.FindByValue(objProvincia.IDProvincia.ToString());
                            if (oListItem != null)
                            {
                                ddlProvincia.SelectedValue = objProvincia.IDProvincia.ToString();
                                CargarDistritos(objProvincia.IDProvincia);

                                oListItem = ddlDistrito.Items.FindByValue(objDistrito.IDDistrito.ToString());
                                if (oListItem != null)
                                {
                                    ddlDistrito.SelectedValue = objDistrito.IDDistrito.ToString();
                                }
                            }
                        }
                    }

                }
            }

        }

        private void SetearValores(ref Cliente objCliente)
        {
            //objCliente.CLI_Contacto = txtContacto.Text;
            objCliente.CLI_Correo = txtEmail.Text;
            objCliente.CLI_Direccion = txtDireccion.Text;
            objCliente.CLI_Fax = txtFax.Text;
            objCliente.CLI_Observaciones = txtObservaciones.Text;
            objCliente.CLI_RazonSocial = txtRazonSocial.Text;
            objCliente.CLI_RUC = txtRUC.Text;
            objCliente.CLI_Telefono1 = txtTelefono1.Text;
            objCliente.CLI_Telefono2 = txtTelefono2.Text;
            objCliente.IDDistrito = ddlDistrito.SelectedValue != "0" ? Convert.ToInt32(ddlDistrito.SelectedValue) : (int?)null;
            objCliente.CLI_Estado = Constantes.EstadoActivo;
        }

        private void LimpiarFormulario()
        {
            ContentPlaceHolder mpContentPlaceHolder;
            mpContentPlaceHolder = (ContentPlaceHolder)Master.FindControl("MainContent");
            if (mpContentPlaceHolder != null)
            {
                foreach (var obj in mpContentPlaceHolder.Controls.OfType<TextBox>())
                {
                    obj.Text = "";
                }
            }
        }
        private bool EsNuevoRegistro()
        {
            bool esNuevo = true;
            if (Request["idCliente"] != null)
            {
                return false;
            }

            return esNuevo;
        }
    }
}