// CreditRiskAgent.Application/Polizas/Queries/GetPolizaByNumero/PolizaDto.cs
namespace CreditRiskAgent.Application.Polizas.Queries.GetPolizaByNumero;

public class PolizaDto
{
    public int IdPoliza { get; set; }
    public string NumeroPoliza { get; set; } = string.Empty;
    public string Estado { get; set; } = string.Empty;
    public decimal MontoAsegurado { get; set; }
    public decimal Prima { get; set; }
    public string NombreAsegurado { get; set; } = string.Empty;
    public string NombreProducto { get; set; } = string.Empty;
}