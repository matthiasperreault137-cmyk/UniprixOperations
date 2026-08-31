using System.Windows;
using System.Windows.Controls;
using UniprixOperations.HelperClasses;
using UniprixOperations.PageData.ManagerPageData;
using UniprixOperations.XmlDataManager;
using UniprixOperations.TaskManagement;
using System.ComponentModel;


namespace UniprixOperations.ManagerPages
{
    /// <summary>
    /// Logique d'interaction pour AddDailyTask.xaml
    /// </summary>
    public partial class AddDailyTask : Page, IPageDef
    {
        private AddDailyTaskData data;
        public AddDailyTask(AddDailyTaskData data)
        {
            InitializeComponent();
            this.data = data;
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

        //Refreshing

        public void Refresh()
        {
            ADTName.Text = "";
            ADTDate.SelectedDate = null;
            ADTHour.Text = "";
            ADTDescription.Text = "";
            ADTCheck1.IsChecked = false;
            ADTCheck2.IsChecked = false;
        }

        //Add product

        private void AddTask(object sender, RoutedEventArgs e)
        {
            string name = "";
            DateOnly date;
            TimeOnly? hour = null;
            string description = "";
            int visibilityLevel;

            if(VerifName())
            {
                name = ADTName.Text;
            }
            else
            {
                return;
            }

            if(VerifDate())
            {
                date = DateOnly.FromDateTime(ADTDate.SelectedDate.Value);
            }
            else
            {
                return;
            }

            if(VerifHour())
            {
                if (ADTHour.Text == null || ADTHour.Text == "")
                {
                    hour = null;
                }
                else
                {
                    hour = TimeOnly.Parse(ADTHour.Text);
                }
            }
            else
            {
                return;
            }

            if(VerifDescription())
            {
                description = ADTDescription.Text;
            }
            else 
            {
                return;
            }

            if (VerifVisibilityLevel() != 0)
            {
                visibilityLevel = VerifVisibilityLevel();
            }
            else
            {
                return;
            }


            DataManager.Instance.DailyTasks.DailyTaskList.Add(new DailyTask(name, description, visibilityLevel, date, hour));
            Refresh();
            MessageBox.Show("Tâche ajoutée avec succès");

        }

        //Field Verifications / Constraint events

        private void Check2Event(object sender, RoutedEventArgs e)
        {
            ADTCheck2.IsChecked = true;
        }
        private void Uncheck2Event(Object sender, RoutedEventArgs e)
        {
            ADTCheck1.IsChecked = false;
        }


        private bool VerifName()
        {
            if (ADTName.Text == null || ADTName.Text == "")
            {
                MessageBox.Show("Veuillez entrer un nom pour le produit");
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool VerifDate()
        {
            if(ADTDate.SelectedDate == null)
            {
                MessageBox.Show("Veuillez entrer une date pour la tâche");
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool VerifHour()
        {
            if (ADTHour.Text == null || ADTHour.Text == "" || TimeOnly.TryParse(ADTHour.Text, out _))
            {
                return true;
            }
            else
            {
                MessageBox.Show("Veuillez entre une heure valide : HH:mm");
                return false;
            }
        }

        private bool VerifDescription()
        {
            if(ADTDescription.Text == null)
            {
                MessageBox.Show("Veuillez entrez une description");
                return false;
            }
            else
            {
                return true;
            }
        }

        private int VerifVisibilityLevel()
        {
            if(ADTCheck1.IsChecked == false && ADTCheck2.IsChecked == false)
            {
                MessageBox.Show("Vous devez rendre la tâche visible à au moins 1 des 2 types de personnel");
                return 0;
            }
            else if(ADTCheck1.IsChecked == false && ADTCheck2.IsChecked == true)
            {
                return 1;
            }
            else
            {
                return 2;
            }
        }
    }
}
