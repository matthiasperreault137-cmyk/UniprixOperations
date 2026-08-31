using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using UniprixOperations.HelperClasses;
using UniprixOperations.EmployeePages;
using UniprixOperations.PageData.EmployeePageData;
using UniprixOperations.TaskManagement;
using UniprixOperations.XmlDataManager;

namespace UniprixOperations
{
    /// <summary>
    /// Logique d'interaction pour EmployeeRecurringTasks.xaml
    /// </summary>
    public partial class EmployeeRecurringTasks : Page, IPageDef
    { 
        private EmployeeRecurringTasksData data;
        private RecurringTask? SelectedTask;
        public EmployeeRecurringTasks(EmployeeRecurringTasksData data)
        {
            InitializeComponent();
            this.data = data;
            Refresh();
        }
        private void OpenMainMenu(Object sender, RoutedEventArgs e)
        {
            Navigator.OpenEmployeeMainMenu();
        }
        private void OpenTasks(Object sender, RoutedEventArgs e)
        {
            Navigator.OpenEmployeeTasks();
        }
        private void OpenExpiredProducts(Object sender, RoutedEventArgs e)
        {
            Navigator.OpenEmployeeAddExpiredProduct();
        }






        //Refresh

        public void Refresh()
        {
            LoadTasks();
        }


        //Load

        private void LoadTasks()
        {
            ERTTasks.Children.Clear();
            List<RecurringTask> tasks = DataManager.Instance.RecurringTasks.RecurringTaskList;
            RecurringTaskSearch.SortRecurringTasksNearest(tasks);
            foreach (RecurringTask task in tasks)
            {
                if (task.VisibilityLevel == 2)
                {
                    ERTTasks.Children.Add(CreateRecurringTask(task));
                }
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
                Style = (Style)FindResource("EmployeeRecurringTask"),
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
            if (SelectedTask == task)
            {
                ERTFrame.Content = null;
                SelectedTask = null;
            }
            else
            {
                SelectedTask = task;
                ERTFrame.Content = new EmployeeRecurringTaskDetails(task, this);
            }
        }
    }
}
