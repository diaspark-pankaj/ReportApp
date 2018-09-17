using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YoelProject.BAL;
using YoelProject.Common.Helper;
using YoelProject.ViewModel.Reports;

namespace YoelProject.Controllers
{
    public class ReportController : Controller
    {
        // GET: Report
        public ActionResult Index()
        {
            ReportManager reportManager = new ReportManager();
            return View(reportManager.GetReportDetailById(3));
        }
        [HttpPost]
        public ActionResult Index(ReportViewModel reportViewModel)
        {
            ReportManager reportManager = new ReportManager();
            var reportDetail = reportManager.GetReportDetailById(reportViewModel.ReportId);
            var table = reportManager.GetReportGridData(reportDetail, reportViewModel.ParameterList);
            IEnumerable<dynamic> dynamicGridData = table.AsDynamicEnumerable();
            List<string> dynamicGridColumns = DataTableHelper.GetDataTableColumns(table.Columns);
            reportDetail.GridColumnList = dynamicGridColumns;
            reportDetail.GridRowsList = dynamicGridData;
            return View(reportDetail);
        }
    }
}