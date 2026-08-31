using System.Xml.Serialization;

namespace UniprixOperations.UserManagement
{

    //Mostly unused but its aight to have it for future use
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
