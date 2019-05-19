

namespace Restaurant.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class Category
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int CategoryOrder { get; set; }

        public ICollection<SubCategory> SubCategoriesCollection { get; set; }
    }
}
