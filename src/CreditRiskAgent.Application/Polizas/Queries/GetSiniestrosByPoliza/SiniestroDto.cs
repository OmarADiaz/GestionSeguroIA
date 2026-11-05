namespace CreditRiskAgent.Application.Polizas.Queries.GetSiniestrosByPoliza;
public class SiniestroDto
{
    public int IdSiniestro { get; set; }
    public string NumeroSiniestro { get; set; } = string.Empty;
    public DateTime FechaOcurrencia { get; set; }
    public DateTime FechaReporte { get; set; }
    public string Descripcion { get; set; } = string.Empty;
    public decimal MontoReclamado { get; set; }
    public decimal MontoPagado { get; set; }
}