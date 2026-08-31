using System.Xml.Serialization;

namespace UniprixOperations.UserManagement
{
    [XmlRoot("Employee")]
    public class Employee : User
    {
        public Employee()
        {

        }
        public Employee(string name) : base(name)
        {

        }
    }
}
