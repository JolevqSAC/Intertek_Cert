using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Intertek.Business.Entities;
using Intertek.DataAccess;
using Intertek.Helpers;

namespace Intertek.Business.Logic
{
    public class DistritoBL:Singleton<DistritoBL>
    {
        /// <summary>
        /// Retorna todos los laboratorios activos y los ordena por nombre
        /// </summary>
        /// <returns></returns>
        public IList<Distrito> ListarTodosDistritosByProvincia(int idProvincia)
        {

            try
            {
                return DistritoDAL.Instancia.GetPaged(aux => aux.IDProvincia == idProvincia, aux => aux.DIS_Nombre, true).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("ListarTodosDistritos By Provincia", ex);
            }
        }
        /// <summary>
        /// Obtiene el distrito con sus relaciones por IDDistrito
        /// </summary>
        /// <param name="idDistritro"></param>
        /// <returns></returns>
        public Distrito ObtenerDistritoByID(int idDistritro)
        {
            try
            {
                //return DistritoDAL.Instancia.Single(aux=> aux.IDDistrito==idDistritro,"Provincia.IDDepartamento","Departamento.IDPais");
                return DistritoDAL.Instancia.Single(aux => aux.IDDistrito == idDistritro, "Provincia");
                //objDistrito.Provincia=ProvinciaDAL.Instancia.Single(aux => aux.IDProvincia == objDistrito.IDProvincia, "Departamento");
                
                //return objDistrito;
                //return DistritoDAL.Instancia.Single(aux => aux.IDDistrito == idDistritro, "Provincia", "Departamento");
            }
            catch (Exception ex)
            {
                throw new Exception("ObtenerDistrito ByID", ex);
            }
        }

        /// <summary>
        /// Retorna el objeto Provincia
        /// </summary>
        /// <param name="idProvincia"></param>
        /// <returns></returns>
        public Provincia ObtenerProvinciaByID(int idProvincia)
        {
            try
            {
                return ProvinciaDAL.Instancia.Single(aux => aux.IDProvincia == idProvincia, "Departamento");
            }
            catch (Exception ex)
            {
                throw new Exception("ObtenerProvincia ByID", ex);
            }
        }
        /// <summary>
        /// Lista las provincias ordenadas según el Departamento
        /// </summary>
        /// <returns></returns>
        public IList<Provincia> ListarTodosProvincias(int idDepartamento)
        {
            try
            {
                return ProvinciaDAL.Instancia.GetPaged(aux=> aux.IDDepartamento==idDepartamento, aux => aux.PRO_Nombre,true).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("ListarTodosProvincias", ex);
            }
        }

        /// <summary>
        /// Lista los departamentos ordenados según el Id del País
        /// </summary>
        /// <returns></returns>
        public IList<Departamento> ListarTodosDepartamentos(int idPais)
        {
            try
            {
                return DepartamentoDAL.Instancia.GetPaged(aux=>aux.IDPais==idPais, aux => aux.DEP_Nombre,true).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("ListarTodosDepartamentos", ex);
            }
        }

        /// <summary>
        /// Lista Los Países Ordenados
        /// </summary>
        /// <returns></returns>
        public IList<Pais> ListarTodosPaises()
        {
            try
            {
                return PaisDAL.Instancia.GetAll().OrderBy(aux => aux.PAI_Nombre).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("ListarTodosPaises", ex);
            }
        }
    }
}
