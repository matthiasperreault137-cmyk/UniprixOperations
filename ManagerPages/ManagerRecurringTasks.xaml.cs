using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using UniprixOperations.HelperClasses;
using UniprixOperations.PageData.ManagerPageData;
using UniprixOperations.TaskManagement;
using UniprixOperations.XmlDataManager;

namespace UniprixOperations.ManagerPages
{
    /// <summary>
    /// Logique d'interaction pour ManagerRecurringTasks.xaml
    /// </summary>
    public partial class ManagerRecurringTasks : Page, IPageDef
    {
        private ManagerRecurringTasksData data;
        private RecurringTask SelectedTask;
        public ManagerRecurringTasks(ManagerRecurringTasksData data)
        {
            InitializeComponent();
            this.data = data;
            Refresh();
        }



        //Side Bar Navigation Functions
        public void OpenMainMenu(Object sender, RoutedEventArgs e)
        {
            Navigator.OpenManagerMainMenu();
        }
        public void OpenTasks(Object sender, RoutedEventArgs e)
        {
            Navigator.OpenManagerTasks();
        }
        public void OpenExpiredProducts(Object sender, RoutedEventArgs e)
        {
            Navigator.OpenManagerExpireMenu();
        }
        public void OpenTaskHistory(Object sender, RoutedEventArgs e)
        {
            Navigator.OpenTaskHistory();
        }
        public void OpenUserMenu(Object sender, RoutedEventArgs e)
        {
            Navigator.OpenUserMenu();
        }


        //Refresh

        public void Refresh()
        {
            LoadTasks();
            MRTFrame.Content = null;
        }


        //Load

        private void LoadTasks()
        {
            MRTTasks.Children.Clear();
            List<RecurringTask> tasks = DataManager.Instance.RecurringTasks.RecurringTaskList;
            RecurringTaskSearch.SortRecurringTasksNearest(tasks);
            foreach (RecurringTask task in tasks)
            {
                MRTTasks.Children.Add(CreateRecurringTask(task));
            }

        }

        private Button CreateRecurringTask(RecurringTask task)
        {
            float a = (DateTime.Today - task.CreationDate).Days / (float)task.Recurrence;
            a = Math.Clamp(a, 0f, 1f);

            byte red = (byte)(Math.Clamp(a * 2, 0f, 1f) * 255);
            byte green = (byte)(Math.Clamp(1 - (a * 2 - 1), 0f, 1f) * 255);

            var button = new Button
            {
                Style = (Style)FindResource("ManagerRecurringTask"),
                Content = task.Name,
                // Tag carries the badge background color
                Tag = new SolidColorBrush(Color.FromRgb(red, green, 0)),
                // ToolTip carries the day count text (re-used as the label binding)
                ToolTip = (DateTime.Today - task.CreationDate).Days.ToString()
            };
            button.Click += (s, e) => SelectTask(task);

            return button;
        }

        private void SelectTask(RecurringTask task)
        {
            if(SelectedTask == task)
            {
                MRTFrame.Content = null;
                SelectedTask = null;
            }
            else
            {
                SelectedTask = task;
                MRTFrame.Content = new ManagerRecurringTaskDetails(this,task);
            }
        }
    }
}