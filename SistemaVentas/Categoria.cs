using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaVentas
{
    public class Categoria
    {
        public int IdCategoria { get; set; }
        public string NombreCategoria { get; set; }
        public string Descripcion { get; set; }
        public bool Estado { get; set; }
    }
}
