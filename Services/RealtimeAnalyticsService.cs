using System.Collections.Generic;
using System.IO;
using System.Linq;
using Google.Apis.Analytics.v3;
using Google.Apis.Analytics.v3.Data;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Http;
using Google.Apis.Services;

namespace RankOne.RealtimeAnalytics.Services
{
    public class RealtimeAnalyticsService
    {
        protected AnalyticsService AnalyticsService;

        public string ApplicationName { get; set; }
        public string ViewId { get; set; }

        public RealtimeAnalyticsService()
        {
            ApplicationName = "Umbraco Realtime Analytics";
        }

        public void Initialize(string filepath, string viewId)
        {
            AnalyticsService = new AnalyticsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = GetCredentials(filepath),
                ApplicationName = ApplicationName,
            });
            ViewId = viewId;
        }

        private IConfigurableHttpClientInitializer GetCredentials(string filepath)
        {
            GoogleCredential credentials;
            using (var stream = new FileStream(filepath, FileMode.Open, FileAccess.Read))
            {
                string[] scopes = { AnalyticsService.Scope.AnalyticsReadonly };
                var googleCredential = GoogleCredential.FromStream(stream);
                credentials = googleCredential.CreateScoped(scopes);
            }
            return credentials;
        }

        public RealtimeData GetData(List<string> metrics, List<string> dimensions = null)
        {
            var request = AnalyticsService.Data.Realtime.Get(string.Format("ga:{0}", ViewId), string.Join(",", metrics));
            if (dimensions != null && dimensions.Any())
            {
                request.Dimensions = string.Join(",", dimensions);
            }
            return request.Execute();
        }
    }
}
