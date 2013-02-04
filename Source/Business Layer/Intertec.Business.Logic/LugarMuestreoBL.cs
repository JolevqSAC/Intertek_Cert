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
    public class LugarMuestreoBL:Singleton<LugarMuestreoBL>
    {
        private System.Linq.Expressions.Expression<Func<LugarMuestreo, bool>> where;
        
        /// <summary>
        /// Inserta un nuevo Lugar Muestreo
        /// </summary>
        /// <param name="objLugarMuestreo"></param>
        /// <returns>objLugarMuestreo</returns>
        public LugarMuestreo Insertar(LugarMuestreo objLugarMuestreo)
        {
            try
            {
                return LugarMuestreoDAL.Instancia.Add(objLugarMuestreo);
            }
            catch (Exception ex)
            {
                throw new Exception("Lugar Muestreo: Insertar" + ex.Message);
            }
        }
        /// <summary>
        /// Actualiza LugarMuestreo
        /// </summary>
        /// <param name="objLugarMuestreo"></param>
        /// <returns>objLugarMuestreo</returns>

        public LugarMuestreo Actualizar(LugarMuestreo objLugarMuestreo)
        {
            try
            {
                return LugarMuestreoDAL.Instancia.Update(objLugarMuestreo);
            }
            catch (Exception ex)
            {
                throw new Exception("Lugar Muestreo: Actualizar" + ex.Message);
            }
        }

        /// <summary>
        /// Lista Lugar Muestreo activos.
        /// </summary>
        /// <param name="objLugarMuestreo"></param>
        /// <returns>objLugarMuestreo</returns>
        public IList<LugarMuestreo> ListarTodosActivos(LugarMuestreo objLugarMuestreo)
        {
            try
            {
                return LugarMuestreoDAL.Instancia.GetPaged(aux => aux.LUM_Estado == objLugarMuestreo.LUM_Estado, aux => aux.IDLugarMuestreo, false,"Cliente");
            }
            catch (Exception ex)
            {
                throw new Exception("LugarMuestreo: Listar" + " " + ex.Message);
            }
        }

        /// <summary>
        /// Obtener Lugar Muestreo por IDLugarMuestreo.
        /// </summary>
        /// <param name="objLugarMuestreo"></param>
        /// <returns>objLugarMuestreo</returns>
        public LugarMuestreo ObtenerDatosPorID(LugarMuestreo objLugarMuestreo)
        {
            try
            {
                return LugarMuestreoDAL.Instancia.Single(aux => aux.IDLugarMuestreo == objLugarMuestreo.IDLugarMuestreo);
            }
            catch (Exception ex)
            {
                throw new Exception("LugarMuestreo: Obtener Datos" + " " + ex.Message);
            }
        }

        /// <summary>
        /// Eliminacion Lógica LugarMuestreo- Estado=0
        /// </summary>
        /// <param name="objLugarMuestreo"></param>
        /// <returns>objLugarMuestreo</returns>


        public LugarMuestreo Eliminar(LugarMuestreo objLugarMuestreo)
        {
            try
            {
                return LugarMuestreoDAL.Instancia.Update(objLugarMuestreo);
            }
            catch (Exception ex)
            {
                throw new Exception("LugarMuestreo: Eliminar" + " " + ex.Message);
            }
        }

        /// <summary>
        /// Busca LugarMuestreo Activos en base filtros
        /// </summary>
        /// <param name="objLugarMuestreo"></param>
        /// <returns>objLugarMuestreo</returns>
        public IList<LugarMuestreo> BuscarPorFiltros(LugarMuestreo objLugarMuestreo)
        {
            try
            {
                return LugarMuestreoDAL.Instancia.GetPaged(aux => (objLugarMuestreo.IDCliente == null || aux.IDCliente == objLugarMuestreo.IDCliente)
                    && aux.LUM_Direccion.Contains(objLugarMuestreo.LUM_Direccion)
                    && aux.LUM_Contacto.Contains(objLugarMuestreo.LUM_Contacto)
                    && aux.LUM_Telefono.Contains(objLugarMuestreo.LUM_Telefono)                    
                    && aux.LUM_Estado == objLugarMuestreo.LUM_Estado, aux=>aux.IDCliente,false,
                    "Cliente");
            }
            catch (Exception ex)
            {
                throw new Exception("Lugar Muestreo: Listar" + " " + ex.Message);
            }
        }
        
    }
}
