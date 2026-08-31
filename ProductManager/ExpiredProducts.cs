using System.Xml.Serialization;

namespace UniprixOperations.ProductManager
{
    [XmlRoot("ExpiredProducts")]
    public class ExpiredProducts
    {
        [XmlElement("ExpiredProduct")]
        public List<ExpiredProduct> ExpiredProductList { get; set; }

        public ExpiredProducts()
        {
        }
    }
}
