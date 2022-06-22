namespace ShoppingCar.Models
{
    using System.Data.Entity;
    public partial class ShoppingCarContext : DbContext
    {
        public ShoppingCarContext() : base("ShoppingCarConnection")
        {

        }
        public DbSet<Member> Members { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
