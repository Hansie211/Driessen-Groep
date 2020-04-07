using System;
using System.Collections.Generic;
using System.Text;

namespace SharedLibrary.Models {
    public abstract class Person {
        public int ID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
