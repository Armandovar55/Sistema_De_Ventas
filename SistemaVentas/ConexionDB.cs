using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.SqlClient;

namespace SistemaVentas
{
    internal class ConexionDB
    {

        private readonly string cadenaConexion =
         "Server=localhost\\SQLEXPRESS; Database=DB_SistemaVentas; Trusted_Connection=True; TrustServerCertificate=True;";

        public SqlConnection CrearConexion()
        {
            return new SqlConnection(cadenaConexion);
        }
    }
}
