namespace Travely.BusinessLogic.DTOs
{
    public class PackingItemDTO
    {
        public Guid Id {  get; set; }

        public string? Title { get; set; }

        public bool IsPacked { get; set; }
    }
}
