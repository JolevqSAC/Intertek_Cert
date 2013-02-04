using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
//agregar referencias
using Intertek.Business.Entities;
using Intertek.Business.Logic;
using Intertek.Helpers;
using System.Globalization;

namespace Intertek.WEB.Mantenimiento
{
    public partial class neoclientes : PaginaBase
    {
        public string Cultura { get; set; }
        public string  rutaImagen { get; set; }
        private List<ContactoCliente> items { get; set; }
        private List<DireccionCliente> items2 { get; set; }
        string accion = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            Cultura = (string)CultureInfo.CurrentCulture.Name;

            if (!Page.IsPostBack)
            {

               // btnQuitarContacto.Enabled = false;
                if (Session["ddlIdiomas"].ToString() == "en-US")
                {
                    imgGrabar.Src = Resources.generales.imgGrabarUS;
                }

                accion = Request.QueryString.Get("accion");
                opciones();

                if (Cultura.Equals("en-US"))
                {
                   
                    //string host = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);
                    //string rootApplication = HttpContext.Current.Request.ApplicationPath;
                    //rutaImagen = host + rootApplication + "img/en/btn_modify.png";
                  
                    //bntActualizarContacto
                }
                string Eliminar = HttpContext.GetLocalResourceObject("~/Mantenimiento/neoclientes.aspx", "btnQuitar2ContactoResource1", new System.Globalization.CultureInfo(Cultura)).ToString();

                btnEliminar0.Value = Eliminar;
                btnEliminar3.Value = Eliminar;

                CargarDropDownList();
                if (!EsNuevoRegistro())
                {
                    ObtenerDatos(Convert.ToInt32(Request["idCliente"].ToString()));

                 
                    if (Cultura.Equals("en-US"))
                    {
                        lblTitulo.Text = "Modify Client";
                    }
                    else
                    {
                        lblTitulo.Text = "Modificar Cliente";
                    }
                }
                else 
                {
                 
                    if (Cultura.Equals("en-US"))
                    {
                        lblTitulo.Text = "Register Client";
                    }
                    else
                    {
                        lblTitulo.Text = "Registrar Cliente";
                    }
                }
            }
        }



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

        private void cargarContactoCliente()
        {
            IList<ContactoCliente> lstContactoClienteOld = ContactoClienteBL.Instancia.ObtenerContactosCliente(0);

            Session["snContactoCliente"] = lstContactoClienteOld;
        }
        private void EnlazarGrilla()
        {
            if (Session["snContactoCliente"] == null)
            {
                cargarContactoCliente();
            }
            items = (List<ContactoCliente>)Session["snContactoCliente"];
            grvwContacto.DataSource = items;
            grvwContacto.DataBind();

        }

        private void cargarDireccionCliente()
        {
            IList<DireccionCliente> lstDireccionClienteOld = DireccionClienteBL.Instancia.ObtenerDireccionesCliente(0);

            Session["snDireccionCliente"] = lstDireccionClienteOld;
        }
        private void EnlazarGrillaDireccion()
        {
            if (Session["snDireccionCliente"] == null)
            {
                cargarDireccionCliente();
            }
            items2 = (List<DireccionCliente>)Session["snDireccionCliente"];
            grvwDireccion.DataSource = items;
            grvwDireccion.DataBind();

        }


