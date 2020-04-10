using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace SharedLibrary.Models {
    public class EventOwnership {

        [Key]
        public int ID { get; set; }

        [Required]
        [JsonIgnore]
        public Event Event { get; set; }

        [Required]
        public User User { get; set; }

        [Required]
        public OwnershipLevel OwnershipLevel { get; set; }
    }
}
