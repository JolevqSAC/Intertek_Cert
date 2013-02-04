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
    public class RolOpcionSistemaBL:Singleton<RolOpcionSistemaBL>
    {
        /// <summary>
        /// Retorna las opciones del sistema dependiendo del rol del usuario
        /// </summary>
        /// <param name="objOpciones"></param>
        /// <returns></returns>
        public IList<RolOpcionSistema> ObtenerOpciones_ByRol(RolOpcionSistema objOpciones)
        {
            
            try {
                return RolOpcionSistemaDAL.Instancia.GetFiltered(aux => aux.IDRol == objOpciones.IDRol);
            }
            catch (Exception ex)
            {
                throw new Exception("ObtenerOpciones_ByRol :" + ex.Message);
            }
        }

        public List<RolOpcionSistema> ObtenerOpcionesSistema(RolOpcionSistema objOpciones)
        {

            try
            {
                return RolOpcionSistemaDAL.Instancia.ObtenerOpcionesSistema(objOpciones);
            }
            catch (Exception ex)
            {
                throw new Exception("ObtenerOpciones_ByRol :" + ex.Message);
            }
        }

        public IList<RolOpcionSistema> ObtenerOpciones_ByRol2(RolOpcionSistema objOpciones)
        {

            try
            {
                return RolOpcionSistemaDAL.Instancia.GetFiltered(aux => aux.IDRol == objOpciones.IDRol, "OpcionSistema");
            }
            catch (Exception ex)
            {
                throw new Exception("ObtenerOpciones_ByRol :" + ex.Message);
            }
        }

        public IList<OpcionSistema> ListarOpciones()
        {
            try
            {
                return RolOpcionSistemaDAL.Instancia.ListarOpcionesSistema();
            }
            catch (Exception ex)
            {
                throw new Exception("ListarOpciones :" + ex.Message);
            }
        }

        public RolOpcionSistema Insertar(RolOpcionSistema rolOpcionSistema)
        {
            try
            {
                return RolOpcionSistemaDAL.Instancia.Add(rolOpcionSistema);
            }
            catch (Exception ex)
            {
                throw new Exception("Insertar :" + ex.Message);
            }
        }

        public void Eliminar(RolOpcionSistema rolOpcionSistema)
        {
            try
            {
                RolOpcionSistemaDAL.Instancia.Delete(rolOpcionSistema);
            }
            catch (Exception ex)
            {
                throw new Exception("Eliminar :" + ex.Message);
            }
        }
    }
}
