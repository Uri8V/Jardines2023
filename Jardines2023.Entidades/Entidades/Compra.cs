using Jardines2023.Entidades.Dtos.Proveedor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jardines2023.Entidades.Entidades
{
    public class Compra
    {
        public int CompraId { get; set; }
        public DateTime FechaCompra { get; set; }
        public int ProveedorId { get; set; }
        public decimal Total { get; set; }
        public Proveedor proveedor { get; set; }

    }
}
