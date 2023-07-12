using Jardines2023.Entidades.Dtos.Cliente;
using Jardines2023.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jardines2023.Servicios.Interfaces
{
    public interface IServicioCompra
    {
        int GetCantidad();
        bool Existe(Compra compra);
        void Guardar(Compra compra);
        void Borrar(int compraId);
        List<Compra> GetProveedoresPorPagina(int registrosPorPagina, int paginaActual, object textoFiltro);
    }
}
