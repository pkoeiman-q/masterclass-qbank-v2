using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MasterclassApiTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OverboekingenController : ControllerBase
    {
        // GET: api/<OverboekingController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<OverboekingController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<OverboekingController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<OverboekingController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<OverboekingController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
