using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//referencia a la capa logica
using Intertek.Business.Entities;
using Intertek.Business.Logic;
using System.Data.Objects.DataClasses;
using Intertek.Helpers;

namespace Intertek.WEB.Seguridad
{
    public partial class opcionesRol : PaginaBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (!EsNuevoRegistro())
                {
                    ObtenerDatos(Convert.ToInt32(Request["idRol"].ToString()));
                }
                else 
                {
                    IEnumerable<RolOpcionSistema> objRolOpcionSistema = null;
                    CargarOpciones(objRolOpcionSistema);
                }
            }
        }

        protected void btnGrabar_Click(object sender, EventArgs e)
        {
            if (txtRol.Text != "")
            {
                GrabarOpciones();
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("opcionRolBuscar.aspx");
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            ContentPlaceHolder mpContentPlaceHolder;
            mpContentPlaceHolder = (ContentPlaceHolder)Master.FindControl("MainContent");
            if (mpContentPlaceHolder != null)
            {
                foreach (var obj in mpContentPlaceHolder.Controls.OfType<TextBox>())
                {
                    obj.Text = "";
                }
                foreach (var obj in mpContentPlaceHolder.Controls.OfType<TreeView>())
                {
                    foreach( TreeNode node in obj.Nodes)
                    {
                        var subNodes = node.ChildNodes;


                        foreach (TreeNode subNode in subNodes)
                        {
                            if (subNode.Checked)
                            {
                                subNode.Checked = false;
                            }
                        }

                    }
                    obj.DataBind();
                    break;
                }
            }
        }

        private void ObtenerDatos(int idRol)
        { 
        
             Rol objRol=new Rol();
            objRol.IDRol = idRol;
            IList<Rol> lstRol = RolBL.Instancia.ObtenerRolConOpcionesByID(objRol);

            if (lstRol.Count > 0)
            {
                txtRol.Text = lstRol[0].ROL_Nombre;
                txtDescripcion.Text = lstRol[0].ROL_Descripcion;
                var objRolOpcionSistema = lstRol[0].RolOpcionSistema;
                CargarOpciones(objRolOpcionSistema);
            }
            if (Session["ddlIdiomas"].ToString() == "en-US")
            {
                lblTitulo.Text = "Modify Role";
            }
            else
            {
                lblTitulo.Text = "Modificar Rol";
            }

        }

        private void CargarOpciones(IEnumerable<RolOpcionSistema> objRolOpcionSistema)
        {
            List<OpcionSistema> lstOpciones = RolOpcionSistemaBL.Instancia.ListarOpciones().ToList();
            var varModulos = lstOpciones.GroupBy(modulo => new { modulo.OSI_Modulo, modulo.OSI_Modulo_en_US }).ToList();
            //List<OpcionSistema> lstModulos = new List<OpcionSistema>();
            foreach (var obj in varModulos)
            {
                OpcionSistema objModulo = new OpcionSistema();
                objModulo.OSI_Modulo = obj.Key.OSI_Modulo;
                objModulo.OSI_Modulo_en_US = obj.Key.OSI_Modulo_en_US;
                if (Session["ddlIdiomas"].ToString() == "en-US")
                {
                    TreeNode nodoPrincipal = new TreeNode(objModulo.OSI_Modulo_en_US, objModulo.OSI_Modulo_en_US);
                    nodoPrincipal.SelectAction = TreeNodeSelectAction.SelectExpand;
                    nodoPrincipal.ShowCheckBox = false;

                    List<OpcionSistema> lstSubOpciones = lstOpciones.FindAll(delegate(OpcionSistema aux) { return aux.OSI_Modulo_en_US == objModulo.OSI_Modulo_en_US; });
                    foreach (OpcionSistema objOpcion in lstSubOpciones)
                    {
                        TreeNode nodoSecundario = new TreeNode(objOpcion.OSI_Nombre_en_US, objOpcion.IDOpcionSistema.ToString());
                        nodoSecundario.SelectAction = TreeNodeSelectAction.None;
                        if (objRolOpcionSistema != null)
                        {
                            IEnumerable<RolOpcionSistema> objAux = objRolOpcionSistema.Where(aux => aux.IDOpcionSistema == objOpcion.IDOpcionSistema && aux.ROS_Estado == Constantes.EstadoActivo);
                            if (objAux != null)
                            {
                                foreach (RolOpcionSistema objSeleccionado in objAux)
                                {

                                    nodoSecundario.Checked = true;
                                }

                            }
                        }
                        nodoPrincipal.ChildNodes.Add(nodoSecundario);
                       
                    }

                    tviewOpciones.Nodes.Add(nodoPrincipal);
                }
                else 
                {
                    TreeNode nodoPrincipal = new TreeNode(objModulo.OSI_Modulo, objModulo.OSI_Modulo);
                    nodoPrincipal.SelectAction = TreeNodeSelectAction.SelectExpand;
                    nodoPrincipal.ShowCheckBox = false;

                    List<OpcionSistema> lstSubOpciones = lstOpciones.FindAll(delegate(OpcionSistema aux) { return aux.OSI_Modulo == objModulo.OSI_Modulo; });
                    foreach (OpcionSistema objOpcion in lstSubOpciones)
                    {
                        TreeNode nodoSecundario = new TreeNode(objOpcion.OSI_Nombre, objOpcion.IDOpcionSistema.ToString());
                        nodoSecundario.SelectAction = TreeNodeSelectAction.None;
                        if (objRolOpcionSistema != null)
                        {
                            IEnumerable<RolOpcionSistema> objAux = objRolOpcionSistema.Where(aux => aux.IDOpcionSistema == objOpcion.IDOpcionSistema && aux.ROS_Estado==Constantes.EstadoActivo);
                            if (objAux != null)
                            {
                                foreach (RolOpcionSistema objSeleccionado in objAux)
                                {

                                    nodoSecundario.Checked = true;
                                }
                                
                            }
                        }
                        nodoPrincipal.ChildNodes.Add(nodoSecundario);
                        //nodoSecundario.Checked = true;
                    }

                    tviewOpciones.Nodes.Add(nodoPrincipal);
                }
               // lstModulos.Add(objModulo);
            }
            //tviewOpciones.DataBind();
            tviewOpciones.CollapseAll();

        }

        private void GrabarOpciones()
        {
            bool registroSatisfactoriamente = false;
            Usuario objUsuario=(Usuario)Session[Constantes.sesionUsuario];
            
            EntityCollection<RolOpcionSistema> entRolOpciones = new EntityCollection<RolOpcionSistema>();
            //recorre los nodos de las opciones del sistema
            for (int i = 0; i < tviewOpciones.Nodes.Count; i++)
            {
                for (int x = 0; x < tviewOpciones.Nodes[i].ChildNodes.Count; x++)
                {
                    RolOpcionSistema objOpciones = new RolOpcionSistema();

                    objOpciones.ROS_UsuarioModificacion = objUsuario.IDUsuario.ToString();
                    objOpciones.ROS_UsuarioCreacion = objUsuario.IDUsuario.ToString();
                    objOpciones.IDOpcionSistema = Convert.ToInt32(tviewOpciones.Nodes[i].ChildNodes[x].Value);
                    objOpciones.ROS_FechaAsignacion = DateTime.Now;
                    objOpciones.ROS_FechaHoraModificacion = DateTime.Now;
                    if (tviewOpciones.Nodes[i].ChildNodes[x].Checked == true)
                    {
                        objOpciones.ROS_Estado = Constantes.EstadoActivo;
                    }
                    else 
                    {
                        objOpciones.ROS_Estado = Constantes.EstadoEliminado;
                    }

                    entRolOpciones.Add(objOpciones);
                }
            }

            if (!EsNuevoRegistro())
            {
                //actualizar
                int idRol = Convert.ToInt32(Request["idRol"]);

                Rol objRol = RolBL.Instancia.ObtenerRolByID(idRol);
                objRol.ROL_Nombre = txtRol.Text;
                objRol.ROL_Descripcion = txtDescripcion.Text;
                objRol.ROL_Estado = Constantes.EstadoActivo;
                objRol.ROL_UsuarioModificacion = objUsuario.IDUsuario.ToString();
                objRol.ROL_FechaHoraModificacion = DateTime.Now;

                if (objRol.RolOpcionSistema == null)
                {
                    objRol.RolOpcionSistema = new EntityCollection<RolOpcionSistema>();
                }

                try
                {
                    RolBL.Instancia.Actualizar(objRol, entRolOpciones);
                    registroSatisfactoriamente = true;
                }
                catch { registroSatisfactoriamente = false; }
            }
            else
            {
                //insertar nuevo rol
                Rol objRol = new Rol();
                objRol.ROL_Nombre = txtRol.Text;
                objRol.ROL_Descripcion = txtDescripcion.Text;
                objRol.ROL_Estado = Constantes.EstadoActivo;
                objRol.ROL_UsuarioCreacion = objUsuario.IDUsuario.ToString();
                objRol.ROL_FechaHoraCreacion = DateTime.Now;
                objRol.RolOpcionSistema = entRolOpciones;
                try
                {
                    RolBL.Instancia.Insertar(objRol);
                    int id = objRol.IDRol;
                    objRol.ROL_Codigo = "ROL" + id.ToString().PadLeft(7, '0');
                    RolBL.Instancia.Actualizar(objRol);
                    registroSatisfactoriamente = true;
                    IEnumerable<RolOpcionSistema> objRolOpcionSistema = null;
                    CargarOpciones(objRolOpcionSistema);
                    txtDescripcion.Text = "";
                    txtRol.Text = "";
                }
                catch
                {
                    registroSatisfactoriamente = false;
                }
            }

            if (registroSatisfactoriamente)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "miscriptError", "$(function(){MostrarMensaje('msjSatisfactorio');});", true);
            }
        }

        private bool EsNuevoRegistro()
        {
            bool esNuevo = true;
            if (Request["idRol"] != null)
            {
                return false;
            }
           
            return esNuevo;
        }
    }
}