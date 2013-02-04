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

namespace Intertec.WEB.Mantenimiento
{
    public partial class neoensayos : PaginaBase
    {
        public string Cultura { get; set; }
        string accion = string.Empty;

        private List<SubEnsayo> items { get; set; }
      

        protected void Page_Load(object sender, EventArgs e)
        {
            Cultura = (string)CultureInfo.CurrentCulture.Name;

            if (!Page.IsPostBack)
            {
                //CargarDropDownList();
             
                string Eliminar = HttpContext.GetLocalResourceObject("~/Mantenimiento/neoclientes.aspx", "btnQuitar2ContactoResource1", new System.Globalization.CultureInfo(Cultura)).ToString();

                btnEliminar2.Value = Eliminar;

                accion = Request.QueryString.Get("accion");

                opciones();
                if (!EsNuevoRegistro())
                {
                    ObtenerDatos(Convert.ToInt32(Request["idEnsayo"].ToString()));

                    if (Cultura.Equals("en-US"))
                    {
                        lblTitulo.Text = "Modify Test";
                    }
                    else

                    {
                        lblTitulo.Text = "Modificar Ensayo";
                    }
                    
                }
                else
                {
                    if (Cultura.Equals("en-US"))
                    {
                        lblTitulo.Text = "Register Essay";
                    }
                    else
                    {
                      lblTitulo.Text = "Registrar Ensayo";
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
                 
                    break;
                case "M":
                    // ModificarLaboratorio();
                    break;
            }
        }


        private void cargarSubEnsayo()
        {
          
            IList<SubEnsayo> lstSubEnsayoOld = SubEnsayoBL.Instancia.ObtenerSubEnsayos(0);

            Session["snSubEnsayo"] = lstSubEnsayoOld;
        }
        private void EnlazarGrilla()
        {
            if (Session["snSubEnsayo"] == null)
            {
                cargarSubEnsayo();
            }
            items = (List<SubEnsayo>)Session["snSubEnsayo"];
            grvwSubEnsayo.DataSource = items;
            grvwSubEnsayo.DataBind();

        }

    


        protected void btnGrabar_Click(object sender, EventArgs e)
        {
            Usuario objLogin = (Usuario)Session[Constantes.sesionUsuario];
            bool graboOK = false;
            lblMensaje.Text = "";
            string mensajeError = "";
            int idEnsayo = 0;
            IList<SubEnsayo> lstSubEnsayo = (IList<SubEnsayo>)Session["snSubEnsayo"];

            if (txtNombre.Text != "")
            {
                if (!EsNuevoRegistro())
                {
                    //actualizar
                    IList<SubEnsayo> lstSubEnsayoOld = SubEnsayoBL.Instancia.ObtenerSubEnsayos(Convert.ToInt32(Request["idEnsayo"].ToString()));
                    Ensayo objEnsayo = EnsayoBL.Instancia.ObtenerEnsayoById(Convert.ToInt32(Request["idEnsayo"].ToString()));
                    idEnsayo = objEnsayo.IDEnsayo;
                    SetearValores(ref objEnsayo);
                    objEnsayo.ENS_UsuarioModificacion = objLogin.IDUsuario.ToString();
                    objEnsayo.ENS_FechaHoraModificacion = DateTime.Now;

                    try
                    {
                        objEnsayo = EnsayoBL.Instancia.Actualizar(objEnsayo);

                        for (int j = 0; j < lstSubEnsayoOld.Count; j++)
                        {
                            SubEnsayo objSubEnsayo = SubEnsayoBL.Instancia.ObtenerSubEnsayoById(lstSubEnsayoOld[j].IDSubEnsayo);
                            SubEnsayoBL.Instancia.Eliminar(objSubEnsayo);
                        }

                        for (int j = 0; j < lstSubEnsayo.Count; j++)
                        {
                            SubEnsayo objSubEnsayo = new SubEnsayo();
                            objSubEnsayo.SEN_Nombre = lstSubEnsayo[j].SEN_Nombre.ToString();
                            objSubEnsayo.SEN_NombreIngles = lstSubEnsayo[j].SEN_NombreIngles.ToString();
                            objSubEnsayo.SEN_Estado = Constantes.EstadoActivo;
                            objSubEnsayo.IDEnsayo = idEnsayo;
                            objSubEnsayo.SEN_UsuarioCreacion = objLogin.IDUsuario.ToString();
                            objSubEnsayo.SEN_FechaHoraCreacion = DateTime.Now;
                            SubEnsayoBL.Instancia.Insertar(objSubEnsayo);
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
                    Ensayo objEnsayo = new Ensayo();
                    SetearValores(ref objEnsayo);
                    objEnsayo.ENS_UsuarioCreacion = objLogin.IDUsuario.ToString();
                    objEnsayo.ENS_FechaHoraCreacion = DateTime.Now;
                    try
                    {
                        objEnsayo = EnsayoBL.Instancia.Insertar(objEnsayo);
                        idEnsayo = objEnsayo.IDEnsayo;
                        objEnsayo.ENS_Codigo = "ENS" + idEnsayo.ToString().PadLeft(7, '0');
                        EnsayoBL.Instancia.Actualizar(objEnsayo);

                        if (lstSubEnsayo != null)
                        {
                            for (int j = 0; j < lstSubEnsayo.Count; j++)
                            {
                                SubEnsayo objSubEnsayo = new SubEnsayo();
                                objSubEnsayo.SEN_Nombre = lstSubEnsayo[j].SEN_Nombre.ToString();
                                objSubEnsayo.SEN_NombreIngles = lstSubEnsayo[j].SEN_NombreIngles.ToString();
                                objSubEnsayo.SEN_Estado = Constantes.EstadoActivo;
                                objSubEnsayo.IDEnsayo = idEnsayo;
                                objSubEnsayo.SEN_UsuarioCreacion = objLogin.IDUsuario.ToString();
                                objSubEnsayo.SEN_FechaHoraCreacion = DateTime.Now;
                                SubEnsayoBL.Instancia.Insertar(objSubEnsayo);
                            }
                        }

                        graboOK = true;
                        //LimpiarFormulario();
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
                Session["snSubEnsayo"] = null;
                ClientScript.RegisterStartupScript(this.GetType(), "miscriptError", "$(function(){MostrarMensaje('msjSatisfactorio');});", true);
                //Response.Redirect("ensayosBuscar.aspx");
            }
            else
            {
                Session["snSubEnsayo"] = null;
                lblMensaje.Text = mensajeError;
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Session["snSubEnsayo"] = null;
            Response.Redirect("ensayosBuscar.aspx");
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            if (!EsNuevoRegistro())
            {
                Response.Redirect("neoensayos.aspx?idEnsayo=" + Request["idEnsayo"].ToString());
            }
            else
            {
                Response.Redirect("neoensayos.aspx");
            }
        }

        //private void CargarDropDownList()
        //{
        //    ddlLaboratorio.DataSource = LaboratorioBL.Instancia.ListarTodosLaboratorios();
        //    ddlLaboratorio.DataValueField = "IDLaboratorio";
        //    ddlLaboratorio.DataTextField = "LAB_Nombre";
        //    ddlLaboratorio.DataBind();
        //    ddlLaboratorio.Items.Insert(0, new ListItem(Resources.generales.textoSeleccionar, "0"));
        //}

        private void ObtenerDatos(int idEnsayo)
        {
            Ensayo objEnsayo = EnsayoBL.Instancia.ObtenerEnsayoById(idEnsayo);
            IList<SubEnsayo> lstSubEnsayo = SubEnsayoBL.Instancia.ObtenerSubEnsayos(idEnsayo);
            Session["snSubEnsayo"] = lstSubEnsayo;

            if (objEnsayo != null)
            {
                hdflIDEnsayo.Value = objEnsayo.IDEnsayo.ToString();
                txtNombre.Text = objEnsayo.ENS_Nombre;
                txtNombreIngles.Text = objEnsayo.ENS_NombreIngles;
                txtDescripcion.Text = objEnsayo.ENS_Descripcion;
            }

            grvwSubEnsayo.DataSource = lstSubEnsayo;
            grvwSubEnsayo.DataBind();
        }

        private void SetearValores(ref Ensayo objEnsayo)
        {
            objEnsayo.ENS_Nombre = txtNombre.Text;
            objEnsayo.ENS_NombreIngles = txtNombreIngles.Text;
            objEnsayo.ENS_Descripcion = txtDescripcion.Text;
            objEnsayo.ENS_Estado = Constantes.EstadoActivo;
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
            if (Request["idEnsayo"] != null)
            {
                return false;
            }

            return esNuevo;
        }

        protected void grvwSubEnsayo_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvwSubEnsayo.PageIndex = e.NewPageIndex;

            var datos = Session["cargarEnsayos"];
            grvwSubEnsayo.DataSource = datos;
            grvwSubEnsayo.DataBind();
        }

        protected void btnQuitarSubEnsayo_Click(object sender, EventArgs e)
        {
            Usuario objusuario = (Usuario)Session[Constantes.sesionUsuario];
            IList<SubEnsayo> lstSubEnsayo = (IList<SubEnsayo>)Session["snSubEnsayo"];
            int contador = 0;

            foreach (GridViewRow row in grvwSubEnsayo.Rows)
            {
                CheckBox chbx = (CheckBox)row.FindControl("chkSeleccionSubEnsayo");

                if (chbx != null && chbx.Checked)
                {
                    lstSubEnsayo.RemoveAt(contador);
                    contador--;
                }

                contador = contador + 1;
            }

            Session["snSubEnsayo"] = lstSubEnsayo;
            grvwSubEnsayo.DataSource = lstSubEnsayo;
            grvwSubEnsayo.DataBind();
        }

        protected void btnAgregarSubEnsayo_Click(object sender, EventArgs e)
        {
            if (txtNombreSubensayo.Text != "")
            {
                SubEnsayo objSubEnsayo = new SubEnsayo();
                IList<SubEnsayo> lstSubEnsayo = (IList<SubEnsayo>)Session["snSubEnsayo"];

                if (!EsNuevoRegistro())
                {
                    objSubEnsayo.SEN_Nombre = txtNombreSubensayo.Text;
                    objSubEnsayo.SEN_NombreIngles = txtNombreInglesSubensayo.Text;
                    objSubEnsayo.SEN_Estado = Constantes.EstadoActivo;
                    objSubEnsayo.IDSubEnsayo = Convert.ToInt32(Request["idEnsayo"].ToString());

                    lstSubEnsayo.Add(objSubEnsayo);
                    Session["snSubEnsayo"] = lstSubEnsayo;
                }
                else
                {
                    objSubEnsayo.SEN_Nombre = txtNombreSubensayo.Text;
                    objSubEnsayo.SEN_NombreIngles = txtNombreInglesSubensayo.Text;
                    objSubEnsayo.SEN_Estado = Constantes.EstadoActivo;

                    if (lstSubEnsayo == null)
                    {
                        lstSubEnsayo = SubEnsayoBL.Instancia.ObtenerSubEnsayos(0);
                        lstSubEnsayo.Add(objSubEnsayo);
                    }
                    else
                    {
                        lstSubEnsayo.Add(objSubEnsayo);
                    }
                    Session["snSubEnsayo"] = lstSubEnsayo;
                }

                txtNombreSubensayo.Text = "";
                txtNombreInglesSubensayo.Text = "";
                grvwSubEnsayo.DataSource = lstSubEnsayo;
                grvwSubEnsayo.DataBind();
            }
            else
            {
                lblMensaje.Text = "Se debe agregar Nombre, Apellido o Telefono SubEnsayo";
            }
        }

   

        protected void bntActualizarSubEnsayo_Click(object sender, EventArgs e)
        {
            if (txtNombreSubensayo.Text != "")
            {
                IList<SubEnsayo> lstSubEnsayo = (IList<SubEnsayo>)Session["snSubEnsayo"];
                for (int i = 0; i < lstSubEnsayo.Count; i++)
                {
                    if (lstSubEnsayo[i].IDSubEnsayo == int.Parse(hdflIdSubEnsayo.Value.ToString()))
                    {
                        hdflIdSubEnsayo.Value = string.Empty;
                        lstSubEnsayo[i].SEN_Nombre = txtNombreSubensayo.Text;
                        lstSubEnsayo[i].SEN_NombreIngles = txtNombreInglesSubensayo.Text;
                        break;
                    }
                }

                grvwSubEnsayo.DataSource = lstSubEnsayo;
                grvwSubEnsayo.DataBind();

                visibleBtnSubEnsayo(false);
                clearSubEnsayo();
            }
        }

        protected void bntCancelarSubEnsayo_Click(object sender, EventArgs e)
        {
            visibleBtnSubEnsayo(false);
            clearSubEnsayo();
        }

        void visibleBtnSubEnsayo(bool value)
        {
            divbtnAgregarSubEnsayo.Visible = !value;
            divbtnQuitarSubEnsayo.Visible = !value;
            divbntActualizarSubEnsayo.Visible = value;
            divbntCancelarSubEnsayo.Visible = value;
            grvwSubEnsayo.Enabled = !value;
        }

        void clearSubEnsayo()
        {
            txtNombre.Text = "";
            txtNombreIngles.Text = "";
        }

        protected void grvwSubEnsayo_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            switch (e.CommandName)
            {
                case "Editar":

                    int idSubEnsayo = int.Parse(e.CommandArgument.ToString());

                    if (idSubEnsayo >= 0)
                    {
                        IList<SubEnsayo> lstSubEnsayo = (IList<SubEnsayo>)Session["snSubEnsayo"];

                        foreach (SubEnsayo objSubEnsayo in lstSubEnsayo)
                        {
                            if (objSubEnsayo.IDSubEnsayo == idSubEnsayo)
                            {
                                hdflIdSubEnsayo.Value = idSubEnsayo.ToString();
                                txtNombreSubensayo.Text = objSubEnsayo.SEN_Nombre.ToString();
                                txtNombreInglesSubensayo.Text = objSubEnsayo.SEN_NombreIngles.ToString();
                                break;
                            }
                        }
                        string actualizar = HttpContext.GetLocalResourceObject("~/Mantenimiento/neoensayos.aspx", "bntActualizarSubEnsayoResource1", new System.Globalization.CultureInfo(Cultura)).ToString();
                        bntActualizarSubEnsayo.Text = actualizar;
                        visibleBtnSubEnsayo(true);

                    }

                    break;
            }

        }
    }
}