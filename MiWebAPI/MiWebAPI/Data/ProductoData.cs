using MiWebAPI.Models;
using System.Data;
using System.Data.SqlClient;

namespace MiWebAPI.Data
{
    public class ProductoData
    {
        private readonly string conexion;

        public ProductoData(IConfiguration configuration)
        {
            conexion = configuration.GetConnectionString("CadenaSQL")!;
        }

        // 1. LISTAR TODOS
        public async Task<List<ProductoDetalle>> Lista()
        {
            List<ProductoDetalle> lista = new List<ProductoDetalle>();

            using (var con = new SqlConnection(conexion))
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("dbo.PRODUCTO_DETALLE_LISTAR", con);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        lista.Add(new ProductoDetalle
                        {
                            Id = reader["Id"] != DBNull.Value ? Convert.ToInt32(reader["Id"]) : (reader["ID"] != DBNull.Value ? Convert.ToInt32(reader["ID"]) : 0),
                            NombreProducto = reader["NombreProducto"] != DBNull.Value ? reader["NombreProducto"].ToString()! : (reader["NOMBRE_PRODUCTO"] != DBNull.Value ? reader["NOMBRE_PRODUCTO"].ToString()! : string.Empty),
                            Precio = reader["Precio"] != DBNull.Value ? Convert.ToDecimal(reader["Precio"]) : (reader["PRECIO"] != DBNull.Value ? Convert.ToDecimal(reader["PRECIO"]) : 0),
                            Cantidad = reader["Cantidad"] != DBNull.Value ? Convert.ToInt32(reader["Cantidad"]) : (reader["CANTIDAD"] != DBNull.Value ? Convert.ToInt32(reader["CANTIDAD"]) : 0),
                            Total = reader["Total"] != DBNull.Value ? Convert.ToDecimal(reader["Total"]) : (reader["TOTAL"] != DBNull.Value ? Convert.ToDecimal(reader["TOTAL"]) : 0),
                            Descripcion = reader["Descripcion"] != DBNull.Value ? reader["Descripcion"].ToString()! : (reader["DESCRIPCION"] != DBNull.Value ? reader["DESCRIPCION"].ToString()! : string.Empty)
                        });
                    }
                }
            }
            return lista;
        }

        // 2. LISTAR POR ID (OBTENER)
        public async Task<ProductoDetalle?> Obtener(int id)
        {
            ProductoDetalle? objeto = null;

            using (var con = new SqlConnection(conexion))
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("dbo.PRODUCTO_DETALLE_OBTENER", con);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        objeto = new ProductoDetalle
                        {
                            Id = reader["Id"] != DBNull.Value ? Convert.ToInt32(reader["Id"]) : (reader["ID"] != DBNull.Value ? Convert.ToInt32(reader["ID"]) : 0),
                            NombreProducto = reader["NombreProducto"] != DBNull.Value ? reader["NombreProducto"].ToString()! : (reader["NOMBRE_PRODUCTO"] != DBNull.Value ? reader["NOMBRE_PRODUCTO"].ToString()! : string.Empty),
                            Precio = reader["Precio"] != DBNull.Value ? Convert.ToDecimal(reader["Precio"]) : (reader["PRECIO"] != DBNull.Value ? Convert.ToDecimal(reader["PRECIO"]) : 0),
                            Cantidad = reader["Cantidad"] != DBNull.Value ? Convert.ToInt32(reader["Cantidad"]) : (reader["CANTIDAD"] != DBNull.Value ? Convert.ToInt32(reader["CANTIDAD"]) : 0),
                            Total = reader["Total"] != DBNull.Value ? Convert.ToDecimal(reader["Total"]) : (reader["TOTAL"] != DBNull.Value ? Convert.ToDecimal(reader["TOTAL"]) : 0),
                            Descripcion = reader["Descripcion"] != DBNull.Value ? reader["Descripcion"].ToString()! : (reader["DESCRIPCION"] != DBNull.Value ? reader["DESCRIPCION"].ToString()! : string.Empty)
                        };
                    }
                }
            }
            return objeto;
        }

        // 3. CREAR (AGREGAR)
        public async Task<bool> Crear(ProductoDetalle objeto)
        {
            bool respuesta = false;

            using (var con = new SqlConnection(conexion))
            {
                SqlCommand cmd = new SqlCommand("dbo.PRODUCTO_DETALLE_AGREGA", con);
                cmd.Parameters.AddWithValue("@nombreProducto", objeto.NombreProducto ?? string.Empty);
                cmd.Parameters.AddWithValue("@precio", objeto.Precio);
                cmd.Parameters.AddWithValue("@cantidad", objeto.Cantidad);
                cmd.Parameters.AddWithValue("@total", objeto.Total);
                cmd.Parameters.AddWithValue("@descripcion", objeto.Descripcion ?? string.Empty);
                cmd.CommandType = CommandType.StoredProcedure;

                await con.OpenAsync();
                int filasAfectadas = await cmd.ExecuteNonQueryAsync();

                if (filasAfectadas > 0)
                {
                    respuesta = true;
                }
            }
            return respuesta;
        }

        // 4. EDITAR
        public async Task<bool> Editar(ProductoDetalle objeto)
        {
            bool respuesta = false;

            using (var con = new SqlConnection(conexion))
            {
                SqlCommand cmd = new SqlCommand("dbo.PRODUCTO_DETALLE_EDITAR", con);
                cmd.Parameters.AddWithValue("@id", objeto.Id);
                cmd.Parameters.AddWithValue("@nombreProducto", objeto.NombreProducto ?? string.Empty);
                cmd.Parameters.AddWithValue("@precio", objeto.Precio);
                cmd.Parameters.AddWithValue("@cantidad", objeto.Cantidad);
                cmd.Parameters.AddWithValue("@total", objeto.Total);
                cmd.Parameters.AddWithValue("@descripcion", objeto.Descripcion ?? string.Empty);
                cmd.CommandType = CommandType.StoredProcedure;

                await con.OpenAsync();
                int filasAfectadas = await cmd.ExecuteNonQueryAsync();

                if (filasAfectadas > 0)
                {
                    respuesta = true;
                }
            }
            return respuesta;
        }

        // 5. ELIMINAR
        public async Task<bool> Eliminar(int id)
        {
            bool respuesta = false;

            using (var con = new SqlConnection(conexion))
            {
                SqlCommand cmd = new SqlCommand("dbo.PRODUCTO_DETALLE_ELIMINAR", con);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.CommandType = CommandType.StoredProcedure;

                await con.OpenAsync();
                int filasAfectadas = await cmd.ExecuteNonQueryAsync();

                if (filasAfectadas > 0)
                {
                    respuesta = true;
                }
            }
            return respuesta;
        }
    }
}