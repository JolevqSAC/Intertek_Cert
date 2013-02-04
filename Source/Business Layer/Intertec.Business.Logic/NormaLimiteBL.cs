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
    public class NormaLimiteBL:Singleton<NormaLimiteBL>
    {
        /// <summary>
        /// Registra los datos del NormaLimite
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public NormaLimite Insertar(NormaLimite objNormaLimite)
        {
            try
            {
                return NormaLimiteDAL.Instancia.Add(objNormaLimite);
            }
            catch (Exception ex)
            {
                throw new Exception("Insertar NormaLimite:" + ex.Message);
            }
        }

        /// <summary>
        /// Actualiza los datos del NormaLimite
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public NormaLimite Actualizar(NormaLimite objNormaLimite)
        {
            try
            {
                return NormaLimiteDAL.Instancia.Update(objNormaLimite);
            }
            catch (Exception ex)
            {
                throw new Exception("Actualizar NormaLimite:" + ex.Message);
            }
        }
        /// <summary>
        /// Eliminar los datos de NormaLimite
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public NormaLimite Eliminar(NormaLimite objNormaLimite)
        {
            try
            {
                return NormaLimiteDAL.Instancia.Update(objNormaLimite);
                //return NormaLimiteDAL.Instancia.Delete(objNormaLimite);
            }
            catch (Exception ex)
            {
                throw new Exception("Eliminar NormaLimite:" + ex.Message);
            }
        }

        public int EliminarFisico(NormaLimite objNormaLimite)
        {
            try
            {
                return NormaLimiteDAL.Instancia.Delete(objNormaLimite);
            }
            catch (Exception ex)
            {
                throw new Exception("Eliminar Fisico NormaLimite:" + ex.Message);
            }
        }

        public void EliminarFisicoByIdNorma(Int32 idNorma)
        {
            try
            {
                List<NormaLimite> lstNormaLimite = ListarNormaLimites(idNorma).ToList();
                foreach (var objNormaLimite in lstNormaLimite)
                {
                    EliminarFisico(objNormaLimite);
                }
               
            }
            catch (Exception ex)
            {
                throw new Exception("Eliminar Fisico NormaLimite:" + ex.Message);
            }
        }
        /// <summary>
        /// Obtiene los datos del NormaLimite por ID
        /// </summary>
        /// <param name="idNormaLimite"></param>
        /// <returns></returns>
        public NormaLimite ObtenerNormaLimiteById(Int32 idNormaLimite)
        {
            try
            {
                return NormaLimiteDAL.Instancia.Single(aux => aux.IDNormaLimite == idNormaLimite);

            }
            catch (Exception ex)
            {
                throw new Exception("obtener NormaLimite BYID: " + ex.Message);
            }
        }

        public IList<NormaLimite> ListarNormaLimites(int idNorma)
        {
            try
            {

                return NormaLimiteDAL.Instancia.GetPaged(aux => aux.IDNorma == idNorma,
                   aux => aux.IDNorma, false,"Metodo","Ensayo");
                //return NormaLimiteDAL.Instancia.GetAll("","");
            }
            catch (Exception ex)
            {
                throw new Exception("Listar NormaLimite: " + ex.Message);
            }
        }
    }
}
