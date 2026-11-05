using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaVentas
{
    internal class MenuCompras
    {

        private static CompraRepository compraRepo = new CompraRepository();

        public static void ComprasMenu()
        {
            bool volver = false;
            while (!volver)
            {
                Console.Clear();
                Console.WriteLine("==========================");
                Console.WriteLine("   MÓDULO DE COMPRAS ");
                Console.WriteLine("==========================");
                Console.WriteLine("1. Registrar nueva compra");
                Console.WriteLine("2. Consultar historial de compras");
                Console.WriteLine("3. Volver al menú Principal");
                Console.WriteLine("\nSeleccione una opción: ");
                
                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        RegistrarCompra();
                        break;
                    case "2":
                        ConsultarCompras();
                        break;
                    case "3":
                        volver = true;
                        break;
                    default:
                        Console.WriteLine("Opción inválida. Intente de nuevo.");
                        Console.ReadLine();
                        break;
                }
            }
        }

        private static void RegistrarCompra()
        {
            Console.Clear();
            Console.WriteLine("=== REGISTRAR COMPRA ===");
            Compra nuevaCompra = new Compra();

            try
            {
                // se solicitan los datos
                Console.Write("Ingrese el número de factura/Compra (Ej: ADE-001): ");
                nuevaCompra.NumeroCompra = Console.ReadLine();

                Console.Write("ID del proveedor: ");
                nuevaCompra.IdProveedor = int.Parse(Console.ReadLine());

                Console.Write("ID del Usuario que registra: ");
                nuevaCompra.IdUsuario = int.Parse(Console.ReadLine());

                Console.Write("Subtotal: ");
                nuevaCompra.Subtotal = decimal.Parse(Console.ReadLine());

                Console.Write("Impuesto (IVA) (Ej: 13 para el 13%): ");
                decimal porcentajeImpuesto = decimal.Parse(Console.ReadLine());

                /* Se calcula el valor del impuesto en dinero: (Subtotal * Porcentaje) / 100;*/
                nuevaCompra.Impuesto = (nuevaCompra.Subtotal * porcentajeImpuesto) / 100;
                Console.WriteLine($"Monto de impuesto calculado ({porcentajeImpuesto}%): ${nuevaCompra.Impuesto}");

                /* Se realiza el calculo automáticamente para evitar 
                 errores del usuario*/
                nuevaCompra.Total = nuevaCompra.Subtotal + nuevaCompra.Impuesto;
                Console.WriteLine($"Total calculado automaticamente: {nuevaCompra.Total}");

                // Se guarda en la base de datos 
                compraRepo.Registrar(nuevaCompra);

                Console.WriteLine("\n¡Compra registrada exitosamente!");
            }
            catch (FormatException)
            {
                Console.WriteLine("\nError: Ingresaste un formato de número de ID inválido.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nOcurrió un propblema: {ex.Message}");
            }

            Console.WriteLine("\nPresione cualquier tecla para continuar...");
            Console.ReadKey();
        }

        private static void ConsultarCompras()
        {
            Console.Clear();
            Console.WriteLine("=== HISTORIAL DE COMPRAS ===\n");

            List<Compra> compras = compraRepo.ConsultarTodas();

            if (compras.Count == 0)
            {
                Console.WriteLine("No hay compras en el sistema.");
            }
            else
            {
                Console.WriteLine("-----------------------------------------------------");
                foreach (var c in compras)
                {
                    Console.WriteLine($"ID: {c.IdCompra} | Factura: {c.NumeroCompra} | Fecha: {c.FechaCompra}");
                    Console.WriteLine($"Proveedor ID: {c.IdProveedor} | Usuario ID: {c.IdUsuario}");
                    Console.WriteLine($"Subtotal: ${c.Subtotal} | Impuestp: ${c.Impuesto} | Total: ${c.Total}");
                    Console.WriteLine($"-----------------------------------------------------");
                    Console.WriteLine($"");
                }
            }

            Console.WriteLine("\nPresione cualquier tecla para regresar...");
            Console.ReadLine();
        }
    }
}
