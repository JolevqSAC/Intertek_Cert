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
    public class PersonalBL:Singleton<PersonalBL>
    {
        /// <summary>
        /// Inserta el objeto Personal
        /// </summary>
        /// <param name="objPersonal"></param>
        /// <returns></returns>
        public Trabajador Insertar(Trabajador objPersonal)
        {
            try
            {
                return PersonalDAL.Instancia.Add(objPersonal);
            }
            catch(Exception ex)
            {
                throw new Exception("Insertar Trabajador", ex);
            }
        }
        /// <summary>
        /// Actualiza el objeto Trabajador
        /// </summary>
        /// <param name="objPersonal"></param>
        /// <returns></returns>
        public Trabajador Actualizar(Trabajador objPersonal)
        {
            try
            {
                return PersonalDAL.Instancia.Update(objPersonal);
            }
            catch (Exception ex)
            {
                throw new Exception("Actualizar Trabajador", ex);
            }
        }

        public Trabajador Eliminar(Trabajador objPersonal)
        {
            try
            {
                return PersonalDAL.Instancia.Update(objPersonal);
            }
            catch (Exception ex)
            {
                throw new Exception("Eliminar Trabajador", ex);
            }
        }
        /// <summary>
        /// Obtiene los dato del Trabajador
        /// </summary>
        /// <param name="idPersonal"></param>
        /// <returns></returns>
        public Trabajador ObtenerPersonalByID(Int32 idPersonal)
        {
            try
            {
                return PersonalDAL.Instancia.Single(aux=> aux.IDPersonal==idPersonal && aux.PER_Estado==Constantes.EstadoActivo);
            }
            catch (Exception ex)
            {
                throw new Exception("Obtener PersonalByID", ex);
            }
        }

        public IList<Trabajador> ListarPersonalPaginado(Trabajador objPersonal)
        {
            try
            {

                return PersonalDAL.Instancia.GetPaged(aux =>
                    aux.PER_Estado == Constantes.EstadoActivo
                    && aux.PER_Codigo.Contains(objPersonal.PER_Codigo)
                    && aux.PER_Nombres.Contains(objPersonal.PER_Nombres)
                    && aux.PER_Apellidos.Contains(objPersonal.PER_Apellidos)
                    && aux.PER_DNI.Contains(objPersonal.PER_DNI)
                    && (objPersonal.IDCargo == null || aux.IDCargo == objPersonal.IDCargo),
                    p => p.PER_Codigo, true, "Cargo");
            }
            catch (Exception ex)
            {
                throw new Exception("ListarTrabajador", ex);
            }
        }

        public IList<Trabajador> ListarPersonalTodos()
        {
            try
            {
                return PersonalDAL.Instancia.GetPaged(aux =>
                    aux.PER_Estado == Constantes.EstadoActivo,
                    aux => aux.PER_Codigo, true).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("ListarTrabajadorTodos", ex);
            }
        }
    }
}
