using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminModel
{
    public class AdminEmbeddedWrapper
    {
        [JsonProperty("_embedded")]
        public Embedded Embedded { get; set; }
    }

    public class Embedded
    {
        [JsonProperty("admins")]
        public List<Admin> Admins { get; set; }
        public Admin Admin { get; internal set; }
    }

    [JsonObject("admins")]
    public class Admin
    {
        public Admin(string FirstName, string LastName, string UserName, string Password, string Email)
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

    public class Link
    {
        [JsonProperty("self")]
        public Self self { get; set; }
        [JsonProperty("admin")]
        public AdminHref adminHref { get; set; }
    }

    public class Self
    {
        [JsonProperty("href")]
        public String href { get; set; }
    }

    public class AdminHref
    {
        [JsonProperty("href")]
        public String href { get; set; }
    }
}
