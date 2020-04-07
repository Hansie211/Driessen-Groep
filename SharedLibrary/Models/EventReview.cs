using System;
using System.Collections.Generic;
using System.Text;

namespace SharedLibrary.Models {
    public class EventReview {

        public int ID { get; set; }

        public User User { get; set; }

        public Rating Overal { get; set; }

        public Rating Speakers { get; set; }

        public Rating Location { get; set; }

        public Rating Facilities { get; set; }

        public string Comment { get; set; }
    }
}
