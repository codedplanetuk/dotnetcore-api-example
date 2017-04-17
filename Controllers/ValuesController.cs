using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using dotnet_core_api_example.Repositories;
using MongoDB.Bson;
using MongoDB.Driver;

namespace dotnet_core_api_example.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
			IMongoRepository mongo = new MongoRepository();
			var mClient = mongo.MongoClient.GetDatabase("syncpizza");
			var coll = mClient.GetCollection<BsonDocument>("pizzapoll");
			var res = coll.Find(new BsonDocument()).First();
			var dt = res["dateLogged"].AsString;

            return new string[] { "value1", dt };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
