using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;


namespace ShoppingCar.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    public partial class Member
    {
        public int Id { get; set; }

        [DisplayName("帳號")]
        [Required]
        public string UserId { get; set; }

        [DisplayName("密碼")]
        [Required]
        public string Pwd { get; set; }

        [DisplayName("姓名")]
        [Required]
        public string Name { get; set; }

        [DisplayName("信箱")]
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        //Navigation Property
        public virtual ICollection<Member> Members { get; set; }
    }
}
