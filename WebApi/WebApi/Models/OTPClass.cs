using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class OTPClass
    {
        private int SERIALNO { get; set; }
        public string MOBILE { get; set; }
        public string OTP { get; set; }
        public string ODATE { get; set; }

        public int OCOUNT { get; set; }
    }
}