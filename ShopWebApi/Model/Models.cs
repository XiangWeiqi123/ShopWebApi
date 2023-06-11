using System.ComponentModel.DataAnnotations;

namespace ShopWebApi.Model
{
    public class User
    {
        public int UserID { get; set; }

        [Required(ErrorMessage ="用户名不能为空")]
        [StringLength(100,ErrorMessage ="用户名过长")]
        public string UserName { get; set; }

        [Required]
        [EmailAddress(ErrorMessage ="请输入有效的邮件地址")]
        public string Email { get; set; }
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        public bool IsDeleted { get; set; } // 软删除


        public List<Order> Orders { get; set; }
    }
    public class Order
    {
        public int OrderID { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderStatus { get; set; }

        public int UserID { get; set; }
        public User User { get; set; }

        public List<OrderDetail> OrderDetails { get; set; }
    }

    public class Product
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }

        public List<OrderDetail> OrderDetails { get; set; }
    }


    public class OrderDetail
    {
        public int OrderDetailID { get; set; }
        public int Quantity { get; set; }

        public int ProductID { get; set; }
        public Product Product { get; set; }

        public int OrderID { get; set; }
        public Order Order { get; set; }
    }



}
