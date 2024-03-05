using QuanLyKhachSan.Daos;
using QuanLyKhachSan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyKhachSan.Controllers.Public
{
    public class PublicRoomController : Controller
    {
        RoomDao roomDao = new RoomDao();
        ServiceDao serviceDao = new ServiceDao();
        BookingDao bookingDao = new BookingDao();
        BookingServiceDao bookingServiceDao = new BookingServiceDao();
        QuanLyKhachSanDBContext myDb = new QuanLyKhachSanDBContext();
        RoomCommentDao roomComment = new RoomCommentDao();
        // GET: PublicRoom
        public ActionResult Index(int page)
        {
            if (page == 0)
            {
                page = 1;
            }
            ViewBag.List = roomDao.GetRoomsBlank(page, 3);
            ViewBag.tag = page;
            ViewBag.pageSize = roomDao.GetNumberRoom();
            return View();
        }

        public ActionResult DetailRoom(int id,string mess)
        {
            ViewBag.mess = mess;
            ViewBag.listComment = roomComment.GetByIdRoom(id);
            ViewBag.Ave = roomComment.getAve(id);
            roomDao.updateView(id);
            Room obj = roomDao.GetDetail(id);
            ViewBag.Room = obj;
            ViewBag.ListService = serviceDao.GetServices();
            ViewBag.ListRoomRelated = roomDao.GetRoomByType(obj.idType);
            return View();
        }

        /*[HttpPost]
        public ActionResult Booking(Booking booking,int[] idService)
        {
            User user = (User)Session["USER"];
            string action = "DetailRoom/" + booking.idRoom;
            if (user == null)
            {              
                return RedirectToAction(action, new { mess = "ErrorLogin" });
            }
            else
            {
                Booking checkExist = bookingDao.CheckBooking(booking.idRoom);
                int priceService = 0;
                if (idService != null)
                {                 
                    for (int i = 0; i < idService.Count(); i++)
                    {

                        priceService += serviceDao.GetCostById(idService[i]);
                    }
                }
                
                if (checkExist == null || (checkExist != null && DateTime.Now > DateTime.Parse(checkExist.checkOutDate)))
                {
                    DateTime dateCheckout = DateTime.Parse(booking.checkOutDate);
                    DateTime dateCheckin = DateTime.Parse(booking.checkInDate);
                    int numberBooking = dateCheckout.Day - dateCheckin.Day;
                    Room room = roomDao.GetDetail(booking.idRoom);
                    booking.idUser = user.idUser;
                    booking.createdDate = DateTime.Now;
                    booking.isPayment = false;
                    booking.status = 0;
                    booking.totalMoney = (room.cost * numberBooking - room.cost * numberBooking * room.discount / 100) + priceService;
                    bookingDao.Add(booking);
                    if(idService != null)
                    {
                       for(int i = 0; i < idService.Count(); i++)
                       {
                            BookingService obj = new BookingService
                            {
                                idService = idService[i],
                                idBooking = booking.idBooking
                            };
                            bookingServiceDao.Add(obj);
                       }
                    }
                    return RedirectToAction(action, new { mess = "Success" });
                }
                else
                {
                    return RedirectToAction(action, new { mess = "ErrorExist" });
                }
            }
        }*/

        [HttpPost]
        public ActionResult Booking(Booking booking, int[] idService)
        {
            User user = (User)Session["USER"];
            string action = "DetailRoom/" + booking.idRoom;
            if (user == null)
            {
                return RedirectToAction(action, new { mess = "ErrorLogin" });
            }
            else
            {
                List<Booking> checkExist = bookingDao.CheckBook(booking.idRoom);
                int priceService = 0;
                if (idService != null)
                {
                    for (int i = 0; i < idService.Count(); i++)
                    {

                        priceService += serviceDao.GetCostById(idService[i]);
                    }
                }
                if(checkExist.Count == 0)
                {
                    DateTime dateCheckout = DateTime.Parse(booking.checkOutDate);
                    DateTime dateCheckin = DateTime.Parse(booking.checkInDate);
                    TimeSpan time = dateCheckout - dateCheckin;
                    int numberBooking = time.Days;
                    if(numberBooking <= 0)
                    {
                        return RedirectToAction(action, new { mess = "Error" });
                    }
                    Room room = roomDao.GetDetail(booking.idRoom);
                    booking.idUser = user.idUser;
                    booking.createdDate = DateTime.Now;
                    booking.isPayment = false;
                    booking.status = 0;
                    booking.totalMoney = (room.cost * numberBooking - room.cost * numberBooking * room.discount / 100) + priceService;
                    bookingDao.Add(booking);
                    if (idService != null)
                    {
                        for (int i = 0; i < idService.Count(); i++)
                        {
                            BookingService obj = new BookingService
                            {
                                idService = idService[i],
                                idBooking = booking.idBooking
                            };
                            bookingServiceDao.Add(obj);
                        }
                    }
                    return RedirectToAction(action, new { mess = "Success" });
                }
                else
                {
                    DateTime dateCheckout = DateTime.Parse(booking.checkOutDate);
                    DateTime dateCheckin = DateTime.Parse(booking.checkInDate);
                    foreach (Booking checkbooking in checkExist)
                    {
                        if((dateCheckin <= DateTime.Parse(checkbooking.checkOutDate) && dateCheckin >= DateTime.Parse(checkbooking.checkInDate)) || (dateCheckout <= DateTime.Parse(checkbooking.checkOutDate) && dateCheckout >= DateTime.Parse(checkbooking.checkInDate)))
                        {
                            return RedirectToAction(action, new { mess = "ErrorExist" });
                        }
                    }
                    TimeSpan time = dateCheckout - dateCheckin;
                    int numberBooking = time.Days;
                    if (numberBooking <= 0)
                    {
                        return RedirectToAction(action, new { mess = "Error" });
                    }
                    Room room = roomDao.GetDetail(booking.idRoom);
                    booking.idUser = user.idUser;
                    booking.createdDate = DateTime.Now;
                    booking.isPayment = false;
                    booking.status = 0;
                    booking.totalMoney = (room.cost * numberBooking - room.cost * numberBooking * room.discount / 100) + priceService;
                    bookingDao.Add(booking);
                    if (idService != null)
                    {
                        for (int i = 0; i < idService.Count(); i++)
                        {
                            BookingService obj = new BookingService
                            {
                                idService = idService[i],
                                idBooking = booking.idBooking
                            };
                            bookingServiceDao.Add(obj);
                        }
                    }
                    return RedirectToAction(action, new { mess = "Success" });
                }
            }
        }

        [HttpPost]
        public ActionResult Search(FormCollection form)
        {
            string name = form["name"] ?? "";
            string cityCode = form["cityCode"] ?? "";
            int districtId = Int32.Parse(form["districtId"] ?? "-1");
            return RedirectToAction("Search", new { page = 0, name = name
                , cityCode = cityCode, districtId = districtId });
        }

        [HttpGet]
        public ActionResult Search(int page, string name , string cityCode, int districtId)
        {
            if (page == 0)
            {
                page = 1;
            }

            var rooms = roomDao.Search(page, 3, name, cityCode, districtId);

            ViewBag.List = rooms.Item2;
            ViewBag.tag = page;
            ViewBag.key = 1;
            ViewBag.cityCode = cityCode;
            ViewBag.districtId = districtId;
            ViewBag.idType = 1;
            ViewBag.numberRoom1 = 1;
            ViewBag.numberRoom2 = 1;
            ViewBag.pageSize = rooms.Item1;

            return View();
        }

        [HttpPost]
        public JsonResult PostComment(string comment, int idRoom, int star)
        {
            User user = (User)Session["USER"];
            roomComment.Add(new RoomComment
            {
                createdDate = DateTime.Now,
                idRoom = idRoom,
                text = comment,
                idUser = user.idUser,
                star = star
            });
            return Json(true, JsonRequestBehavior.AllowGet);
        }

    }
}