using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuanLyKhachSan.Models
{
    public class District
    {
        [Key]
        public int Id { get;set;}
        public string CityCode { get;set;}
        public string Name { get;set;}
        public int DistrictId { get;set;}
    }
}