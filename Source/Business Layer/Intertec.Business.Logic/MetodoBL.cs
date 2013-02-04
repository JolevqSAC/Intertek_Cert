using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//agregamos la referencia al repositorio
using Intertek.Business.Entities;
using Intertek.DataAccess;
using Intertek.Helpers;

namespace Intertek.Business.Logic
{
    public class MetodoBL:Singleton<MetodoBL>
    {
        /// <summary>
        /// Registra los datos del Metodo
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public Metodo Insertar(Metodo objMetodo)
        {
            try
            {
                return MetodoDAL.Instancia.Add(objMetodo);
            }
            catch (Exception ex)
            {
                throw new Exception("Insertar Metodo:" + ex.Message);
            }
        }

        /// <summary>
        /// Actualiza los datos del Metodo
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public Metodo Actualizar(Metodo objMetodo)
        {
            try
            {
                return MetodoDAL.Instancia.Update(objMetodo);
            }
            catch (Exception ex)
            {
                throw new Exception("Actualizar Metodo:" + ex.Message);
            }
        }
        /// <summary>
        /// Eliminar los datos del Metodo
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public Metodo Eliminar(Metodo objMetodo)
        {
            try
            {
                return MetodoDAL.Instancia.Update(objMetodo);
            }
            catch (Exception ex)
            {
                throw new Exception("Eliminar Metodo:" + ex.Message);
            }
        }

        /// <summary>
        /// Obtiene los datos del Metodo por ID
        /// </summary>
        /// <param name="idMetodo"></param>
        /// <returns></returns>
        public Metodo ObtenerMetodoById(Int32 idMetodo)
        {
            try
            {
                return MetodoDAL.Instancia.Single(aux => aux.IDMetodo == idMetodo);

            }
            catch (Exception ex)
            {
                throw new Exception("obtener Metodo por ID: " + ex.Message);
            }
        }

        public IList<Metodo> ListarMetodos(Metodo objMetodo)
        {
            try
            {

                return MetodoDAL.Instancia.GetPaged(aux => 
                    aux.MET_Codigo.Contains(objMetodo.MET_Codigo)
                    && aux.MET_Nombre.Contains(objMetodo.MET_Nombre)
                    && aux.MET_NombreIngles.Contains(objMetodo.MET_NombreIngles)
                    && aux.MET_Descripcion.Contains(objMetodo.MET_Descripcion)
                    && aux.MET_Estado == Constantes.EstadoActivo ,
                   aux => aux.MET_Codigo, true);

            }
            catch (Exception ex)
            {
                throw new Exception("Listar Metodo: " + ex.Message);
            }
        }


        public IList<Metodo> ListarMetodosTodos()
        {
            try
            {

                return MetodoDAL.Instancia.GetPaged(aux => 
                    aux.MET_Estado == Constantes.EstadoActivo,
                   aux => aux.MET_Codigo, true);

            }
            catch (Exception ex)
            {
                throw new Exception("Listar Metodo Todos: " + ex.Message);
            }
        }
    }
}
