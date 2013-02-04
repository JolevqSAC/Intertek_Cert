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
    public class AreaBL:Singleton<AreaBL>
    {
        /// <summary>
        /// Inserta Área
        /// </summary>
        /// <param name="objArea"></param>
        /// <returns>objArea</returns>
        public Area Insertar(Area objArea)
        {
            try
            {
                return AreaDAL.Instancia.Add(objArea);

            }
            catch (Exception ex)
            {
                throw new Exception("Area: Insertar" + ex.Message);
            }
        }
        /// <summary>
        /// Retorna la lista de areas
        /// </summary>
        /// <param name="objArea">busca por codigo, nombre,descripcion</param>
        /// <returns></returns>
        public IList<Area> ListarAreas(Area objArea)
        {
            try
            {
                return AreaDAL.Instancia.GetFiltered(aux => aux.ARE_Codigo.Contains(objArea.ARE_Codigo) && aux.ARE_Nombre.Contains(objArea.ARE_Nombre) && aux.ARE_Descripcion.Contains(objArea.ARE_Descripcion)
                    && aux.ARE_Estado == Constantes.EstadoActivo);

            }
            catch (Exception ex)
            {
                throw new Exception("Area: Listar " + ex.Message);
            }
        }

        /// <summary>
        /// Lista los Areas ordenados 
        /// </summary>
        /// <returns></returns>
        public IList<Area> ListarTodosAreas()
        {
            try
            {
                return AreaDAL.Instancia.GetPaged(aux => aux.ARE_Estado == Constantes.EstadoActivo,
                                                  aux => aux.ARE_Codigo, true).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("ListarTodosAreas", ex);
            }
        }
        /// <summary>
        /// Elimina el Area
        /// </summary>
        /// <param name="objArea">Actualiza el Estado a 0</param>
        /// <returns></returns>
        public Area Eliminar(Area objArea)
        {
            try
            {
                return AreaDAL.Instancia.Update(objArea);

            }
            catch (Exception ex)
            {
                throw new Exception("Area: Eliminar" + ex.Message);
            }
        }

        /// <summary>
        /// Elimina el Area
        /// </summary>
        /// <param name="objArea">Actualiza el Estado a 0</param>
        /// <returns></returns>
        public Area Actualizar(Area objArea)
        {
            try
            {
                return AreaDAL.Instancia.Update(objArea);

            }
            catch (Exception ex)
            {
                throw new Exception("Area: Actualizar" + ex.Message);
            }
        }
        /// <summary>
        /// Retorna el objeto Area 
        /// </summary>
        /// <param name="idArea">Id del Area</param>
        /// <returns></returns>
        public Area ObtenerAreaByID(int idArea)
        {
            try
            {
                return AreaDAL.Instancia.Single(aux => aux.IDArea == idArea && aux.ARE_Estado == Constantes.EstadoActivo);

            }
            catch (Exception ex)
            {
                throw new Exception("Area: Actualizar" + ex.Message);
            }
        }
    }
}
