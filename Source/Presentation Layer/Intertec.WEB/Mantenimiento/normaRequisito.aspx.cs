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
    public partial class normaRequisito : PaginaBase
    {
        NormaRequisito objnorma = new NormaRequisito();
        string accion = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            txtNombre.Focus();
            accion = Request.QueryString.Get("accion");
            if (!Page.IsPostBack)
            {
                if (Session["ddlIdiomas"].ToString() == "en-US")
                {
                    imgGrabar.Src = Resources.generales.imgGrabarUS;
                }
                if (accion == "M")
                    CargarDatos(Request.QueryString.Get("IDNormaRequisito"));
            }
        }

        protected void btnGrabar_Click(object sender, EventArgs e)
        {
            GuardarDatos();
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("normaRequisitoBuscar.aspx");
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void CargarDatos(string idnorma)
        {
            int id = Convert.ToInt32(idnorma);
            var entidad = NormaRequisitoBL.Instancia.ObtenerDatosPorID(id);
            txtNombre.Text = entidad.NRE_Nombre;
            txtAcreditador.Text = entidad.NRE_Acreditador;
            txtDescripcion.Text = entidad.NRE_Descripcion;
            txtanio.Text = entidad.NRE_Anio.ToString();
            if (Session["ddlIdiomas"].ToString() == "en-US")
            {
                lblTitulo.Text = "Modify Standar Requirement";
            }
            else
            {
                lblTitulo.Text = "Modificar Norma Requisito";
            }
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
                    objnorma.NRE_Nombre = txtNombre.Text.Trim();
                    objnorma.NRE_Acreditador = txtAcreditador.Text.Trim();
                    objnorma.NRE_Descripcion = txtDescripcion.Text.Trim();
                    objnorma.NRE_Anio = Convert.ToInt32(txtanio.Text.Trim());
                    objnorma.NRE_Estado = Constantes.EstadoActivo;
                    objnorma.NRE_UsuarioCreacion = objusuario.IDUsuario.ToString();
                    objnorma.NRE_FechaHoraCreacion = DateTime.Now;
                    objnorma = NormaRequisitoBL.Instancia.Insertar(objnorma);
                    int idnorma = objnorma.IDNormaRequisito;
                    objnorma.NRE_Codigo = "NRE" + idnorma.ToString().PadLeft(7, '0');
                    NormaRequisitoBL.Instancia.Actualizar(objnorma);
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
            try
            {
                Usuario objusuario = (Usuario)Session[Constantes.sesionUsuario];
                int idnorma = Convert.ToInt32(Request.QueryString.Get("IDNormaRequisito"));
                var entidadlab = NormaRequisitoBL.Instancia.ObtenerDatosPorID(idnorma);
                entidadlab.NRE_Nombre = txtNombre.Text.Trim();
                entidadlab.NRE_Acreditador = txtAcreditador.Text.Trim();
                entidadlab.NRE_Descripcion = txtDescripcion.Text.Trim();
                entidadlab.NRE_Anio = Convert.ToInt32(txtanio.Text.Trim());
                entidadlab.NRE_UsuarioModificacion = objusuario.IDUsuario.ToString();
                entidadlab.NRE_FechaHoraModificacion = DateTime.Now;
                NormaRequisitoBL.Instancia.Actualizar(entidadlab);
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
           
            txtNombre.Text = "";
            txtAcreditador.Text = "";
            txtanio.Text = "";
            txtDescripcion.Text = "";
            
        }

    }
}