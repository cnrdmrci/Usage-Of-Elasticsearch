using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;

namespace Usage_Of_Elasticsearch
{
    public static class ElasticInstanceCreator
    {
        private static readonly ConnectionSettings connSettings =
            new ConnectionSettings(new Uri("http://localhost:9200/"))
                .DefaultIndex("log_history")
                .DisableDirectStreaming()
                .PrettyJson()
                .OnRequestCompleted(callDetails =>
                {
                    if (callDetails.RequestBodyInBytes != null)
                    {
                        Console.WriteLine(
                            $"{callDetails.HttpMethod} {callDetails.Uri} \n" +
                            $"{Encoding.UTF8.GetString(callDetails.RequestBodyInBytes)}");
                    }
                    else
                    {
                        Console.WriteLine($"{callDetails.HttpMethod} {callDetails.Uri}");
                    }

                    Console.WriteLine();

                    if (callDetails.ResponseBodyInBytes != null)
                    {
                        Console.WriteLine($"Status: {callDetails.HttpStatusCode}\n" +
                                          $"{Encoding.UTF8.GetString(callDetails.ResponseBodyInBytes)}\n" +
                                          $"{new string('-', 30)}\n");
                    }
                    else
                    {
                        Console.WriteLine($"Status: {callDetails.HttpStatusCode}\n" +
                                          $"{new string('-', 30)}\n");
                    }
                });
        private static readonly ElasticClient elasticClient = new ElasticClient(connSettings);

        public static ElasticClient GetElasticClientInstance()
        {
            return elasticClient;
        }


}
}
