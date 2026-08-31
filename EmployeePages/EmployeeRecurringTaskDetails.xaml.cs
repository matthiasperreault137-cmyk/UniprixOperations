using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Effects;
using UniprixOperations.HelperClasses;
using UniprixOperations.PageData.EmployeePageData;
using UniprixOperations.TaskManagement;
using UniprixOperations.UserManagement;

namespace UniprixOperations.EmployeePages
{
    /// <summary>
    /// Logique d'interaction pour EmployeeRecurringTaskDetails.xaml
    /// </summary>
    public partial class EmployeeRecurringTaskDetails : Page ,IPageDef
    {
        private bool isEditing = false;
        private RecurringTask task;

        private EmployeeRecurringTasks parent;
        public EmployeeRecurringTaskDetails(RecurringTask task, EmployeeRecurringTasks parent)
        {
            InitializeComponent();
            this.parent = parent;
            this.task = task;
            Refresh();
        }




        public void Refresh()
        {
            Load();
        }
        //Loading
        private void Load()
        {
            ERTDName.Text = task.Name;
            ERTDDescription.Text = task.Description;
            ERTDRecurrence.Text = task.Recurrence.ToString();

            LoadSubtasks();
        }

        //Subtask loading

        private void LoadSubtasks()
        {
            ERTDSubtasks.Children.Clear();
            RecurringTaskSearch.SortSubtasksNearest(task.SubTasks);
            if (task.SubTasks != null)
            {
                foreach (SubTask subtask in task.SubTasks)
                {
                    //Aucun check de visibilité parce que je pense qu'on s'en fout, si on a accès à la tâche on a accès à ses sous-tâches
                    ERTDSubtasks.Children.Add(CreateSubtaskGrid(subtask));
                }
            }
            else
            {
                ERTDSubtasks.Children.Add(new TextBlock { Text = "Aucune Sous-Tâches", FontSize = 50, Height = 200, TextAlignment = TextAlignment.Center, VerticalAlignment = VerticalAlignment.Center});
            }
        }

        private Border CreateSubtaskGrid(SubTask subtask)
        {
            float a = (DateTime.Today - subtask.CreationDate).Days / (float)subtask.Recurrence;
            a = Math.Clamp(a, 0f, 1f);
            byte red = (byte)(Math.Clamp(a * 2, 0f, 1f) * 255);
            byte green = (byte)(Math.Clamp(1 - (a * 2 - 1), 0f, 1f) * 255);

            var grid = new Grid();
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1.5, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(90, GridUnitType.Pixel) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            var nameBlock = new TextBlock
            {
                Text = subtask.Name,
                Style = (Style)FindResource("SubtaskNameBlock")
            };
            Grid.SetColumn(nameBlock, 0);

            // Color-coded day badge (same logic as RecurringTask)
            var daysBadge = new Border
            {
                Background = new SolidColorBrush(Color.FromRgb(red, green, 0)),
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                BorderThickness = new Thickness(1.5, 0, 1.5, 0),
                BorderBrush = new SolidColorBrush(Color.FromRgb(0xE0, 0xE0, 0xE0)),
                Child = new StackPanel
                {
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Children =
            {
                new TextBlock
                {
                    Text              = (DateTime.Today - subtask.CreationDate).Days.ToString(),
                    FontSize          = 16,
                    FontWeight        = FontWeights.Bold,
                    Foreground        = Brushes.Black,
                    TextAlignment     = TextAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center
                },
                new TextBlock
                {
                    Text              = "jours",
                    FontSize          = 9,
                    Foreground        = new SolidColorBrush(Color.FromArgb(0xDD, 0x00, 0x00, 0x00)),
                    TextAlignment     = TextAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center
                }
            }
                }
            };
            Grid.SetColumn(daysBadge, 1);

            var completeButton = new Button
            {
                Content = "Compléter",
                Style = (Style)FindResource("SubtaskButton"),
                Margin = new Thickness(8)
            };
            completeButton.Click += (s, e) => CompleteSubtask(subtask);
            Grid.SetColumn(completeButton, 2);

            grid.Children.Add(nameBlock);
            grid.Children.Add(daysBadge);
            grid.Children.Add(completeButton);

            // Wrap in styled border (hover effect + shadow)
            var border = new Border
            {
                Style = (Style)FindResource("SubtaskGrid"),
                Child = grid
            };
            border.Effect = new DropShadowEffect
            {
                BlurRadius = 4,
                ShadowDepth = 2,
                Color = Color.FromRgb(0x17, 0x1A, 0x1C),
                Opacity = 0.08
            };

            return border;
        }

        private void CompleteSubtask(SubTask subtask)
        {
            MessageBox.Show($"Tâche '{subtask.Name}' complétée ! \n (Rafraichir la page pour voir les modifications)");
            subtask.Complete(UserStore.CurrentUser);
            LoadSubtasks();
        }

        //Completion
        private void CompleteTask(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"Tâche '{task.Name}' complétée ! \n (Rafraichir la page pour voir les modifications)");
            task.Complete(UserStore.CurrentUser);
            parent.Refresh();
        }
    }
}
