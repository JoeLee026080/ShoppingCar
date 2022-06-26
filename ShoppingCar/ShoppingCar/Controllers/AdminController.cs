using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShoppingCar.Models;
using System.IO;

namespace ShoppingCar.Controllers
{
    public class AdminController : Controller
    {
        private ShoppingCarContext db = new ShoppingCarContext();

        // GET: Products
        public ActionResult Index()
        {
            var product = db.Products.OrderByDescending(m => m.Id).ToList();
            return View(product);
        }

        public ActionResult AddProduct()
        {
            return View();
        }

        bool IsExisting(string target_ID)
        {
            //檢查商品ID是否已存在
            var Product = db.Products.Where(m => m.PId == target_ID).FirstOrDefault();
            return Product != null;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddProduct([Bind(Include = "Id,PId,Name,Price,Img")] Product product, HttpPostedFileBase ProductImg)
        {
            //新增商品
            if (!ModelState.IsValid)
                return View(product);

            //檢查商品ID是否已存在
            if (IsExisting(product.PId))
            {
                ViewBag.Error = "此商品ID已存在";
                return View();
            }

            //新增商品
            try
            {
                string FileName = "";
                //存圖
                if (ProductImg != null && ProductImg.ContentLength > 0)
                {
                    FileName = Guid.NewGuid().ToString() + ".jpg";
                    string path = Path.Combine(Server.MapPath("~/images"), FileName);
                    ProductImg.SaveAs(path);
                }

                //新增產品資訊
                Product NewProduct = new Product();
                NewProduct.PId = product.PId;
                NewProduct.Img = FileName;
                NewProduct.Name = product.Name;
                NewProduct.Price = product.Price;
                db.Products.Add(NewProduct);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ViewBag.Error = e.ToString();
                return View();
            }
        }

        public ActionResult DeleteProduct(string PId)
        {
            if (PId == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            //找出資料
            var product = db.Products.Where(m => m.PId == PId).FirstOrDefault();
            if (product == null)
                return HttpNotFound();

            db.Products.Remove(product);
            db.SaveChanges();

            //刪除
            return RedirectToAction("Index");
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PId,Name,Price,Img")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
