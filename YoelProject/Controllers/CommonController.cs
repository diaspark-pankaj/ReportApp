using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YoelProject.Helpers;

namespace YoelProject.Controllers
{
    public class CommonController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Common/UnderConstruction/
        public ActionResult UnderConstruction()
        {
            return View();
        }

        //
        // GET: /Common/UnauthorizedAccess/
        public ActionResult UnauthorizedAccess()
        {
            return View();
        }

        public ActionResult URLExpired()
        {
            return View();
        }
        //
        // GET: /Common/Error/
        public ActionResult Error()
        {
            if (Session["User"] != null)
            {
                var sessionHelper = (SessionHelper)Session["User"];

                ViewBag.ExceptionMessage = sessionHelper.LoggedUserInfo.ExceptionMessage;
                if (sessionHelper.LoggedUserInfo != null && sessionHelper.LoggedUserInfo.ExceptionMessage != null ? sessionHelper.LoggedUserInfo.ExceptionMessage.Contains("A public action method") && sessionHelper.LoggedUserInfo.ExceptionMessage.Contains("was not found on controller"):false)
                {
                    return RedirectToAction("Home", "Home");
                }
            }

            return View();
        }

        // GET: /Common/SessionExpired/
        public ActionResult SessionExpired()
        {
            return View();
        }

        //
        // GET: /Common/PrivacyPolicy/
        public ActionResult PrivacyPolicy()
        {
            return View();
        }

        //
        // GET: /Common/TermsOfUse/
        public ActionResult TermsOfUse()
        {
            return View();
        }


        public ActionResult NotFound()
        {
            return View();
        }

    }
}