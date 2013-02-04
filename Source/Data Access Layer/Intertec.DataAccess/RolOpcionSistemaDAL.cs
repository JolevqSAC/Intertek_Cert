using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//agregamos la referencia al repositorio
using Intertek.DataAccess.Repository;
using Intertek.Business.Entities;
using Intertek.Helpers;

namespace Intertek.DataAccess
{
    public class RolOpcionSistemaDAL:Repository<RolOpcionSistemaDAL,RolOpcionSistema>
    {
        public List<RolOpcionSistema> ObtenerOpcionesSistema(RolOpcionSistema objOpciones)
        {
            ANALISIS_ENSAYOEntities ss = new ANALISIS_ENSAYOEntities();

            var resultado = (from RolOpcionesSistemas in ss.RolOpcionSistema.Include("OpcionSistema")
                             where RolOpcionesSistemas.IDRol == objOpciones.IDRol && RolOpcionesSistemas.ROS_Estado == Constantes.EstadoActivo
                             select RolOpcionesSistemas).ToList();
            
            return resultado;
        }
        /// <summary>
        /// Retorna la lista de las opciones del sistema activo
        /// </summary>
        /// <returns></returns>
        public IList<OpcionSistema> ListarOpcionesSistema()
        {
            ANALISIS_ENSAYOEntities ss = new ANALISIS_ENSAYOEntities();

            var resultado = (from OpcionSistema in ss.OpcionSistema where OpcionSistema.OSI_Estado== Constantes.EstadoActivo
                             select OpcionSistema).ToList();

            return resultado;
        }
    }
}
