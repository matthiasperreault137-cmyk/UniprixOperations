using System;
using System.Collections.Generic;
using System.Text;
using UniprixOperations.UserManagement;

using System.Xml.Serialization;

namespace UniprixOperations.MessageManagement
{
    [XmlRoot("Message")]
    public class Message
    {
        [XmlElement("Content")]
        public string Content { get; set; }
        [XmlElement("Timestamp")]
        public DateTime Timestamp { get; set; }
        [XmlElement("Sender")]
        public User Sender { get; set; }

        [XmlElement("IsSecret")]
        public bool? IsSecret { get; set; } = false;  //Make sure old data can be read without issues

        public Message() { }

        public Message(string content, User sender, bool? isSecret)
        {
            Content = content;
            Timestamp = DateTime.Now;
            Sender = sender;
            if (isSecret == null)
            {
                IsSecret = false;
            }
            else
            {
                IsSecret = isSecret;
            }
        }
    }
}
