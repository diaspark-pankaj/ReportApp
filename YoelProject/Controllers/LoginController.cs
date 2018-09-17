
using System.Collections.Generic;
using System;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using YoelProject.ViewModel.Login;
using YoelProject.Admin;
using YoelProject.Helpers;
using YoelProject.Login;
using YoelProject.Common.Enums;
using YoelProject.Common.Constants;
using YoelProject.Admin.Exigent;

namespace YoelProject.Controllers
{
    public class LoginController : Controller
    {
        UserManager _userManager;


        public AuditViewModel ShowMessage(string message, MessageType messageType, string userName = "Anonymous", int? userId = null)
        {
            ViewBag.MessageType = messageType;
            ModelState.AddModelError(string.Empty, message);
            return new AuditViewModel
            {
                AuditID = Guid.NewGuid(),
                IPAddress = Request.ServerVariables["REMOTE_ADDR"],
                URLAccessed = Request.RawUrl,
                TimeAccessed = DateTime.UtcNow,
                UserName = userName,
                CustomMessage = message,
                UserId = Convert.ToInt32(userId)
            };
        }

        #region Actions
        //
        // GET: /Login/      
        public ActionResult Index(string returnAction, string returnController, string msg, string siteKeyword,MessageType? messageType)
        {
            LoginViewModel loginViewModel = new LoginViewModel
            {
                ReturnAction = returnAction,
                ReturnController = returnController,
                Msg = msg,
                SiteKeyword = siteKeyword,               
                ManageLoginPageVM = new ManageLoginPageViewModel()
            };
            //if (ExigentServiceHelper.isAuthCookieExist())
            //{
            //    HttpCookie authcookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            //    FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authcookie.Value);
            //    string[] userData = ticket.UserData.Split(':');
            //    loginViewModel.UserName = userData[0];
            //    loginViewModel.Password = userData[1];
            //    loginViewModel.RememberMe = true;
            //}
            //else
            //    loginViewModel.RememberMe = false;

            loginViewModel.RedirectURL = "Index";
            if (!string.IsNullOrEmpty(siteKeyword) && siteKeyword != Convert.ToString(Session["siteKeyword"]))
            {
                Session["siteKeyword"] = siteKeyword;
                if (Request.Cookies.Get("siteKeyword") == null)
                {
                    Request.Cookies.Remove("siteKeyword");
                }
                HttpCookie cookie = new HttpCookie("siteKeyword");
                cookie.Value = siteKeyword;
                cookie.Expires = DateTime.Now.AddDays(1);
                Response.Cookies.Add(cookie);
            }
            else if (Session["siteKeyword"] == null && string.IsNullOrEmpty(siteKeyword)) { Session["siteKeyword"] = siteKeyword; }
            else if (System.Web.HttpContext.Current.Request.UrlReferrer == null && string.IsNullOrEmpty(siteKeyword))
            { Session["siteKeyword"] = null; }
            if (!string.IsNullOrEmpty(msg))
            {
               ShowMessage(msg, messageType.Value);
            }
            if (!string.IsNullOrEmpty(returnAction) && !string.IsNullOrEmpty(returnController))
            {
                return RedirectToAction(returnAction, returnController);
            }
            string pwdChangedMsg = TempData["pwdChanged"] as string;
            //if (!string.IsNullOrEmpty(pwdChangedMsg))
               // ShowMessage(pwdChangedMsg, MessageType.success);

            //Gets notification and background image of login page 
          
            return View(loginViewModel);
        }

        //
        // GET: /Login/      
        public ActionResult Login(string returnAction, string returnController, string msg, MessageType messageType)
        {
            LoginViewModel loginViewModel = new LoginViewModel
            {
                ReturnAction = returnAction,
                ReturnController = returnController,
                Msg = msg
            };
            loginViewModel.RedirectURL = "Login";
            if (!string.IsNullOrEmpty(msg))
            {
               // ShowMessage(msg, messageType);
            }
            if (!string.IsNullOrEmpty(returnAction) && !string.IsNullOrEmpty(returnController))
            {
                return RedirectToAction(returnAction, returnController);
            }
            return View(loginViewModel);
        }

