using System.Web.Mvc;

namespace ChameleonInformExigent.Controllers
{
    public class SessionController : Controller
    {

        // GET: /Common/SessionExpired/
        public ActionResult SessionExpired(string returnUrl, string siteKeyword)
        {
            if (!string.IsNullOrEmpty(returnUrl))
            {
                if (string.IsNullOrEmpty(siteKeyword))
                {
                    siteKeyword = Request.Cookies.Get("siteKeyword") != null ? Request.Cookies.Get("siteKeyword").Value : string.Empty;
                }
                return RedirectToAction("SessionExpired", "Common");
            }
            else
            {
                return View();
            }

        }

    }
}