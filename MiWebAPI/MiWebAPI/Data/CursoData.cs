using System.Data;
using System.Data.SqlClient;
using MiWebAPI.Models;

namespace MiWebAPI.Data
{
    public class CursoData
    {
        private readonly string _conexion;

        public CursoData(IConfiguration configuration)
        {
            _conexion = configuration.GetConnectionString("CadenaSQL")!;
        }

        public async Task<List<Curso>> ConsultarTodos()
        {
            var lista = new List<Curso>();

            using (var sql = new SqlConnection(_conexion))
            {
                using (var cmd = new SqlCommand("CURSO_SELECCIONA", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            lista.Add(new Curso
                            {
                                IdCurso = Convert.ToInt32(reader["ID_CURSO"]),
                                Clave = reader["CLAVE"].ToString()!,
                                Nombre = reader["NOMBRE"].ToString()!,
                                Descripcion = reader["DESCRIPCION"] != DBNull.Value ? reader["DESCRIPCION"].ToString() : null,
                                DuracionHoras = Convert.ToInt32(reader["DURACION_HORAS"]),
                                Costo = Convert.ToDecimal(reader["COSTO"]),
                                Activo = Convert.ToBoolean(reader["ACTIVO"]),
                                FechaRegistro = Convert.ToDateTime(reader["FECHA_REGISTRO"]),
                                FechaActualizacion = reader["FECHA_ACTUALIZACION"] != DBNull.Value ? Convert.ToDateTime(reader["FECHA_ACTUALIZACION"]) : null
                            });
                        }
                    }
                }
            }
            return lista;
        }

        public async Task<Curso?> ConsultarPorId(int id)
        {
            Curso? objeto = null;

            using (var sql = new SqlConnection(_conexion))
            {
                using (var cmd = new SqlCommand("CURSO_SELECCIONA_ID", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdCurso", id);
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            objeto = new Curso
                            {
                                IdCurso = Convert.ToInt32(reader["ID_CURSO"]),
                                Clave = reader["CLAVE"].ToString()!,
                                Nombre = reader["NOMBRE"].ToString()!,
                                Descripcion = reader["DESCRIPCION"] != DBNull.Value ? reader["DESCRIPCION"].ToString() : null,
                                DuracionHoras = Convert.ToInt32(reader["DURACION_HORAS"]),
                                Costo = Convert.ToDecimal(reader["COSTO"]),
                                Activo = Convert.ToBoolean(reader["ACTIVO"]),
                                FechaRegistro = Convert.ToDateTime(reader["FECHA_REGISTRO"]),
                                FechaActualizacion = reader["FECHA_ACTUALIZACION"] != DBNull.Value ? Convert.ToDateTime(reader["FECHA_ACTUALIZACION"]) : null
                            };
                        }
                    }
                }
            }
            return objeto;
        }

        public async Task<bool> Insertar(Curso objeto)
        {
            using (var sql = new SqlConnection(_conexion))
            {
                using (var cmd = new SqlCommand("CURSO_INSERTAR", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Clave", objeto.Clave);
                    cmd.Parameters.AddWithValue("@Nombre", objeto.Nombre);
                    cmd.Parameters.AddWithValue("@Descripcion", (object?)objeto.Descripcion ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@DuracionHoras", objeto.DuracionHoras);
                    cmd.Parameters.AddWithValue("@Costo", objeto.Costo);

                    await sql.OpenAsync();
                    int filasAfectadas = await cmd.ExecuteNonQueryAsync();
                    return filasAfectadas > 0;
                }
            }
        }

        public async Task<bool> Actualizar(Curso objeto)
        {
            using (var sql = new SqlConnection(_conexion))
            {
                using (var cmd = new SqlCommand("CURSO_ACTUALIZA", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdCurso", objeto.IdCurso);
                    cmd.Parameters.AddWithValue("@Clave", objeto.Clave);
                    cmd.Parameters.AddWithValue("@Nombre", objeto.Nombre);
                    cmd.Parameters.AddWithValue("@Descripcion", (object?)objeto.Descripcion ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@DuracionHoras", objeto.DuracionHoras);
                    cmd.Parameters.AddWithValue("@Costo", objeto.Costo);

                    await sql.OpenAsync();
                    int filasAfectadas = await cmd.ExecuteNonQueryAsync();
                    return filasAfectadas > 0;
                }
            }
        }

        public async Task<bool> Eliminar(int id)
        {
            using (var sql = new SqlConnection(_conexion))
            {
                using (var cmd = new SqlCommand("CURSO_ELIMINA", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdCurso", id);

                    await sql.OpenAsync();
                    int filasAfectadas = await cmd.ExecuteNonQueryAsync();
                    return filasAfectadas > 0;
                }
            }
        }
    }
}