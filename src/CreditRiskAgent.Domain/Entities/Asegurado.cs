using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditRiskAgent.Domain.Entities
{
    public class Asegurado
    {
        [Key]
        public int IdAsegurado { get; set; }
        public string TipoDocumento { get; set; } = string.Empty;
        public string NumeroDocumento { get; set; } = string.Empty;
        public string Nombres { get; set; } = string.Empty;
        public string Apellidos { get; set; } = string.Empty;
        public string? Correo { get; set; }
        public string? Telefono { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

        // Relación
        public ICollection<Poliza> Polizas { get; set; } = new List<Poliza>();
    }
}
