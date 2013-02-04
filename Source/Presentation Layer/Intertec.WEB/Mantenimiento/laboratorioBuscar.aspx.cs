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
    public partial class laboratorioBuscar : PaginaBase
    {
        Laboratorio objlaboratorio = new Laboratorio();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {               
                CargarTipoLaboratorio();
                BuscarPorFiltros();
                if (Session["ddlIdiomas"].ToString() == "en-US")
                {
                    imgeliminar.Src = Resources.generales.imgEliminarUS;
                }  
            }
        }

        protected void btnbuscar_Click(object sender, EventArgs e)
        {
          BuscarPorFiltros();
        }

        protected void btnlimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        protected void btnnuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("laboratorio.aspx?accion=N");
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            EliminarLaboratorio();
        }

        private void LlenarGridview()
        {            
            objlaboratorio.LAB_Estado = Constantes.EstadoActivo;
            var listlaboratorio = LaboratorioBL.Instancia.ListarTodosActivos(objlaboratorio);
            var datos = (from x in listlaboratorio
                         select new { x.IDLaboratorio,                                      
                                      x.LAB_Nombre,
                                      TLA_Nombre = x.TipoLaboratorio==null? "": x.TipoLaboratorio.TLA_Nombre
                                    }).ToList();
            dgvlaboratorio.DataSource = datos;
            dgvlaboratorio.DataBind();
        }

        protected void dgvlaboratorio_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvlaboratorio.EditIndex = -1;
            dgvlaboratorio.PageIndex = e.NewPageIndex;
            BuscarPorFiltros();
        }

        protected void dgvlaboratorio_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Editar":
                    Response.Redirect(string.Format("laboratorio.aspx?IDLaboratorio={0}&accion=M", Convert.ToInt32(e.CommandArgument), true));
                    break;
            }
        }

        private void CargarTipoLaboratorio()
        {
            TipoLaboratorio objtipolaboratorio = new TipoLaboratorio();
            objtipolaboratorio.TLA_Estado = Constantes.EstadoActivo;
            ddlTipoLaboratorio.DataSource = TipoLaboratorioBL.Instancia.ListarPorNombre(objtipolaboratorio);
            ddlTipoLaboratorio.DataValueField = "IDTipoLaboratorio";
            ddlTipoLaboratorio.DataTextField = "TLA_Nombre";
            ddlTipoLaboratorio.DataBind();
            if (Session["ddlIdiomas"].ToString() == "en-US")
            {
                ddlTipoLaboratorio.Items.Insert(0, new ListItem(Resources.generales.testoAll, "0"));
            }
            else
            {
                ddlTipoLaboratorio.Items.Insert(0, new ListItem(Resources.generales.textoTodos, "0"));
            }
        }

        private void BuscarPorFiltros()
        {
            objlaboratorio.LAB_Nombre = txtNombre.Text.Trim();
            objlaboratorio.IDTipoLaboratorio = ddlTipoLaboratorio.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlTipoLaboratorio.SelectedValue); 
            objlaboratorio.LAB_Estado = Constantes.EstadoActivo;
            var entidades = LaboratorioBL.Instancia.BuscarPorFiltros(objlaboratorio);
            if (entidades.Count != 0)
            {
                lblmensaje.Text = "";
                var datos = (from x in entidades
                             select new
                             {
                                 x.IDLaboratorio,
                                 x.LAB_Nombre,
                                 TLA_Nombre = x.TipoLaboratorio==null? "": x.TipoLaboratorio.TLA_Nombre
                             }).ToList();
                dgvlaboratorio.DataSource = datos;
                dgvlaboratorio.DataBind();
            }
            else
            {
                dgvlaboratorio.DataSource = null;
                dgvlaboratorio.DataBind();
                if ((Session["ddlIdiomas"].ToString() == "en-US"))
                {
                    lblmensaje.Text = "No Data Found";
                }
                else
                {
                    lblmensaje.Text = "No Existen Datos Encontrados";
                }

            }
            dgvlaboratorio.PageIndex = 0;
        }

        private void LimpiarCampos()
        {
            this.txtNombre.Text = "";
            this.lblmensaje.Text = "";
            this.ddlTipoLaboratorio.SelectedIndex = -1;
            BuscarPorFiltros();
        }

        private void EliminarLaboratorio()
        {

            var listaEliminados = new List<int>();

            foreach (GridViewRow row in dgvlaboratorio.Rows)
            {
                var chkBox = (CheckBox)row.Cells[3].FindControl("chkSeleccion");

                if (chkBox != null)
                {
                    if (chkBox.Checked)
                    {
                        var hidIdLaboratorio = (HiddenField)row.Cells[3].FindControl("hidIdLaboratorio");
                        var idCentro = Convert.ToInt32(hidIdLaboratorio.Value);
                        listaEliminados.Add(idCentro);
                    }
                }
            }

            Usuario objusuario = (Usuario)Session[Constantes.sesionUsuario];

            var listaLab = new List<Laboratorio>();

            foreach (var eliminado in listaEliminados)
            {
                var entidad = LaboratorioBL.Instancia.ObtenerDatosPorID(eliminado);
                entidad.LAB_Estado = Constantes.EstadoEliminado;
                entidad.LAB_UsuarioModificacion = objusuario.IDUsuario.ToString();
                entidad.LAB_FechaHoraModificacion = DateTime.Now;

                listaLab.Add(entidad);
            }

            try
            {
                foreach (var centro in listaLab)
                {
                    LaboratorioBL.Instancia.Actualizar(centro);
                }

                ClientScript.RegisterStartupScript(this.GetType(), "Confirmacion", "$(function(){MostrarMensaje('msjEliminacionOK');});", true);
            }
            catch
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Error", "$(function(){MostrarMensaje('msjErrorEliminar');});", true);
            }
            BuscarPorFiltros();
            
        }
    }
}