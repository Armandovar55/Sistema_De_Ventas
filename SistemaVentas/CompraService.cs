using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaVentas
{
    public class CompraService : ICalculable
    {
        private readonly CompraRepository repository; /* Hace falta crear el CompraRepository*/

        public CompraService()
        {
            repository = new CompraRepository();
        }

        public decimal CalcularTotal()
        {
            return 0;
        }
    }
}
