using System.ComponentModel;
using Microsoft.SemanticKernel;
using MediatR;
using CreditRiskAgent.Application.Polizas.Queries.GetPolizaByNumero;
using CreditRiskAgent.Application.Siniestros.Queries.GetSiniestrosByPoliza;
using CreditRiskAgent.Application.Siniestros.Queries.GetPolizaBySiniestro;


namespace CreditRiskAgent.Application.Common.Plugins;

public class SegurosPlugin
{
    private readonly IMediator _mediator;

    public SegurosPlugin(IMediator mediator)
    {
        _mediator = mediator;
    }

    [KernelFunction, Description("Consulta los detalles y el estado de una póliza de seguro en la base de datos utilizando su número único.")]
    public async Task<PolizaDto?> ObtenerPolizaPorNumeroAsync(
        [Description("El número de la póliza que se desea consultar (por ejemplo: POL-001)")] string numeroPoliza)
    {
        var query = new GetPolizaByNumeroQuery(numeroPoliza);
        return await _mediator.Send(query);
    }
    [KernelFunction, Description("Consulta la lista de siniestros asociados a una póliza de seguro mediante su número.")]
    public async Task<List<SiniestroDto>> ObtenerSiniestrosPorPolizaAsync(
    [Description("El número de la póliza (por ejemplo: POL-AUT-2026-001)")] string numeroPoliza)
    {
        var query = new GetSiniestrosByPolizaQuery(numeroPoliza);
        return await _mediator.Send(query);
    }

    [KernelFunction, Description("Consulta los detalles de la póliza de seguro asociada a un número de siniestro específico.")]
    public async Task<PolizaDto?> ObtenerPolizaPorSiniestroAsync(
        [Description("El número del siniestro (por ejemplo: SIN-2026-089)")] string numeroSiniestro)
    {
        var query = new GetPolizaBySiniestroQuery(numeroSiniestro);
        return await _mediator.Send(query);
    }
}