using MySqlConnector;
using EntregaAlex.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace EntregaAlex.Repository
{
    public class ColeccionRepository : IColeccionRepository
    {
        private readonly string _connectionString;

        public ColeccionRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") ?? "";
        }

        // 1. GET ALL
        public async Task<List<Coleccion>> GetAllColeccionesAsync()
        {
            var lista = new List<Coleccion>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                // Leemos 'DisenadorId' (sin ñ) de la BD
                string query = "SELECT Id, NombreColeccion, Temporada, NumeroPiezas, PresupuestoInversion, EsLimitada, FechaLanzamiento, DisenadorId FROM Colecciones";

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
                            DiseñadorId = reader.GetInt32(7) // Asignamos a tu propiedad C# (con ñ)
                        });
                    }
                }
            }
            return lista;
        }

        // 2. GET BY ID
        public async Task<Coleccion?> GetByIdAsync(int id)
        {
            Coleccion? coleccion = null;

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "SELECT Id, NombreColeccion, Temporada, NumeroPiezas, PresupuestoInversion, EsLimitada, FechaLanzamiento, DisenadorId FROM Colecciones WHERE Id = @Id";

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

        // 3. CREATE (POST) -> ¡AQUÍ ESTABA EL ERROR!
        public async Task<Coleccion> CreateAsync(Coleccion coleccion)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                // CORRECCIÓN CLAVE: En el INSERT ponemos 'DisenadorId' (sin ñ)
                string query = @"INSERT INTO Colecciones (NombreColeccion, Temporada, NumeroPiezas, PresupuestoInversion, EsLimitada, FechaLanzamiento, DisenadorId) 
                                 VALUES (@Nombre, @Temporada, @Piezas, @Presupuesto, @Limitada, @Fecha, @DisenadorId);
                                 SELECT LAST_INSERT_ID();";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nombre", coleccion.NombreColeccion);
                    command.Parameters.AddWithValue("@Temporada", coleccion.Temporada);
                    command.Parameters.AddWithValue("@Piezas", coleccion.NumeroPiezas);
                    command.Parameters.AddWithValue("@Presupuesto", coleccion.PresupuestoInversion);
                    command.Parameters.AddWithValue("@Limitada", coleccion.EsLimitada);
                    // Si la fecha viene vacía, ponemos la actual
                    command.Parameters.AddWithValue("@Fecha", coleccion.FechaLanzamiento == default ? DateTime.Now : coleccion.FechaLanzamiento);
                    
                    // Aquí usamos tu propiedad C# (con ñ) para rellenar el parámetro
                    command.Parameters.AddWithValue("@DisenadorId", coleccion.DiseñadorId);

                    var id = await command.ExecuteScalarAsync();
                    if (id != null) coleccion.Id = Convert.ToInt32(id);
                }
            }
            return coleccion;
        }

        // 4. UPDATE (PUT) -> También corregido por si acaso
        public async Task<Coleccion?> UpdateAsync(Coleccion coleccion)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                // CORRECCIÓN: 'DisenadorId' (sin ñ)
                string query = @"UPDATE Colecciones 
                                 SET NombreColeccion=@Nombre, Temporada=@Temporada, NumeroPiezas=@Piezas, 
                                     PresupuestoInversion=@Presupuesto, EsLimitada=@Limitada, DisenadorId=@DisenadorId 
                                 WHERE Id=@Id";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", coleccion.Id);
                    command.Parameters.AddWithValue("@Nombre", coleccion.NombreColeccion);
                    command.Parameters.AddWithValue("@Temporada", coleccion.Temporada);
                    command.Parameters.AddWithValue("@Piezas", coleccion.NumeroPiezas);
                    command.Parameters.AddWithValue("@Presupuesto", coleccion.PresupuestoInversion);
                    command.Parameters.AddWithValue("@Limitada", coleccion.EsLimitada);
                    command.Parameters.AddWithValue("@DisenadorId", coleccion.DiseñadorId);

                    int filas = await command.ExecuteNonQueryAsync();
                    if (filas == 0) return null;
                }
            }
            return coleccion;
        }

        // 5. DELETE
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