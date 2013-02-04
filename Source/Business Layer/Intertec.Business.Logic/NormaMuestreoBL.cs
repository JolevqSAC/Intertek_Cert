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
    public class NormaMuestreoBL:Singleton<NormaMuestreoBL>
    {
        private System.Linq.Expressions.Expression<Func<NormaMuestreo, bool>> where;
        /// <summary>
        /// Inserta Norma Muestreo
        /// </summary>
        /// <param name="objNormaMuestreo"></param>
        /// <returns>objNormaMuestreo</returns>
        public NormaMuestreo Insertar(NormaMuestreo objNormaMuestreo)
        {
            try
            {
                return NormaMuestreoDAL.Instancia.Add(objNormaMuestreo);
            }
            catch (Exception ex)
            {
                throw new Exception("Norma Muestreo: Insertar" + ex.Message);
            }
        }
        /// <summary>
        /// Actualiza NormaMuestreo
        /// </summary>
        /// <param name="objNormaMuestreo"></param>
        /// <returns>objNormaMuestreo</returns>

        public NormaMuestreo Actualizar(NormaMuestreo objNormaMuestreo)
        {
            try
            {
                return NormaMuestreoDAL.Instancia.Update(objNormaMuestreo);
            }
            catch (Exception ex)
            {
                throw new Exception("NormaMuestreo: Actualizar" + ex.Message);
            }
        }

        /// <summary>
        /// Lista NormaMuestreo activos ordenados de forma descendente.
        /// </summary>
        /// <param name="objNormaMuestreo"></param>
        /// <returns>objNormaMuestreo</returns>
        public IList<NormaMuestreo> ListarTodosActivos(NormaMuestreo objNormaMuestreo)
        {
            try
            {
                return NormaMuestreoDAL.Instancia.GetPaged(aux => aux.NOM_Estado == objNormaMuestreo.NOM_Estado, aux => aux.NOM_Codigo, true);
            }
            catch (Exception ex)
            {
                throw new Exception("Norma Muestreo: Listar" + " " + ex.Message);
            }
        }

        /// <summary>
        /// Obtener Norma Muestreo por IDNormaMuestreo.
        /// </summary>
        /// <param name="objNormaMuestreo"></param>
        /// <returns>objNormaMuestreo</returns>
        public NormaMuestreo ObtenerDatosPorID(int id)
        {
            try
            {
                return NormaMuestreoDAL.Instancia.Single(aux =>
                    aux.IDNormaMuestreo == id
                    && aux.NOM_Estado==Constantes.EstadoActivo);
            }
            catch (Exception ex)
            {
                throw new Exception("Norma Muestreo: Obtener Datos" + " " + ex.Message);
            }
        }

        /// <summary>
        /// Eliminacion Lógica NormaMuestreo - Estado=0
        /// </summary>
        /// <param name="objNormaMuestreo"></param>
        /// <returns>objNormaMuestreo</returns>


        public NormaMuestreo Eliminar(NormaMuestreo objNormaMuestreo)
        {
            try
            {
                return NormaMuestreoDAL.Instancia.Update(objNormaMuestreo);
            }
            catch (Exception ex)
            {
                throw new Exception("Norma Muestreo: Eliminar" + " " + ex.Message);
            }
        }

        /// <summary>
        /// Busca NormaMuestreo Activos en base filtros
        /// </summary>
        /// <param name="objNormaMuestreo"></param>
        /// <returns>objNormaMuestreo</returns>
        public IList<NormaMuestreo> BuscarPorFiltros(NormaMuestreo objNormaMuestreo)
        {
            try
            {
                return NormaMuestreoDAL.Instancia.GetPaged(aux => 
                    aux.NOM_Codigo.Contains(objNormaMuestreo.NOM_Codigo)
                    && aux.NOM_Nombre.Contains(objNormaMuestreo.NOM_Nombre)
                    && aux.NOM_Anio==objNormaMuestreo.NOM_Anio
                    && aux.NOM_Estado == Constantes.EstadoActivo,
                    aux => aux.NOM_Codigo, true);
            }
            catch (Exception ex)
            {
                throw new Exception("Norma Muestreo: Listar" + " " + ex.Message);
            }
        }


        /// <summary>
        /// Busca NormaMuestreo Activos en base a los filtros de codigo y nombre
        /// </summary>
        /// <param name="objNormaMuestreo"></param>
        /// <returns>objNormaMuestreo</returns>
        public IList<NormaMuestreo> BuscarPorNombreCodigo(NormaMuestreo objNormaMuestreo)
        {
            try
            {
                return NormaMuestreoDAL.Instancia.GetPaged(aux =>
                    aux.NOM_Codigo.Contains(objNormaMuestreo.NOM_Codigo)
                    && aux.NOM_Nombre.Contains(objNormaMuestreo.NOM_Nombre)
                    && aux.NOM_Estado == Constantes.EstadoActivo,
                    aux => aux.NOM_Codigo, true);
            }
            catch (Exception ex)
            {
                throw new Exception("Norma Muestreo: Listar" + " " + ex.Message);
            }
        }
        
        
        /// <summary>
        /// Busca Norma Muestreo Activos por Nombre
        /// </summary>
        /// <param name="objNormaMuestreo"></param>
        /// <returns>objNormaMuestreo</returns>
        public IList<NormaMuestreo> BuscarPorNombre(NormaMuestreo objNormaMuestreo)
        {
            try
            {
                return NormaMuestreoDAL.Instancia.GetFiltered(aux => aux.NOM_Nombre == objNormaMuestreo.NOM_Nombre
                    && aux.NOM_Estado == objNormaMuestreo.NOM_Estado);

            }
            catch (Exception ex)
            {
                throw new Exception("Norma Muestreo: Listar" + " " + ex.Message);
            }
        }

    }
}
