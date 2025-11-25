-- 1. PREPARACIÓN (Borrar todo para empezar limpio)
CREATE DATABASE IF NOT EXISTS ModaDB;
USE ModaDB;

SET FOREIGN_KEY_CHECKS = 0;
DROP TABLE IF EXISTS Eventos;
DROP TABLE IF EXISTS Prendas;
DROP TABLE IF EXISTS Colecciones;
DROP TABLE IF EXISTS Diseñadores;
DROP TABLE IF EXISTS Marcas;
SET FOREIGN_KEY_CHECKS = 1;

-- --------------------------------------------------------

-- 2. TABLA MARCAS
-- Coincide con tu clase Marca.cs
CREATE TABLE Marcas (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Nombre VARCHAR(50) NOT NULL,
    PaisOrigen VARCHAR(100) NOT NULL,
    AnioFundacion INT NOT NULL,
    ValorMercadoMillones DECIMAL(18,2) NOT NULL,
    EsAltaCostura BOOLEAN NOT NULL,
    FechaAlianza DATETIME NOT NULL
);

-- 3. TABLA DISEÑADORES
-- Coincide con tu clase Diseñador.cs (tiene MarcaId)
CREATE TABLE Diseñadores (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    NombreCompleto VARCHAR(100) NOT NULL,
    Especialidad VARCHAR(50) DEFAULT 'General',
    Edad INT NOT NULL,
    SalarioAnual DECIMAL(18,2) NOT NULL,
    EstaActivo BOOLEAN NOT NULL,
    FechaContratacion DATETIME NOT NULL,
    MarcaId INT NOT NULL,
    CONSTRAINT fk_diseñador_marca FOREIGN KEY (MarcaId) REFERENCES Marcas(Id) ON DELETE CASCADE
);

-- 4. TABLA COLECCIONES
-- Coincide con tu clase Coleccion.cs (tiene DiseñadorId)
CREATE TABLE Colecciones (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    NombreColeccion VARCHAR(100) NOT NULL,
    Temporada VARCHAR(50) DEFAULT 'Invierno',
    NumeroPiezas INT NOT NULL,
    PresupuestoInversion DECIMAL(18,2) NOT NULL,
    EsLimitada BOOLEAN NOT NULL,
    FechaLanzamiento DATETIME NOT NULL,
    DiseñadorId INT NOT NULL,
    CONSTRAINT fk_coleccion_diseñador FOREIGN KEY (DiseñadorId) REFERENCES Diseñadores(Id) ON DELETE CASCADE
);

-- 5. TABLA PRENDAS
-- Coincide con tu clase Prenda.cs (tiene ColeccionId)
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
-- Coincide con tu clase Evento.cs (tiene ColeccionId)
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
-- DATOS DE EJEMPLO (Para que no esté vacía al probar)
-- --------------------------------------------------------

INSERT INTO Marcas (Nombre, PaisOrigen, AnioFundacion, ValorMercadoMillones, EsAltaCostura, FechaAlianza)
VALUES ('Versace', 'Italia', 1978, 2000.00, 1, NOW());

-- El ID de Versace será 1
INSERT INTO Diseñadores (NombreCompleto, Especialidad, Edad, SalarioAnual, EstaActivo, FechaContratacion, MarcaId)
VALUES ('Donatella Versace', 'Alta Costura', 67, 5000000.00, 1, NOW(), 1);

-- El ID de Donatella será 1
INSERT INTO Colecciones (NombreColeccion, Temporada, NumeroPiezas, PresupuestoInversion, EsLimitada, FechaLanzamiento, DiseñadorId)
VALUES ('Medusa Power', 'Primavera-Verano', 45, 150000.00, 1, NOW(), 1);