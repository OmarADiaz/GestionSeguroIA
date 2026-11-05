using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditRiskAgent.Domain.Entities
{
    public class CoberturaPoliza
    {
        [Key]
        public int IdCoberturaPoliza { get; set; }
        public int IdPoliza { get; set; }
        public string NombreCobertura { get; set; } = string.Empty;
        public decimal SumaAsegurada { get; set; }
        public string Deducible { get; set; } = string.Empty;

        public Poliza Poliza { get; set; } = null!;
    }
}
