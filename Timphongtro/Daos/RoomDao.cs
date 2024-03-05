
using QuanLyKhachSan.Models;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Web;

namespace QuanLyKhachSan.Daos
{
    public class RoomDao
    {
        QuanLyKhachSanDBContext myDb = new QuanLyKhachSanDBContext();


        public List<Room> GetRooms()
        {
            return myDb.rooms.ToList();
        }

        public List<Room> GetRoomTop5()
        {
            return myDb.rooms.OrderByDescending(x => x.view).Take(30).ToList();
        }

        public List<Room> GetRoomDiscount()
        {
            return myDb.rooms.Where(x => x.discount > 0).OrderByDescending(x => x.discount).Take(15).ToList();
        }

        public Room GetDetail(int id)
        {
            return myDb.rooms.FirstOrDefault(x => x.idRoom == id);
        }

        public List<Room> GetRoomByType(int typeId)
        {
            return myDb.rooms.Where(x => x.idType == typeId).ToList();
        }

        public List<Room> GetRoomsBlank(int page, int pagesize)
        {
            var arrIdRoom = myDb.bookings.Where(x => x.status == 0 || x.status == 1).Select(x => x.idRoom).Distinct().ToList();
            var allId = myDb.rooms.Select(x => x.idRoom).ToList();
            var ids = allId.Except(arrIdRoom).ToList();
            return myDb.rooms.Where(x => ids.Contains(x.idRoom)).ToList().Skip((page - 1) * pagesize).Take(pagesize).ToList();
        }
        public int GetNumberRoom()
        {
            var arrIdRoom = myDb.bookings.Where(x => x.status == 0 || x.status == 1).Select(x => x.idRoom).Distinct().ToList();
            var allId = myDb.rooms.Select(x => x.idRoom).ToList();
            var ids = allId.Except(arrIdRoom).ToList();
            int total = myDb.rooms.Where(x => ids.Contains(x.idRoom)).ToList().Count;
            int count = 0;
            count = total / 3;
            if (total % 3 != 0)
            {
                count++;
            }
            return count;
        }

        public List<Room> SearchByName(int page, int pagesize,string name, int numberRoom1, int numberRoom2)
        {
            var arrIdRoom = myDb.bookings.Where(x => x.status == 0 || x.status == 1).Select(x => x.idRoom).Distinct().ToList();
            var allId = myDb.rooms.Select(x => x.idRoom).ToList();
            var ids = allId.Except(arrIdRoom).ToList();
            return myDb.rooms.Where(x => ids.Contains(x.idRoom) && x.name.Contains(name) && x.numberRoom2 >= numberRoom2 && x.numberRoom1 >= numberRoom1).ToList().Skip((page - 1) * pagesize).Take(pagesize).ToList();
        }

        public List<Room> SearchByType(int page, int pagesize,int idType,int numberRoom1, int numberRoom2)
        {
            var arrIdRoom = myDb.bookings.Where(x => x.status == 0 || x.status == 1).Select(x => x.idRoom).Distinct().ToList();
            var allId = myDb.rooms.Select(x => x.idRoom).ToList();
            var ids = allId.Except(arrIdRoom).ToList();
            return myDb.rooms.Where(x => ids.Contains(x.idRoom) && x.idType == idType && x.numberRoom2 >= numberRoom2 && x.numberRoom1 >= numberRoom1).ToList().Skip((page - 1) * pagesize).Take(pagesize).ToList();
        }

        public int GetNumberRoomByType(int idType, int numberRoom1, int numberRoom2)
        {
            var arrIdRoom = myDb.bookings.Where(x => x.status == 0 || x.status == 1).Select(x => x.idRoom).Distinct().ToList();
            var allId = myDb.rooms.Select(x => x.idRoom).ToList();
            var ids = allId.Except(arrIdRoom).ToList();
            int total = myDb.rooms.Where(x => ids.Contains(x.idRoom) && x.idType == idType && x.numberRoom2 >= numberRoom2 && x.numberRoom1 >= numberRoom1).ToList().Count;
            int count = 0;
            count = total / 3;
            if (total % 3 != 0)
            {
                count++;
            }
            return count;
        }

        public int GetNumberRoomByName(string name, int numberRoom1, int numberRoom2)
        {
            var arrIdRoom = myDb.bookings.Where(x => x.status == 0 || x.status == 1).Select(x => x.idRoom).Distinct().ToList();
            var allId = myDb.rooms.Select(x => x.idRoom).ToList();
            var ids = allId.Except(arrIdRoom).ToList();
            int total = myDb.rooms.Where(x => ids.Contains(x.idRoom) && x.name.Contains(name) && x.numberRoom2 >= numberRoom2 && x.numberRoom1 >= numberRoom1).ToList().Count;
            int count = 0;
            count = total / 3;
            if (total % 3 != 0)
            {
                count++;
            }
            return count;
        }

