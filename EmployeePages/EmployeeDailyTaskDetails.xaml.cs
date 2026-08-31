using System.Windows;
using System.Windows.Controls;
using UniprixOperations.PageData.EmployeePageData;
using UniprixOperations.TaskManagement;
using UniprixOperations.UserManagement;

namespace UniprixOperations.EmployeePages
{
    /// <summary>
    /// Logique d'interaction pour EmployeeDailyTaskDetails.xaml
    /// </summary>
    public partial class EmployeeDailyTaskDetails : Page, IPageDef
    {

        private DailyTask task;

        private bool isEditing = false;
        EmployeeDailyTasks parent;

        public EmployeeDailyTaskDetails(DailyTask task, EmployeeDailyTasks parent)
        {
            InitializeComponent();
            this.parent = parent;
            this.task = task;
            Refresh();
        }




        public void Refresh()
        {
            load();
        }


        //Task loading
        private void load()
        {
            EDTDName.Text = task.Name;
            EDTDDate.Text = task.TargetDate.ToString("yyyy-MM-dd");
            if (task.TargetTime != null)
            {
                EDTDHour.Text = task.TargetTime.ToString();
            }
            else
            {
                EDTDHour.Text = "N/A";
            }
            EDTDDescription.Text = task.Description;
        }


        //Complete Task
        private void CompleteTask(Object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Tâche complétée");
            task.Complete(UserStore.CurrentUser);
            parent.EDTTaskFrame.Content = null;
            parent.Refresh();
        }

    }
}
