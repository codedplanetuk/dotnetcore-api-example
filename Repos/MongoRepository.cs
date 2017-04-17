using System;
using System.Linq;
using System.Collections.Generic;
using MongoDB.Driver;
using MongoDB.Bson;

namespace dotnet_core_api_example.Repositories
{
	public interface IMongoRepository
	{
		MongoClient MongoClient { get; }
	}

	public class MongoRepository : IMongoRepository
	{
		MongoClient _mongoClient;
		public MongoClient MongoClient
		{
			get
			{
				if (_mongoClient == null)
				{
					_mongoClient = new MongoClient("mongodb://10.0.1.80:27017");
					//_mongoClient = new MongoClient("mongodb://localhost:27017");
					return _mongoClient;
				}
				else
				{
					return _mongoClient;
				}
			}
		}

		public MongoRepository()
		{
		}

	}
}
