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
    public class TipoLaboratorioBL:Singleton<TipoLaboratorioBL>
    {
        private System.Linq.Expressions.Expression<Func<TipoLaboratorio, bool>> where;
        /// <summary>
        /// Inserta un nuevo TipoLaboratorio
        /// </summary>
        /// <param name="objTipoLaboratorio"></param>
        /// <returns>objTipoLaboratorio</returns>
        public TipoLaboratorio Insertar(TipoLaboratorio objTipoLaboratorio)
        {
            try
            {
                return TipoLaboratorioDAL.Instancia.Add(objTipoLaboratorio);
            }
            catch (Exception ex)
            {
                throw new Exception("Tipo Laboratorio: Insertar" + ex.Message);
            }
        }
        /// <summary>
        /// Actualiza TipoLaboratorio
        /// </summary>
        /// <param name="objTipoLaboratorio"></param>
        /// <returns>objTipoLaboratorio</returns>

        public TipoLaboratorio Actualizar(TipoLaboratorio objTipoLaboratorio)
        {
            try
            {
                return TipoLaboratorioDAL.Instancia.Update(objTipoLaboratorio);
            }
            catch (Exception ex)
            {
                throw new Exception("Tipo Laboratorio: Actualizar" + ex.Message);
            }
        }

        /// <summary>
        /// Lista TipoLaboratorio activos ordenados de forma descendente.
        /// </summary>
        /// <param name="objTipoLaboratorio"></param>
        /// <returns>objTipoLaboratorio</returns>
        public IList<TipoLaboratorio> ListarTodosActivos(TipoLaboratorio objTipoLaboratorio)
        {
            try
            {
                return TipoLaboratorioDAL.Instancia.GetPaged(aux=>aux.TLA_Estado==objTipoLaboratorio.TLA_Estado, aux=> aux.IDTipoLaboratorio, false);
            }
            catch (Exception ex)
            {
                throw new Exception("Tipo Laboratorio: Listar" + " " + ex.Message);
            }
        }

        /// <summary>
        /// Obtener TipoLaboratorio por IDTipoLaboratorio.
        /// </summary>
        /// <param name="objTipoLaboratorio"></param>
        /// <returns>objTipoLaboratorio</returns>
        public TipoLaboratorio ObtenerDatosPorID(TipoLaboratorio objTipoLaboratorio)
        {
            try
            {
                return TipoLaboratorioDAL.Instancia.Single(aux => aux.IDTipoLaboratorio == objTipoLaboratorio.IDTipoLaboratorio);
            }
            catch (Exception ex)
            {
                throw new Exception("Tipo Laboratorio: Obtener Datos" + " " + ex.Message);
            }
        }

        /// <summary>
        /// Eliminacion Lógica TipoLaboratorio - Estado=0
        /// </summary>
        /// <param name="objTipoLaboratorio"></param>
        /// <returns>objTipoLaboratorio</returns>


        public TipoLaboratorio Eliminar(TipoLaboratorio objTipoLaboratorio)
        {
            try
            {
                return TipoLaboratorioDAL.Instancia.Update(objTipoLaboratorio);
            }
            catch (Exception ex)
            {
                throw new Exception("Tipo Laboratorio: Eliminar" + " " + ex.Message);
            }
        }

        /// <summary>
        /// Busca TipoLaboratorio Activos en base filtros
        /// </summary>
        /// <param name="objTipoLaboratorio"></param>
        /// <returns>objTipoLaboratorio</returns>
        public IList<TipoLaboratorio> BuscarPorFiltros(TipoLaboratorio objTipoLaboratorio)
        {
            try
            {
                return TipoLaboratorioDAL.Instancia.GetPaged(aux => aux.TLA_Nombre.Contains(objTipoLaboratorio.TLA_Nombre)
                    && aux.TLA_Estado == objTipoLaboratorio.TLA_Estado, aux => aux.IDTipoLaboratorio, false);

            }
            catch (Exception ex)
            {
                throw new Exception("Tipo Laboratorio: Listar" + " " + ex.Message);
            }
        }

        /// <summary>
        /// Busca TipoLaboratorio Activos en base filtros
        /// </summary>
        /// <param name="objTipoLaboratorio"></param>
        /// <returns>objTipoLaboratorio</returns>
        public IList<TipoLaboratorio> BuscarPorNombre(TipoLaboratorio objTipoLaboratorio)
        {
            try
            {
                return TipoLaboratorioDAL.Instancia.GetFiltered(aux => aux.TLA_Nombre == objTipoLaboratorio.TLA_Nombre
                    && aux.TLA_Estado == objTipoLaboratorio.TLA_Estado);

            }
            catch (Exception ex)
            {
                throw new Exception("Tipo Laboratorio: Listar" + " " + ex.Message);
            }
        }

        /// <summary>
        /// Lista TipoLaboratorio activos ordenados x Nombre.
        /// </summary>
        /// <param name="objTipoLaboratorio"></param>
        /// <returns>objTipoLaboratorio</returns>
        public IList<TipoLaboratorio> ListarPorNombre(TipoLaboratorio objTipoLaboratorio)
        {
            try
            {
                return TipoLaboratorioDAL.Instancia.GetPaged(aux => aux.TLA_Estado == objTipoLaboratorio.TLA_Estado, aux => aux.TLA_Nombre, true);
            }
            catch (Exception ex)
            {
                throw new Exception("Tipo Laboratorio: Listar" + " " + ex.Message);
            }
        }
    }
}
