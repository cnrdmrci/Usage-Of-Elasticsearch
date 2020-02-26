# Usage Of Elasticsearch

### Elasticsearch Basic Concept
- Cluster
- Node
- Shard
- Replicas
- Index – stands for database in rdbms
- Document with properties – stand for table row/column in rdbms
- Types – stands for tables in rdbms
- Mapping

> All of index
> http://localhost:9200/_cat/indices?v&pretty

# Elasticsearch API's
- Document Api
- Search Api
- Indices Api
- Cat Api
- Cluster Api
- Aggregation

### Document Api
- Single Document Api
	- Index Api
	- Get Api
	- Delete Api
	- Update Api

- Multi Document Api
	- Multi Get Api
	- Bulk Api
	- Delete By Query Api
	- Update By Query Api
	- Reindex Api

### Index Api
The index Api adds and updates a typed json document in a specific index, making it searchable.
```json
PUT newindex 			// index name must be lowercase.
PUT newindex/_doc/1 	//auto created index with adding document or updating document.
{
	"user" : "caner"
}
```

### Get Api
The get api allows to get a typed Json document from the index based on its id.
```json
GET newindex
GET newindex/_doc/1
```

### Delete Api
The delete Api allows to delete a document from the index based its id.
```json
DELETE newindex/_doc/1
```

### Update Api
The update Api allows to update a document based on a script provided.
```json
POST newindex/_doc/1
{
	"user":"canerUpdated"
}
```
### Multi Get Api
```json
GET /_mget

GET //_mget

GET newindex/mget
{
	"docs" :
	[
		{
			"_index" : "newindex",
			"_id" : "1"
		},
		{
			"_index" : "newindex",
			"id" : "2"
		}

	]
}

GET newindex/_mget
{
	"ids" : ["1","2"]
}
```

### Bulk Api
```json
POST user_information/bulk
{"index":{"_id":"3"}}
{"id":3,"userName":"user3","firstName":"First user 3","lastName":"LastName3","gender":"M","age":23}
{"index":{"_id":"4"}}
{"id":4,"userName":"user4","firstName":"First user 4","lastName":"LastName4","gender":"M","age":24}
{"index":{"_id":"5"}}
{"id":5,"userName":"user5","firstName":"First user 5","lastName":"LastName5","gender":"M","age":25}
```

### Delete By Query Api
```json
POST user_information/_delete_by_query
{
	"query":
	{
		"term":
		{
			"userName":
			{
				"value":"user4"
			}
		}
	}
}
```

### Update By Query Api
```json
POST user_information/update_by_query
{
	"query":
	{
		"term":
		{
			"userName":{"value":"user6"}
		}
		},
			"script":
		{
			"source":"ctx.source.LastName = params.LastName",
			"params":
			{
				"LastName":"LastName updated by query2"
			}
	}
}
```

### Reindex Api
The reindex api allows deep copy source index to destionation index.

```json
POST _reindex
{
	"source" :
	{
		"index" : "newindex"
	},
	"dest" : 
	{
		"index" : "newindex2"
	}
}
```

# Search Api
The search api allows you to execute a search query and get back search hits that match the query.
The query can either be provided using a simple query string as a parameter, or using a request body.

```json
GET newindex/search 	//Fetch all the result.
GET newindex/search?size=10
GET all/search?q=user:*
GET all/search?q=user:canerUpdated
```

### Search Uri
A search request can be executed purely using a uri by providing request parameters. Not all search options are exposed when executing a search using this mode. But it can be handy for quick "curl tests".

GET newindex/_search?q=user:canerUpdated

### Request body search (Query)
The search request can be executed with a search DSL, which includes the Query DSL, within its body.
The query element within the search request body allows to define a query using the Query DSL.

```json
GET newindex/_search
{
	"query": 
	{
		"match": 
		{
			"user": "canerUpdated"
		}
	}
}
```

### Request body search (From/Size)
Pagination of results can be done by using the FROM and SIZE parameters.
The FROM parameter defines the offset from the first result you want to fetch.
The SIZE parameter allows you to configure the maximum amount of hits to be returned.

```json
GET newindex/_search
{
	"from":0,
  	"size":2,
	"query": 
	{
		"match": 
		{
			"user": "canerUpdated"
		}
	}
}
```

