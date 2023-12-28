using PlayCatalogService.Dtos;
using PlayCatalogService.Entities;

namespace PlayCatalogService.Extensions
{
    public static class Extensions
    {
        public static ItemDto AsDto(this Item item)
        {
            return new ItemDto(item.Id, item.Name, item.Description, item.Price, item.CreatedDate);
        }
    }
}
