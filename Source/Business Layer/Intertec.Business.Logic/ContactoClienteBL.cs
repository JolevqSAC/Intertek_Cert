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
    public class ContactoClienteBL : Singleton<ContactoClienteBL>
    {
        /// <summary>
        /// Registra los datos del ensayo
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public ContactoCliente Insertar(ContactoCliente objContactoCliente)
        {
            try
            {
                return ContactoClienteDAL.Instancia.Add(objContactoCliente);
            }
            catch (Exception ex)
            {
                throw new Exception("Insertar Contacto de Cliente:" + ex.Message);
            }
        }

        /// <summary>
        /// Actualiza los datos del ContactoCliente
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public ContactoCliente Actualizar(ContactoCliente objContactoCliente)
        {
            try
            {
                return ContactoClienteDAL.Instancia.Update(objContactoCliente);
            }
            catch (Exception ex)
            {
                throw new Exception("Actualizar ContactoCliente:" + ex.Message);
            }
        }

        /// <summary>
        /// Eliminar los datos del ContactoCliente
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public int Eliminar(ContactoCliente objContactoCliente)
        {
            try
            {
                return ContactoClienteDAL.Instancia.Delete(objContactoCliente);
            }
            catch (Exception ex)
            {
                throw new Exception("Eliminar ContactoCliente:" + ex.Message);
            }
        }

        /// <summary>
        /// Obtiene todos los ContactoClientes de un cliente
        /// </summary>
        /// <param name="idCliente"></param>
        /// <returns></returns>
        public IList<ContactoCliente> ObtenerContactosCliente(Int32 idCliente)
        {
            try
            {
                return ContactoClienteDAL.Instancia.GetFiltered(aux => aux.IDCliente == idCliente && aux.COC_Estado == Constantes.EstadoActivo);
            }
            catch (Exception ex)
            {
                throw new Exception("Obtener Contactos por Cliente: " + ex.Message);
            }
        }

        /// <summary>
        /// Obtiene los datos del usuario por ID
        /// </summary>
        /// <param name="idContactoCliente"></param>
        /// <returns></returns>
        public ContactoCliente ObtenerContactoClienteById(Int32 idContactoCliente)
        {
            try
            {
                return ContactoClienteDAL.Instancia.Single(aux => aux.IDContactoCliente == idContactoCliente);
            }
            catch (Exception ex)
            {
                throw new Exception("obtener ContactoCliente BYID: " + ex.Message);
            }
        }

        public IList<ContactoCliente> ListarContactosCliente(ContactoCliente objContactoCliente)
        {
            try
            {
                return ContactoClienteDAL.Instancia.GetPaged(aux => aux.COC_Nombres.Contains(objContactoCliente.COC_Nombres)
                    && aux.COC_Apellidos.Contains(objContactoCliente.COC_Apellidos)
                    && aux.COC_Estado == Constantes.EstadoActivo, aux => aux.COC_Nombres, true);
            }
            catch (Exception ex)
            {
                throw new Exception("Listar Contactos: " + ex.Message);
            }
        }

        /// <summary>
        /// Lista ContactoClientes activos ordenados x Razon Social.
        /// </summary>
        /// <param name="objContactoCliente"></param>
        /// <returns>objContactoCliente</returns>
        public IList<ContactoCliente> ListarPorRazonSocial(ContactoCliente objContactoCliente)
        {
            try
            {
                return ContactoClienteDAL.Instancia.GetPaged(aux => aux.COC_Estado == objContactoCliente.COC_Estado, aux => aux.COC_Nombres, true);
            }
            catch (Exception ex)
            {
                throw new Exception("Contactos: Listar" + " " + ex.Message);
            }
        }

        /// <summary>
        /// Lista ContactoClientes activos.
        /// </summary>
        /// <param name="objContactoCliente"></param>
        /// <returns>objContactoCliente</returns>
        public IList<ContactoCliente> ListarTodosActivos(ContactoCliente objContactoCliente)
        {
            try
            {
                return ContactoClienteDAL.Instancia.GetFiltered(aux => aux.COC_Estado == objContactoCliente.COC_Estado);
            }
            catch (Exception ex)
            {
                throw new Exception("Contactos: Listar" + " " + ex.Message);
            }
        }
    }
}
