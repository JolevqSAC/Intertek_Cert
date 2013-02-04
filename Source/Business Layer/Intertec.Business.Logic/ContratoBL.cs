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
    public class ContratoBL:Singleton<ContratoBL>
    {
        private System.Linq.Expressions.Expression<Func<Contrato, bool>> where;
        
        /// <summary>
        /// Inserta un nuevo Contrato
        /// </summary>
        /// <param name="objContrato"></param>
        /// <returns>objContrato</returns>
        public Contrato Insertar(Contrato objContrato)
        {
            try
            {
                return ContratoDAL.Instancia.Add(objContrato);
            }
            catch (Exception ex)
            {
                throw new Exception("Contrato: Insertar" + ex.Message);
            }
        }
        /// <summary>
        /// Actualiza Contrato
        /// </summary>
        /// <param name="objContrato"></param>
        /// <returns>objContrato</returns>

        public Contrato Actualizar(Contrato objContrato)
        {
            try
            {
                return ContratoDAL.Instancia.Update(objContrato);
            }
            catch (Exception ex)
            {
                throw new Exception("Contrato: Actualizar" + ex.Message);
            }
        }

        /// <summary>
        /// Lista Contrato activos.
        /// </summary>
        /// <param name="objContrato"></param>
        /// <returns>objContrato</returns>
        public IList<Contrato> ListarTodosActivos(Contrato objContrato)
        {
            try
            {
                return ContratoDAL.Instancia.GetPaged(aux => 
                    aux.CON_Estado == Constantes.EstadoActivo, 
                    aux => aux.CON_Codigo, true,"Cliente");
            }
            catch (Exception ex)
            {
                throw new Exception("Contrato: Listar" + " " + ex.Message);
            }
        }

        /// <summary>
        /// Obtener Contrato por IDContrato.
        /// </summary>
        /// <param name="objContrato"></param>
        /// <returns>objContrato</returns>
        public Contrato ObtenerDatosPorID(int id)
        {
            try
            {
                return ContratoDAL.Instancia.Single(aux => aux.IDContrato == id);
            }
            catch (Exception ex)
            {
                throw new Exception("Contrato: Obtener Datos" + " " + ex.Message);
            }
        }

        /// <summary>
        /// Eliminacion Lógica Contrato- Estado=0
        /// </summary>
        /// <param name="objContrato"></param>
        /// <returns>objContrato</returns>


        public Contrato Eliminar(Contrato objContrato)
        {
            try
            {
                return ContratoDAL.Instancia.Update(objContrato);
            }
            catch (Exception ex)
            {
                throw new Exception("Contrato: Eliminar" + " " + ex.Message);
            }
        }

        /// <summary>
        /// Busca Contrato Activos en base filtros
        /// </summary>
        /// <param name="objContrato"></param>
        /// <returns>objContrato</returns>
        public IList<Contrato> BuscarPorFiltros(Contrato objContrato)
        {
            try
            {
                return ContratoDAL.Instancia.GetPaged(aux => 
                    (objContrato.IDCliente == null || aux.IDCliente == objContrato.IDCliente)
                    && aux.CON_Codigo.Contains(objContrato.CON_Codigo)
                    && aux.CON_Descripcion.Contains(objContrato.CON_Descripcion)
                    && (objContrato.CON_FechaInico==null || aux.CON_FechaInico>=objContrato.CON_FechaInico)
                    && (objContrato.CON_FechaFin==null || aux.CON_FechaFin<=objContrato.CON_FechaFin)
                    && aux.CON_NumReferencia.Contains(objContrato.CON_NumReferencia)
                    //&& (objContrato.CON_EstadoContrato == "0" || aux.CON_EstadoContrato == objContrato.CON_EstadoContrato)                  
                    && aux.CON_Estado == Constantes.EstadoActivo, aux=>aux.CON_Codigo,true,
                    "Cliente");
            }
            catch (Exception ex)
            {
                throw new Exception("Contrato: Listar" + " " + ex.Message);
            }
        }
        
    }
}
