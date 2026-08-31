using System.Xml.Serialization;

namespace UniprixOperations.TaskManagement
{
    [XmlRoot("DailyTasks")]
    public class DailyTasks
    {
        [XmlElement("DailyTask")]
        public List<DailyTask> DailyTaskList { get; set; }

        public DailyTasks()
        {

        }
    }
}
