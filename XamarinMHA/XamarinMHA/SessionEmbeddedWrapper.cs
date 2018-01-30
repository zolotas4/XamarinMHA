using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace SessionModel
{
    public class SessionEmbeddedWrapper
    {
        [JsonProperty("_embedded")]
        public Embedded Embedded { get; set; }
    }

    public class Embedded
    {
        [JsonProperty("sessions")]
        public List<Session> Sessions { get; set; }
    }

    [JsonObject("sessions")]
    public class Session
    {
        public Session(String person, String mentor, DateTime date, int startingSlotNumber, int duration)
        {
            this.Person = person;
            this.Mentor = mentor;
            this.date = date;
            this.startingSlotNumber = startingSlotNumber;
            this.duration = duration;
            this.logged = false;
            this.comments = "";
            this.actualDuration = 0;
        }

        [JsonProperty("id")]
        public String id { get; set; }
        [JsonProperty("person")]
        public String Person { get; set; }
        [JsonProperty("mentor")]
        public String Mentor { get; set; }
        [JsonProperty("date")]
        public DateTime date { get; set; }
        [JsonProperty("startingSlotNumber")]
        public int startingSlotNumber { get; set; }
        [JsonProperty("duration")]
        public int duration { get; set; }
        [JsonProperty("logged")]
        public Boolean logged { get; set; }
        [JsonProperty("comments")]
        public String comments { get; set; }
        [JsonProperty("actualDuration")]
        public int actualDuration { get; set; }
        [JsonProperty("_links")]
        public Link _links { get; set; }
    }

    public class Link
    {
        [JsonProperty("self")]
        public Self self { get; set; }
        [JsonProperty("session")]
        public SessionHref sessionHref { get; set; }
    }

    public class Self
    {
        [JsonProperty("href")]
        public String href { get; set; }
    }

    public class SessionHref
    {
        [JsonProperty("href")]
        public String href { get; set; }
    }
}