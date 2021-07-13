using System;
using System.Collections.Generic;
using System.Linq;

namespace StateStatsExtractor.Models
{
    public class FileData
    {
        public FileData() { }
        public FileData(string fileName, List<string> columns, List<string> data)
        {
            FileName = fileName;
            Data = data;
            Columns = ExtractColumns(columns, data.First().Split(",").ToList());
        }

        public List<Column> Columns { get; set; }
        public List<string> Data { get; set; }
        public string FileName { get; set; }

        private List<Column> ExtractColumns(List<string> columns, List<string> firstRowData)
        {
            var typedColumns = new List<Column>();
            for (int i=0; i < columns.Count; i++)
            {
                typedColumns.Add(new Column($"[{columns[i].Replace(" ", "")}]", ParseTypeFromString(firstRowData[i])));
            }
            return typedColumns;
        }

        private string ParseTypeFromString(string str)
        {
            if (bool.TryParse(str, out bool boolValue))
                return "bit";
            else if (Int32.TryParse(str, out Int32 intValue))
                return "int";
            else if (Int64.TryParse(str, out Int64 bigintValue))
                return "bigint";
            else if (double.TryParse(str, out double doubleValue))
                return "float";
            else if (DateTime.TryParse(str, out DateTime dateValue))
                return "datetime";
            else return "varchar(max)";
        }
    }
}
