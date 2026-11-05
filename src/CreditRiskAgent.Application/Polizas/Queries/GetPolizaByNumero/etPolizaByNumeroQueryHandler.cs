
using CreditRiskAgent.Application.Common;
using CreditRiskAgent.Application.Polizas.Queries.GetPolizaByNumero;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

public class GetPolizaByNumeroQueryHandler
    : IRequestHandler<GetPolizaByNumeroQuery, PolizaDto?>
{
    private readonly IApplicationDbContext _context;
    private readonly ILogger<GetPolizaByNumeroQueryHandler> _logger;

    public GetPolizaByNumeroQueryHandler(
        IApplicationDbContext context,
        ILogger<GetPolizaByNumeroQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<PolizaDto?> Handle(
        GetPolizaByNumeroQuery request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "INICIO consulta de póliza: {NumeroPoliza}",
            request.NumeroPoliza);

        try
        {
            if (string.IsNullOrWhiteSpace(request.NumeroPoliza))
                throw new ArgumentException("El número de póliza está vacío.");

            var poliza = await _context.Polizas
                .AsNoTracking()
                .Include(p => p.Asegurado)
                .Include(p => p.Producto)
                .Where(p => p.NumeroPoliza == request.NumeroPoliza)
                .Select(p => new PolizaDto
                {
                    IdPoliza = p.IdPoliza,
                    NumeroPoliza = p.NumeroPoliza,
                    Estado = p.Estado,
                    MontoAsegurado = p.MontoAsegurado,
                    Prima = p.Prima,
                    NombreAsegurado =
                        $"{p.Asegurado.Nombres} {p.Asegurado.Apellidos}",
                    NombreProducto = p.Producto.NombreProducto
                })
                .FirstOrDefaultAsync(cancellationToken);

            if (poliza == null)
            {
                _logger.LogWarning(
                    "No se encontró la póliza: {NumeroPoliza}",
                    request.NumeroPoliza);

                return null;
            }

            _logger.LogInformation(
                "CONSULTA EXITOSA. Póliza: {NumeroPoliza}, Asegurado: {Asegurado}",
                poliza.NumeroPoliza,
                poliza.NombreAsegurado);

            return poliza;
        }
        catch (OperationCanceledException ex)
        {
            _logger.LogWarning(
                ex,
                "Consulta cancelada para la póliza {NumeroPoliza}",
                request.NumeroPoliza);

            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "ERROR consultando póliza {NumeroPoliza}",
                request.NumeroPoliza);

            throw;
        }
    }
}
