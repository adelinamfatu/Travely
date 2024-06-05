namespace Travely.BusinessLogic.DTOs
{
    public class SpotDTO
    {
        public Guid Id { get; set; }

        public string? Name { get; set; }

        public string? Address { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        public DateTime Date { get; set; }

        public TimeSpan Time { get; set; }

        public decimal? EntryFee { get; set; }
    }
}
