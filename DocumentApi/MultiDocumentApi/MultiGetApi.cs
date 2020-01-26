using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nest;

namespace Usage_Of_Elasticsearch.DocumentApi.MultiDocumentApi
{
    public class MultiGetApi
    {
        private ElasticClient elasticClient = ElasticInstanceCreator.GetElasticClientInstance();

        public async Task<List<T>> MultiGetDataByIds<T>(string indexName,List<string> ids) where T : class
        {
            var response = await elasticClient.MultiGetAsync(m=> m
                .Index(indexName)
                .GetMany<T>(ids)
            );
            List<T> list = new List<T>();
            if (response.Hits.Count > 0)
            {
                foreach (IMultiGetHit<object> hit in response.Hits)
                {
                    if(hit.Source != null)
                        list.Add((T)hit.Source);
                }
            }
            return list;
        }
    }
}
