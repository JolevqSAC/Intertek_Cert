using System;
using System.Collections.Generic;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Intertek.Business.Entities;
using Intertek.Business.Logic;
using Intertek.Helpers;

namespace Intertek.WEB.Mantenimiento
{
    public partial class normaProductos : PaginaBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CargarDropDownList();
                if (!EsNuevoRegistro())
                {
                    ObtenerDatos(Convert.ToInt32(Request["idNormaProducto"].ToString()));
                }
            }
        }

        protected void btnGrabar_Click(object sender, EventArgs e)
        {
            Usuario objLogin = (Usuario)Session[Constantes.sesionUsuario];
            bool graboOK = false;
            lblMensaje.Text = "";
            string mensajeError = "";

            if (txtNombre.Text != "" && ddlProducto.SelectedValue!="0")
            {
                EntityCollection<NormaLimite> listNormaLimite = new EntityCollection<NormaLimite>();

                if (Session["listNormaLimite"] != null)
                {
                    foreach (NormaLimite objNormaLimite in (IList<NormaLimite>)Session["listNormaLimite"])
                    {
                        NormaLimite normaLimite = new NormaLimite();
                        normaLimite.IDEnsayo = objNormaLimite.IDEnsayo;
                        normaLimite.IDMetodo = objNormaLimite.IDMetodo;
                        normaLimite.NOL_Forma = objNormaLimite.NOL_Forma;
                        normaLimite.NOL_Minimo = objNormaLimite.NOL_Minimo;
                        normaLimite.NOL_Maximo = objNormaLimite.NOL_Maximo;

                        listNormaLimite.Add(normaLimite);
                    }
                }

                if (!EsNuevoRegistro())
                {
                    //actualizar
                    var objNormaProducto = NormaProductoBL.Instancia.ObtenerNormaProductoById(Convert.ToInt32(Request["idNormaProducto"]));
                    if (objNormaProducto.NormaLimite == null)
                    {
                        objNormaProducto.NormaLimite = new EntityCollection<NormaLimite>();
                    }
                    else
                    {
                        objNormaProducto.NormaLimite.Clear();
                    }

                    SetearValores(ref objNormaProducto);
                    objNormaProducto.NOR_UsuarioModificacion = objLogin.IDUsuario.ToString();
                    objNormaProducto.NOR_FechaHoraModificacion = DateTime.Now;
                    
                    try
                    {
                        NormaProductoBL.Instancia.Actualizar(objNormaProducto, listNormaLimite);

                        graboOK = true;

                        ObtenerDatosDetalle(objNormaProducto.IDNorma);
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
                    var objNormaProducto = new NormaProducto();
                    SetearValores(ref objNormaProducto);
                    objNormaProducto.NOR_UsuarioCreacion = objLogin.IDUsuario.ToString();
                    objNormaProducto.NOR_FechaHoraCreacion = DateTime.Now;
                    objNormaProducto.NormaLimite = listNormaLimite;

                    try
                    {
                        NormaProductoBL.Instancia.Insertar(objNormaProducto);
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
            Response.Redirect("normaProductoBuscar.aspx");
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            if (!EsNuevoRegistro())
            {
                Response.Redirect("normaProductos.aspx?idNormaProducto=" + Request["idNormaProducto"].ToString());
            }
            else
            {
                Response.Redirect("normaProductos.aspx");
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            Usuario objUsuario = (Usuario)Session[Constantes.sesionUsuario];
            IList<NormaLimite> listNormaLimite = (List<NormaLimite>)Session["listNormaLimite"];
            NormaLimite objEliminar = NormaLimiteBL.Instancia.ObtenerNormaLimiteById(Convert.ToInt32(hdEliminarID.Value));

           // objEliminar.PRO_Estado = Constantes.EstadoEliminado;
            //objEliminar.PRO_UsuarioModificacion = objUsuario.IDUsuario.ToString();

            try
            {
                if (objEliminar != null && objEliminar.IDNormaLimite>0)
                {
                   // NormaLimiteBL.Instancia.Eliminar(objEliminar);
                    NormaLimiteBL.Instancia.EliminarFisico(objEliminar);
                    //ObtenerDatosDetalle(Convert.ToInt32(Request["idNormaProducto"]));
                }
                objEliminar=listNormaLimite.First(delegate(NormaLimite objAux) { return objAux.IDNormaLimite == Convert.ToInt32(hdEliminarID.Value); });
                listNormaLimite.Remove(objEliminar);
                Session["listNormaLimite"] = listNormaLimite;
                CargarGrillaNormaLimite(listNormaLimite);
               // ClientScript.RegisterStartupScript(this.GetType(), "miscriptAlerta", "$(function(){MostrarMensaje('msjSatisfactorio');});", true);
            }
            catch
            {

            }
        }

        protected void gvBuscar_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvBuscar.PageIndex = e.NewPageIndex;

            var datos = Session["cargarNormaLimite"];
            gvBuscar.DataSource = datos;
            gvBuscar.DataBind();
        }

        protected void btnAgregarNormaLimite_Click(object sender, EventArgs e)
        {
            IList<NormaLimite> listNormaLimite=new List<NormaLimite>();
            NormaLimite objNorma = new NormaLimite();
            objNorma.IDEnsayo=ddlEnsayo.SelectedValue!="0"? Convert.ToInt32(ddlEnsayo.SelectedValue):(int?)null;
            objNorma.IDMetodo = ddlMetodo.SelectedValue != "0" ? Convert.ToInt32(ddlMetodo.SelectedValue) : (int?)null;
            objNorma.NOL_Minimo = txtValorMinimo.Text;
            objNorma.NOL_Maximo = txtValorMaximo.Text;
            objNorma.NOL_Forma = txtForma.Text;
            Ensayo objEnsayo = new Ensayo();
            objEnsayo.IDLaboratorio = ddlLaboratorio.SelectedValue != "0" ? Convert.ToInt32(ddlLaboratorio.SelectedValue) : (int?)null;
            //objEnsayo.IDEnsayo = ddlEnsayo.SelectedValue != "0" ? Convert.ToInt32(ddlEnsayo.SelectedValue) : (int?)null;
            objEnsayo.ENS_Nombre = ddlEnsayo.SelectedItem.Text;
            objNorma.Ensayo = objEnsayo;
            //objNorma.Ensayo.IDLaboratorio = ddlLaboratorio.SelectedValue != "0" ? Convert.ToInt32(ddlLaboratorio.SelectedValue) : (int?)null;
            //objNorma.Ensayo.ENS_Nombre = ddlEnsayo.SelectedItem.Text;
            Metodo objMetodo = new Metodo();
            objMetodo.MET_Nombre = ddlMetodo.SelectedItem.Text;
            objNorma.Metodo = objMetodo;
            //objNorma.Metodo.MET_Nombre = ddlMetodo.SelectedItem.Text;
            if (Session["listNormaLimite"] != null)
            {
                listNormaLimite = (IList<NormaLimite>)Session["listNormaLimite"]; 
            }
            listNormaLimite.Add(objNorma);

            Session["listNormaLimite"] = listNormaLimite;
            CargarGrillaNormaLimite(listNormaLimite);
        
        }

        protected void ddlLaboratorio_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlLaboratorio.SelectedValue != "0")
            {
                CargarEnsayo(Convert.ToInt32(ddlLaboratorio.SelectedValue));
            }
        }

        private void CargarDropDownList()
        {
            ddlProducto.DataSource = ProductoBL.Instancia.ListarProductosTodos();
            ddlProducto.DataValueField = "IDProducto";
            ddlProducto.DataTextField = "PRO_Nombre";
            ddlProducto.DataBind();
            ddlProducto.Items.Insert(0, new ListItem(Resources.generales.textoSeleccionar, "0"));

            ddlLaboratorio.DataSource = LaboratorioBL.Instancia.ListarTodosLaboratorios();
            ddlLaboratorio.DataValueField = "IDLaboratorio";
            ddlLaboratorio.DataTextField = "LAB_Nombre";
            ddlLaboratorio.DataBind();
            ddlLaboratorio.Items.Insert(0, new ListItem(Resources.generales.textoSeleccionar, "0"));

            ddlEnsayo.Items.Insert(0, new ListItem(Resources.generales.textoSeleccionar, "0"));

            ddlMetodo.DataSource = MetodoBL.Instancia.ListarMetodosTodos();
            ddlMetodo.DataValueField = "IDMetodo";
            ddlMetodo.DataTextField = "MET_Nombre";
            ddlMetodo.DataBind();
            ddlMetodo.Items.Insert(0, new ListItem(Resources.generales.textoSeleccionar, "0"));
        }

        private void CargarEnsayo(int idLaboratorio)
        {
            ddlEnsayo.DataSource = EnsayoBL.Instancia.ListarEnsayosByLaboratorio(idLaboratorio);
            ddlEnsayo.DataValueField = "IDEnsayo";
            ddlEnsayo.DataTextField = "ENS_Nombre";
            ddlEnsayo.DataBind();
            ddlEnsayo.Items.Insert(0, new ListItem(Resources.generales.textoSeleccionar, "0"));
        }

        private void ObtenerDatos(int idNormaProducto)
        {
            NormaProducto objNormaProducto = NormaProductoBL.Instancia.ObtenerNormaProductoById(idNormaProducto);
            if (objNormaProducto != null)
            {
                txtNombre.Text = objNormaProducto.NOR_Nombre;
                txtAcreditador.Text = objNormaProducto.NOR_Acreditador;
                txtAnio.Text = objNormaProducto.NOR_Anio.ToString();
                txtObservaciones.Text = objNormaProducto.NOR_Observaciones;
                ListItem oListItem = ddlProducto.Items.FindByValue(objNormaProducto.IDProducto.ToString());
                if (oListItem != null)
                {
                    ddlProducto.SelectedValue = objNormaProducto.IDProducto.ToString();
                }

                ObtenerDatosDetalle(objNormaProducto.IDNorma);
            }

        }

        private void ObtenerDatosDetalle(int idNormaProducto)
        {
            IList<NormaLimite> listNormaLimite = NormaLimiteBL.Instancia.ListarNormaLimites(idNormaProducto);
            Session["listNormaLimite"] = listNormaLimite;

            CargarGrillaNormaLimite(listNormaLimite);

        }

        private void CargarGrillaNormaLimite(IList<NormaLimite> listNormaLimite)
        {
            var datos = (from p in listNormaLimite
                         select new
                         {
                             ENS_Nombre = p.Ensayo.ENS_Nombre,
                             MET_Nombre = p.Metodo.MET_Nombre,
                             p.NOL_Minimo,
                             p.NOL_Maximo,
                             p.NOL_Forma,
                             p.IDNormaLimite,
                             p.IDEnsayo,
                             p.IDMetodo,
                             IDLaboratorio = p.Ensayo.IDLaboratorio
                         }).ToList();

            Session["cargarNormaLimite"] = datos;

            gvBuscar.DataSource = datos;
            gvBuscar.DataBind();
        }

        private void SetearValores(ref NormaProducto objNormaProducto)
        {
            objNormaProducto.NOR_Nombre = txtNombre.Text;
            objNormaProducto.NOR_Acreditador = txtAcreditador.Text;
            objNormaProducto.NOR_Anio = Convert.ToInt32(txtAnio.Text);
            objNormaProducto.NOR_Observaciones = txtObservaciones.Text;
            objNormaProducto.IDProducto = ddlProducto.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlProducto.SelectedValue);
            objNormaProducto.NOR_Estado = Constantes.EstadoActivo;
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
            if (Request["idNormaProducto"] != null)
            {
                return false;
            }

            return esNuevo;
        }
    }
}