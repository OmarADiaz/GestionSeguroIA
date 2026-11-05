using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditRiskAgent.Domain.Entities
{
    public class Poliza
    {
        [Key]
        public int IdPoliza { get; set; }
        public string NumeroPoliza { get; set; } = string.Empty;
        public int IdAsegurado { get; set; }
        public int IdProducto { get; set; }
        public string Estado { get; set; } = string.Empty;
        public decimal MontoAsegurado { get; set; }
        public decimal Prima { get; set; }
        public DateOnly VigenciaInicio { get; set; }
        public DateOnly VigenciaFin { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

        public Asegurado Asegurado { get; set; } = null!;
        public Producto Producto { get; set; } = null!;
        public ICollection<CoberturaPoliza> Coberturas { get; set; } = new List<CoberturaPoliza>();
        public ICollection<Siniestro> Siniestros { get; set; } = new List<Siniestro>();
    }
}
