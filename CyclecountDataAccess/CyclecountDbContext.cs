
using TransferDataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using GIDataAccess.Models;
using BinBalanceDataAccess.Models;

namespace DataAccess
{
    public class CyclecountDbContext : DbContext
    {


        public virtual DbSet<wm_CycleCount> wm_CycleCount { get; set; }
        public virtual DbSet<wm_CycleCountItem> wm_CycleCountItem { get; set; }
        public virtual DbSet<wm_CycleCountDetail> wm_CycleCountDetail { get; set; }
        public virtual DbSet<wm_TaskCycleCount> wm_TaskCycleCount { get; set; }
        public virtual DbSet<View_BinBalanceLocation> View_BinBalanceLocation { get; set; }
        public virtual DbSet<wm_BinBalance> wm_BinBalance { get; set; }
        public virtual DbSet<GetValueByColumn> GetValueByColumn { get; set; }
        public virtual DbSet<View_TaskCycleCount> View_TaskCycleCount { get; set; }
        public virtual DbSet<wm_TaskCycleCountItem> wm_TaskCycleCountItem { get; set; }
        public virtual DbSet<View_TaskCyclecountfilter> View_TaskCyclecountfilter { get; set; }
        public virtual DbSet<View_Cyclecount> View_Cyclecount { get; set; }
        public virtual DbSet<View_Movement30> View_Movement30 { get; set; }
        public virtual DbSet<View_Movement60> View_Movement60 { get; set; }
        public virtual DbSet<View_Movement90> View_Movement90 { get; set; }

        



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var builder = new ConfigurationBuilder();
                builder.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"), optional: false);

                var configuration = builder.Build();

                var connectionString = configuration.GetConnectionString("DefaultConnection").ToString();

                optionsBuilder.UseSqlServer(connectionString);
            }
        }
    }
}
