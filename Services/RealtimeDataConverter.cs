using System.Collections.Generic;
using System.Linq;
using Google.Apis.Analytics.v3.Data;
using RankOne.RealtimeAnalytics.Models;

namespace RankOne.RealtimeAnalytics.Services
{
    public class RealtimeDataConverter<T> where T : RealtimeDataResult, new()
    {
        public IEnumerable<T> Convert(RealtimeData data)
        {
            var results = new List<T>();
            if (data.Rows != null)
            {
                foreach (var row in data.Rows)
                {
                    var result = new T();

                    for (var i = 0; i < row.Count; i++)
                    {
                        var header = data.ColumnHeaders[i];
                        var name = GetPropertyName(header.Name);

                        var type = typeof(T);
                        var property = type.GetProperty(name);
                        if (property != null)
                        {
                            property.SetValue(result, row[i], null);
                        }
                    }

                    results.Add(result);
                }
            }
            return results;
        }

        public string GetPropertyName(string headerName)
        {
            var propertyName = headerName.Replace("rt:", "");
            propertyName = propertyName.First().ToString().ToUpper() + string.Join("", propertyName.Skip(1));
            return propertyName;
        }
    }
}
