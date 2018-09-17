using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using YoelProject.Helpers;

namespace YoelProject
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            var application = (HttpApplication)sender;

            var context = application.Context;
            //var context = new HttpContextWrapper(HttpContext.Current);
            var ex = context.Server.GetLastError().GetBaseException();

            if (ex != null)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    if (ex.Message.Contains("/favicon.ico"))
                    {
                        return;
                    }
                }
                else if (ex.InnerException != null)
                {
                    if (!string.IsNullOrEmpty(ex.InnerException.Message))
                    {
                        if (ex.InnerException.Message.Contains("/favicon.ico"))
                        {
                            return;
                        }
                    }
                }
            }
            // These values in the application state collection for use later by the error page.
            if (HttpContext.Current.Application["lastError"] == null)
                HttpContext.Current.Application.Add("lastError", ex);
            else
                HttpContext.Current.Application["lastError"] = ex;

            if (HttpContext.Current.Application["context"] == null)
                HttpContext.Current.Application.Add("context", context);

            else
                HttpContext.Current.Application["context"] = context;

            if (UserAgentIgnore(context) || UrlIgnorePhrases(context) || context.Request.Browser.Crawler)
            {
                return;
            }
            Uri aa = ((HttpApplication)sender).Context.Request.Url;
            // Code that runs when an unhandled error occurs
            //Exception ex = Server.GetLastError();
            if (ex is HttpUnhandledException && ex.InnerException != null)
            {
                ex = ex.InnerException;
            }


            if (ex != null)
            {
                string hostUrl = context.Request.Url.Scheme + "://" + context.Request.Url.Authority + ":" + context.Request.Url.Port;// System.Configuration.ConfigurationManager.AppSettings["ApplicationUrl"].ToString();//
                string errorurl = hostUrl + "/UnderMaintanance/Index";

                var httpException = ex as HttpException;
                if (httpException != null)
                {
                    Response.StatusCode = httpException.GetHttpCode();
                }

                if (ex.Message.Contains("Login failed for user"))
                {
                    context.Response.Redirect(errorurl);
                    return;
                }

                if (Response != null && Response.StatusCode == 404)
                {
                    var con = HttpContext.Current;
                    var request = con.Request;
                    string controller = request.RequestContext.RouteData.Values["controller"].ToString();
                    string action = request.RequestContext.RouteData.Values["action"].ToString();
                    if (!controller.Contains("Login") && action != "Index")
                    {
                        errorurl = hostUrl + "/Common/Error";
                        context.Response.Redirect(errorurl);
                        return;
                    }
                }

                SessionHelper SessionHelper = (SessionHelper)HttpContext.Current.Session["User"];
                if (SessionHelper != null)
                    SessionHelper.LoggedUserInfo.ExceptionMessage = ex.ToString();

                //Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex, "AllExceptionsPolicy");
                //Server.ClearError();


                UrlHelper u = new UrlHelper(context.Request.RequestContext);
                errorurl = hostUrl + "/Common/Error";
                context.Response.Redirect(errorurl);

            }
        }
        private static bool UserAgentIgnore(HttpContext context)
        {
            if (context.Request.UserAgent == null)
            {
                return false;
            }

            var userAgentIgnore = ConfigurationManager.AppSettings["UserAgentIgnore"] ?? "";
            var userAgentIgnoreList = new List<string>(userAgentIgnore.Split(';'));
            var userAgent = context.Request.UserAgent.ToLower();

            return userAgentIgnoreList.Contains(userAgent);
        }
        private static bool UrlIgnorePhrases(HttpContext context)
        {
            var urlIgnorePhrases = ConfigurationManager.AppSettings["URLIgnorePhrases"] ?? "";
            var urlIgnorePhrasesList = new List<string>(urlIgnorePhrases.Split(';'));

            return urlIgnorePhrasesList.Any(phrase => context.Request.Url.OriginalString.ToLower().Contains(phrase));
        }

    }
}
