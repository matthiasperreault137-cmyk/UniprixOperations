using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Effects;
using UniprixOperations.HelperClasses;
using UniprixOperations.PageData.ManagerPageData;
using UniprixOperations.TaskManagement;
using UniprixOperations.UserManagement;
using UniprixOperations.XmlDataManager;



namespace UniprixOperations.ManagerPages
{
    /// <summary>
    /// Logique d'interaction pour ManagerRecurringTaskDetails.xaml
    /// </summary>
    public partial class ManagerRecurringTaskDetails : Page, IPageDef
    {
        private bool isEditing = false;
        private RecurringTask task;
        private ManagerRecurringTasks parent;
        public ManagerRecurringTaskDetails(ManagerRecurringTasks parent, RecurringTask task)
        {
            InitializeComponent();
            this.task = task;
            this.parent = parent;
            Refresh();
        }

        public void Refresh()
        {
            Load();
        }
        //Loading
        private void Load()
        {
            MRTDName.Text = task.Name;
            MRTDDescription.Text = task.Description;
            MRTDRecurrence.Text = task.Recurrence.ToString();

            LoadSubtasks();
        } 

        //Subtask loading

        private void LoadSubtasks()
        {
            MRTDSubtasks.Children.Clear();
            RecurringTaskSearch.SortSubtasksNearest(task.SubTasks);
            if (task.SubTasks != null)
            {
                foreach (SubTask subtask in task.SubTasks)
                {
                    MRTDSubtasks.Children.Add(CreateSubtaskGrid(subtask));
                }
            }
            else
            {
                MRTDSubtasks.Children.Add(new TextBlock { Text = "Aucun Sous-Tâches" });
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
                Style = (Style)FindResource("SubtaskButton2"),
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
                Style = (Style)FindResource("SubtaskGrid2"),
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

        //Deletion

        private void DeleteTask(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show(
                "Vous êtes sur le point de SUPPRIMER la tâche, êtes vous sûr?\n Your about to DELETE the task, are you sure?",
                "",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question
            );

            if (result == MessageBoxResult.Yes)
            {
                MessageBox.Show($"Tâche '{task.Name}' supprimée ! \n (Rafraichir la page pour voir les modifications)");
                DataManager.Instance.RecurringTasks.RecurringTaskList.Remove(task);
                parent.Refresh();
            }
            else
            {
                // user cancelled
            }
        }


        //Modify


        private void ModifyTask(object sender, RoutedEventArgs e)
        {
            if(isEditing)
            {
                LockFields();
            }
            else
            {
                UnlockFields();
            }   
        }

        private void LockFields()
        {
            MRTDName.IsReadOnly = true;
            MRTDDescription.IsReadOnly = true;
            MRTDRecurrence.IsReadOnly = true;
            isEditing = false;

            if (MRTDName.Text != null && MRTDDescription.Text != null)
            {
                task.Name = MRTDName.Text;
            }
            else
            {
                MessageBox.Show("Le nom de la tâche ne peut pas être vide.");
            }

            if(MRTDDescription.Text != null && MRTDDescription.Text != "")
            {
                task.Description = MRTDDescription.Text;
            }
            else
            {
                MessageBox.Show("La description de la tâche ne peut pas être vide.");
            }

            if (MRTDRecurrence.Text != null && MRTDRecurrence.Text != "")
            {
                if (int.TryParse(MRTDRecurrence.Text, out int result))
                {
                    task.Recurrence = result;
                }
                else
                {
                    MessageBox.Show("Entrez un chiffre valide \n Enter a valid number");
                }
            }
            else
            {
                MessageBox.Show("Veuillez entrer une valeur de récurrence pour la tâche \n Enter a value for the recurrence of the task");
            }

            MRTDModify.Content = "Modifier";
            MessageBox.Show("Veuillez rafraichir pour voir les modifications \n Please refresh the page to see your modifications");
            Refresh();
        }

        private void UnlockFields()
        {
            MRTDName.IsReadOnly = false;
            MRTDDescription.IsReadOnly = false;
            MRTDRecurrence.IsReadOnly = false;
            isEditing = true;
            MRTDModify.Content = "Enregistrer";
        }
    }
}
