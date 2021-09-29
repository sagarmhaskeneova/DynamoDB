using Amazon.DynamoDBv2.DataModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DynamoDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly IDynamoDBContext _dynamoDBContext;
        public CatalogController(IDynamoDBContext dynamoDBContext)
        {
            _dynamoDBContext = dynamoDBContext;
        }
        // GET: api/<CatalogController>
        [HttpGet]
        public async Task<IEnumerable<CatalogInfo>> Get()
        {
            var conditions = new List<ScanCondition>();
            var a=  await _dynamoDBContext.ScanAsync<CatalogInfo>(conditions).GetRemainingAsync();
            return a;
            
        }

        // GET api/<CatalogController>/5
        [HttpGet("{Id}")]
        public async Task<CatalogInfo> Get(int Id)
        {
            var a = await _dynamoDBContext.LoadAsync<CatalogInfo>(Id);
                    
            return a;
        }

        // POST api/<CatalogController>
        [HttpPost]
        public async Task Post([FromBody] CatalogInfo catalogInfo)
        {
            await _dynamoDBContext.SaveAsync(catalogInfo);
        }

        // PUT api/<CatalogController>/5
        [HttpPut("{Id}")]
        public async Task Put(int Id, [FromBody] CatalogInfo catalogInfo)
        {
            var specificItem = await _dynamoDBContext.LoadAsync<CatalogInfo>(Id);
            specificItem = catalogInfo;
            await _dynamoDBContext.SaveAsync(specificItem);
        }

        // DELETE api/<CatalogController>/5
        [HttpDelete("{Id}")]
        public async Task Delete(int Id)
        {
            try
            {
                var specificItem = await _dynamoDBContext.LoadAsync<CatalogInfo>(Id);
                await _dynamoDBContext.DeleteAsync(specificItem);
            }
            catch (Exception ex)
            {

                

            }
          
        }
    }
}
