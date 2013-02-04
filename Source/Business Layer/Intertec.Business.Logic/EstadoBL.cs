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
    public class EstadoBL: Singleton<EstadoBL>
    {
        private System.Linq.Expressions.Expression<Func<Estado, bool>> where;
        /// <summary>
        /// Inserta un nuevo estado
        /// </summary>
        /// <param name="objEstado"></param>
        /// <returns>objEstado</returns>
        public Estado Insertar(Estado objEstado)
        {
            try
            {
                return EstadoDAL.Instancia.Add(objEstado);
            }
            catch (Exception ex)
            {
                throw new Exception("Estado: Insertar" + ex.Message);
            }
        }
        /// <summary>
        /// Actualiza un estado existente
        /// </summary>
        /// <param name="objEstado"></param>
        /// <returns>objEstado</returns>

        public Estado Actualizar(Estado objEstado)
        {
            try
            {
                return EstadoDAL.Instancia.Update(objEstado);
            }
            catch (Exception ex)
            {
                throw new Exception("Estado: Actualizar" + ex.Message);
            }
        }

        /// <summary>
        /// Lista los Estado activos.
        /// </summary>
        /// <param name="objEstado"></param>
        /// <returns>objEstado</returns>
        public IList<Estado> ListarTodosActivos(Estado objEstado)
        {
            try
            {
                //return EstadoDAL.Instancia.GetFiltered(aux => aux.EST_Estado == objEstado.EST_Estado);
                return EstadoDAL.Instancia.GetPaged(aux => aux.EST_Estado == objEstado.EST_Estado, aux => aux.IDEstado, false);
            }
            catch (Exception ex)
            {
                throw new Exception("Estado: Listar" + " " + ex.Message);
            }
        }

        /// <summary>
        /// Obtener Estado por IDEstado.
        /// </summary>
        /// <param name="objEstado"></param>
        /// <returns>objEstado</returns>
        public Estado ObtenerDatosPorID(Estado objEstado)
        {
            try
            {
                return EstadoDAL.Instancia.Single(aux => aux.IDEstado == objEstado.IDEstado);
            }
            catch (Exception ex)
            {
                throw new Exception("Estado: Obtener Datos" + " " + ex.Message);
            }
        }

       
        /// <summary>
        /// Busca Estados Activos en base filtros
        /// </summary>
        /// <param name="objEstado"></param>
        /// <returns>objEstado</returns>
        public IList<Estado> BuscarPorFiltros(Estado objEstado)
        {
            try
            {
                return EstadoDAL.Instancia.GetPaged(aux => aux.EST_Descripcion.Contains(objEstado.EST_Descripcion)
                    && aux.EST_Tipo.Contains(objEstado.EST_Tipo) && aux.EST_Estado == objEstado.EST_Estado,
                    aux => aux.IDEstado, false);

            }
            catch (Exception ex)
            {
                throw new Exception("Estado: Buscar Por Filtros" + " " + ex.Message);
            }
        }

        /// <summary>
        /// Busca Estado Activos por descripción
        /// </summary>
        /// <param name="objEstado"></param>
        /// <returns>objEstado</returns>
        public IList<Estado> BuscarPorDescripcion(Estado objEstado)
        {
            try
            {
                return EstadoDAL.Instancia.GetFiltered(aux => aux.EST_Descripcion == objEstado.EST_Descripcion
                    && aux.EST_Estado == objEstado.EST_Estado);

            }
            catch (Exception ex)
            {
                throw new Exception("Estado: Buscar Por Descripción" + " " + ex.Message);
            }
        }
    }
}
