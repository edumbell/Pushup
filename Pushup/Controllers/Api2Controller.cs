using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Pushup.Controllers
{
    public class Api2Controller : ApiController
    {
        // GET api/api2
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/api2/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/api2
        public void Post([FromBody]string value)
        {
        }

        // PUT api/api2/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/api2/5
        public void Delete(int id)
        {
        }
    }
}
