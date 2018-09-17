using YoelProject.DAL.DataModels;
using System.Data.SqlClient;

namespace YoelProject.DAL.SqlConnectionHelper
{
    public class SqlConnectionHelper
    {
        public static SqlConnection GetExigentConnection()
        {
            ReportApplicationEntities ReportApplicationEntities = new ReportApplicationEntities();
            return new SqlConnection((ReportApplicationEntities.Database.Connection).ConnectionString);                 
        }

        public static SqlConnection GetClientConnection()
        {
            //ClientEntities ClientEntitie = new ClientEntities();
            //return new SqlConnection((ClientEntitie.Database.Connection).ConnectionString);
            return new SqlConnection();
        }
    }   
}
