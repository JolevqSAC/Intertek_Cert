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
    public partial class personalBuscar : PaginaBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CargarCargo();
                CargarDatos();
                if (Session["ddlIdiomas"].ToString() == "en-US")
                {
                    imgeliminar.Src = Resources.generales.imgEliminarUS;
                } 
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarDatos();
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            Response.Redirect("personalBuscar.aspx");
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("personal.aspx");
        }

        private void CargarCargo()
        {
            Cargo objcargo = new Cargo();
            ddlcargo.DataSource = CargosBL.Instancia.ListarTodosCargos();
            ddlcargo.DataValueField = "IDCargo";
            ddlcargo.DataTextField = "CAR_Nombre";
            ddlcargo.DataBind();

            if ((Session["ddlIdiomas"].ToString() == "en-US"))
            {
                ddlcargo.Items.Insert(0, new ListItem(Resources.generales.testoAll, "0"));
            }
            else
            {
                ddlcargo.Items.Insert(0, new ListItem(Resources.generales.textoTodos, "0"));
            }
            
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            Eliminar();
        }

        private void Eliminar()
        {
            var listaEliminados = new List<int>();

            foreach (GridViewRow row in gvBuscar.Rows)
            {
                var chkBox = (CheckBox)row.Cells[3].FindControl("chkSeleccion");

                if (chkBox != null)
                {
                    if (chkBox.Checked)
                    {
                        var hidIdPersonal = (HiddenField)row.Cells[3].FindControl("hidIdPersonal");
                        var idpersonal = Convert.ToInt32(hidIdPersonal.Value);
                        listaEliminados.Add(idpersonal);
                    }
                }
            }

            Usuario objusuario = (Usuario)Session[Constantes.sesionUsuario];

            var listapersonal = new List<Trabajador>();

            foreach (var eliminado in listaEliminados)
            {
                var entidad = PersonalBL.Instancia.ObtenerPersonalByID(eliminado);
                entidad.PER_Estado = Constantes.EstadoEliminado;
                entidad.PER_UsuarioModificacion = objusuario.IDUsuario.ToString();
                entidad.PER_FechaHoraModificacion = DateTime.Now;

                listapersonal.Add(entidad);
            }

            try
            {
                foreach (var personal in listapersonal)
                {
                    PersonalBL.Instancia.Actualizar(personal);
                }

                ClientScript.RegisterStartupScript(this.GetType(), "Confirmacion", "$(function(){MostrarMensaje('msjSatisfactorio');});", true);
            }
            catch
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Error", "$(function(){MostrarMensaje('msjErrorEliminar');});", true);
            }
            CargarDatos();
        }

        private void LlenarGridview()
        {
            var listPersonal = PersonalBL.Instancia.ListarPersonalTodos();
            var datos = (from p in listPersonal
                         select new
                         {

                             CAR_Nombre = p.Cargo == null ? "" : p.Cargo.CAR_Nombre,
                             p.IDPersonal,
                             p.PER_DNI,
                             p.PER_Apellidos,
                             p.PER_Nombres,
                             p.PER_Codigo
                         }).ToList();
            gvBuscar.DataSource = datos;
            gvBuscar.DataBind();
        }


        protected void gvBuscar_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvBuscar.PageIndex = e.NewPageIndex;
            CargarDatos();
        }

        private void CargarDatos()
        {
            Trabajador objPersonal = new Trabajador();
            objPersonal.PER_Codigo = txtCodigo.Text;
            objPersonal.PER_Nombres = txtNombre.Text;
            objPersonal.PER_Apellidos = txtApellidos.Text;
            objPersonal.PER_DNI = txtDNI.Text;
            objPersonal.IDCargo = ddlcargo.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlcargo.SelectedValue);

            var listPersonal = PersonalBL.Instancia.ListarPersonalPaginado(objPersonal);
            if (listPersonal.Count != 0)
            {
                lblmensaje.Text = "";
                var datos = (from p in listPersonal
                             select new
                             {
                                 CAR_Nombre = p.Cargo == null ? "" : p.Cargo.CAR_Nombre,
                                 p.IDPersonal,
                                 p.PER_DNI,
                                 p.PER_Apellidos,
                                 p.PER_Nombres,
                                 p.PER_Codigo
                             }).ToList();

                gvBuscar.DataSource = datos;
                gvBuscar.DataBind();
            }
            else
            {
                gvBuscar.DataSource = null;
                gvBuscar.DataBind();
                if ((Session["ddlIdiomas"].ToString() == "en-US"))
                {
                    lblmensaje.Text = "No Data Found";
                }
                else
                {
                    lblmensaje.Text = "No Existen Datos Encontrados";
                }

            }
            gvBuscar.PageIndex = 0;
        }

        protected void gvBuscar_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            
            switch (e.CommandName)
            {
                case "Editar":
                    Response.Redirect(string.Format("personal.aspx?idPersonal={0}", Convert.ToInt32(e.CommandArgument)), true);
                    break;
            }

        }
    }
}