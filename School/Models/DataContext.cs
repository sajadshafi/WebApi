
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace School.Models {
    public class DataContext : IdentityDbContext {
        public DataContext(DbContextOptions dbContextOptions) : base(dbContextOptions) {

        }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    }
}
