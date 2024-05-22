using Travely.Domain.CRUD;
using Travely.Domain;
using Travely.BusinessLogic.Converters;
using Travely.BusinessLogic.DTOs;

namespace Travely.BusinessLogic.Services
{
    public class PackingService
    {
        public PackingData packingData;

        public PackingService(AppDbContext context)
        {
            this.packingData = new PackingData(context);
        }

        public void AddPackingItem(PackingItemDTO packingItem)
        {
            packingData.AddPackingItem(DTOEntity.DTOtoEntity(packingItem));
        }

        public void DeletePackingItem(Guid packingItemId)
        {
            packingData.DeletePackingItem(packingItemId);
        }

        public void UpdatePackingItem(Guid packingItemId, bool newIsPacked)
        {
            packingData.UpdatePackingItem(packingItemId, newIsPacked);
        }

        public async Task<List<PackingItemDTO>> GetPackingItems()
        {
            var packingItems = await packingData.GetPackingItems();
            return packingItems.Select(packingItem => EntityDTO.EntityToDTO(packingItem)).ToList();
        }
    }
}
