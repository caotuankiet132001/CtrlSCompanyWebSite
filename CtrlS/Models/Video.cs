using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CtrlS.Models
{
    public class Video
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Link { get; set; }
        public string Type { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime DateTime { get; set; } 
        public DateTime DateTime2 { get; set; } 
        public int Status { get; set; }
    }
}