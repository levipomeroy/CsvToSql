using StateStatsExtractor.DAL;
using StateStatsExtractor.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace StateStatsExtractor.Logic
{
    public class Extractor
    {
        private const string CONNECTION_STRING = "Data Source=DESKTOP-7O0FVBH;Initial Catalog=StateStats;Integrated Security = True;";
        private DataAccess DAL;

        public Extractor()
        {
            DAL = new DataAccess(CONNECTION_STRING);
        }

        public List<FileData> GetDataFromFiles(string folderPath)
        {
            var data = new List<FileData>();

            var di = new DirectoryInfo(folderPath);
            var files = di.GetFiles("*.csv", SearchOption.AllDirectories);

            foreach (var file in files)
            {
                var content = File.ReadAllLines(file.ToString());
                data.Add(new FileData(
                    file.Name.Replace(" ", "").Replace(file.Extension, ""), 
                    content.First().Split(",").ToList(), 
                    content.Skip(1).ToList()
                ));
            }

            return data;
        }

        public bool CreateTables(List<FileData> fileData)
        {
            var success = false; 

            foreach(var file in fileData)
            {
                success = CreateTableStructure(file);
                if(success)
                {
                    success = AddRecords(file);
                }
            }

            return success;
        }

        bool CreateTableStructure(FileData fileInfo)
        {
            var success = false;

            var creationString = $"CREATE TABLE {fileInfo.FileName} (";
            creationString += $"[{fileInfo.FileName}Id] [int] IDENTITY(1,1) NOT NULL,"; 

            foreach(var column in fileInfo.Columns)
            {
                var comma = column.Name != fileInfo.Columns.Last().Name ? "," : string.Empty;
                creationString += $"{column.Name} {column.Type}{comma}";
            }
            creationString += ");";

            success = DAL.CreateTable(creationString);

            return success;
        }

        bool AddRecords(FileData fileInfo)
        {
            var success = false;

            var insertString = $"INSERT INTO {fileInfo.FileName} ({string.Join(",", fileInfo.Columns.Select(c => c.Name))}) VALUES ";
            foreach(var record in fileInfo.Data)
            {
                var comma = record != fileInfo.Data.Last() ? "," : string.Empty;
                insertString += $"({record.Replace("\"", "'")}){comma}";
            }

           success = DAL.AddRecords(insertString);

            return success;
        }

    }
}
