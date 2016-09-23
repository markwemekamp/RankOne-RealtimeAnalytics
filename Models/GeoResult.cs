namespace RankOne.RealtimeAnalytics.Models
{
    public class GeoResult : RealtimeDataResult
    {
        public string ActiveUsers { get; set; }
        public string Country { get; set; }
        public string Region { get; set; }
        public string City { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }
}
