using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SharedLibrary.Models
{
    public class User : Person, ICopyFromRequest
    {
        [Required]
        public SecurityLevel SecurityLevel { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [JsonIgnore]
        public string PasswordHash { get; set; }

        [Required]
        [JsonIgnore]
        public string PasswordSalt { get; set; }

        [Required]
        [JsonIgnore]
        public IList<EventOwnership> Ownerships { get; set; }

        public User() {

            Ownerships = new List<EventOwnership>();
        }

        public void CopyFromRequest( object request ) {

            User source = (User)request;

            FirstName       = source.FirstName;
            LastName        = source.LastName;
            Email           = source.Email;

            // Everyone can downgrade, unless you're a sysadmin
            if ( ( source.SecurityLevel < SecurityLevel )  && ( SecurityLevel != SecurityLevel.SystemAdministrator ) ){
                SecurityLevel = source.SecurityLevel;
            }
        }
    }
}
