using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditRiskAgent.Domain.Entities
{
    public class Producto
    {
        [Key]
        public int IdProducto { get; set; }
        public int IdRamo { get; set; }
        public string CodigoProducto { get; set; } = string.Empty;
        public string NombreProducto { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public bool Activo { get; set; } = true;

        public Ramo Ramo { get; set; } = null!;
        public ICollection<Poliza> Polizas { get; set; } = new List<Poliza>();
    }
}
