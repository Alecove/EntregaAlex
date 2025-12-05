using MySqlConnector;
using EntregaAlex.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace EntregaAlex.Repository
{
    public class DiseñadorRepository : IDiseñadorRepository
    {
        private readonly string _connectionString;

        public DiseñadorRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") ?? "";
        }

        // 1. GET ALL
        public async Task<List<Diseñador>> GetAllAsync()
        {
            var lista = new List<Diseñador>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                
                // CAMBIO: Tabla 'Disenadores' (sin ñ)
                string query = "SELECT Id, NombreCompleto, Especialidad, Edad, SalarioAnual, EstaActivo, MarcaId, FechaContratacion FROM Disenadores";

                using (var command = new MySqlCommand(query, connection))
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        lista.Add(new Diseñador
                        {
                            Id = reader.GetInt32(0),
                            NombreCompleto = reader.GetString(1),
                            Especialidad = reader.IsDBNull(2) ? "General" : reader.GetString(2), // Protegemos nulos
                            Edad = reader.GetInt32(3),
                            SalarioAnual = reader.GetDecimal(4),
                            EstaActivo = reader.GetBoolean(5),
                            MarcaId = reader.GetInt32(6),
                            FechaContratacion = reader.GetDateTime(7) // Añadido para completar
                        });
                    }
                }
            }
            return lista;
        }

        // 2. GET BY ID
        public async Task<Diseñador?> GetByIdAsync(int id)
        {
            Diseñador? diseñador = null;

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                // CAMBIO: Tabla 'Disenadores' (sin ñ)
                string query = "SELECT Id, NombreCompleto, Especialidad, Edad, SalarioAnual, EstaActivo, MarcaId, FechaContratacion FROM Disenadores WHERE Id = @Id";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            diseñador = new Diseñador
                            {
                                Id = reader.GetInt32(0),
                                NombreCompleto = reader.GetString(1),
                                Especialidad = reader.IsDBNull(2) ? "General" : reader.GetString(2),
                                Edad = reader.GetInt32(3),
                                SalarioAnual = reader.GetDecimal(4),
                                EstaActivo = reader.GetBoolean(5),
                                MarcaId = reader.GetInt32(6),
                                FechaContratacion = reader.GetDateTime(7)
                            };
                        }
                    }
                }
            }
            return diseñador;
        }

        // 3. CREATE
        public async Task<Diseñador> CreateAsync(Diseñador diseñador)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                // CAMBIO: Insert en 'Disenadores' (sin ñ)
                string query = @"INSERT INTO Disenadores (NombreCompleto, Especialidad, Edad, SalarioAnual, EstaActivo, FechaContratacion, MarcaId) 
                                 VALUES (@Nombre, @Especialidad, @Edad, @Salario, @Activo, @Fecha, @MarcaId);
                                 SELECT LAST_INSERT_ID();";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nombre", diseñador.NombreCompleto);
                    command.Parameters.AddWithValue("@Especialidad", diseñador.Especialidad);
                    command.Parameters.AddWithValue("@Edad", diseñador.Edad);
                    command.Parameters.AddWithValue("@Salario", diseñador.SalarioAnual);
                    command.Parameters.AddWithValue("@Activo", diseñador.EstaActivo);
                    // Si la fecha viene vacía, ponemos la actual
                    command.Parameters.AddWithValue("@Fecha", diseñador.FechaContratacion == default ? DateTime.Now : diseñador.FechaContratacion);
                    command.Parameters.AddWithValue("@MarcaId", diseñador.MarcaId);

                    var id = await command.ExecuteScalarAsync();
                    if (id != null) diseñador.Id = Convert.ToInt32(id);
                }
            }
            return diseñador;
        }

        // 4. UPDATE
        public async Task<Diseñador?> UpdateAsync(Diseñador diseñador)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                // CAMBIO: Update en 'Disenadores' (sin ñ)
                string query = @"UPDATE Disenadores 
                                 SET NombreCompleto=@Nombre, Especialidad=@Especialidad, Edad=@Edad, 
                                     SalarioAnual=@Salario, EstaActivo=@Activo, MarcaId=@MarcaId 
                                 WHERE Id=@Id";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", diseñador.Id);
                    command.Parameters.AddWithValue("@Nombre", diseñador.NombreCompleto);
                    command.Parameters.AddWithValue("@Especialidad", diseñador.Especialidad);
                    command.Parameters.AddWithValue("@Edad", diseñador.Edad);
                    command.Parameters.AddWithValue("@Salario", diseñador.SalarioAnual);
                    command.Parameters.AddWithValue("@Activo", diseñador.EstaActivo);
                    command.Parameters.AddWithValue("@MarcaId", diseñador.MarcaId);

                    int filas = await command.ExecuteNonQueryAsync();
                    if (filas == 0) return null;
                }
            }
            return diseñador;
        }

        // 5. DELETE
        public async Task<bool> DeleteAsync(int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                // CAMBIO: Delete en 'Disenadores' (sin ñ)
                string query = "DELETE FROM Disenadores WHERE Id = @Id";
                
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    return await command.ExecuteNonQueryAsync() > 0;
                }
            }
        }
    }
}