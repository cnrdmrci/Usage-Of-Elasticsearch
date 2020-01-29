using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nest;

namespace Usage_Of_Elasticsearch.Mapping
{
    public class FluentMapping
    {
        private ElasticClient _elasticClient = ElasticInstanceCreator.GetElasticClientInstance();
        public void Create()
        {
            var createIndexResponse = _elasticClient.Indices.Create("myindex", c => c
                .Map<Company>(m => m
                    .Properties(ps => ps
                        .Text(s => s
                            .Name(n => n.Name)
                        )
                        .Object<Employee>(o => o
                            .Name(n => n.Employees)
                            .Properties(eps => eps
                                .Text(s => s
                                    .Name(e => e.FirstName)
                                    .Analyzer("my_analyzer")
                                )
                                .Text(s => s
                                    .Name(e => e.LastName)
                                )
                                .Number(n => n
                                    .Name(e => e.Salary)
                                    .Type(NumberType.Integer)
                                )
                                .Date(d => d
                                    .Name(n => n.Birthday)
                                    .Format("dd-MM-yyyy"))
                            )
                        )
                    )
                )
            );
        }
    }

    public class Company
    {
        public string Name { get; set; }
        public List<Employee> Employees { get; set; }
    }

    public class Employee
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Salary { get; set; }
        public DateTime Birthday { get; set; }
        public bool IsManager { get; set; }
        public List<Employee> Employees { get; set; }
        public TimeSpan Hours { get; set; }
    }
}
