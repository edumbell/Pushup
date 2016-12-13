using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pushup.Models
{
    public class DataAccount
    {
        public DataAccount()
        {
            Variables = new List<VariableData>();
        }

        // used in URL's of API and dashboard display
        public string Slug { get; set; }

        public List<VariableData> Variables { get; set; }
    }
}