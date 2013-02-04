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
    public class LaboratorioBL:Singleton<LaboratorioBL>
    {
        private System.Linq.Expressions.Expression<Func<Laboratorio, bool>> where;
        /// <summary>
        /// Retorna todos los laboratorios activos y los ordena por nombre
        /// </summary>
        /// <returns></returns>
        public IList<Laboratorio> ListarTodosLaboratorios()
        {            
            try {
                return LaboratorioDAL.Instancia.GetPaged(aux => aux.LAB_Estado == Constantes.EstadoActivo, aux=> aux.LAB_Nombre,true);
            }
            catch(Exception ex)
            {
                throw new Exception("ListarTodosLaboratorios", ex);
            }
        }

        /// <summary>
        /// Inserta un nuevo Laboratorio
        /// </summary>
        /// <param name="objLaboratorio"></param>
        /// <returns>objLaboratorio</returns>
        public Laboratorio Insertar(Laboratorio objLaboratorio)
        {
            try
            {
                return LaboratorioDAL.Instancia.Add(objLaboratorio);
            }
            catch (Exception ex)
            {
                throw new Exception("Laboratorio: Insertar" + ex.Message);
            }
        }
        /// <summary>
        /// Actualiza Laboratorio
        /// </summary>
        /// <param name="objLaboratorio"></param>
        /// <returns>objLaboratorio</returns>

        public Laboratorio Actualizar(Laboratorio objLaboratorio)
        {
            try
            {
                return LaboratorioDAL.Instancia.Update(objLaboratorio);
            }
            catch (Exception ex)
            {
                throw new Exception("Laboratorio: Actualizar" + ex.Message);
            }
        }

        /// <summary>
        /// Lista Laboratorio activos.
        /// </summary>
        /// <param name="objLaboratorio"></param>
        /// <returns>objLaboratorio</returns>
        public IList<Laboratorio> ListarTodosActivos(Laboratorio objLaboratorio)
        {
            try
            {
                return LaboratorioDAL.Instancia.GetPaged(aux => aux.LAB_Estado == objLaboratorio.LAB_Estado, aux => aux.LAB_Nombre, true,"TipoLaboratorio");
            }
            catch (Exception ex)
            {
                throw new Exception("Laboratorio: Listar" + " " + ex.Message);
            }
        }

        /// <summary>
        /// Obtener Laboratorio por IDLaboratorio.
        /// </summary>
        /// <param name="objLaboratorio"></param>
        /// <returns>objLaboratorio</returns>
        public Laboratorio ObtenerDatosPorID(int id)
        {
            try
            {
                return LaboratorioDAL.Instancia.Single(aux => aux.IDLaboratorio == id);
            }
            catch (Exception ex)
            {
                throw new Exception("Laboratorio: Obtener Datos" + " " + ex.Message);
            }
        }

        /// <summary>
        /// Eliminacion Lógica Laboratorio- Estado=0
        /// </summary>
        /// <param name="objLaboratorio"></param>
        /// <returns>objLaboratorio</returns>


        public Laboratorio Eliminar(Laboratorio objLaboratorio)
        {
            try
            {
                return LaboratorioDAL.Instancia.Update(objLaboratorio);
            }
            catch (Exception ex)
            {
                throw new Exception("Laboratorio: Eliminar" + " " + ex.Message);
            }
        }

        /// <summary>
        /// Busca Laboratorio Activos en base filtros
        /// </summary>
        /// <param name="objLaboratorio"></param>
        /// <returns>objLaboratorio</returns>
        public IList<Laboratorio> BuscarPorFiltros(Laboratorio objLaboratorio)
        {
            try
            {
                return LaboratorioDAL.Instancia.GetPaged(aux => aux.LAB_Nombre.Contains(objLaboratorio.LAB_Nombre)
                    && (objLaboratorio.IDTipoLaboratorio == null || aux.IDTipoLaboratorio == objLaboratorio.IDTipoLaboratorio)
                    && aux.LAB_Estado == objLaboratorio.LAB_Estado, aux => aux.IDLaboratorio, true,
                    "TipoLaboratorio");

            }
            catch (Exception ex)
            {
                throw new Exception("Laboratorio: Listar" + " " + ex.Message);
            }
        }

        /// <summary>
        /// Busca Laboratorio Activos en base filtros
        /// </summary>
        /// <param name="objLaboratorio"></param>
        /// <returns>objLaboratorio</returns>
        public IList<Laboratorio> BuscarPorNombre(Laboratorio objLaboratorio)
        {
            try
            {
                return LaboratorioDAL.Instancia.GetFiltered(aux => aux.LAB_Nombre == objLaboratorio.LAB_Nombre
                    && aux.LAB_Estado == objLaboratorio.LAB_Estado);

            }
            catch (Exception ex)
            {
                throw new Exception("Laboratorio: Listar" + " " + ex.Message);
            }
        }

    }
}
