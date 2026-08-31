using System.Xml.Serialization;
using UniprixOperations.XmlDataManager;

namespace UniprixOperations.ProductManager
{
    [XmlRoot("Section")]
    public class Section
    {
        [XmlElement("Id")]
        public int Id { get; set; }
        [XmlElement("Name")]
        public string Name { get; set; }
        [XmlElement("Description")]
        public string Description { get; set; }
        public Section() { }
        public Section(string name, string description)
        {
            Id = DataManager.Instance.SectionIdCounter++;
            Name = name;
            Description = description;
        }
    }
}
