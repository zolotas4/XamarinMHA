using HelloWorld;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace PeopleModel
{
    class PeopleEmbeddedWrapper
    {
        [JsonProperty("_embedded")]
        public Embedded Embedded { get; set; }
    }

    class Embedded
    {
        [JsonProperty("people")]
        public List<Person> People { get; set; }
    }

    [JsonObject("people")]
    class Person
    {
        public Person(string FirstName, string LastName, string UserName, string Password, string Email, string Phone, string DateOfBirth, string Approved, string Submitted)
        {
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.UserName = UserName;
            this.Password = Password;
            this.Email = Email;
            this.Phone = Phone;
            this.DateOfBirth = DateOfBirth;
            this.Approved = Approved;
            this.Submitted = Submitted;
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
        [JsonProperty("dateOfBirth")]
        public String DateOfBirth { get; set; }
        [JsonProperty("phone")]
        public String Phone { get; set; }
        [JsonProperty("approved")]
        public String Approved { get; set; }
        [JsonProperty("submitted")]
        public String Submitted { get; set; }
        [JsonProperty("_links")]
        public Link _links { get; set; }
        public String FirstLastName { get; set; }
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
