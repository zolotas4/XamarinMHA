using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace HelloWorld
{
    class Embedded
    {
        [JsonProperty("people")]
        public List<Person> people { get; set; }
    }
}
