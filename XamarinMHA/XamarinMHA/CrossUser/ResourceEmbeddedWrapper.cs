using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace ResourceModel
{
    class ResourceEmbeddedWrapper
    {
        [JsonProperty("_embedded")]
        public Embedded Embedded { get; set; }
    }

    public class Embedded
    {
        [JsonProperty("resources")]
        public List<Resource> Resources { get; set; }
    }

    [JsonObject("resources")]
    public class Resource
    {
        public Resource(String title, String shortDescription, String filename, String filetype, String free, String description, String author)
        {
            this.title = title;
            this.shortDescription = shortDescription;
            this.filename = filename;
            this.filetype = filetype;
            this.description = description;
            this.author = author;
            this.free = free;
        }

        [JsonProperty("id")]
        public String id { get; set; }
        [JsonProperty("title")]
        public String title { get; set; }
        [JsonProperty("free")]
        public String free { get; set; }
        [JsonProperty("shortDescription")]
        public String shortDescription { get; set; }
        [JsonProperty("author")]
        public String author { get; set; }
        [JsonProperty("description")]
        public String description { get; set; }
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
