using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SolarPanelAPI.Models
{
    public partial class TblMonthlyAnnualPivot
    {
        [Key]
        public int Year { get; set; }
        public double? Jan { get; set; }
        public double? Feb { get; set; }
        public double? Mar { get; set; }
        public double? Apr { get; set; }
        public double? May { get; set; }
        public double? Jun { get; set; }
        public double? Jul { get; set; }
        public double? Aug { get; set; }
        public double? Sep { get; set; }
        public double? Oct { get; set; }
        public double? Nov { get; set; }
        public double? Dec { get; set; }
    }

    [Table("MonthlyAnnualCumulativePivot")]
    public class MonthlyAnnualCumulativePivot : TblMonthlyAnnualPivot{ }

    [Table("MonthlyAnnualPivot")]
    public class MonthlyAnnualPivot : TblMonthlyAnnualPivot { }
}
