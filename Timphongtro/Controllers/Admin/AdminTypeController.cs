using QuanLyKhachSan.Daos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyKhachSan.Controllers.Admin
{
    public class AdminTypeController : Controller
    {
        TypeDao typeDao = new TypeDao();
        // GET: AdminType
        public ActionResult Index(string msg)
        {
            ViewBag.Msg = msg;
            ViewBag.List = typeDao.GetTypes();
            return View();
        }
        public ActionResult Add(QuanLyKhachSan.Models.Type type)
        {
            typeDao.add(type);
            return RedirectToAction("Index", new { msg = "1" });
        }

        public ActionResult Update(QuanLyKhachSan.Models.Type type)
        {
            typeDao.update(type);
            return RedirectToAction("Index", new { msg = "1" });
        }

        public ActionResult Delete(QuanLyKhachSan.Models.Type type)
        {
            var check = typeDao.getRoomType(type.idType);
            if(check.Count == 0)
            {
                typeDao.delete(type.idType);
                return RedirectToAction("Index", new { msg = "1" });
            }
            else
            {
                return RedirectToAction("Index", new { msg = "2" });
            }
        }
    }
}