using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nest;

namespace Usage_Of_Elasticsearch.Mapping
{
    public class AttributeMapping
    {
        private ElasticClient _elasticClient = ElasticInstanceCreator.GetElasticClientInstance();
        public void Create()
        {
            var createIndexResponse = _elasticClient.Indices.Create("myindex", c => c
                .Map<EmployeeA>(m => m.AutoMap())
            );
        }
    }

    [ElasticsearchType(RelationName = "employee")]
    public class EmployeeA
    {
        [Text(Name = "first_name", Norms = false, Similarity = "LMDirichlet")]
        public string FirstName { get; set; }

        [Text(Name = "last_name")]
        public string LastName { get; set; }

        [Number(DocValues = false, IgnoreMalformed = true, Coerce = true)]
        public int Salary { get; set; }

        [Date(Format = "MMddyyyy")]
        public DateTime Birthday { get; set; }

        [Boolean(NullValue = false, Store = true)]
        public bool IsManager { get; set; }

        [Nested]
        [PropertyName("empl")]
        public List<EmployeeA> Employees { get; set; }

        [Text(Name = "office_hours")]
        public TimeSpan? OfficeHours { get; set; }

        [Object]
        public List<Skill> Skills { get; set; }
    }

    public class Skill
    {
        [Text]
        public string Name { get; set; }

        [Number(NumberType.Byte, Name = "level")]
        public int Proficiency { get; set; }
    }
}

/*
 {
  "mappings": {
    "properties": {
      "birthday": {
        "format": "MMddyyyy",
        "type": "date"
      },
      "empl": {
        "properties": {},
        "type": "nested"
      },
      "first_name": {
        "type": "text",
        "norms": false,
        "similarity": "LMDirichlet"
      },
      "isManager": {
        "null_value": false,
        "store": true,
        "type": "boolean"
      },
      "last_name": {
        "type": "text"
      },
      "office_hours": {
        "type": "text"
      },
      "salary": {
        "coerce": true,
        "doc_values": false,
        "ignore_malformed": true,
        "type": "float"
      },
      "skills": {
        "properties": {
          "level": {
            "type": "byte"
          },
          "name": {
            "type": "text"
          }
        },
        "type": "object"
      }
    }
  }
}
 */
