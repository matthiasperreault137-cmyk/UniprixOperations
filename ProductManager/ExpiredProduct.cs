using System.Xml.Serialization;
using UniprixOperations.XmlDataManager;

namespace UniprixOperations.ProductManager
{
    [XmlRoot("ExpiredProduct")]
    public class ExpiredProduct
    {
        [XmlElement("Id")]
        public int Id { get; set; }
        [XmlElement("Name")]
        public string Name { get; set; }
        [XmlElement("UPCCode")]
        public string UPCCode { get; set; }
        [XmlElement("ExpiryDate")]
        public DateOnly ExpiryDate { get; set; }
        [XmlElement("ShelfStatus")]
        public bool ShelfStatus { get; set; } //True = en tablette, False = pas en tablettes
        [XmlElement("Quantity")]
        public int Quantity { get; set; }
        [XmlElement("SectionId")]
        public int SectionId { get; set; }
        public ExpiredProduct()
        {

        }
        public ExpiredProduct(string name, string uPCCode, DateOnly expiryDate, bool shelfStatus, int quantity, int sectionId)
        {
            Id = DataManager.Instance.ProductIdCounter++;
            Name = name;
            UPCCode = uPCCode;
            ExpiryDate = expiryDate;
            ShelfStatus = shelfStatus;
            Quantity = quantity;
            SectionId = sectionId;
        }
    }
}
