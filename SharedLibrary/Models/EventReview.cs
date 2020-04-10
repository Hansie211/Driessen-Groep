using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SharedLibrary.Models {
    public class EventReview {

        [Key]
        public int ID { get; set; }

        [Required]
        public Event Event { get; set; }

        [Required]
        public User User { get; set; }

        [Required]
        public Rating Overal { get; set; }

        [Required]
        public Rating Speakers { get; set; }

        [Required]
        public Rating Location { get; set; }

        [Required]
        public Rating Facilities { get; set; }

        [Required]
        public string Comment { get; set; }
    }
}
