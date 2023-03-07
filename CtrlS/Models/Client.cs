using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CtrlS.Models
{

    public class Client
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Img { get; set; } //Chứa đường dẫn
        public string ImgName { get; set; } //Chứa tên file hình
        public DateTime DateTime { get; set; } //Ngày tạo blog
        public DateTime DateTime2 { get; set; } //Ngày Sửa blog gần nhất
        public int Status { get; set; }
    }

}