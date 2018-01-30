using MentorModel;
using Newtonsoft.Json;
using PeopleModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentModel
{
    public class AppointmentEmbeddedWrapper
    {
        [JsonProperty("_embedded")]
        public Embedded Embedded { get; set; }
    }

    public class Embedded
    {
        [JsonProperty("appointments")]
        public List<Appointment> Appointments { get; set; }
    }

    [JsonObject("appointments")]
    public class Appointment
    {
        public Appointment(String person, String mentor, DateTime date, int slotNumber)
        {
            this.Person = person;
            this.Mentor = mentor;
            this.date = date;
            this.slotNumber = slotNumber;
        }

        [JsonProperty("id")]
        public String id { get; set; }
        [JsonProperty("person")]
        public String Person { get; set; }
        [JsonProperty("mentor")]
        public String Mentor { get; set; }
        [JsonProperty("date")]
        public DateTime date { get; set; }
        [JsonProperty("slotNumber")]
        public int slotNumber { get; set; }
        [JsonProperty("_links")]
        public Link _links { get; set; }
    }

    public class Link
    {
        [JsonProperty("self")]
        public Self self { get; set; }
        [JsonProperty("appointment")]
        public AppointmentHref appointmentHref { get; set; }
    }

    public class Self
    {
        [JsonProperty("href")]
        public String href { get; set; }
    }

    public class AppointmentHref
    {
        [JsonProperty("href")]
        public String href { get; set; }
    }
}
