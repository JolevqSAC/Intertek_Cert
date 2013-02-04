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
    public class DireccionProveedorBL : Singleton<DireccionProveedorBL>
    {
        /// <summary>
        /// Registra los datos del ensayo
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public DireccionProveedor Insertar(DireccionProveedor objDireccionProveedor)
        {
            try
            {
                return DireccionProveedorDAL.Instancia.Add(objDireccionProveedor);
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
        public DireccionProveedor Actualizar(DireccionProveedor objDireccionProveedor)
        {
            try
            {
                return DireccionProveedorDAL.Instancia.Update(objDireccionProveedor);
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
        public int Eliminar(DireccionProveedor objDireccionProveedor)
        {
            try
            {
                return DireccionProveedorDAL.Instancia.Delete(objDireccionProveedor);
            }
            catch (Exception ex)
            {
                throw new Exception("Eliminar Direccion:" + ex.Message);
            }
        }

        /// <summary>
        /// Obtiene todos los Direccions de un cliente
        /// </summary>
        /// <param name="idProveedor"></param>
        /// <returns></returns>
        public IList<DireccionProveedor> ObtenerDireccionesProveedor(Int32 idProveedor)
        {
            try
            {
                //return DireccionDAL.Instancia.Single(aux => aux.IDDireccion == idProveedor);
                return DireccionProveedorDAL.Instancia.GetFiltered(aux => aux.IDProveedor == idProveedor);
            }
            catch (Exception ex)
            {
                throw new Exception("Obtener Direccions por Proveedor: " + ex.Message);
            }
        }

        /// <summary>
        /// Obtiene los datos del usuario por ID
        /// </summary>
        /// <param name="idDireccion"></param>
        /// <returns></returns>
        public DireccionProveedor ObtenerDireccionProveedorById(Int32 idDireccion)
        {
            try
            {
                return DireccionProveedorDAL.Instancia.Single(aux => aux.IDDireccionProveedor == idDireccion);
            }
            catch (Exception ex)
            {
                throw new Exception("obtener Direccion BYID: " + ex.Message);
            }
        }

        public IList<DireccionProveedor> ListarDireccionesProveedor(DireccionProveedor objDireccionProveedor)
        {
            try
            {
                return DireccionProveedorDAL.Instancia.GetPaged(aux => aux.DIP_Tipo.Contains(objDireccionProveedor.DIP_Tipo)
                    && aux.DIP_Descripcion.Contains(objDireccionProveedor.DIP_Descripcion)
                    && aux.DIP_Estado == Constantes.EstadoActivo, aux => aux.DIP_Tipo, true);
            }
            catch (Exception ex)
            {
                throw new Exception("Listar Direccions: " + ex.Message);
            }
        }

        /// <summary>
        /// Lista Direccions activos ordenados x Razon Social.
        /// </summary>
        /// <param name="objDireccionProveedor"></param>
        /// <returns>objDireccionProveedor</returns>
        public IList<DireccionProveedor> ListarPorRazonSocial(DireccionProveedor objDireccionProveedor)
        {
            try
            {
                return DireccionProveedorDAL.Instancia.GetPaged(aux => aux.DIP_Estado == objDireccionProveedor.DIP_Estado, aux => aux.DIP_Tipo, true);
            }
            catch (Exception ex)
            {
                throw new Exception("Direccion: Listar" + " " + ex.Message);
            }
        }

        /// <summary>
        /// Lista Direccions activos.
        /// </summary>
        /// <param name="objDireccionProveedor"></param>
        /// <returns>objDireccionProveedor</returns>
        public IList<DireccionProveedor> ListarTodosActivos(DireccionProveedor objDireccionProveedor)
        {
            try
            {
                return DireccionProveedorDAL.Instancia.GetFiltered(aux => aux.DIP_Estado == objDireccionProveedor.DIP_Estado);
            }
            catch (Exception ex)
            {
                throw new Exception("Direccion: Listar" + " " + ex.Message);
            }
        }
    }
}
