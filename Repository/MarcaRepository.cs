using MySqlConnector; 
using EntregaAlex.Models;
using Microsoft.Extensions.Configuration;

namespace EntregaAlex.Repository
{
    public class MarcaRepository : IMarcaRepository
    {
        private readonly string _connectionString;

        public MarcaRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") ?? "";
        }

        // 1. OBTENER TODAS
        public async Task<List<Marca>> GetAllAsync()
        {
            var listaMarcas = new List<Marca>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                // HE QUITADO ValorMercadoMillones DE AQUÍ
                string query = "SELECT Id, Nombre, PaisOrigen, AnioFundacion, EsAltaCostura FROM Marcas";

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
                            // ANTES ESTO ERA EL 5, AHORA ES EL 4 PORQUE QUITAMOS UNA COLUMNA
                            EsAltaCostura = reader.GetBoolean(4) 
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

                // HE QUITADO ValorMercadoMillones DE AQUÍ TAMBIÉN
                string query = "SELECT Id, Nombre, PaisOrigen, AnioFundacion, EsAltaCostura FROM Marcas WHERE Id = @Id";

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
                                // AQUÍ TAMBIÉN CAMBIAMOS EL ÍNDICE A 4
                                EsAltaCostura = reader.GetBoolean(4)
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

                // BORRAMOS ValorMercadoMillones Y SU PARÁMETRO @Valor
                string query = @"INSERT INTO Marcas (Nombre, PaisOrigen, AnioFundacion, EsAltaCostura, FechaAlianza) 
                                 VALUES (@Nombre, @Pais, @Anio, @EsAlta, @Fecha);
                                 SELECT LAST_INSERT_ID();";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nombre", marca.Nombre);
                    command.Parameters.AddWithValue("@Pais", marca.PaisOrigen);
                    command.Parameters.AddWithValue("@Anio", marca.AnioFundacion);
                    // HE BORRADO LA LÍNEA DE @Valor QUE DABA PROBLEMAS
                    command.Parameters.AddWithValue("@EsAlta", marca.EsAltaCostura);
                    command.Parameters.AddWithValue("@Fecha", DateTime.Now);

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

                // BORRAMOS ValorMercadoMillones DEL UPDATE
                string query = @"UPDATE Marcas 
                                 SET Nombre = @Nombre, PaisOrigen = @Pais, AnioFundacion = @Anio, 
                                     EsAltaCostura = @EsAlta 
                                 WHERE Id = @Id";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", marca.Id);
                    command.Parameters.AddWithValue("@Nombre", marca.Nombre);
                    command.Parameters.AddWithValue("@Pais", marca.PaisOrigen);
                    command.Parameters.AddWithValue("@Anio", marca.AnioFundacion);
                    // BORRADO EL PARÁMETRO @Valor
                    command.Parameters.AddWithValue("@EsAlta", marca.EsAltaCostura);

                    int filasAfectadas = await command.ExecuteNonQueryAsync();
                    if (filasAfectadas == 0) return null; 
                }
            }
            return marca;
        }

        // 5. BORRAR (DELETE) - ESTE SE QUEDA IGUAL
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