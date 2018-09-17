using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Data.Entity;
using YoelProject.Login;
using YoelProject.DAL.Repository;
using YoelProject.ViewModel.Login;
using YoelProject.Common.Helpers;
using YoelProject.Common.Enums;

namespace YoelProject.Admin
{
    public class UserManager
    {
        public FetchedUserViewModel GetUserByUserName(string userName)
        {
            using (UserRepository _userRepository = new UserRepository())
            {
                var user = _userRepository.First(r => r.UserName == userName);
                if (user == null)
                {
                    return null;
                }
                FetchedUserViewModel fetchedUserViewModel = new FetchedUserViewModel
                {
                    UserId = user.Id,
                    ThumbImage = user.ThumbImage,
                    Email = user.Email,
                    UserName = user.UserName,
                    FullName = user.FullName,
                    PasswordHash = user.PasswordHash,
                    Status = user.Status,
                    FailedLoginAttempts = user.FailedLoginAttempts,
                    lockedDate = user.LockedDate,
                    LastPasswordChangeDate = user.LastPasswordChangeDate,
                    IsActive = user.IsActive,
                    UserTypeId = user.EntityId  
                };

                return fetchedUserViewModel;
            }
        }

        public bool ValidateCredentials(LoginViewModel loginViewModel)
        {
            using (UserRepository _userRepository = new UserRepository())
            {
                return SaltGenerationHelper.VerifyHashedPassword(loginViewModel.PasswordHash, loginViewModel.Password);
            }
        }

        public void UpdateInvalidLoginAttempt(string userName, out int failedLoginAttempts, out DateTime? lockedDate, int userCurrStatus)
        {
            using (UserRepository _userRepository = new UserRepository())
            {
                var addUser = _userRepository.First(q => q.UserName == userName);
                if (addUser.FailedLoginAttempts == null)
                {
                    addUser.FailedLoginAttempts = 1;
                }
                else
                {
                    if (addUser.FailedLoginAttempts < 6)
                    {
                        addUser.FailedLoginAttempts += 1;
                        if (addUser.FailedLoginAttempts == 6)
                        {
                            addUser.LockedDate = DateTime.Now;
                            if (userCurrStatus == Convert.ToInt32(UserStatus.Active))
                            {
                                addUser.Status = Convert.ToInt32(UserStatus.LockedFromActive);
                            }
                            if (userCurrStatus == Convert.ToInt32(UserStatus.JustCreated))
                            {
                                addUser.Status = Convert.ToInt32(UserStatus.LockedFromJustCreated);
                            }
                        }
                    }
                }

                _userRepository.SaveChanges();

                failedLoginAttempts = addUser.FailedLoginAttempts ?? 1;
                lockedDate = addUser.LockedDate;
            }
        }

        public void UpdateSuccessLoginAttempt(string userName, int userCurrStatus, out int userUpdatedStatus)
        {
            using (UserRepository _userRepository = new UserRepository())
            {
                var addUser = _userRepository.First(q => q.UserName == userName);

                addUser.FailedLoginAttempts = null;
                if (userCurrStatus == Convert.ToInt32(UserStatus.LockedFromActive))
                {
                    addUser.Status = Convert.ToInt32(UserStatus.Active);
                }
                if (userCurrStatus == Convert.ToInt32(UserStatus.LockedFromJustCreated))
                {
                    addUser.Status = Convert.ToInt32(UserStatus.JustCreated);
                }
                userUpdatedStatus = addUser.Status;
                addUser.LockedDate = null;
                _userRepository.SaveChanges();
            }
        }

        public bool IsPasswordExpired(FetchedUserViewModel userModel, int passwordExpiryDaysCount)
        {
            int daysElapsedAfterLastPwdChange = (DateTime.Today - userModel.LastPasswordChangeDate).Days;
            return daysElapsedAfterLastPwdChange > passwordExpiryDaysCount;
        }
        public void DeleteFromSession(int UserId)
        {
            //using (CheckLoginRepository checkLoginRepository = new CheckLoginRepository())
            //{
            //    var logins = checkLoginRepository.GetByUserId(UserId);
            //    if (logins != null)
            //    {
            //        checkLoginRepository.Delete(logins);
            //        checkLoginRepository.SaveChanges();
            //    }
            //}
        }

        public bool IsSessionExits(int userId, string sid)
        {
            //using (UserRepository _userRepository = new UserRepository())
            //{
            //    IEnumerable<UserViewModel> logins = _userRepository.Find(q => q.CheckLogins.FirstOrDefault().LoggedIn == true && q.Id == userId && q.CheckLogins.FirstOrDefault().SessionId == sid).Select(r =>
            //    {
            //        return new UserViewModel
            //        {
            //            LoggedIn = r.CheckLogins.FirstOrDefault().LoggedIn,
            //            SessionId = r.CheckLogins.FirstOrDefault().SessionId
            //        };
            //    });
            //    return logins.Any();
            //}
            return false;
        }


        public bool IsUserSessionExist(int userId)
        {
            //using (UserRepository _userRepository = new UserRepository())
            //{
            //    IEnumerable<UserViewModel> logins = _userRepository.Find(q => q.CheckLogins.FirstOrDefault().LoggedIn == true && q.Id == userId)
            //    .OrderByDescending(x => x.CheckLogins).Select(r =>
            //    {
            //        return new UserViewModel
            //        {
            //            LoggedIn = r.CheckLogins.FirstOrDefault().LoggedIn,
            //            SessionId = r.CheckLogins.FirstOrDefault().SessionId
            //        };
            //    });
            //    return logins.Any();
            //}
            return false;
        }
        

    }
}