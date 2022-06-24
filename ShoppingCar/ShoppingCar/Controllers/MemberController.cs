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

        public ActionResult AddCar(string PId)
        {
            //購物車加入商品
            string UserId = User.Identity.Name;
            var CerrentCarDetail = db.OrderDetails.Where(m => m.UserId == UserId && m.PId == PId && m.IsApproved == "NO").FirstOrDefault();

            bool IsOrder = CerrentCarDetail != null;

            //在購物車中  數量+1
            if (IsOrder)
            {
                CerrentCarDetail.Qty += 1;
            }
            //不再購物車中  新增一筆
            else
            {
                var product = db.Products.Where(m => m.PId == PId).FirstOrDefault();
                OrderDetail OrderDetial = new OrderDetail();
                OrderDetial.UserId = UserId;
                OrderDetial.PId = product.PId;
                OrderDetial.Name = product.Name;
                OrderDetial.Price = product.Price;
                OrderDetial.Qty = 1;
                OrderDetial.IsApproved = "NO";
                db.OrderDetails.Add(OrderDetial);
            }

            db.SaveChanges();
            return RedirectToAction("ShoppingCar");

        }

        public ActionResult DeleteCar(int Id)
        {
            //查詢要刪除的購物車明細
            var OrderDetail = db.OrderDetails.Where(m => m.Id == Id).FirstOrDefault();

            db.OrderDetails.Remove(OrderDetail);
            db.SaveChanges();
            return RedirectToAction("ShoppingCar");
        }


    }
}