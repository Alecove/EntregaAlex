using MySqlConnector; // <--- ESTO ES LO QUE CAMBIA RESPECTO AL PROFE (Él usa SqlClient)
using EntregaAlex.Models;
using Microsoft.Extensions.Configuration;

namespace EntregaAlex.Repository
{
    public class MarcaRepository : IMarcaRepository
    {
        private readonly string _connectionString;

        public MarcaRepository(IConfiguration configuration)
        {
            // El profesor lo hace así: lee la conexión directamente aquí
            _connectionString = configuration.GetConnectionString("DefaultConnection") ?? "";
        }

        // 1. OBTENER TODAS
        public async Task<List<Marca>> GetAllAsync()
        {
            var listaMarcas = new List<Marca>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                // QUERY MANUAL (Igual que el profe)
                string query = "SELECT Id, Nombre, PaisOrigen, AnioFundacion, ValorMercadoMillones, EsAltaCostura FROM Marcas";

                using (var command = new MySqlCommand(query, connection))
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var marca = new Marca
                        {
                            Id = reader.GetInt32(0),
                            Nombre = reader.GetString(1),
                            PaisOrigen = reader.GetString(2),
                            AnioFundacion = reader.GetInt32(3),
                            EsAltaCostura = reader.GetBoolean(5)
                            // Nota: Omito fechas complejas para simplificar, igual que el ejemplo del profe
                        };
                        listaMarcas.Add(marca);
                    }
                }
            }
            return listaMarcas;
        }

        // 2. OBTENER POR ID
        public async Task<Marca?> GetByIdAsync(int id)
        {
            Marca? marca = null;

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT Id, Nombre, PaisOrigen, AnioFundacion, ValorMercadoMillones, EsAltaCostura FROM Marcas WHERE Id = @Id";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            marca = new Marca
                            {
                                Id = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                PaisOrigen = reader.GetString(2),
                                AnioFundacion = reader.GetInt32(3),
                                EsAltaCostura = reader.GetBoolean(5)
                            };
                        }
                    }
                }
            }
            return marca;
        }

        // 3. CREAR (INSERT)
        public async Task<Marca> CreateAsync(Marca marca)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                // OJO: En MySQL para obtener el ID generado se usa SELECT LAST_INSERT_ID();
                string query = @"INSERT INTO Marcas (Nombre, PaisOrigen, AnioFundacion, ValorMercadoMillones, EsAltaCostura, FechaAlianza) 
                                 VALUES (@Nombre, @Pais, @Anio, @Valor, @EsAlta, @Fecha);
                                 SELECT LAST_INSERT_ID();";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nombre", marca.Nombre);
                    command.Parameters.AddWithValue("@Pais", marca.PaisOrigen);
                    command.Parameters.AddWithValue("@Anio", marca.AnioFundacion);
                    command.Parameters.AddWithValue("@EsAlta", marca.EsAltaCostura);
                    command.Parameters.AddWithValue("@Fecha", DateTime.Now);

                    // Ejecutamos y recuperamos el ID generado
                    var idGenerado = await command.ExecuteScalarAsync();
                    if (idGenerado != null)
                    {
                        marca.Id = Convert.ToInt32(idGenerado);
                    }
                }
            }
            return marca;
        }

        // 4. ACTUALIZAR (UPDATE)
        public async Task<Marca?> UpdateAsync(Marca marca)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = @"UPDATE Marcas 
                                 SET Nombre = @Nombre, PaisOrigen = @Pais, AnioFundacion = @Anio, 
                                     ValorMercadoMillones = @Valor, EsAltaCostura = @EsAlta 
                                 WHERE Id = @Id";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", marca.Id);
                    command.Parameters.AddWithValue("@Nombre", marca.Nombre);
                    command.Parameters.AddWithValue("@Pais", marca.PaisOrigen);
                    command.Parameters.AddWithValue("@Anio", marca.AnioFundacion);
                    command.Parameters.AddWithValue("@EsAlta", marca.EsAltaCostura);

                    int filasAfectadas = await command.ExecuteNonQueryAsync();
                    if (filasAfectadas == 0) return null; // No existía
                }
            }
            return marca;
        }

        // 5. BORRAR (DELETE)
        public async Task<bool> DeleteAsync(int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "DELETE FROM Marcas WHERE Id = @Id";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    int filasAfectadas = await command.ExecuteNonQueryAsync();
                    return filasAfectadas > 0;
                }
            }
        }
    }
}