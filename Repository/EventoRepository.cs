using MySqlConnector;
using EntregaAlex.Models;
using Microsoft.Extensions.Configuration;

namespace EntregaAlex.Repository
{
    public class EventoRepository : IEventoRepository
    {
        private readonly string _connectionString;

        public EventoRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") ?? "";
        }

        public async Task<List<Evento>> GetAllAsync()
        {
            var lista = new List<Evento>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "SELECT Id, Ciudad, UbicacionExacta, CapacidadAsistentes, CosteEntrada, EsBenefico, FechaEvento, ColeccionId FROM Eventos";

                using (var command = new MySqlCommand(query, connection))
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        lista.Add(new Evento
                        {
                            Id = reader.GetInt32(0),
                            Ciudad = reader.GetString(1),
                            UbicacionExacta = reader.IsDBNull(2) ? "" : reader.GetString(2),
                            CapacidadAsistentes = reader.GetInt32(3),
                            CosteEntrada = reader.GetDecimal(4),
                            EsBenefico = reader.GetBoolean(5),
                            FechaEvento = reader.GetDateTime(6),
                            ColeccionId = reader.GetInt32(7)
                        });
                    }
                }
            }
            return lista;
        }

        public async Task<Evento?> GetByIdAsync(int id)
        {
            Evento? evento = null;

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "SELECT Id, Ciudad, UbicacionExacta, CapacidadAsistentes, CosteEntrada, EsBenefico, FechaEvento, ColeccionId FROM Eventos WHERE Id = @Id";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            evento = new Evento
                            {
                                Id = reader.GetInt32(0),
                                Ciudad = reader.GetString(1),
                                UbicacionExacta = reader.IsDBNull(2) ? "" : reader.GetString(2),
                                CapacidadAsistentes = reader.GetInt32(3),
                                CosteEntrada = reader.GetDecimal(4),
                                EsBenefico = reader.GetBoolean(5),
                                FechaEvento = reader.GetDateTime(6),
                                ColeccionId = reader.GetInt32(7)
                            };
                        }
                    }
                }
            }
            return evento;
        }

        public async Task<Evento> CreateAsync(Evento evento)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = @"INSERT INTO Eventos (Ciudad, UbicacionExacta, CapacidadAsistentes, CosteEntrada, EsBenefico, FechaEvento, ColeccionId) 
                                 VALUES (@Ciudad, @Ubicacion, @Capacidad, @Coste, @Benefico, @Fecha, @ColeccionId);
                                 SELECT LAST_INSERT_ID();";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Ciudad", evento.Ciudad);
                    command.Parameters.AddWithValue("@Ubicacion", evento.UbicacionExacta);
                    command.Parameters.AddWithValue("@Capacidad", evento.CapacidadAsistentes);
                    command.Parameters.AddWithValue("@Coste", evento.CosteEntrada);
                    command.Parameters.AddWithValue("@Benefico", evento.EsBenefico);
                    command.Parameters.AddWithValue("@Fecha", DateTime.Now); // O la fecha que venga
                    command.Parameters.AddWithValue("@ColeccionId", evento.ColeccionId);

                    var id = await command.ExecuteScalarAsync();
                    if (id != null) evento.Id = Convert.ToInt32(id);
                }
            }
            return evento;
        }

        public async Task<Evento?> UpdateAsync(Evento evento)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = @"UPDATE Eventos 
                                 SET Ciudad=@Ciudad, UbicacionExacta=@Ubicacion, CapacidadAsistentes=@Capacidad, 
                                     CosteEntrada=@Coste, EsBenefico=@Benefico, ColeccionId=@ColeccionId 
                                 WHERE Id=@Id";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", evento.Id);
                    command.Parameters.AddWithValue("@Ciudad", evento.Ciudad);
                    command.Parameters.AddWithValue("@Ubicacion", evento.UbicacionExacta);
                    command.Parameters.AddWithValue("@Capacidad", evento.CapacidadAsistentes);
                    command.Parameters.AddWithValue("@Coste", evento.CosteEntrada);
                    command.Parameters.AddWithValue("@Benefico", evento.EsBenefico);
                    command.Parameters.AddWithValue("@ColeccionId", evento.ColeccionId);

                    int filas = await command.ExecuteNonQueryAsync();
                    if (filas == 0) return null;
                }
            }
            return evento;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "DELETE FROM Eventos WHERE Id = @Id";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    return await command.ExecuteNonQueryAsync() > 0;
                }
            }
        }
    }
}