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
    public partial class lugarMuestreoBuscar : PaginaBase
    {
        LugarMuestreo objlugarmuestro = new LugarMuestreo();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LlenarGridview();
                CargarCliente();
            }
        }

        protected void btnbuscar_Click(object sender, EventArgs e)
        {
            BuscarPorFiltros();
        }

        protected void btnlimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
            LlenarGridview();
        }

        protected void btnnuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("lugarMuestreo.aspx?accion=N");
        }


        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            EliminarTipoLaboratorio(Convert.ToInt32(hdEliminarID.Value));
        }

        protected void dgvlugarmuestreo_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.dgvlugarmuestreo.EditIndex = -1;
            this.dgvlugarmuestreo.PageIndex = e.NewPageIndex;
            LlenarGridview();
        }

        protected void dgvlugarmuestreo_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Editar":
                    Response.Redirect(string.Format("lugarMuestreo.aspx?IDLugarMuestreo={0}&accion=M", Convert.ToInt32(e.CommandArgument), true));
                    break;
            }
        }


        private void LlenarGridview()
        {
            //Cliente objCliente=new Cliente();
            objlugarmuestro.LUM_Estado = Constantes.EstadoActivo;
            //objCliente.CLI_Estado=Constantes.EstadoActivo;
            var listlugarmuestreo = LugarMuestreoBL.Instancia.ListarTodosActivos(objlugarmuestro);
           // var listclientes= ClienteBL.Instancia.ListarTodosActivos(objCliente);
            var datos = (from x in listlugarmuestreo
                         //from y in listclientes
                         //where x.IDCliente == y.IDCliente
                         select new { x.IDLugarMuestreo,
                                      CLI_RazonSocial= x.Cliente==null? "": x.Cliente.CLI_RazonSocial,
                                      x.LUM_Direccion,
                                      x.LUM_Telefono,
                                      x.LUM_Contacto
                                    }).ToList();
            this.dgvlugarmuestreo.DataSource = datos;
            this.dgvlugarmuestreo.DataBind();
        }

        private void CargarCliente()
        {
            Cliente objcliente = new Cliente();
            objcliente.CLI_Estado = Constantes.EstadoActivo;
            ddlCliente.DataSource = ClienteBL.Instancia.ListarPorRazonSocial(objcliente);
            ddlCliente.DataValueField = "IDCliente";
            ddlCliente.DataTextField = "CLI_RazonSocial";
            ddlCliente.DataBind();
            ddlCliente.Items.Insert(0, new ListItem(Resources.generales.textoTodos, "0"));
        }

        private void BuscarPorFiltros()
        {
            objlugarmuestro.IDCliente = ddlCliente.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlCliente.SelectedValue);
            objlugarmuestro.LUM_Contacto = txtConctacto.Text.Trim();
            objlugarmuestro.LUM_Direccion = txtDireccion.Text.Trim();
            objlugarmuestro.LUM_Telefono = txtTelefono.Text.Trim();            
            objlugarmuestro.LUM_Estado = Constantes.EstadoActivo;
            var entidades = LugarMuestreoBL.Instancia.BuscarPorFiltros(objlugarmuestro);
            if (entidades.Count != 0)
            {
                lblmensaje.Text = "";
                var datos = (from x in entidades
                             select new {
                                            x.IDLugarMuestreo,
                                            CLI_RazonSocial = x.Cliente == null ? "" : x.Cliente.CLI_RazonSocial,
                                            x.LUM_Direccion,
                                            x.LUM_Telefono,
                                            x.LUM_Contacto
                                        }).ToList();
                dgvlugarmuestreo.DataSource = datos;
                dgvlugarmuestreo.DataBind();
            }
            else
            {
                dgvlugarmuestreo.DataSource = null;
                dgvlugarmuestreo.DataBind();
                lblmensaje.Text = "No Existen Datos Encontrados";

            }
            dgvlugarmuestreo.PageIndex = 0;
            
        }

        private void LimpiarCampos()
        {
            this.txtConctacto.Text = "";
            this.txtDireccion.Text = "";
            this.txtTelefono.Text = "";
            this.ddlCliente.SelectedIndex = -1;
            this.lblmensaje.Text = "";
        }

        private void EliminarTipoLaboratorio(int idlugarmuestreo)
        {
            Usuario objusuario = (Usuario)Session[Constantes.sesionUsuario];
            objlugarmuestro.IDLugarMuestreo = idlugarmuestreo;
            var entidad = LugarMuestreoBL.Instancia.ObtenerDatosPorID(objlugarmuestro);
            entidad.LUM_Estado = Constantes.EstadoEliminado;
            entidad.LUM_UsuarioModificacion = objusuario.IDUsuario.ToString();
            entidad.LUM_FechaHoraModificacion = DateTime.Now;
            try
            {

                LugarMuestreoBL.Instancia.Actualizar(entidad);
                ClientScript.RegisterStartupScript(this.GetType(), "Confirmacion", "$(function(){MostrarMensaje('msjEliminacionOK');});", true);
            }
            catch
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Error", "$(function(){MostrarMensaje('msjErrorEliminar');});", true);
            }
            LlenarGridview();
        }
    }
}