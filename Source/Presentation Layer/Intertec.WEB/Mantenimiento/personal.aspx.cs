using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//agregr referencias
using Intertek.Business.Entities;
using Intertek.Business.Logic;
using Intertek.Helpers;

namespace Intertek.WEB.Mantenimiento
{
    public partial class personal : PaginaBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CargarDropDownList();
                if (Session["ddlIdiomas"].ToString() == "en-US")
                {
                    imgGrabar.Src = Resources.generales.imgGrabarUS;
                }
                if (!EsNuevoRegistro())
                {
                    ObtenerDatos(Convert.ToInt32(Request["idPersonal"].ToString()));
                }

            }
        }

        protected void btnGrabar_Click(object sender, EventArgs e)
        {
            Usuario objUsuario = (Usuario)Session[Constantes.sesionUsuario];
            bool graboOK = false;
            lblMensaje.Text = "";
            string mensajeError = "";

            if (txtNombre.Text != "" && txtApellidos.Text != "" && txtDNI.Text != "")
            {
                if (!EsNuevoRegistro())
                {
                    //actualizar
                    Trabajador objPersonal = PersonalBL.Instancia.ObtenerPersonalByID(Convert.ToInt32(Request["idPersonal"].ToString()));

                    SetearValores(ref objPersonal);
                    objPersonal.PER_UsuarioModificacion= objUsuario.IDUsuario.ToString();
                    objPersonal.PER_FechaHoraModificacion = DateTime.Now;
                    
                    try
                    {
                        objPersonal = PersonalBL.Instancia.Actualizar(objPersonal);
                        graboOK = true;
                        LimpiarFormulario();
                    }
                    catch(Exception ex)
                    {
                        graboOK = false;
                        mensajeError = "Actualizar"+ ex.Message;
                    }
                }
                else
                {
                    //insertar
                    Trabajador objPersonal = new Trabajador();
                    SetearValores(ref objPersonal);
                    objPersonal.PER_UsuarioCreacion = objUsuario.IDUsuario.ToString();
                    objPersonal.PER_FechaHoraCreacion = DateTime.Now;
                    try
                    {
                        objPersonal = PersonalBL.Instancia.Insertar(objPersonal);
                        int idpersonal = objPersonal.IDPersonal;
                        objPersonal.PER_Codigo = "TRA" + idpersonal.ToString().PadLeft(7, '0');
                        PersonalBL.Instancia.Actualizar(objPersonal);
                        graboOK = true;
                        LimpiarFormulario();
                    }
                    catch(Exception ex)
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
            Response.Redirect("personalBuscar.aspx");
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {

            LimpiarFormulario();
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

        private bool EsNuevoRegistro()
        {
            bool esNuevo = true;
            if (Request["idPersonal"] != null)
            {
                return false;
            }

            return esNuevo;
        }

        private void CargarDropDownList()
        {

            ddlCargo.DataSource = CargosBL.Instancia.ListarTodosCargos();
            ddlCargo.DataValueField = "IDCargo";
            ddlCargo.DataTextField = "CAR_Nombre";
            ddlCargo.DataBind();
            if (Session["ddlIdiomas"].ToString() == "en-US")
            {
                ddlCargo.Items.Insert(0, new ListItem(Resources.generales.textoSelect, "0"));
            }
            else
            {
                ddlCargo.Items.Insert(0, new ListItem(Resources.generales.textoSeleccionar, "0"));
            }
         

            //ddlLaboratorio.DataSource = LaboratorioBL.Instancia.ListarTodosLaboratorios().ToList();
            //ddlLaboratorio.DataValueField = "IDLaboratorio";
            //ddlLaboratorio.DataTextField = "LAB_Nombre";
            //ddlLaboratorio.DataBind();
            //ddlLaboratorio.Items.Insert(0, new ListItem(Resources.generales.textoSeleccionar, "0"));

            ddlPais.DataSource = DistritoBL.Instancia.ListarTodosPaises();
            ddlPais.DataValueField = "IDPais";
            ddlPais.DataTextField = "PAI_Nombre";
            ddlPais.DataBind();

            if (Session["ddlIdiomas"].ToString() == "en-US")
            {
                ddlPais.Items.Insert(0, new ListItem(Resources.generales.textoSelect, "0"));
                ddlDepartamento.Items.Insert(0, new ListItem(Resources.generales.textoSelect, "0"));
                ddlProvincia.Items.Insert(0, new ListItem(Resources.generales.textoSelect, "0"));
                ddlDistrito.Items.Insert(0, new ListItem(Resources.generales.textoSelect, "0"));
            }
            else
            {
                ddlPais.Items.Insert(0, new ListItem(Resources.generales.textoSeleccionar, "0"));
                ddlDepartamento.Items.Insert(0, new ListItem(Resources.generales.textoSeleccionar, "0"));
                ddlProvincia.Items.Insert(0, new ListItem(Resources.generales.textoSeleccionar, "0"));
                ddlDistrito.Items.Insert(0, new ListItem(Resources.generales.textoSeleccionar, "0"));
            }
        }

        private void CargarDepartamento(int idPais)
        {
            ddlDepartamento.DataSource = DistritoBL.Instancia.ListarTodosDepartamentos(idPais);
            ddlDepartamento.DataValueField = "IDDepartamento";
            ddlDepartamento.DataTextField = "DEP_Nombre";
            ddlDepartamento.DataBind();

            if (Session["ddlIdiomas"].ToString() == "en-US")
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
            if (Session["ddlIdiomas"].ToString() == "en-US")
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

            if (Session["ddlIdiomas"].ToString() == "en-US")
            {
                ddlDistrito.Items.Insert(0, new ListItem(Resources.generales.textoSelect, "0"));
            }
            else
            {
                ddlDistrito.Items.Insert(0, new ListItem(Resources.generales.textoSeleccionar, "0"));
            }

           

        }
        private void ObtenerDatos(int idPersonal)
        {
            if (Session["ddlIdiomas"].ToString() == "en-US")
            {
                lblTitulo.Text = "Modify Employee";
            }
            else
            {
                lblTitulo.Text = "Modificar Trabajador";
            }
            Trabajador objPersonal = PersonalBL.Instancia.ObtenerPersonalByID(idPersonal);

            if (objPersonal != null)
            {
                txtApellidos.Text=objPersonal.PER_Apellidos;                
                txtDireccion.Text=objPersonal.PER_Direccion;
                txtDNI.Text=objPersonal.PER_DNI;
                txtNombre.Text=objPersonal.PER_Nombres ;
               
                //ListItem oListItem = ddlLaboratorio.Items.FindByValue(objPersonal.IDLaboratorio.ToString());
                //if (oListItem!=null)
                //{
                //    ddlLaboratorio.SelectedValue = objPersonal.IDLaboratorio.ToString();
                //}
                ListItem oListItem = ddlCargo.Items.FindByValue(objPersonal.IDCargo.ToString());
                if (oListItem != null)
                {
                    ddlCargo.SelectedValue = objPersonal.IDCargo.ToString();
                }
                //verificamos si tiene dirección
                if (objPersonal.IDDistrito != null)
                {
                    Distrito objDistrito = DistritoBL.Instancia.ObtenerDistritoByID(objPersonal.IDDistrito.Value);
                    Provincia objProvincia = DistritoBL.Instancia.ObtenerProvinciaByID(objDistrito.IDProvincia.Value);

                    oListItem = ddlPais.Items.FindByValue(objProvincia.Departamento.IDPais.ToString());
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

        private void SetearValores(ref Trabajador objPersonal)
        {
            objPersonal.PER_Apellidos = txtApellidos.Text;
            objPersonal.PER_Direccion = txtDireccion.Text;
            objPersonal.PER_DNI = txtDNI.Text;
            objPersonal.PER_Nombres = txtNombre.Text;
            objPersonal.IDCargo = ddlCargo.SelectedValue != "0" ? Convert.ToInt32(ddlCargo.SelectedValue) : (int?)null;
            objPersonal.IDDistrito = ddlDistrito.SelectedValue != "0" ? Convert.ToInt32(ddlDistrito.SelectedValue) : (int?)null;
            //objPersonal.IDLaboratorio = ddlLaboratorio.SelectedValue != "0" ? Convert.ToInt32(ddlLaboratorio.SelectedValue) : (int?)null;
            objPersonal.PER_Estado = Constantes.EstadoActivo;
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
                    obj.SelectedIndex = -1;
                }
            }
        }
    }
}