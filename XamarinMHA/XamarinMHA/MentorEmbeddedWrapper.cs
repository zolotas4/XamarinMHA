using System;
using System.Collections.Generic;
using Newtonsoft.Json;


namespace MentorModel
{
    class MentorEmbeddedWrapper
    {
        [JsonProperty("_embedded")]
        public Embedded Embedded { get; set; }
    }

    class Embedded
    {
        [JsonProperty("mentors")]
        public List<Mentor> Mentors { get; set; }
    }

    [JsonObject("mentors")]
    class Mentor
    {
        public Mentor(string FirstName, string LastName, string UserName, string Password, string Email)
        {
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.UserName = UserName;
            this.Password = Password;
            this.Email = Email;
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
        [JsonProperty("_links")]
        public Link _links { get; set; }
        [JsonIgnore]
        public String FirstLastName { get; set; }
    }

    class Link
    {
        [JsonProperty("self")]
        public Self self { get; set; }
        [JsonProperty("mentor")]
        public MentorHref mentorHref { get; set; }
    }

    class Self
    {
        [JsonProperty("href")]
        public String href { get; set; }
    }

    class MentorHref
    {
        [JsonProperty("href")]
        public String href { get; set; }
    }
}
