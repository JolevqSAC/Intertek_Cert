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
    public class ProveedorBL:Singleton<ProveedorBL>
    {
        /// <summary>
        /// Registra los datos del Proveedor
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public Proveedor Insertar(Proveedor objProveedor)
        {
            try
            {
                return ProveedorDAL.Instancia.Add(objProveedor);
            }
            catch (Exception ex)
            {
                throw new Exception("Insertar Proveedor:" + ex.Message);
            }
        }

        /// <summary>
        /// Actualiza los datos del Proveedor
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public Proveedor Actualizar(Proveedor objProveedor)
        {
            try
            {
                return ProveedorDAL.Instancia.Update(objProveedor);
            }
            catch (Exception ex)
            {
                throw new Exception("Actualizar Proveedor:" + ex.Message);
            }
        }
        /// <summary>
        /// Eliminar los datos del Proveedor
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public Proveedor Eliminar(Proveedor objProveedor)
        {
            try
            {
                return ProveedorDAL.Instancia.Update(objProveedor);
            }
            catch (Exception ex)
            {
                throw new Exception("Eliminar Proveedor:" + ex.Message);
            }
        }

        /// <summary>
        /// Obtiene los datos del usuario por ID
        /// </summary>
        /// <param name="idProveedor"></param>
        /// <returns></returns>
        public Proveedor ObtenerProveedorById(Int32 idProveedor)
        {
            try
            {
                return ProveedorDAL.Instancia.Single(aux => aux.IDProveedor == idProveedor);

            }
            catch (Exception ex)
            {
                throw new Exception("obtener Proveedor BYID: " + ex.Message);
            }
        }

        public IList<Proveedor> ListarProveedores(Proveedor objProveedor)
        {
            try
            {
                return ProveedorDAL.Instancia.GetPaged(aux => aux.PRV_Codigo.Contains(objProveedor.PRV_Codigo) 
                    && aux.PRV_RazonSocial.Contains(objProveedor.PRV_RazonSocial) 
                    && aux.PRV_RUC.Contains(objProveedor.PRV_RUC)
                    && (objProveedor.PRV_IndicadorArea == "0" || aux.PRV_IndicadorArea == objProveedor.PRV_IndicadorArea)
                    && aux.PRV_Estado == Constantes.EstadoActivo,
                   aux => aux.PRV_Codigo, true);
            }
            catch (Exception ex)
            {
                throw new Exception("Listar Proveedores: " + ex.Message);
            }
        }
    }
}
