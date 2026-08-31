using System.Windows;
using System.Windows.Controls;
using UniprixOperations.HelperClasses;
using UniprixOperations.PageData.ManagerPageData;
using UniprixOperations.XmlDataManager;
using UniprixOperations.TaskManagement;

namespace UniprixOperations.ManagerPages
{
    /// <summary>
    /// Logique d'interaction pour ManagerDailyTasks.xaml
    /// </summary>
    public partial class ManagerDailyTasks : Page, IPageDef
    {
        private ManagerDailyTasksData data;
        
        private DateOnly selectedDate = DateOnly.FromDateTime(DateTime.Now);
        public ManagerDailyTasks(ManagerDailyTasksData data)
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

        public void Refresh()
        {
            MDTDatePicker.SelectedDate = selectedDate.ToDateTime(new TimeOnly(0, 0));
            LoadTasks();
            MDTTaskFrame.Content = null;
        }


        //Task details navigation
        private void DateChange(Object sender, SelectionChangedEventArgs e)
        {
            if (MDTDatePicker.SelectedDate.HasValue)
            {
                selectedDate = DateOnly.FromDateTime(MDTDatePicker.SelectedDate.Value);
                Refresh();
            }
        }
        private void LoadTasks()
        {
            MDTTaskList.Children.Clear();
            foreach (DailyTask task in DataManager.Instance.DailyTasks.DailyTaskList)
            {
                if (selectedDate <= DateOnly.FromDateTime(DateTime.Today))
                {
                    if (task.TargetDate <= selectedDate)
                    {
                        Button b = AddDailyTaskButton(task);
                        MDTTaskList.Children.Add(b);
                    }
                }
                else
                {
                    if (task.TargetDate == selectedDate)
                    {
                        Button b = AddDailyTaskButton(task);
                        MDTTaskList.Children.Add(b);
                    }
                }

            }
        }

        private Button AddDailyTaskButton(DailyTask task)
        {
            Button b = new Button
            {
                Content = task.Name,
                Height = 80
            };
            b.Click += (s, e) => SelectDailyTask(task);
            b.Style = (Style)FindResource("ManagerDailyTask");
            return b;
        }


        private void SelectDailyTask(DailyTask task)
        {
            MDTTaskFrame.Content = new ManagerDailyTaskDetails(task, this);
        }


    }
}
