
using System.ComponentModel.DataAnnotations;

namespace Launchpad.Models {
    public class EditCategoryForm {
        [Required]
        [Display(Name = "Category Name")]
        [StringLength(100, MinimumLength = 1)]
        public string categoryName {get; set;}
    }
}