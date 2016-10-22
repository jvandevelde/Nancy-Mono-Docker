using System;
using Nancy;

namespace NancyMonoDemo
{
    public class PingInfo
    {
        public string OsVersion { get; set; }
        public string[] EnvironmentVariables { get; set; }
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
                    }
                };

                return Response.AsJson(result);
            };

        }
    }
}
