using System;
using System.Collections.Generic;
using System.Text;

using System.Xml.Serialization;

namespace UniprixOperations.MessageManagement
{
    [XmlRoot("Messages")]
    public class Messages
    {
        [XmlElement("Message")]
        public List<Message> MessageList { get; set; }
    }
}
