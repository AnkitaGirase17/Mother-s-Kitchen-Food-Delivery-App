using System;
using System.Collections.Generic;
using System.Text;

namespace MothersKitchenClient.Models
{
    public class ITEMClass
    {
        private int SERIALNO { get; set; }
        public string CATID { get; set; }
        public string IID { get; set; }
        public string INAME { get; set; }
        public string MRP { get; set; }
        public string DISCOUNT { get; set; }
        public string SP { get; set; }
        public string TAX_NAME { get; set; }
        public string TAX_PERCEMENT { get; set; }
        public string UOM { get; set; }//KG
        public string AM { get; set; }//ACTUAL MEASURMENT
        public string FOOD_TYPE { get; set; }
        public string BRIEF { get; set; }//INGREDIANTS
        public string AI { get; set; }//ALERGIC INFO
        public string CONTENTS { get; set; }
        public string ICON { get; set; }
        public string IMG1 { get; set; }

        public string IMG2 { get; set; }
        public string MAX_ORDER { get; set; }
        public string STATUS { get; set; }
        public string LMODIFY { get; set; }
    }
}
