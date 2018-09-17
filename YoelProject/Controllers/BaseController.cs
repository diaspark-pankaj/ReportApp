using Exigent_BusinessLogicLayer.Admin;
using Exigent.Common.Enums;
using Exigent.Helpers;
using Exigent_ViewModels.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ChameleonInformExigent.Helpers;
using Exigent.Common;
using Exigent.ViewModels.Common;
using System.Web;
using ChameleonInformExigent.CustomAttributes;

namespace YoelProject.Controllers
{
    //[NoCache]
    public class BaseController : Controller
    {
        protected SessionHelper SessionHelper;


        public static int GridPageSize
        {

            get
            {
                if (System.Configuration.ConfigurationManager.AppSettings["GridPageSize"] == null)
                    return 10;

                return Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["GridPageSize"]);
            }
        }

        /// <summary>
        /// Get the url for back redirection & clear url from memory.
        /// </summary>
        public string RedirectBackUrl
        {
            get
            {
                if (TempData["RedirectBackUrl"] == null)
                    return null;

                return TempData["RedirectBackUrl"].ToString();
            }
            set
            {
                TempData["RedirectBackUrl"] = value;
            }
        }

        public string PageMessage
        {
            get
            {
                if (TempData["PageMessage"] == null)
                    return null;

                return TempData["PageMessage"].ToString();
            }
            set
            {
                TempData["PageMessage"] = value;
            }
        }

        public string MessageStyle
        {
            get
            {
                if (TempData["MessageStyle"] == null)
                    return null;

                return TempData["MessageStyle"].ToString();
            }
            set
            {
                TempData["MessageStyle"] = value;
            }
        }

        /// <summary>
        /// Get the url for back redirection & keep it maitained for next redirection.
        /// Proposed to use "RedirectBackUrl" property.
        /// Imp note: Use it only if required so.
        /// </summary>
        public string RedirectBackUrlKeep
        {
            get
            {
                if (TempData["RedirectBackUrl"] == null)
                    return null;

                var url = TempData["RedirectBackUrl"].ToString();
                TempData.Keep("RedirectBackUrl");
                return url;
            }
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            UserManager _userManager = new UserManager();
            if (Session["User"] == null)
            {
                if ((((System.Web.Mvc.ControllerContext)(filterContext)).Controller.ControllerContext.RouteData.Values["action"].ToString() == "Index" && ((System.Web.Mvc.ControllerContext)(filterContext)).Controller.ControllerContext.RouteData.Values["controller"].ToString() == "Home") && filterContext.ActionParameters["userid"] != null)
                {
                    if (filterContext.ActionParameters["userid"] != null)
                    {
                        FetchedUserViewModel user = _userManager.GetUserById((int)filterContext.ActionParameters["userid"]);
                        SetBaseSession(user);
                    }
                }
                else if ((((System.Web.Mvc.ControllerContext)(filterContext)).Controller.ControllerContext.RouteData.Values["action"].ToString() == "UnderConstruction" && ((System.Web.Mvc.ControllerContext)(filterContext)).Controller.ControllerContext.RouteData.Values["controller"].ToString() == "Common"))
                {
                    filterContext.Result = RedirectToAction("Index", "UnderMaintanance");
                }
                else
                {
                    filterContext.Result = RedirectToAction("SessionExpired", "Session", new { area = "", @returnUrl = HttpContext.Request.RawUrl, siteKeyword = Convert.ToString(Session["siteKeyword"]) });
                }
            }
            else
            {
                SessionHelper = (SessionHelper)Session["User"];
                if (_userManager.IsSessionExits(SessionHelper.LoggedUserInfo.UserId, SessionHelper.LoggedUserInfo.SessionId))
                {
                    int id = SessionHelper.LoggedUserInfo.UserId;
                    base.OnActionExecuting(filterContext);

                    if (_userManager.GetUserById(SessionHelper.LoggedUserInfo.UserId).Status == (int)UserStatus.JustCreated)
                    {
                        filterContext.Result = RedirectToAction("ForceChangePassword", "Login", new { area = "" });
                    }
                }
                else
                {
                    // clear user specific session.. by sessionid.
                    // also need to create sesssion with sessionid value.
                    Session.Clear();
                    filterContext.Result = RedirectToAction("Index", "Login", new { area = "" });
                }
            }
        }

        #region Methods

        public void ShowMessage(string message, MessageType messageType, bool useTempData = false)
        {
            ViewBag.MessageType = messageType;

            if (useTempData)
            {
                TempData["MessageType"] = messageType;
                TempData["Message"] = message;
            }

            ModelState.AddModelError(string.Empty, message);
        }

        public void RecallMessageIfAny()
        {
            var message = string.Empty;
            var messageType = string.Empty;

            if (TempData.ContainsKey("MessageType"))
            {
                messageType = TempData["MessageType"].ToString();
                ViewBag.MessageType = messageType;
            }
            if (TempData.ContainsKey("Message"))
            {
                message = TempData["Message"].ToString();
            }

            ModelState.AddModelError(string.Empty, message);
        }

        public void SetBaseSession(FetchedUserViewModel fetchedUserViewModel)
        {
            UserManager _userManager = new UserManager();
            if (fetchedUserViewModel != null)
            {
                SessionHelper = new SessionHelper
                {
                    LoggedUserInfo = new LoggedUserInfo
                    {
                        UserId = fetchedUserViewModel.UserId,
                        FullName = fetchedUserViewModel.FullName.Trim(),
                        ThumbImage = fetchedUserViewModel.ThumbImage,
                        Email = fetchedUserViewModel.Email,
                        Roles = fetchedUserViewModel.Roles,
                        UserAccessList = fetchedUserViewModel.UserAccessList,
                        SessionId = fetchedUserViewModel.SessionId,
                        SecurityStamp = fetchedUserViewModel.PasswordHash,
                        LawFirm = fetchedUserViewModel.LawFirm,
						LawFirm_ID = fetchedUserViewModel.LawFirm_ID,
                        SelectedDashboardID = fetchedUserViewModel.DefaultDashboardID,
                        RoleAppActivityList=fetchedUserViewModel.RoleAppActivityList,
                        //SelectedDashboardID = CommonHelper.GetLoggedInUserDashboard(fetchedUserViewModel.UserTypeId),
                        AccessDashboardPages = fetchedUserViewModel.UserAccessList != null ? fetchedUserViewModel.UserAccessList
                        .Select(x => new AccessDashboardViewModel()
                        {
                            ID = x.Rights,
                            Name = x.AccessDashboard.Name,
                            SortOrder = x.AccessDashboard.SortOrder,
                            IsActive = x.AccessDashboard.IsActive,
                            Description = x.AccessDashboard.Description,
                            AcessDashboardPages = x.AccessDashboard.AcessDashboardPages.Select(y => new AccessDashboardPageViewModel()
                            {
                                ID = y.ID,
                                Name = y.Name,
                                Description = y.Description,
                                IsActive = y.IsActive,
                                PageURL = y.PageURL,
                                IsDropdown = y.IsDropdown,
                                SortOrder = y.SortOrder,
                                DashboardID = y.DashboardID,
                                ImageURL = y.ImageURL,
                            }).ToList(),
                        }).ToList() : new List<AccessDashboardViewModel>(),
                    }
                };
            }
            Session["User"] = SessionHelper;
        }
        #endregion

    }
}