using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaVentas
{
    public class DetalleVenta
    {
        public int IdDetalleVenta { get; set; }

        public int IdVenta { get; set; }

        public int IdProducto { get; set; }

        public int Cantidad { get; set; }

        public decimal PrecioUnitario { get; set; }

        public decimal Subtotal { get; set; }


        public string NombreProducto { get; set; }
    }
}
