using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace HelloWorld
{
    class PeopleMessage
    {
        [JsonProperty("_embedded")]
        public MentorEmbeddedWrapper embedded { get; set; }
    }
}
