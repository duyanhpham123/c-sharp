using QuanLyKhachSan.Daos;
using QuanLyKhachSan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyKhachSan.Controllers.Public
{
    public class PublicUserController : Controller
    {
        UserDao userDao = new UserDao();
        QuanLyKhachSanDBContext myDb = new QuanLyKhachSanDBContext();
        // GET: PublicUser
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ProfileUser(int id,string mess)
        {
            ViewBag.profile = userDao.getInfor(id);
            ViewBag.mess = mess;
            return View();
        }

        [HttpPost]
        public ActionResult UpdateProfile(User user)
        {
            userDao.update(user);
            return RedirectToAction("ProfileUser", new { id = user.idUser, mess = "Success" });
        }

        [HttpPost]
        public ActionResult UpdatePassword(FormCollection form)
        {
            string passwordNew = form["password"];
            string rePasswordNew = form["rePassword"];
            int id = Int32.Parse(form["idUser"]);
            if (passwordNew.Equals(rePasswordNew))
            {
                User user = myDb.users.FirstOrDefault(x => x.idUser == id);
                user.password = userDao.md5(passwordNew);
                myDb.SaveChanges();
                return RedirectToAction("ProfileUser", new { id = id, mess = "Success" });
            }
            else
            {
                return RedirectToAction("ProfileUser", new { id = id, mess = "Error" });
            }
                    
        }
    }
}