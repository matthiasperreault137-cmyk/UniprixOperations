using System.Xml.Serialization;

namespace UniprixOperations.TaskManagement
{
    [XmlRoot("RecurringTasks")]
    public class RecurringTasks
    {
        [XmlElement("RecurringTask")]
        public List<RecurringTask> RecurringTaskList { get; set; }
        public RecurringTasks()
        {

        }
    }
}
