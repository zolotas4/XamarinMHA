using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResourceModel
{
    class ResourceEmbeddedWrapped
    {
        [JsonProperty("_embedded")]
        public Embedded Embedded { get; set; }
    }

    public class Embedded
    {
        [JsonProperty("resources")]
        public List<Resource> Resources { get; set; }
    }

    [JsonObject("resource")]
    public class Resource
    {
        public Resource(String title, String shortDescription, String filename, String filetype)
        {
            this.title = title;
            this.shortDescription = shortDescription;
            this.filename = filename;
            this.filetype = filetype;
        }

        [JsonProperty("title")]
        public String title { get; set; }
        [JsonProperty("shortDescription")]
        public String shortDescription { get; set; }
        [JsonProperty("filename")]
        public String filename { get; set; }
        [JsonProperty("filetype")]
        public String filetype { get; set; }
        [JsonProperty("_links")]
        public Link _links { get; set; }
    }

    public class Link
    {
        [JsonProperty("self")]
        public Self self { get; set; }
        [JsonProperty("resource")]
        public ResourceHref resourceHref { get; set; }
    }

    public class Self
    {
        [JsonProperty("href")]
        public String href { get; set; }
    }

    public class ResourceHref
    {
        [JsonProperty("href")]
        public String href { get; set; }
    }
}
