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
    public class NormaProductoBL:Singleton<NormaProductoBL>
    {
        /// <summary>
        /// Registra los datos del NormaProducto
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public NormaProducto Insertar(NormaProducto objNormaProducto)
        {
            try
            {
                return NormaProductoDAL.Instancia.Add(objNormaProducto);
            }
            catch (Exception ex)
            {
                throw new Exception("Insertar NormaProducto:" + ex.Message);
            }
        }

        /// <summary>
        /// Actualiza los datos del NormaProducto
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public NormaProducto Actualizar(NormaProducto objNormaProducto, IEnumerable<NormaLimite> normaLimites)
        {
            try
            {
                NormaProductoDAL.Instancia.Update(objNormaProducto);

                NormaLimiteBL.Instancia.EliminarFisicoByIdNorma(objNormaProducto.IDNorma);

                foreach (var normaLimite in normaLimites)
                {
                    normaLimite.IDNorma = objNormaProducto.IDNorma;
                    NormaLimiteBL.Instancia.Insertar(normaLimite);
                }

                return objNormaProducto;
            }
            catch (Exception ex)
            {
                throw new Exception("Actualizar NormaProducto:" + ex.Message);
            }
        }

        /// <summary>
        /// Eliminar los datos de NormaProducto
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public NormaProducto Eliminar(NormaProducto objNormaProducto)
        {
            try
            {
                return NormaProductoDAL.Instancia.Update(objNormaProducto);
            }
            catch (Exception ex)
            {
                throw new Exception("Eliminar NormaProducto:" + ex.Message);
            }
        }

        /// <summary>
        /// Obtiene los datos del NormaProducto por ID
        /// </summary>
        /// <param name="idNormaProducto"></param>
        /// <returns></returns>
        public NormaProducto ObtenerNormaProductoById(Int32 idNormaProducto)
        {
            try
            {
                return NormaProductoDAL.Instancia.Single(aux => aux.IDNorma == idNormaProducto, "NormaLimite");

            }
            catch (Exception ex)
            {
                throw new Exception("obtener NormaProducto BYID: " + ex.Message);
            }
        }

        public IList<NormaProducto> ListarNormaProductos(NormaProducto objNormaProducto)
        {
            try
            {

                return NormaProductoDAL.Instancia.GetPaged(aux => aux.NOR_Nombre.Contains(objNormaProducto.NOR_Nombre) && aux.NOR_Estado == Constantes.EstadoActivo,
                   aux => aux.NOR_Nombre, true, "Producto");

            }
            catch (Exception ex)
            {
                throw new Exception("Listar NormaProducto: " + ex.Message);
            }
        }
    }
}
