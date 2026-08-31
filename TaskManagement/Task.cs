using System.Xml.Serialization;
using UniprixOperations.UserManagement;


namespace UniprixOperations.TaskManagement
{
    [XmlInclude(typeof(DailyTask))]
    [XmlInclude(typeof(RecurringTask))]
    [XmlInclude(typeof(SubTask))]
    public abstract class Task
    {
        [XmlElement("Name")]
        public string Name { get; set; }
        [XmlElement("Description")]
        public string Description { get; set; }
        [XmlElement("CreationDate")]
        public DateTime CreationDate { get; set; }
        [XmlElement("CompletionDate")]
        public DateTime? CompletionDate { get; set; }
        [XmlElement("CompletionUserId")]
        public User? CompletionUser { get; set; }
        [XmlElement("VisibilityLevel")]
        public int VisibilityLevel { get; set; }        // 0 = Undefined  1 = Manager 2 = Employee/All

        public Task() { }
        public Task(string name, string description, int visibilityLevel)
        {
            Name = name;
            Description = description;
            VisibilityLevel = visibilityLevel;
            CreationDate = DateTime.Today;
            CompletionDate = null;
            CompletionUser = null;
        }

        public abstract void Complete(User user);
    }
}
