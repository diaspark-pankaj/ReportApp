using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoelProject.Common.Helper
{  
    public static class DataTableHelper
    {
        public static IEnumerable<dynamic> AsDynamicEnumerable(this DataTable table)
        {
            // Validate argument here..

            return table.AsEnumerable().Select(row => new DynamicRow(row));
        }

        public static List<string> GetDataTableColumns(DataColumnCollection columnCollection)
        {
            List<string> columnList = new List<string>();            
            for (int i = 0; i < columnCollection.Count; i++)
            {
                columnList.Add(columnCollection[i].ColumnName.ToString());
            }
            return columnList;
        }

        private sealed class DynamicRow : DynamicObject
        {
            private readonly DataRow _row;

            internal DynamicRow(DataRow row) { _row = row; }

            // Interprets a member-access as an indexer-access on the 
            // contained DataRow.
            public override bool TryGetMember(GetMemberBinder binder, out object result)
            {
                var retVal = _row.Table.Columns.Contains(binder.Name);
                result = retVal ? _row[binder.Name] : null;
                return retVal;
            }
        }
    }
}
