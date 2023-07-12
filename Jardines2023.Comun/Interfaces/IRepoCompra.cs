using Jardines2023.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jardines2023.Comun.Interfaces
{
    public interface IRepoCompra

    {
        void Agregar(Compra compra);
        void Editar(Compra compra);
        void Borrar(int compraID);
        bool Existe(Compra compra);
        int GetCantidad();
        List<Compra> GetCompra();
        List<Compra> GetClientesPorPagina(int registrosPorPagina, int paginaActual);
    }
}
