using System.Xml.Serialization;

namespace UniprixOperations.UserManagement
{
    [XmlRoot("Users")]
    public class UserWrapper
    {
        [XmlElement("User")]
        public List<User> UserList { get; set; }
        public UserWrapper()
        {

        }
    }
}
