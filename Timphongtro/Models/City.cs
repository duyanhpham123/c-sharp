using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuanLyKhachSan.Models
{
    public class City
    {
        [Key]
        public int Id { get;set;}
        public string Code { get;set;}
        public string Name { get;set;}
    }
}