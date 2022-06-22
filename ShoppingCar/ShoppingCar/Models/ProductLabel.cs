using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ShoppingCar.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public class ProductLabel
    {
        public int Id { get; set; }

        [DisplayName("產品標籤")]
        public string Label { get; set; }
        public virtual ICollection<Product> ProductLabels { get; set; }
    }
}