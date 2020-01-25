using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nest;

namespace Usage_Of_Elasticsearch.DocumentApi
{
    public class IndexApi
    {
        private ElasticClient elasticClient = ElasticInstanceCreator.GetElasticClientInstance();
        public bool CreateIndex<T>(int numberOfShards, int numberOfReplicas, string indexName, string aliasName = null) where T : class
        {
            if (!checkIndexExists(indexName))
            {
                var indexSettings = new IndexSettings();
                indexSettings.NumberOfReplicas = numberOfReplicas;
                indexSettings.NumberOfShards = numberOfShards;

                var createIndexDescriptor = new CreateIndexDescriptor(indexName)
                    .Map<T>(m => m.AutoMap())
                    .InitializeUsing(new IndexState() { Settings = indexSettings });

                if(!String.IsNullOrWhiteSpace(aliasName))
                    createIndexDescriptor.Aliases(a => a.Alias(aliasName));

                CreateIndexResponse response = elasticClient.Indices.Create(createIndexDescriptor);
                return response.Acknowledged;
            }

            return true;
        }

        public bool CreateIndex(int numberOfShards, int numberOfReplicas, string indexName, string aliasName = null)
        {
            if (!checkIndexExists(indexName))
            {
                var indexSettings = new IndexSettings();
                indexSettings.NumberOfReplicas = numberOfReplicas;
                indexSettings.NumberOfShards = numberOfShards;

                var createIndexDescriptor = new CreateIndexDescriptor(indexName)
                    .InitializeUsing(new IndexState() { Settings = indexSettings });

                if (!String.IsNullOrWhiteSpace(aliasName))
                    createIndexDescriptor.Aliases(a => a.Alias(aliasName));

                CreateIndexResponse response = elasticClient.Indices.Create(createIndexDescriptor);
                return response.Acknowledged;
            }

            return true;
        }

        private bool checkIndexExists(string indexName)
        {
            return elasticClient.Indices.Exists(indexName).Exists;
        }
    }
}
