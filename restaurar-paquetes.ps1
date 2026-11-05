# ==========================================
# Script para Restaurar Paquetes NuGet
# Solución: GestionSeguroIA / CreditRiskAgent
# ==========================================

Write-Host "=== Iniciando la restauración de paquetes NuGet ===" -ForegroundColor Cyan

# 1. Verificar si .NET CLI está instalado
if (!(Get-Command "dotnet" -ErrorAction SilentlyContinue)) {
    Write-Host "[ERROR] El SDK de .NET no está instalado o no se encuentra en el PATH del sistema." -ForegroundColor Red
    Write-Host "Por favor, instala .NET 8 SDK desde https://dotnet.microsoft.com/" -ForegroundColor Yellow
    Pause
    Exit 1
}

# 2. Ubicar la ruta del script y posicionarse en la raíz
$scriptDir = Split-Path -Parent $MyInvocation.MyCommand.Definition
Set-Location $scriptDir

# 3. Buscar automáticamente el archivo de solución (.sln)
$slnFile = Get-ChildItem -Filter "*.sln" | Select-Object -First 1

if ($null -eq $slnFile) {
    Write-Host "[ERROR] No se encontró ningún archivo de solución (.sln) en esta carpeta." -ForegroundColor Red
    Pause
    Exit 1
}

Write-Host "Solución detectada: $($slnFile.Name)" -ForegroundColor Green
Write-Host "Ejecutando 'dotnet restore'..." -ForegroundColor Yellow

# 4. Ejecutar la restauración de NuGet
dotnet restore $slnFile.FullName

# 5. Validar resultado
if ($LASTEXITCODE -eq 0) {
    Write-Host "`n[ÉXITO] ¡Todos los paquetes NuGet se han restaurado correctamente!" -ForegroundColor Green
} else {
    Write-Host "`n[ERROR] Ocurrió un problema al restaurar los paquetes." -ForegroundColor Red
}

Write-Host "`nProceso finalizado." -ForegroundColor Cyan