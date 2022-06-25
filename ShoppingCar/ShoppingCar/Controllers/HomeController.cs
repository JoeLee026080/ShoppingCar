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
            var products = db.Products.OrderByDescending(m => m.Id).ToList();

            if (products == null)
                return HttpNotFound();
            
            return View(products);
        }

        public ActionResult KeyWordSearch(string key)
        {
            if (key == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            
            //商品關鍵字搜尋
            var products = db.Products.Where(m => m.Name.Contains(key))
                .OrderByDescending(m => m.Id).ToList();

            if (products == null)
                return HttpNotFound();
            
            return View("Index", products);
        }

        public ActionResult Login()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string UserId, string Pwd)
        {
            if (UserId == null || Pwd == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            //查詢會員
            var member = db.Members.Where(m => m.UserId == UserId && m.Pwd == Pwd).FirstOrDefault();

            bool is_member = member != null;

            //是會員
            if (is_member)
            {
                //紀錄歡迎詞
                Session["Welcome"] = member.Name + "歡迎光臨";
                //登入驗證
                FormsAuthentication.RedirectFromLoginPage(UserId, true);
                //導向首頁 Index
                if (member.UserId == "admin")
                    return RedirectToAction("Index", "Admin");
                else
                    return RedirectToAction("Index", "Member");
            }
            //非會員
            else
            {
                //通知
                ViewBag.Massage = "帳號或密碼錯誤";
                return View();
            }
        }

        public ActionResult Register()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "Id,UserId,Pwd,Name,Email")] Member NewMember)
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