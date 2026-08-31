using System.ComponentModel;
using System.Windows;
using System.Xml.Serialization;
using UniprixOperations.UserManagement;
using UniprixOperations.XmlDataManager;


namespace UniprixOperations.TaskManagement
{

    [XmlRoot("RecurringTask")]
    public class RecurringTask : Task
    {
        [XmlElement("Id")]
        public int Id { get; set; }
        [XmlElement("Recurrence")]
        private int recurrence;
        public int Recurrence
        {
            get => recurrence;
            set
            {
                recurrence = value;

                if (SubTasks != null && SubTasks.Count > 0)
                {
                    foreach (var subtask in SubTasks)
                    {
                        subtask.Recurrence = value;
                    }
                }
            }
        }
        [XmlElement("SubTask")]
        public List<SubTask> SubTasks { get; set; } = new List<SubTask>();

        public RecurringTask()
        {

        }
        public RecurringTask(string name, string description, int visibilityLevel, int recurrence) : base(name, description, visibilityLevel)
        {
            Id = DataManager.Instance.RecurringTasksIdCounter++;
            Recurrence = recurrence;
        }
        public override void Complete(User user)
        {
            CompletionDate = DateTime.Now;
            CompletionUser = user;

            DataManager.Instance.RecurringTasks.RecurringTaskList.Remove(this);
            DataManager.Instance.RecurringTasks.RecurringTaskList.Add(this.Clone());
            DataManager.Instance.TaskHistory.CompletedTasks.Add(this);
        }
        public RecurringTask Clone()
        {
            RecurringTask clone = new RecurringTask(Name, Description, VisibilityLevel, Recurrence);
            foreach (SubTask subTask in SubTasks)
            {
                SubTask subclone = subTask.Clone();
                clone.SubTasks.Add(subclone);
                subclone.ParentTaskId = clone.Id;
            }
            return clone;
        }


    }
}
