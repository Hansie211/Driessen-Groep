using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace SharedLibrary.Models {
    public class Event : ICopyFromRequest {

        [Key]
        public int ID { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public IList<EventProgram> Programs { get; set; }

        [Required]
        public IList<EventOwnership> Ownerships { get; set; }

        [Required]
        public IList<Speaker> Speakers { get; set; }

        [Required]
        public IList<EventReview> Reviews { get; set; }

        [Required]
        public IList<EventRegistration> Registrations { get; set; }

        public Event() {

            Programs    = new List<EventProgram>();
            Ownerships  = new List<EventOwnership>();
            Speakers    = new List<Speaker>();
            Reviews     = new List<EventReview>();
            Registrations = new List<EventRegistration>();
        }

        public void CopyFromRequest( object request ) {
            
            Event source = (Event)request;

            Title       = source.Title;
            Date        = source.Date;
            Location    = source.Location;
            Description = source.Description;
        }
    }
}
