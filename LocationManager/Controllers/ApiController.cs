using LocationManager.Classes;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LocationManager.Controllers
{
    public class ApiController : Controller
    {
        // GET: Api
        public ActionResult Index()
        {
            return View();
        }
        public string SaveLocation(string locationEntry)
        {
            Newtonsoft.Json.Linq.JArray arr = Newtonsoft.Json.Linq.JArray.Parse(locationEntry);
            DataAccess data = new DataAccess();
            string msg;
            int code;
            foreach (dynamic obj in arr)
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@lat", Convert.ToString(obj.Latitude)));
                parameters.Add(new SqlParameter("@lon", Convert.ToString(obj.Longitude)));
                parameters.Add(new SqlParameter("@altitude", Convert.ToString(obj.Altitude)));
                parameters.Add(new SqlParameter("@trackingtime", Convert.ToDateTime(obj.TrackingTime)));
                data.RunProc("sp_saveNewLocation", parameters, out msg, out code);
            }
            return "Done";
        }
    }
}