using Microsoft.Data.SqlClient;
using SistemaVentas;

class Program
{

    static void Main()
    {

        bool continuar = true;

        while (continuar)
        {
            Console.Clear();

            Console.WriteLine("========================================================");
            Console.WriteLine("     SISTEMA VENTAFLEX - Distribuidora La America");
            Console.WriteLine("========================================================");
            Console.WriteLine("1. Cliente");
            Console.WriteLine("2. Categorias");
            Console.WriteLine("3. Producto");
            Console.WriteLine("4. Proveedores");
            Console.WriteLine("5. Compras");
            Console.WriteLine("6. Inventario");
            Console.WriteLine("7. Ventas");
            Console.WriteLine("8. Pago");
            Console.WriteLine("9. Reportes");
            Console.WriteLine("10. SALIR DEL PROGRAMA");


            Console.WriteLine("Por favore ingrese una opción: ");
            String opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    GestionarClientes();
                    break;
                case "2":
                    Categorias();
                    break;
                case "3":
                    GestionarProductos();
                    break;
                case "4":
                    GestionarProveedores();
                    break;
                case "5":
                    MenuCompras.ComprasMenu();
                    break;
                case "6":
                    MenuInventario.Inventario();
                    break;
                case "7":
                    MenuVentas.MenuVenta();
                    break;
                case "10":
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

    static void GestionarClientes()
    {
        string conexion = "Server=localhost\\SQLEXPRESS; Database=DB_SistemaVentas; Trusted_Connection=True; TrustServerCertificate=True;";

        int opcionCliente = 0;

        while (opcionCliente != 6)
        {
            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine("MENU CLIENTES");
            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine("1. Registrar cliente");
            Console.WriteLine("2. Consultar todos los clientes");
            Console.WriteLine("3. Buscar cliente");
            Console.WriteLine("4. Modificar cliente");
            Console.WriteLine("5. Desactivar cliente");
            Console.WriteLine("6. Regresar");
            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine("Seleccione una opcion: ");
            opcionCliente = int.Parse(Console.ReadLine()!);

            if (opcionCliente == 1)
            {
                // REGISTRAR CLIENTE

                string sql = @"INSERT INTO Clientes
                                           (Identificacion, NombreCompleto, Telefono, Correo, Direccion)
                                           VALUES
                                           (@Identificacion, @NombreCompleto, @Telefono, @Correo, @Direccion);";

                using (SqlConnection con = new SqlConnection(conexion))
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        Console.WriteLine("-----------------------------------------------");
                        Console.WriteLine("REGISTRAR CLIENTE");
                        Console.WriteLine("-----------------------------------------------");

                        Console.WriteLine("Ingrese el numero de identificacion: ");
                        string identificacion = Console.ReadLine()!;

                        Console.WriteLine("Ingrese el nombre completo: ");
                        string nombre = Console.ReadLine()!;

                        Console.WriteLine("Ingrese el numero de telefono: ");
                        string telefono = Console.ReadLine()!;

                        Console.WriteLine("Ingrese el correo: ");
                        string correo = Console.ReadLine()!;

                        Console.WriteLine("Ingrese la direccion: ");
                        string direccion = Console.ReadLine()!;

                        cmd.Parameters.AddWithValue("@Identificacion", identificacion);
                        cmd.Parameters.AddWithValue("@NombreCompleto", nombre);
                        cmd.Parameters.AddWithValue("@Telefono", telefono);
                        cmd.Parameters.AddWithValue("@Correo", correo);
                        cmd.Parameters.AddWithValue("@Direccion", direccion);

                        cmd.ExecuteNonQuery();

                        Console.WriteLine("Cliente registrado correctamente");
                        Console.WriteLine("-----------------------------------------------");
                    }
                }
            }
            else if (opcionCliente == 2)
            {
                // CONSULTAR TODOS LOS CLIENTES

                string sql = @"SELECT IdCliente, Identificacion, NombreCompleto,
                                            Telefono, Correo, Direccion, Estado, FechaRegistro
                                            FROM Clientes;";

                using (SqlConnection con = new SqlConnection(conexion))
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        Console.WriteLine("-----------------------------------------------");
                        Console.WriteLine("LISTA DE CLIENTES");
                        Console.WriteLine("-----------------------------------------------");

                        using (SqlDataReader respuestaBD = cmd.ExecuteReader())
                        {
                            while (respuestaBD.Read())
                            {
                                Console.WriteLine("ID: " + respuestaBD["IdCliente"]);
                                Console.WriteLine("Identificacion: " + respuestaBD["Identificacion"]);
                                Console.WriteLine("Nombre: " + respuestaBD["NombreCompleto"]);
                                Console.WriteLine("Telefono: " + respuestaBD["Telefono"]);
                                Console.WriteLine("Correo: " + respuestaBD["Correo"]);
                                Console.WriteLine("Direccion: " + respuestaBD["Direccion"]);
                                Console.WriteLine("Estado: " + respuestaBD["Estado"]);
                                Console.WriteLine("Fecha de registro: " + respuestaBD["FechaRegistro"]);
                                Console.WriteLine("-----------------------------------------------");
                            }
                        }
                    }
                }
            }
            else if (opcionCliente == 3)
            {
                // BUSCAR CLIENTE

                string sql = @"SELECT IdCliente, Identificacion, NombreCompleto,
                                            Telefono, Correo, Direccion
                                            FROM Clientes
                                            WHERE Identificacion = @Identificacion;";

                using (SqlConnection con = new SqlConnection(conexion))
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        Console.WriteLine("-----------------------------------------------");
                        Console.WriteLine("BUSCAR CLIENTE");
                        Console.WriteLine("-----------------------------------------------");

                        Console.WriteLine("Ingrese el numero de identificacion del cliente: ");
                        string identificacion = Console.ReadLine()!;

                        cmd.Parameters.AddWithValue("@Identificacion", identificacion);

                        using (SqlDataReader respuestaBD = cmd.ExecuteReader())
                        {
                            if (respuestaBD.Read())
                            {
                                Console.WriteLine("ID: " + respuestaBD["IdCliente"]);
                                Console.WriteLine("Identificacion: " + respuestaBD["Identificacion"]);
                                Console.WriteLine("Nombre: " + respuestaBD["NombreCompleto"]);
                                Console.WriteLine("Telefono: " + respuestaBD["Telefono"]);
                                Console.WriteLine("Correo: " + respuestaBD["Correo"]);
                                Console.WriteLine("Direccion: " + respuestaBD["Direccion"]);
                            }
                            else
                            {
                                Console.WriteLine("No se encontro el cliente");
                            }

                            Console.WriteLine("-----------------------------------------------");
                        }
                    }
                }
            }
            else if (opcionCliente == 4)
            {
                // MODIFICAR CLIENTE

                string sql = @"UPDATE Clientes
                                            SET Identificacion = @Identificacion,
                                                NombreCompleto = @NombreCompleto,
                                                Telefono = @Telefono,
                                                Correo = @Correo,
                                                Direccion = @Direccion
                                            WHERE IdCliente = @IdCliente;";

                using (SqlConnection con = new SqlConnection(conexion))
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        Console.WriteLine("-----------------------------------------------");
                        Console.WriteLine("MODIFICAR CLIENTE");
                        Console.WriteLine("-----------------------------------------------");

                        Console.WriteLine("Ingrese el ID del cliente que desea modificar: ");
                        int id = int.Parse(Console.ReadLine()!);

                        Console.WriteLine("Ingrese el nuevo numero de identificacion: ");
                        string identificacion = Console.ReadLine()!;

                        Console.WriteLine("Ingrese el nuevo nombre completo: ");
                        string nombre = Console.ReadLine()!;

                        Console.WriteLine("Ingrese el nuevo numero de telefono: ");
                        string telefono = Console.ReadLine()!;

                        Console.WriteLine("Ingrese el nuevo correo: ");
                        string correo = Console.ReadLine()!;

                        Console.WriteLine("Ingrese la nueva direccion: ");
                        string direccion = Console.ReadLine()!;

                        cmd.Parameters.AddWithValue("@IdCliente", id);
                        cmd.Parameters.AddWithValue("@Identificacion", identificacion);
                        cmd.Parameters.AddWithValue("@NombreCompleto", nombre);
                        cmd.Parameters.AddWithValue("@Telefono", telefono);
                        cmd.Parameters.AddWithValue("@Correo", correo);
                        cmd.Parameters.AddWithValue("@Direccion", direccion);

                        int resultado = cmd.ExecuteNonQuery();

                        if (resultado > 0)
                        {
                            Console.WriteLine("Cliente modificado correctamente");
                        }
                        else
                        {
                            Console.WriteLine("No se encontro el cliente");
                        }

                        Console.WriteLine("-----------------------------------------------");
                    }
                }
            }
            else if (opcionCliente == 5)
            {
                // DESACTIVAR CLIENTE

                string sql = @"UPDATE Clientes
                                            SET Estado = 0
                                            WHERE IdCliente = @IdCliente;";

                using (SqlConnection con = new SqlConnection(conexion))
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        Console.WriteLine("-----------------------------------------------");
                        Console.WriteLine("DESACTIVAR CLIENTE");
                        Console.WriteLine("-----------------------------------------------");

                        Console.WriteLine("Ingrese el ID del cliente que desea desactivar: ");
                        int id = int.Parse(Console.ReadLine()!);

                        cmd.Parameters.AddWithValue("@IdCliente", id);

                        int resultado = cmd.ExecuteNonQuery();

                        if (resultado > 0)
                        {
                            Console.WriteLine("Cliente desactivado correctamente");
                        }
                        else
                        {
                            Console.WriteLine("No se encontro el cliente");
                        }

                        Console.WriteLine("-----------------------------------------------");
                    }
                }
            }
            else if (opcionCliente != 6)
            {
                Console.WriteLine("Opcion no valida");
            }
        }
    }

    static void Categorias()
    {

        string conexion = "Server=localhost\\SQLEXPRESS; Database=DB_SistemaVentas; Trusted_Connection=True; TrustServerCertificate=True;";

        int opcionCategoria = 0;

        while (opcionCategoria != 4)
        {
            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine("MENU CATEGORIAS");
            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine("1. Registrar categoria");
            Console.WriteLine("2. Consultar todas las categorias");
            Console.WriteLine("3. Modificar categoria");
            Console.WriteLine("4. Regresar");
            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine("Seleccione una opcion: ");
            opcionCategoria = int.Parse(Console.ReadLine()!);

            if (opcionCategoria == 1)
            {
                // REGISTRAR CATEGORIA

                string sql = @"INSERT INTO Categorias
                                            (NombreCategoria, Descripcion)
                                            VALUES
                                            (@NombreCategoria, @Descripcion);";

                using (SqlConnection con = new SqlConnection(conexion))
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        Console.WriteLine("-----------------------------------------------");
                        Console.WriteLine("REGISTRAR CATEGORIA");
                        Console.WriteLine("-----------------------------------------------");

                        Console.WriteLine("Ingrese el nombre de la categoria: ");
                        string nombre = Console.ReadLine()!;

                        Console.WriteLine("Ingrese la descripcion: ");
                        string descripcion = Console.ReadLine()!;

                        cmd.Parameters.AddWithValue("@NombreCategoria", nombre);
                        cmd.Parameters.AddWithValue("@Descripcion", descripcion);

                        cmd.ExecuteNonQuery();

                        Console.WriteLine("Categoria registrada correctamente");
                        Console.WriteLine("-----------------------------------------------");
                    }
                }
            }
            else if (opcionCategoria == 2)
            {
                // CONSULTAR TODAS LAS CATEGORIAS

                string sql = @"SELECT IdCategoria, NombreCategoria, Descripcion, Estado
                                            FROM Categorias;";

                using (SqlConnection con = new SqlConnection(conexion))
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        Console.WriteLine("-----------------------------------------------");
                        Console.WriteLine("LISTA DE CATEGORIAS");
                        Console.WriteLine("-----------------------------------------------");

                        using (SqlDataReader respuestaBD = cmd.ExecuteReader())
                        {
                            while (respuestaBD.Read())
                            {
                                Console.WriteLine("ID: " + respuestaBD["IdCategoria"]);
                                Console.WriteLine("Nombre: " + respuestaBD["NombreCategoria"]);
                                Console.WriteLine("Descripcion: " + respuestaBD["Descripcion"]);
                                Console.WriteLine("Estado: " + respuestaBD["Estado"]);
                                Console.WriteLine("-----------------------------------------------");
                            }
                        }
                    }
                }
            }
            else if (opcionCategoria == 3)
            {
                // MODIFICAR CATEGORIA

                string sql = @"UPDATE Categorias
                                            SET NombreCategoria = @NombreCategoria,
                                                Descripcion = @Descripcion
                                            WHERE IdCategoria = @IdCategoria;";

                using (SqlConnection con = new SqlConnection(conexion))
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        Console.WriteLine("-----------------------------------------------");
                        Console.WriteLine("MODIFICAR CATEGORIA");
                        Console.WriteLine("-----------------------------------------------");

                        Console.WriteLine("Ingrese el ID de la categoria que desea modificar: ");
                        int id = int.Parse(Console.ReadLine()!);

                        Console.WriteLine("Ingrese el nuevo nombre de la categoria: ");
                        string nombre = Console.ReadLine()!;

                        Console.WriteLine("Ingrese la nueva descripcion: ");
                        string descripcion = Console.ReadLine()!;

                        cmd.Parameters.AddWithValue("@IdCategoria", id);
                        cmd.Parameters.AddWithValue("@NombreCategoria", nombre);
                        cmd.Parameters.AddWithValue("@Descripcion", descripcion);

                        int resultado = cmd.ExecuteNonQuery();

                        if (resultado > 0)
                        {
                            Console.WriteLine("Categoria modificada correctamente");
                        }
                        else
                        {
                            Console.WriteLine("No se encontro la categoria");
                        }

                        Console.WriteLine("-----------------------------------------------");
                    }
                }
            }
            else if (opcionCategoria != 4)
            {
                Console.WriteLine("Opcion no valida");
            }
        }
    }

    static void GestionarProductos()
    {

        string conexion = "Server=localhost\\SQLEXPRESS; Database=DB_SistemaVentas; Trusted_Connection=True; TrustServerCertificate=True;";
        int opcionProducto = 0;

        while (opcionProducto != 7)
        {
            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine("MENU PRODUCTOS");
            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine("1. Registrar producto");
            Console.WriteLine("2. Consultar todos los productos");
            Console.WriteLine("3. Buscar producto");
            Console.WriteLine("4. Modificar producto");
            Console.WriteLine("5. Desactivar producto");
            Console.WriteLine("6. Consultar existencias");
            Console.WriteLine("7. Regresar");
            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine("Seleccione una opcion: ");
            opcionProducto = int.Parse(Console.ReadLine()!);

            if (opcionProducto == 1)
            {
                // REGISTRAR PRODUCTO

                string sql = @"INSERT INTO Productos
                                            (CodigoProducto, NombreProducto, Descripcion, PrecioCompra,
                                            PrecioVenta, StockActual, StockMinimo, IdCategoria)
                                            VALUES
                                            (@CodigoProducto, @NombreProducto, @Descripcion, @PrecioCompra,
                                            @PrecioVenta, @StockActual, @StockMinimo, @IdCategoria);";

                using (SqlConnection con = new SqlConnection(conexion))
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        Console.WriteLine("-----------------------------------------------");
                        Console.WriteLine("REGISTRAR PRODUCTO");
                        Console.WriteLine("-----------------------------------------------");

                        Console.WriteLine("Ingrese el codigo del producto: ");
                        string codigo = Console.ReadLine()!;

                        Console.WriteLine("Ingrese el nombre del producto: ");
                        string nombre = Console.ReadLine()!;

                        Console.WriteLine("Ingrese la descripcion: ");
                        string descripcion = Console.ReadLine()!;

                        Console.WriteLine("Ingrese el precio de compra: ");
                        decimal precioCompra = decimal.Parse(Console.ReadLine()!);

                        Console.WriteLine("Ingrese el precio de venta: ");
                        decimal precioVenta = decimal.Parse(Console.ReadLine()!);

                        Console.WriteLine("Ingrese el stock actual: ");
                        int stockActual = int.Parse(Console.ReadLine()!);

                        Console.WriteLine("Ingrese el stock minimo: ");
                        int stockMinimo = int.Parse(Console.ReadLine()!);

                        Console.WriteLine("Ingrese el ID de la categoria: ");
                        int idCategoria = int.Parse(Console.ReadLine()!);

                        if (precioCompra > 0 && precioVenta > 0 &&
                            stockActual >= 0 && stockMinimo >= 0)
                        {
                            cmd.Parameters.AddWithValue("@CodigoProducto", codigo);
                            cmd.Parameters.AddWithValue("@NombreProducto", nombre);
                            cmd.Parameters.AddWithValue("@Descripcion", descripcion);
                            cmd.Parameters.AddWithValue("@PrecioCompra", precioCompra);
                            cmd.Parameters.AddWithValue("@PrecioVenta", precioVenta);
                            cmd.Parameters.AddWithValue("@StockActual", stockActual);
                            cmd.Parameters.AddWithValue("@StockMinimo", stockMinimo);
                            cmd.Parameters.AddWithValue("@IdCategoria", idCategoria);

                            cmd.ExecuteNonQuery();

                            Console.WriteLine("Producto registrado correctamente");
                        }
                        else
                        {
                            Console.WriteLine("Los precios deben ser mayores que cero y el stock no puede ser negativo");
                        }

                        Console.WriteLine("-----------------------------------------------");
                    }
                }
            }
            else if (opcionProducto == 2)
            {
                // CONSULTAR TODOS LOS PRODUCTOS

                string sql = @"SELECT P.IdProducto, P.CodigoProducto, P.NombreProducto,
                                            P.Descripcion, P.PrecioCompra, P.PrecioVenta,
                                            P.StockActual, P.StockMinimo, C.NombreCategoria, P.Estado
                                            FROM Productos P
                                            INNER JOIN Categorias C ON P.IdCategoria = C.IdCategoria;";

                using (SqlConnection con = new SqlConnection(conexion))
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        Console.WriteLine("-----------------------------------------------");
                        Console.WriteLine("LISTA DE PRODUCTOS");
                        Console.WriteLine("-----------------------------------------------");

                        using (SqlDataReader respuestaBD = cmd.ExecuteReader())
                        {
                            while (respuestaBD.Read())
                            {
                                Console.WriteLine("ID: " + respuestaBD["IdProducto"]);
                                Console.WriteLine("Codigo: " + respuestaBD["CodigoProducto"]);
                                Console.WriteLine("Nombre: " + respuestaBD["NombreProducto"]);
                                Console.WriteLine("Descripcion: " + respuestaBD["Descripcion"]);
                                Console.WriteLine("Precio de compra: " + respuestaBD["PrecioCompra"]);
                                Console.WriteLine("Precio de venta: " + respuestaBD["PrecioVenta"]);
                                Console.WriteLine("Stock actual: " + respuestaBD["StockActual"]);
                                Console.WriteLine("Stock minimo: " + respuestaBD["StockMinimo"]);
                                Console.WriteLine("Categoria: " + respuestaBD["NombreCategoria"]);
                                Console.WriteLine("Estado: " + respuestaBD["Estado"]);
                                Console.WriteLine("-----------------------------------------------");
                            }
                        }
                    }
                }
            }
            else if (opcionProducto == 3)
            {
                // BUSCAR PRODUCTO

                string sql = @"SELECT IdProducto, CodigoProducto, NombreProducto,
                                            Descripcion, PrecioCompra, PrecioVenta,
                                            StockActual, StockMinimo, IdCategoria
                                            FROM Productos
                                            WHERE CodigoProducto = @CodigoProducto;";

                using (SqlConnection con = new SqlConnection(conexion))
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        Console.WriteLine("-----------------------------------------------");
                        Console.WriteLine("BUSCAR PRODUCTO");
                        Console.WriteLine("-----------------------------------------------");

                        Console.WriteLine("Ingrese el codigo del producto: ");
                        string codigo = Console.ReadLine()!;

                        cmd.Parameters.AddWithValue("@CodigoProducto", codigo);

                        using (SqlDataReader respuestaBD = cmd.ExecuteReader())
                        {
                            if (respuestaBD.Read())
                            {
                                Console.WriteLine("ID: " + respuestaBD["IdProducto"]);
                                Console.WriteLine("Codigo: " + respuestaBD["CodigoProducto"]);
                                Console.WriteLine("Nombre: " + respuestaBD["NombreProducto"]);
                                Console.WriteLine("Descripcion: " + respuestaBD["Descripcion"]);
                                Console.WriteLine("Precio de compra: " + respuestaBD["PrecioCompra"]);
                                Console.WriteLine("Precio de venta: " + respuestaBD["PrecioVenta"]);
                                Console.WriteLine("Stock actual: " + respuestaBD["StockActual"]);
                                Console.WriteLine("Stock minimo: " + respuestaBD["StockMinimo"]);
                                Console.WriteLine("ID de categoria: " + respuestaBD["IdCategoria"]);
                            }
                            else
                            {
                                Console.WriteLine("No se encontro el producto");
                            }

                            Console.WriteLine("-----------------------------------------------");
                        }
                    }
                }
            }
            else if (opcionProducto == 4)
            {
                // MODIFICAR PRODUCTO

                string sql = @"UPDATE Productos
                                            SET CodigoProducto = @CodigoProducto,
                                                NombreProducto = @NombreProducto,
                                                Descripcion = @Descripcion,
                                                PrecioCompra = @PrecioCompra,
                                                PrecioVenta = @PrecioVenta,
                                                StockActual = @StockActual,
                                                StockMinimo = @StockMinimo,
                                            WHERE IdProducto = @IdProducto;";

                using (SqlConnection con = new SqlConnection(conexion))
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        Console.WriteLine("-----------------------------------------------");
                        Console.WriteLine("MODIFICAR PRODUCTO");
                        Console.WriteLine("-----------------------------------------------");

                        Console.WriteLine("Ingrese el ID del producto que desea modificar: ");
                        int id = int.Parse(Console.ReadLine()!);

                        Console.WriteLine("Ingrese el nuevo codigo: ");
                        string codigo = Console.ReadLine()!;

                        Console.WriteLine("Ingrese el nuevo nombre: ");
                        string nombre = Console.ReadLine()!;

                        Console.WriteLine("Ingrese la nueva descripcion: ");
                        string descripcion = Console.ReadLine()!;

                        Console.WriteLine("Ingrese el nuevo precio de compra: ");
                        decimal precioCompra = decimal.Parse(Console.ReadLine()!);

                        Console.WriteLine("Ingrese el nuevo precio de venta: ");
                        decimal precioVenta = decimal.Parse(Console.ReadLine()!);

                        Console.WriteLine("Ingrese el nuevo stock actual: ");
                        int stockActual = int.Parse(Console.ReadLine()!);

                        Console.WriteLine("Ingrese el nuevo stock minimo: ");
                        int stockMinimo = int.Parse(Console.ReadLine()!);

                        if (precioCompra > 0 && precioVenta > 0 &&
                            stockActual >= 0 && stockMinimo >= 0)
                        {
                            cmd.Parameters.AddWithValue("@IdProducto", id);
                            cmd.Parameters.AddWithValue("@CodigoProducto", codigo);
                            cmd.Parameters.AddWithValue("@NombreProducto", nombre);
                            cmd.Parameters.AddWithValue("@Descripcion", descripcion);
                            cmd.Parameters.AddWithValue("@PrecioCompra", precioCompra);
                            cmd.Parameters.AddWithValue("@PrecioVenta", precioVenta);
                            cmd.Parameters.AddWithValue("@StockActual", stockActual);
                            cmd.Parameters.AddWithValue("@StockMinimo", stockMinimo);


                            int resultado = cmd.ExecuteNonQuery();

                            if (resultado > 0)
                            {
                                Console.WriteLine("Producto modificado correctamente");
                            }
                            else
                            {
                                Console.WriteLine("No se encontro el producto");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Los precios deben ser mayores que cero y el stock no puede ser negativo");
                        }

                        Console.WriteLine("-----------------------------------------------");
                    }
                }
            }
            else if (opcionProducto == 5)
            {
                // DESACTIVAR PRODUCTO

                string sql = @"UPDATE Productos
                                            SET Estado = 0
                                            WHERE IdProducto = @IdProducto;";

                using (SqlConnection con = new SqlConnection(conexion))
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        Console.WriteLine("-----------------------------------------------");
                        Console.WriteLine("DESACTIVAR PRODUCTO");
                        Console.WriteLine("-----------------------------------------------");

                        Console.WriteLine("Ingrese el ID del producto que desea desactivar: ");
                        int id = int.Parse(Console.ReadLine()!);

                        cmd.Parameters.AddWithValue("@IdProducto", id);

                        int resultado = cmd.ExecuteNonQuery();

                        if (resultado > 0)
                        {
                            Console.WriteLine("Producto desactivado correctamente");
                        }
                        else
                        {
                            Console.WriteLine("No se encontro el producto");
                        }

                        Console.WriteLine("-----------------------------------------------");
                    }
                }
            }
            else if (opcionProducto == 6)
            {
                // CONSULTAR EXISTENCIAS

                string sql = @"SELECT CodigoProducto, NombreProducto, StockActual, StockMinimo
                                            FROM Productos
                                            WHERE Estado = 1;";

                using (SqlConnection con = new SqlConnection(conexion))
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        Console.WriteLine("-----------------------------------------------");
                        Console.WriteLine("CONSULTAR EXISTENCIAS");
                        Console.WriteLine("-----------------------------------------------");

                        using (SqlDataReader respuestaBD = cmd.ExecuteReader())
                        {
                            while (respuestaBD.Read())
                            {
                                Console.WriteLine("Codigo: " + respuestaBD["CodigoProducto"]);
                                Console.WriteLine("Producto: " + respuestaBD["NombreProducto"]);
                                Console.WriteLine("Stock actual: " + respuestaBD["StockActual"]);
                                Console.WriteLine("Stock minimo: " + respuestaBD["StockMinimo"]);

                                int stock = Convert.ToInt32(respuestaBD["StockActual"]);
                                int minimo = Convert.ToInt32(respuestaBD["StockMinimo"]);

                                if (stock <= minimo)
                                {
                                    Console.WriteLine("ALERTA: El producto tiene stock bajo");
                                }

                                Console.WriteLine("-----------------------------------------------");
                            }
                        }
                    }
                }
            }
            else if (opcionProducto != 7)
            {
                Console.WriteLine("Opcion no valida");
            }
        }
    }

    static void GestionarProveedores()
    {
        string conexion = "Server=localhost\\SQLEXPRESS; Database=DB_SistemaVentas; Trusted_Connection=True; TrustServerCertificate=True;";

        int opcionProveedor = 0;

        while (opcionProveedor != 4)
        {
            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine("MENU PROVEEDORES");
            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine("1. Registrar proveedor");
            Console.WriteLine("2. Consultar todos los proveedores");
            Console.WriteLine("3. Modificar proveedor");
            Console.WriteLine("4. Regresar");
            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine("Seleccione una opcion: ");
            opcionProveedor = int.Parse(Console.ReadLine()!);

            if (opcionProveedor == 1)
            {
                // REGISTRAR PROVEEDOR

                string sql = @"INSERT INTO Proveedores
                                            (NombreEmpresa, NombreContacto, Telefono, Correo, Direccion)
                                            VALUES
                                            (@NombreEmpresa, @NombreContacto, @Telefono, @Correo, @Direccion);";

                using (SqlConnection con = new SqlConnection(conexion))
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        Console.WriteLine("-----------------------------------------------");
                        Console.WriteLine("REGISTRAR PROVEEDOR");
                        Console.WriteLine("-----------------------------------------------");

                        Console.WriteLine("Ingrese el nombre de la empresa: ");
                        string empresa = Console.ReadLine()!;

                        Console.WriteLine("Ingrese el nombre del contacto: ");
                        string contacto = Console.ReadLine()!;

                        Console.WriteLine("Ingrese el telefono: ");
                        string telefono = Console.ReadLine()!;

                        Console.WriteLine("Ingrese el correo: ");
                        string correo = Console.ReadLine()!;

                        Console.WriteLine("Ingrese la direccion: ");
                        string direccion = Console.ReadLine()!;

                        cmd.Parameters.AddWithValue("@NombreEmpresa", empresa);
                        cmd.Parameters.AddWithValue("@NombreContacto", contacto);
                        cmd.Parameters.AddWithValue("@Telefono", telefono);
                        cmd.Parameters.AddWithValue("@Correo", correo);
                        cmd.Parameters.AddWithValue("@Direccion", direccion);

                        cmd.ExecuteNonQuery();

                        Console.WriteLine("Proveedor registrado correctamente");
                        Console.WriteLine("-----------------------------------------------");
                    }
                }
            }
            else if (opcionProveedor == 2)
            {
                // CONSULTAR TODOS LOS PROVEEDORES

                string sql = @"SELECT IdProveedor, NombreEmpresa, NombreContacto,
                                            Telefono, Correo, Direccion, Estado
                                            FROM Proveedores;";

                using (SqlConnection con = new SqlConnection(conexion))
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        Console.WriteLine("-----------------------------------------------");
                        Console.WriteLine("LISTA DE PROVEEDORES");
                        Console.WriteLine("-----------------------------------------------");

                        using (SqlDataReader respuestaBD = cmd.ExecuteReader())
                        {
                            while (respuestaBD.Read())
                            {
                                Console.WriteLine("ID: " + respuestaBD["IdProveedor"]);
                                Console.WriteLine("Empresa: " + respuestaBD["NombreEmpresa"]);
                                Console.WriteLine("Contacto: " + respuestaBD["NombreContacto"]);
                                Console.WriteLine("Telefono: " + respuestaBD["Telefono"]);
                                Console.WriteLine("Correo: " + respuestaBD["Correo"]);
                                Console.WriteLine("Direccion: " + respuestaBD["Direccion"]);
                                Console.WriteLine("Estado: " + respuestaBD["Estado"]);
                                Console.WriteLine("-----------------------------------------------");
                            }
                        }
                    }
                }
            }
            else if (opcionProveedor == 3)
            {
                // MODIFICAR PROVEEDOR

                string sql = @"UPDATE Proveedores
                                            SET NombreEmpresa = @NombreEmpresa,
                                                NombreContacto = @NombreContacto,
                                                Telefono = @Telefono,
                                                Correo = @Correo,
                                                Direccion = @Direccion
                                            WHERE IdProveedor = @IdProveedor;";

                using (SqlConnection con = new SqlConnection(conexion))
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        Console.WriteLine("-----------------------------------------------");
                        Console.WriteLine("MODIFICAR PROVEEDOR");
                        Console.WriteLine("-----------------------------------------------");

                        Console.WriteLine("Ingrese el ID del proveedor que desea modificar: ");
                        int id = int.Parse(Console.ReadLine()!);

                        Console.WriteLine("Ingrese el nuevo nombre de la empresa: ");
                        string empresa = Console.ReadLine()!;

                        Console.WriteLine("Ingrese el nuevo nombre del contacto: ");
                        string contacto = Console.ReadLine()!;

                        Console.WriteLine("Ingrese el nuevo telefono: ");
                        string telefono = Console.ReadLine()!;

                        Console.WriteLine("Ingrese el nuevo correo: ");
                        string correo = Console.ReadLine()!;

                        Console.WriteLine("Ingrese la nueva direccion: ");
                        string direccion = Console.ReadLine()!;

                        cmd.Parameters.AddWithValue("@IdProveedor", id);
                        cmd.Parameters.AddWithValue("@NombreEmpresa", empresa);
                        cmd.Parameters.AddWithValue("@NombreContacto", contacto);
                        cmd.Parameters.AddWithValue("@Telefono", telefono);
                        cmd.Parameters.AddWithValue("@Correo", correo);
                        cmd.Parameters.AddWithValue("@Direccion", direccion);

                        int resultado = cmd.ExecuteNonQuery();

                        if (resultado > 0)
                        {
                            Console.WriteLine("Proveedor modificado correctamente");
                        }
                        else
                        {
                            Console.WriteLine("No se encontro el proveedor");
                        }

                        Console.WriteLine("-----------------------------------------------");
                    }
                }
            }
            else if (opcionProveedor != 4)
            {
                Console.WriteLine("Opcion no valida");
            }
        }
    }

}


