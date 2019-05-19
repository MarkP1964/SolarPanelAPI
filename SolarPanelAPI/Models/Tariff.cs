using System;
using System.ComponentModel.DataAnnotations;

namespace SolarPanelAPI.Models
{
    public partial class Tariff
    {
        [Key]
        public decimal Rate { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
