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
    class AppointmentEmbeddedWrapper
    {
        [JsonProperty("_embedded")]
        public Embedded Embedded { get; set; }
    }

    class Embedded
    {
        [JsonProperty("appointments")]
        public List<Appointment> Appointments { get; set; }
    }

    [JsonObject("appointments")]
    class Appointment
    {
        public Appointment(Person person, Mentor mentor, DateTime startingDateTime, DateTime endingDateTime)
        {
            this.Person = person;
            this.Mentor = mentor;
            this.StartingDateTime = startingDateTime;
            this.EndingDateTime = endingDateTime;
        }

        [JsonProperty("person")]
        public Person Person { get; set; }
        [JsonProperty("mentor")]
        public Mentor Mentor { get; set; }
        [JsonProperty("startingDateTime")]
        public DateTime StartingDateTime { get; set; }
        [JsonProperty("EndingDateTime")]
        public DateTime EndingDateTime { get; set; }
        [JsonProperty("_links")]
        public Link _links { get; set; }
    }

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
