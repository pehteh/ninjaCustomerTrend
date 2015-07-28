using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CustomerTrendNinja.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Login(string email, string phone)
        {
            // Add call details from the user database.
            PfAuthParams pfAuthParams = new PfAuthParams();
            pfAuthParams.Username = email;
            pfAuthParams.Phone = phone;
            pfAuthParams.Mode = pf_auth.MODE_STANDARD;

            // Specify a client certificate 
            // NOTE: This file contains the private key for the client
            // certificate. It must be stored with appropriate file 
            // permissions.
            pfAuthParams.CertFilePath = "C:\\Users\\pehteh\\Documents\\visual studio 2015\\Projects\\MFA_Demo\\MFA_Demo\\pf\\cert_key.p12";

            // Perform phone-based authentication
            int callStatus;
            int errorId;

            if (pf_auth.pf_authenticate(pfAuthParams, out callStatus, out errorId))
            {
                System.Diagnostics.Debug.WriteLine("Multi-Factor Authentication succeeded.");
                return View("CustomerTrendMain");
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Multi-Factor Authentication failed.");
                return View("Index");
            }
        }
    }
}