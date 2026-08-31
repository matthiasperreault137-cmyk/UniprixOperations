using System.Xml.Serialization;

namespace UniprixOperations.ProductManager
{
    [XmlRoot("Sections")]
    public class Sections
    {
        [XmlElement("Section")]
        public List<Section> SectionList { get; set; }
        public Sections()
        {

        }
    }
}
