using System;
using System.Collections.Generic;
using System.Linq;
using Intertek.Business.Entities;
using Intertek.DataAccess;
using Intertek.Helpers;

namespace Intertek.Business.Logic
{
    public class RolBL : Singleton<RolBL>
    {
        /// <summary>
        /// agrega el rol con sus opciones
        /// </summary>
        /// <param name="objRol"></param>
        /// <returns></returns>
        public Rol Insertar(Rol objRol)
        {
            try
            {
                return RolDAL.Instancia.Add(objRol);
            }
            catch (Exception ex)
            {
                throw new Exception("Insertar Rol :" + ex.Message);
            }
        }

        /// <summary>
        /// Actualiza el rol
        /// </summary>
        /// <param name="objRol"></param>
        /// <returns></returns>
        public Rol Actualizar(Rol objRol, IEnumerable<RolOpcionSistema> opciones)
        {
            try
            {
                RolDAL.Instancia.Update(objRol);

                foreach (var rolOpcionSistema in objRol.RolOpcionSistema)
                {
                    RolOpcionSistemaBL.Instancia.Eliminar(rolOpcionSistema);
                }

                foreach (var rolOpcionSistema in opciones)
                {
                    rolOpcionSistema.IDRol = objRol.IDRol;
                    RolOpcionSistemaBL.Instancia.Insertar(rolOpcionSistema);
                }

                return objRol;
            }
            catch (Exception ex)
            {
                throw new Exception("Actualizar Rol :" + ex.Message);
            }
        }

        public Rol Actualizar(Rol objRol)
        {
            try
            {
                return RolDAL.Instancia.Update(objRol);
            }
            catch (Exception ex)
            {
                throw new Exception("Actualizar Rol :" + ex.Message);
            }
        }

        /// <summary>
        /// Obtiene la lista de Roles
        /// </summary>
        /// <param name="objRol"></param>
        /// <returns></returns>
        public IList<Rol> ListarRol(Rol objRol)
        {

            try
            {
                return
                    RolDAL.Instancia.GetFiltered(aux => 
                        aux.ROL_Codigo.Contains(objRol.ROL_Codigo)
                        && aux.ROL_Nombre.Contains(objRol.ROL_Nombre)
                        && aux.ROL_Descripcion.Contains(objRol.ROL_Descripcion) 
                        && aux.ROL_Estado == Constantes.EstadoActivo);
            }
            catch (Exception ex)
            {
                throw new Exception("Listar Roles :" + ex.Message);
            }
        }

        /// <summary>
        /// Obtiene la lista de Roles
        /// </summary>
        /// <param name="objRol"></param>
        /// <returns></returns>
        public IList<Rol> ListarRolTodos()
        {

            try
            {
                return RolDAL.Instancia.GetPaged(aux=> aux.ROL_Estado==Constantes.EstadoActivo,aux => aux.ROL_Nombre,true).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Listar Roles todos:" + ex.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objRol"></param>
        /// <returns></returns>
        public IList<Rol> ObtenerRolByID(Rol objRol)
        {
            try
            {
                return RolDAL.Instancia.GetFiltered(aux => aux.IDRol == objRol.IDRol);

            }
            catch (Exception ex)
            {
                throw new Exception("Obtener RolByID :" + ex.Message);
            }
        }

        public Rol ObtenerRolByID(int idRol)
        {
            try
            {
                return RolDAL.Instancia.Single(aux => aux.IDRol == idRol && aux.ROL_Estado == Constantes.EstadoActivo, "RolOpcionSistema");

            }
            catch (Exception ex)
            {
                throw new Exception("Obtener RolByID :" + ex.Message);
            }
        }

        public IList<Rol> ObtenerRolConOpcionesByID(Rol objRol)
        {
            try
            {
                return RolDAL.Instancia.GetFiltered(aux => aux.IDRol == objRol.IDRol && aux.ROL_Estado == Constantes.EstadoActivo, "RolOpcionSistema");
            }
            catch (Exception ex)
            {
                throw new Exception("Obtener RolByID :" + ex.Message);
            }
        }
    }
}
