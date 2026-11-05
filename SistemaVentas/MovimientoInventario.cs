using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaVentas
{
    public class MovimientoInventario
    {
        public int IdMovimiento {  get; set; }

        public int IdProducto { get; set; }

        public string TipoMovimiento { get; set; }

        public int Cantidad { get; set; }

        public DateTime FechaMovimiento { get; set; }

        public string Referencia { get; set; }

        public string Observacion { get; set; }

        public int IdUsuario { get; set; }
        
    }
}
