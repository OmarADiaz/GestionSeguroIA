using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditRiskAgent.Domain.Entities
{
    public class Siniestro
    {
        [Key]
        public int IdSiniestro { get; set; }
        public string NumeroSiniestro { get; set; } = string.Empty;
        public int IdPoliza { get; set; }
        public DateTime FechaOcurrencia { get; set; }
        public DateTime FechaReporte { get; set; } = DateTime.UtcNow;
        public string Descripcion { get; set; } = string.Empty;
        public decimal MontoReclamado { get; set; }
        public decimal MontoPagado { get; set; }
        public string EstadoSiniestro { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

        public Poliza Poliza { get; set; } = null!;
    }
}