        public List<Room> SearchByTypeAndName(int page, int pagesize, int idType,string name, int numberRoom1, int numberRoom2)
        {
            var arrIdRoom = myDb.bookings.Where(x => x.status == 0 || x.status == 1).Select(x => x.idRoom).Distinct().ToList();
            var allId = myDb.rooms.Select(x => x.idRoom).ToList();
            var ids = allId.Except(arrIdRoom).ToList();
            return myDb.rooms.Where(x => ids.Contains(x.idRoom) && x.idType == idType && x.name.Contains(name) && x.numberRoom2 >= numberRoom2 && x.numberRoom1 >= numberRoom1).ToList().Skip((page - 1) * pagesize).Take(pagesize).ToList();
        }

        public int GetNumberRoomByNameAndType(string name, int idType, int numberRoom1, int numberRoom2)
        {
            var arrIdRoom = myDb.bookings.Where(x => x.status == 0 || x.status == 1).Select(x => x.idRoom).Distinct().ToList();
            var allId = myDb.rooms.Select(x => x.idRoom).ToList();
            var ids = allId.Except(arrIdRoom).ToList();
            int total = myDb.rooms.Where(x => ids.Contains(x.idRoom) && x.name.Contains(name) && x.idType == idType && x.numberRoom2 >= numberRoom2 && x.numberRoom1 >= numberRoom1).ToList().Count;
            int count = 0;
            count = total / 3;
            if (total % 3 != 0)
            {
                count++;
            }
            return count;
        }

        public void add(Room room)
        {
            myDb.rooms.Add(room);
            myDb.SaveChanges();
        }

        public void delete(int id)
        {
            var obj = myDb.rooms.FirstOrDefault(x => x.idRoom == id);
            myDb.rooms.Remove(obj);
            myDb.SaveChanges();
        }

        public void update(Room room)
        {
            var obj = myDb.rooms.FirstOrDefault(x => x.idRoom == room.idRoom);
            obj.name = room.name;
            obj.image = room.image;
            obj.description = room.description;
            obj.discount = room.discount;
            obj.cost = room.cost;
            obj.idType = room.idType;
            obj.numberRoom1 = room.numberRoom1;
            obj.numberRoom2 = room.numberRoom2;
            myDb.SaveChanges();
        }

        public void updateView(int id)
        {
            var obj = myDb.rooms.FirstOrDefault(x => x.idRoom == id);
            obj.view = obj.view + 1;
            myDb.SaveChanges();
        }

        public List<Booking> getCheck(int id)
        {
            return myDb.bookings.Where(x => x.idRoom == id).ToList();
        }

        public Tuple<int, List<Room>> Search(int page, int pageSize, string name, string cityCode, int districtId)
        {
            var cityName = myDb.Cities.Where(x => x.Code == cityCode)
                .Select(x => x.Name)
                .FirstOrDefault();

            var districtName = myDb.Districts.Where(x => x.DistrictId == districtId)
                .Select(x => x.Name)
                .FirstOrDefault();

            var arrIdRoom = myDb.bookings.Where(x => x.status == 0 || x.status == 1)
                .Select(x => x.idRoom)
                .Distinct()
                .ToList();
            var allId = myDb.rooms
                .Select(x => x.idRoom).ToList();
            var ids = allId.Except(arrIdRoom).ToList();

            List<Room> avaiableRooms = myDb.rooms.Where(x => ids.Contains(x.idRoom)).ToList();
            // Tìm tất cả các phòng có chứa từ trong tiêu đề hoặc nội dung
            if (!string.IsNullOrEmpty(name))
            {
                avaiableRooms = avaiableRooms.Where(x => x.name.ToLower().Contains(name.ToLower()) 
                        || x.description.ToLower().Contains(name.ToLower()))
                            .ToList();
            }

            // Tìm từ có chứa trong tiêu đề
            if (!string.IsNullOrEmpty(cityName))
            {
                avaiableRooms = avaiableRooms.Where(x => x.name.ToLower().Contains(cityName.ToLower()))
                    .ToList();
            }

            // Tìm từ có chứa trong tiêu đề
            if (!string.IsNullOrEmpty(districtName))
            {
                avaiableRooms = avaiableRooms.Where(x => x.name.ToLower().Contains(districtName.ToLower()))
                    .ToList();
            }

            int total = avaiableRooms.Count;
            int pageCount = 0;
            pageCount = total / pageSize;
            if (total % pageSize != 0)
            {
                pageCount++;
            }
            return new Tuple<int, List<Room>>(pageCount, avaiableRooms
                .Skip((page - 1) * pageSize).Take(pageSize).ToList());
        }
    }
}