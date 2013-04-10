using System.Data;

namespace Qt.Service.Results
{
    /// <summary>
    /// Represents a property of an entity in the UI world
    /// </summary>
    public class Field
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public string Type { get; set; }
        public bool IsLinked { get; set; }
        public string Link { get; set; }
    }
}