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
    public class UsuarioBL : Singleton<UsuarioBL>
    {
        private System.Linq.Expressions.Expression<Func<Usuario, bool>> where;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public Usuario Insertar(Usuario usuario)
        {
            try
            {
                return UsuarioDAL.Instancia.Add(usuario);
            }
            catch (Exception ex)
            {
                throw new Exception("Insertar usuario:"+ex.Message);
            }
        }

        /// <summary>
        /// Actualiza los datos del usuario
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public Usuario Actualizar(Usuario usuario)
        {
            try
            {
                return UsuarioDAL.Instancia.Update(usuario);
            }
            catch (Exception ex)
            {
                throw new Exception("Actualizar usuario:"+ex.Message);
            }
        }
        /// <summary>
        /// Eliminar los datos del usuario
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public Usuario Eliminar(Usuario usuario)
        {
            try
            {
                return UsuarioDAL.Instancia.Update(usuario);
            }
            catch (Exception ex)
            {
                throw new Exception("Eliminar usuario:" + ex.Message);
            }
        }
        /// <summary>
        /// Retorna los datos del usuario
        /// </summary>
        /// <param name="objUsuario"></param>
        /// <returns></returns>
        public IList<Usuario> obtenerDatos(Usuario objUsuario)
        {
            try
            {
                return UsuarioDAL.Instancia.GetFiltered(aux => aux.USU_Login == objUsuario.USU_Login && aux.USU_Clave == objUsuario.USU_Clave && aux.USU_Estado==Constantes.EstadoActivo);
                
            }
            catch (Exception ex)
            {
                throw new Exception("obtenerDatos Usuario: " + ex.Message);
            }
        }
        /// <summary>
        /// Obtiene los datos del usuario por ID
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <returns></returns>
        public Usuario ObtenerUsuarioById(Int32 idUsuario)
        {
            try
            {
                return UsuarioDAL.Instancia.Single(aux => aux.IDUsuario == idUsuario);

            }
            catch (Exception ex)
            {
                throw new Exception("obtener Usuario BYID: " + ex.Message);
            }
        }

        public IList<Usuario> ListarUsuarios(Usuario objUsuario)
        {
            try
            {
                //return UsuarioDAL.Instancia.GetPaged(aux => aux.USU_Login.Contains(objUsuario.USU_Login) && aux.IDRol == objUsuario.IDRol,
                //    aux=> aux.USU_Login,true,"Rol");
                return UsuarioDAL.Instancia.GetPaged(aux => 
                    aux.USU_Login.Contains(objUsuario.USU_Login) 
                    && aux.USU_Estado==Constantes.EstadoActivo
                    && (objUsuario.USU_IndicadorSignatario == "0" || aux.USU_IndicadorSignatario == objUsuario.USU_IndicadorSignatario)
                    && (objUsuario.IDRol==null || aux.IDRol==objUsuario.IDRol) ,
                   aux => aux.USU_Login, true, "Rol");

            }
            catch (Exception ex)
            {
                throw new Exception("Listar Usuario: " + ex.Message);
            }
        }
    }
}
