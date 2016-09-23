namespace RankOne.RealtimeAnalytics.Services
{
    public class BaseService
    {
        protected readonly RealtimeAnalyticsService RealtimeAnalyticsService;

        public BaseService()
        {
            RealtimeAnalyticsService = new RealtimeAnalyticsService();
            RealtimeAnalyticsService.Initialize("C:/Umbraco/UmbracoTestEnvironment/Realtime monitoring-62a4b9367fea.json", "124304066");
        }
    }
}
