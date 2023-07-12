using Jardines2023.Comun.Interfaces;
using Jardines2023.Comun.Repositorios;
using Jardines2023.Datos.Repositorios;
using Jardines2023.Entidades.Entidades;
using Jardines2023.Servicios.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jardines2023.Servicios.Servicios
{
    public class ServicioCompra:IServicioCompra
    {
        private readonly IRepoCompra repoCompra;
        private readonly IRepositorioProveedores repositorioProveedores;
        public ServicioCompra()
        {
            repoCompra = new RepositorioCompra();
            repositorioProveedores = new RepositorioProveedores();
        }

        public void Borrar(int compraId)
        {
            try
            {
                repoCompra.Borrar(compraId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Existe(Compra compra)
        {
            try
            {
                return repoCompra.Existe(compra);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int GetCantidad()
        {
            try
            {
                return repoCompra.GetCantidad();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Compra> GetProveedoresPorPagina(int registrosPorPagina, int paginaActual, object textoFiltro)
        {
            try
            {
                var lista = repoCompra.GetClientesPorPagina(registrosPorPagina, paginaActual);
                foreach (var item in lista)
                {
                    var proveedor = repositorioProveedores.GetProveedoresPorPagina(registrosPorPagina, paginaActual);
                    item.ProveedorId = item.ProveedorId;
                    
                }
                return lista;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public void Guardar(Compra compra)
        {
            try
            {
                if (compra.CompraId == 0)
                {
                    repoCompra.Agregar(compra);
                }
                else
                {
                    repoCompra.Editar(compra);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}
