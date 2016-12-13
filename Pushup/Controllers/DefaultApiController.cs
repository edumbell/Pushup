using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using Pushup.ApiModels;
using Pushup.BusinessLayer;

namespace Pushup.Controllers
{
    public class DefaultApiController : ApiController
    {
        [HttpPost]
        // api/myurl/push  [  { name='asdf', value=1.1}, {name='xyz', value=-3}   ]
        public string Push([FromUri]string slug, [FromBody] List<VariableDataPush> data)
        {
            if (string.IsNullOrEmpty(slug))
                throw new Exception("URL must be in format: /api/{slug}/push");
            if (data == null || !data.Any())
            {
                throw new Exception("No data points found.  Try: using contentType:'application/json; charset=utf-8', and request body '[{name:\"x\", value:\"1\"}]' ");
            }
            var manager = new DataPointManager();
            foreach (var p in data)
            {
                var duplicates = data.Where(x => x.name == p.name);
                if (duplicates.Count() > 1)
                {
                    throw new Exception("Multiple values for single variable name [" + p.name + "]" );
                }
                manager.AddRawDataPoint(slug, p);
            }
            return "";
        }

        [HttpPost]
        // api/myurl/configure  [  { [name='asdf', summaryGranularityMinutes}, ...  ]
        public string Configure([FromUri]string slug, [FromBody] List<VariableConfigPush> data)
        {
            return "";
        }
    }
}
