using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATT.Core
{
    public class Unit
    {
        [JsonProperty]
        public string Name { get; set; }

        [JsonProperty]
        public List<Tab> TabList = new List<Tab>();
    }
}
