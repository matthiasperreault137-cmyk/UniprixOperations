using System.Windows;
using System.Windows.Controls;
using UniprixOperations.EmployeePages;
using UniprixOperations.ManagerPages;
using UniprixOperations.PageData.EmployeePageData;
using UniprixOperations.PageData.ManagerPageData;
using UniprixOperations.UserManagement;

namespace UniprixOperations.HelperClasses
{
    public class Navigator
    {
        private static Frame frame;

        //Employee Pages
        private static EmployeeDailyTasksData employeeDailyTasksData;
        private static EmployeeLoginData employeeLoginData;
        private static EmployeeMainMenuData employeeMainMenuData;
        private static EmployeeRecurringTasksData employeeRecurringTasksData;
        private static EmployeeTasksData employeeTasksData;
        private static EmployeeAddExpiredProductData employeeAddExpiredProductData;

        private static EmployeeMessagesData employeeMessagesData;


        //Manager Pages

        private static AddDailyTaskData addDailyTaskData;
        private static AddRecurringTaskData addRecurringTaskData;
        private static ManagerAddExpiredProductData managerAddExpiredProductData;
        private static ManagerDailyTasksData managerDailyTasksData;
        private static ManagerExpireMenuData managerExpireMenuData;
        private static ManagerLoginData managerLoginData;
        private static ManagerMainMenuData managerMainMenuData;
        private static ManagerRecurringTasksData managerRecurringTasksData;
        private static ManagerTasksData managerTasksData;
        private static SectionsData sectionsData;
        private static TaskHistoryData taskHistoryData;
        private static ManagerMessagesData managerMessagesData;
        private static ManagerAllExpiredProductsData managerAllExpiredProductsData;
        private static UserMenuData userMenuData;
        private static ManagerUserListData managerUserListData;
        private static ManagerAddUserData managerAddUserData;
        private static SectionListData sectionListData;
        private static AddSectionData addSectionData;

        //Common Pages



        public static void Initialize(Frame _frame)
        {
            frame = _frame;
        }

        //Employee Navigation
        public static void OpenEmployeeDailyTasks()
        {
            frame.NavigationService.Navigate(new EmployeeDailyTasks(employeeDailyTasksData));
        }
        public static void OpenEmployeeAddExpiredProduct()
        {
            frame.NavigationService.Navigate(new EmployeeAddExpiredProduct(employeeAddExpiredProductData));
        }
        public static void OpenEmployeeLogin()
        {
            frame.NavigationService.Navigate(new EmployeeLogin(employeeLoginData));
        }
        public static void OpenEmployeeMainMenu()
        {
            frame.NavigationService.Navigate(new EmployeeMainMenu(employeeMainMenuData));
        }
        public static void OpenEmployeeRecurringTasks()
        {
            frame.NavigationService.Navigate(new EmployeeRecurringTasks(employeeRecurringTasksData));
        }
        public static void OpenEmployeeTasks()
        {
            frame.NavigationService.Navigate(new EmployeeTasks(employeeTasksData));
        }

        //Manager Navigation

        public static void OpenAddDailyTask()
        {
            frame.NavigationService.Navigate(new AddDailyTask(addDailyTaskData));
        }

        public static void OpenAddRecurringTask()
        {
            frame.NavigationService.Navigate(new AddRecurringTask(addRecurringTaskData));
        }

        public static void OpenManagerAddExpiredProduct()
        {
            frame.NavigationService.Navigate(new ManagerAddExpiredProduct(managerAddExpiredProductData));
        }

        public static void OpenManagerDailyTasks()
        {
            frame.NavigationService.Navigate(new ManagerDailyTasks(managerDailyTasksData));
        }

        public static void OpenManagerExpireMenu()
        {
            frame.NavigationService.Navigate(new ManagerExpireMenu(managerExpireMenuData));
        }

        public static void OpenManagerLogin()
        {
            frame.NavigationService.Navigate(new ManagerLogin(managerLoginData));
        }

        public static void OpenManagerMainMenu()
        {
            frame.NavigationService.Navigate(new ManagerMainMenu(managerMainMenuData));
        }

        public static void OpenManagerRecurringTasks()
        {
            frame.NavigationService.Navigate(new ManagerRecurringTasks(managerRecurringTasksData));
        }

        public static void OpenManagerTasks()
        {
            frame.NavigationService.Navigate(new ManagerTasks(managerTasksData));
        }
        public static void OpenSections()
        {
            frame.NavigationService.Navigate(new Sections(sectionsData));
        }

        public static void OpenTaskHistory()
        {
            frame.NavigationService.Navigate(new TaskHistory(taskHistoryData));
        }
        public static void OpenManagerMessages()
        {
            frame.NavigationService.Navigate(new ManagerMessages(managerMessagesData));

        }

        public static void OpenManagerAllExpiredProducts()
        {
            frame.NavigationService.Navigate(new ManagerAllExpiredProducts(managerAllExpiredProductsData));
        }

        public static void OpenUserMenu()
        {
            if(UserStore.CurrentUser is Manager)
            {
                frame.NavigationService.Navigate(new UserMenu(userMenuData));
            }
            else
            {
                MessageBox.Show("Seul un gérant peut accéder à ceci\nOnly Managers can access this");
                return;
            }
        }
        public static void OpenManagerAddUser()
        {
            frame.NavigationService.Navigate(new ManagerAddUser(managerAddUserData));
        }
        public static void OpenManagerUserList()
        {
            frame.NavigationService.Navigate(new ManagerUserList(managerUserListData));
        }
        public static void OpenAddSection()
        {
            frame.NavigationService.Navigate(new AddSection(addSectionData));
        }
        public static void OpenSectionList()
        {
            frame.NavigationService.Navigate(new SectionList(sectionListData));
        }

        public static void OpenEmployeeMessages()
        {
            if ((UserStore.CurrentUser != null) && (UserStore.CurrentUser.Name.ToLower() == "felix couture"))
            {
                frame.NavigationService.Navigate(new EmployeeMessages(employeeMessagesData));
            }
            else
            {
                return;
            }
        }
    }
}