### Request body search (Sort)
Allows you to add one or more sorts on specific fields. Each sort can be reserved as well. The sort is defined on a per field level, with special field name for _search to sort by score, and _doc to sort by index order.
```json
GET newindex/search
{
	"from":0,
  	"size":2,
	"query": 
	{
		"query_string": 
		{
			"defaultfield": "user",
			"query": "*"
		}
	},
	"sort": 
	[
		{
			"user.keyword": 
			{
				"order": "desc"
			}
		}
	]
}
```
# Query DSL
Elasticsearch provides a full Query DSL based on Json to define queries.
Two type of clauses:
- Leaf query clauese
Leaf query clauses loof for a particular value in a particular field, such as the match, term or range queries.
These queries can be used by themselves.
- Compound query clauses
Compound query clauses wrap other leaf or compound queries and are used to combine multiple queries in a logical fashion (such as the bool or dis_max query) or to alter their behaviour (such as the constant score query).

### Full text query
The high level full text queries are usually used for running full text queries on full text fields like the body of an email.

- Match query
- Match phrase query
- Match phrase prefix query
- Multi match query
- Query string query

### Match query
The match query search word one by one. Search ‘are’ and ‘you’.

```json
GET newindex/_search
{
	"query":
	{
		"match": 
		{
			"Comments": "are you"
		}
	}
}
```

### Match phrase query
The match phrase query search one. Search ‘are you Caner’.

```json
GET newindex/search
{
	"query":
	{
		"matchphrase": 
		{
			"Comments": "are you Caner"
		}
	}
}
```

//slop means ‘how’ and ‘Caner’ words contains 2 word each other. ‘How are you Caner’
```json
GET newindex/search
{
	"query":
	{
		"match_phraseprefix": 
		{
			"Comments": 
			{
				"query": "How Caner",
				"slop":2
			}
		}
	}
}
```

### Match phrase prefix query
The match phrase prefix query search ‘are you…..’.

```json
GET newindex/search
{
	"query":
	{
		"match_phraseprefix": 
		{
			"Comments": "are you"
		}
	}
}
```

### Multi match query
```json
GET newindex/search
{
	"query": 
	{
		"multimatch": 
		{
			"query": "how",
			"fields": ["user","Comments"]
		}
	}
}

GET newindex/search
{
	"query":
	{
		"matchall": {}
	}
}
```

### Query string query
```json
GET newindex/search
{
	"query": 
	{
		"query_string": 
		{
			"default_field": "Comments",
			"query": "how are you AND (Ahmet OR Tamer)",
			"defaultoperator": "AND"
		}
	}
}
//search → ‘how are you Ahmet’ or ‘how are you Tamer’
//or

GET newindex/search
{
	"query": 
	{
		"query_string": 
		{
			"defaultfield": "Comments",
			"query": "\"how are you\" AND (Ahmet OR Tamer)"
		}
	}
}
//search → ‘how are you Ahmet’ or ‘how are you Tamer’

//with multi match
GET newindex/search
{
	"query": 
	{
		"querystring": 
		{
			"fields": ["Comments","user"],
			"query": "\"how are you\" AND (Ahmet OR Tamer)"
		}
	}
}
```

# Term Level Query
- Term query
- Terms query
- Range query
- Prefix query
- Wildcard query

### Term query
// you only find ‘Hello, how are you Caner’ not ‘Hello, how are you caner’. so not lowercase and otherwise.
```json
GET newindex/_search
{
	"query": 
	{
		"term": 
		{
			"Comments.keyword": 
			{
				"value" :"Hello, how are you Caner"
			}
		}
	}
}
```

### Terms query
```json
GET newindex/_search
{
	"query": 
	{
		"terms": 
		{
			"Comments.keyword": 
			[
				"Hello, how are you Caner",
				"Hello, how are you Tamer"
			]
		}
	}
}

> source; select only inner field.
GET newindex/_search
{
	"query": 
	{
		"terms": 
		{
			"Comments.keyword": 
			[
				"Hello, how are you Caner",
				"Hello, how are you Tamer"
			]
		}
	},
	"source": ["Comments"]
}
```

### Range query
```json
GET newindex/_search
{
	"query": 
	{
		"range": 
		{
			"Age": 
			{
				"gte" : 10,
				"lte" : 30
			}
		}
	}
}
```

