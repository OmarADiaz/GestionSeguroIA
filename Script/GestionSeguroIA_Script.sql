-- ====================================================================
-- Creación de la Base de Datos para el Agente de Seguros
-- ====================================================================
CREATE DATABASE SegurosEnterpriseDb;
GO

USE SegurosEnterpriseDb;
GO

-- 1. Tabla de Asegurados (Clientes / Tomadores)
CREATE TABLE dbo.Asegurados (
    IdAsegurado INT IDENTITY(1,1) PRIMARY KEY,
    TipoDocumento VARCHAR(10) NOT NULL, -- CC, NIT, CE, PAS
    NumeroDocumento VARCHAR(20) NOT NULL,
    Nombres VARCHAR(100) NOT NULL,
    Apellidos VARCHAR(100) NOT NULL,
    Correo VARCHAR(150) NULL,
    Telefono VARCHAR(30) NULL,
    FechaCreacion DATETIME2(7) NOT NULL CONSTRAINT DF_Asegurados_FechaCreacion DEFAULT GETDATE(),
    
    CONSTRAINT UQ_Asegurados_Documento UNIQUE (TipoDocumento, NumeroDocumento)
);
GO

-- 2. Tabla Maestra de Ramos (Ej: Autos, Vida, Hogar, ARL, Salud)
CREATE TABLE dbo.Ramos (
    IdRamo INT IDENTITY(1,1) PRIMARY KEY,
    CodigoRamo VARCHAR(30) NOT NULL,
    NombreRamo VARCHAR(100) NOT NULL,
    Descripcion VARCHAR(255) NULL,
    Activo BIT NOT NULL CONSTRAINT DF_Ramos_Activo DEFAULT 1,

    CONSTRAINT UQ_Ramos_Codigo UNIQUE (CodigoRamo)
);
GO

-- 3. Tabla Maestra de Productos (Ej: Todo Riesgo Élite, Vida Grupo, etc.)
CREATE TABLE dbo.Productos (
    IdProducto INT IDENTITY(1,1) PRIMARY KEY,
    IdRamo INT NOT NULL,
    CodigoProducto VARCHAR(50) NOT NULL,
    NombreProducto VARCHAR(150) NOT NULL,
    Descripcion VARCHAR(255) NULL,
    Activo BIT NOT NULL CONSTRAINT DF_Productos_Activo DEFAULT 1,

    CONSTRAINT FK_Productos_Ramo FOREIGN KEY (IdRamo) REFERENCES dbo.Ramos(IdRamo),
    CONSTRAINT UQ_Productos_Codigo UNIQUE (CodigoProducto)
);
GO

-- 4. Tabla de Pólizas (Transaccional)
CREATE TABLE dbo.Polizas (
    IdPoliza INT IDENTITY(1,1) PRIMARY KEY,
    NumeroPoliza VARCHAR(50) NOT NULL,
    IdAsegurado INT NOT NULL,
    IdProducto INT NOT NULL, 
    Estado VARCHAR(30) NOT NULL, -- Activa, Cancelada, Suspendida, Vencida
    MontoAsegurado DECIMAL(18,2) NOT NULL,
    Prima DECIMAL(18,2) NOT NULL,
    VigenciaInicio DATE NOT NULL,
    VigenciaFin DATE NOT NULL,
    FechaCreacion DATETIME2(7) NOT NULL CONSTRAINT DF_Polizas_FechaCreacion DEFAULT GETDATE(),

    CONSTRAINT FK_Polizas_Asegurado FOREIGN KEY (IdAsegurado) REFERENCES dbo.Asegurados(IdAsegurado),
    CONSTRAINT FK_Polizas_Producto FOREIGN KEY (IdProducto) REFERENCES dbo.Productos(IdProducto),
    CONSTRAINT UQ_Polizas_NumeroPoliza UNIQUE (NumeroPoliza),
    CONSTRAINT CK_Polizas_Vigencia CHECK (VigenciaFin >= VigenciaInicio)
);
GO

-- 5. Tabla de Coberturas por Póliza
CREATE TABLE dbo.CoberturasPoliza (
    IdCoberturaPoliza INT IDENTITY(1,1) PRIMARY KEY,
    IdPoliza INT NOT NULL,
    NombreCobertura VARCHAR(100) NOT NULL, -- Ej: "Amparo Básico", "Terremoto", "Asistencia Vial"
    SumaAsegurada DECIMAL(18,2) NOT NULL,
    Deducible VARCHAR(100) NOT NULL, -- Ej: "10% del valor de la pérdida, mínimo 1 SMMLV"
    
    CONSTRAINT FK_Coberturas_Poliza FOREIGN KEY (IdPoliza) REFERENCES dbo.Polizas(IdPoliza) ON DELETE CASCADE
);
GO

-- 6. Tabla de Siniestros (Historial de Reclamos)
CREATE TABLE dbo.Siniestros (
    IdSiniestro INT IDENTITY(1,1) PRIMARY KEY,
    NumeroSiniestro VARCHAR(50) NOT NULL,
    IdPoliza INT NOT NULL,
    FechaOcurrencia DATETIME2(7) NOT NULL,
    FechaReporte DATETIME2(7) NOT NULL CONSTRAINT DF_Siniestros_FechaReporte DEFAULT GETDATE(),
    Descripcion VARCHAR(MAX) NOT NULL,
    MontoReclamado DECIMAL(18,2) NOT NULL,
    MontoPagado DECIMAL(18,2) CONSTRAINT DF_Siniestros_MontoPagado DEFAULT 0.00,
    EstadoSiniestro VARCHAR(30) NOT NULL, -- En Estudio, Pagado, Objetado, Cerrado
    FechaCreacion DATETIME2(7) NOT NULL CONSTRAINT DF_Siniestros_FechaCreacion DEFAULT GETDATE(),

    CONSTRAINT FK_Siniestros_Poliza FOREIGN KEY (IdPoliza) REFERENCES dbo.Polizas(IdPoliza),
    CONSTRAINT UQ_Siniestros_Numero UNIQUE (NumeroSiniestro)
);
GO

-- ====================================================================
-- Índices Estratégicos para Optimización de Consultas del Agente de IA
-- ====================================================================

CREATE NONCLUSTERED INDEX IX_Asegurados_NumeroDocumento 
ON dbo.Asegurados (NumeroDocumento);
GO

CREATE NONCLUSTERED INDEX IX_Polizas_Numero_Estado 
ON dbo.Polizas (NumeroPoliza) 
INCLUDE (IdAsegurado, IdProducto, Estado, VigenciaInicio, VigenciaFin);
GO

CREATE NONCLUSTERED INDEX IX_Productos_Ramo 
ON dbo.Productos (IdRamo);
GO

CREATE NONCLUSTERED INDEX IX_Siniestros_IdPoliza 
ON dbo.Siniestros (IdPoliza) 
INCLUDE (NumeroSiniestro, EstadoSiniestro, MontoPagado);
GO