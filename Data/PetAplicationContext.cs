using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PetAplication.Models;

namespace PetAplication.Data
{
    public class PetAplicationContext : DbContext
    {
        public PetAplicationContext (DbContextOptions<PetAplicationContext> options)
            : base(options)
        {
        }

        public DbSet<PetAplication.Models.Facility> Facility { get; set; } = default!;

        public DbSet<PetAplication.Models.Location> Location { get; set; }

       

        public DbSet<PetAplication.Models.Client> Client { get; set; }

        public DbSet<PetAplication.Models.Request> Request { get; set; }

        public DbSet<PetAplication.Models.Pet> Pet { get; set; }
    }
}
