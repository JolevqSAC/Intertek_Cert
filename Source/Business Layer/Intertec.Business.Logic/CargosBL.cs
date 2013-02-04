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
    public class CargosBL:Singleton<CargosBL>
    {
        /// <summary>
        /// Inserta un nuevo cargo
        /// </summary>
        /// <param name="objCargo"></param>
        /// <returns>obj Cargo</returns>
        public Cargo Insertar(Cargo objCargo)
        {
            try
            {
                return CargosDAL.Instancia.Add(objCargo);

            }
            catch(Exception ex)
            {
                throw new Exception("Cargo: Insertar" + ex.Message);
            }
        }
        /// <summary>
        /// Retorna la lista de cargos
        /// </summary>
        /// <param name="objCargo">busca por nombre,descripcion</param>
        /// <returns></returns>
        public IList<Cargo> ListarCargos(Cargo objCargo)
        {
            try
            {
                return CargosDAL.Instancia.GetPaged(aux =>
                    (objCargo.CAR_Codigo==null || aux.CAR_Codigo.Contains(objCargo.CAR_Codigo))
                    && aux.CAR_Nombre.Contains(objCargo.CAR_Nombre)                    
                    && (objCargo.IDArea == null || aux.IDArea == objCargo.IDArea)
                    && aux.CAR_Estado==Constantes.EstadoActivo, aux =>aux.CAR_Codigo,true,"Area").ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Cargo: Listar " + ex.Message);
            }
        }

        /// <summary>
        /// Lista los cargos ordenados 
        /// </summary>
        /// <returns></returns>
        public IList<Cargo> ListarTodosCargos()
        {
            try
            {
                return CargosDAL.Instancia.GetPaged(aux => 
                    aux.CAR_Estado == Constantes.EstadoActivo, aux => aux.CAR_Codigo, true,"Area").ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("ListarTodosCargos", ex);
            }
        }
        /// <summary>
        /// Elimina el cargo
        /// </summary>
        /// <param name="objCargo">Actualiza el Estado a 0</param>
        /// <returns></returns>
        public Cargo Eliminar(Cargo objCargo)
        {
            try
            {
                return CargosDAL.Instancia.Update(objCargo);

            }
            catch (Exception ex)
            {
                throw new Exception("Cargo: Eliminar" + ex.Message);
            }
        }

        /// <summary>
        /// Elimina el cargo
        /// </summary>
        /// <param name="objCargo">Actualiza el Estado a 0</param>
        /// <returns></returns>
        public Cargo Actualizar(Cargo objCargo)
        {
            try
            {
                return CargosDAL.Instancia.Update(objCargo);

            }
            catch (Exception ex)
            {
                throw new Exception("Cargo: Actualizar" + ex.Message);
            }
        }
        /// <summary>
        /// Retorna el objeto Cargo 
        /// </summary>
        /// <param name="idCargo">Id del cargo</param>
        /// <returns></returns>
        public Cargo ObtenerCargoByID(int idCargo)
        {
            try
            {
                return CargosDAL.Instancia.Single(aux=> aux.IDCargo==idCargo && aux.CAR_Estado==Constantes.EstadoActivo);

            }
            catch (Exception ex)
            {
                throw new Exception("Cargo: Actualizar" + ex.Message);
            }
        }

        public List<Cargo> ListarxIdArea(int idarea)
        {
            try
            {
                return CargosDAL.Instancia.GetPaged(aux => aux.IDArea == idarea
                    && aux.CAR_Estado == Constantes.EstadoActivo, aux => aux.CAR_Codigo, true, "Area").ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Cargo: Listar " + ex.Message);
            }
        }


    }
}
