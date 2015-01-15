using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcTest.Models;

namespace MvcTest.Controllers
{
    public class ResetPasswordController : Controller
    {
        // GET: ResetPassword
        public ActionResult Index(int id, string email)
        {
            if (email == null)
            {
                throw new ArgumentNullException("传入参数异常");
                //return RedirectToAction("Index", "Login");
            }
            ViewBag.id = id;
            ViewBag.email = email;
            return View();
        }

        // GET: ResetPassword/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ResetPassword/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ResetPassword/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ResetPassword/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ResetPassword/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, string Email)
        {
            //if (id == null)
            //{
            //    throw new ArgumentNullException("id不能为空");
            //}
            //if (Email == null )
            //{
            //    throw new ArgumentNullException("Email不能为空");
            //}

            Manager manager = new Manager();
            try
            {
                using (var db = new ToolsDBEntities())
                {
                    manager = db.Managers.Where(s => s.MID == id && s.Email == Email).FirstOrDefault();
                    manager.LoginPass = Request.Form["password"].ToString();
                    manager.UpdateTime = DateTime.Now;
                    db.SaveChanges();
                }


                return RedirectToAction("Index", "Login");
            }
            catch
            {
                return View();
            }
        }

        // GET: ResetPassword/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ResetPassword/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
