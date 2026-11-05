using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaVentas
{
    public class Inventario
    {
        public int IdProducto { get; set; }

        public string NombreProducto { get; set; }

        public int StockActual {  get; set; }

        public int StockMinimo { get; set; }
    }
}
