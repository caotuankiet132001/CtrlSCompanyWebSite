﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CtrlS.Models
{
    public class CateBlog
    {
        [Key]
        public int Id { get; set; }
        public string CateName { get; set; }
    }
}