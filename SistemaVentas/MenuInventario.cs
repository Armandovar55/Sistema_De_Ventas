using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaVentas
{
    public class MenuInventario
    {
        private static InventarioRespository repo = new InventarioRespository();

        public static void Inventario()
        {
            bool volver = false;
            while (!volver)
            {
                Console.Clear();
                Console.WriteLine("===============================");
                Console.WriteLine("    GESTION DE INVENTARIO");
                Console.WriteLine("===============================");
                Console.WriteLine("1. Consultar Inventario Actual");
                Console.WriteLine("2. Registrar Movimiento (Entrada/Salida)");
                Console.WriteLine("3. Realizar Ajuste de Inventario");
                Console.WriteLine("4. Alertas de Stock Mínimo");
                Console.WriteLine("5. volver al Menú Principal");
                Console.WriteLine("\nSeleccione una opción: ");

                string opcion = Console.ReadLine();
                switch (opcion)
                {
                    case "1":
                        ConsultarInventario();
                        break;
                    case "2":
                        RegistrarMovimiento();
                        break;
                    case "3":
                        AjustarInventario();
                        break;
                    case "4":
                        AlertasInventario();
                        break;
                    case "5":
                        volver = true;
                        break;
                    default:
                        Console.WriteLine("Opción invalida");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private static void ConsultarInventario()
        {
            Console.Clear();
            Console.WriteLine("=== INVENTARIO ACTUAL ===");
            var lista = repo.ConsultarInventarioCompleto();


            if (lista.Count == 0)
            {
                Console.WriteLine("\n El inventario se encuentra actualmente vacío.");
                Console.WriteLine("No hay productos registrados en la base de datos.");
            }
            else
            {

            
                Console.WriteLine("--------------------------------------------");
            foreach (var item in lista)
            {
                Console.WriteLine($"ID: {item.IdProducto} | Producto: {item.NombreProducto}");
                Console.WriteLine($"Stock Actual: {item.StockActual} | Stock Mínimo Sugerido {item.StockMinimo}");
                Console.WriteLine("--------------------------------------------");
            }
           }

            Console.WriteLine("\nPresione cualquier tecla para regresar...");
            Console.ReadKey();
          
        }

        private static void RegistrarMovimiento()
        {
            Console.Clear();
            Console.WriteLine("=== REGISTRAR MOVIMIENTO ===");
            MovimientoInventario mov = new MovimientoInventario();

            try
            {
                Console.WriteLine("Tipo Movimiento (1- ENTRADA, 2- SALIDA): ");
                string tipo = Console.ReadLine();
                mov.TipoMovimiento = (tipo == "1") ? "ENTRADA" : "SALIDA";

                Console.WriteLine("ID del Producto: "); mov.IdProducto = int.Parse(Console.ReadLine());
                Console.WriteLine("cantidad de unidades: "); mov.Cantidad = int.Parse(Console.ReadLine());
                Console.WriteLine("Referencia (Ej: Factura o código.): "); mov.Referencia = Console.ReadLine();
                Console.WriteLine("Obervación: "); mov.Observacion = Console.ReadLine();
                Console.WriteLine("ID del Usuario logeado: "); mov.IdUsuario = int.Parse(Console.ReadLine());

                repo.RegistrarMovimiento(mov);
                Console.WriteLine($"\n¡Movimiento de {mov.TipoMovimiento} registrado y stock actualziado con éxito!");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
            }
            Console.ReadKey();
        }

        private static void AjustarInventario()
        {
            Console.Clear();
            Console.WriteLine("=== REALIZAR AJUSTE FÍSICO  ===");
            MovimientoInventario mov = new MovimientoInventario();
            mov.TipoMovimiento = "AJUSTE";

            try
            {
                Console.WriteLine("ID del Producto a ajustar: "); mov.IdProducto = int.Parse(Console.ReadLine());
                Console.WriteLine("Ingrese el Stock FÍSICO REAL en almacén: "); mov.Cantidad = int.Parse(Console.ReadLine());
                Console.WriteLine("Referencia del Ajuste: "); mov.Referencia = Console.ReadLine();
                Console.WriteLine("Razón del Ajuste (Ej: Producto dañado / Pérdida): "); mov.Observacion = Console.ReadLine();
                Console.WriteLine("ID del Usuario Supervisor: "); mov.IdUsuario = int.Parse(Console.ReadLine());

                repo.RegistrarMovimiento(mov);
                Console.WriteLine("\n¡EL stock ha sido nivelado al ajuste físico con éxito!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            Console.ReadKey();
        }

        private static void AlertasInventario()
        {
            Console.Clear();
            Console.WriteLine("=== ALERTAS DE STOCK MÍNIMO  ===");
            var criticos = repo.ObtenerAlertasStockBajo();

            if (criticos.Count == 0)
            {
                Console.WriteLine("\n ¡Todo en orden! Todos los productos están bien abastecidos.");
            }
            else
            {
                Console.WriteLine("¡CUIDADO! Los siguientes productos requieren reabastecimiento urgente:");
                Console.WriteLine("--------------------------------------------------------------");
                foreach (var item in criticos)
                {
                    Console.WriteLine($"ID: {item.IdProducto} | Producto: {item.NombreProducto}");
                    Console.WriteLine($"STOCK CRÍTICO: {item.StockActual} (Mínimo requerido: {item.StockMinimo})");
                    Console.WriteLine("--------------------------------------------------------------");
                }
            }
            Console.ReadKey();
        }
    }
}
