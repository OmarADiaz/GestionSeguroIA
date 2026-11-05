USE SegurosEnterpriseDb;
GO

-- ====================================================================
-- 1. Inserción de Ramos Maestros
-- ====================================================================
INSERT INTO dbo.Ramos (CodigoRamo, NombreRamo, Descripcion) VALUES
('AUT', 'Automóviles', 'Seguros para vehículos particulares, comerciales y flotas'),
('VID', 'Vida', 'Pólizas de vida individual, colectiva y accidentes personales'),
('HOG', 'Hogar', 'Protección integral para vivienda, incendio y terremoto'),
('SAL', 'Salud', 'Pólizas de asistencia médica, hospitalaria y prepagada');
GO

-- ====================================================================
-- 2. Inserción de Productos (Vinculados a sus Ramos)
-- ====================================================================
INSERT INTO dbo.Productos (IdRamo, CodigoProducto, NombreProducto, Descripcion) VALUES
((SELECT IdRamo FROM dbo.Ramos WHERE CodigoRamo = 'AUT'), 'AUT-01', 'Todo Riesgo Élite', 'Cobertura total con vehículo de reemplazo y asistencia 24/7'),
((SELECT IdRamo FROM dbo.Ramos WHERE CodigoRamo = 'AUT'), 'AUT-02', 'Básico Obligatorio', 'Amparo de responsabilidad civil extracontractual y grúa básica'),
((SELECT IdRamo FROM dbo.Ramos WHERE CodigoRamo = 'VID'), 'VID-01', 'Vida Grupo Empresarial', 'Amparo por fallecimiento, incapacidad y enfermedades graves'),
((SELECT IdRamo FROM dbo.Ramos WHERE CodigoRamo = 'HOG'), 'HOG-01', 'Hogar Protegido Plus', 'Cobertura contra incendio, terremoto, asalto y daños por agua'),
((SELECT IdRamo FROM dbo.Ramos WHERE CodigoRamo = 'SAL'), 'SAL-01', 'Salud Global Premium', 'Medicina prepagada con cobertura y red médica internacional');
GO

-- ====================================================================
-- 3. Inserción de Asegurados (Clientes / Tomadores)
-- ====================================================================
INSERT INTO dbo.Asegurados (TipoDocumento, NumeroDocumento, Nombres, Apellidos, Correo, Telefono) VALUES
('CC', '52345678', 'María', 'Gomez Pérez', 'maria.gomez@email.com', '3101234567'),
('CC', '79876543', 'Carlos', 'Mendoza Silva', 'carlos.mendoza@email.com', '3209876543'),
('CC', '35412789', 'Ana Lucía', 'Torres Rincón', 'ana.torres@email.com', '3154567890'),
('NIT', '900123456', 'Logística y Transportes Andina', 'SAS', 'contacto@logisticaandina.com', '6012345678');
GO

-- ====================================================================
-- 4. Inserción de Pólizas Transaccionales
-- ====================================================================
INSERT INTO dbo.Polizas (NumeroPoliza, IdAsegurado, IdProducto, Estado, MontoAsegurado, Prima, VigenciaInicio, VigenciaFin) VALUES
('POL-AUT-2026-001', 
 (SELECT IdAsegurado FROM dbo.Asegurados WHERE NumeroDocumento = '79876543'), 
 (SELECT IdProducto FROM dbo.Productos WHERE CodigoProducto = 'AUT-01'), 
 'Activa', 65000000.00, 2400000.00, '2026-01-15', '2027-01-15'),

('POL-VID-2026-042', 
 (SELECT IdAsegurado FROM dbo.Asegurados WHERE NumeroDocumento = '52345678'), 
 (SELECT IdProducto FROM dbo.Productos WHERE CodigoProducto = 'VID-01'), 
 'Activa', 150000000.00, 1800000.00, '2026-03-01', '2027-03-01'),

('POL-HOG-2025-110', 
 (SELECT IdAsegurado FROM dbo.Asegurados WHERE NumeroDocumento = '35412789'), 
 (SELECT IdProducto FROM dbo.Productos WHERE CodigoProducto = 'HOG-01'), 
 'Activa', 200000000.00, 950000.00, '2025-10-10', '2026-10-10');
GO

-- ====================================================================
-- 5. Inserción de Coberturas asociadas a las Pólizas
-- ====================================================================
INSERT INTO dbo.CoberturasPoliza (IdPoliza, NombreCobertura, SumaAsegurada, Deducible) VALUES
((SELECT IdPoliza FROM dbo.Polizas WHERE NumeroPoliza = 'POL-AUT-2026-001'), 'Amparo Básico Daños y Pérdida Total', 65000000.00, '10% mínimo 1 SMMLV'),
((SELECT IdPoliza FROM dbo.Polizas WHERE NumeroPoliza = 'POL-AUT-2026-001'), 'Terremoto y Fenómenos de la Naturaleza', 65000000.00, '10% de la pérdida'),
((SELECT IdPoliza FROM dbo.Polizas WHERE NumeroPoliza = 'POL-AUT-2026-001'), 'Asistencia Vial y Grúa 24/7', 2000000.00, 'Sin deducible'),
((SELECT IdPoliza FROM dbo.Polizas WHERE NumeroPoliza = 'POL-VID-2026-042'), 'Fallecimiento por Cualquier Causa', 150000000.00, 'Sin deducible'),
((SELECT IdPoliza FROM dbo.Polizas WHERE NumeroPoliza = 'POL-HOG-2025-110'), 'Incendio, Rayo y Explosión', 200000000.00, '5 SMMLV');
GO

-- ====================================================================
-- 6. Inserción de Historial de Siniestros
-- ====================================================================
INSERT INTO dbo.Siniestros (NumeroSiniestro, IdPoliza, FechaOcurrencia, Descripcion, MontoReclamado, MontoPagado, EstadoSiniestro) VALUES
('SIN-2026-089', 
 (SELECT IdPoliza FROM dbo.Polizas WHERE NumeroPoliza = 'POL-AUT-2026-001'), 
 '2026-05-12 14:30:00', 
 'Inundación parcial en vía pública afectando motor y sistema eléctrico.', 
 4500000.00, 4050000.00, 'Pagado'),

('SIN-2026-150', 
 (SELECT IdPoliza FROM dbo.Polizas WHERE NumeroPoliza = 'POL-AUT-2026-001'), 
 '2026-08-20 09:15:00', 
 'Colisión menor en intersección, daños en bómper y farola delantera.', 
 1800000.00, 0.00, 'En Estudio');
GO