using System;
using Microsoft.EntityFrameworkCore;
using StokYonetimi.DB.Entities;

namespace StokYonetimi.DB
{
    public class StokYonetimiDbContext : DbContext
    {
	    public StokYonetimiDbContext(DbContextOptions<StokYonetimiDbContext> options)
            :base(options)
	    {

	    }

        public virtual DbSet<Kategoriler> Kategoriler { get; set; }
        public virtual DbSet<Urunler> Urunler { get; set; }
    }
}

