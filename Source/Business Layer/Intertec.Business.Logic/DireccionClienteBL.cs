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
    public class DireccionClienteBL : Singleton<DireccionClienteBL>
    {
        /// <summary>
        /// Registra los datos del ensayo
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public DireccionCliente Insertar(DireccionCliente objDireccionCliente)
        {
            try
            {
                return DireccionClienteDAL.Instancia.Add(objDireccionCliente);
            }
            catch (Exception ex)
            {
                throw new Exception("Insertar Direccion:" + ex.Message);
            }
        }

        /// <summary>
        /// Actualiza los datos del Direccion
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public DireccionCliente Actualizar(DireccionCliente objDireccionCliente)
        {
            try
            {
                return DireccionClienteDAL.Instancia.Update(objDireccionCliente);
            }
            catch (Exception ex)
            {
                throw new Exception("Actualizar Direccion:" + ex.Message);
            }
        }

        /// <summary>
        /// Eliminar los datos del Direccion
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public int Eliminar(DireccionCliente objDireccionCliente)
        {
            try
            {
                return DireccionClienteDAL.Instancia.Delete(objDireccionCliente);
            }
            catch (Exception ex)
            {
                throw new Exception("Eliminar Direccion:" + ex.Message);
            }
        }

        /// <summary>
        /// Obtiene todos los Direccions de un cliente
        /// </summary>
        /// <param name="idCliente"></param>
        /// <returns></returns>
        public IList<DireccionCliente> ObtenerDireccionesCliente(Int32 idCliente)
        {
            try
            {
                //return DireccionDAL.Instancia.Single(aux => aux.IDDireccion == idCliente);
                return DireccionClienteDAL.Instancia.GetFiltered(aux => aux.IDCliente == idCliente);
            }
            catch (Exception ex)
            {
                throw new Exception("Obtener Direccions por Cliente: " + ex.Message);
            }
        }

        /// <summary>
        /// Obtiene los datos del usuario por ID
        /// </summary>
        /// <param name="idDireccion"></param>
        /// <returns></returns>
        public DireccionCliente ObtenerDireccionClienteById(Int32 idDireccion)
        {
            try
            {
                return DireccionClienteDAL.Instancia.Single(aux => aux.IDDireccionCliente == idDireccion);
            }
            catch (Exception ex)
            {
                throw new Exception("obtener Direccion BYID: " + ex.Message);
            }
        }

        public IList<DireccionCliente> ListarDireccionesCliente(DireccionCliente objDireccionCliente)
        {
            try
            {
                return DireccionClienteDAL.Instancia.GetPaged(aux => aux.DIC_Tipo.Contains(objDireccionCliente.DIC_Tipo)
                    && aux.DIC_Descripcion.Contains(objDireccionCliente.DIC_Descripcion)
                    && aux.DIC_Estado == Constantes.EstadoActivo, aux => aux.DIC_Tipo, true);
            }
            catch (Exception ex)
            {
                throw new Exception("Listar Direccions: " + ex.Message);
            }
        }

        /// <summary>
        /// Lista Direccions activos ordenados x Razon Social.
        /// </summary>
        /// <param name="objDireccionCliente"></param>
        /// <returns>objDireccionCliente</returns>
        public IList<DireccionCliente> ListarPorRazonSocial(DireccionCliente objDireccionCliente)
        {
            try
            {
                return DireccionClienteDAL.Instancia.GetPaged(aux => aux.DIC_Estado == objDireccionCliente.DIC_Estado, aux => aux.DIC_Tipo, true);
            }
            catch (Exception ex)
            {
                throw new Exception("Direccion: Listar" + " " + ex.Message);
            }
        }

        /// <summary>
        /// Lista Direccions activos.
        /// </summary>
        /// <param name="objDireccionCliente"></param>
        /// <returns>objDireccionCliente</returns>
        public IList<DireccionCliente> ListarTodosActivos(DireccionCliente objDireccionCliente)
        {
            try
            {
                return DireccionClienteDAL.Instancia.GetFiltered(aux => aux.DIC_Estado == objDireccionCliente.DIC_Estado);
            }
            catch (Exception ex)
            {
                throw new Exception("Direccion: Listar" + " " + ex.Message);
            }
        }
    }
}
