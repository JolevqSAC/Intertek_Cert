using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//referencia
using Intertek.Helpers;
using Intertek.Business.Entities;
using Intertek.DataAccess;

namespace Intertek.Business.Logic
{
    public class SubEnsayoBL:Singleton<SubEnsayoBL>
    {
        /// <summary>
        /// Registra los datos del ensayo
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public SubEnsayo Insertar(SubEnsayo objSubEnsayo)
        {
            try
            {
                return SubEnsayoDAL.Instancia.Add(objSubEnsayo);
            }
            catch (Exception ex)
            {
                throw new Exception("Insertar SubEnsayo:" + ex.Message);
            }
        }

        /// <summary>
        /// Actualiza los datos del SubEnsayo
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public SubEnsayo Actualizar(SubEnsayo objSubEnsayo)
        {
            try
            {
                return SubEnsayoDAL.Instancia.Update(objSubEnsayo);
            }
            catch (Exception ex)
            {
                throw new Exception("Actualizar SubEnsayo:" + ex.Message);
            }
        }
        /// <summary>
        /// Eliminar los datos del SubEnsayo
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public int Eliminar(SubEnsayo objSubEnsayo)
        {
            try
            {
                return SubEnsayoDAL.Instancia.Delete(objSubEnsayo);
            }
            catch (Exception ex)
            {
                throw new Exception("Eliminar SubEnsayo:" + ex.Message);
            }
        }

        /// <summary>
        /// Obtiene todos los SubEnsayos de un cliente
        /// </summary>
        /// <param name="idEnsayo"></param>
        /// <returns></returns>
        public IList<SubEnsayo> ObtenerSubEnsayos(Int32 idEnsayo)
        {
            try
            {
                return SubEnsayoDAL.Instancia.GetFiltered(aux => aux.IDEnsayo == idEnsayo && aux.SEN_Estado == Constantes.EstadoActivo);
            }
            catch (Exception ex)
            {
                throw new Exception("Obtener Contactos por Ensayo: " + ex.Message);
            }
        }

        /// <summary>
        /// Obtiene los datos del usuario por ID
        /// </summary>
        /// <param name="idSubEnsayo"></param>
        /// <returns></returns>
        public SubEnsayo ObtenerSubEnsayoById(Int32 idSubEnsayo)
        {
            try
            {
                return SubEnsayoDAL.Instancia.Single(aux => aux.IDSubEnsayo == idSubEnsayo);

            }
            catch (Exception ex)
            {
                throw new Exception("obtener SubEnsayo BYID: " + ex.Message);
            }
        }

        public IList<SubEnsayo> ListarSubEnsayos(SubEnsayo objSubEnsayo)
        {
            try
            {
                return SubEnsayoDAL.Instancia.GetPaged(aux => aux.SEN_Codigo.Contains(objSubEnsayo.SEN_Codigo) 
                    && aux.SEN_Nombre.Contains(objSubEnsayo.SEN_Nombre)
                    && aux.SEN_NombreIngles.Contains(objSubEnsayo.SEN_NombreIngles)
                    && aux.SEN_Estado == Constantes.EstadoActivo, 
                    aux => aux.SEN_Nombre, true, "Laboratorio");
            }
            catch (Exception ex)
            {
                throw new Exception("Listar SubEnsayo: " + ex.Message);
            }
        }

        public IList<SubEnsayo> ListarSubEnsayosByLaboratorio(int idLaboratorio)
        {
            try
            {

                return SubEnsayoDAL.Instancia.GetPaged(aux => aux.SEN_Estado == Constantes.EstadoActivo,
                    aux => aux.SEN_Nombre, true);

            }
            catch (Exception ex)
            {
                throw new Exception("Listar SubEnsayoTodos: " + ex.Message);
            }
        }
    }
}
