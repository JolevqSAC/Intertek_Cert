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
    public class ClienteBL:Singleton<ClienteBL>
    {
        /// <summary>
        /// Registra los datos del ensayo
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public Cliente Insertar(Cliente objCliente)
        {
            try
            {
                return ClienteDAL.Instancia.Add(objCliente);
            }
            catch (Exception ex)
            {
                throw new Exception("Insertar Cliente:" + ex.Message);
            }
        }

        /// <summary>
        /// Actualiza los datos del Cliente
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public Cliente Actualizar(Cliente objCliente)
        {
            try
            {
                return ClienteDAL.Instancia.Update(objCliente);
            }
            catch (Exception ex)
            {
                throw new Exception("Actualizar Cliente:" + ex.Message);
            }
        }
        /// <summary>
        /// Eliminar los datos del Cliente
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public Cliente Eliminar(Cliente objCliente)
        {
            try
            {
                return ClienteDAL.Instancia.Update(objCliente);
            }
            catch (Exception ex)
            {
                throw new Exception("Eliminar Cliente:" + ex.Message);
            }
        }

        /// <summary>
        /// Obtiene los datos del usuario por ID
        /// </summary>
        /// <param name="idCliente"></param>
        /// <returns></returns>
        public Cliente ObtenerClienteById(Int32 idCliente)
        {
            try
            {
                return ClienteDAL.Instancia.Single(aux => aux.IDCliente == idCliente);

            }
            catch (Exception ex)
            {
                throw new Exception("obtener Cliente BYID: " + ex.Message);
            }
        }

        public IList<Cliente> ListarClientes(Cliente objCliente)
        {
            try
            {
                //return ClienteDAL.Instancia.GetPaged(aux => aux.CLI_RazonSocial.Contains(objCliente.CLI_RazonSocial) 
                //    && aux.CLI_RUC.Contains(objCliente.CLI_RUC)
                //    && aux.CLI_Direccion.Contains(objCliente.CLI_Direccion) 
                //    && aux.CLI_Estado == Constantes.EstadoActivo,
                //    aux => aux.CLI_RazonSocial, true);
                return ClienteDAL.Instancia.GetPaged(aux => aux.CLI_Codigo.Contains(objCliente.CLI_Codigo)
                    && aux.CLI_RazonSocial.Contains(objCliente.CLI_RazonSocial)
                    && aux.CLI_RUC.Contains(objCliente.CLI_RUC)
                    && (objCliente.CLI_IndicadorArea == "0" || aux.CLI_IndicadorArea == objCliente.CLI_IndicadorArea)
                    && aux.CLI_Estado == Constantes.EstadoActivo,
                    aux => aux.CLI_Codigo, true);
            }
            catch (Exception ex)
            {
                throw new Exception("Listar Clientes: " + ex.Message);
            }
        }

        /// <summary>
        /// Lista Clientes activos ordenados x Razon Social.
        /// </summary>
        /// <param name="objCliente"></param>
        /// <returns>objCliente</returns>
        public IList<Cliente> ListarPorRazonSocial(Cliente objCliente)
        {
            try
            {
                return ClienteDAL.Instancia.GetPaged(aux => aux.CLI_Estado == Constantes.EstadoActivo,
                   aux => aux.CLI_RazonSocial, true);
            }
            catch (Exception ex)
            {
                throw new Exception("Cliente: Listar" + " " + ex.Message);
            }
        }

        /// <summary>
        /// Lista Clientes activos.
        /// </summary>
        /// <param name="objCliente"></param>
        /// <returns>objCliente</returns>
        public IList<Cliente> ListarTodosActivos(Cliente objCliente)
        {
            try
            {
                return ClienteDAL.Instancia.GetFiltered(aux => aux.CLI_Estado == objCliente.CLI_Estado);
            }
            catch (Exception ex)
            {
                throw new Exception("Cliente: Listar" + " " + ex.Message);
            }
        }
    }
}
