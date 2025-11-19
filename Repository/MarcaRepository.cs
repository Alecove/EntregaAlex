using MySqlConnector;
using EntregaAlex.Models;
using System.Data;
using Microsoft.Extensions.Configuration; // <-- ESTA LÍNEA ES CLAVE

namespace EntregaAlex.Repository
{
    public interface IMarcaRepository
    {
        Task<List<Marca>> GetAllAsync();
        Task<Marca?> GetByIdAsync(int id);
        Task<Marca> CreateAsync(Marca marca);
        Task<Marca?> UpdateAsync(Marca marca);
        Task<bool> DeleteAsync(int id);
    }

    public class MarcaRepository : IMarcaRepository
    {
        private readonly string _connectionString;

        public MarcaRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }

        public async Task<List<Marca>> GetAllAsync()
        {
            var lista = new List<Marca>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var query = "SELECT * FROM Marcas";
                
                using (var command = new MySqlCommand(query, connection))
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        lista.Add(MapToMarca(reader));
                    }
                }
            }
            return lista;
        }

        public async Task<Marca?> GetByIdAsync(int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var query = "SELECT * FROM Marcas WHERE Id = @Id";
                
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return MapToMarca(reader);
                        }
                    }
                }
            }
            return null;
        }

        public async Task<Marca> CreateAsync(Marca marca)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                // Importante: Insertamos los 6 campos obligatorios
                var query = @"INSERT INTO Marcas (Nombre, PaisOrigen, AnioFundacion, ValorMercadoMillones, EsAltaCostura, FechaAlianza) 
                              VALUES (@Nombre, @PaisOrigen, @AnioFundacion, @ValorMercadoMillones, @EsAltaCostura, @FechaAlianza);
                              SELECT LAST_INSERT_ID();";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nombre", marca.Nombre);
                    command.Parameters.AddWithValue("@PaisOrigen", marca.PaisOrigen);
                    command.Parameters.AddWithValue("@AnioFundacion", marca.AnioFundacion);
                    command.Parameters.AddWithValue("@ValorMercadoMillones", marca.ValorMercadoMillones);
                    command.Parameters.AddWithValue("@EsAltaCostura", marca.EsAltaCostura);
                    command.Parameters.AddWithValue("@FechaAlianza", marca.FechaAlianza);

                    var newId = await command.ExecuteScalarAsync();
                    marca.Id = Convert.ToInt32(newId);
                }
            }
            return marca;
        }

        public async Task<Marca?> UpdateAsync(Marca marca)
        {
             using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var query = @"UPDATE Marcas 
                              SET Nombre = @Nombre, 
                                  PaisOrigen = @PaisOrigen, 
                                  AnioFundacion = @AnioFundacion, 
                                  ValorMercadoMillones = @ValorMercadoMillones, 
                                  EsAltaCostura = @EsAltaCostura
                              WHERE Id = @Id";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", marca.Id);
                    command.Parameters.AddWithValue("@Nombre", marca.Nombre);
                    command.Parameters.AddWithValue("@PaisOrigen", marca.PaisOrigen);
                    command.Parameters.AddWithValue("@AnioFundacion", marca.AnioFundacion);
                    command.Parameters.AddWithValue("@ValorMercadoMillones", marca.ValorMercadoMillones);
                    command.Parameters.AddWithValue("@EsAltaCostura", marca.EsAltaCostura);

                    var rowsAffected = await command.ExecuteNonQueryAsync();
                    if (rowsAffected == 0) return null;
                }
            }
            return marca;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var query = "DELETE FROM Marcas WHERE Id = @Id";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    var rowsAffected = await command.ExecuteNonQueryAsync();
                    return rowsAffected > 0;
                }
            }
        }

        private Marca MapToMarca(MySqlDataReader reader)
        {
            return new Marca
            {
                Id = reader.GetInt32("Id"),
                Nombre = reader.GetString("Nombre"),
                PaisOrigen = reader.GetString("PaisOrigen"),
                AnioFundacion = reader.GetInt32("AnioFundacion"),
                // Manejo seguro de nulos para decimales si fuera necesario, aquí asumimos que siempre tiene valor
                ValorMercadoMillones = reader.GetDecimal("ValorMercadoMillones"),
                EsAltaCostura = reader.GetBoolean("EsAltaCostura"),
                FechaAlianza = reader.GetDateTime("FechaAlianza")
            };
        }
    }
}