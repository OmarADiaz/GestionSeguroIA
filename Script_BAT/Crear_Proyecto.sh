# 1. Crear la solución
dotnet new sln -n CreditRiskAgent

# 2. Crear los proyectos en .NET 8
dotnet new classlib -n CreditRiskAgent.Domain -o src/CreditRiskAgent.Domain --framework net8.0
dotnet new classlib -n CreditRiskAgent.Application -o src/CreditRiskAgent.Application --framework net8.0
dotnet new classlib -n CreditRiskAgent.Infrastructure -o src/CreditRiskAgent.Infrastructure --framework net8.0
dotnet new webapi -n CreditRiskAgent.Api -o src/CreditRiskAgent.Api --framework net8.0

# 3. Agregarlos a la solución
dotnet sln CreditRiskAgent.sln add src/CreditRiskAgent.Domain/CreditRiskAgent.Domain.csproj
dotnet sln CreditRiskAgent.sln add src/CreditRiskAgent.Application/CreditRiskAgent.Application.csproj
dotnet sln CreditRiskAgent.sln add src/CreditRiskAgent.Infrastructure/CreditRiskAgent.Infrastructure.csproj
dotnet sln CreditRiskAgent.sln add src/CreditRiskAgent.Api/CreditRiskAgent.Api.csproj

# 4. Establecer dependencias
dotnet add src/CreditRiskAgent.Application/CreditRiskAgent.Application.csproj reference src/CreditRiskAgent.Domain/CreditRiskAgent.Domain.csproj
dotnet add src/CreditRiskAgent.Infrastructure/CreditRiskAgent.Infrastructure.csproj reference src/CreditRiskAgent.Application/CreditRiskAgent.Application.csproj
dotnet add src/CreditRiskAgent.Api/CreditRiskAgent.Api.csproj reference src/CreditRiskAgent.Application/CreditRiskAgent.Application.csproj
dotnet add src/CreditRiskAgent.Api/CreditRiskAgent.Api.csproj reference src/CreditRiskAgent.Infrastructure/CreditRiskAgent.Infrastructure.csproj