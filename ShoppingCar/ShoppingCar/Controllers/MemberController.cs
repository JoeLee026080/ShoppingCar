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
            return View();
        }
    }
}