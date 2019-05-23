using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BannerFlowService.Models
{
    public class BannerContext : DbContext
    {
        public DbSet<Banner> Banners { get; set; }
        public BannerContext()
        { }

        public BannerContext(DbContextOptions<BannerContext> options)
            : base(options)
        { }
    }
}
