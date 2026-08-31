using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace UniprixOperations.TaskManagement
{
    [XmlRoot("TaskHistory")]
    public class TaskHistory
    {
        [XmlElement("CompletedTasks")]
        public List<Task> CompletedTasks { get; set; } = new List<Task>();

        public TaskHistory() { }    
    }
}
