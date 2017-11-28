using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HelloWorld
{
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
    }
}
