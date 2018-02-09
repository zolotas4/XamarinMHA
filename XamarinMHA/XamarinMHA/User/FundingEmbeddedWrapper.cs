using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundingModel
{
    public class FundingEmbeddedWrapper
    {
        [JsonProperty("_embedded")]
        public Embedded Embedded { get; set; }
    }

    public class Embedded
    {
        [JsonProperty("funding")]
        public List<Funding> Funding { get; set; }
    }

    [JsonObject("funding")]
    public class Funding
    {
        public Funding(string Person, string Provider, int Time)
        {
            this.Person = Person;
            this.Provider = Provider;
            this.Time = Time;
            
        }

        [JsonProperty("person")]
        public string Person { get; set; }
        [JsonProperty("provider")]
        public string Provider { get; set; }
        [JsonProperty("time")]
        public int Time { get; set; }
        [JsonProperty("_links")]
        public Link _links { get; set; }
    }

    public class Link
    {
        [JsonProperty("self")]
        public Self self { get; set; }
        [JsonProperty("funding")]
        public FundingHref fundingHref { get; set; }
    }

    public class Self
    {
        [JsonProperty("href")]
        public String href { get; set; }
    }

    public class FundingHref
    {
        [JsonProperty("href")]
        public String href { get; set; }
    }
}
