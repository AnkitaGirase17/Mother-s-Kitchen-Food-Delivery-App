﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class CLIENTClass
    {
        private int SERIALNO { get; set; }
        public string CID { get; set; }
        public string CNAME { get; set; }
        public string MOBILE { get; set; }
        public string LDATE { get; set; }
        public string STATUS { get; set; }
    }
}