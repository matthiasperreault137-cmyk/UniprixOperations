using UniprixOperations.UserManagement;
using UniprixOperations.XmlDataManager;

namespace UniprixOperations.HelperClasses
{
    public static class UserSearch
    {
        public static User SearchByName(string name, List<User> userList)
        {
            foreach (User user in userList)
            {
                if (user.Name.Equals(name))
                {
                    return user;
                }
            }
            return null; // Return null if no user is found with the given name
        }

        public static List<User> SortByName(string name)
        {
            List<User> users = new List<User>();
            if(name == null || name == "")
            {
                foreach(User user in DataManager.Instance.Users.UserList)
                {
                    users.Add(user);
                }
            }
            else
            {
                foreach (User user in DataManager.Instance.Users.UserList)
                {
                    if (user.Name.Contains(name))
                    {
                        users.Add(user);
                    }
                }
            }
            return users;
        }
    }
}
