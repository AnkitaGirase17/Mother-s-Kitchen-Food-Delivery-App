using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class B2BUSERClass
    {
        private int SERIALNO { get; set; }
        public string BID { get; set; }
        public string BNAME { get; set; }
        public string MOBILE { get; set; }
        public string CATEGORY { get; set; }
        public string ADDRESS { get; set; }
        public string PIN { get; set; }
        public string CITY { get; set; }
        public string STATE { get; set; }

        public string RDATE { get; set; }
        public string REDATE { get; set; }
        public string EXDATE { get; set; }
        public string LDATE { get; set; }
        public string RATING { get; set; }
        public string STATUS { get; set; }
    }
}