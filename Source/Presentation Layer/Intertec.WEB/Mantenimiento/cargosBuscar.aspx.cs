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
    public partial class cargosBuscar : PaginaBase
    {
        Cargo objcargo = new Cargo();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtCodigo.Focus();
                CargarArea();
                BuscarPorFiltros();
                if (Session["ddlIdiomas"].ToString() == "en-US")
                {
                    imgeliminar.Src = Resources.generales.imgEliminarUS;
                }  
            }
            
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            BuscarPorFiltros();
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            Response.Redirect("cargosBuscar.aspx");
        }
        
        
        private void CargarArea()
        {
            Area objarea = new Area();
            ddlarea.DataSource = AreaBL.Instancia.ListarTodosAreas();
            ddlarea.DataValueField = "IDArea";
            ddlarea.DataTextField = "ARE_Nombre";
            ddlarea.DataBind();
            if (Session["ddlIdiomas"].ToString() == "en-US")
            {
                ddlarea.Items.Insert(0, new ListItem(Resources.generales.testoAll, "0"));
            }
            else
            {
                ddlarea.Items.Insert(0, new ListItem(Resources.generales.textoTodos, "0"));
            }
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("cargos.aspx");
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            Eliminar();           
        }

        private void LlenarGridview()
        {            
            var listcargo = CargosBL.Instancia.ListarTodosCargos();
            var datos = (from x in listcargo
                         select new
                         {
                             x.IDCargo,
                             CAR_Codigo = x.CAR_Codigo == null ? "" : x.CAR_Codigo,                             
                             CAR_Nombre = x.CAR_Nombre == null ? "" : x.CAR_Nombre,
                             CAR_Descripcion = x.CAR_Descripcion == null ? "" : x.CAR_Descripcion,
                             ARE_Nombre = x.Area == null ? "" : x.Area.ARE_Nombre
                         }).ToList();
            gvBuscar.DataSource = datos;
            gvBuscar.DataBind();
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
                        var hidIdCargo = (HiddenField)row.Cells[3].FindControl("hidIdCargo");
                        var idcargo = Convert.ToInt32(hidIdCargo.Value);
                        listaEliminados.Add(idcargo);
                    }
                }
            }

            Usuario objusuario = (Usuario)Session[Constantes.sesionUsuario];

            var listacargo = new List<Cargo>();

            foreach (var eliminado in listaEliminados)
            {
                var entidad = CargosBL.Instancia.ObtenerCargoByID(eliminado);
                entidad.CAR_Estado = Constantes.EstadoEliminado;
                entidad.CAR_UsuarioModificacion = objusuario.IDUsuario.ToString();
                entidad.CAR_FechaHoraModificacion = DateTime.Now;

                listacargo.Add(entidad);
            }

            try
            {
                foreach (var area in listacargo)
                {
                    CargosBL.Instancia.Actualizar(area);
                }

                ClientScript.RegisterStartupScript(this.GetType(), "Confirmacion", "$(function(){MostrarMensaje('msjSatisfactorio');});", true);
            }
            catch
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Error", "$(function(){MostrarMensaje('msjErrorEliminar');});", true);
            }
            BuscarPorFiltros();
        }

      
        protected void gvBuscar_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvBuscar.PageIndex = e.NewPageIndex;
            BuscarPorFiltros();
        }


        private void BuscarPorFiltros()
        {
            objcargo.CAR_Codigo = txtCodigo.Text.Trim();
            objcargo.CAR_Nombre = txtnombre.Text.Trim();
            objcargo.IDArea = ddlarea.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlarea.SelectedValue);
            objcargo.CAR_Estado = Constantes.EstadoActivo;
            var entidades = CargosBL.Instancia.ListarCargos(objcargo);
            if (entidades.Count != 0)
            {
                lblmensaje.Text = "";
                var datos = (from x in entidades
                             select new
                             {
                                 x.IDCargo,
                                 CAR_Codigo = x.CAR_Codigo == null ? "" : x.CAR_Codigo,
                                 CAR_Nombre=x.CAR_Nombre == null ? "":x.CAR_Nombre,
                                 CAR_Descripcion=x.CAR_Descripcion==null? "":x.CAR_Descripcion,
                                 ARE_Nombre= x.Area == null ? "" : x.Area.ARE_Nombre
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
                    Response.Redirect(string.Format("cargos.aspx?IDCargo={0}&accion=M", Convert.ToInt32(e.CommandArgument), true));
                    break;
            }
        }
       

    }
}