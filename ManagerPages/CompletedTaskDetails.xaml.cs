using System.Windows.Controls;
using System.Windows;
using UniprixOperations.TaskManagement;
using UniprixOperations.XmlDataManager;
using UniprixOperations.UserManagement;
using Task = UniprixOperations.TaskManagement.Task;
using System.ComponentModel;

namespace UniprixOperations.ManagerPages
{
    /// <summary>
    /// Logique d'interaction pour CompletedTaskDetails.xaml
    /// </summary>
    public partial class CompletedTaskDetails : Page, IPageDef
    {
        private bool isEditing = false;
        private Task task;
        private TaskHistory parent;
        public CompletedTaskDetails(Task task, TaskHistory parent)
        {
            InitializeComponent();
            this.task = task;
            this.parent = parent;
            Refresh();
        }





        public void Refresh()
        {
            load();
        }


        //Task loading
        private void load()
        {
            CTDName.Text = task.Name;
            CTDUser.Text = ((User) task.CompletionUser).Name;
            if(task.CompletionDate != null)
            {
                CTDDate.Text = DateOnly.FromDateTime((DateTime)task.CompletionDate).ToString("yyyy-MM-dd");
                CTDHour.Text = TimeOnly.FromDateTime((DateTime)task.CompletionDate).ToString("HH:mm");
            }
            CTDDescription.Text = task.Description;
        }


        //Task deletion

        private void DeleteTask(Object sender, RoutedEventArgs e)
        {
            DataManager.Instance.TaskHistory.CompletedTasks.Remove(task);
            parent.Refresh();
            MessageBox.Show("Tâche supprimée de l'historique");
        }


        //Task Modify

        private void ModifyTask(object sender, RoutedEventArgs e)
        {
            if (isEditing)
            {
                isEditing = false;
                Save();
                CTDModify.Content = "Modifier";
            }
            else
            {
                isEditing = true;
                Edit();
                CTDModify.Content = "Enregistrer";
            }
        }

        private void Save()
        {
            CTDModDate.Visibility = Visibility.Collapsed;
            CTDDisDate.Visibility = Visibility.Visible;

            if(CTDDatePicker.SelectedDate != null)
            {
                task.CompletionDate = CTDDatePicker.SelectedDate;

            }
            Refresh();
            parent.Refresh();
        }
        private void Edit()
        {
            CTDDisDate.Visibility = Visibility.Collapsed;
            CTDDatePicker.SelectedDate = null;
            CTDModDate.Visibility = Visibility.Visible;
        }
    }
}
