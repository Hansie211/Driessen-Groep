using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SharedLibrary.Models {
    public class Speaker : Person, ICopyFromRequest {

        [JsonIgnore]
        [ForeignKey( "EventID" )]
        public Event Event { get; set; }

        public int EventID { get; }

        public void CopyFromRequest( object request ) {

            Speaker source = (Speaker)request;

            FirstName   = source.FirstName;
            LastName    = source.LastName;
        }

        public Speaker() : base() {

        }

        public Speaker( int eventID ) : base() {

            EventID = eventID;
        }
    }
}
