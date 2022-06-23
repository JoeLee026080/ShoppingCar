using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using System.Net;
using ShoppingCar.Models;
using System.Web.Security;

namespace ShoppingCar.Controllers
{
    public class HomeController : Controller
    {
        private ShoppingCarContext db = new ShoppingCarContext();

        public ActionResult Index()
        {
            //查詢全部商品
            var Products = db.Products.OrderByDescending(m => m.Id).ToList();
            return View(Products);
        }

        public ActionResult KeyWordSearch(string key)
        {
            //商品關鍵字搜尋
            var products = db.Products.Where(m => m.Name.Contains(key))
                .OrderByDescending(m => m.Id).ToList();
            return View("Index", products);
        }


        public ActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Register(Member NewMember)
        {
            //若模型沒有通過驗證則顯示目前的View
            if (ModelState.IsValid == false)
                return View();

            //查詢會員
            var member = db.Members.Where(m => m.UserId == NewMember.UserId).FirstOrDefault();

            bool is_member = member != null;

            if (is_member)
            {
                ViewBag.Message = "此帳號己註冊過";
                return View();
            }
            else
            {
                db.Members.Add(NewMember);
                db.SaveChanges();
                return RedirectToAction("Login");
            }
        }

    }
}