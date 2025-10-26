using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using Models;

namespace Repositories
{
    public class ProductoRepository
    {
        private string cadenaConexion = "Data Source=Tienda.db;";

        //logica para crear un nuevo producto 
        public void Crear(Producto producto)
        {
            using var conexion = new SqliteConnection(cadenaConexion);
            conexion.Open();

            string sql = "INSERT INTO Productos (descripcion, precio) VALUES (@desc, @precio)";
            using var comando = new SqliteCommand(sql,conexion);
            comando.Parameters.AddWithValue("@desc", producto.Descripcion);
            comando.Parameters.AddWithValue("@precio", producto.Precio);
            comando.ExecuteNonQuery();
        }

        //logica para modificar un producto existente
        public void Modificar(Producto producto)
        {
            using var conexion = new SqliteConnection(cadenaConexion);
            conexion.Open();

            string sql = "UPDATE Productos SET descripcion = @desc, precio = @precio WHERE idProducto = @id";
            using var comando = new SqliteCommand(sql, conexion);
            comando.Parameters.AddWithValue("@desc", producto.Descripcion);
            comando.Parameters.AddWithValue("@precio", producto.Precio);
            comando.Parameters.AddWithValue("@id", producto.IdProducto);
            comando.ExecuteNonQuery();
        }

        //logica para listar todos los productos
        public List<Producto> Listar()
        {
            var lista = new List<Producto>();
            using var conexion = new SqliteConnection(cadenaConexion);
            conexion.Open();

            string sql = "SELECT idProducto, descripcion, precio FROM Productos";
            using var comando = new SqliteCommand(sql, conexion);
            using var lector = comando.ExecuteReader();

            while (lector.Read())
            {
                var prod = new Producto
                {
                    IdProducto = lector.GetInt32(0),
                    Descripcion = lector.GetString(1),
                    Precio = lector.GetInt32(2)
                };
                lista.Add(prod);
            }

            return lista;
        }

        //logica para obtener un producto por ID
        public Producto? ObtenerPorId(int id)
        {
            using var conexion = new SqliteConnection(cadenaConexion);
            conexion.Open();

            string sql = "SELECT idProducto, descripcion, precio FROM Productos WHERE idProducto = @id";
            using var comando = new SqliteCommand(sql, conexion);
            comando.Parameters.AddWithValue("@id", id);

            using var lector = comando.ExecuteReader();
            if (lector.Read())
            {
                return new Producto
                {
                    IdProducto = lector.GetInt32(0),
                    Descripcion = lector.GetString(1),
                    Precio = lector.GetInt32(2)
                };
            }
            return null;
        }

        //logica para eliminar producto por ID
        public bool Eliminar(int id)
        {
            using var conexion = new SqliteConnection(cadenaConexion);
            conexion.Open();

            string sql = "DELETE FROM Productos WHERE idProducto = @id";
            using var comando = new SqliteCommand(sql, conexion);
            comando.Parameters.AddWithValue("@id", id);

            int filasAfectadas = comando.ExecuteNonQuery();
            return filasAfectadas > 0;
        }







    }
}