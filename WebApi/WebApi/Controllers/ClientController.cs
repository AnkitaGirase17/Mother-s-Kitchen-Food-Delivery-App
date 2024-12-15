using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApi.Models;
using System.Security.Cryptography;

namespace WebApi.Controllers
{    
    public class ClientController : Controller
    {
        
        // CREATE Client
        SqlConnection con = new SqlConnection("data source=DESKTOP-GFS3DJE\\SQLEXPRESS; uid=sa;  TrustServerCertificate=True; password=server@123; database=MK");
        SqlCommand cmd;
        SqlDataAdapter da;  
        DataSet ds = new DataSet(); 

        [HttpPost]
       public ActionResult UserRegister()
        {
            // insert | uopdate | delete
            con.Open();
            cmd = new SqlCommand("RegisterClient", con);
            cmd.Parameters.Add(new SqlParameter("@CID", Request.Form["CID"].ToString()));
            cmd.Parameters.Add(new SqlParameter("@CNAME",Request.Form["CNAME"].ToString()));
            cmd.Parameters.Add(new SqlParameter("@MOBILE", Request.Form["MOBILE"].ToString()));
            cmd.Parameters.Add(new SqlParameter("@LDATE", Request.Form["LDATE"].ToString()));
            cmd.Parameters.Add(new SqlParameter("@STATUS", Request.Form["STATUS"].ToString()));
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
            con.Close();

            CLIENTClass CC = new CLIENTClass();
            CC.STATUS = "SUCCESS";

            return Json(CC);
        }
        [HttpPost]
        public ActionResult Createdeleiveryaddress()// LOGIC NOT GET
        {
            con.Open();
         
          
            // insert | uopdate | delete  
         
            
            cmd = new SqlCommand("CREATE_DELIVERY_ADDRESS", con);
          
           
            cmd.Parameters.Add(new SqlParameter("@PIN", Request.Form["PIN"].ToString()));
            cmd.Parameters.Add(new SqlParameter("@ADL1", Request.Form["ADL1"].ToString()));
            cmd.Parameters.Add(new SqlParameter("@ADL2", Request.Form["ADL2"].ToString()));
            cmd.Parameters.Add(new SqlParameter("@ADL3", Request.Form["ADL3"].ToString()));
            cmd.Parameters.Add(new SqlParameter("@CITY", Request.Form["CITY"].ToString()));
            cmd.Parameters.Add(new SqlParameter("@STATE", Request.Form["STATE"].ToString()));
            cmd.Parameters.Add(new SqlParameter("@STATUS", Request.Form["STATUS"].ToString()));
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
            con.Close();

            DELIVERY_ADDRESSClass ADC = new DELIVERY_ADDRESSClass(); 
            ADC.STATUS = "SUCCESS";

            return Json(ADC);
        }
        [HttpPost]
        public ActionResult showClientName()
        {
            CLIENTClass CC = new CLIENTClass();
            var Mobile = Request.Form["MOBILE"].ToString();

            da = new SqlDataAdapter("GET_CLIENT_NAME", con);
            da.SelectCommand.Parameters.Add(new SqlParameter("@MOBILE", Mobile));
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.Fill(ds, "CLIENT");
            CC.CNAME = ds.Tables["CLIENT"].Rows[0][0].ToString();

            return Json(CC);
        }

