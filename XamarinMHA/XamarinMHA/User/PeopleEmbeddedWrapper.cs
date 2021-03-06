﻿using XamarinMHA;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace PeopleModel
{
    public class PeopleEmbeddedWrapper
    {
        [JsonProperty("_embedded")]
        public Embedded Embedded { get; set; }
    }

    public class Embedded
    {
        [JsonProperty("people")]
        public List<Person> People { get; set; }
    }

    [JsonObject("people")]
    public class Person
    {
        public Person(string FirstName, string LastName, string UserName, string Password, string Email, string Phone, string DateOfBirth, string Messenger, string Skype, string WhatsApp, string Viber, string DefaultSocial, string Approved, string Submitted)
        {
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.UserName = UserName;
            this.Password = Password;
            this.Email = Email;
            this.Phone = Phone;
            this.DateOfBirth = DateOfBirth;
            this.Messenger = Messenger;
            this.Skype = Skype;
            this.WhatsApp = WhatsApp;
            this.Viber = Viber;
            this.DefaultSocial = DefaultSocial;
            this.Approved = Approved;
            this.Submitted = Submitted;
            this.FirstLastName = FirstName + " " + LastName;
            this.FavoriteResources = new List<String>();
            this.SuggestedResources = new List<String>();
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
        [JsonProperty("messenger")]
        public String Messenger { get; set; }
        [JsonProperty("skype")]
        public String Skype { get; set; }
        [JsonProperty("whatsApp")]
        public String WhatsApp { get; set; }
        [JsonProperty("viber")]
        public String Viber { get; set; }
        [JsonProperty("defaultSocial")]
        public String DefaultSocial { get; set; }
        [JsonProperty("approved")]
        public String Approved { get; set; }
        [JsonProperty("submitted")]
        public String Submitted { get; set; }
        [JsonProperty("suggestedResources")]
        public List<String> SuggestedResources { get; set; }
        [JsonProperty("favoriteResources")]
        public List<String> FavoriteResources { get; set; }
        [JsonProperty("_links")]
        public Link _links { get; set; }
        [JsonIgnore]
        public String FirstLastName { get; set; }
    }

    public class Link
    {
        [JsonProperty("self")]
        public Self self { get; set; }
        [JsonProperty("person")]
        public PersonHref personHref { get; set; }
    }

    public class Self
    {
        [JsonProperty("href")]
        public String href { get; set; }
    }

    public class PersonHref
    {
        [JsonProperty("href")]
        public String href { get; set; }
    }
}
