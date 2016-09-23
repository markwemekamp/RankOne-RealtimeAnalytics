using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using RankOne.RealtimeAnalytics.Models;
using RankOne.RealtimeAnalytics.Services;
using Umbraco.Web.WebApi;

namespace RankOne.RealtimeAnalytics
{
    public class RealtimeAnalyticsApiController : UmbracoAuthorizedApiController
    {
        private readonly RealtimeDataService _realtimeDataService;

        public RealtimeAnalyticsApiController()
        {
            _realtimeDataService = new RealtimeDataService();
        }

        [HttpGet]
        public int GetActiveUsers()
        {
            return _realtimeDataService.GetActiveUsers();
        }

        [HttpGet]
        public IEnumerable<CountryData> GetAllCountries()
        {
            return _realtimeDataService.GetGeoData().GroupBy(x => x.Country).Select(x => new CountryData
            {
                Country = x.First().Country,
                ActiveUsers = x.Sum(c => int.Parse(c.ActiveUsers))
            });
        }

        [HttpGet]
        public IEnumerable<CityData> GetCountry(string name)
        {
            return _realtimeDataService.GetGeoData().Where(x => x.Country == name).GroupBy(x => x.City).Select(x => new CityData
            {
                City = x.First().City,
                ActiveUsers = x.Sum(c => int.Parse(c.ActiveUsers))
            });
        }

        [HttpGet]
        public IEnumerable<BrowserData> GetBrowserInformation()
        {
            return _realtimeDataService.GetBrowserData().Select(x => new BrowserData
            {
                ActiveUsers = x.ActiveUsers,
                Browser = x.Browser
            });
        }

        [HttpGet]
        public IEnumerable<OperatingSystemData> GetOperatingSystemInformation()
        {
            return _realtimeDataService.GetOperatingSystemData().Select(x => new OperatingSystemData
            {
                ActiveUsers = x.ActiveUsers,
                OperatingSystem = x.OperatingSystem
            });
        }
        
    }
}

