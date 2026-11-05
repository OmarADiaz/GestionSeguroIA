// CreditRiskAgent.Application/Common/Interfaces/IApplicationDbContext.cs
using Microsoft.EntityFrameworkCore;
using CreditRiskAgent.Domain.Entities;

namespace CreditRiskAgent.Application.Common;

public interface IApplicationDbContext
{
    DbSet<Asegurado> Asegurados { get; }
    DbSet<Ramo> Ramos { get; }
    DbSet<Producto> Productos { get; }
    DbSet<Poliza> Polizas { get; }
    DbSet<CoberturaPoliza> CoberturasPoliza { get; }
    DbSet<Siniestro> Siniestros { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}