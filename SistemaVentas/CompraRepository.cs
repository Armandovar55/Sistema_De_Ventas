
using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Linq;

namespace SistemaVentas
{
    internal class CompraRepository
    {
        public void Registrar(Compra compra)
        {
            try
            {

                using (var conexion = new ConexionDB().CrearConexion())
                {
                    string sql = @"INSERT INTO Compras (NumeroCompra, FechaCompra, IdProveedor,
                        IdUsuario, Subtotal, Impuesto, Total) VALUES (@NumeroCompra, GETDATE(), @IdProveedor,
                        @IdUsuario, @Subtotal, @Impuesto, @Total)";

                    conexion.Execute(sql, compra);
                }
            }
            catch (SqlException ex)
            {
                throw new Exception($"Error de base de datos al registrar compra: { ex.Message }");
            }
        }

        // Metodo para consultar el historial de compras
        public List<Compra> ConsultarTodas()
        {
            try
            {
                using (var conexion = new ConexionDB().CrearConexion()) 
                {
                    string sql = "SELECT IdCompra, NumeroCompra, FechaCompra, IdProveedor, IdUsuario, Subtotal, Impuesto, Total   FROM Compras";
                    return conexion.Query<Compra>(sql).ToList();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Error de SQL al consultar compras: {ex.Message}");
                return new List<Compra>();
            }
        }
    }
}
