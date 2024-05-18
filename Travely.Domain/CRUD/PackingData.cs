using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travely.Domain.Entities;

namespace Travely.Domain.CRUD
{
    public class PackingData
    {
        private readonly AppDbContext context;

        public PackingData(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<List<PackingItemSqlView>> GetPackingItems()
        {
            return await this.context.PackingItems.ToListAsync();
        }
    }
}
