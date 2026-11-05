using MediatR;
using CreditRiskAgent.Application.Polizas.Queries.GetPolizaByNumero;

namespace CreditRiskAgent.Application.Siniestros.Queries.GetPolizaBySiniestro;

public record GetPolizaBySiniestroQuery(string NumeroSiniestro) : IRequest<PolizaDto?>;