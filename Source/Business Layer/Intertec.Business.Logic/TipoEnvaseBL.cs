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
    public class TipoEnvaseBL:Singleton<TipoEnvaseBL>
    {
        private System.Linq.Expressions.Expression<Func<TipoEnvase, bool>> where;
        /// <summary>
        /// Inserta un nuevo Tipo Envase
        /// </summary>
        /// <param name="objTipoEnvase"></param>
        /// <returns>objTipoEnvase</returns>
        public TipoEnvase Insertar(TipoEnvase objTipoEnvase)
        {
            try
            {
                return TipoEnvaseDAL.Instancia.Add(objTipoEnvase);
            }
            catch (Exception ex)
            {
                throw new Exception("Tipo Envase: Insertar" + ex.Message);
            }
        }
        /// <summary>
        /// Actualiza TipoEnvase
        /// </summary>
        /// <param name="objTipoEnvase"></param>
        /// <returns>objTipoEnvase</returns>

        public TipoEnvase Actualizar(TipoEnvase objTipoEnvase)
        {
            try
            {
                return TipoEnvaseDAL.Instancia.Update(objTipoEnvase);
            }
            catch (Exception ex)
            {
                throw new Exception("Tipo Envase: Actualizar" + ex.Message);
            }
        }

        /// <summary>
        /// Lista TipoEnvase activos.
        /// </summary>
        /// <param name="objTipoEnvase"></param>
        /// <returns>objTipoEnvase</returns>
        public IList<TipoEnvase> ListarTodosActivos(TipoEnvase objTipoEnvase)
        {
            try
            {
                return TipoEnvaseDAL.Instancia.GetPaged(aux => aux.TIE_Estado == objTipoEnvase.TIE_Estado, aux => aux.IDTiposEnvase, false);
            }
            catch (Exception ex)
            {
                throw new Exception("Tipo Envase: Listar" + " " + ex.Message);
            }
        }

        /// <summary>
        /// Lista TipoEnvase activos.
        /// </summary>
        /// <param name="objTipoEnvase"></param>
        /// <returns>objTipoEnvase</returns>
        public IList<TipoEnvase> ListarTiposEnvaseTodosActivos()
        {
            try
            {
                return TipoEnvaseDAL.Instancia.GetPaged(aux => aux.TIE_Estado == Constantes.EstadoActivo, aux => aux.TIE_Descripcion, true);
            }
            catch (Exception ex)
            {
                throw new Exception("Tipo Envase: ListarTiposEnvaseTodosActivos" + " " + ex.Message);
            }
        }
        /// <summary>
        /// Obtener TipoEnvase por IDTipoEnvase.
        /// </summary>
        /// <param name="objTipoEnvase"></param>
        /// <returns>objTipoEnvase</returns>
        public TipoEnvase ObtenerDatosPorID(TipoEnvase objTipoEnvase)
        {
            try
            {
                return TipoEnvaseDAL.Instancia.Single(aux => aux.IDTiposEnvase == objTipoEnvase.IDTiposEnvase);
            }
            catch (Exception ex)
            {
                throw new Exception("Tipo Envase: Obtener Datos" + " " + ex.Message);
            }
        }

        /// <summary>
        /// Eliminacion Lógica TipoEnvase - Estado=0
        /// </summary>
        /// <param name="objTipoEnvase"></param>
        /// <returns>objTipoEnvase</returns>


        public TipoEnvase Eliminar(TipoEnvase objTipoEnvase)
        {
            try
            {
                return TipoEnvaseDAL.Instancia.Update(objTipoEnvase);
            }
            catch (Exception ex)
            {
                throw new Exception("Tipo Envase: Eliminar" + " " + ex.Message);
            }
        }

        /// <summary>
        /// Busca TipoEnvase Activos en base filtros
        /// </summary>
        /// <param name="objTipoEnvase"></param>
        /// <returns>objTipoEnvase</returns>
        public IList<TipoEnvase> BuscarPorFiltros(TipoEnvase objTipoEnvase)
        {
            try
            {
                return TipoEnvaseDAL.Instancia.GetPaged(aux => aux.TIE_Descripcion.Contains(objTipoEnvase.TIE_Descripcion)
                    && aux.TIE_Estado == objTipoEnvase.TIE_Estado, aux => aux.IDTiposEnvase, false);

            }
            catch (Exception ex)
            {
                throw new Exception("Tipo Envase: Buscar Por Filtros" + " " + ex.Message);
            }
        }

        /// <summary>
        /// Busca TipoEnvaseActivos en base filtros
        /// </summary>
        /// <param name="objTipoEnvase"></param>
        /// <returns>objTipoEnvase</returns>
        public IList<TipoEnvase> BuscarPorNombre(TipoEnvase objTipoEnvase)
        {
            try
            {
                return TipoEnvaseDAL.Instancia.GetFiltered(aux => aux.TIE_Descripcion == objTipoEnvase.TIE_Descripcion
                    && aux.TIE_Estado == objTipoEnvase.TIE_Estado);

            }
            catch (Exception ex)
            {
                throw new Exception("Tipo Envase: Buscar Por Nombre" + " " + ex.Message);
            }
        }
    }
}
