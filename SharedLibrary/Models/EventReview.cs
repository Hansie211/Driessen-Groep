using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SharedLibrary.Models {
    public class EventReview : ICopyFromRequest {

        [Key]
        public int ID { get; set; }

        [JsonIgnore]
        [ForeignKey("EventID")]
        public Event Event { get; set; }

        public int EventID { get; }

        [Required]
        public User User { get; set; }

        [Required]
        public float Overal { get; set; }

        [Required]
        public float Speakers { get; set; }

        [Required]
        public float Location { get; set; }

        [Required]
        public float Facilities { get; set; }

        [Required]
        public string Comment { get; set; }

        public void CopyFromRequest( object request ) {
            
            EventReview source = (EventReview)request;

            Overal = source.Overal;
            Speakers = source.Speakers;
            Location = source.Location;
            Facilities = source.Facilities;
            Comment = source.Comment;
        }
    }
}
