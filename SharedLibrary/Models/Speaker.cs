using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SharedLibrary.Models {
    public class Speaker : Person, ICopyFromRequest {

        [JsonIgnore]
        public Event Event { get; set; }

        public void CopyFromRequest( object request ) {

            Speaker source = (Speaker)request;

            FirstName   = source.FirstName;
            LastName    = source.LastName;
        }
    }
}
