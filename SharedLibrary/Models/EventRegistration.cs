using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SharedLibrary.Models {
    public class EventRegistration {

        [Key]
        public int ID { get; set; }

        [Required]
        [JsonIgnore]
        [ForeignKey( "EventID" )]
        public Event Event { get; set; }

        public int EventID { get; }

        [Required]
        [ForeignKey( "UserID" )]
        public User User { get; set; }

        public int UserID { get; }
    }
}
