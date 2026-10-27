using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaVentas
{
    public class DetalleCompra
    {
        public int IdProducto { get; set; }

        public int Cantidad { get; set; }

        public decimal precioCompra { get; set; }

        public decimal Subtotal { get; set; }
    }
}
