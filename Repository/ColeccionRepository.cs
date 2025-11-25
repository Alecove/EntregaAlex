using MySqlConnector;
using EntregaAlex.Models;
using Microsoft.Extensions.Configuration;

namespace EntregaAlex.Repository
{
    public class ColeccionRepository : IColeccionRepository
    {
        private readonly string _connectionString;

        public ColeccionRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") ?? "";
        }

        public async Task<List<Coleccion>> GetAllAsync()
        {
            var lista = new List<Coleccion>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                
                string query = "SELECT Id, NombreColeccion, Temporada, NumeroPiezas, PresupuestoInversion, EsLimitada, FechaLanzamiento, DiseñadorId FROM Colecciones";

                using (var command = new MySqlCommand(query, connection))
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        lista.Add(new Coleccion
                        {
                            Id = reader.GetInt32(0),
                            NombreColeccion = reader.GetString(1),
                            Temporada = reader.GetString(2),
                            NumeroPiezas = reader.GetInt32(3),
                            PresupuestoInversion = reader.GetDecimal(4),
                            EsLimitada = reader.GetBoolean(5),
                            FechaLanzamiento = reader.GetDateTime(6),
                            DiseñadorId = reader.GetInt32(7)
                        });
                    }
                }
            }
            return lista;
        }

        public async Task<Coleccion?> GetByIdAsync(int id)
        {
            Coleccion? coleccion = null;

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "SELECT Id, NombreColeccion, Temporada, NumeroPiezas, PresupuestoInversion, EsLimitada, FechaLanzamiento, DiseñadorId FROM Colecciones WHERE Id = @Id";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            coleccion = new Coleccion
                            {
                                Id = reader.GetInt32(0),
                                NombreColeccion = reader.GetString(1),
                                Temporada = reader.GetString(2),
                                NumeroPiezas = reader.GetInt32(3),
                                PresupuestoInversion = reader.GetDecimal(4),
                                EsLimitada = reader.GetBoolean(5),
                                FechaLanzamiento = reader.GetDateTime(6),
                                DiseñadorId = reader.GetInt32(7)
                            };
                        }
                    }
                }
            }
            return coleccion;
        }

        public async Task<Coleccion> CreateAsync(Coleccion coleccion)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = @"INSERT INTO Colecciones (NombreColeccion, Temporada, NumeroPiezas, PresupuestoInversion, EsLimitada, FechaLanzamiento, DiseñadorId) 
                                 VALUES (@Nombre, @Temp, @Piezas, @Presu, @Limitada, @Fecha, @DiseñadorId);
                                 SELECT LAST_INSERT_ID();";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nombre", coleccion.NombreColeccion);
                    command.Parameters.AddWithValue("@Temp", coleccion.Temporada);
                    command.Parameters.AddWithValue("@Piezas", coleccion.NumeroPiezas);
                    command.Parameters.AddWithValue("@Presu", coleccion.PresupuestoInversion);
                    command.Parameters.AddWithValue("@Limitada", coleccion.EsLimitada);
                    command.Parameters.AddWithValue("@Fecha", DateTime.Now);
                    command.Parameters.AddWithValue("@DiseñadorId", coleccion.DiseñadorId);

                    var id = await command.ExecuteScalarAsync();
                    if (id != null) coleccion.Id = Convert.ToInt32(id);
                }
            }
            return coleccion;
        }

        public async Task<Coleccion?> UpdateAsync(Coleccion coleccion)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = @"UPDATE Colecciones 
                                 SET NombreColeccion=@Nombre, Temporada=@Temp, NumeroPiezas=@Piezas, 
                                     PresupuestoInversion=@Presu, EsLimitada=@Limitada, DiseñadorId=@DiseñadorId 
                                 WHERE Id=@Id";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", coleccion.Id);
                    command.Parameters.AddWithValue("@Nombre", coleccion.NombreColeccion);
                    command.Parameters.AddWithValue("@Temp", coleccion.Temporada);
                    command.Parameters.AddWithValue("@Piezas", coleccion.NumeroPiezas);
                    command.Parameters.AddWithValue("@Presu", coleccion.PresupuestoInversion);
                    command.Parameters.AddWithValue("@Limitada", coleccion.EsLimitada);
                    command.Parameters.AddWithValue("@DiseñadorId", coleccion.DiseñadorId);

                    int filas = await command.ExecuteNonQueryAsync();
                    if (filas == 0) return null;
                }
            }
            return coleccion;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "DELETE FROM Colecciones WHERE Id = @Id";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    return await command.ExecuteNonQueryAsync() > 0;
                }
            }
        }
    }
}