using System;
using Nancy;

namespace NancyMonoDemo
{
    public class PingInfo
    {
        public string OsVersion { get; set; }
        public string[] EnvironmentVariables { get; set; }
        public DateTime RequestTime { get; set; }
        public string IndexingResult { get; set; }
    }

    public class RootModule : NancyModule
    {
        public RootModule() 
            : base("/")
        {
            Get["/"] = ctx =>
            {
                var env = Environment.GetEnvironmentVariable("environment");
                var connStr = Environment.GetEnvironmentVariable("esConnection");
                var result = new PingInfo
                {
                    OsVersion = Environment.OSVersion.ToString(),
                    EnvironmentVariables = new[]
                    {
                        env,
                        connStr
                    },
                    RequestTime = DateTime.Now
                };

                // log to ES
                var es = ElasticsearchClientProvider.Instance;
                if (es != null)
                {
                    var indexResult = es.Index(result);
                    result.IndexingResult = indexResult.DebugInformation;
                    Console.WriteLine(result.IndexingResult);
                }
                else
                {
                    Console.WriteLine("No ES connection string found");
                }
                return Response.AsJson(result);
            };
        }
    }
}
