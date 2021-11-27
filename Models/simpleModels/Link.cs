using System;
using System.ComponentModel.DataAnnotations;

namespace Launchpad.Models {

    public class Link {
        public int id { get; set; }
        public int categoryId {get; set;}
        public string url {get; set;}
        public string title {get; set;}
        public bool pinned {get; set;}
    }
}