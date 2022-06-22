using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;


namespace ShoppingCar.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    public partial class Product
    {
        public int Id { get; set; }

        [DisplayName("產品編號")]
        public string PId { get; set; }

        [DisplayName("品名")]
        public string Name { get; set; }

        [DisplayName("單價")]
        public Nullable<int> Price { get; set; }

        [DisplayName("圖示")]
        public string Img { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