        //
        // POST: /Login/      
        [HttpPost]
        public ActionResult Index(LoginViewModel loginViewModel, string returnUrl = "")
        {
            
            _userManager = new UserManager();

            if (loginViewModel != null)
            {
                //Server-side validation
                if (!ModelState.IsValid)
                {
                    return View(loginViewModel);
                }

                //Check for Email existence 
                var user = _userManager.GetUserByUserName(loginViewModel.UserName);

                if (user == null)
                {
                    //Audit
                  

                    ShowMessage(VarConstants.UserNameNotExists, MessageType.danger);
                    return View(loginViewModel);
                }

                if (user != null && user.SessionId != null)
                {
                    ShowMessage(VarConstants.UserAlreadyLoggedIn, MessageType.danger);
                    return View(loginViewModel);
                }
               
                string sessionId = Guid.NewGuid().ToString();
                //Get values from AppSetting
                int acceptableInvalidPasswordAttempts = Convert.ToInt32(CoreManager.GetAllAppConfigSettings().First(i => i.AppKey == "AcceptableInvalidPasswordAttempts").AppVaue);
                int accountLockMinutes = Convert.ToInt32(CoreManager.GetAllAppConfigSettings().First(i => i.AppKey == "AccountLockMinutes").AppVaue);
                int passwordExpiryDaysCount = Convert.ToInt32(CoreManager.GetAllAppConfigSettings().First(i => i.AppKey == "PasswordExpiryDaysCount").AppVaue);


                // vyom dixit, added 3/15/2017
                loginViewModel.PasswordHash = user.PasswordHash; // set salt token.
                user.SessionId = sessionId;
                if (user.Status == (int)UserStatus.Inactive)
                {
                    //Audit
                    //AuditManager.SaveAudit("Account Suspended", "Login/Index", loginViewModel.UserName, user.UserId);

                    ShowMessage(VarConstants.AccountSuspended, MessageType.danger, loginViewModel.UserName, user.UserId);

                    return View(loginViewModel);

                }

                if (user.IsActive == false)
                {
                    //Audit
                    //AuditManager.SaveAudit("User InActive", "Login/Index", loginViewModel.UserName, user.UserId);
                    ShowMessage(VarConstants.AccountNotActive, MessageType.danger, loginViewModel.UserName, user.UserId);
                    return View(loginViewModel);
                }

                //Credential Validation
                int userUpdatedStatus;
                if (!_userManager.ValidateCredentials(loginViewModel))
                {
                    //Invalid Login Attempt (Invalid Password)
                    Outer:
                    int failedLoginAttempts;
                    DateTime? lockedDate;

                    _userManager.UpdateInvalidLoginAttempt(loginViewModel.UserName, out failedLoginAttempts, out lockedDate, user.Status);
                    AuditViewModel auditViewModel;
                    if (failedLoginAttempts < acceptableInvalidPasswordAttempts)
                    {
                        //Audit
                        //AuditManager.SaveAudit("Enter Invalid Username/Password", "Login/Index", loginViewModel.UserName, user.UserId);
                        auditViewModel = ShowMessage("Wrong Password, " + (acceptableInvalidPasswordAttempts - failedLoginAttempts) + " Attempts Remaining", MessageType.danger, loginViewModel.UserName, user.UserId);
                        auditViewModel = ShowMessage("Username / Password you have entered is invalid, please try again. Your account will get locked after six successive invalid password attempts. " + (acceptableInvalidPasswordAttempts - failedLoginAttempts) + " attempts remaining ", MessageType.danger, loginViewModel.UserName, user.UserId);
                    }

                    else
                    {
                        int elapsedMinutes = (DateTime.Now - lockedDate) != null ? (DateTime.Now - lockedDate).Value.Minutes : 0;
                        if (elapsedMinutes < accountLockMinutes)
                        {
                            //Audit
                            //AuditManager.SaveAudit("Account Locked", "Login/Index", loginViewModel.UserName, user.UserId);
                            auditViewModel = ShowMessage(" Your account has been locked for " + (accountLockMinutes - elapsedMinutes) + " minutes", MessageType.danger, loginViewModel.UserName, user.UserId);
                        }
                        else
                        {
                            //Lock Resume Case
                            _userManager.UpdateSuccessLoginAttempt(loginViewModel.UserName, user.Status, out userUpdatedStatus);
                            if (userUpdatedStatus == (int)UserStatus.JustCreated)
                            {
                                //Audit
                                //AuditManager.SaveAudit("Access ChangePassword", "Login/Index", loginViewModel.UserName, user.UserId);
                                TempData["userId"] = user.UserId;

                                //Set Session
                                SetSession(user);

                                return RedirectToAction("ForceChangePassword", "Login", new { area = "" });
                            }
                            goto Outer;
                        }
                    }
                    //_auditManager.SaveAudit(auditViewModel);
                    if (!string.IsNullOrEmpty(loginViewModel.RedirectURL))
                    { return View(loginViewModel); }
                    else
                    {
                        return View(loginViewModel);
                    }
                }

                //Password Expiry Check
                if (_userManager.IsPasswordExpired(user, passwordExpiryDaysCount))
                {
                    SetSession(user);
                    //Audit
                    //AuditManager.SaveAudit("Passpord Expired", "Login/Index", loginViewModel.UserName, user.UserId);

                    //_auditManager.SaveAudit(ShowMessage("Your password is expired", MessageType.danger, loginViewModel.UserName, user.UserId));
                    TempData["PwdExpireMsg"] = "Your password is expired";
                    TempData["userId"] = user.UserId;

                    var qsDictionary = new Dictionary<string, string>
                                    {
                                        { "AuthUID", user.UserId.ToString()}
                                    };

                    return RedirectToAction("ForceChangePassword", "Login", new { area = "", q = YoelProject.Common.Helpers.Crypto.Encrypt(qsDictionary) });
                }
                //Check for Status
                if (user.Status == (int)UserStatus.LockedFromActive || user.Status == (int)UserStatus.LockedFromJustCreated)
                {
                    int failedLoginAttempts = user.FailedLoginAttempts ?? 0;
                    DateTime? lockedDate = user.lockedDate;
                    if (failedLoginAttempts < acceptableInvalidPasswordAttempts)
                    {
                        //Audit
                        //AuditManager.SaveAudit("Enter Invalid Username/Password", "Login/Index", loginViewModel.UserName, user.UserId);

                        //_auditManager.SaveAudit(ShowMessage("Wrong Password," + (acceptableInvalidPasswordAttempts - failedLoginAttempts) + " Attempts Remaining", MessageType.danger, loginViewModel.UserName, user.UserId));
                        //_auditManager.SaveAudit(ShowMessage("Username / Password you have entered is invalid, please try again. Your account will get locked after six successive invalid password attempts. " + (acceptableInvalidPasswordAttempts - failedLoginAttempts) + " attempts remaining ", MessageType.danger, loginViewModel.UserName, user.UserId));

                        return View(loginViewModel);

                    }
                    int elapsedMinutes = (DateTime.Now - lockedDate) != null ? Convert.ToInt32((DateTime.Now - lockedDate).Value.TotalMinutes) : 0;
                    if (elapsedMinutes < accountLockMinutes)
                    {
                        ////Audit
                        //AuditManager.SaveAudit("Account Locked", "Login/Index", loginViewModel.UserName, user.UserId);
                        //_auditManager.SaveAudit(ShowMessage("Wrong Password. Your Account resume after " + (accountLockMinutes - elapsedMinutes) + "minutes", MessageType.danger, loginViewModel.UserName, user.UserId));
                        //_auditManager.SaveAudit(ShowMessage(" Your account has been locked for " + (accountLockMinutes - elapsedMinutes) + " minutes", MessageType.danger, loginViewModel.UserName, user.UserId));

                        return View(loginViewModel);

                    }

                    //Lock Resume Case
                    _userManager.UpdateSuccessLoginAttempt(loginViewModel.UserName, user.Status, out userUpdatedStatus);
                    if (userUpdatedStatus == (int)UserStatus.JustCreated)
                    {
                        //Audit
                        //AuditManager.SaveAudit("Visited Change Password", "Login/Index", loginViewModel.UserName, user.UserId);

                        TempData["userId"] = user.UserId;

                        //Set Session
                        SetSession(user);

                        return RedirectToAction("ForceChangePassword", "Login", new { area = "" });
                    }

                }


                // vyom dixit, TWO-FACT-CHANGE, 20-07-2017
                // Start.......
                // redirect user to validate two factor authentication
                string isTwoFactorAuthentication = "false";//System.Configuration.ConfigurationManager.AppSettings["IsTwoFactorAuthentication"].ToString();

                if (Convert.ToBoolean(isTwoFactorAuthentication))
                {
                    //var qsDictionary1 = new Dictionary<string, string>
                    //{
                    //    { "emailID",    user.Email.ToString()               },
                    //    { "userId",     user.UserId.ToString()              },
                    //    { "userName",   user.UserName                       },
                    //    { "portalType", loginViewModel.PortalType           },
                    //    { "rememberMe", loginViewModel.RememberMe.ToString()},
                    //    { "userPwd",    loginViewModel.Password             },
                    //    { "sessionId",  sessionId                           }
                    //};
                    //return RedirectToAction("SendVerificationCode", "Login", new { area = "", q = Exigent.Common.Helpers.Crypto.Encrypt(qsDictionary1) });
                }
                else
                {
                    //check for multiple Logins
                    //CheckLoginViewModel checkLogin = new CheckLoginViewModel();
                    //checkLogin.UserId = user.UserId;
                    //checkLogin.SessionId = sessionId;
                    //checkLogin.LoggedIn = true;

                    //_userManager.CheckUserLogin(checkLogin);
                }
                // End.......

                string selectedDashboardUrl = string.Empty;//GetRedirectURL(loginViewModel, user, returnUrl);
                SetSession(user);
                ExigentServiceHelper.callAuthCookie(user.UserName, loginViewModel.Password);
                return RedirectToAction("Index","Dashboard",new {@area="Admin"});
            }
            else
            {
                LoginViewModel _loginViewModel = new LoginViewModel();
                //ManageLoginPageManager manageLoginPageManager = new ManageLoginPageManager();
                //_loginViewModel.ManageLoginPageVM = manageLoginPageManager.GetLoginPageAttributes();
                return View(_loginViewModel);
            }
        }

