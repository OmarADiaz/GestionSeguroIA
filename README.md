# AI Insurance Agent (.NET 8, CQRS, MediatR & Semantic Kernel)

Un agente de inteligencia artificial conversacional desarrollado en **.NET 8** capaz de interactuar mediante lenguaje natural con una base de datos relacional en SQL Server. El sistema utiliza **Semantic Kernel** con **Google Gemini** y ejecuta consultas tipadas y seguras utilizando el patrón **CQRS con MediatR**.

---

## Características Principales

* **Asistente Conversacional Inteligente:** Integración con Google Gemini a través de Semantic Kernel utilizando *Function Calling* automático.
* **Arquitectura Limpia y CQRS:** Separación estricta de responsabilidades mediante **MediatR** para el manejo de Queries, Handlers y DTOs.
* **Persistencia Robusta:** Conexión a **SQL Server** mediante **Entity Framework Core** para la gestión de pólizas, asegurados, productos y siniestros.
* **Resiliencia:** Políticas de reintento configuradas con **Polly** para el manejo de errores transitorios en llamadas HTTP.

---

## Tecnologías y Librerías

* **.NET 8 / C#**
* **Entity Framework Core** (SQL Server)
* **MediatR** (Patrón CQRS)
* **Microsoft Semantic Kernel**
* **Polly** (Políticas de reintento)
* **ASP.NET Core Web API & Swagger**

---

## Estructura del Proyecto

```text
GestionSeguroIA/
│
├── src/
│   ├── CreditRiskAgent.Api/            # Capa de Presentación: Controladores REST, Swagger y configuración del Kernel/Mediator en Program.cs.
│   ├── CreditRiskAgent.Application/    # Capa de Aplicación: Casos de uso (Queries/Handlers MediatR), DTOs y Plugins de IA para Semantic Kernel.
│   ├── CreditRiskAgent.Domain/         # Capa de Dominio: Entidades del negocio (Poliza, Asegurado, Siniestro, CoberturaPoliza, Producto, Ramo).
│   └── CreditRiskAgent.Infrastructure/ # Capa de Infraestructura: Persistencia con Entity Framework Core (SegurosDbContext) y acceso a SQL Server.
│
├── Script/                             # Scripts SQL para la creación y poblamiento de la base de datos.
├── Script_BAT/                         # Scripts de automatización y ejecución.
└── CreditRiskAgent.sln                 # Solución principal de Visual Studio.

### Restauración de Paquetes NuGet

Para descargar y restaurar automáticamente todas las dependencias del proyecto (.NET SDK, MediatR, Semantic Kernel, Entity Framework, etc.) sin necesidad de abrir Visual Studio, puedes ejecutar el script de PowerShell incluido en la raíz de la solución:

1. **Abrir PowerShell en la raíz del proyecto** y ejecutar:
   ```powershell
   ./restaurar-paquetes.ps1