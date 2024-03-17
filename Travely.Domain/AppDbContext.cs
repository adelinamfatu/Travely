using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travely.Domain.Entities;

namespace Travely.Domain
{
    public class AppDbContext : DbContext
    {
        public DbSet<UserSqlView> Users { get; set; }
    }
}
