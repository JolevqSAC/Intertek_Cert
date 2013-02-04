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
    public class ServicioBL : Singleton<ServicioBL>
    {
        /// <summary>
        /// Inserta Categoría
        /// </summary>
        /// <param name="objServicio"></param>
        /// <returns>objServicio</returns>
        public Servicio Insertar(Servicio objServicio)
        {
            try
            {
                return ServicioDAL.Instancia.Add(objServicio);

            }
            catch (Exception ex)
            {
                throw new Exception("Servicio Producto: Insertar" + ex.Message);
            }
        }
        /// <summary>
        /// Retorna la lista de Servicios
        /// </summary>
        /// <param name="objServicio">busca por codigo, nombre,nombre en ingles</param>
        /// <returns></returns>
        public IList<Servicio> ListarServicio(Servicio objServicio)
        {
            try
            {
                return ServicioDAL.Instancia.GetFiltered(aux => 
                    aux.SER_Codigo.Contains(objServicio.SER_Codigo) 
                    && aux.SER_Nombre.Contains(objServicio.SER_Nombre) 
                    && aux.SER_NombreIngles.Contains(objServicio.SER_NombreIngles)
                    && aux.SER_Estado == Constantes.EstadoActivo);
            }
            catch (Exception ex)
            {
                throw new Exception("Servicio Producto: Listar " + ex.Message);
            }
        }

        /// <summary>
        /// Lista los Servicios ordenados 
        /// </summary>
        /// <returns></returns>
        public IList<Servicio> ListarTodosServicios()
        {
            try
            {
                return ServicioDAL.Instancia.GetPaged(aux => aux.SER_Estado == Constantes.EstadoActivo,
                                                  aux => aux.SER_Codigo, true).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("ListarTodosServicios", ex);
            }
        }
        /// <summary>
        /// Elimina el Servicio
        /// </summary>
        /// <param name="objServicio">Actualiza el Estado a 0</param>
        /// <returns></returns>
        public Servicio Eliminar(Servicio objServicio)
        {
            try
            {
                return ServicioDAL.Instancia.Update(objServicio);

            }
            catch (Exception ex)
            {
                throw new Exception("Categoria Producto: Eliminar" + ex.Message);
            }
        }

        /// <summary>
        /// Elimina el Servicios
        /// </summary>
        /// <param name="objServicio">Actualiza el Estado a 0</param>
        /// <returns></returns>
        public Servicio Actualizar(Servicio objServicio)
        {
            try
            {
                return ServicioDAL.Instancia.Update(objServicio);

            }
            catch (Exception ex)
            {
                throw new Exception("Servicio: Actualizar" + ex.Message);
            }
        }
        /// <summary>
        /// Retorna el objeto Servicios
        /// </summary>
        /// <param name="idServicio">Id del Servicio</param>
        /// <returns></returns>
        public Servicio ObtenerServicioByID(int idServicio)
        {
            try
            {
                return ServicioDAL.Instancia.Single(aux =>
                    aux.IDServicio == idServicio 
                    && aux.SER_Estado == Constantes.EstadoActivo);
            }
            catch (Exception ex)
            {
                throw new Exception("Servicio: Actualizar" + ex.Message);
            }
        }
    }
}
