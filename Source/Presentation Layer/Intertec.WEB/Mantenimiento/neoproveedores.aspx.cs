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
    public partial class neoproveedores : PaginaBase
    {
        string accion = string.Empty;
        string Cultura;

        private  List<ContactoProveedor> items { get; set; }
        private List<DireccionProveedor> items2 { get; set; }
     

        protected void Page_Load(object sender, EventArgs e)

        {
           
           
             Cultura = (string)CultureInfo.CurrentCulture.Name;

           

            if (!Page.IsPostBack)
            {
             

                accion = Request.QueryString.Get("accion");

              
                string Eliminar = HttpContext.GetLocalResourceObject("~/Mantenimiento/neoproveedores.aspx", "btnQuitar2ContactoResource1", new System.Globalization.CultureInfo(Cultura)).ToString();

                btnEliminar0.Value = Eliminar;
                btnEliminar3.Value = Eliminar;

                opciones();

                CargarDropDownList();
                if (Session["ddlIdiomas"].ToString() == "en-US")
                {
                    imgGrabar.Src = Resources.generales.imgGrabarUS;
                }
                if (!EsNuevoRegistro())
                {
                    ObtenerDatos(Convert.ToInt32(Request["idProveedor"].ToString()));
                  
                    if (Cultura.Equals("en-US"))
                    {
                        lblTitulo.Text = "Modify Supplier";
                    }
                    else
                    {
                        lblTitulo.Text = "Modificar Proveedor";
                    }

                  
                }
                else
                {
                   
                    
                    if (Cultura.Equals("en-US"))
                    {
                        lblTitulo.Text = "Register Supplier";
                    }
                    else
                    {
                        lblTitulo.Text = "Registrar Proveedor";
                    }
                   
                    
                }
            }
        }



  

        private void cargarContactoProveedor()
        {
            IList<ContactoProveedor> lstContactoProveedor = ContactoProveedorBL.Instancia.ObtenerContactosProveedor(0);
            Session["snContactoProveedor"] = lstContactoProveedor;
        }
        private void EnlazarGrilla()
        {
            if (Session["snContactoProveedor"] == null)
            {
                cargarContactoProveedor();
            }
            items = (List<ContactoProveedor>)Session["snContactoProveedor"];
            grvwContacto.DataSource = items;
            grvwContacto.DataBind();
        
        }

        private void cargarDireccionProveedor()
        {
            IList<DireccionProveedor> lstDireccionProveedor = DireccionProveedorBL.Instancia.ObtenerDireccionesProveedor(0);
            Session["snDireccionProveedor"] = lstDireccionProveedor;
        }
        private void EnlazarGrillaDireccion()
        {
            if (Session["snDireccionProveedor"] == null)
            {
                cargarDireccionProveedor();
            }
            items2 = (List<DireccionProveedor>)Session["snDireccionProveedor"];
            grvwDireccion.DataSource = items;
            grvwDireccion.DataBind();

        }





        // IList<DireccionProveedor> lstDireccionProveedor = DireccionProveedorBL.Instancia.ObtenerDireccionesProveedor(0);
        // Session["snContactoProveedor"] = lstContactoProveedor;
        // Session["snDireccionProveedor"] = lstDireccionProveedor;


        private void opciones()
        {
            switch (accion)
            {
                case "N":
                    EnlazarGrilla();
                    EnlazarGrillaDireccion();
                    break;
                case "M":
                   // ModificarLaboratorio();
                    break;
            }
        }


        private bool validar()
        {
            bool rpt = false;
            if (txtEmail.Text == string.Empty)
            {
                rpt = true;
            }
            else
            {
                string email_format = "^\\w+([-._]\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*$";

                if (System.Text.RegularExpressions.Regex.IsMatch(txtEmail.Text, email_format))
                {
                    //rpt = rpt && true;
                    rpt = true;
                }
                else

                    rpt = false;
            }



            return rpt;
        }




        protected void btnGrabar_Click(object sender, EventArgs e)
        {

          

            if (validar())
            {

                lblerrorEmail.Visible = false;

                Usuario objLogin = (Usuario)Session[Constantes.sesionUsuario];
                bool graboOK = false;
                lblMensaje.Text = string.Empty;
                string mensajeError = string.Empty;
                int idProveedor = 0;

                IList<ContactoProveedor> lstContactoProveedor = (IList<ContactoProveedor>)Session["snContactoProveedor"];
                IList<DireccionProveedor> lstDireccionProveedor = (IList<DireccionProveedor>)Session["snDireccionProveedor"];

                if (txtRUC.Text != "" && txtRazonSocial.Text != "")
                {
                    if (!EsNuevoRegistro())
                    {
                        //actualizar
                        IList<ContactoProveedor> lstContactoProveedorOld = ContactoProveedorBL.Instancia.ObtenerContactosProveedor(Convert.ToInt32(Request["idProveedor"].ToString()));
                        IList<DireccionProveedor> lstDireccionProveedorOld = DireccionProveedorBL.Instancia.ObtenerDireccionesProveedor(Convert.ToInt32(Request["idProveedor"].ToString()));
                        Proveedor objProveedor = ProveedorBL.Instancia.ObtenerProveedorById(Convert.ToInt32(Request["idProveedor"].ToString()));
                        idProveedor = objProveedor.IDProveedor;
                        SetearValores(ref objProveedor);
                        objProveedor.PRV_UsuarioModificacion = objLogin.IDUsuario.ToString();
                        objProveedor.PRV_FechaHoraModificacion = DateTime.Now;

                        try
                        {
                            ProveedorBL.Instancia.Actualizar(objProveedor);

                            for (int j = 0; j < lstContactoProveedorOld.Count; j++)
                            {
                                ContactoProveedor objContactoProveedor = ContactoProveedorBL.Instancia.ObtenerContactoProveedorById(lstContactoProveedorOld[j].IDContactoProveedor);
                                ContactoProveedorBL.Instancia.Eliminar(objContactoProveedor);
                            }

                            for (int j = 0; j < lstDireccionProveedorOld.Count; j++)
                            {
                                DireccionProveedor objDireccionProveedor = DireccionProveedorBL.Instancia.ObtenerDireccionProveedorById(lstDireccionProveedorOld[j].IDDireccionProveedor);
                                DireccionProveedorBL.Instancia.Eliminar(objDireccionProveedor);
                            }

                            for (int j = 0; j < lstContactoProveedor.Count; j++)
                            {
                                ContactoProveedor objContactoProveedor = new ContactoProveedor();
                                objContactoProveedor.COC_Nombres = lstContactoProveedor[j].COC_Nombres.ToString();
                                objContactoProveedor.COC_Apellidos = lstContactoProveedor[j].COC_Apellidos.ToString();
                                objContactoProveedor.COC_Cargo = lstContactoProveedor[j].COC_Cargo.ToString();
                                objContactoProveedor.COC_Telefono1 = lstContactoProveedor[j].COC_Telefono1.ToString();
                                objContactoProveedor.COC_Telefono2 = lstContactoProveedor[j].COC_Telefono2.ToString();
                                objContactoProveedor.COC_Estado = Constantes.EstadoActivo;
                                objContactoProveedor.IDProveedor = idProveedor;
                                objContactoProveedor.COC_UsuarioCreacion = objLogin.IDUsuario.ToString();
                                objContactoProveedor.COC_FechaHoraCreacion = DateTime.Now;
                                ContactoProveedorBL.Instancia.Insertar(objContactoProveedor);
                            }

                            for (int j = 0; j < lstDireccionProveedor.Count; j++)
                            {
                                DireccionProveedor objDireccionProveedor = new DireccionProveedor();
                                objDireccionProveedor.DIP_Tipo = lstDireccionProveedor[j].DIP_Tipo.ToString();
                                objDireccionProveedor.DIP_Descripcion = lstDireccionProveedor[j].DIP_Descripcion.ToString();
                                objDireccionProveedor.DIP_Estado = Constantes.EstadoActivo;
                                objDireccionProveedor.IDProveedor = idProveedor;
                                objDireccionProveedor.DIP_UsuarioCreacion = objLogin.IDUsuario.ToString();
                                objDireccionProveedor.DIP_FechaHoraCreacion = DateTime.Now;
                                DireccionProveedorBL.Instancia.Insertar(objDireccionProveedor);
                            }

                            graboOK = true;
                            LimpiarFormulario();
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
                            idProveedor = objProveedor.IDProveedor;
                            objProveedor.PRV_Codigo = "PRV" + idProveedor.ToString().PadLeft(7, '0');
                            ProveedorBL.Instancia.Actualizar(objProveedor);

                            if (lstContactoProveedor != null)
                            {
                                for (int j = 0; j < lstContactoProveedor.Count; j++)
                                {
                                    ContactoProveedor objContactoProveedor = new ContactoProveedor();
                                    objContactoProveedor.COC_Nombres = lstContactoProveedor[j].COC_Nombres.ToString();
                                    objContactoProveedor.COC_Apellidos = lstContactoProveedor[j].COC_Apellidos.ToString();
                                    objContactoProveedor.COC_Cargo = lstContactoProveedor[j].COC_Cargo.ToString();
                                    objContactoProveedor.COC_Telefono1 = lstContactoProveedor[j].COC_Telefono1.ToString();
                                    objContactoProveedor.COC_Telefono2 = lstContactoProveedor[j].COC_Telefono2.ToString();
                                    objContactoProveedor.COC_Estado = Constantes.EstadoActivo;
                                    objContactoProveedor.IDProveedor = idProveedor;
                                    objContactoProveedor.COC_UsuarioCreacion = objLogin.IDUsuario.ToString();
                                    objContactoProveedor.COC_FechaHoraCreacion = DateTime.Now;
                                    ContactoProveedorBL.Instancia.Insertar(objContactoProveedor);
                                }
                            }

                            if (lstDireccionProveedor != null)
                            {
                                for (int j = 0; j < lstDireccionProveedor.Count; j++)
                                {
                                    DireccionProveedor objDireccionProveedor = new DireccionProveedor();
                                    objDireccionProveedor.DIP_Tipo = lstDireccionProveedor[j].DIP_Tipo.ToString();
                                    objDireccionProveedor.DIP_Descripcion = lstDireccionProveedor[j].DIP_Descripcion.ToString();
                                    objDireccionProveedor.DIP_Estado = Constantes.EstadoActivo;
                                    objDireccionProveedor.IDProveedor = idProveedor;
                                    objDireccionProveedor.DIP_UsuarioCreacion = objLogin.IDUsuario.ToString();
                                    objDireccionProveedor.DIP_FechaHoraCreacion = DateTime.Now;
                                    DireccionProveedorBL.Instancia.Insertar(objDireccionProveedor);
                                }
                            }

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
                    Session["snContactoProveedor"] = null;
                    Session["snDireccionProveedor"] = null;
                    ClientScript.RegisterStartupScript(this.GetType(), "miscriptError", "$(function(){MostrarMensaje('msjSatisfactorio');});", true);
                }
                else
                {
                    Session["snContactoProveedor"] = null;
                    Session["snDireccionProveedor"] = null;
                    lblMensaje.Text = mensajeError;
                }



            }
            else
            {

                lblerrorEmail.Visible = true;
            }






        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Session["snContactoProveedor"] = null;
            Session["snDireccionProveedor"] = null;
            Response.Redirect("proveedoresBuscar.aspx");
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        private void limpair()
        {
            txtEmail.Text = string.Empty;
            txtDireccion.Text = string.Empty;
            txtFax.Text = string.Empty;
            txtObservaciones.Text = string.Empty;
            txtRazonSocial.Text = string.Empty;
            txtRUC.Text = string.Empty;
            txtTelefono1.Text = string.Empty;
            txtTelefono2.Text = string.Empty;
            ddlDepartamento.SelectedIndex = -1;
            ddlDistrito.SelectedIndex = -1;
            ddlPais.SelectedIndex = -1;
            ddlProvincia.SelectedIndex = -1;
            drdwlsTipo.SelectedIndex = -1;
            rbIndicadorArea.SelectedIndex = -1;
        }

        private void CargarDropDownList()
        {
            ddlPais.DataSource = DistritoBL.Instancia.ListarTodosPaises();
            ddlPais.DataValueField = "IDPais";
            ddlPais.DataTextField = "PAI_Nombre";
            ddlPais.DataBind();
           

            if (Cultura.Equals("en-US"))
            {
                ddlPais.Items.Insert(0, new ListItem(Resources.generales.textoSelect, "0"));
            }
            else
            {
                ddlPais.Items.Insert(0, new ListItem(Resources.generales.textoSeleccionar, "0"));
            }
          
            if (Cultura.Equals("en-US"))
            {
                ddlDepartamento.Items.Insert(0, new ListItem(Resources.generales.textoSelect, "0"));
            }
            else
            {
                ddlDepartamento.Items.Insert(0, new ListItem(Resources.generales.textoSeleccionar, "0"));
            }

            if (Cultura.Equals("en-US"))
            {
                ddlProvincia.Items.Insert(0, new ListItem(Resources.generales.textoSelect, "0"));
            }
            else
            {
                ddlProvincia.Items.Insert(0, new ListItem(Resources.generales.textoSeleccionar, "0"));
            }

            if (Cultura.Equals("en-US"))
            {
                ddlDistrito.Items.Insert(0, new ListItem(Resources.generales.textoSelect, "0"));
            }
            else
            {
                ddlDistrito.Items.Insert(0, new ListItem(Resources.generales.textoSeleccionar, "0"));
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

        private void ObtenerDatos(int idProveedor)
        {
            Proveedor objProveedor = ProveedorBL.Instancia.ObtenerProveedorById(idProveedor);
            IList<ContactoProveedor> lstContactoProveedor = ContactoProveedorBL.Instancia.ObtenerContactosProveedor(idProveedor);
            IList<DireccionProveedor> lstDireccionProveedor = DireccionProveedorBL.Instancia.ObtenerDireccionesProveedor(idProveedor);
            Session["snContactoProveedor"] = lstContactoProveedor;
            Session["snDireccionProveedor"] = lstDireccionProveedor;

            if (objProveedor != null)
            {
                //txtContacto.Text = objProveedor.PRV_Contacto;
                hdflIDProveedor.Value = objProveedor.IDProveedor.ToString();
                txtEmail.Text = objProveedor.PRV_Correo;
                txtDireccion.Text = objProveedor.PRV_Direccion;
                txtFax.Text = objProveedor.PRV_Fax;
                txtObservaciones.Text = objProveedor.PRV_Observaciones;
                txtRazonSocial.Text = objProveedor.PRV_RazonSocial;
                txtRUC.Text = objProveedor.PRV_RUC;
                txtTelefono1.Text = objProveedor.PRV_Telefono1;
                txtTelefono2.Text = objProveedor.PRV_Telefono2;
                rbIndicadorArea.SelectedValue = objProveedor.PRV_IndicadorArea;
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
            grvwContacto.DataSource = lstContactoProveedor;
            grvwContacto.DataBind();
            grvwDireccion.DataSource = lstDireccionProveedor;
            grvwDireccion.DataBind();
        }

        private void CargarDepartamento(int idPais)
        {
            ddlDepartamento.DataSource = DistritoBL.Instancia.ListarTodosDepartamentos(idPais);
            ddlDepartamento.DataValueField = "IDDepartamento";
            ddlDepartamento.DataTextField = "DEP_Nombre";
            ddlDepartamento.DataBind();
            if (Cultura.Equals("en-US"))
            {
                ddlDepartamento.Items.Insert(0, new ListItem(Resources.generales.textoSelect, "0"));
            }
            else
            {
                ddlDepartamento.Items.Insert(0, new ListItem(Resources.generales.textoSeleccionar, "0"));
            }
        }

        private void CargarProvincias(int idDepartamento)
        {
            ddlProvincia.DataSource = DistritoBL.Instancia.ListarTodosProvincias(idDepartamento);
            ddlProvincia.DataValueField = "IDProvincia";
            ddlProvincia.DataTextField = "PRO_Nombre";
            ddlProvincia.DataBind();
            if (Cultura.Equals("en-US"))
            {
                ddlProvincia.Items.Insert(0, new ListItem(Resources.generales.textoSelect, "0"));
            }
            else
            {
                ddlProvincia.Items.Insert(0, new ListItem(Resources.generales.textoSeleccionar, "0"));
            }
          
        }

        private void CargarDistritos(int idProvincia)
        {
            ddlDistrito.DataSource = DistritoBL.Instancia.ListarTodosDistritosByProvincia(idProvincia);
            ddlDistrito.DataValueField = "IDDistrito";
            ddlDistrito.DataTextField = "DIS_Nombre";
            ddlDistrito.DataBind();
            if (Cultura.Equals("en-US"))
            {
                ddlDistrito.Items.Insert(0, new ListItem(Resources.generales.textoSelect, "0"));
            }
            else
            {
                ddlDistrito.Items.Insert(0, new ListItem(Resources.generales.textoSeleccionar, "0"));
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
            objProveedor.PRV_IndicadorArea = rbIndicadorArea.SelectedValue.ToString();
            objProveedor.IDDistrito = ddlDistrito.SelectedValue != "0" ? Convert.ToInt32(ddlDistrito.SelectedValue) : (int?)null;
            objProveedor.PRV_Estado = Constantes.EstadoActivo;
        }

        protected void btnAgregarDireccion_Click(object sender, EventArgs e)
        {
            if (drdwlsTipo.SelectedValue != "" && txtDireccioSecundar.Text != "")
            {
                DireccionProveedor objDireccionProveedor = new DireccionProveedor();

                List<DireccionProveedor> lstDireccionProveedor = (List<DireccionProveedor>)Session["snDireccionProveedor"];

                int id_max = 0;
                if (lstDireccionProveedor.Count > 0)
                {
                    id_max = (from datos in lstDireccionProveedor select datos.IDDireccionProveedor).Max();
                    id_max++;

                }


                if (!EsNuevoRegistro())
                {
                    objDireccionProveedor.DIP_Tipo = drdwlsTipo.SelectedValue;
                    objDireccionProveedor.DIP_Descripcion = txtDireccioSecundar.Text;
                    objDireccionProveedor.DIP_Estado = Constantes.EstadoActivo;
                    objDireccionProveedor.IDProveedor = Convert.ToInt32(Request["idProveedor"].ToString());
                    objDireccionProveedor.IDDireccionProveedor = id_max;
                    lstDireccionProveedor.Add(objDireccionProveedor);
                    Session["snDireccionProveedor"] = lstDireccionProveedor;
                }
                else
                {
                    objDireccionProveedor.DIP_Tipo = drdwlsTipo.SelectedValue;
                    objDireccionProveedor.DIP_Descripcion = txtDireccioSecundar.Text;
                    objDireccionProveedor.DIP_Estado = Constantes.EstadoActivo;

                    if (lstDireccionProveedor == null)
                    {
                        lstDireccionProveedor = (List<DireccionProveedor>)DireccionProveedorBL.Instancia.ObtenerDireccionesProveedor(0);
                        lstDireccionProveedor.Add(objDireccionProveedor);
                    }
                    else
                    {
                        lstDireccionProveedor.Add(objDireccionProveedor);
                    }
                    Session["snDireccionProveedor"] = lstDireccionProveedor;
                }

                drdwlsTipo.SelectedValue = "";
                txtDireccioSecundar.Text = "";
                grvwDireccion.DataSource = lstDireccionProveedor;
                grvwDireccion.DataBind();
            }
            else
            {
                lblMensaje.Text = "Se debe agregar Tipo o Descripción";
            }
        }

        protected void btnQuitarContacto_Click(object sender, EventArgs e)
        {
            Usuario objusuario = (Usuario)Session[Constantes.sesionUsuario];
            IList<ContactoProveedor> lstContactoProveedor = (IList<ContactoProveedor>)Session["snContactoProveedor"];
            int contador = 0;

            foreach (GridViewRow row in grvwContacto.Rows)
            {
                CheckBox chbx = (CheckBox)row.FindControl("chkSeleccionContacto");

                if (chbx != null && chbx.Checked)
                {
                    lstContactoProveedor.RemoveAt(contador);
                    contador--;
                }

                contador = contador + 1;
            }

            Session["snContactoProveedor"] = lstContactoProveedor;
            grvwContacto.DataSource = lstContactoProveedor;
            grvwContacto.DataBind();
        }

        protected void btnAgregarContacto_Click(object sender, EventArgs e)
        {
            if (txtNombre.Text != "" && txtApellido.Text != "" && txtTelefonoContacto1.Text != "")
            {
                ContactoProveedor objContactoProveedor = new ContactoProveedor();
              
 
                IList<ContactoProveedor> lstContactoProveedor = (IList<ContactoProveedor>)Session["snContactoProveedor"];

                int id_max = 0;
                if (lstContactoProveedor.Count > 0)
                {
                    id_max = (from datos in lstContactoProveedor select datos.IDContactoProveedor).Max();
                    id_max++;
                }



                if (!EsNuevoRegistro())
                {
                    objContactoProveedor.IDContactoProveedor = id_max;
                    objContactoProveedor.COC_Nombres = txtNombre.Text;
                    objContactoProveedor.COC_Apellidos = txtApellido.Text;
                    objContactoProveedor.COC_Cargo = txtCargo.Text;
                    objContactoProveedor.COC_Telefono1 = txtTelefonoContacto1.Text;
                    objContactoProveedor.COC_Telefono2 = txtTelefonoContacto2.Text;
                    objContactoProveedor.COC_Estado = Constantes.EstadoActivo;
                    objContactoProveedor.IDProveedor = Convert.ToInt32(Request["idProveedor"].ToString());

                    lstContactoProveedor.Add(objContactoProveedor);
                    Session["snContactoProveedor"] = lstContactoProveedor;
                }
                else
                {
                    objContactoProveedor.COC_Nombres = txtNombre.Text;
                    objContactoProveedor.COC_Apellidos = txtApellido.Text;
                    objContactoProveedor.COC_Cargo = txtCargo.Text;
                    objContactoProveedor.COC_Telefono1 = txtTelefonoContacto1.Text;
                    objContactoProveedor.COC_Telefono2 = txtTelefonoContacto2.Text;
                    objContactoProveedor.COC_Estado = Constantes.EstadoActivo;

                    if (lstContactoProveedor == null)
                    {
                        lstContactoProveedor = ContactoProveedorBL.Instancia.ObtenerContactosProveedor(0);
                        lstContactoProveedor.Add(objContactoProveedor);
                    }
                    else
                    {
                        lstContactoProveedor.Add(objContactoProveedor);
                    }
                    Session["snContactoProveedor"] = lstContactoProveedor;
                }

                txtNombre.Text = "";
                txtApellido.Text = "";
                txtCargo.Text = "";
                txtTelefonoContacto1.Text = "";
                txtTelefonoContacto2.Text = "";
                grvwContacto.DataSource = lstContactoProveedor;
                grvwContacto.DataBind();
            }
            else
            {
                lblMensaje.Text = "Se debe agregar Nombre, Apellido o Telefono Contacto";
            }
        }
        
        


        void visibleBtnContacto(bool value)
        {
            divbtnAgregarContacto.Visible = !value;
            divbtnQuitarContacto.Visible = !value;
            divbntActualizarContacto.Visible = value;
            divbntCancelarContacto.Visible = value;
            grvwContacto.Enabled = !value;
        }

        void clearContacto()
        {
            txtNombre.Text = "";
            txtApellido.Text = "";
            txtCargo.Text = "";
            txtTelefonoContacto1.Text = "";
            txtTelefonoContacto2.Text = "";
        }

        void visibleBtnDireccion(bool value)
        {
            divbtnAgregarDireccion.Visible = !value;
            divbtnQuitarDireccion.Visible = !value;
            divbntActualizarDireccion.Visible = value;
            divbntCancelarDireccion.Visible = value;
            grvwDireccion.Enabled = !value;
        }

        void clearDireccion()
        {
            drdwlsTipo.SelectedValue = "";
            txtDireccioSecundar.Text = "";
        }

        protected void bntActualizarContacto_Click(object sender, EventArgs e)
        {
            IList<ContactoProveedor> lstContactoProveedor = (IList<ContactoProveedor>)Session["snContactoProveedor"];
            for (int i = 0; i < lstContactoProveedor.Count; i++)
            {
                if (lstContactoProveedor[i].IDContactoProveedor == int.Parse(hdflIdContacto.Value.ToString()))
                {
                    hdflIdContacto.Value = string.Empty;
                    lstContactoProveedor[i].COC_Nombres = txtNombre.Text;
                    lstContactoProveedor[i].COC_Apellidos = txtApellido.Text;
                    lstContactoProveedor[i].COC_Cargo = txtCargo.Text;
                    lstContactoProveedor[i].COC_Telefono1 = txtTelefonoContacto1.Text;
                    lstContactoProveedor[i].COC_Telefono2 = txtTelefonoContacto2.Text;
                    break;
                }
            }

            grvwContacto.DataSource = lstContactoProveedor;
            grvwContacto.DataBind();
            visibleBtnContacto(false);
            clearContacto();
        }

        protected void bntCancelarContacto_Click(object sender, EventArgs e)
        {
            visibleBtnContacto(false);
            clearContacto();
        }

        protected void btnQuitarDireccion_Click(object sender, EventArgs e)
        {
            Usuario objusuario = (Usuario)Session[Constantes.sesionUsuario];
            IList<DireccionProveedor> lstDireccionProveedor = (IList<DireccionProveedor>)Session["snDireccionProveedor"];
            int contador = 0;

            foreach (GridViewRow row in grvwDireccion.Rows)
            {
                CheckBox chbx = (CheckBox)row.FindControl("chkSeleccionDireccion");

                if (chbx != null && chbx.Checked)
                {
                    lstDireccionProveedor.RemoveAt(contador);
                    contador--;
                }

                contador = contador + 1;
            }

            Session["snDireccionProveedor"] = lstDireccionProveedor;
            grvwDireccion.DataSource = lstDireccionProveedor;
            grvwDireccion.DataBind();
        }

        protected void bntActualizarDireccion_Click(object sender, EventArgs e)
        {
            IList<DireccionProveedor> lstDireccionProveedor = (IList<DireccionProveedor>)Session["snDireccionProveedor"];
            for (int i = 0; i < lstDireccionProveedor.Count; i++)
            {
                if (lstDireccionProveedor[i].IDDireccionProveedor == int.Parse(hdflIdDireccion.Value.ToString()))
                {
                    hdflIdDireccion.Value = string.Empty;
                    lstDireccionProveedor[i].DIP_Tipo = drdwlsTipo.SelectedValue;
                    lstDireccionProveedor[i].DIP_Descripcion = txtDireccioSecundar.Text;

                    grvwDireccion.DataSource = lstDireccionProveedor;
                    grvwDireccion.DataBind();
                    break;
                }
            }

            visibleBtnDireccion(false);
            clearDireccion();
        }

        protected void bntCancelarDireccion_Click(object sender, EventArgs e)
        {
            visibleBtnDireccion(false);
            clearDireccion();
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            bool graboOK = false;
            string mensajeError = "";
            Usuario objUsuario = (Usuario)Session[Constantes.sesionUsuario];
            ContactoProveedor objEliminar = ContactoProveedorBL.Instancia.ObtenerContactoProveedorById(Convert.ToInt32(Request["idProveedor"].ToString()));

            objEliminar.COC_Estado = Constantes.EstadoEliminado;
            objEliminar.COC_UsuarioModificacion = objUsuario.IDUsuario.ToString();
            objEliminar.COC_FechaHoraModificacion = DateTime.Now;

            try
            {
                ContactoProveedorBL.Instancia.Eliminar(objEliminar);
                //CargarDatos();
                graboOK = true;
            }
            catch (Exception ex)
            {
                graboOK = false;
                mensajeError = "Eliminar" + ex.Message;
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
                foreach (var obj in mpContentPlaceHolder.Controls.OfType<DropDownList>())
                {
                    obj.SelectedIndex=-1;
                }
                foreach(var obj in mpContentPlaceHolder.Controls.OfType<RadioButtonList>())
                {
                    obj.SelectedIndex = -1;
                }
            }
        }

        protected void grvwContacto_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvwContacto.PageIndex = e.NewPageIndex;

            var datos = Session["cargarProveedores"];
            grvwContacto.DataSource = datos;
            grvwContacto.DataBind();
        }

        protected void grvwDireccion_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvwDireccion.PageIndex = e.NewPageIndex;

            var datos = Session["cargarProveedores"];
            grvwDireccion.DataSource = datos;
            grvwDireccion.DataBind();
        }

        protected void grvwDireccion_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Editar":

                    int idDireccion = int.Parse(e.CommandArgument.ToString());

                    if (idDireccion >= 0)
                    {
                        List<DireccionProveedor> lstDireccionProveedor = (List<DireccionProveedor>)Session["snDireccionProveedor"];


                        int pos = lstDireccionProveedor.FindIndex(x => x.IDDireccionProveedor == idDireccion);

                        //foreach (DireccionProveedor objDireccionProveedor in lstDireccionProveedor)
                        //{
                        //    if (objDireccionProveedor.IDDireccionProveedor == idDireccion)
                        //    {
                                hdflIdDireccion.Value = idDireccion.ToString();
                                drdwlsTipo.SelectedValue = lstDireccionProveedor[pos].DIP_Tipo.ToString();
                                txtDireccioSecundar.Text = lstDireccionProveedor[pos].DIP_Descripcion.ToString();
                        //        break;
                        //    }
                        //}

                        string actualizar = HttpContext.GetLocalResourceObject("~/Mantenimiento/neoproveedores.aspx", "bntActualizarDireccionResource1", new System.Globalization.CultureInfo(Cultura)).ToString();
                        bntActualizarDireccion.Text = actualizar;
                        visibleBtnDireccion(true);
                    }                    

                    break;
            }

        }

        protected void grvwContacto_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Editar":
       
                    int idContacto = int.Parse(e.CommandArgument.ToString());
                    if (idContacto >=0)
                    {
                    
                        List<ContactoProveedor> lstContactoProveedor = (List<ContactoProveedor>)Session["snContactoProveedor"];
                        int pos = lstContactoProveedor.FindIndex(x => x.IDContactoProveedor == idContacto);


                        //foreach (ContactoProveedor objContactoProveedor in lstContactoProveedor)
                        //{
                        //    if (objContactoProveedor.IDContactoProveedor == idContacto)
                        //    {
                                hdflIdContacto.Value = idContacto.ToString();
                                txtNombre.Text = lstContactoProveedor[pos].COC_Nombres.ToString();
                                txtApellido.Text = lstContactoProveedor[pos].COC_Apellidos.ToString();
                                txtCargo.Text = lstContactoProveedor[pos].COC_Cargo.ToString();
                                txtTelefonoContacto1.Text = lstContactoProveedor[pos].COC_Telefono1.ToString();
                                txtTelefonoContacto2.Text = lstContactoProveedor[pos].COC_Telefono2.ToString();
                        //        break;
                        //    }
                        //}
                        string actualizar = HttpContext.GetLocalResourceObject("~/Mantenimiento/neoproveedores.aspx", "bntActualizarContactoResource1", new System.Globalization.CultureInfo(Cultura)).ToString();
                        bntActualizarContacto.Text = actualizar;


                        visibleBtnContacto(true);

                    }
                    
                    break;
            }
        }
    }
}