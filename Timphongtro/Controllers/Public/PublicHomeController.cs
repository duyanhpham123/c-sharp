using QuanLyKhachSan.Daos;
using QuanLyKhachSan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyKhachSan.Controllers.Public
{
    public class PublicHomeController : Controller
    {
        RoomDao roomDao = new RoomDao();
        ServiceDao serviceDao = new ServiceDao();
        TypeDao typeDao = new TypeDao();
        GeoDao geoDao = new GeoDao();
        // GET: PublicHome
        public ActionResult Index()
        {
            ViewBag.ListRoomTop5 = roomDao.GetRoomTop5();
            ViewBag.ListRoomDiscount = roomDao.GetRoomDiscount();
            ViewBag.ListService = serviceDao.GetServicesTop5();
            ViewBag.ListType = typeDao.GetTypes();
            ViewBag.ListCity = geoDao.GetCities();
            ViewBag.active = "home";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.active = "contact";
            return View();
        }

        public ActionResult AboutUs()
        {
            ViewBag.active = "aboutus";
            return View();
        }

        public ActionResult Introduce()
        {
            ViewBag.active = "introduce";
            return View();
        }

        [HttpGet]
        public JsonResult GetDistricts(string cityCode)
        {
            var districts = geoDao.GetDistricts(cityCode);
            return Json(districts, JsonRequestBehavior.AllowGet);
        }
    }
}