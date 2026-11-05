using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.SqlClient;
using Dapper;
using System.Linq;

namespace SistemaVentas
{
    public class InventarioRespository
    {

        public List<Inventario> ConsultarInventarioCompleto()
        {
            try
            {
                using (var con = new ConexionDB().CrearConexion())
                {
                    string sql = @"SELECT IdProducto, NombreProducto, StockActual, StockMinimo
                                    FROM Productos";
                    return con.Query<Inventario>(sql).ToList();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Error de SQL al consultar inventario: {ex.Message}");
                return new List<Inventario>(); 
            }
        }

        public void RegistrarMovimiento(MovimientoInventario mov)
        {
            try
            {
                using (var con = new ConexionDB().CrearConexion())
                {
                    con.Open();

                    using (var transaccion = con.BeginTransaction())
                    {
                        string sqlMov = @"INSERT INTO MovimientosInventario (IdProducto, TipoMovimiento, Cantidad, FechaMovimiento, Referencia, Observacion, IdUsuario)
                                        VALUES (@IdProducto, @TipoMovimiento, @Cantidad, GETDATE(), @Referencia, @Observacion, @IdUsuario)";
                        con.Execute(sqlMov, mov, transaction: transaccion);

                        string sqlStock = "";
                        if (mov.TipoMovimiento == "ENTRADA")
                        {
                            sqlStock = "UPDATE Producto SET StockActual = StockActual + @Cantidad WHERE IdProducto = @IdProducto";
                        }
                        else if (mov.TipoMovimiento == "SALIDA")
                        {
                            sqlStock = "UPDATE Producto SET StockActual = StockActual - @Cantidad WHERE IdProducto = @IdProducto";
                        }
                        else if (mov.TipoMovimiento == "AJUSTE")
                        {
                            sqlStock = "UPDATE Producto SET StockActual = @Cantidad WHERE IdProducto = @IdProducto";
                        }

                        con.Execute(sqlStock, new { Cantidad = mov.Cantidad, IdProducto = mov.IdProducto }, transaction: transaccion);

                        transaccion.Commit();
                    }
                }
            }
            catch(SqlException ex)
            {
                throw new Exception($"Error: en base de datos al mover inventario: {ex.Message}");
            }
        }

        public List<Inventario> ObtenerAlertasStockBajo()
        {
            return ConsultarInventarioCompleto()
                .Where(propa => propa.StockActual <= propa.StockMinimo)
                .ToList();
        }
    }
}
