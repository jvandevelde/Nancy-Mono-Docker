using System;
using Nancy;
using NancyMonoDemo.Data;
using NancyMonoDemo.Entities;

namespace NancyMonoDemo
{
    public class RootModule : NancyModule
    {
        public RootModule() 
            : base("/")
        {
            Get["/"] = HandleRootRequest;
        }

        private dynamic HandleRootRequest(dynamic ctx)
        {
            // prepare an object to do something with

            // Environment variables can be set via `docker run`
            var env = Environment.GetEnvironmentVariable("environment");
            var connStr = Environment.GetEnvironmentVariable("esConnection");
            var loggedRequest = new PageRequest
            {
                EnvironmentReturned = env, EsConnectionReturned = connStr, RequestPath = Request.Path, RequestTime = DateTime.Now
            };

            TryWriteRequestToDatabase(loggedRequest);
            TryWriteRequestToElasticsearch(loggedRequest);

            return Response.AsJson(loggedRequest);
        }

        private static void TryWriteRequestToElasticsearch(PageRequest loggedRequest)
        {
            try
            {
                var esClient = ElasticsearchClientProvider.Instance;
                if (esClient != null)
                {
                    var indexResult = esClient.Index(loggedRequest);
                    Console.WriteLine(indexResult.DebugInformation);
                }
                else
                {
                    Console.WriteLine("No ES connection string found");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception while trying to write to ES - {0}", ex.Message);
            }
        }

        private static void TryWriteRequestToDatabase(PageRequest loggedRequest)
        {
            try
            {
                // http://stackoverflow.com/questions/38378741/incompatible-versions-of-npgsql-and-entityframework6-npgsql
                // Very weird - default install of EF.Npgsql has the wrong npgsql dependency version associated to it
                //              Fix: Just update Npgsql to latest
                using (var context = new LoggingContext())
                {
                    context.PageRequests.Add(loggedRequest);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception while trying to write to RDBMS - {0}", ex.Message);
            }
        }
    }
}
