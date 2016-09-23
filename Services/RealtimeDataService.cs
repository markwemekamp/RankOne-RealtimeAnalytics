using System.Collections.Generic;
using System.Linq;
using RankOne.RealtimeAnalytics.Models;

namespace RankOne.RealtimeAnalytics.Services
{
    public class RealtimeDataService : BaseService
    {
        public int GetActiveUsers()
        {
            var metrics = new List<string> { Metrics.ActiveUsers };

            var realtimeData = RealtimeAnalyticsService.GetData(metrics);

            return realtimeData.Rows != null ? int.Parse(realtimeData.Rows.First().First()) : 0;
        }

        public IEnumerable<GeoResult> GetGeoData()
        {
            var metrics = new List<string> { Metrics.ActiveUsers };
            var dimensions = new List<string> { Dimensions.City, Dimensions.Region, Dimensions.Country, Dimensions.Latitude, Dimensions.Longitude };

            var realtimeData = RealtimeAnalyticsService.GetData(metrics, dimensions);

            var dataConverter = new RealtimeDataConverter<GeoResult>();
            return dataConverter.Convert(realtimeData);
        }

        public IEnumerable<BrowserResult> GetBrowserData()
        {
            var metrics = new List<string> { Metrics.ActiveUsers };
            var dimensions = new List<string> { Dimensions.Browser };

            var realtimeData = RealtimeAnalyticsService.GetData(metrics, dimensions);

            var dataConverter = new RealtimeDataConverter<BrowserResult>();
            return dataConverter.Convert(realtimeData);
        }

        public IEnumerable<OperatingSystemResult> GetOperatingSystemData()
        {
            var metrics = new List<string> { Metrics.ActiveUsers };
            var dimensions = new List<string> { Dimensions.OperatingSystem };

            var realtimeData = RealtimeAnalyticsService.GetData(metrics, dimensions);

            var dataConverter = new RealtimeDataConverter<OperatingSystemResult>();
            return dataConverter.Convert(realtimeData);
        }
    }
}
