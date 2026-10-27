using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaVentas
{
    public class Compra : DocumentoComercial
    {
        public int IdCompra {  get; set; }

        public string NumeroCompra { get; set; }

        public DateTime FechaCompra { get; set; }

        public int IdProveedor { get; set; }

        public int IdUsuario { get; set; }

        public decimal Subtotal { get; set; }

        public decimal Total { get; set; }

        public List<DetalleCompra> Detalles { get; set; }

    }
}
