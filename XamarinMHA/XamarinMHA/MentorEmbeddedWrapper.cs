using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace HelloWorld
{

    class MentorEmbeddedWrapper
    {
        [JsonProperty("_embedded")]
        public Embedded Embedded { get; set; }
    }

    class Embedded
    {
        //[JsonProperty("people")]
        //public List<Person> people { get; set; }
        [JsonProperty("mentors")]
        public List<Mentor> Mentors { get; set; }
    }
}