        // GET: Client INFO
        [HttpPost]
        public ActionResult ShowClientDetails()
        {

            CLIENTClass cc = new CLIENTClass();
            var CID = Request.Form["CID"].ToString();


            try
            {
                da = new SqlDataAdapter("GET_CLIENT_DETAILS", con);
                da.SelectCommand.Parameters.Add(new SqlParameter("@CID", CID));

                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.Fill(ds, "CLIENT");
                cc.CNAME = ds.Tables["CLIENT"].Rows[0][0].ToString();
                cc.MOBILE = ds.Tables["CLIENT"].Rows[0][1].ToString();
                
                

            }
            catch (Exception ee) { }


            return Json(cc);
        }
        [HttpPost]
        public ActionResult CheckMobile() // CHECK MOBILE IS IN DATABASE ORNOT ASK SIR FOR REPAIR IT 
        { 
            var MOBILE = Request.Form["MOBILE"].ToString();
            CLIENTClass CC = new CLIENTClass();
            da = new SqlDataAdapter("LoginClient", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add(new SqlParameter("@MOBILE",MOBILE));
            da.Fill(ds, "CLIENT");
           
            return Json(CC);
        }
        

        // GET: Client ADDRESS WITH DEFAULT STATUS
        [HttpPost]
        public ActionResult ShowAddress()
        {

            DELIVERY_ADDRESSClass dc = new DELIVERY_ADDRESSClass();
            var CID = Request.Form["CID"].ToString();
            var STATUS = Request.Form["STATUS"].ToString();

            try
            {
                da = new SqlDataAdapter("GET_CLIENT_DETAILS", con);
                da.SelectCommand.Parameters.Add(new SqlParameter("@CID", CID));
                da.SelectCommand.Parameters.Add(new SqlParameter("@STATUS", STATUS));
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.Fill(ds, "DELIVERY_ADDRESS");

                
                dc.PIN = ds.Tables["DELIVERY_ADDRESS"].Rows[0][0].ToString();
                dc.ADL1 = ds.Tables["DELIVERY_ADDRESS"].Rows[0][1].ToString();
                dc.ADL2 = ds.Tables["DELIVERY_ADDRESS"].Rows[0][2].ToString();
                dc.ADL3 = ds.Tables["DELIVERY_ADDRESS"].Rows[0][3].ToString ();
                dc.CITY = ds.Tables["DELIVERY_ADDRESS"].Rows[0][4].ToString();
                dc.STATE = ds.Tables["DELIVERY_ADDRESS"].Rows[0][5].ToString();
                dc.STATUS = ds.Tables["DELIVERY_ADDRESS"].Rows[0][6].ToString();


            }
            catch (Exception ee) { }


            return Json(dc);
        }

        // ALL ADDRESSS
        [HttpPost]
        public ActionResult ShowAllAddress()
        {
            List<DELIVERY_ADDRESSClass> lcc = new List<DELIVERY_ADDRESSClass>();
            DELIVERY_ADDRESSClass address = new DELIVERY_ADDRESSClass();
            var CID = Request.Form["CID"].ToString();
            string s = "";
            try
            {
                da = new SqlDataAdapter("GET_DELIVERY_ADDRESS_LIST", con);
                da.SelectCommand.Parameters.Add(new SqlParameter("@CID", CID));
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.Fill(ds, "DELIVERY_ADDRESS");

                for (int I = 0; I < ds.Tables["DELIVERY_ADDRESS"].Rows.Count; I++)
                {
                    DELIVERY_ADDRESSClass dac = new DELIVERY_ADDRESSClass()
                    {
                        CNAME= ds.Tables["DELIVERY_ADDRESS"].Rows[I][0].ToString(),
                        PIN= ds.Tables["DELIVERY_ADDRESS"].Rows[I][1].ToString(),
                        ADL1 = ds.Tables["DELIVERY_ADDRESS"].Rows[I][2].ToString(),
                        ADL2 = ds.Tables["DELIVERY_ADDRESS"].Rows[I][3].ToString(),
                        ADL3 = ds.Tables["DELIVERY_ADDRESS"].Rows[I][4].ToString(),
                        CITY = ds.Tables["DELIVERY_ADDRESS"].Rows[I][5].ToString(),
                        STATE = ds.Tables["DELIVERY_ADDRESS"].Rows[I][6].ToString(),
                        STATUS= ds.Tables["DELIVERY_ADDRESS"].Rows[I][7].ToString(),   

                    };
                    lcc.Add(dac);
                }
            }
            catch (Exception ee) { }


            return Json(lcc);
        }


        //MARK ADRESS AS DEFULT

        [HttpPost]
        public ActionResult MakeAddressAsDefault_nonDaefult()
        {
            var CID = Request.Form["CID"].ToString();
            var AID = Request.Form["SERIAL"].ToString();
            // insert | uopdate | delete
            con.Open();
            cmd = new SqlCommand("MARK_ADDRESS_TO_DEFAULT_AND_REST_TO_NON_DEFAULT", con);
            da.SelectCommand.Parameters.Add(new SqlParameter("@CID", CID));
            da.SelectCommand.Parameters.Add(new SqlParameter("SERIAL", AID));
            cmd.Parameters.Add(new SqlParameter("@STATUS", Request.Form["STATUS"].ToString()));
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
            con.Close();


            return Json("");
        }


        // ALL Categories AT home page

        [HttpPost]
        public ActionResult ShowAllCategories()
        {
            List<PRODUCT_CATEGORYClass> lcc = new List<PRODUCT_CATEGORYClass>();
            PRODUCT_CATEGORYClass category = new PRODUCT_CATEGORYClass();
       //     var CID = Request.Form["CATID"].ToString();
            string s = "";
            try
            {
                da = new SqlDataAdapter("GET_CATEGORIES_LIST", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.Fill(ds, "PRODUCT_CATEGORY");

                for( int I =0; I< ds.Tables["PRODUCT_CATEGORY"].Rows.Count; I++)
                {
                    PRODUCT_CATEGORYClass pc = new PRODUCT_CATEGORYClass()
                    {
                        CATID = ds.Tables["PRODUCT_CATEGORY"].Rows[I][0].ToString(),
                        CATNAME = ds.Tables["PRODUCT_CATEGORY"].Rows[I][1].ToString(),
                        CATIMAGE = "http://192.168.223.77/" + ds.Tables["PRODUCT_CATEGORY"].Rows[I][2].ToString(),

                    };
                    lcc.Add(pc);
                }
            }catch (Exception ee) { }


            return Json(lcc);
        }

        // All Items ON Home Page
        [HttpPost]
        public ActionResult ShowAllItem()
        {
            List<ITEMClass> lcc = new List<ITEMClass>();
            


            string s = "";
            try
            {
                da = new SqlDataAdapter("GET_ALL_ITEMS_LIST", con);
            
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.Fill(ds, "ITEM");

                for (int I = 0; I < ds.Tables["ITEM"].Rows.Count; I++)
                {
                         ITEMClass ic = new ITEMClass()
                        {
                             CATID = ds.Tables["ITEM"].Rows[I][0].ToString(),
                             IID = ds.Tables["ITEM"].Rows[I][1].ToString(),
                             INAME = ds.Tables["ITEM"].Rows[I][2].ToString(),
                             MRP = ds.Tables["ITEM"].Rows[I][3].ToString(),
                             DISCOUNT = ds.Tables["ITEM"].Rows[I][4].ToString(),
                             SP = ds.Tables["ITEM"].Rows[I][5].ToString(),
                             BRIEF = ds.Tables["ITEM"].Rows[I][6].ToString(),
                             ICON= "http://192.168.223.77/" + ds.Tables["ITEM"].Rows[I][7].ToString(),

                         };
                    lcc.Add(ic); 
                }
            }
            catch (Exception ee) { }


            return Json(lcc);
        }

        // after select category fetch the all items of category

        [HttpPost]
        public ActionResult ShowAllItemListedbycategories()
        {
            List<ITEMClass> lcc = new List<ITEMClass>();
            var CATID = Request.Form["CATID"].ToString();
            string s = "";
            try
            {
                da = new SqlDataAdapter("GET_ALL_ITEMS_LIST_BY_CAT_ID", con);
                da.SelectCommand.Parameters.Add(new SqlParameter("@CIATD", CATID));
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.Fill(ds, "ITEM");

                for (int I = 0; I < ds.Tables["ITEM"].Rows.Count; I++)
                {
                    ITEMClass ic = new ITEMClass()
                    {
                        CATID = ds.Tables["ITEM"].Rows[I][0].ToString(),
                        IID = ds.Tables["ITEM"].Rows[I][1].ToString(),
                        INAME = ds.Tables["ITEM"].Rows[I][2].ToString(),
                        MRP = ds.Tables["ITEM"].Rows[I][3].ToString(),
                        DISCOUNT = ds.Tables["ITEM"].Rows[I][4].ToString(),
                        SP = ds.Tables["ITEM"].Rows[I][5].ToString(),
                        BRIEF = ds.Tables["ITEM"].Rows[I][6].ToString(),
                        ICON = "http://192.168.223.77/" + ds.Tables["ITEM"].Rows[I][7].ToString(),

                    };
                    lcc.Add(ic);
                }
            }
            catch (Exception ee) { }


            return Json(lcc);
        }

        // on details of the item on detailm page
        [HttpPost]
        public ActionResult ShowItemDetails()
        {
            var IID = Request.Form["IID"].ToString();
            ITEMClass ic = new ITEMClass();

            try
            {
                da = new SqlDataAdapter("GET_ITEM_DETAILS", con);
                da.SelectCommand.Parameters.Add(new SqlParameter("@IID", IID));
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.Fill(ds, "ITEM");
                ic.CATID = ds.Tables["ITEM"].Rows[0][0].ToString();
                ic.IID = ds.Tables["ITEM"].Rows[0][1].ToString();
                ic.INAME = ds.Tables["ITEM"].Rows[0][2].ToString();
              
                ic.MRP = ds.Tables["ITEM"].Rows[0][3].ToString();
                ic.DISCOUNT = ds.Tables["ITEM"].Rows[0][4].ToString();
                ic.SP = ds.Tables["ITEM"].Rows[0][5].ToString();
                ic.TAX_NAME = ds.Tables["ITEM"].Rows[0][6].ToString();
                ic.TAX_PERCEMENT = ds.Tables["ITEM"].Rows[0][7].ToString();
                ic.UOM = ds.Tables["ITEM"].Rows[0][8].ToString();
                ic.AM = ds.Tables["ITEM"].Rows[0][9].ToString();
                ic.FOOD_TYPE = ds.Tables["ITEM"].Rows[0][10].ToString();
                ic.BRIEF = ds.Tables["ITEM"].Rows[0][11].ToString();
                ic.AI = ds.Tables["ITEM"].Rows[0][12].ToString();
                ic.CONTENTS = ds.Tables["ITEM"].Rows[0][13].ToString();
                ic.ICON = "http://192.168.223.77/" + ds.Tables["ITEM"].Rows[0][14].ToString();
                ic.IMG1 = "http://192.168.223.77/" + ds.Tables["ITEM"].Rows[0][15].ToString();
                ic.IMG2 = "http://192.168.223.77/" + ds.Tables["ITEM"].Rows[0][16].ToString();
                ic.MAX_ORDER = ds.Tables["ITEM"].Rows[0][17].ToString();
                ic.STATUS = ds.Tables["ITEM"].Rows[0][18].ToString();
                ic.LMODIFY = ds.Tables["ITEM"].Rows[0][19].ToString();

            }
            catch (Exception ee) { }


            return Json(ic);
        }
        // add to cart

        [HttpPost]
        public ActionResult additemincart()
        {
            // insert | uopdate | delete
            con.Open();
            cmd = new SqlCommand("IsertOrUpdateItemsInCart", con);
            cmd.Parameters.Add(new SqlParameter("@CID", Request.Form["CID"].ToString()));
            cmd.Parameters.Add(new SqlParameter("@BID", Request.Form["BID"].ToString()));
            cmd.Parameters.Add(new SqlParameter("@OID", Request.Form["OID"].ToString()));
            cmd.Parameters.Add(new SqlParameter("@CATID", Request.Form["CATID"].ToString()));
            cmd.Parameters.Add(new SqlParameter("@CNAME", Request.Form["CNAME"].ToString()));
            cmd.Parameters.Add(new SqlParameter("@IID", Request.Form["IID"].ToString()));
            cmd.Parameters.Add(new SqlParameter("@INAME", Request.Form["INAME"].ToString()));
            cmd.Parameters.Add(new SqlParameter("@BATCH_NO",Request.Form["BATCH_NO"].ToString()));
            cmd.Parameters.Add(new SqlParameter("@MFGDATE", Request.Form["MFGDATE"].ToString()));
            cmd.Parameters.Add(new SqlParameter("@EXPDATE", Request.Form["EXPDATE"].ToString()));
            cmd.Parameters.Add(new SqlParameter("@QT", Request.Form["QT"].ToString()));
            cmd.Parameters.Add(new SqlParameter("@RATE", Request.Form["RATE"].ToString()));
            cmd.Parameters.Add(new SqlParameter("@DISCOUNT", Request.Form["DISCOUNT"].ToString()));
            cmd.Parameters.Add(new SqlParameter("@SRATE", Request.Form["SRATE"].ToString()));
            cmd.Parameters.Add(new SqlParameter("@NON_TAX_RATE", Request.Form["NON_TAX_RATE"].ToString()));
            cmd.Parameters.Add(new SqlParameter("@TAX_NAME", Request.Form["TAX_NAME"].ToString()));
            cmd.Parameters.Add(new SqlParameter("@TAX_RATE", Request.Form["TAX_RATE"].ToString()));
            cmd.Parameters.Add(new SqlParameter("@CGST_PER", Request.Form["CGST_PER"].ToString()));
            cmd.Parameters.Add(new SqlParameter("@CGST_AMT", Request.Form["CGST_AMT"].ToString()));
            cmd.Parameters.Add(new SqlParameter("@SGST_PER", Request.Form["SGST_PER"].ToString()));
            cmd.Parameters.Add(new SqlParameter("@SGST_AMT", Request.Form["SGST_PER"].ToString()));
            cmd.Parameters.Add(new SqlParameter("@IGST_PER", Request.Form["IGST_PER"].ToString()));
            cmd.Parameters.Add(new SqlParameter("@IGST_AMT", Request.Form["IGST_AMT"].ToString()));
            cmd.Parameters.Add(new SqlParameter("@TAX_AMOUNT", Request.Form["TAX_AMOUNT"].ToString()));
            cmd.Parameters.Add(new SqlParameter("@AMOUNT", Request.Form["AMOUNT"].ToString()));
            cmd.Parameters.Add(new SqlParameter("@ICON", Request.Form["ICON"].ToString()));
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
            con.Close();
           
            

            return Json("");
        }


        //get quntity
        [HttpPost]
        public ActionResult GetQuntity()
        {

            ITEM_SUMMERYClass isc = new ITEM_SUMMERYClass();
            var CID = Request.Form["CID"].ToString();
            var OID = Request.Form["OID"].ToString(); 
            var IID = Request.Form["IID"].ToString();
       
            
                da = new SqlDataAdapter("GET_ITEM_QT_IF_ALREADY_ADDED_TO_CART", con);
                da.SelectCommand.Parameters.Add(new SqlParameter("@CID", CID));
                da.SelectCommand.Parameters.Add(new SqlParameter("@OID", OID));
                da.SelectCommand.Parameters.Add(new SqlParameter("@IID", IID));
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.Fill(ds, "ITEM_SUMMERY");

                isc.QT = ds.Tables["ITEM_SUMMERY"].Rows[0][0].ToString();
                

       


            return Json(isc);
        }
        
        [HttpPost]
        public ActionResult DeleteItemFromCart()//error
            
        {
            var IID = Request.Form["IID"].ToString();
            var OID = Request.Form["OID"].ToString();
            // insert | uopdate | delete
            con.Open();
            cmd = new SqlCommand("DELETE_ITEM_FROM_CART", con);
            da.SelectCommand.Parameters.Add(new SqlParameter("@IID", IID));
            da.SelectCommand.Parameters.Add(new SqlParameter("@OID", OID));
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
            con.Close();


            return Json("");
        }


      

        // GET ALL ITEM FOR CART

        [HttpPost]
        public ActionResult AllIetmforCart()//item
        {
            List<ITEM_SUMMERYClass> lisc = new List<ITEM_SUMMERYClass>();
            ITEM_SUMMERYClass ISC = new ITEM_SUMMERYClass();
            var CID = Request.Form["CID"].ToString();
            var OID = Request.Form["OID"].ToString();

            string s = "";
            try
            {
                da = new SqlDataAdapter("GET_ALL_ITEMS_FOR_CART", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add(new SqlParameter("@CID", CID));
                da.SelectCommand.Parameters.Add(new SqlParameter("@OID", OID));
                da.Fill(ds, " ITEM_SUMMERY");

                for (int I = 0; I < ds.Tables[" ITEM_SUMMERY"].Rows.Count; I++)
                {
                    ITEM_SUMMERYClass isc = new ITEM_SUMMERYClass()
                    {
                        SERIAL = ds.Tables[" ITEM_SUMMERY"].Rows[I][0].ToString(),
                        CATID = ds.Tables[" ITEM_SUMMERY"].Rows[I][1].ToString(),
                        CNAME = ds.Tables[" ITEM_SUMMERY"].Rows[I][2].ToString(),
                        ITEM_NAME = ds.Tables[" ITEM_SUMMERY"].Rows[I][4].ToString(),
                        QT = ds.Tables[" ITEM_SUMMERY"].Rows[I][5].ToString(),
                        SRATE = ds.Tables[" ITEM_SUMMERY"].Rows[I][6].ToString(),
                        AMOUNT = ds.Tables[" ITEM_SUMMERY"].Rows[I][7].ToString(),
                        ICON = "http://192.168.223.77/" + ds.Tables[" ITEM_SUMMERY"].Rows[I][8].ToString(),
                        IID = ds.Tables[" ITEM_SUMMERY"].Rows[I][3].ToString(),

                    };
                    lisc.Add(isc);
                }
            }
            catch (Exception ee) { }


            return Json(lisc);
        }

        // billing
        [HttpPost]
        public ActionResult BillForCart()
        {
            List<ORDER_SUMMERYClass> lcc = new List<ORDER_SUMMERYClass>();
            ORDER_SUMMERYClass OC = new ORDER_SUMMERYClass();
            var CID = Request.Form["CID"].ToString();
            var OID = Request.Form["OID"].ToString();
            string s = "";
            try
            {
                da = new SqlDataAdapter("GET_BILLING_SUMMARY_FOR_CART", con);
                da.SelectCommand.Parameters.Add(new SqlParameter("@CID", CID));
                da.SelectCommand.Parameters.Add(new SqlParameter("@OID", OID));
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.Fill(ds, " ORDER_SUMMERY");

                for (int I = 0; I < ds.Tables["ORDER_SUMMERY"].Rows.Count; I++)
                {
                    ORDER_SUMMERYClass oc = new ORDER_SUMMERYClass()
                    {
                        OID = ds.Tables["ORDER_SUMMERY"].Rows[I][0].ToString(),
                        GRAND_TOTAL = ds.Tables["ORDER_SUMMERY"].Rows[I][1].ToString(),


                    };
                    lcc.Add(oc);
                }
            }
            catch (Exception ee) { }


            return Json(lcc);
        }

    }
}