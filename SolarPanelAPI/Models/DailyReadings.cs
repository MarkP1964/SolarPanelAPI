using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SolarPanelAPI.Models
{
    public partial class DailyReadings
    {
        [Key]
        public DateTime Date { get; set; }
        public double? Reading { get; set; }
        public double? PowerGenerated { get; set; }
        public decimal? RebateGenerated { get; set; }
    }
}
