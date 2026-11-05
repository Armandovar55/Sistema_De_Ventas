using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Linq;
using Dapper;

namespace SistemaVentas
{
    public class ProveedorRepository
    {

      
        // Funcion para agregar nuevo proveedor
        public void Insertar(Proveedor proveedor)
        {
            try
            {
                using (var conexion = new ConexionDB().CrearConexion())
                {
                    conexion.Open();


                    string sql =
                    @"INSERT INTO Proveedores
                    (
                      NombreEmpresa,
                      NombreContacto,
                     Telefono,
                     Correo,
                     Direccion,
                     Estado
                    )
                    VALUES
                    (
                    @NombreEmpresa,
                    @NombreContacto,
                    @Telefono,
                    @Correo,
                    @Direccion,
                    @Estado
                    )";

                    conexion.Execute(sql, proveedor);


                }
            }catch (SqlException ex)
            {
                Console.WriteLine($"Error de SQL{ex.Message}");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
            

        }
    

        // Funcion para COnsultar Todos los proveedores 
        public List<Proveedor> ConsultarTodos()
         {
             try
              {
                    using (var conexion = new ConexionDB().CrearConexion())
                    {

                    string sql = "SELECT IdProveedor, NombreEmpresa, NombreContacto, Telefono, Correo, Direccion, Estado FROM Proveedores";

                    var resultado = conexion.Query<Proveedor>(sql);

                    return resultado.ToList();
                    }
             }
             catch (SqlException ex)
            {
                Console.WriteLine($"Erro de SQL al consultar: {ex.Message}");
                Console.ReadKey ();
                return new List<Proveedor>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error general al consultar: {ex.Message}");
                Console.ReadKey();
                return new List<Proveedor> ();
            }
        }
    }    
}
