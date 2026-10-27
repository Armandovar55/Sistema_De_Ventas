using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaVentas
{
    public abstract class DocumentoComercial
    {
        public int IdCompra { get; set; }

        public decimal Subtotal { get; set; }

        public decimal Impuesto { get; set; }

        public decimal Total { get; set; }
    }
}
