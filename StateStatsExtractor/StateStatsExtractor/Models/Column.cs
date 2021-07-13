namespace StateStatsExtractor.Models
{
    public class Column
    {
        public Column() { }
        public Column(string name, string type) {
            Name = name;
            Type = type;
        }

        public string Name { get; set; }
        public string Type { get; set; }
    }
}
