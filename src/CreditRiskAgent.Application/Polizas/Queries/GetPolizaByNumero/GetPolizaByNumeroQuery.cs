using MediatR;
namespace CreditRiskAgent.Application.Polizas.Queries.GetPolizaByNumero;

public record GetPolizaByNumeroQuery(string NumeroPoliza): IRequest<PolizaDto>;

