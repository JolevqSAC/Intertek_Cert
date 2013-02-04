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
    public partial class normaMuestreo : PaginaBase
    {
        NormaMuestreo objnorma = new NormaMuestreo();
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
                    CargarDatos(Request.QueryString.Get("IDNormaMuestreo"));
            }
        }

        protected void btnGrabar_Click(object sender, EventArgs e)
        {
            GuardarDatos();
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("normaMuestreoBuscar.aspx");
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void CargarDatos(string idnorma)
        {
            int id = Convert.ToInt32(idnorma);
            var entidad = NormaMuestreoBL.Instancia.ObtenerDatosPorID(id);
            txtNombre.Text = entidad.NOM_Nombre;
            txtAcreditador.Text = entidad.NOM_Acreditador;
            txtDescripcion.Text = entidad.NOM_Descripcion;
            txtanio.Text = entidad.NOM_Anio.ToString();
            if (Session["ddlIdiomas"].ToString() == "en-US")
            {
                lblTitulo.Text = "Modify Standar Sample";
            }
            else
            {
                lblTitulo.Text = "Modificar Norma Muestreo";
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
                    objnorma.NOM_Nombre = txtNombre.Text.Trim();
                    objnorma.NOM_Acreditador = txtAcreditador.Text.Trim();
                    objnorma.NOM_Descripcion = txtDescripcion.Text.Trim();
                    objnorma.NOM_Anio =Convert.ToInt32(txtanio.Text.Trim());
                    objnorma.NOM_Estado = Constantes.EstadoActivo;
                    objnorma.NOM_UsuarioCreacion = objusuario.IDUsuario.ToString();
                    objnorma.NOM_FechaHoraCreacion = DateTime.Now;
                    objnorma = NormaMuestreoBL.Instancia.Insertar(objnorma);
                    int idnorma = objnorma.IDNormaMuestreo;
                    objnorma.NOM_Codigo = "NOM" + idnorma.ToString().PadLeft(7, '0');
                    NormaMuestreoBL.Instancia.Actualizar(objnorma);
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
                int idnorma = Convert.ToInt32(Request.QueryString.Get("IDNormaMuestreo"));
                var entidadlab = NormaMuestreoBL.Instancia.ObtenerDatosPorID(idnorma);
                entidadlab.NOM_Nombre = txtNombre.Text.Trim();
                entidadlab.NOM_Acreditador = txtAcreditador.Text.Trim();
                entidadlab.NOM_Descripcion = txtDescripcion.Text.Trim();
                entidadlab.NOM_Anio = Convert.ToInt32(txtanio.Text.Trim());
                entidadlab.NOM_UsuarioModificacion = objusuario.IDUsuario.ToString();
                entidadlab.NOM_FechaHoraModificacion = DateTime.Now;
                NormaMuestreoBL.Instancia.Actualizar(entidadlab);
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