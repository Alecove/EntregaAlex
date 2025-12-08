using MySqlConnector;
using EntregaAlex.Models;
using System.Data;
using Microsoft.Extensions.Configuration;

namespace EntregaAlex.Repository
{
    // Ahora implementa la interfaz que está en el otro archivo
    public class PrendaRepository : IPrendaRepository
    {
        private readonly string _connectionString;

        public PrendaRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }

        public async Task<List<Prenda>> GetAllAsync()
        {
            var lista = new List<Prenda>();
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var query = "SELECT * FROM Prendas";
                
                using (var command = new MySqlCommand(query, connection))
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync()) lista.Add(MapToPrenda(reader));
                }
            }
            return lista;
        }

        public async Task<Prenda?> GetByIdAsync(int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var query = "SELECT * FROM Prendas WHERE Id = @Id";
                
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync()) return MapToPrenda(reader);
                    }
                }
            }
            return null;
        }

        public async Task<Prenda> CreateAsync(Prenda prenda)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var query = @"INSERT INTO Prendas (Tipo, MaterialPrincipal, TallaNumerica, PrecioVenta, EnStock, FechaFabricacion, ColeccionId) 
                              VALUES (@Tipo, @MaterialPrincipal, @TallaNumerica, @PrecioVenta, @EnStock, @FechaFabricacion, @ColeccionId);
                              SELECT LAST_INSERT_ID();";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Tipo", prenda.Tipo);
                    command.Parameters.AddWithValue("@MaterialPrincipal", prenda.MaterialPrincipal);
                    command.Parameters.AddWithValue("@TallaNumerica", prenda.TallaNumerica);
                    command.Parameters.AddWithValue("@PrecioVenta", prenda.PrecioVenta);
                    command.Parameters.AddWithValue("@EnStock", prenda.EnStock);
                    command.Parameters.AddWithValue("@FechaFabricacion", prenda.FechaFabricacion);
                    command.Parameters.AddWithValue("@ColeccionId", prenda.ColeccionId);

                    var newId = await command.ExecuteScalarAsync();
                    prenda.Id = Convert.ToInt32(newId);
                }
            }
            return prenda;
        }

        public async Task<Prenda?> UpdateAsync(Prenda prenda)
        {
             using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var query = @"UPDATE Prendas 
                              SET Tipo = @Tipo, 
                                  MaterialPrincipal = @MaterialPrincipal, 
                                  TallaNumerica = @TallaNumerica, 
                                  PrecioVenta = @PrecioVenta, 
                                  EnStock = @EnStock,
                                  FechaFabricacion = @FechaFabricacion,
                                  ColeccionId = @ColeccionId
                              WHERE Id = @Id";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", prenda.Id);
                    command.Parameters.AddWithValue("@Tipo", prenda.Tipo);
                    command.Parameters.AddWithValue("@MaterialPrincipal", prenda.MaterialPrincipal);
                    command.Parameters.AddWithValue("@TallaNumerica", prenda.TallaNumerica);
                    command.Parameters.AddWithValue("@PrecioVenta", prenda.PrecioVenta);
                    command.Parameters.AddWithValue("@EnStock", prenda.EnStock);
                    command.Parameters.AddWithValue("@FechaFabricacion", prenda.FechaFabricacion);
                    command.Parameters.AddWithValue("@ColeccionId", prenda.ColeccionId);

                    var rowsAffected = await command.ExecuteNonQueryAsync();
                    if (rowsAffected == 0) return null;
                }
            }
            return prenda;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var query = "DELETE FROM Prendas WHERE Id = @Id";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    return await command.ExecuteNonQueryAsync() > 0;
                }
            }
        }

        private Prenda MapToPrenda(MySqlDataReader reader)
        {
            return new Prenda
            {
                Id = reader.GetInt32("Id"),
                Tipo = reader.GetString("Tipo"),
                MaterialPrincipal = reader.GetString("MaterialPrincipal"),
                TallaNumerica = reader.GetInt32("TallaNumerica"),
                PrecioVenta = reader.GetDecimal("PrecioVenta"),
                EnStock = reader.GetBoolean("EnStock"),
                FechaFabricacion = reader.GetDateTime("FechaFabricacion"),
                ColeccionId = reader.GetInt32("ColeccionId")
            };
        }
    }
}