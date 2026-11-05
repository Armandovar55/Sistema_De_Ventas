using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.SqlClient;
using Dapper;
using System.Linq;

namespace SistemaVentas
{
    public class VentaRepository
    {
        public void RegistrarVentaCompleta(Venta venta)
        {
            try 
            {
                using (var con = new ConexionDB().CrearConexion())
                {

                    con.Open();
                    using (var transaccion = con.BeginTransaction())
                    {
                        string sqlVenta = @"INSERT INTO Ventas (NumeroVenta, FechaVenta, IdCliente, IdUsuario, Subtotal, Descuento,
                                            Impuesto, Total) VALUES (@NumeroVenta, GETDATE(), @IdCliente, @IdUsuario, @Subtotal,
                                            @Descuento, @Impuesto, @Total)
                                            SELECT CAST(SCOPE_IDENTITY() as int);";

                        int idVentaGenerado = con.QuerySingle<int>(sqlVenta, venta, transaction: transaccion);
                     
                        // Procesar articulos en la cesta o carrito de compra pendientes de pago
                        foreach (var item in venta.Detalle)
                        {
                            item.IdVenta = idVentaGenerado;

                            // Insertar en la tabla de venta 
                            string sqlDetalle = @"INSERT INTO DetalleVentas (IdVenta, IdProducto, Cantidad, PrecioUnitario, Subtotal)
                                                    VALUES (@IdVenta, @IdProducto, @Cantidad, @PrecioUnitario, @Subtotal)";
                            con.Execute(sqlDetalle, item, transaction: transaccion);

                            //Se actualzia el inventario restando las unicades vendidas
                            string sqlStock = "UPDATE Productos SET StockActual = StockActual - @cantidad WHERE IdProducto = @IdProducto";
                            con.Execute(sqlStock, new { Cantidad = item.Cantidad, IdProducto = item.IdProducto }, transaction: transaccion);
                        }

                        // Si todas las trsansacciones y el inventario seactualizaron con éxito, se consolida la transacción
                        transaccion.Commit();

                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception($"Error de base de datos al procesar la venta y sus detalles: {ex.Message}");
            }

        }

        public List<Venta> ConsultasTodas()
        {
            try
            {
                using (var con = new ConexionDB().CrearConexion())
                {
                    string sql = "SELECT IdVenta, NumeroVenta, FechaVenta, IdCliente, IdUsuario, Subtotal, Descuento, Impuesto, Total FROM Ventas";
                    return con.Query<Venta>(sql).ToList();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Error de SQL al consultar historia {ex.Message}");
                return new List<Venta>();
            }
        }
    }
}
