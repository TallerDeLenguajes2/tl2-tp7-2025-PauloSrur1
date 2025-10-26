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








    }
}