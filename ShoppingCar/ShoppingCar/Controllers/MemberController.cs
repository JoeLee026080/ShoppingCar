using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ShoppingCar.Models; //表單驗證

namespace ShoppingCar.Controllers
{
    [Authorize]
    public class MemberController : Controller
    {
        private ShoppingCarContext db = new ShoppingCarContext();
        public ActionResult Index()
        {
            //查詢全部商品
            var products = db.Products.OrderByDescending(m => m.Id).ToList();

            //使用_LayoutMember
            return View("..\\Home\\Index", "_LayoutMember", products);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("index", "Home");
        }

        public ActionResult ShoppingCar()
        {
            string UserId = User.Identity.Name;
            //查詢購物車清單(未核准狀態的明細)
            var OrderDetails = db.OrderDetails.Where(m => m.UserId == UserId && m.IsApproved == "NO").ToList();

            return View(OrderDetails);
        }
    }
}