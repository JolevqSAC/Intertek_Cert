using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//agregar referencia
using Intertek.Business.Entities;
using Intertek.DataAccess;
using Intertek.Helpers;

namespace Intertek.Business.Logic
{
    public class NormaRequisitoBL:Singleton<NormaRequisitoBL>
    {
        private System.Linq.Expressions.Expression<Func<NormaRequisito, bool>> where;
        /// <summary>
        /// Inserta Norma Requisito
        /// </summary>
        /// <param name="objNormaRequisito"></param>
        /// <returns>objNormaRequisito</returns>
        public NormaRequisito Insertar(NormaRequisito objNormaRequisito)
        {
            try
            {
                return NormaRequisitoDAL.Instancia.Add(objNormaRequisito);
            }
            catch (Exception ex)
            {
                throw new Exception("Norma Requisito: Insertar" + ex.Message);
            }
        }
        /// <summary>
        /// Actualiza NormaRequisito
        /// </summary>
        /// <param name="objNormaRequisito"></param>
        /// <returns>objNormaRequisito</returns>

        public NormaRequisito Actualizar(NormaRequisito objNormaRequisito)
        {
            try
            {
                return NormaRequisitoDAL.Instancia.Update(objNormaRequisito);
            }
            catch (Exception ex)
            {
                throw new Exception("NormaRequisito: Actualizar" + ex.Message);
            }
        }

        /// <summary>
        /// Lista NormaRequisito activos ordenados de forma descendente.
        /// </summary>
        /// <param name="objNormaRequisito"></param>
        /// <returns>objNormaRequisito</returns>
        public IList<NormaRequisito> ListarTodosActivos(NormaRequisito objNormaRequisito)
        {
            try
            {
                return NormaRequisitoDAL.Instancia.GetPaged(aux => aux.NRE_Estado == objNormaRequisito.NRE_Estado, aux => aux.NRE_Codigo, true);
            }
            catch (Exception ex)
            {
                throw new Exception("Norma Requisito: Listar" + " " + ex.Message);
            }
        }

        /// <summary>
        /// Obtener Norma Requisito por IDNormaRequisito.
        /// </summary>
        /// <param name="objNormaRequisito"></param>
        /// <returns>objNormaRequisito</returns>
        public NormaRequisito ObtenerDatosPorID(int id)
        {
            try
            {
                return NormaRequisitoDAL.Instancia.Single(aux =>
                    aux.IDNormaRequisito == id
                    && aux.NRE_Estado == Constantes.EstadoActivo);
            }
            catch (Exception ex)
            {
                throw new Exception("Norma Requisito: Obtener Datos" + " " + ex.Message);
            }
        }

        /// <summary>
        /// Eliminacion Lógica NormaRequisito - Estado=0
        /// </summary>
        /// <param name="objNormaRequisito"></param>
        /// <returns>objNormaRequisito</returns>


        public NormaRequisito Eliminar(NormaRequisito objNormaRequisito)
        {
            try
            {
                return NormaRequisitoDAL.Instancia.Update(objNormaRequisito);
            }
            catch (Exception ex)
            {
                throw new Exception("Norma Requisito: Eliminar" + " " + ex.Message);
            }
        }

        /// <summary>
        /// Busca NormaRequisito Activos en base filtros
        /// </summary>
        /// <param name="objNormaRequisito"></param>
        /// <returns>objNormaRequisito</returns>
        public IList<NormaRequisito> BuscarPorFiltros(NormaRequisito objNormaRequisito)
        {
            try
            {
                return NormaRequisitoDAL.Instancia.GetPaged(aux =>
                    aux.NRE_Codigo.Contains(objNormaRequisito.NRE_Codigo)
                    && aux.NRE_Nombre.Contains(objNormaRequisito.NRE_Nombre)
                    && aux.NRE_Anio == objNormaRequisito.NRE_Anio
                    && aux.NRE_Estado == Constantes.EstadoActivo,
                    aux => aux.NRE_Codigo, true);
            }
            catch (Exception ex)
            {
                throw new Exception("Norma Requisito: Listar" + " " + ex.Message);
            }
        }


        /// <summary>
        /// Busca NormaRequisito Activos en base a los filtros de codigo y nombre
        /// </summary>
        /// <param name="objNormaRequisito"></param>
        /// <returns>objNormaRequisito</returns>
        public IList<NormaRequisito> BuscarPorNombreCodigo(NormaRequisito objNormaRequisito)
        {
            try
            {
                return NormaRequisitoDAL.Instancia.GetPaged(aux =>
                    aux.NRE_Codigo.Contains(objNormaRequisito.NRE_Codigo)
                    && aux.NRE_Nombre.Contains(objNormaRequisito.NRE_Nombre)
                    && aux.NRE_Estado == Constantes.EstadoActivo,
                    aux => aux.NRE_Codigo, true);
            }
            catch (Exception ex)
            {
                throw new Exception("Norma Requisito: Listar" + " " + ex.Message);
            }
        }
        
        
        /// <summary>
        /// Busca Norma Requisito Activos por Nombre
        /// </summary>
        /// <param name="objNormaRequisito"></param>
        /// <returns>objNormaRequisito</returns>
        public IList<NormaRequisito> BuscarPorNombre(NormaRequisito objNormaRequisito)
        {
            try
            {
                return NormaRequisitoDAL.Instancia.GetFiltered(aux =>
                    aux.NRE_Nombre == objNormaRequisito.NRE_Nombre
                    && aux.NRE_Estado == objNormaRequisito.NRE_Estado);

            }
            catch (Exception ex)
            {
                throw new Exception("Norma Requisito: Listar" + " " + ex.Message);
            }
        }

    }
}
