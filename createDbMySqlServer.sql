/*******************************************************************************
   Creación de Base de Datos para Moda Lujo (Simplificada)
   NOTA: Se han eliminado ValorMercadoMillones y FechaAlianza
********************************************************************************/

-- 1. PREPARACIÓN
CREATE DATABASE IF NOT EXISTS ModaLujoDB;
USE ModaLujoDB;

SET FOREIGN_KEY_CHECKS = 0;
DROP TABLE IF EXISTS Eventos;
DROP TABLE IF EXISTS Prendas;
DROP TABLE IF EXISTS Colecciones;
DROP TABLE IF EXISTS Disenadores;
DROP TABLE IF EXISTS Marcas;
SET FOREIGN_KEY_CHECKS = 1;

-- --------------------------------------------------------

-- 2. TABLA MARCAS (Simplificada)
CREATE TABLE Marcas (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Nombre VARCHAR(50) NOT NULL,
    PaisOrigen VARCHAR(100) NOT NULL,
    AnioFundacion INT NOT NULL,
    EsAltaCostura BOOLEAN NOT NULL
);

-- 3. TABLA DISENADORES
CREATE TABLE Disenadores (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    NombreCompleto VARCHAR(100) NOT NULL,
    Especialidad VARCHAR(50) DEFAULT 'General',
    Edad INT NOT NULL,
    SalarioAnual DECIMAL(18,2) NOT NULL,
    EstaActivo BOOLEAN NOT NULL,
    FechaContratacion DATETIME NOT NULL,
    MarcaId INT NOT NULL,
    CONSTRAINT fk_disenador_marca FOREIGN KEY (MarcaId) REFERENCES Marcas(Id) ON DELETE CASCADE
);

-- 4. TABLA COLECCIONES
CREATE TABLE Colecciones (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    NombreColeccion VARCHAR(100) NOT NULL,
    Temporada VARCHAR(50) DEFAULT 'Invierno',
    NumeroPiezas INT NOT NULL,
    PresupuestoInversion DECIMAL(18,2) NOT NULL,
    EsLimitada BOOLEAN NOT NULL,
    FechaLanzamiento DATETIME NOT NULL,
    DisenadorId INT NOT NULL,
    CONSTRAINT fk_coleccion_disenador FOREIGN KEY (DisenadorId) REFERENCES Disenadores(Id) ON DELETE CASCADE
);

-- 5. TABLA PRENDAS
CREATE TABLE Prendas (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Tipo VARCHAR(50) NOT NULL,
    MaterialPrincipal VARCHAR(50),
    TallaNumerica INT NOT NULL,
    PrecioVenta DECIMAL(18,2) NOT NULL,
    EnStock BOOLEAN NOT NULL,
    FechaFabricacion DATETIME NOT NULL,
    ColeccionId INT NOT NULL,
    CONSTRAINT fk_prenda_coleccion FOREIGN KEY (ColeccionId) REFERENCES Colecciones(Id) ON DELETE CASCADE
);

-- 6. TABLA EVENTOS
CREATE TABLE Eventos (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Ciudad VARCHAR(100) NOT NULL,
    UbicacionExacta VARCHAR(200),
    CapacidadAsistentes INT NOT NULL,
    CosteEntrada DECIMAL(18,2) NOT NULL,
    EsBenefico BOOLEAN NOT NULL,
    FechaEvento DATETIME NOT NULL,
    ColeccionId INT NOT NULL,
    CONSTRAINT fk_evento_coleccion FOREIGN KEY (ColeccionId) REFERENCES Colecciones(Id) ON DELETE CASCADE
);

-- --------------------------------------------------------
-- DATOS DE EJEMPLO
-- --------------------------------------------------------

-- 1. Marca: Versace (Sin los campos extra)
INSERT INTO Marcas (Nombre, PaisOrigen, AnioFundacion, EsAltaCostura)
VALUES ('Versace', 'Italia', 1978, 1);

-- 2. Diseñador
INSERT INTO Disenadores (NombreCompleto, Especialidad, Edad, SalarioAnual, EstaActivo, FechaContratacion, MarcaId)
VALUES ('Donatella Versace', 'Alta Costura', 67, 5000000.00, 1, NOW(), 1);

-- 3. Colección
INSERT INTO Colecciones (NombreColeccion, Temporada, NumeroPiezas, PresupuestoInversion, EsLimitada, FechaLanzamiento, DisenadorId)
VALUES ('Medusa Power', 'Primavera-Verano', 45, 150000.00, 1, NOW(), 1);

-- 4. Prenda
INSERT INTO Prendas (Tipo, MaterialPrincipal, TallaNumerica, PrecioVenta, EnStock, FechaFabricacion, ColeccionId)
VALUES ('Vestido de Noche', 'Seda', 38, 1200.50, 1, NOW(), 1);

-- 5. Evento
INSERT INTO Eventos (Ciudad, UbicacionExacta, CapacidadAsistentes, CosteEntrada, EsBenefico, FechaEvento, ColeccionId)
VALUES ('Milan', 'Via Gesu 12', 500, 0.00, 0, NOW(), 1);