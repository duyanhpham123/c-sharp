using QuanLyKhachSan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyKhachSan.Daos
{
    public class GeoDao
    {
        QuanLyKhachSanDBContext myDb = new QuanLyKhachSanDBContext();
        public List<City> GetCities()
        {
            return myDb.Cities.ToList();
        }

        public List<District> GetDistricts(string cityCode)
        {
            return myDb.Districts.Where(x => x.CityCode == cityCode)
                .ToList();
        }
    }
}