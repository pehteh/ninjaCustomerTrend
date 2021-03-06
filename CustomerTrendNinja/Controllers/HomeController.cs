﻿using System;
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

        public ActionResult Login(string alias, string phone)
        {
            // Add call details from the user database.
            PfAuthParams pfAuthParams = new PfAuthParams();
            pfAuthParams.Username = alias + "@sqlninja.onmicrosoft.com";
            pfAuthParams.Phone = phone;
            pfAuthParams.Mode = pf_auth.MODE_STANDARD;

            // Specify a client certificate 
            // NOTE: This file contains the private key for the client
            // certificate. It must be stored with appropriate file 
            // permissions.
            pfAuthParams.CertFilePath = System.Web.HttpContext.Current.Server.MapPath("~/Libraries/Cert/cert_key.p12");
            
            // Perform phone-based authentication
            int callStatus;
            int errorId;

            if (pf_auth.pf_authenticate(pfAuthParams, out callStatus, out errorId))
            {
                System.Diagnostics.Debug.WriteLine("Multi-Factor Authentication succeeded.");
                ViewBag.Message = "";
                return View("CustomerTrendMain");
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Multi-Factor Authentication failed.");
                ViewBag.Message = "Multi-Factor Authentication failed.";
                return View("Index");
            }
        }
    }
}