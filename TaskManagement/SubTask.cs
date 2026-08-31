using UniprixOperations.HelperClasses;
using System.Xml.Serialization;
using UniprixOperations.UserManagement;
using UniprixOperations.XmlDataManager;
using System.Windows;

namespace UniprixOperations.TaskManagement
{
    [XmlRoot("SubTask")]
    public class SubTask : Task
    {
        [XmlElement("ParentTask")]
        public int ParentTaskId { get; set; }
        [XmlElement("Recurrence")]
        public int Recurrence { get; set; }

        public SubTask(string name, string description, int visibilityLevel, int parentTaskId, int recurrence) : base(name, description, visibilityLevel)
        {
            ParentTaskId = parentTaskId;
            Recurrence = recurrence;
        }
        public SubTask() { }


        public override void Complete(User user)
        {
            CompletionDate = DateTime.Now;
            CompletionUser = user;

            RecurringTask parent = RecurringTaskSearch.SearchId(ParentTaskId);
            if(parent == null)
            {
                MessageBox.Show("Parent task not found for SubTask: " + Name  +"ID :" +  ParentTaskId + "\n appelez un responsable et en attendant éviter la complétion de sous-tâches");
                return;
            }


            parent.SubTasks.Remove(this);
            parent.SubTasks.Add(this.Clone());

            DateTime? oldest = null;

            foreach(SubTask subtask in parent.SubTasks)
            {
                if (oldest == null || subtask.CreationDate < oldest)
                {
                    oldest = subtask.CreationDate;
                }
            }
            parent.CreationDate = (DateTime) oldest;


            DataManager.Instance.TaskHistory.CompletedTasks.Add(this);

        }
        public SubTask Clone()
        {
            return new SubTask(Name, Description, VisibilityLevel, ParentTaskId, Recurrence);
        }
    }
}