### Prefix query
> user field start with bro…
```json
GET newindex/_search
{
	"query": 
	{
		"prefix":
		{
			"user" : "bro"
		}
	}
}
```

### Wildcard query
> bosluk karakteri kullanilmamali yoksa bulamaz, dezavanaji.
> ‘*’ string bir yada birden fazla karakteri simgeler.
> ‘?’ bir adet karakteri simgeler.

```json
GET newindex/_search
{
	"query": 
	{
		"wildcard":
		{
			"user" : "*bro*n"
		}
	}
}
```

# Compound Query

Bool query:
- Must, —> AND
- Must-not, —> Not
- should, —> OR
- Filter, —> Filtre
> minimum_should_match

```json
GET newindex/search
{
	"query": 
	{
		"bool" : 
		{
			"must" : 
			{
				"term" : 
				{
					"user.keyword" : { "value" : "Tamer7" }
				}
			},
			"filter": 
			{
				"term" : 
					{ "Tag": "normal" }
			},
			"must_not" : 
			{
				"range" : 
				{
					"age" : { "gte" : 10, "lte" : 20 }
				}
			},
			"should" : 
			[
				{ "term" : { "Comments" : "Hello, how you Tamer" } },
				{ "term" : 
					{
						"Comments.keyword": { "value" : "Hello, how you Tamer" }
					} 
				}
			],
			"minimum_shouldmatch" : 1,
			"boost" : 1.0
		}
	}
}
```

> Eğer ilgili indexin mapping biçimine aşağıdaki gibi ‘not_analysed’ eklemez isek;
```json
{ "term" : { "Comments" : "Hello, how you Tamer" } } //herhangi bir sonuç dönmeyecektir.
```
```json
curl -XPUT http://localhost:9200/nonanalyzed_example -d ‘{
"mappings": 
{
	"mytype": 
	{
		"_source": {"enabled": true},
		"properties": 
		{
			"content": 
			{
				"type": "string",
				"index": "not_analyzed"
			}
		}
	}
}
}’
```

### Text vs. keyword
The string field has split into two new types: text, which should be used for full-text search, and keyword, which should be used for keyword search
//https://www.elastic.co/blog/strings-are-dead-long-live-strings

# Analysis in ElasticSearch
- What is analysis?
Converting text into Token or terms
Sencente: "A quick brown fox jumped over the lazy dog"
Tokens: [quick,brown,fox,jump,over,lazy,dog]

- Analysis performed by:
	- Analyzer
	- Tokenizer
	- Token Filter
	- Character Filter

- Where we use analysis?
	- Query
	- Mapping parameter
	- Index setting

- Analyzer
Analysing text into token or keywords to be search/indexed
Builds token stream
Analyzer are provide to parse and analyse different languages

Reader → Tokenizer → Token Filter → Token Filter → Token

analyzer type : standard,simple,whitespace otherwise custom

- Mapping
Mapping is the process of defining how a document should be mapped to the Search Engine, including its searchable characteristic such as which field are searchable and if/how they are tokenized.

- Fields
Each mapping has a number of fields associated with it can be used to control how the document metadata is indexed.

- Types
The datatype for each field in a document (eg strings, numbers, objects etc) can be controller via the type mapping.
→Object
→Nested

- Object DataType
Json documents are hierarchical in nature: the document may contain inner objects which, in turn, may contain inner objects themselves.

# Aggregations
- Min
- Max
- Sum
- Average
- Stats

### Min
> POST user_information/search
```json
{"aggs":{"age":{"min":{"field":"age"}}},"query":{"matchall":{}},"size":0}
```

### Max
> POST user_information/search
```json
{"aggs":{"age":{"max":{"field":"age"}}},"query":{"matchall":{}},"size":0}
```

### Sum
> POST user_information/search
```json
{"aggs":{"age":{"sum":{"field":"age"}}},"query":{"matchall":{}},"size":0}
```

### Average
> POST user_information/search
```json
{"aggs":{"age":{"avg":{"field":"age"}}},"query":{"matchall":{}},"size":0}
```

### Stats
> POST user_information/search
```json
{"aggs":{"age":{"stats":{"field":"age"}}},"query":{"matchall":{}},"size":0}
```