using CreditRiskAgent.Application.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CreditRiskAgent.Application.Siniestros.Queries.GetSiniestrosByPoliza;

public class GetSiniestrosByPolizaQueryHandler : IRequestHandler<GetSiniestrosByPolizaQuery, List<SiniestroDto>>
{
    private readonly IApplicationDbContext _context;

    public GetSiniestrosByPolizaQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<SiniestroDto>> Handle(GetSiniestrosByPolizaQuery request, CancellationToken cancellationToken)
    {
        return await _context.Siniestros
            .Where(s => s.Poliza.NumeroPoliza == request.NumeroPoliza)
            .Select(s => new SiniestroDto
            {                
                IdSiniestro = s.IdSiniestro,
                NumeroSiniestro = s.NumeroSiniestro,
                FechaOcurrencia = s.FechaOcurrencia,
                FechaReporte = s.FechaReporte,
                Descripcion = s.Descripcion,
                MontoReclamado = s.MontoReclamado,
                MontoPagado = s.MontoPagado
            })
            .ToListAsync(cancellationToken);
    }
}