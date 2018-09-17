using YoelProject.DAL.Repository;
using YoelProject.ViewModel.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using YoelProject.Models;

namespace YoelProject.Admin.Exigent
{
    public static class CoreManager
    {
        #region AppConfig

        /// <summary>
        /// Get list of all app config settings
        /// </summary>
        /// <returns></returns>
        public static List<AppConfigViewModel> GetAllAppConfigSettings()
        {
            using (AppConfigRepository _AppConfigrepository = new AppConfigRepository())
            {
                return _AppConfigrepository.GetAllCached().Select(e => new AppConfigViewModel
             {
                 Id = e.Id,
                 AppKey = e.AppKey,
                 AppVaue = e.AppVaue,
                 Caption = e.Caption,
             }).ToList();
            }
        }

        /// <summary>
        /// http://chameleoninformexigent.azurewebsites.net/
        /// </summary>
        /// <returns></returns>
        public static string GetApplicationURL()
        {
            using (AppConfigRepository _AppConfigrepository = new AppConfigRepository())
            {
                return _AppConfigrepository.GetAllCached().Select(e => new AppConfigViewModel
                 {
                     Id = e.Id,
                     AppKey = e.AppKey,
                     AppVaue = e.AppVaue,
                 }).FirstOrDefault(e => e.AppKey == "ApplicationURL").AppVaue;
            }
        }




        /// <summary>
        /// Update App config settrings
        /// </summary>
        /// <param name="AppConfigViewModelList"></param>
        /// <param name="userId"></param>
        public static void UpdateAppConfigSettings(List<AppConfigViewModel> AppConfigViewModelList, int userId)
        {
            using (AppConfigRepository _AppConfigrepository = new AppConfigRepository())
            {
                AppConfig appConfig = new AppConfig();

                foreach (var item in AppConfigViewModelList)
                {
                    appConfig = _AppConfigrepository.GetById(Convert.ToInt32(item.Id));
                    appConfig.AppVaue = item.AppVaue;
                    appConfig.ModifiedBy = userId;
                    _AppConfigrepository.SaveChanges();
                }
            }
        }

        //// get app values for supportDesk
        public static List<AppConfigViewModel> GetAppValuesForSupportDesk()
        {
            List<AppConfigViewModel> lstAppConfigViewModel = new List<AppConfigViewModel>();
            using (AppConfigRepository _AppConfigrepository = new AppConfigRepository())
            {
                lstAppConfigViewModel = _AppConfigrepository.GetAll().Select(e => new AppConfigViewModel
                {
                    Id = e.Id,
                    AppKey = e.AppKey,
                    AppVaue = e.AppVaue,
                    Section = e.Section
                }).Where(e => e.Section == "SupportDesk").ToList();
                return lstAppConfigViewModel;
            }
        }
        #endregion
    }
}
