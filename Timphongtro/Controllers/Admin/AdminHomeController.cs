using QuanLyKhachSan.Daos;
using QuanLyKhachSan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyKhachSan.Controllers.Admin
{
    public class AdminHomeController : Controller
    {
        BookingDao bookingDao = new BookingDao();
        // GET: AdminHome
        public ActionResult Index()
        {
            User user = (User)Session["ADMIN"];
            if (user == null)
            {
                return RedirectToAction("Login", "PublicAuthentication");
            }
            else
            {
                ViewBag.Month1 = bookingDao.statictis(1);
                ViewBag.Month2 = bookingDao.statictis(2);
                ViewBag.Month3 = bookingDao.statictis(3);
                ViewBag.Month4 = bookingDao.statictis(4);
                ViewBag.Month5 = bookingDao.statictis(5);
                ViewBag.Month6 = bookingDao.statictis(6);
                ViewBag.Month7 = bookingDao.statictis(7);
                ViewBag.Month8 = bookingDao.statictis(8);
                ViewBag.Month9 = bookingDao.statictis(9);
                ViewBag.Month10 = bookingDao.statictis(10);
                ViewBag.Month11 = bookingDao.statictis(11);
                ViewBag.Month12 = bookingDao.statictis(12);
                return View();
            }

        }
    }
}