using System.Xml.Serialization;

namespace UniprixOperations.XmlDataManager
{
    [XmlRoot("IdCounters")]
    public class IdCountersWrapper
    {
        [XmlElement("SectionIdCounter")]
        public int SectionIdCounter { get; set; }
        [XmlElement("ProductIdCounter")]
        public int ProductIdCounter { get; set; }
        [XmlElement("DailyTasksIdCounter")]
        public int DailyTasksIdCounter { get; set; }
        [XmlElement("RecurringTasksIdCounter")]
        public int RecurringTasksIdCounter { get; set; }
        [XmlElement("UserIdCounter")]
        public int UserIdCounter { get; set; }

        //No parameters for the serializer
        public IdCountersWrapper()
        {

        }
    }
}
