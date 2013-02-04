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
    public class ProductoBL:Singleton<ProductoBL>
    {
        /// <summary>
        /// Registra los datos del Producto
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public Producto Insertar(Producto objProducto)
        {
            try
            {
                return ProductoDAL.Instancia.Add(objProducto);
            }
            catch (Exception ex)
            {
                throw new Exception("Insertar Producto:" + ex.Message);
            }
        }

        /// <summary>
        /// Actualiza los datos del Producto
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public Producto Actualizar(Producto objProducto)
        {
            try
            {
                return ProductoDAL.Instancia.Update(objProducto);
            }
            catch (Exception ex)
            {
                throw new Exception("Actualizar Producto:" + ex.Message);
            }
        }
        /// <summary>
        /// Eliminar los datos del Producto
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public Producto Eliminar(Producto objProducto)
        {
            try
            {
                return ProductoDAL.Instancia.Update(objProducto);
            }
            catch (Exception ex)
            {
                throw new Exception("Eliminar Producto:" + ex.Message);
            }
        }

        /// <summary>
        /// Obtiene los datos del producto por ID
        /// </summary>
        /// <param name="idProducto"></param>
        /// <returns></returns>
        public Producto ObtenerProductoById(Int32 idProducto)
        {
            try
            {
                return ProductoDAL.Instancia.Single(aux => aux.IDProducto == idProducto);

            }
            catch (Exception ex)
            {
                throw new Exception("obtener Producto BYID: " + ex.Message);
            }
        }

        public IList<Producto> ListarProductos(Producto objProducto)
        {
            try
            {

                return ProductoDAL.Instancia.GetPaged(aux => 
                    aux.PRO_Nombre.Contains(objProducto.PRO_Nombre) 
                    && aux.PRO_Estado == Constantes.EstadoActivo
                    && aux.PRO_Codigo.Contains(objProducto.PRO_Codigo) 
                    && aux.PRO_NombreIngles.Contains(objProducto.PRO_NombreIngles)
                    && (objProducto.IDCategoria == null || aux.IDCategoria == objProducto.IDCategoria) 
                    , aux => aux.PRO_Codigo, true,"CategoriaProducto", "UnidadMedida");

            }
            catch (Exception ex)
            {
                throw new Exception("Listar Productos: " + ex.Message);
            }
        }

        public IList<Producto> ListarProductosTodos()
        {
            try
            {

                return ProductoDAL.Instancia.GetPaged(aux => aux.PRO_Estado == Constantes.EstadoActivo , aux => aux.PRO_Nombre, true);

            }
            catch (Exception ex)
            {
                throw new Exception("Listar Productos Todos: " + ex.Message);
            }
        }
    }

}
