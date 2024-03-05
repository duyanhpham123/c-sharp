using QuanLyKhachSan.Daos;
using QuanLyKhachSan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyKhachSan.Controllers.Admin
{
    public class AdminBookingController : Controller
    {
        BookingDao bookingDao = new BookingDao();
        // GET: AdminUser
        public ActionResult Index(string msg)
        {
            ViewBag.Msg = msg;
            ViewBag.List = bookingDao.getAll();
            return View();
        }

        public ActionResult Detail(int id)
        {
            ViewBag.Booking = bookingDao.GetBookingById(id);
            ViewBag.List = bookingDao.getBS(id);
            return View();
        }

        public ActionResult Bill(int id)
        {
            ViewBag.Booking = bookingDao.GetBookingById(id);
            ViewBag.List = bookingDao.getBS(id);
            return View();
        }

        public ActionResult Update(Booking booking)
        {
            bookingDao.update(booking);
            return RedirectToAction("Index", new { msg = "1" });
        }


    }
}