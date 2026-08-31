using System.Xml.Serialization;
using UniprixOperations.UserManagement;
using UniprixOperations.XmlDataManager;



namespace UniprixOperations.TaskManagement
{
    [XmlRoot("DailyTask")]
    public class DailyTask : Task
    {
        [XmlElement("Id")]
        public int Id { get; set; }
        [XmlElement("TargetDate")]
        public DateOnly TargetDate { get; set; }
        [XmlElement("TargetTime")]
        public TimeOnly? TargetTime { get; set; }

        public DailyTask()
        {

        }
        public DailyTask(string name, string description, int visibilityLevel, DateOnly targetDate, TimeOnly? targetTime) : base(name, description, visibilityLevel)
        {
            Id = DataManager.Instance.DailyTasksIdCounter++;
            TargetDate = targetDate;
            TargetTime = targetTime;
        }

        public override void Complete(User user)
        {
            CompletionDate = DateTime.Now;
            CompletionUser = user;

            DataManager.Instance.DailyTasks.DailyTaskList.Remove(this);
            DataManager.Instance.TaskHistory.CompletedTasks.Add(this);
        }
    }
}
