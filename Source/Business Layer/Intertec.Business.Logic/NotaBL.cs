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
    public class NotaBL:Singleton<NotaBL>
    {
        private System.Linq.Expressions.Expression<Func<Nota, bool>> where;
        /// <summary>
        /// Inserta un nuevo Nota
        /// </summary>
        /// <param name="objCCosto"></param>
        /// <returns>objNota</returns>
        public Nota Insertar(Nota objNota)
        {
            try
            {
                return NotaDAL.Instancia.Add(objNota);
            }
            catch (Exception ex)
            {
                throw new Exception("Nota: Insertar" + ex.Message);
            }
        }
        /// <summary>
        /// Actualiza un Nota existente
        /// </summary>
        /// <param name="objNota"></param>
        /// <returns>objNota</returns>

        public Nota Actualizar(Nota objNota)
        {
            try
            {
                return NotaDAL.Instancia.Update(objNota);
            }
            catch (Exception ex)
            {
                throw new Exception("Nota: Actualizar" + ex.Message);
            }
        }

        /// <summary>
        /// Lista los Nota activos.
        /// </summary>
        /// <param name="objNota"></param>
        /// <returns>objNota</returns>
        public IList<Nota> ListarTodosActivos(Nota objNota)
        {
            try
            {
                //return NotaDAL.Instancia.GetFiltered(aux => aux.COM_Estado == objNota.COM_Estado);
                return NotaDAL.Instancia.GetPaged(aux =>
                    aux.NOT_Estado == objNota.NOT_Estado, aux => aux.NOT_Codigo, true);
            }
            catch (Exception ex)
            {
                throw new Exception("Nota: Listar" + " " + ex.Message);
            }
        }

        /// <summary>
        /// Obtener Nota por IDNota.
        /// </summary>
        /// <param name="objNota"></param>
        /// <returns>objNota</returns>
        public Nota ObtenerDatosPorID(int id)
        {
            try
            {
                return NotaDAL.Instancia.Single(aux => aux.IDNota == id
                    && aux.NOT_Estado == Constantes.EstadoActivo);
            }
            catch (Exception ex)
            {
                throw new Exception("Nota: Obtener Datos" + " " + ex.Message);
            }
        }

        public int Eliminar(Nota objNota)
        {
            try
            {
                return NotaDAL.Instancia.Delete(objNota);
            }
            catch (Exception ex)
            {
                throw new Exception("Nota: Eliminar" + " " + ex.Message);
            }
        }

        /// <summary>
        /// Busca Nota Activos en base filtros
        /// </summary>
        /// <param name="objNota"></param>
        /// <returns>objNota</returns>
        public IList<Nota> BuscarPorFiltros(Nota objNota)
        {
            try
            {
                return NotaDAL.Instancia.GetPaged(aux =>
                    aux.NOT_Codigo.Contains(objNota.NOT_Codigo)
                    && aux.NOT_Nombre.Contains(objNota.NOT_Nombre)                   
                    && aux.NOT_Estado == Constantes.EstadoActivo,
                    aux => aux.NOT_Codigo, true);

            }
            catch (Exception ex)
            {
                throw new Exception("Nota: Listar" + " " + ex.Message);
            }
        }

        /// <summary>
        /// Busca Nota Activos en base filtros
        /// </summary>
        /// <param name="objNota"></param>
        /// <returns>objNota</returns>
        public IList<Nota> BuscarPorDescripcion(Nota objNota)
        {
            try
            {
                return NotaDAL.Instancia.GetFiltered(aux => aux.NOT_Descripcion == objNota.NOT_Descripcion
                    && aux.NOT_Estado == objNota.NOT_Estado);

            }
            catch (Exception ex)
            {
                throw new Exception("Nota: Listar" + " " + ex.Message);
            }
        }
    }
}