        private bool validar()
        {

            bool rpt = false;
            string email_format = "^\\w+([-._]\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*$";
            if (txtEmail.Text.Trim() == string.Empty)
            {
                rpt = true;

            }
            else
            {

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
                int idCliente = 0;

                IList<ContactoCliente> lstContactoCliente = (IList<ContactoCliente>)Session["snContactoCliente"];
                IList<DireccionCliente> lstDireccionCliente = (IList<DireccionCliente>)Session["snDireccionCliente"];

                if (txtRazonSocial.Text != "")
                {
                    if (!EsNuevoRegistro())
                    {
                        //actualizar
                        IList<ContactoCliente> lstContactoClienteOld = ContactoClienteBL.Instancia.ObtenerContactosCliente(Convert.ToInt32(Request["idCliente"].ToString()));
                        IList<DireccionCliente> lstDireccionClienteOld = DireccionClienteBL.Instancia.ObtenerDireccionesCliente(Convert.ToInt32(Request["idCliente"].ToString()));
                        Cliente objCliente = ClienteBL.Instancia.ObtenerClienteById(Convert.ToInt32(Request["idCliente"].ToString()));
                        idCliente = objCliente.IDCliente;
                        SetearValores(ref objCliente);
                        objCliente.CLI_UsuarioModificacion = objLogin.IDUsuario.ToString();
                        objCliente.CLI_FechaHoraModificacion = DateTime.Now;

                        try
                        {
                            ClienteBL.Instancia.Actualizar(objCliente);

                            for (int j = 0; j < lstContactoClienteOld.Count; j++)
                            {
                                ContactoCliente objContactoCliente = ContactoClienteBL.Instancia.ObtenerContactoClienteById(lstContactoClienteOld[j].IDContactoCliente);
                                ContactoClienteBL.Instancia.Eliminar(objContactoCliente);
                            }

                            for (int j = 0; j < lstDireccionClienteOld.Count; j++)
                            {
                                DireccionCliente objDireccionCliente = DireccionClienteBL.Instancia.ObtenerDireccionClienteById(lstDireccionClienteOld[j].IDDireccionCliente);
                                DireccionClienteBL.Instancia.Eliminar(objDireccionCliente);
                            }

                            for (int j = 0; j < lstContactoCliente.Count; j++)
                            {
                                ContactoCliente objContactoCliente = new ContactoCliente();
                                objContactoCliente.COC_Nombres = lstContactoCliente[j].COC_Nombres.ToString();
                                objContactoCliente.COC_Apellidos = lstContactoCliente[j].COC_Apellidos.ToString();
                                objContactoCliente.COC_Cargo = lstContactoCliente[j].COC_Cargo.ToString();
                                objContactoCliente.COC_Telefono1 = lstContactoCliente[j].COC_Telefono1.ToString();
                                objContactoCliente.COC_Telefono2 = lstContactoCliente[j].COC_Telefono2.ToString();
                                objContactoCliente.COC_Estado = Constantes.EstadoActivo;
                                objContactoCliente.IDCliente = idCliente;
                                objContactoCliente.COC_UsuarioCreacion = objLogin.IDUsuario.ToString();
                                objContactoCliente.COC_FechaHoraCreacion = DateTime.Now;
                                ContactoClienteBL.Instancia.Insertar(objContactoCliente);
                            }

                            for (int j = 0; j < lstDireccionCliente.Count; j++)
                            {
                                DireccionCliente objDireccionCliente = new DireccionCliente();
                                objDireccionCliente.DIC_Tipo = lstDireccionCliente[j].DIC_Tipo.ToString();
                                objDireccionCliente.DIC_Descripcion = lstDireccionCliente[j].DIC_Descripcion.ToString();
                                objDireccionCliente.DIC_Estado = Constantes.EstadoActivo;
                                objDireccionCliente.IDCliente = idCliente;
                                objDireccionCliente.DIC_UsuarioCreacion = objLogin.IDUsuario.ToString();
                                objDireccionCliente.DIC_FechaHoraCreacion = DateTime.Now;
                                DireccionClienteBL.Instancia.Insertar(objDireccionCliente);
                            }

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
                            idCliente = objCliente.IDCliente;
                            objCliente.CLI_Codigo = "CLI" + idCliente.ToString().PadLeft(7, '0');
                            ClienteBL.Instancia.Actualizar(objCliente);

                            if (lstContactoCliente != null)
                            {
                                for (int j = 0; j < lstContactoCliente.Count; j++)
                                {
                                    ContactoCliente objContactoCliente = new ContactoCliente();
                                    objContactoCliente.COC_Nombres = lstContactoCliente[j].COC_Nombres.ToString();
                                    objContactoCliente.COC_Apellidos = lstContactoCliente[j].COC_Apellidos.ToString();
                                    objContactoCliente.COC_Cargo = lstContactoCliente[j].COC_Cargo.ToString();
                                    objContactoCliente.COC_Telefono1 = lstContactoCliente[j].COC_Telefono1.ToString();
                                    objContactoCliente.COC_Telefono2 = lstContactoCliente[j].COC_Telefono2.ToString();
                                    objContactoCliente.COC_Estado = Constantes.EstadoActivo;
                                    objContactoCliente.IDCliente = idCliente;
                                    objContactoCliente.COC_UsuarioCreacion = objLogin.IDUsuario.ToString();
                                    objContactoCliente.COC_FechaHoraCreacion = DateTime.Now;
                                    ContactoClienteBL.Instancia.Insertar(objContactoCliente);
                                }
                            }

                            if (lstDireccionCliente != null)
                            {
                                for (int j = 0; j < lstDireccionCliente.Count; j++)
                                {
                                    DireccionCliente objDireccionCliente = new DireccionCliente();
                                    objDireccionCliente.DIC_Tipo = lstDireccionCliente[j].DIC_Tipo.ToString();
                                    objDireccionCliente.DIC_Descripcion = lstDireccionCliente[j].DIC_Descripcion.ToString();
                                    objDireccionCliente.DIC_Estado = Constantes.EstadoActivo;
                                    objDireccionCliente.IDCliente = idCliente;
                                    objDireccionCliente.DIC_UsuarioCreacion = objLogin.IDUsuario.ToString();
                                    objDireccionCliente.DIC_FechaHoraCreacion = DateTime.Now;
                                    DireccionClienteBL.Instancia.Insertar(objDireccionCliente);
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
                    Session["snContactoCliente"] = null;
                    Session["snDireccionCliente"] = null;
                    ClientScript.RegisterStartupScript(this.GetType(), "miscriptError", "$(function(){MostrarMensaje('msjSatisfactorio');});", true);
                    //Response.Redirect("clientesBuscar.aspx");
                }
                else
                {
                    Session["snContactoCliente"] = null;
                    Session["snDireccionCliente"] = null;
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
            Session["snContactoCliente"] = null;
            Session["snDireccionCliente"] = null;
            Response.Redirect("clientesBuscar.aspx");
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            
            txtApellido.Text = string.Empty;
            txtCargo.Text = string.Empty;
            txtDireccion.Text = string.Empty;
            txtDireccioSecundar.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtFax.Text = string.Empty;
            txtNombre.Text = string.Empty;
            txtObservaciones.Text = string.Empty;
            txtRazonSocial.Text = string.Empty;
            txtRUC.Text = string.Empty;
            txtTelefono1.Text = string.Empty;
            txtTelefono2.Text = string.Empty;
            txtTelefonoContacto1.Text = string.Empty;
            ddlDistrito.SelectedIndex = -1;
            ddlProvincia.SelectedIndex = -1;
            ddlDepartamento.SelectedIndex = -1;
            ddlPais.SelectedIndex=-1;
            drdwlsTipo.SelectedIndex = -1;
            rbIndicadorArea.SelectedIndex = -1;
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

        private void CargarDropDownList()
        {
            ddlPais.DataSource = DistritoBL.Instancia.ListarTodosPaises();
            ddlPais.DataValueField = "IDPais";
            ddlPais.DataTextField = "PAI_Nombre";
            ddlPais.DataBind();
            //ddlPais.Items.Insert(0, new ListItem(Resources.generales.textoSeleccionar, "0"));
            //ddlDepartamento.Items.Insert(0, new ListItem(Resources.generales.textoSeleccionar, "0"));
            //ddlProvincia.Items.Insert(0, new ListItem(Resources.generales.textoSeleccionar, "0"));
            //ddlDistrito.Items.Insert(0, new ListItem(Resources.generales.textoSeleccionar, "0"));

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

        private void ObtenerDatos(int idCliente)
        {
            Cliente objCliente = ClienteBL.Instancia.ObtenerClienteById(idCliente);
            IList<ContactoCliente> lstContactoCliente = ContactoClienteBL.Instancia.ObtenerContactosCliente(idCliente);
            IList<DireccionCliente> lstDireccionCliente = DireccionClienteBL.Instancia.ObtenerDireccionesCliente(idCliente);
            Session["snContactoCliente"] = lstContactoCliente;
            Session["snDireccionCliente"] = lstDireccionCliente;

            if (objCliente != null)
            {
                //txtContacto.Text = objCliente.CLI_Contacto;
                hdflIDCliente.Value = objCliente.IDCliente.ToString();
                txtEmail.Text = objCliente.CLI_Correo;
                txtDireccion.Text = objCliente.CLI_Direccion;
                txtFax.Text = objCliente.CLI_Fax;
                txtObservaciones.Text = objCliente.CLI_Observaciones;
                txtRazonSocial.Text = objCliente.CLI_RazonSocial;
                txtRUC.Text = objCliente.CLI_RUC;
                txtTelefono1.Text = objCliente.CLI_Telefono1;
                txtTelefono2.Text = objCliente.CLI_Telefono2;
                rbIndicadorArea.SelectedValue = objCliente.CLI_IndicadorArea;

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
            grvwContacto.DataSource = lstContactoCliente;
            grvwContacto.DataBind();
            grvwDireccion.DataSource = lstDireccionCliente;
            grvwDireccion.DataBind();
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
            objCliente.CLI_IndicadorArea = rbIndicadorArea.SelectedValue.ToString();
        }

        protected void grvwContacto_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvwContacto.PageIndex = e.NewPageIndex;

            var datos = Session["snContactoCliente"];
            grvwContacto.DataSource = datos;
            grvwContacto.DataBind();
        }

        protected void grvwDireccion_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvwDireccion.PageIndex = e.NewPageIndex;

            var datos = Session["snDireccionCliente"];
            grvwDireccion.DataSource = datos;
            grvwDireccion.DataBind();
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

        protected void btnAgregarDireccion_Click(object sender, EventArgs e)
        {
            if (drdwlsTipo.SelectedValue != "" && txtDireccioSecundar.Text != "")
            {
                DireccionCliente objDireccionCliente = new DireccionCliente();
                IList<DireccionCliente> lstDireccionCliente = (IList<DireccionCliente>)Session["snDireccionCliente"];

                int id_max = 0;
                if (lstDireccionCliente.Count > 0)
                {
                     id_max = (from datos in lstDireccionCliente select datos.IDDireccionCliente).Max();
                    id_max++;

                }

                if (!EsNuevoRegistro())
                {
                    objDireccionCliente.DIC_Tipo = drdwlsTipo.SelectedValue;
                    objDireccionCliente.DIC_Descripcion = txtDireccioSecundar.Text;
                    objDireccionCliente.DIC_Estado = Constantes.EstadoActivo;
                    objDireccionCliente.IDCliente = Convert.ToInt32(Request["idCliente"].ToString());
                    objDireccionCliente.IDDireccionCliente = id_max;
                    lstDireccionCliente.Add(objDireccionCliente);
                    Session["snDireccionCliente"] = lstDireccionCliente;
                }
                else
                {
                    objDireccionCliente.DIC_Tipo = drdwlsTipo.SelectedValue;
                    objDireccionCliente.DIC_Descripcion = txtDireccioSecundar.Text;
                    objDireccionCliente.DIC_Estado = Constantes.EstadoActivo;

                    if (lstDireccionCliente == null)
                    {
                        lstDireccionCliente = DireccionClienteBL.Instancia.ObtenerDireccionesCliente(0);
                        lstDireccionCliente.Add(objDireccionCliente);
                    }
                    else
                    {
                        lstDireccionCliente.Add(objDireccionCliente);
                    }
                    Session["snDireccionCliente"] = lstDireccionCliente;
                }

                drdwlsTipo.SelectedValue = "";
                txtDireccioSecundar.Text = "";
                grvwDireccion.DataSource = lstDireccionCliente;
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
            IList<ContactoCliente> lstContactoCliente = (IList<ContactoCliente>)Session["snContactoCliente"];
            int contador = 0;

            foreach (GridViewRow row in grvwContacto.Rows)
            {
                CheckBox chbx = (CheckBox)row.FindControl("chkSeleccionContacto");

                if (chbx != null && chbx.Checked)
                {
                    lstContactoCliente.RemoveAt(contador);
                    contador--;
                }

                contador++;
            }

            Session["snContactoCliente"] = lstContactoCliente;
            grvwContacto.DataSource = lstContactoCliente;
            grvwContacto.DataBind();
        }

        protected void btnAgregarContacto_Click(object sender, EventArgs e)
        {
            if (txtNombre.Text != "" && txtApellido.Text != "" && txtTelefonoContacto1.Text != "")
            {


                ContactoCliente objContactoCliente = new ContactoCliente();
               
             

                 
                IList<ContactoCliente> lstContactoCliente = (IList<ContactoCliente>)Session["snContactoCliente"];
                int id_max = 0;
                if (lstContactoCliente.Count > 0)
                {
                     id_max = (from datos in lstContactoCliente select datos.IDContactoCliente).Max();
                    id_max++;
                }
               
                if (!EsNuevoRegistro())
                {
                    objContactoCliente.COC_Nombres = txtNombre.Text;
                    objContactoCliente.COC_Apellidos = txtApellido.Text;
                    objContactoCliente.COC_Cargo = txtCargo.Text;
                    objContactoCliente.COC_Telefono1 = txtTelefonoContacto1.Text;
                    objContactoCliente.COC_Telefono2 = txtTelefonoContacto2.Text;
                    objContactoCliente.COC_Estado = Constantes.EstadoActivo;
                    objContactoCliente.IDCliente = Convert.ToInt32(Request["idCliente"].ToString());
                    objContactoCliente.IDContactoCliente = id_max;
                    lstContactoCliente.Add(objContactoCliente);
                    Session["snContactoCliente"] = lstContactoCliente;
                }
                else
                {
                    objContactoCliente.COC_Nombres = txtNombre.Text;
                    objContactoCliente.COC_Apellidos = txtApellido.Text;
                    objContactoCliente.COC_Cargo = txtCargo.Text;
                    objContactoCliente.COC_Telefono1 = txtTelefonoContacto1.Text;
                    objContactoCliente.COC_Telefono2 = txtTelefonoContacto2.Text;
                    objContactoCliente.COC_Estado = Constantes.EstadoActivo;

                    if (lstContactoCliente == null)
                    {
                        lstContactoCliente = ContactoClienteBL.Instancia.ObtenerContactosCliente(0);
                        lstContactoCliente.Add(objContactoCliente);
                    }
                    else
                    {
                        lstContactoCliente.Add(objContactoCliente);
                    }
                    Session["snContactoCliente"] = lstContactoCliente;
                }

                txtNombre.Text = "";
                txtApellido.Text = "";
                txtCargo.Text = "";
                txtTelefonoContacto1.Text = "";
                txtTelefonoContacto2.Text = "";
                grvwContacto.DataSource = lstContactoCliente;
                grvwContacto.DataBind();
            }
            else
            {
                lblMensaje.Text = "Se debe agregar Nombre, Apellido o Telefono Contacto";
            }
        }
      
     

   

        void visibleBtnContacto(bool value){
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
            txtDireccioSecundar.Text = "";
        }
        
        protected void bntActualizarContacto_Click(object sender, EventArgs e)
        {
            if (txtNombre.Text != "" && txtApellido.Text != "" && txtTelefonoContacto1.Text != "")
            {
                IList<ContactoCliente> lstContactoCliente = (IList<ContactoCliente>)Session["snContactoCliente"];
                for (int i = 0; i < lstContactoCliente.Count; i++)
                {
                    if (lstContactoCliente[i].IDContactoCliente == int.Parse(hdflIdContacto.Value.ToString()))
                    {
                        hdflIdContacto.Value = string.Empty;
                        lstContactoCliente[i].COC_Nombres = txtNombre.Text;
                        lstContactoCliente[i].COC_Apellidos = txtApellido.Text;
                        lstContactoCliente[i].COC_Cargo = txtCargo.Text;
                        lstContactoCliente[i].COC_Telefono1 = txtTelefonoContacto1.Text;
                        lstContactoCliente[i].COC_Telefono2 = txtTelefonoContacto2.Text;
                        break;
                    }
                }

                grvwContacto.DataSource = lstContactoCliente;
                grvwContacto.DataBind();

                visibleBtnContacto(false);
                clearContacto();
            }
        }

        protected void bntCancelarContacto_Click(object sender, EventArgs e)
        {
            visibleBtnContacto(false);
            clearContacto();
        }

        protected void btnQuitarDireccion_Click(object sender, EventArgs e)
        {
            Usuario objusuario = (Usuario)Session[Constantes.sesionUsuario];
            IList<DireccionCliente> lstDireccionCliente = (IList<DireccionCliente>)Session["snDireccionCliente"];
            int contador = 0;

            foreach (GridViewRow row in grvwDireccion.Rows)
            {
                CheckBox chbx = (CheckBox)row.FindControl("chkSeleccionDireccion");

                if (chbx != null && chbx.Checked)
                {
                    lstDireccionCliente.RemoveAt(contador);
                    contador--;
                }

                contador++;
            }

            Session["snDireccionCliente"] = lstDireccionCliente;
            grvwDireccion.DataSource = lstDireccionCliente;
            grvwDireccion.DataBind();
        }

        protected void bntActualizarDireccion_Click(object sender, EventArgs e)
        {
            if (drdwlsTipo.SelectedValue != "" && txtDireccioSecundar.Text != "")
            {
                IList<DireccionCliente> lstDireccionCliente = (IList<DireccionCliente>)Session["snDireccionCliente"];
                for (int i = 0; i < lstDireccionCliente.Count; i++)
                {
                    if (lstDireccionCliente[i].IDDireccionCliente == int.Parse(hdflIdDireccion.Value.ToString()))
                    {
                        hdflIdDireccion.Value = string.Empty;
                        lstDireccionCliente[i].DIC_Tipo = drdwlsTipo.SelectedValue;
                        lstDireccionCliente[i].DIC_Descripcion = txtDireccioSecundar.Text;
                        break;
                    }
                }

                grvwDireccion.DataSource = lstDireccionCliente;
                grvwDireccion.DataBind();

                visibleBtnDireccion(false);
                clearDireccion();
            }
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
            ContactoCliente objEliminar = ContactoClienteBL.Instancia.ObtenerContactoClienteById(Convert.ToInt32(Request["idCliente"].ToString()));

          
           
           // objEliminar.COC_FechaHoraModificacion = DateTime.Now;

            try
            {
                if (objEliminar != null)
                {

                    objEliminar.COC_Estado = Constantes.EstadoEliminado;
                    objEliminar.COC_UsuarioModificacion = objUsuario.IDUsuario.ToString();
                    ContactoClienteBL.Instancia.Eliminar(objEliminar);
                }

               
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

        protected void grvwDireccion_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            switch (e.CommandName)
            {
                case "Editar":

                    int idDireccion = int.Parse(e.CommandArgument.ToString());
                 
                    if (idDireccion >=0)
                    {
                        List<DireccionCliente> lstDireccionCliente = (List<DireccionCliente>)Session["snDireccionCliente"];
                        int pos = lstDireccionCliente.FindIndex(x => x.IDDireccionCliente == idDireccion);
                        //foreach (DireccionCliente objDireccionCliente in lstDireccionCliente)
                        //{
                        //    if (objDireccionCliente.IDDireccionCliente == idDireccion)
                        //    {
                                hdflIdDireccion.Value = idDireccion.ToString();
                                drdwlsTipo.SelectedValue = lstDireccionCliente[pos].DIC_Tipo.ToString();
                                txtDireccioSecundar.Text = lstDireccionCliente[pos].DIC_Descripcion.ToString();
                        //        break;
                        //    }
                        //}
                        string actualizar = HttpContext.GetLocalResourceObject("~/Mantenimiento/neoclientes.aspx", "bntActualizarDireccionResource1", new System.Globalization.CultureInfo(Cultura)).ToString();
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
                    if (idContacto >= 0)
                    {
                        List<ContactoCliente> lstContactoCliente = (List<ContactoCliente>)Session["snContactoCliente"];

                        int pos = lstContactoCliente.FindIndex(x => x.IDContactoCliente == idContacto);
                        //foreach (ContactoCliente objContactoCliente in lstContactoCliente)
                        //{
                        //    if (objContactoCliente.IDContactoCliente == idContacto)
                        //    {
                                hdflIdContacto.Value = idContacto.ToString();
                                txtNombre.Text =lstContactoCliente[pos].COC_Nombres.ToString();
                                txtApellido.Text = lstContactoCliente[pos].COC_Apellidos.ToString();
                                txtCargo.Text = lstContactoCliente[pos].COC_Cargo.ToString();
                                txtTelefonoContacto1.Text = lstContactoCliente[pos].COC_Telefono1.ToString();
                                txtTelefonoContacto2.Text = lstContactoCliente[pos].COC_Telefono2.ToString();
                        //        break;
                        //    }
                        //}
                        string actualizar = HttpContext.GetLocalResourceObject("~/Mantenimiento/neoclientes.aspx", "bntActualizarContactoResource1", new System.Globalization.CultureInfo(Cultura)).ToString();
                        bntActualizarContacto.Text = actualizar;
                       

                        visibleBtnContacto(true);
          
                    }
          

           

                    break;
            }

            
        }

 

       
    }
}