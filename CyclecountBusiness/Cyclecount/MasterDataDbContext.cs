using MasterDataDataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using TransferDataAccess.Models;

namespace CyclecountBusiness.Transfer
{
    internal class MasterDataDbContext : DbContext
    {
        public DbSet<ms_Location> MS_Location { get; set; }
        
    }
}