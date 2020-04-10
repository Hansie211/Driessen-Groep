using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SharedLibrary.Models
{
    public class User : Person
    {
        [Required]
        public SecurityLevel SecurityLevel { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required]
        public string PasswordSalt { get; set; }

        [Required]
        public IList<EventOwnership> Ownerships { get; set; }

        public User() {

            Ownerships = new List<EventOwnership>();
        }

    }
}
