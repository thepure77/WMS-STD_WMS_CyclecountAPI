using LogDataAccess.Models;
using MasterDataDataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IO;

namespace MasterDataAccess
{
    public class MasterDbContext : DbContext
    {
  

        public virtual DbSet<MS_Product> MS_Product { get; set; }
        public virtual DbSet<MS_Zone> MS_Zone { get; set; }
        public DbSet<MS_ProductOwner> MS_ProductOwner { get; set; }
        public DbSet<ms_Location> MS_Location { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var builder = new ConfigurationBuilder();
                builder.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"), optional: false);

                var configuration = builder.Build();

                var connectionString = configuration.GetConnectionString("MasterDataAccess").ToString();

                optionsBuilder.UseSqlServer(connectionString);
            }


        }
    }

    public class Blog
    {
        public int BlogId { get; set; }
        public string Url { get; set; }
        public int Rating { get; set; }
        public List<Post> Posts { get; set; }
    }

    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public int BlogId { get; set; }
        public Blog Blog { get; set; }
    }
}
