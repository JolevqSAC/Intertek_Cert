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
    public partial class categoriaProductos : PaginaBase
    {
        CategoriaProducto objcategoria = new CategoriaProducto();
        string accion = "";
        
        protected void Page_Load(object sender, EventArgs e)
        {
            accion = Request.QueryString.Get("accion");
            if (!Page.IsPostBack)
            {
                if (Session["ddlIdiomas"].ToString() == "en-US")
                {
                    imgGrabar.Src = Resources.generales.imgGrabarUS;
                }
                if (accion == "M")
                    CargarDatos(Request.QueryString.Get("idcategoria"));
            }
        }

        private void CargarDatos(string idcategoria)
        {
            objcategoria.IDCategoria = Convert.ToInt32(idcategoria);
            var entidad = CategoriaProductoBL.Instancia.ObtenerCategoriaProductoByID(objcategoria.IDCategoria);
            txtNombre.Text = entidad.CAT_Nombre;
            txtingles.Text = entidad.CAT_NombreIngles;
            txtDescripcion.Text = entidad.CAT_Descripcion;
            rbIndicadorArea.SelectedValue = entidad.CAT_IndicadorArea;

            if (Session["ddlIdiomas"].ToString() == "en-US")
            {
                lblTitulo.Text = "Modify Category Product";
            }
            else
            {
                lblTitulo.Text = "Modificar Categoría del Producto";
            }
        }

        protected void btnGrabar_Click(object sender, EventArgs e)
        {
            GuardarDatos();
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("categoriaProductosBuscar.aspx");
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void GuardarDatos()
        {
            switch (accion)
            {
                case "N":
                    Insertar();
                    break;
                case "M":
                    Modificar();
                    break;
            }
        }

        private void Insertar()
        {
            try
            {
                if (txtNombre.Text != "")
                {
                    Usuario objusuario = (Usuario)Session[Constantes.sesionUsuario];
                    objcategoria.CAT_Nombre = txtNombre.Text.Trim();
                    objcategoria.CAT_NombreIngles = txtingles.Text.Trim();
                    objcategoria.CAT_Descripcion = txtDescripcion.Text.Trim();
                    objcategoria.CAT_Estado = Constantes.EstadoActivo;
                    objcategoria.CAT_UsuarioCreacion = objusuario.IDUsuario.ToString();
                    objcategoria.CAT_FechaHoraCreacion = DateTime.Now;
                    objcategoria.CAT_IndicadorArea = rbIndicadorArea.SelectedValue.ToString();
                    objcategoria = CategoriaProductoBL.Instancia.Insertar(objcategoria);
                    int idcategoria = objcategoria.IDCategoria;
                    objcategoria.CAT_Codigo = "CAT" + idcategoria.ToString().PadLeft(7, '0');
                    CategoriaProductoBL.Instancia.Actualizar(objcategoria);
                    LimpiarCampos();
                    ClientScript.RegisterStartupScript(this.GetType(), "Confirmacion", "$(function(){MostrarMensaje('msjRegistroOK');});", true);
                }
            }
            catch
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Error", "$(function(){MostrarMensaje('msjErrorGrabar');});", true);
            }

        }


        private void Modificar()
        {
            Usuario objusuario = (Usuario)Session[Constantes.sesionUsuario];
            objcategoria.IDCategoria = Convert.ToInt32(Request.QueryString.Get("idcategoria"));
            var entidad = CategoriaProductoBL.Instancia.ObtenerCategoriaProductoByID(objcategoria.IDCategoria);
            entidad.CAT_Nombre = txtNombre.Text;
            entidad.CAT_NombreIngles = txtingles.Text;
            entidad.CAT_Descripcion = txtDescripcion.Text;
            entidad.CAT_IndicadorArea = rbIndicadorArea.SelectedValue.ToString();
            entidad.CAT_UsuarioModificacion = objusuario.IDUsuario.ToString();
            entidad.CAT_FechaHoraModificacion = DateTime.Now;

                try
                {
                    CategoriaProductoBL.Instancia.Actualizar(entidad);
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
                this.txtNombre.Text = " ";
                this.txtingles.Text = " ";
                this.txtDescripcion.Text = " ";
                this.rbIndicadorArea.SelectedIndex = -1;
            
        }

    }
}