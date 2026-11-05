using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaVentas
{
    public class Cliente
    {
        public int IdCliente { get; set; }
        public string Identificacion { get; set; }
        public string NombreCompleto { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string Direccion { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}
