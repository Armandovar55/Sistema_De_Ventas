using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.SqlClient;


namespace SistemaVentas
{
    public class MenuProveedores
    {
        public static void MenuProveedor()
        {

            ProveedorRepository repo = new ProveedorRepository();
            bool continuar = true;

            while (continuar)
            {
                Console.Clear();

                Console.WriteLine("================================");
                Console.WriteLine("     PROVEEDORES");
                Console.WriteLine("================================");
                Console.WriteLine("1. Registrar");
                Console.WriteLine("2. Consultar");
                Console.WriteLine("3. Buscar");
                Console.WriteLine("4. Modificar");
                Console.WriteLine("5. Desactivar");
                Console.WriteLine("6. REGRESAR");


                Console.WriteLine("Por favore ingrese una opción: ");
                String opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        RegistrarProveedor();
                        break;
                    case "2":
                        ConsultarProveedores(repo);
                        break;

                    case "3":

                        break;
                    case "4":

                        break;
                    case "5":

                        break;
                    case "6":
                        Console.Clear();
                        Console.WriteLine("*** HAS SALIDO DEL SISTEMA DE VENTAS ***");
                        continuar = false;
                        break;
                    default:
                        Console.WriteLine("\nOpción no válida. Debes elegir un numero del 1 al 10");
                        break;
                }


            }

        }

        

        static void RegistrarProveedor()
        {
            try { 
                Proveedor proveedor = new Proveedor();

                Console.Clear();

                Console.Write("Empresa: ");
                proveedor.NombreEmpresa =
                    Console.ReadLine();

                Console.Write("Contacto: ");
                proveedor.NombreContacto =
                    Console.ReadLine();

                Console.Write("Telefono: ");
                proveedor.Telefono =
                    Console.ReadLine();

                Console.Write("Correo:");
                proveedor.Correo =
                    Console.ReadLine();

                Console.Write("Direccion:");
                proveedor.Direccion =
                    Console.ReadLine();

                proveedor.Estado = true;

                ProveedorRepository repo = 
                    new ProveedorRepository();

                repo.Insertar(proveedor);

                Console.WriteLine("\nProveedor registrado correctamente");

                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }

        }

        static void ConsultarProveedores(ProveedorRepository repo)
        {
            Console.Clear();
            Console.WriteLine("=== CONSULTA DE PROVEEDORES===");
            Console.WriteLine("Cargando datos desde la base de datos...\n");

            // Se recoge la lista desde el repositorio 
            List<Proveedor> lista = repo.ConsultarTodos();

            if (lista.Count == 0)
            {
                Console.WriteLine("No hay proveedores registrados  o la tabla está vacía.");
            }
            else
            {
                Console.WriteLine("-------------------------------------------------------");
                foreach (var prov in lista)
                {
                    string contacto = prov.NombreContacto ?? "N/A";
                    string telefono = prov.Telefono ?? "N/A";
                    string estado = prov.Estado ? "Activo" : "Inactivo";

                    Console.WriteLine($"ID: {prov.IdProveedor} | Empresa: {prov.NombreEmpresa}");
                    Console.WriteLine($"Contacto: {contacto} | Teléfono: {telefono} | Estado: {estado}");
                    Console.WriteLine("-------------------------------------------------------");
                }
            }

            Console.WriteLine("\nPresione cualqueir tecla para regrsar al menú principal...");
            Console.ReadKey();
        }



        /*Funcion de Buscar*/
        /*Funcion de Modificar*/
        /*Funcion de Desactivar*/
    }
}
