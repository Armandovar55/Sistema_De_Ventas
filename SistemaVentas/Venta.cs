using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaVentas
{
    public class Venta
    {

        public int IdVenta { get; set; }
        
        public string NumeroVenta { get; set; }
            
        public DateTime FechaVenta { get; set; }
            
        public int IdCliente { get; set; }

        public int IdUsuario { get; set; }
        
        public decimal Subtotal { get; set; }

        public decimal Descuento { get; set; }

        public decimal Impuesto { get; set; }

        public decimal Total { get; set; }
        

        public List<DetalleVenta> Detalle { get; set; } = new List<DetalleVenta>();
    }
}
