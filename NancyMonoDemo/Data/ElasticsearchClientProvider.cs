using System;
using System.Diagnostics;
using System.Text;
using Elasticsearch.Net;
using Nest;

namespace NancyMonoDemo.Data
{
    public static class ElasticsearchClientProvider
    {
        public static ElasticClient Instance => BuildEsClient();

        public static ElasticClient BuildEsClient()
        {
            var esConnection = Environment.GetEnvironmentVariable("esConnection");
            if (string.IsNullOrWhiteSpace(esConnection))
                return null;

            var esNode1 = new Uri(esConnection);
            var connectionPool = new SniffingConnectionPool(new[] { esNode1 });

            var settings = new ConnectionSettings(connectionPool)
                .DefaultIndex("nancy.requests")
                //.EnableTrace(true).ExposeRawResponse(true);   // Not currently supported in new 2.0 client. Use OnRequestCompleted() instead
                .DisableDirectStreaming()
                .OnRequestCompleted(details =>
                {
                    Debug.WriteLine("### ES REQEUST ###");
                    if (details.RequestBodyInBytes != null) Debug.WriteLine(Encoding.UTF8.GetString(details.RequestBodyInBytes));
                    Debug.WriteLine("### ES RESPONSE ###");
                    if (details.ResponseBodyInBytes != null) Debug.WriteLine(Encoding.UTF8.GetString(details.ResponseBodyInBytes));
                })
                .PrettyJson();

            return new ElasticClient(settings);
        }
    }
}