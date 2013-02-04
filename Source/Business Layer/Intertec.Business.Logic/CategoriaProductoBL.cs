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
    public class CategoriaProductoBL: Singleton<CategoriaProductoBL>
    {
        /// <summary>
        /// Inserta Categoría
        /// </summary>
        /// <param name="objCategoria"></param>
        /// <returns>objCategoria</returns>
        public CategoriaProducto Insertar(CategoriaProducto objCategoria)
        {
            try
            {
                return CategoriaProductoDAL.Instancia.Add(objCategoria);

            }
            catch (Exception ex)
            {
                throw new Exception("Categoria Producto: Insertar" + ex.Message);
            }
        }
        /// <summary>
        /// Retorna la lista de Categorias
        /// </summary>
        /// <param name="objCategoria">busca por codigo, nombre,nombre en ingles</param>
        /// <returns></returns>
        public IList<CategoriaProducto> ListarCategoriaProductos(CategoriaProducto objCategoria)
        {
            try
            {
                return CategoriaProductoDAL.Instancia.GetPaged(aux => 
                    aux.CAT_Codigo.Contains(objCategoria.CAT_Codigo) 
                    && aux.CAT_Nombre.Contains(objCategoria.CAT_Nombre) 
                    && aux.CAT_NombreIngles.Contains(objCategoria.CAT_NombreIngles)
                    && (objCategoria.CAT_IndicadorArea == "0" || aux.CAT_IndicadorArea == objCategoria.CAT_IndicadorArea)
                    && aux.CAT_Estado == Constantes.EstadoActivo, aux => aux.CAT_Codigo, true);
            }
            catch (Exception ex)
            {
                throw new Exception("Categoria Producto: Listar " + ex.Message);
            }
        }

        /// <summary>
        /// Lista los Categorias ordenados 
        /// </summary>
        /// <returns></returns>
        public IList<CategoriaProducto> ListarTodosCategoriaProductos()
        {
            try
            {
                return CategoriaProductoDAL.Instancia.GetPaged(aux => aux.CAT_Estado == Constantes.EstadoActivo,
                                                  aux => aux.CAT_Codigo, true).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("ListarTodosCategoriaProductos", ex);
            }
        }
        /// <summary>
        /// Elimina el CategoriaProducto
        /// </summary>
        /// <param name="objCategoria">Actualiza el Estado a 0</param>
        /// <returns></returns>
        public CategoriaProducto Eliminar(CategoriaProducto objCategoria)
        {
            try
            {
                return CategoriaProductoDAL.Instancia.Update(objCategoria);

            }
            catch (Exception ex)
            {
                throw new Exception("Categoria Producto: Eliminar" + ex.Message);
            }
        }

        /// <summary>
        /// Elimina el Categorias
        /// </summary>
        /// <param name="objCategoria">Actualiza el Estado a 0</param>
        /// <returns></returns>
        public CategoriaProducto Actualizar(CategoriaProducto objCategoria)
        {
            try
            {
                return CategoriaProductoDAL.Instancia.Update(objCategoria);

            }
            catch (Exception ex)
            {
                throw new Exception("Categoria Producto: Actualizar" + ex.Message);
            }
        }
        /// <summary>
        /// Retorna el objeto Categorias
        /// </summary>
        /// <param name="idCategoria">Id del CategoriaProducto</param>
        /// <returns></returns>
        public CategoriaProducto ObtenerCategoriaProductoByID(int idCategoria)
        {
            try
            {
                return CategoriaProductoDAL.Instancia.Single(aux => aux.IDCategoria == idCategoria && aux.CAT_Estado == Constantes.EstadoActivo);

            }
            catch (Exception ex)
            {
                throw new Exception("CategoriaProducto: Actualizar" + ex.Message);
            }
        }
    }
}
