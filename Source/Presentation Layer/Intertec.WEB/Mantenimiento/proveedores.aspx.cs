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
    public partial class proveedores : PaginaBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CargarDropDownList();
                if (!EsNuevoRegistro())
                {
                    ObtenerDatos(Convert.ToInt32(Request["idProveedor"].ToString()));
                }
            }
        }

        protected void btnGrabar_Click(object sender, EventArgs e)
        {
            Usuario objLogin = (Usuario)Session[Constantes.sesionUsuario];
            bool graboOK = false;
            lblMensaje.Text = "";
            string mensajeError = "";

            if (txtRUC.Text != "" && txtRazonSocial.Text != "")
            {
                if (!EsNuevoRegistro())
                {
                    //actualizar
                    Proveedor objProveedor = ProveedorBL.Instancia.ObtenerProveedorById(Convert.ToInt32(Request["idProveedor"].ToString()));

                    SetearValores(ref objProveedor);
                    objProveedor.PRV_UsuarioModificacion = objLogin.IDUsuario.ToString();
                    objProveedor.PRV_FechaHoraModificacion = DateTime.Now;

                    try
                    {
                        objProveedor = ProveedorBL.Instancia.Actualizar(objProveedor);
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
                    Proveedor objProveedor = new Proveedor();
                    SetearValores(ref objProveedor);
                    objProveedor.PRV_UsuarioCreacion = objLogin.IDUsuario.ToString();
                    objProveedor.PRV_FechaHoraCreacion = DateTime.Now;
                    try
                    {
                        objProveedor = ProveedorBL.Instancia.Insertar(objProveedor);
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
            Response.Redirect("proveedoresBuscar.aspx");
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            if (!EsNuevoRegistro())
            {
                Response.Redirect("proveedores.aspx?idProveedor=" + Request["idProveedor"].ToString());
            }
            else
            {
                Response.Redirect("proveedores.aspx");
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

        private void ObtenerDatos(int idProveedor)
        {
            Proveedor objProveedor = ProveedorBL.Instancia.ObtenerProveedorById(idProveedor);
            if (objProveedor != null)
            {
                //txtContacto.Text = objProveedor.PRV_Contacto;
                txtEmail.Text = objProveedor.PRV_Correo;
                txtDireccion.Text = objProveedor.PRV_Direccion;
                txtFax.Text = objProveedor.PRV_Fax;
                txtObservaciones.Text = objProveedor.PRV_Observaciones;
                txtRazonSocial.Text = objProveedor.PRV_RazonSocial;
                txtRUC.Text = objProveedor.PRV_RUC;
                txtTelefono1.Text = objProveedor.PRV_Telefono1;
                txtTelefono2.Text = objProveedor.PRV_Telefono2;

                //verificamos si tiene dirección
                if (objProveedor.IDDistrito != null)
                {
                    Distrito objDistrito = DistritoBL.Instancia.ObtenerDistritoByID(objProveedor.IDDistrito.Value);
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

        private void SetearValores(ref Proveedor objProveedor)
        {
            //objProveedor.PRV_Contacto = txtContacto.Text;
            objProveedor.PRV_Correo = txtEmail.Text;
            objProveedor.PRV_Direccion = txtDireccion.Text;
            objProveedor.PRV_Fax = txtFax.Text;
            objProveedor.PRV_Observaciones = txtObservaciones.Text;
            objProveedor.PRV_RazonSocial = txtRazonSocial.Text;
            objProveedor.PRV_RUC = txtRUC.Text;
            objProveedor.PRV_Telefono1 = txtTelefono1.Text;
            objProveedor.PRV_Telefono2 = txtTelefono2.Text;
            objProveedor.IDDistrito = ddlDistrito.SelectedValue != "0" ? Convert.ToInt32(ddlDistrito.SelectedValue) : (int?)null;
            objProveedor.PRV_Estado = Constantes.EstadoActivo;
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
            if (Request["idProveedor"] != null)
            {
                return false;
            }

            return esNuevo;
        }
    }
}