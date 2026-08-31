using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace UniprixOperations.UserManagement
{
    [XmlRoot("Manager")]
    public class Manager : Supervisor
    {

        public Manager(string name, string password): base(name, password) { }
        public Manager() { }
    }
}
