using StateStatsExtractor.Logic;

namespace StateStatsExtractor
{
    class Program
    {
        static void Main(string[] args)
        {
            var extractor = new Extractor();
            var fileData = extractor.GetDataFromFiles("../../../DataSource/");
            extractor.CreateTables(fileData);
        }
    }
}
