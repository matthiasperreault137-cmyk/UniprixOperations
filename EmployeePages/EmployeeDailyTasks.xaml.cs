using System.Windows;
using System.Windows.Controls;
using UniprixOperations.HelperClasses;
using UniprixOperations.EmployeePages;
using UniprixOperations.PageData.EmployeePageData;
using UniprixOperations.TaskManagement;
using UniprixOperations.XmlDataManager;

namespace UniprixOperations
{
    /// <summary>
    /// Logique d'interaction pour EmployeeDailyTasks.xaml
    /// </summary>
    public partial class EmployeeDailyTasks : Page, IPageDef
    {
        private EmployeeDailyTasksData data;

        private DateOnly selectedDate = DateOnly.FromDateTime(DateTime.Now);

        public EmployeeDailyTasks(EmployeeDailyTasksData data)
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
            EDTDatePicker.SelectedDate = selectedDate.ToDateTime(new TimeOnly(0, 0));
            LoadTasks();
            EDTTaskFrame.Content = null;
        }


        //Task details navigation
        private void DateChange(Object sender, SelectionChangedEventArgs e)
        {
            if (EDTDatePicker.SelectedDate.HasValue)
            {
                selectedDate = DateOnly.FromDateTime(EDTDatePicker.SelectedDate.Value);
                Refresh();
            }
        }
        private void LoadTasks()
        {
            EDTTaskList.Children.Clear();
            foreach (DailyTask task in DataManager.Instance.DailyTasks.DailyTaskList)
            {
                if (selectedDate <= DateOnly.FromDateTime(DateTime.Today))
                {
                    if (task.TargetDate <= selectedDate && task.VisibilityLevel == 2)
                    {
                        Button b = AddDailyTaskButton(task);
                        EDTTaskList.Children.Add(b);
                    }
                }
                else
                {
                    if (task.TargetDate == selectedDate && task.VisibilityLevel == 2)
                    {
                        Button b = AddDailyTaskButton(task);
                        EDTTaskList.Children.Add(b);
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
            b.Style = (Style)FindResource("EmployeeDailyTask");
            return b;
        }


        private void SelectDailyTask(DailyTask task)
        {
            EDTTaskFrame.Content = new EmployeeDailyTaskDetails(task, this);
        }

    }
}
