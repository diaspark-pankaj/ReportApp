using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoelProject.ViewModel.Reports
{
    public class ReportViewModel
    {
        public ReportViewModel()
        {
            //GridRowsList = new IEnumerable<dynamic>();
            GridColumnList = new List<string>();
        }
        public int ReportId { get; set; }
        public string Name { get; set; }
        public string Query { get; set; }
        public string DBConnection { get; set; }
        public List<ParameterViewModel> ParameterList { get; set; }

        public IEnumerable<dynamic> GridRowsList { get; set; }
        public List<string> GridColumnList { get; set; }
    }

    public class ParameterViewModel
    {
        public string ColumnName { get; set; }
        public string ColumnLable { get; set; }
        public string DataType { get; set; }
        public string ValidationMsg { get; set; }
        public bool IsRequired { get; set; }
        public string ValidationRegex { get; set; }
        public string Value { get; set; }

    }
}