        public void SetSession(FetchedUserViewModel fetchedUserViewModel)
        {
            _userManager = new UserManager();
            var sessionHelper = new SessionHelper
            {
                LoggedUserInfo = new LoggedUserInfo
                {
                    UserId = fetchedUserViewModel.UserId,
                    UserTypeId = fetchedUserViewModel.UserTypeId,
                    FullName = fetchedUserViewModel.FullName.Trim(),
                    ThumbImage = fetchedUserViewModel.ThumbImage,
                    Email = fetchedUserViewModel.Email,
                    UserName = fetchedUserViewModel.UserName.Trim(),                    
                    SessionId = fetchedUserViewModel.SessionId,
                    DateModified = fetchedUserViewModel.DateModified,
                    LawFirm = fetchedUserViewModel.LawFirm,
                    LawFirm_ID = fetchedUserViewModel.LawFirm_ID,
                    SelectedDashboardID = fetchedUserViewModel.DefaultDashboardID                    
                }
            };

            Session[CommonConstants.SESSION_USER] = sessionHelper;            

        }
        //
        // GET: /Login/
        public ActionResult LogOut()
        {
            _userManager = new UserManager();
            //Get Session Values
            var sessionHelper = (SessionHelper)Session["User"];

            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
            Response.Cache.SetNoStore();

            if (sessionHelper == null)
            {
                return RedirectToAction("SessionExpired", "Common");
            }
            if (_userManager.IsSessionExits(sessionHelper.LoggedUserInfo.UserId, sessionHelper.LoggedUserInfo.SessionId))
            {

                _userManager.DeleteFromSession(sessionHelper.LoggedUserInfo.UserId);
                Session.Clear();

                //Audit                
                return RedirectToAction("Index", "Login");
            }
            return RedirectToAction("Index", "Login");
        }     

        #endregion               
    }
}