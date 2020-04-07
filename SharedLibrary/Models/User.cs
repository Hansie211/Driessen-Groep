using System;
using System.Collections.Generic;
using System.Text;

namespace SharedLibrary.Models
{
    public class User : Person
    {
        public SecurityLevel SecurityLevel { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public string PasswordSalt { get; set; }
    }
}
