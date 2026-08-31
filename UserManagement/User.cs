using System.Xml.Serialization;

using UniprixOperations.XmlDataManager;

namespace UniprixOperations.UserManagement
{
    [XmlInclude(typeof(Employee))]
    [XmlInclude(typeof(Supervisor))]
    public abstract class User
    {
        [XmlElement("Id")]
        public int Id { get; set; }
        [XmlElement("Name")]
        public string Name { get; set; }

        public User()
        {

        }
        public User(string name)
        {
            Id = DataManager.Instance.UserIdCounter++;
            Name = name;
        }
    }
}
