using System.Data.SqlClient; // Recomendado para .NET 6/7/8
using MiWebAPI.Models;
using System.Data;

namespace MiWebAPI.Data
{
    public class ProveedorData
    {
        private readonly string conexion;

        public ProveedorData(IConfiguration configuration)
        {
            conexion = configuration.GetConnectionString("CadenaSQL")!;
        }

        // 1. LISTAR TODOS
        public async Task<List<Proveedor>> Listar()
        {
            List<Proveedor> lista = new List<Proveedor>();

            using (var con = new SqlConnection(conexion))
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("dbo.PROVEEDOR_LISTAR", con);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        lista.Add(new Proveedor
                        {
                            Id = reader["Id"] != DBNull.Value ? Convert.ToInt64(reader["Id"]) : 0,
                            Nombre = reader["Nombre"] != DBNull.Value ? reader["Nombre"].ToString()! : string.Empty,
                            ApellidoPaterno = reader["Apellido_Paterno"] != DBNull.Value ? reader["Apellido_Paterno"].ToString()! : string.Empty,
                            ApellidoMaterno = reader["Apellido_Materno"] != DBNull.Value ? reader["Apellido_Materno"].ToString()! : string.Empty,
                            Rfc = reader["Rfc"] != DBNull.Value ? reader["Rfc"].ToString()! : string.Empty,
                            Correo = reader["Correo"] != DBNull.Value ? reader["Correo"].ToString()! : string.Empty,
                            Credito = reader["Credito"] != DBNull.Value ? Convert.ToDecimal(reader["Credito"]) : 0,
                            NombreBanco = reader["Nombre_Banco"] != DBNull.Value ? reader["Nombre_Banco"].ToString()! : string.Empty,
                            FechaUltimaModificacion = reader["FECHA_ULTIMA_MODIFICACION"] != DBNull.Value ? Convert.ToDateTime(reader["FECHA_ULTIMA_MODIFICACION"]) : null
                        });
                    }
                }
            }
            return lista;
        }

        // 2. CREAR (AGREGAR)
        public async Task<bool> Crear(Proveedor objeto)
        {
            bool respuesta = false;

            using (var con = new SqlConnection(conexion))
            {
                SqlCommand cmd = new SqlCommand("dbo.PROVEEDOR_AGREGA", con);
                cmd.Parameters.AddWithValue("@nombre", objeto.Nombre ?? string.Empty);
                cmd.Parameters.AddWithValue("@apellido_paterno", objeto.ApellidoPaterno ?? string.Empty);
                cmd.Parameters.AddWithValue("@apellido_materno", objeto.ApellidoMaterno ?? string.Empty);
                cmd.Parameters.AddWithValue("@rfc", objeto.Rfc ?? string.Empty);
                cmd.Parameters.AddWithValue("@correo", objeto.Correo ?? string.Empty);
                cmd.Parameters.AddWithValue("@credito", objeto.Credito);
                cmd.Parameters.AddWithValue("@nombre_banco", objeto.NombreBanco ?? string.Empty);
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