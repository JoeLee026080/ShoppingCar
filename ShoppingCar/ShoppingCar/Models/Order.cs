using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;


namespace ShoppingCar.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    public partial class Order
    {
        public int Id { get; set; }

        [DisplayName("訂單編號")]
        public string OrderGuid { get; set; }

        [DisplayName("會員帳號")]
        public string UserId { get; set; }

        [DisplayName("收件人姓名")]
        [Required]
        public string Receiver { get; set; }

        [DisplayName("收件人信箱")]
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [DisplayName("收件人地址")]
        [Required]
        public string Address { get; set; }

        [DisplayName("訂單日期")]
        public Nullable<System.DateTime> Date { get; set; }
        //Navigation Property
        public virtual ICollection<Order> Orders { get; set; }
    }
}
