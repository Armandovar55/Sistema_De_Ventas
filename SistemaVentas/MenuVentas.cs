using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace SistemaVentas
{
    public static class MenuVentas
    {

        private static VentaRepository ventaRepo = new VentaRepository();

        public static void MenuVenta()
        {
            
            bool volver = false;
            while (!volver)
            {
                Console.Clear();
                Console.WriteLine("================================");
                Console.WriteLine("      MÓDULO DE VENTAS ");
                Console.WriteLine("================================");
                Console.WriteLine("1. Registrar una Venta (Nueva Venta)");
                Console.WriteLine("2. Consultar Historial de Ventas");
                Console.WriteLine("3. Volver al Menú Principal");
                Console.Write("\nSeleccione una opción: ");

                string opcion = Console.ReadLine();
                switch (opcion)
                {
                    case "1":
                        RegistrarVenta();
                        break;
                    case "2":
                        ConsultarVentas();
                        break; 
                    case "3":
                        volver = true; 
                        break;
                        Console.WriteLine("Opción no válida.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private static void RegistrarVenta()
        {

            Console.Clear();
            Console.WriteLine("=== REGISTRAR NUEVA VENTA ===");
            Venta nuevaVenta = new Venta();

            try
            {
                Console.Write("Ingrese Número de Venta/Factura (Ej: VEN-101): ");
                nuevaVenta.NumeroVenta = Console.ReadLine();
                Console.WriteLine("ID del CLiente: ");
                nuevaVenta.IdCliente = int.Parse(Console.ReadLine());
                Console.WriteLine("ID del Usuario Vendedor: ");
                nuevaVenta.IdUsuario = int.Parse(Console.ReadLine());

                // Agregae productos a la venta 
                bool agregarProductos = true;
                decimal subtotalAcumulado = 0;

                while (agregarProductos)
                {
                    Console.WriteLine("\n--- Agregar Producto a la Venta ---");
                    DetalleVenta item = new DetalleVenta();

                    Console.Write("ID del Producto: "); item.IdProducto = int.Parse(Console.ReadLine());
                    Console.Write("Nombre/Descripción del producto: "); item.NombreProducto = Console.ReadLine();
                    Console.Write("Cantidad a Vender "); item.Cantidad = int.Parse(Console.ReadLine());
                    Console.Write("Cantidad a Vender "); item.PrecioUnitario = decimal.Parse(Console.ReadLine());

                    item.Subtotal = item.Cantidad * item.PrecioUnitario;
                    subtotalAcumulado += item.Subtotal;

                    nuevaVenta.Detalle.Add(item);

                    Console.WriteLine("\n¿Desea agregar otro producto a esta venta? (S/N): ");
                    if (Console.ReadLine()?.ToUpper() != "S") agregarProductos = false;

                }

                // Calcular totales automaticamente
                nuevaVenta.Subtotal = subtotalAcumulado;

                Console.Write("\nIngrese porcentaje de descuento a aplicar (Ej: 5 para 5%, o 0 si ninguno): ");
                decimal porcDescuento = decimal.Parse(Console.ReadLine());
                nuevaVenta.Descuento = (nuevaVenta.Subtotal * porcDescuento) / 100;

                Console.Write("Ingrese porcentaje de impuesto (Ej: 13 para el 13%): ");
                decimal porcImpuesto = decimal.Parse(Console.ReadLine());
                nuevaVenta.Impuesto = ((nuevaVenta.Subtotal - nuevaVenta.Descuento) * porcImpuesto) / 100;

                nuevaVenta.Total = (nuevaVenta.Subtotal - nuevaVenta.Descuento) + nuevaVenta.Impuesto;

                // Se guarda la informacion 
                ventaRepo.RegistrarVentaCompleta(nuevaVenta);

                // Generar e imprimir comprobante en la pantalla
                ImprimirComprobante(nuevaVenta);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError al procesar la venta: {ex.Message}");
            }
            Console.ReadKey();
        }

        private static void ImprimirComprobante(Venta v)
        {
            Console.Clear();
            Console.WriteLine("===================================================");
            Console.WriteLine("      COMPROBANTE DE VENTA ");
            Console.WriteLine("===================================================");
            Console.WriteLine($"Factura N: {v.NumeroVenta}");
            Console.WriteLine($"Fecha: {DateTime.Now}");
            Console.WriteLine($"Cliente ID: {v.IdCliente} | Vendedor ID: {v.IdUsuario}");
            Console.WriteLine("----------------------------------------------------");
            Console.WriteLine("Cant         Descripcion       P.Unit       Total   ");
            Console.WriteLine("----------------------------------------------------");
            foreach (var d in v.Detalle)
            {
                // se le da formato para que los datos de la factura sea alinien
                string desc = d.NombreProducto.Length > 20 ? d.NombreProducto.Substring(0, 20) : d.NombreProducto.PadRight(20);
                Console.WriteLine($"{d.Cantidad.ToString()}.PadRight(6)){desc}${d.PrecioUnitario.ToString().PadRight(10)}${d.Subtotal}");
            }
            Console.WriteLine("----------------------------------------------------");
            Console.WriteLine($"SUBTOTAL:  ${v.Subtotal.ToString("N2")}");
            Console.WriteLine($"DESCUENTO:  ${v.Descuento.ToString("N2")}");
            Console.WriteLine($"IMPUESTO:  ${v.Impuesto.ToString("N2")}");
            Console.WriteLine("===================================================");
            Console.WriteLine($"TOTAL A PAGAR:    {v.Total.ToString("N2")}");
            Console.WriteLine("===================================================");
            Console.WriteLine("\n¡Venta registrada e inventario actualizado con éxito!");
        }

        private static void ConsultarVentas()
        {
            Console.Clear();
            Console.WriteLine("=== HISTORIAL DE VENTAS===\n");
            List<Venta> ventas = ventaRepo.ConsultasTodas();

            if (ventas.Count == 0)
            {
                Console.WriteLine("No se resgitran ventas en el sistema.");
            }
            else
            {
                Console.WriteLine("----------------------------------------------------");
                foreach (var v in ventas)
                {
                    Console.WriteLine($"ID: {v.IdVenta} | Factura: {v.NumeroVenta} | Fecha: {v.FechaVenta}");
                    Console.WriteLine($"Subtotal: ${v.Subtotal} | Desc: ${v.Descuento} | Imp: ${v.Impuesto} | Total: ${v.Total}");
                    Console.WriteLine("----------------------------------------------------");
                }
            }
            Console.WriteLine("\nPresione cualquier tecla para regresar...");
            Console.ReadKey();
        }
    }
}
