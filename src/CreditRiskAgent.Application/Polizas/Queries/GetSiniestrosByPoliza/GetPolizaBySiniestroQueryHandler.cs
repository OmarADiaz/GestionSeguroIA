using CreditRiskAgent.Application.Common;
using CreditRiskAgent.Application.Polizas.Queries.GetPolizaByNumero;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CreditRiskAgent.Application.Siniestros.Queries.GetPolizaBySiniestro;

public class GetPolizaBySiniestroQueryHandler : IRequestHandler<GetPolizaBySiniestroQuery, PolizaDto?>
{
    private readonly IApplicationDbContext _context;

    public GetPolizaBySiniestroQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PolizaDto?> Handle(GetPolizaBySiniestroQuery request, CancellationToken cancellationToken)
    {
        return await _context.Siniestros
            .Where(s => s.NumeroSiniestro == request.NumeroSiniestro)
            .Select(s => s.Poliza)
            .Select(p => new PolizaDto
            {
                IdPoliza = p.IdPoliza,
                NumeroPoliza = p.NumeroPoliza,
                Estado = p.Estado,
                MontoAsegurado = p.MontoAsegurado,
                Prima = p.Prima,
                NombreAsegurado = $"{p.Asegurado.Nombres} {p.Asegurado.Apellidos}",
                NombreProducto = p.Producto.NombreProducto
            })
            .FirstOrDefaultAsync(cancellationToken);
    }
}