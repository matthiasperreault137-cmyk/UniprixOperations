using System.Windows;
using System.Windows.Controls;
using UniprixOperations.HelperClasses;
using UniprixOperations.PageData.ManagerPageData;
using UniprixOperations.UserManagement;
using UniprixOperations.XmlDataManager;
using System.Windows.Input;

namespace UniprixOperations.ManagerPages
{
    /// <summary>
    /// Logique d'interaction pour ManagerLogin.xaml
    /// </summary>
    public partial class ManagerLogin : Page, IPageDef
    {
        private ManagerLoginData data;
        public ManagerLogin(ManagerLoginData data)
        {
            InitializeComponent();
            this.data = data;
            Refresh();
        }

        public void Refresh()
        {
            ManagerComboBox.Items.Clear();
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
                if (user is Supervisor)
                {
                    ManagerComboBox.Items.Add(CreateUserChoice(user));
                }
            }
        }

        //User connection
        private void ConnectUser(Object sender, RoutedEventArgs e)
        {
            if (ManagerComboBox.SelectedItem == null)
            {
                return;
            }
            else
            {

                User user = UserSearch.SearchByName(((ComboBoxItem)ManagerComboBox.SelectedItem).Content.ToString(), DataManager.Instance.Users.UserList);
                if (ValidatePassWord(user))
                {
                    UserStore.CurrentUser = user;
                    Navigator.OpenManagerMainMenu();
                }
                else
                {
                    MessageBox.Show("Mot de passe incorrect");
                }
            }
        }

        private bool ValidatePassWord(User user)
        {
            if (user == null)
            {
                return false;
            }
            Supervisor manager = (Supervisor)user;
            if (manager.PassWord.Trim() == PasswordTextBox.Text.Trim())
            {
                return true;
            }
            return false;
        }

        private void EnterKeyDown(object sender, KeyEventArgs e)
        {
            if(Key.Enter == e.Key)
            ConnectUser(sender, e);
        }
    }
}
