using System.Windows;
using System.Windows.Controls;
using UniprixOperations.HelperClasses;
using UniprixOperations.PageData.ManagerPageData;
using UniprixOperations.TaskManagement;
using UniprixOperations.XmlDataManager;

namespace UniprixOperations.ManagerPages
{
    /// <summary>
    /// Logique d'interaction pour AddRecurringTask.xaml
    /// </summary>
    public partial class AddRecurringTask : Page, IPageDef
    {
        private AddRecurringTaskData data;
        public AddRecurringTask(AddRecurringTaskData data)
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
            ARTName.Text = "";
            ARTRecurrence.Text = "";
            ARTDescription.Text = "";
            ARTCheck1.IsChecked = false;
            ARTCheck2.IsChecked = false;
        }

        //Add product

        private void AddTask(object sender, RoutedEventArgs e)
        {
            string name = "";
            int recurrence;
            string description = "";
            int visibilityLevel;

            if (VerifName())
            {
                name = ARTName.Text;
            }
            else
            {
                return;
            }

            if (VerifRecurrence())
            {
                recurrence = int.Parse(ARTRecurrence.Text);
            }
            else
            {
                return;
            }

            if (VerifDescription())
            {
                description = ARTDescription.Text;
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


            DataManager.Instance.RecurringTasks.RecurringTaskList.Add(new RecurringTask(name, description, visibilityLevel, recurrence));
            Refresh();
            MessageBox.Show("Tâche ajoutée avec succès");

        }

        //Field Verifications / Constraint events

        private void Check2Event(object sender, RoutedEventArgs e)
        {
            ARTCheck2.IsChecked = true;
        }
        private void Uncheck2Event(Object sender, RoutedEventArgs e)
        {
            ARTCheck1.IsChecked = false;
        }


        private bool VerifName()
        {
            if (ARTName.Text == null || ARTName.Text == "")
            {
                MessageBox.Show("Veuillez entrer un nom pour le produit");
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool VerifRecurrence()
        {
            if(ARTRecurrence.Text == null || ARTRecurrence.Text == "" || !int.TryParse(ARTRecurrence.Text, out _))
            {
                MessageBox.Show("Veuillez entrer une récurrence valide pour la tâche");
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool VerifDescription()
        {
            if (ARTDescription.Text == null)
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
            if (ARTCheck1.IsChecked == false && ARTCheck2.IsChecked == false)
            {
                MessageBox.Show("Vous devez rendre la tâche visible à au moins 1 des 2 types de personnel");
                return 0;
            }
            else if (ARTCheck1.IsChecked == false && ARTCheck2.IsChecked == true)
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
