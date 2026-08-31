using System.Windows;
using System.Windows.Controls;
using UniprixOperations.HelperClasses;
using UniprixOperations.PageData.EmployeePageData;
using UniprixOperations.UserManagement;
using UniprixOperations.XmlDataManager;

namespace UniprixOperations
{
    /// <summary>
    /// Logique d'interaction pour EmployeeLogin.xaml
    /// </summary>
    public partial class EmployeeLogin : Page, IPageDef
    {
        private EmployeeLoginData data;
        public EmployeeLogin(EmployeeLoginData data)
        {
            InitializeComponent();
            this.data = data;
            Refresh();
        }


        public void Refresh()
        {
            EmployeeComboBox.Items.Clear();
            LoadUserChoices();
        }

        //ComboBox Item loading
        private ComboBoxItem CreateUserChoice(User user)
        {
            ComboBoxItem item = new ComboBoxItem
            {
                Content = user.Name
            };
            return item;
        }

        private void LoadUserChoices()
        {
            foreach (User user in DataManager.Instance.Users.UserList)
            {
                if (user is Employee)
                {
                    EmployeeComboBox.Items.Add(CreateUserChoice(user));
                }
            }
        }

        //User connection
        private void ConnectUser(Object sender, RoutedEventArgs e)
        {
            if (EmployeeComboBox.SelectedItem == null)
            {
                return;
            }
            else
            {

                UserStore.CurrentUser = UserSearch.SearchByName(((ComboBoxItem)EmployeeComboBox.SelectedItem).Content.ToString(), DataManager.Instance.Users.UserList);
                Navigator.OpenEmployeeMainMenu();
            }
        }

    }
}
