using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nest;

namespace Usage_Of_Elasticsearch.DocumentApi.MultiDocumentApi
{
    public class ReindexApi
    {
        private ElasticClient elasticClient = ElasticInstanceCreator.GetElasticClientInstance();

        public async Task<bool> ReindexSourceToDestionation(string sourceIndex, string destinationIndex)
        {
            var response = await elasticClient.ReindexOnServerAsync(r => r.Source(s => s.Index(sourceIndex)).Destination(d => d.Index(destinationIndex)));
            return response.IsValid;
        }
    }
}
