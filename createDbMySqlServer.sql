CREATE DATABASE IF NOT EXISTS ModaDB;
USE ModaDB;

-- 1. Tabla MARCAS
-- Coincide con Models/Marca.cs
CREATE TABLE IF NOT EXISTS Marcas (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Nombre VARCHAR(50) NOT NULL UNIQUE, -- [Required] y StringLength(50)
    PaisOrigen VARCHAR(100) NOT NULL,   -- [Required]
    AnioFundacion INT,                  -- [Range(1800, 2025)]
    ValorMercadoMillones DECIMAL(10, 2),
    EsAltaCostura BOOLEAN,              -- bool
    FechaAlianza DATETIME               -- DateTime
);

-- 2. Tabla SEDES
-- Coincide con Models/Sede.cs
CREATE TABLE IF NOT EXISTS Sedes (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    NombreRecinto VARCHAR(100) NOT NULL, -- [Required]
    Ciudad VARCHAR(100) NOT NULL,        -- [Required]
    AforoMaximo INT,                     -- [Range(10, 50000)]
    CosteAlquilerPorHora DECIMAL(10, 2),
    TieneZonaVip BOOLEAN,
    UltimaInspeccionSeguridad DATETIME
);

-- 3. Tabla USUARIOS
-- Coincide con Models/Usuario.cs
CREATE TABLE IF NOT EXISTS Usuarios (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    NombreCompleto VARCHAR(100) NOT NULL,
    Email VARCHAR(100) NOT NULL UNIQUE, -- [EmailAddress]
    PuntosFidelidad INT DEFAULT 0,      -- Inicializado a 0 en C#
    SaldoEnCartera DECIMAL(10, 2) DEFAULT 0.00,
    EsClienteCeleb BOOLEAN DEFAULT FALSE,
    FechaRegistro DATETIME DEFAULT CURRENT_TIMESTAMP
);

-- 4. Tabla EVENTOS
-- Coincide con Models/Evento.cs
CREATE TABLE IF NOT EXISTS Eventos (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Titulo VARCHAR(150) NOT NULL,       -- [Required]
    DescripcionActividades TEXT,
    EntradasDisponibles INT,
    PrecioEntradaBase DECIMAL(10, 2),
    EsEventoPrivado BOOLEAN,
    FechaInicio DATETIME NOT NULL,
    FechaFin DATETIME NOT NULL,
    
    -- Relaciones (Claves Foráneas)
    MarcaId INT NOT NULL,
    SedeId INT NOT NULL,
    
    CONSTRAINT FK_Eventos_Marcas FOREIGN KEY (MarcaId) REFERENCES Marcas(Id) ON DELETE CASCADE,
    CONSTRAINT FK_Eventos_Sedes FOREIGN KEY (SedeId) REFERENCES Sedes(Id) ON DELETE CASCADE
);

-- 5. Tabla ENTRADAS
-- Coincide con Models/Entrada.cs
CREATE TABLE IF NOT EXISTS Entradas (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    CodigoQr VARCHAR(255) NOT NULL,
    TipoAsiento VARCHAR(50) DEFAULT 'General', -- Inicializado a "General" en C#
    PrecioFinalPagado DECIMAL(10, 2),
    NumeroAsiento INT,
    IncluyeMeetAndGreet BOOLEAN DEFAULT FALSE,
    FechaCompra DATETIME DEFAULT CURRENT_TIMESTAMP,
    
    -- Relaciones (Claves Foráneas)
    UsuarioId INT NOT NULL,
    EventoId INT NOT NULL,
    
    CONSTRAINT FK_Entradas_Usuarios FOREIGN KEY (UsuarioId) REFERENCES Usuarios(Id) ON DELETE CASCADE,
    CONSTRAINT FK_Entradas_Eventos FOREIGN KEY (EventoId) REFERENCES Eventos(Id) ON DELETE CASCADE
);