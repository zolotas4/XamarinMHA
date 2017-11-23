using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld
{
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
        public String FirstLastName { get; set; }
    }
}

