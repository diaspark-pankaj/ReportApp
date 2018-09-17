using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoelProject.DAL.Repository;
using YoelProject.ViewModel.Reports;

namespace YoelProject.BAL
{
    public class ReportManager
    {

        public ReportViewModel GetReportDetailById(int ReportId)
        {
            ReportViewModel reportViewModel = new ReportViewModel();
            using (var reportRepository = new ReportRepository())
            {
                var reportDetail = reportRepository.Find(x => x.ReportId == ReportId).FirstOrDefault();
                if (reportDetail != null)
                {
                    reportViewModel.ReportId = reportDetail.ReportId;
                    reportViewModel.Name = reportDetail.Name;
                    reportViewModel.Query = reportDetail.Query;
                    reportViewModel.DBConnection = reportDetail.DBConnection;
                    reportViewModel.ParameterList = JsonConvert.DeserializeObject<List<ParameterViewModel>>(reportDetail.Parameter);
                }
            }
            return reportViewModel;
        }

        public DataTable GetReportGridData(ReportViewModel reportViewModel, List<ParameterViewModel> parameterViewModelList)
        {
            DataSet dataSet = new DataSet();
            SqlParameter sqlParameter = new SqlParameter();
            List<SqlParameter> sqlParameterList = new List<SqlParameter>();
            //SqlParameter[] parameterList = { new SqlParameter("@Id",DBNull.Value),
            //                                   new SqlParameter("@name",DBNull.Value),
            //                                   new SqlParameter("@address",DBNull.Value)};

            if (parameterViewModelList.Any())
            {
                foreach (var item in parameterViewModelList)
                {
                    sqlParameter = new SqlParameter();
                    sqlParameter.ParameterName = string.IsNullOrEmpty(item.ColumnName) ? string.Empty : item.ColumnName;
                    sqlParameter.Value = string.IsNullOrEmpty(item.ColumnName) ? string.Empty : item.ColumnName;
                    sqlParameterList.Add(sqlParameter);
                }
            }



            dataSet = CommonRepository.ExecuteQueryForGrid(reportViewModel.Query, reportViewModel.DBConnection, System.Data.CommandType.Text, sqlParameterList.ToArray());
            //studentsList = table.AsEnumerable().Select(row => new Entities.Student { Id = row.Field<int>("Id"), Name = row.Field<string>("Name"), Address = row.Field<string>("Address") }).ToList();
            //return studentsList;
            return dataSet.Tables[0];
        }
    }
}
