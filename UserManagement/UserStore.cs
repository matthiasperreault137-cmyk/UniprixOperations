using System.Windows;


namespace UniprixOperations.UserManagement
{
    public static class UserStore
    {
        private static User _currentUser;
        public static User CurrentUser
        {
            get => _currentUser;
            set
            {
                if (value != null)
                    ((MainWindow)Application.Current.MainWindow).MWCurrentUser.Text = value.Name;
                else
                    ((MainWindow)Application.Current.MainWindow).MWCurrentUser.Text = "Non connecté / Disconnected";

                _currentUser = value;
            }
        }
    }
}
