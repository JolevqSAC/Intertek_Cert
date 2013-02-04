using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
//agregar referencias
using Intertek.Business.Entities;
using Intertek.Business.Logic;
using Intertek.Helpers;
using System.Globalization;
using System.Web;

namespace Intertek.WEB.Mantenimiento
{
    public partial class ensayosBuscar : PaginaBase
        
    {
        public string Cultura { get; set; }

        public string ruta { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
           
            Cultura =(string)CultureInfo.CurrentCulture.Name;
            if (Cultura.Equals("en-US"))
            {
                string host = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);
                string rootApplication = HttpContext.Current.Request.ApplicationPath;
                ruta = host + rootApplication + "img/en/btn_modify.png";

            }

            string Eliminar  = HttpContext.GetLocalResourceObject("~/Mantenimiento/ensayosBuscar.aspx", "btnEliminarResource1", new System.Globalization.CultureInfo(Cultura)).ToString();
  
            btnEliminar0.Value = Eliminar;
        
            txtCodigo.Focus();
            if (!Page.IsPostBack)
            {
                CargarDatos();
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)

        {
          
    
            CargarDatos();
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();


        }
        void Limpiar()
        {

            txtCodigo.Text = string.Empty;
            txtDescripcion.Text = string.Empty;
            txtNombre.Text = string.Empty;
            txtNombreIngles.Text = string.Empty;
            CargarDatos();
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
       
            Response.Redirect("neoensayos.aspx?accion=N");

        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            Usuario objUsuario = (Usuario)Session[Constantes.sesionUsuario];

            try
            {
                foreach (GridViewRow row in gvBuscar.Rows)
                {
                    CheckBox chbx = (CheckBox)row.FindControl("chkSeleccion");

                    if (chbx != null && chbx.Checked)
                    {
                        HiddenField hidIdEnsayo = (HiddenField)row.FindControl("hidIdEnsayo");
                        int idEnsayo = Convert.ToInt32(hidIdEnsayo.Value);
                        Ensayo objEnsayo = EnsayoBL.Instancia.ObtenerEnsayoById(idEnsayo);
                        objEnsayo.ENS_Estado = Constantes.EstadoEliminado;
                        objEnsayo.ENS_UsuarioModificacion = objUsuario.IDUsuario.ToString();
                        objEnsayo.ENS_FechaHoraModificacion = DateTime.Now;
                        EnsayoBL.Instancia.Actualizar(objEnsayo);
                    }
                }

                ClientScript.RegisterStartupScript(this.GetType(), "Confirmacion", "$(function(){MostrarMensaje('msjEliminacionOK');});", true);
            }
            catch
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Error", "$(function(){MostrarMensaje('msjErrorEliminar');});", true);
            }

            CargarDatos();
        }

        protected void gvBuscar_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvBuscar.PageIndex = e.NewPageIndex;

            var datos=Session["cargarEnsayos"];
            gvBuscar.DataSource = datos;
            gvBuscar.DataBind();

            
        }

        private void CargarDatos()
        {
            lblmensaje.Text = string.Empty;
            Ensayo objEnsayo = new Ensayo();
            objEnsayo.ENS_Codigo = txtCodigo.Text;
            objEnsayo.ENS_Nombre = txtNombre.Text;
            objEnsayo.ENS_NombreIngles = txtNombreIngles.Text;
            objEnsayo.ENS_Descripcion = txtDescripcion.Text;

            IList<Ensayo> listEnsayos = EnsayoBL.Instancia.ListarEnsayos(objEnsayo);
            //var datos = (from p in listEnsayos
            //             select new
            //             {
            //                 LAB_Nombre = p.Laboratorio==null?"": p.Laboratorio.LAB_Nombre,
            //                 p.IDEnsayo,
            //                 p.ENS_Nombre
            //             }).ToList();

            if (listEnsayos.Count > 0)
            {
                gvBuscar.DataSource = listEnsayos;
                gvBuscar.DataBind();

                Session["cargarEnsayos"] = listEnsayos;

                //if(Cultura.Equals("es-ES"))
                //{
                //    lblmensaje.Text = "Total de registros encontrados: " + listEnsayos.Count.ToString();  
                //}
                //else if (Cultura.Equals("en-US"))
                //{
                //    lblmensaje.Text = "Total records found: " + listEnsayos.Count.ToString();  
                //}


              
            }
            else
            {
                gvBuscar.DataSource = null;
                gvBuscar.DataBind();

                //if (Cultura.Equals("es-ES"))
                //{
                //    lblmensaje.Text = "Total records found: " + listEnsayos.Count.ToString();
                //}
                //else if (Cultura.Equals("en-US"))
                //{
                //    lblmensaje.Text = "No data available: ";    
                //}

                if (Cultura.Equals("en-US"))
                {
                    lblmensaje.Text = "No Data Found";
                }
                else
                {
                    lblmensaje.Text = "No Existen Datos Encontrados";
                }
               
            }           
        }

        protected void gvBuscar_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Editar":
                    Response.Redirect(string.Format("neoensayos.aspx?IDEnsayo={0}", Convert.ToInt32(e.CommandArgument)), true);
                    break;
            }
        }

    
    }
}