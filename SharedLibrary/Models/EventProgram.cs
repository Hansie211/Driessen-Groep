using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SharedLibrary.Models {
    public class EventProgram : ICopyFromRequest {

        [Key]
        public int ID { get; set; }

        [JsonIgnore]
        public Event Event { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        [Required]
        public string Description { get; set; }

        public void CopyFromRequest( object request ) {
            
            EventProgram source = (EventProgram)request;

            Title = source.Title;
            Location = source.Location;
            StartTime = source.StartTime;
            EndTime = source.EndTime;
            Description = source.Description;
        }
    }
}
