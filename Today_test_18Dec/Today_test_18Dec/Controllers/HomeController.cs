using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Today_test_18Dec.Models;

namespace Today_test_18Dec.Controllers
{

    public class HomeController : Controller
    {
        today_task_18decEntities db = new today_task_18decEntities();
        public ActionResult Index()
        {
            return View(db.tbl_product.ToList());
        }

        public ActionResult create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult create([Bind(Include = "p_id,p_name,p_pricep_picture,p_description,p_category")] tbl_product product, HttpPostedFileBase file)

        {
            if (ModelState.IsValid)
            {
                string fileName = Path.GetFileNameWithoutExtension(file.FileName);
                string extension = Path.GetExtension(file.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                product.p_picture = "~/External/Products/" + fileName;
                fileName = Path.Combine(Server.MapPath("~/External/Products/"), fileName);
                file.SaveAs(fileName);

                db.tbl_product.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }


            return View(product);
        }




        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}