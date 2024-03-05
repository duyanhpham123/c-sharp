using QuanLyKhachSan.Daos;
using QuanLyKhachSan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyKhachSan.Controllers.Admin
{
    public class AdminRoomController : Controller
    {
        RoomDao roomDao = new RoomDao();
        TypeDao typeDao = new TypeDao();
        // GET: Adminservice
        public ActionResult Index(string msg)
        {
            ViewBag.Msg = msg;
            ViewBag.List = roomDao.GetRooms();
            ViewBag.listType = typeDao.GetTypes();
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(Room room)
        {
            var file = Request.Files["file"];
            string reName = DateTime.Now.Ticks.ToString() + file.FileName;
            file.SaveAs(Server.MapPath("~/Content/images/" + reName));
            room.image = reName;
            room.view = 0;
            roomDao.add(room);
            return RedirectToAction("Index", new { msg = "1" });
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Update(Room room)
        {
            string reName = "";
            var objCourse = roomDao.GetDetail(room.idRoom);
            var file = Request.Files["file"];
            if (file.FileName == "")
            {
                reName = objCourse.image;
            }
            else
            {
                reName = DateTime.Now.Ticks.ToString() + file.FileName;
                file.SaveAs(Server.MapPath("~/Content/images/" + reName));
            }
            room.image = reName;
            roomDao.update(room);
            return RedirectToAction("Index", new { msg = "1" });
        }

        public ActionResult Delete(Room room)
        {
            var check = roomDao.getCheck(room.idRoom);
            if (check.Count == 0)
            {
                roomDao.delete(room.idRoom);
                return RedirectToAction("Index", new { msg = "1" });
            }
            else
            {
                return RedirectToAction("Index", new { msg = "2" });
            }
        }
    }
}