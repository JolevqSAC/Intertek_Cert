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
    public partial class productos : PaginaBase
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
                    ObtenerDatos(Convert.ToInt32(Request["idProducto"].ToString()));
                }

            }
        }

        protected void btnGrabar_Click(object sender, EventArgs e)
        {
            Usuario objUsuario = (Usuario)Session[Constantes.sesionUsuario];
            bool graboOK = false;
            lblMensaje.Text = "";
            string mensajeError = "";

            if (txtNombre.Text != "" )
            {
                if (!EsNuevoRegistro())
                {
                    //actualizar
                    Producto objProducto = ProductoBL.Instancia.ObtenerProductoById(Convert.ToInt32(Request["idProducto"].ToString()));

                    SetearValores(ref objProducto);
                    objProducto.PRO_UsuarioModificacion = objUsuario.IDUsuario.ToString();
                    objProducto.PRO_FechaHoraModificacion = DateTime.Now;

                    try
                    {
                        objProducto = ProductoBL.Instancia.Actualizar(objProducto);
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
                    Producto objProducto = new Producto();
                    SetearValores(ref objProducto);
                    objProducto.PRO_UsuarioCreacion = objUsuario.IDUsuario.ToString();
                    objProducto.PRO_FechaHoraCreacion = DateTime.Now;
                    try
                    {
                        objProducto = ProductoBL.Instancia.Insertar(objProducto);
                        int idproducto = objProducto.IDProducto;
                        objProducto.PRO_Codigo = "PRO" + idproducto.ToString().PadLeft(7, '0');
                        ProductoBL.Instancia.Actualizar(objProducto);
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
            Response.Redirect("productosBuscar.aspx");
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {

            LimpiarFormulario();
            
        }
        private bool EsNuevoRegistro()
        {
            bool esNuevo = true;
            if (Request["idProducto"] != null)
            {
                return false;
            }

            return esNuevo;
        }

        private void CargarDropDownList()
        {
            ddlCategoria.DataSource = CategoriaProductoBL.Instancia.ListarTodosCategoriaProductos();
            ddlCategoria.DataValueField = "IDCategoria";
            ddlCategoria.DataTextField = "CAT_Nombre";
            ddlCategoria.DataBind();
            if (Session["ddlIdiomas"].ToString() == "en-US")
            {
                ddlCategoria.Items.Insert(0, new ListItem(Resources.generales.textoSelect, "0"));
            }
            else
            {
                ddlCategoria.Items.Insert(0, new ListItem(Resources.generales.textoSeleccionar, "0"));
            }
            

            ddlUnidadMedida.DataSource = UnidadMedidaBL.Instancia.ListarUnidadMedidaTodosActivos();
            ddlUnidadMedida.DataValueField = "IDUnidadMedida";
            ddlUnidadMedida.DataTextField = "UNM_Nombre";
            ddlUnidadMedida.DataBind();

            if (Session["ddlIdiomas"].ToString() == "en-US")
            {
                ddlUnidadMedida.Items.Insert(0, new ListItem(Resources.generales.textoSelect, "0"));
            }
            else
            {
                ddlUnidadMedida.Items.Insert(0, new ListItem(Resources.generales.textoSeleccionar, "0"));
            }
            
        }

        private void ObtenerDatos(int idProducto)
        {
            if (Session["ddlIdiomas"].ToString() == "en-US")
            {
                lblTitulo.Text = "Modify Product";
            }
            else
            {
                lblTitulo.Text = "Modificar Producto";
            }
            Producto objProducto = ProductoBL.Instancia.ObtenerProductoById(idProducto);

            if (objProducto != null)
            {
                txtNombre.Text=objProducto.PRO_Nombre;
                txtDescripcion.Text=objProducto.PRO_Descripcion;
                txtingles.Text = objProducto.PRO_NombreIngles;
                ListItem oListItem = ddlCategoria.Items.FindByValue(objProducto.IDCategoria.ToString());
                if (oListItem != null)
                {
                    ddlCategoria.SelectedValue = objProducto.IDCategoria.ToString();
                }
                ListItem oListItemUnidad = ddlUnidadMedida.Items.FindByValue(objProducto.IDUnidadMedida.ToString());
                if (oListItemUnidad != null)
                {
                    ddlUnidadMedida.SelectedValue = objProducto.IDUnidadMedida.ToString();
                }
            }
        }

        private void SetearValores(ref Producto objProducto)
        {
            objProducto.PRO_Nombre = txtNombre.Text;
            objProducto.PRO_NombreIngles = txtingles.Text;
            objProducto.PRO_Descripcion = txtDescripcion.Text;
            objProducto.IDCategoria = ddlCategoria.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlCategoria.SelectedValue);
            objProducto.IDUnidadMedida = ddlUnidadMedida.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlUnidadMedida.SelectedValue);

            objProducto.PRO_Estado = Constantes.EstadoActivo;
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