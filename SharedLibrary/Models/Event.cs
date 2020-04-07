using System;
using System.Collections.Generic;
using System.Text;

namespace SharedLibrary.Models {
    public class Event {

        public int ID { get; set; }
        
        public string Name { get; set; }

        public DateTime Date { get; set; }

        public string Location { get; set; }

        public string Description { get; set; }

        public List<EventProgram> Programs { get; set; }

        public List<User> Owners { get; set; }

        public List<Speaker> Speakers { get; set; }

        public List<EventReview> Reviews { get; set; }
    }
}
