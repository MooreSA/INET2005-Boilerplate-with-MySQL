
using System.ComponentModel.DataAnnotations;

namespace Launchpad.Models {
    public class LinkForm {
        
        [Display(Name = "Category")]
        public int categoryId { get; set; }
        [Required]
        [Display(Name = "Title")]
        [StringLength(255, MinimumLength = 1)]
        public string title { get; set; }
        [Required]
        [Display(Name = "URL")]
        [Url]
        [MaxLength(255)]
        public string url { get; set; }
        [Required]
        [Display(Name = "Pin This Link")]
        public bool pinned { get; set; }
    }
}