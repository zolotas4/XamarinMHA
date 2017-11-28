using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld
{
    class Link
    {
        [JsonProperty("self")]
        public Self self { get; set; }
    }

    class Self
    {
        [JsonProperty("href")]
        public String href { get; set; }
    }
}
