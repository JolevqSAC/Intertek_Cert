using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//referencia al helper para usar el utilitario
using Intertek.Helpers;
using Intertek.Business.Entities;
using Intertek.DataAccess;

namespace Intertek.Business.Logic
{
    public class CentroCostoBL:Singleton<CentroCostoBL>
    {

        private System.Linq.Expressions.Expression<Func<CentroCosto, bool>> where;
        /// <summary>
        /// Inserta un nuevo centro de costo
        /// </summary>
        /// <param name="objCCosto"></param>
        /// <returns>objCCosto</returns>
        public CentroCosto Insertar(CentroCosto objCCosto)
        {
            try
            {
                return CentroCostoDAL.Instancia.Add(objCCosto);
            }
            catch (Exception ex)
            {
                throw new Exception("Centro Costo: Insertar" + ex.Message);
            }
        }
        /// <summary>
        /// Actualiza un centro de costo existente
        /// </summary>
        /// <param name="objCCosto"></param>
        /// <returns>objCCosto</returns>

        public CentroCosto Actualizar(CentroCosto objCCosto)
        {
            try
            {
                return CentroCostoDAL.Instancia.Update(objCCosto);
            }
            catch (Exception ex)
            {
                throw new Exception("Centro Costo: Actualizar" + ex.Message);
            }
        }

        /// <summary>
        /// Lista los centros de costos activos.
        /// </summary>
        /// <param name="objCCosto"></param>
        /// <returns>objCCosto</returns>
        public IList<CentroCosto> ListarTodosActivos(CentroCosto objCCosto)
        {
            try
            {
               // return CentroCostoDAL.Instancia.GetFiltered(aux=>aux.CCO_Estado==objCCosto.CCO_Estado);
                return CentroCostoDAL.Instancia.GetPaged(aux => 
                    aux.CCO_Estado == objCCosto.CCO_Estado, aux => aux.CCO_Codigo, true);
            }
            catch (Exception ex)
            {
                throw new Exception("Centro Costo: Listar" + " " + ex.Message);
            }
        }

        /// <summary>
        /// Obtener Centro de Costo por IDCentroCosto.
        /// </summary>
        /// <param name="objCCosto"></param>
        /// <returns>objCCosto</returns>
        public CentroCosto ObtenerDatosPorID(int id)
        {
            try
            {
                return CentroCostoDAL.Instancia.Single(aux => aux.IDCentroCosto == id 
                    && aux.CCO_Estado==Constantes.EstadoActivo);
            }
            catch (Exception ex)
            {
                throw new Exception("Centro Costo: Obtener Datos" + " " + ex.Message);
            }
        }

        public int Eliminar(CentroCosto objCCosto)
        {
            try
            {
                return CentroCostoDAL.Instancia.Delete(objCCosto);
            }
            catch (Exception ex)
            {
                throw new Exception("Centro Costo: Eliminar" + " " + ex.Message);
            }
        }

        /// <summary>
        /// Busca Centro Costos Activos en base filtros
        /// </summary>
        /// <param name="objCCosto"></param>
        /// <returns>objCCosto</returns>
        public IList<CentroCosto> BuscarPorFiltros(CentroCosto objCCosto)
        {
            try
            {
                return CentroCostoDAL.Instancia.GetPaged(aux => 
                    aux.CCO_Codigo.Contains(objCCosto.CCO_Codigo)
                    && aux.CCO_Numero.Contains(objCCosto.CCO_Numero) 
                    && aux.CCO_Estado==Constantes.EstadoActivo 
                    && (objCCosto.IDArea == null || aux.IDArea == objCCosto.IDArea),
                    aux=>aux.CCO_Codigo,true,"Area");

            }
            catch (Exception ex)
            {
                throw new Exception("Centro Costo: Buscar Filtros" + " " + ex.Message);
            }
        }

        /// <summary>
        /// Busca Centro Costos Activos en base filtros
        /// </summary>
        /// <param name="objCCosto"></param>
        /// <returns>objCCosto</returns>
        public IList<CentroCosto> BuscarPorNombre(CentroCosto objCCosto)
        {
            try
            {
                return CentroCostoDAL.Instancia.GetFiltered(aux => aux.CCO_Nombre==objCCosto.CCO_Nombre
                    && aux.CCO_Estado == Constantes.EstadoActivo);

            }
            catch (Exception ex)
            {
                throw new Exception("Centro Costo: Buscar Por Nombre" + " " + ex.Message);
            }
        }
    }
}
