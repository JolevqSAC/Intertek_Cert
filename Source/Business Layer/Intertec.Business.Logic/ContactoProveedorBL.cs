using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//agregamos la referencia al repositorio
using Intertek.Business.Entities;
using Intertek.DataAccess;
using Intertek.Helpers;

namespace Intertek.Business.Logic
{
    public class ContactoProveedorBL : Singleton<ContactoProveedorBL>
    {
        /// <summary>
        /// Registra los datos del ensayo
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public ContactoProveedor Insertar(ContactoProveedor objContactoProveedor)
        {
            try
            {
                return ContactoProveedorDAL.Instancia.Add(objContactoProveedor);
            }
            catch (Exception ex)
            {
                throw new Exception("Insertar Contacto de Proveedor:" + ex.Message);
            }
        }

        /// <summary>
        /// Actualiza los datos del ContactoProveedor
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public ContactoProveedor Actualizar(ContactoProveedor objContactoProveedor)
        {
            try
            {
                return ContactoProveedorDAL.Instancia.Update(objContactoProveedor);
            }
            catch (Exception ex)
            {
                throw new Exception("Actualizar ContactoProveedor:" + ex.Message);
            }
        }

        /// <summary>
        /// Eliminar los datos del ContactoProveedor
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public int Eliminar(ContactoProveedor objContactoProveedor)
        {
            try
            {
                return ContactoProveedorDAL.Instancia.Delete(objContactoProveedor);
            }
            catch (Exception ex)
            {
                throw new Exception("Eliminar ContactoProveedor:" + ex.Message);
            }
        }

        /// <summary>
        /// Obtiene todos los ContactoProveedors de un cliente
        /// </summary>
        /// <param name="idProveedor"></param>
        /// <returns></returns>
        public IList<ContactoProveedor> ObtenerContactosProveedor(Int32 idProveedor)
        {
            try
            {
                return ContactoProveedorDAL.Instancia.GetFiltered(aux => aux.IDProveedor == idProveedor && aux.COC_Estado == Constantes.EstadoActivo);
            }
            catch (Exception ex)
            {
                throw new Exception("Obtener Contactos por Proveedor: " + ex.Message);
            }
        }

        /// <summary>
        /// Obtiene los datos del usuario por ID
        /// </summary>
        /// <param name="idContactoProveedor"></param>
        /// <returns></returns>
        public ContactoProveedor ObtenerContactoProveedorById(Int32 idContactoProveedor)
        {
            try
            {
                return ContactoProveedorDAL.Instancia.Single(aux => aux.IDContactoProveedor == idContactoProveedor);
            }
            catch (Exception ex)
            {
                throw new Exception("obtener ContactoProveedor BYID: " + ex.Message);
            }
        }

        public IList<ContactoProveedor> ListarContactoProveedors(ContactoProveedor objContactoProveedor)
        {
            try
            {
                return ContactoProveedorDAL.Instancia.GetPaged(aux => aux.COC_Nombres.Contains(objContactoProveedor.COC_Nombres)
                    && aux.COC_Apellidos.Contains(objContactoProveedor.COC_Apellidos)
                    && aux.COC_Estado == Constantes.EstadoActivo, aux => aux.COC_Nombres, true);
            }
            catch (Exception ex)
            {
                throw new Exception("Listar Contactos: " + ex.Message);
            }
        }

        /// <summary>
        /// Lista ContactoProveedors activos ordenados x Razon Social.
        /// </summary>
        /// <param name="objContactoProveedor"></param>
        /// <returns>objContactoProveedor</returns>
        public IList<ContactoProveedor> ListarPorRazonSocial(ContactoProveedor objContactoProveedor)
        {
            try
            {
                return ContactoProveedorDAL.Instancia.GetPaged(aux => aux.COC_Estado == objContactoProveedor.COC_Estado, aux => aux.COC_Nombres, true);
            }
            catch (Exception ex)
            {
                throw new Exception("Contactos: Listar" + " " + ex.Message);
            }
        }

        /// <summary>
        /// Lista ContactoProveedors activos.
        /// </summary>
        /// <param name="objContactoProveedor"></param>
        /// <returns>objContactoProveedor</returns>
        public IList<ContactoProveedor> ListarTodosActivos(ContactoProveedor objContactoProveedor)
        {
            try
            {
                return ContactoProveedorDAL.Instancia.GetFiltered(aux => aux.COC_Estado == objContactoProveedor.COC_Estado);
            }
            catch (Exception ex)
            {
                throw new Exception("Contactos: Listar" + " " + ex.Message);
            }
        }
    }
}
