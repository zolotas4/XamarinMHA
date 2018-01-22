using System;
using System.Collections.Generic;
using Newtonsoft.Json;


namespace MentorModel
{
    public class MentorEmbeddedWrapper
    {
        [JsonProperty("_embedded")]
        public Embedded Embedded { get; set; }
    }

    public class Embedded
    {
        [JsonProperty("mentors")]
        public List<Mentor> Mentors { get; set; }
    }

    [JsonObject("mentors")]
    public class Mentor
    {
        public Mentor(string FirstName, string LastName, string UserName, string Password, string Email, int startSlot, int endSlot)
        {
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.UserName = UserName;
            this.Password = Password;
            this.Email = Email;
            this.startSlot = startSlot;
            this.endSlot = endSlot;
            this.FirstLastName = FirstName + " " + LastName;
        }

        [JsonProperty("firstName")]
        public string FirstName { get; set; }
        [JsonProperty("lastName")]
        public string LastName { get; set; }
        [JsonProperty("userName")]
        public string UserName { get; set; }
        [JsonProperty("password")]
        public String Password { get; set; }
        [JsonProperty("email")]
        public String Email { get; set; }
        [JsonProperty("startSlot")]
        public int startSlot { get; set; }
        [JsonProperty("endSlot")]
        public int endSlot { get; set; }
        [JsonProperty("_links")]
        public Link _links { get; set; }
        [JsonIgnore]
        public String FirstLastName { get; set; }
    }

    public class Link
    {
        [JsonProperty("self")]
        public Self self { get; set; }
        [JsonProperty("mentor")]
        public MentorHref mentorHref { get; set; }
    }

    public class Self
    {
        [JsonProperty("href")]
        public String href { get; set; }
    }

    public class MentorHref
    {
        [JsonProperty("href")]
        public String href { get; set; }
    }
}
