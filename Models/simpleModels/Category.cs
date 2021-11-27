using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Launchpad.Models {

    public class Category {
        public int id { get; set; }
        public string categoryName {get; set;}
        public List<Link> links {get; set;}
        
    }
}