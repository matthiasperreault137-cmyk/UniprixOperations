using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using UniprixOperations.PageData.ManagerPageData;
using UniprixOperations.ProductManager;
using UniprixOperations.TaskManagement;
using UniprixOperations.UserManagement;
using UniprixOperations.XmlDataManager;

namespace UniprixOperations.ManagerPages
{
    /// <summary>
    /// Logique d'interaction pour ManagerDailyTaskDetails.xaml
    /// </summary>
    public partial class ManagerDailyTaskDetails : Page, IPageDef
    {
        private DailyTask task;

        private bool isEditing = false;

        private ManagerDailyTasks parent;
        public ManagerDailyTaskDetails(DailyTask task, ManagerDailyTasks parent)
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
            MDTDName.Text = task.Name;
            MDTDDate.Text = task.TargetDate.ToString("yyyy-MM-dd");
            if (task.TargetTime != null)
            {
                MDTDHour.Text = task.TargetTime.ToString();
            }
            else
            {
                MDTDHour.Text = "N/A";
            }
            MDTDDescription.Text = task.Description;
        }


        //Complete Task
        private void CompleteTask(Object sender, RoutedEventArgs e)
        {
            task.Complete(UserStore.CurrentUser);
            parent.MDTTaskFrame.Content = null;
            parent.Refresh();
        }


        //Delete Task
        private void DeleteTask(Object sender, RoutedEventArgs e)
        {
            DataManager.Instance.DailyTasks.DailyTaskList.Remove(task);
            parent.Refresh();
        }


        //Modify Task

        private void ModifyTask(Object sender, RoutedEventArgs e)
        {
            if (isEditing)
            {
                LockFields();
            }
            else
            {
                UnlockFields(sender, e);
            }
        }
        private void UnlockFields(Object sender, RoutedEventArgs e) 
        {
            MDTDName.IsReadOnly = false;
            MDTDDate.IsReadOnly = false;
            MDTDHour.IsReadOnly = false;
            MDTDDescription.IsReadOnly = false;
            isEditing = true;
            MDTDModify.Content = "Enregistrer";
        }


        private void LockFields()
        {
            MDTDName.IsReadOnly = true;
            MDTDDate.IsReadOnly = true;
            MDTDHour.IsReadOnly = true;
            MDTDDescription.IsReadOnly = true;



            if (MDTDName.Text != null && MDTDName.Text != "")
            {
                task.Name = MDTDName.Text;
            }
            else
            {
                MessageBox.Show("Le nom ne peut pas être vide");
            }


            if (DateOnly.TryParse(MDTDDate.Text, out DateOnly expiryDate))
            {
                task.TargetDate = expiryDate;
            }
            else
            {
                MessageBox.Show("Entrez une date d'expiration valide : yyyy-MM-dd");
            }

            
            if(MDTDHour.Text != null && TimeOnly.TryParse(MDTDHour.Text, out TimeOnly targetTime))
            {
                task.TargetTime = targetTime;
            }
            else
            {
                MessageBox.Show("Entrez une heure valide : HH:mm");
            }

            if (MDTDDescription.Text != null && MDTDDescription.Text != "")
            {
                task.Description = MDTDDescription.Text;
            }
            else
            {
                MessageBox.Show("La description ne peut pas être vide");
            }

            isEditing = false;
            MDTDModify.Content = "Modifier";
            load();
            parent.Refresh();
        }
    }
}
