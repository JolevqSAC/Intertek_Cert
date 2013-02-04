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
    public class EnsayoBL:Singleton<EnsayoBL>
    {
        /// <summary>
        /// Registra los datos del ensayo
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public Ensayo Insertar(Ensayo objEnsayo)
        {
            try
            {
                return EnsayoDAL.Instancia.Add(objEnsayo);
            }
            catch (Exception ex)
            {
                throw new Exception("Insertar Ensayo:" + ex.Message);
            }
        }

        /// <summary>
        /// Actualiza los datos del Ensayo
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public Ensayo Actualizar(Ensayo objEnsayo)
        {
            try
            {
                return EnsayoDAL.Instancia.Update(objEnsayo);
            }
            catch (Exception ex)
            {
                throw new Exception("Actualizar Ensayo:" + ex.Message);
            }
        }
        /// <summary>
        /// Eliminar los datos del Ensayo
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public Ensayo Eliminar(Ensayo objEnsayo)
        {
            try
            {
                return EnsayoDAL.Instancia.Update(objEnsayo);
            }
            catch (Exception ex)
            {
                throw new Exception("Eliminar Ensayo:" + ex.Message);
            }
        }
        
        /// <summary>
        /// Obtiene los datos del usuario por ID
        /// </summary>
        /// <param name="idEnsayo"></param>
        /// <returns></returns>
        public Ensayo ObtenerEnsayoById(Int32 idEnsayo)
        {
            try
            {
                return EnsayoDAL.Instancia.Single(aux => aux.IDEnsayo == idEnsayo);

            }
            catch (Exception ex)
            {
                throw new Exception("obtener Ensayo BYID: " + ex.Message);
            }
        }

        public IList<Ensayo> ListarEnsayos(Ensayo objEnsayo)
        {
            try
            {
                return EnsayoDAL.Instancia.GetPaged(aux => aux.ENS_Codigo.Contains(objEnsayo.ENS_Codigo) 
                    && aux.ENS_Nombre.Contains(objEnsayo.ENS_Nombre)
                    && aux.ENS_NombreIngles.Contains(objEnsayo.ENS_NombreIngles)
                    && aux.ENS_Descripcion.Contains(objEnsayo.ENS_Descripcion) 
                    && aux.ENS_Estado == Constantes.EstadoActivo, 
                    aux => aux.ENS_Nombre, true, "Laboratorio");
            }
            catch (Exception ex)
            {
                throw new Exception("Listar Ensayo: " + ex.Message);
            }
        }

        public IList<Ensayo> ListarEnsayosByLaboratorio(int idLaboratorio)
        {
            try
            {

                return EnsayoDAL.Instancia.GetPaged(aux => aux.ENS_Estado == Constantes.EstadoActivo && aux.IDLaboratorio==idLaboratorio ,
                   aux => aux.ENS_Nombre, true);

            }
            catch (Exception ex)
            {
                throw new Exception("Listar EnsayoTodos: " + ex.Message);
            }
        }
    }
}
