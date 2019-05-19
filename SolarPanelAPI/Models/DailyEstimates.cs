using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SolarPanelAPI.Models
{
    public partial class DailyEstimates
    {
        [Key]
        public DateTime Date { get; set; }
        public double? PowerGenerated { get; set; }
        public double? EstimatedRebate { get; set; }
    }
}
