using System.Xml.Serialization;

namespace UniprixOperations.UserManagement
{
    [XmlInclude(typeof(Manager))]
    public class Supervisor : User
    {
        [XmlElement("PassWord")]
        public string PassWord { get; set; }

        public Supervisor()
        {

        }
        public Supervisor(string name, string passWord) : base(name)
        {
            PassWord = passWord;
        }
    }
}
