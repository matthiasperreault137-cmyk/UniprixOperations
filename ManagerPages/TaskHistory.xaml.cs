using System.ComponentModel.Design;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using UniprixOperations.HelperClasses;
using UniprixOperations.PageData.ManagerPageData;
using System.Windows.Shapes;
using UniprixOperations.UserManagement;
using UniprixOperations.XmlDataManager;
using Task = UniprixOperations.TaskManagement.Task;

namespace UniprixOperations.ManagerPages
{
    /// <summary>
    /// Logique d'interaction pour TaskHistory.xaml
    /// </summary>
    public partial class TaskHistory : Page, IPageDef
    {
        private List<Task> tasks;
        private Task SelectedTask { get; set; }
        private string SearchMethod { get; set; }
        private string SearchText { get; set; }



        private TaskHistoryData data;
        public TaskHistory(TaskHistoryData data)
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
            LoadTasks();
            THTaskDetails.Content = null;
        }



        //Loading

        private Button CreateTask(Task task)
        {
            var completionDate = DateOnly.FromDateTime((DateTime)task.CompletionDate).ToString("yyyy/MM/dd");
            var completionTime = TimeOnly.FromDateTime((DateTime)task.CompletionDate).ToString("HH:mm");

            var grid = new Grid();
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition());

            var textBlock1 = new TextBlock
            {
                Text = $"Fait le :\n{completionDate}\nà : {completionTime}",
                FontSize = 15,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center
            };
            Grid.SetColumn(textBlock1, 0);

            var textBlock2 = new TextBlock
            {
                Text = $"{task.Name}\nFait par : {((User)task.CompletionUser).Name}",
                FontSize = 15,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center
            };
            Grid.SetColumn(textBlock2, 1);

            var detailsButton = new Button
            {
                Content = "Voir les détails",
                Style = (Style)FindResource("TaskHistoryButton")
            };
            Grid.SetColumn(detailsButton, 2);

            grid.Children.Add(textBlock1);
            grid.Children.Add(textBlock2);
            grid.Children.Add(detailsButton);

            var item = new Button();
            item.Style = (Style)FindResource("TaskHistoryItem");
            item.Content = grid;
            item.Click += (s, e) => SelectTask(task);

            return item;
        }
        private void LoadTasks()
        {
            THTasks.Children.Clear(); 
            tasks = Search();
            foreach(Task t in tasks)
            {
                THTasks.Children.Add(CreateTask(t));
            }

        }

        private void SelectTask(Task task)
        {
            if (SelectedTask == task)
            {
                THTaskDetails.Content = null;
                SelectedTask = null;
            }
            else
            {
                SelectedTask = task;
                THTaskDetails.Content = new CompletedTaskDetails(task, this);
            }
        }


        //Search Handling

        private void SearchFilterChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

            ComboBox comboBox = (ComboBox)sender;

            SearchMethod = ((ComboBoxItem)comboBox.SelectedItem).Content.ToString();
            if(SearchMethod == "Date")
            {
                THSearchText.Visibility = Visibility.Collapsed;
                THDatePickerPanel.Visibility = Visibility.Visible;
                SearchText = null;
                THSearchText.Text = "";
            }
            else
            {
                THSearchText.Visibility = Visibility.Visible;
                THDatePickerPanel.Visibility = Visibility.Collapsed;
                THStartDate.SelectedDate = null;
                THEndDate.SelectedDate = null;
            }

            if (this.IsLoaded)
            {
                Refresh();
            }
        }
        private void DateChange(object sender, SelectionChangedEventArgs e)
        {
            Refresh();
        }

        private void SearchTextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            TextBox text = (TextBox)sender;


            SearchText = text.Text;

            if (this.IsLoaded)
            {
                Refresh();
            }
        }


        private List<Task> Search()
        {
            List<Task> tasks = new List<Task>();
            if (SearchMethod == "Nom de tâche")
            {
                tasks = TaskSearch.GetUnsortedTasksName(SearchText);

                TaskSearch.SortTasksNearest(tasks);

                tasks.Reverse();
            }
            else if (SearchMethod == "Nom d'utilisateur")
            {
                tasks = TaskSearch.GetUnsortedTasksUserName(SearchText);

                TaskSearch.SortTasksNearest(tasks);
                tasks.Reverse();
            }
            else if (SearchMethod == "Date de complétion(plus lointaine)")
            {
                tasks = TaskSearch.GetUnsortedTasksName(SearchText);
                TaskSearch.SortTasksNearest(tasks);
            }
            else if(SearchMethod == "Date")
            {
                DateOnly? startDate = THStartDate.SelectedDate.HasValue
                    ? DateOnly.FromDateTime(THStartDate.SelectedDate.Value)  
                    : null;

                DateOnly? endDate = THEndDate.SelectedDate.HasValue
                    ? DateOnly.FromDateTime(THEndDate.SelectedDate.Value)
                    : null;

                tasks = TaskSearch.GetUnsortedTasksDateRange(startDate, endDate);

                TaskSearch.SortTasksNearest(tasks);
                
                tasks.Reverse();
            }
            return tasks;
        }


        //Deletion

        private void ClearAllTasks(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Êtes vous sur de vouloir supprimer la liste complète de l'historique de tâches?\nAre you sure you want to delete the entiry Task History?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if(result == MessageBoxResult.Yes)
            {
                foreach (Task task in tasks)
                {
                    DataManager.Instance.TaskHistory.CompletedTasks.Remove(task);
                }
                Refresh();
            }
                
        }


        //Printing


        private void PrintTasks(object sender, RoutedEventArgs e)
        {
            Printer.PrintTasks(tasks);
        }


    }
}
