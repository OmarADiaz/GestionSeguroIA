using MediatR;

namespace CreditRiskAgent.Application.Siniestros.Queries.GetSiniestrosByPoliza;

public record GetSiniestrosByPolizaQuery(string NumeroPoliza) : IRequest<List<SiniestroDto>>;