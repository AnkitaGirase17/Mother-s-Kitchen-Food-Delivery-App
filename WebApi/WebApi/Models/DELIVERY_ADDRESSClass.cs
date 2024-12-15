using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class DELIVERY_ADDRESSClass
    {
        private int SERIALNO { get; set; }
        public string CID { get; set; }
        public string CNAME { get; set; }
        public string MOBILE { get; set; }
        public string PIN { get; set; }

        public string ADL1 { get; set; }
        public string ADL2 { get; set; }
        public string ADL3 { get; set; }
        public string CITY { get; set; }
        public string STATE { get; set; }
        public string STATUS {get; set; }
    }
}