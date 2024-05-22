using Microsoft.EntityFrameworkCore;
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

        public void AddPackingItem(PackingItemSqlView packingItem)
        {
            var existingItem = this.context.PackingItems.FirstOrDefault(t => t.Id == packingItem.Id);

            if (existingItem == null)
            {
                this.context.PackingItems.Add(packingItem);
                this.context.SaveChanges();
            }
        }

        public void DeletePackingItem(Guid packingItemId)
        {
            var existingItem = this.context.PackingItems.FirstOrDefault(t => t.Id == packingItemId);
            if (existingItem != null)
            {
                this.context.PackingItems.Remove(existingItem);
                this.context.SaveChanges();
            }
        }

        public void UpdatePackingItem(Guid packingItemId, bool newIsPacked)
        {
            var existingItem = this.context.PackingItems.FirstOrDefault(t => t.Id == packingItemId);
            if (existingItem != null)
            {
                existingItem.IsPacked = newIsPacked;
                this.context.SaveChanges();
            }
        }

        public async Task<List<PackingItemSqlView>> GetPackingItems()
        {
            return await this.context.PackingItems.ToListAsync();
        }
    }
}
