using System;
using System.Collections.Generic;

namespace NewApi.Models {
    public class Grupo {
        public Guid id { get; set; }

        public string nome {get;set;}

        public virtual ICollection<Pessoa> pessoas {get; set;}
    }
}