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
    public class UnidadMedidaBL:Singleton<UnidadMedidaBL>
    {
        private System.Linq.Expressions.Expression<Func<UnidadMedida, bool>> where;
        /// <summary>
        /// Inserta un nueva Unidad Medida
        /// </summary>
        /// <param name="objUnidadMedida"></param>
        /// <returns>objUnidadMedida</returns>
        public UnidadMedida Insertar(UnidadMedida objUnidadMedida)
        {
            try
            {
                return UnidadMedidaDAL.Instancia.Add(objUnidadMedida);
            }
            catch (Exception ex)
            {
                throw new Exception("Unidad Medida: Insertar" + ex.Message);
            }
        }
        /// <summary>
        /// Actualiza Unidad Medida existente
        /// </summary>
        /// <param name="objUnidadMedida"></param>
        /// <returns>objUnidadMedida</returns>

        public UnidadMedida Actualizar(UnidadMedida objUnidadMedida)
        {
            try
            {
                return UnidadMedidaDAL.Instancia.Update(objUnidadMedida);
            }
            catch (Exception ex)
            {
                throw new Exception("Unidad Medida: Actualizar" + ex.Message);
            }
        }

        /// <summary>
        /// Lista Unidad Medidas activos.
        /// </summary>
        /// <param name="objUnidadMedida"></param>
        /// <returns>objUnidadMedida</returns>
        public IList<UnidadMedida> ListarTodosActivos(UnidadMedida objUnidadMedida)
        {
            try
            {
               // return UnidadMedidaDAL.Instancia.GetFiltered(aux => aux.UNM_Estado == objUnidadMedida.UNM_Estado);
               return UnidadMedidaDAL.Instancia.GetPaged(aux =>
                   aux.UNM_Estado == objUnidadMedida.UNM_Estado, aux => aux.UNM_Codigo, true);
            }
            catch (Exception ex)
            {
                throw new Exception("Unidad Medida: Listar" + " " + ex.Message);
            }
        }
        /// <summary>
        /// Lista Unidad Medidas activos.
        /// </summary>
        /// <param name="objUnidadMedida"></param>
        /// <returns>objUnidadMedida</returns>
        public IList<UnidadMedida> ListarUnidadMedidaTodosActivos()
        {
            try
            {
                return UnidadMedidaDAL.Instancia.GetPaged(aux => 
                    aux.UNM_Estado == Constantes.EstadoActivo, aux => aux.UNM_Codigo, true);
            }
            catch (Exception ex)
            {
                throw new Exception("Unidad Medida: ListarUnidadMedidaTodosActivos" + " " + ex.Message);
            }
        }
        /// <summary>
        /// Obtener Unidad Medida por IDUnidadMedida.
        /// </summary>
        /// <param name="objUnidadMedida"></param>
        /// <returns>objUnidadMedida</returns>
        public UnidadMedida ObtenerDatosPorID(int id)
        {
            try
            {
                return UnidadMedidaDAL.Instancia.Single(aux => 
                    aux.IDUnidadMedida == id && aux.UNM_Estado==Constantes.EstadoActivo);
            }
            catch (Exception ex)
            {
                throw new Exception("Unidad Medida: Obtener Datos" + " " + ex.Message);
            }
        }

        /// <summary>
        /// Eliminacion Lógica Unidad Medida - Estado=0
        /// </summary>
        /// <param name="objUnidadMedida"></param>
        /// <returns>objUnidadMedida</returns>


        public UnidadMedida Eliminar(UnidadMedida objUnidadMedida)
        {
            try
            {
                return UnidadMedidaDAL.Instancia.Update(objUnidadMedida);
            }
            catch (Exception ex)
            {
                throw new Exception("Unidad Medida: Eliminar" + " " + ex.Message);
            }
        }

        /// <summary>
        /// Busca Unidad Medida Activos en base filtros
        /// </summary>
        /// <param name="objUnidadMedida"></param>
        /// <returns>objUnidadMedida</returns>
        public IList<UnidadMedida> BuscarPorFiltros(UnidadMedida objUnidadMedida)
        {
            try
            {
                return UnidadMedidaDAL.Instancia.GetPaged(aux => 
                    aux.UNM_Codigo.Contains(objUnidadMedida.UNM_Codigo)
                    && aux.UNM_Nombre.Contains(objUnidadMedida.UNM_Nombre)
                    && aux.UNM_NombreCorto.Contains(objUnidadMedida.UNM_NombreCorto) 
                    && aux.UNM_Estado == Constantes.EstadoActivo,
                    aux => aux.UNM_Codigo, true);

            }
            catch (Exception ex)
            {
                throw new Exception("Unidad Medida: Listar" + " " + ex.Message);
            }
        }

        /// <summary>
        /// Busca Unidad Medida Activos en base filtros
        /// </summary>
        /// <param name="objUnidadMedida"></param>
        /// <returns>objUnidadMedida</returns>
        public IList<UnidadMedida> BuscarPorNombre(UnidadMedida objUnidadMedida)
        {
            try
            {
                return UnidadMedidaDAL.Instancia.GetFiltered(aux => 
                    aux.UNM_Nombre == objUnidadMedida.UNM_Nombre
                    && aux.UNM_Estado == objUnidadMedida.UNM_Estado);

            }
            catch (Exception ex)
            {
                throw new Exception("Unidad Medida: Listar" + " " + ex.Message);
            }
        }
    }
}
