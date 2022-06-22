using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;


namespace ShoppingCar.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    public partial class OrderDetail
    {
        public int Id { get; set; }

        [DisplayName("訂單編號")]
        public string OrderGuid { get; set; }

        [DisplayName("會員帳號")]
        public string UserId { get; set; }

        [DisplayName("產品編號")]
        public string PId { get; set; }

        [DisplayName("品名")]
        public string Name { get; set; }

        [DisplayName("單價")]
        public Nullable<int> Price { get; set; }

        [DisplayName("訂購數量")]
        public Nullable<int> Qty { get; set; }

        [DisplayName("是否為訂單")]
        public string IsApproved { get; set; }

        //Navigation Property
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
