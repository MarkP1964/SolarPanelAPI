using System;
using Microsoft.EntityFrameworkCore;
using SolarPanelAPI.Models;

namespace SolarPanelAPI.Models
{
    public class SolarPanelAPIContext : DbContext
    {
        public SolarPanelAPIContext(DbContextOptions<SolarPanelAPIContext> options) : base(options)
        {}

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if(!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseSqlServer(@"Data Source=ULL;Initial Catalog=SolarPanels;Integrated Security=False;User ID=sa;Password=x;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        //    }
        //}

        #region DbSets
        public DbSet<DailyEstimates> DailyEstimates { get; set; }
        public DbSet<DailyReadings> DailyReadings { get; set; }
        public DbSet<Tariff> Tariff { get; set; }
        public DbSet<MonthlyAnnualPivot> MonthlyAnnualPivot { get; set; }
        public DbSet<MonthlyAnnualCumulativePivot> MonthlyAnnualCumulativePivot { get; set; }

        #endregion DbSets
    }
}
