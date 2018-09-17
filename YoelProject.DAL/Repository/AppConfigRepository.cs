
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web;
using YoelProject.DAL.AbstractRepository;
using YoelProject.Models;

namespace YoelProject.DAL.Repository
{
    public class AppConfigRepository : AbstractRepository<AppConfig>, ICacheble<AppConfig>
    {
        public IEnumerable<AppConfig> GetAllCached()
        {
            Debug.Print("CachedAppConfigRepository:GetAllAppConfig");
            var result = HttpRuntime.Cache[typeof(AppConfig).FullName] as IEnumerable<AppConfig>;
            if (result == null)
            {
                result = base.GetAll();
                HttpRuntime.Cache.Insert(typeof(AppConfig).FullName, result, null, DateTime.Now.AddSeconds(60), TimeSpan.Zero);
            }
            return result;
        }
    }
}
