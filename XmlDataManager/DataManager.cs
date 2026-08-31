using System.IO;
using System.Windows;
using UniprixOperations.HelperClasses;
using UniprixOperations.MessageManagement;
using UniprixOperations.ProductManager;
using UniprixOperations.TaskManagement;
using UniprixOperations.UserManagement;

namespace UniprixOperations.XmlDataManager
{
    public class DataManager
    {
        private static DataManager _instance;

        public static DataManager Instance => _instance ??= new DataManager();

        private DataManager()
        {

        }



        //Counter Wrapper
        private IdCountersWrapper idCounters;


        //Counters
        public int SectionIdCounter { get; set; }
        public int ProductIdCounter { get; set; }
        public int DailyTasksIdCounter { get; set; }
        public int RecurringTasksIdCounter { get; set; }
        public int UserIdCounter { get; set; }

        //Data Lists

        public Sections Sections { get; set; }
        public ExpiredProducts ExpiredProducts { get; set; }
        public DailyTasks DailyTasks { get; set; }
        public RecurringTasks RecurringTasks { get; set; }
        public UserWrapper Users { get; set; }
        public TaskHistory TaskHistory { get; set; }
        public Messages Messages { get; set; }




        //Fonctions pour load la data

        public void LoadData()
        {
            LoadSections();
            LoadIdCounters();
            LoadDailyTasks();
            LoadRecurringTasks();
            LoadUsers();
            LoadProducts();
            LoadTaskHistory();
            LoadMessages();
        }

        private void LoadSections()
        {
            string fileName = "Sections.xml";
            string test = Path.Combine(XmlHelper.GetAppDataPath(), "XmlData");
            string fullPath = Path.Combine(test, fileName);
            using FileStream stream = new FileStream(fullPath, FileMode.Open);
            Sections result = (Sections)XmlHelper.SectionsSerializer.Deserialize(stream);
            if (result != null)
            {
                Sections = result;
            }
        }
        private void LoadIdCounters()
        {
            string fileName = "IdCounters.xml";
            string test = Path.Combine(XmlHelper.GetAppDataPath(), "XmlData");
            string fullPath = Path.Combine(test, fileName);
            using FileStream stream = new FileStream(fullPath, FileMode.Open);
            IdCountersWrapper result = (IdCountersWrapper)XmlHelper.IdCountersSerializer.Deserialize(stream);
            if (result != null)
            {
                SectionIdCounter = result.SectionIdCounter;
                ProductIdCounter = result.ProductIdCounter;
                DailyTasksIdCounter = result.DailyTasksIdCounter;
                RecurringTasksIdCounter = result.RecurringTasksIdCounter;
                UserIdCounter = result.UserIdCounter;
            }
        }
        private void LoadDailyTasks()
        {
            string fileName = "DailyTasks.xml";
            string test = Path.Combine(XmlHelper.GetAppDataPath(), "XmlData");
            string fullPath = Path.Combine(test, fileName);
            using FileStream stream = new FileStream(fullPath, FileMode.Open);
            DailyTasks result = (DailyTasks)XmlHelper.DailyTasksSerializer.Deserialize(stream);
            if (result != null)
            {
                DailyTasks = result;
            }
        }
        private void LoadRecurringTasks()
        {
            string fileName = "RecurringTasks.xml";
            string test = Path.Combine(XmlHelper.GetAppDataPath(), "XmlData");
            string fullPath = Path.Combine(test, fileName);
            using FileStream stream = new FileStream(fullPath, FileMode.Open);
            RecurringTasks result = (RecurringTasks)XmlHelper.RecurringTasksSerializer.Deserialize(stream);
            if (result != null)
            {
                RecurringTasks = result;
            }
        }
        private void LoadUsers()
        {
            string fileName = "Users.xml";
            string test = Path.Combine(XmlHelper.GetAppDataPath(), "XmlData");
            string fullPath = Path.Combine(test, fileName);
            using FileStream stream = new FileStream(fullPath, FileMode.Open);
            UserWrapper result = (UserWrapper)XmlHelper.UsersSerializer.Deserialize(stream);
            if (result != null)
            {
                Users = result;
            }
        }
        private void LoadProducts()
        {
            string fileName = "ExpiredProducts.xml";
            string test = Path.Combine(XmlHelper.GetAppDataPath(), "XmlData");
            string fullPath = Path.Combine(test, fileName);
            using FileStream stream = new FileStream(fullPath, FileMode.Open);
            ExpiredProducts result = (ExpiredProducts)XmlHelper.ExpiredProductsSerializer.Deserialize(stream);
            foreach (ExpiredProduct product in result.ExpiredProductList)
            {
                product.UPCCode = product.UPCCode.PadLeft(11, '0'); 
            }
            if (result != null)
            {
                ExpiredProducts = result;
            }
        }
        private void LoadTaskHistory()
        {
            string fileName = "TaskHistory.xml";
            string test = Path.Combine(XmlHelper.GetAppDataPath(), "XmlData");
            string fullPath = Path.Combine(test, fileName);
            using FileStream stream = new FileStream(fullPath, FileMode.Open);
            TaskHistory result = (TaskHistory)XmlHelper.TaskHistorySerializer.Deserialize(stream);
            if (result != null)
            {
                TaskHistory = result;
            }
        }
        private void LoadMessages()
        {
            string fileName = "Messages.xml";
            string test = Path.Combine(XmlHelper.GetAppDataPath(), "XmlData");
            string fullPath = Path.Combine(test, fileName);
            using FileStream stream = new FileStream(fullPath, FileMode.Open);
            Messages result = (Messages)XmlHelper.MessagesSerializer.Deserialize(stream);
            if (result != null)
            {
                Messages = result;
            }
        }


        //Fonctions pour Save la data
        public void SaveData()
        {
            SaveSections();
            SaveIdCounters();
            SaveDailyTasks();
            SaveRecurringTasks();
            SaveUsers();
            SaveProducts();
            SaveTaskHistory();
            SaveMessages();
        }

        private void SaveSections()
        {
            string fileName = "Sections.xml";
            string test = Path.Combine(XmlHelper.GetAppDataPath(), "XmlData");
            string fullPath = Path.Combine(test, fileName);
            using FileStream stream = new FileStream(fullPath, FileMode.Create);
            XmlHelper.SectionsSerializer.Serialize(stream, Sections);
        }
        private void SaveIdCounters()
        {
            string fileName = "IdCounters.xml";
            string test = Path.Combine(XmlHelper.GetAppDataPath(), "XmlData");
            string fullPath = Path.Combine(test, fileName);
            using FileStream stream = new FileStream(fullPath, FileMode.Create);
            IdCountersWrapper wrapper = new IdCountersWrapper
            {
                SectionIdCounter = SectionIdCounter,
                ProductIdCounter = ProductIdCounter,
                DailyTasksIdCounter = DailyTasksIdCounter,
                RecurringTasksIdCounter = RecurringTasksIdCounter,
                UserIdCounter = UserIdCounter
            };
            XmlHelper.IdCountersSerializer.Serialize(stream, wrapper);
        }
        private void SaveDailyTasks()
        {
            string fileName = "DailyTasks.xml";
            string test = Path.Combine(XmlHelper.GetAppDataPath(), "XmlData");
            string fullPath = Path.Combine(test, fileName);
            using FileStream stream = new FileStream(fullPath, FileMode.Create);
            XmlHelper.DailyTasksSerializer.Serialize(stream, DailyTasks);
        }
        private void SaveRecurringTasks()
        {
            string fileName = "RecurringTasks.xml";
            string test = Path.Combine(XmlHelper.GetAppDataPath(), "XmlData");
            string fullPath = Path.Combine(test, fileName);
            using FileStream stream = new FileStream(fullPath, FileMode.Create);
            XmlHelper.RecurringTasksSerializer.Serialize(stream, RecurringTasks);
        }
        private void SaveUsers()
        {
            string fileName = "Users.xml";
            string test = Path.Combine(XmlHelper.GetAppDataPath(), "XmlData");
            string fullPath = Path.Combine(test, fileName);
            using FileStream stream = new FileStream(fullPath, FileMode.Create);
            XmlHelper.UsersSerializer.Serialize(stream, Users);
        }
        private void SaveProducts()
        {
            string fileName = "ExpiredProducts.xml";
            string test = Path.Combine(XmlHelper.GetAppDataPath(), "XmlData");
            string fullPath = Path.Combine(test, fileName);
            using FileStream stream = new FileStream(fullPath, FileMode.Create);
            XmlHelper.ExpiredProductsSerializer.Serialize(stream, ExpiredProducts);
        }
        private void SaveTaskHistory()
        {
            string fileName = "TaskHistory.xml";
            string test = Path.Combine(XmlHelper.GetAppDataPath(), "XmlData");
            string fullPath = Path.Combine(test, fileName);
            using FileStream stream = new FileStream(fullPath, FileMode.Create);
            XmlHelper.TaskHistorySerializer.Serialize(stream, TaskHistory);
        }
        private void SaveMessages()
        {
            string fileName = "Messages.xml";
            string test = Path.Combine(XmlHelper.GetAppDataPath(), "XmlData");
            string fullPath = Path.Combine(test, fileName);
            using FileStream stream = new FileStream(fullPath, FileMode.Create);
            XmlHelper.MessagesSerializer.Serialize(stream, Messages);
        }



        //Importing data mmanually

        public void ImportData()
        {
            // let the user pick a folder to import from
            var dialog = new Microsoft.Win32.OpenFolderDialog();
            if (dialog.ShowDialog() != true) return;

            string selectedFolder = dialog.FolderName;

            string destFolder = Path.Combine(XmlHelper.GetAppDataPath(), "XmlData");

            // only copy files that actually exist in your app already
            // prevents the user from importing random junk
            foreach (string file in Directory.GetFiles(destFolder, "*.xml"))
            {
                string fileName = Path.GetFileName(file);
                string importFile = Path.Combine(selectedFolder, fileName);

                if (File.Exists(importFile))
                {
                    File.Copy(importFile, file, overwrite: true);
                }
            }

            // reload everything so the app reflects the new files immediately
            DataManager.Instance.LoadData();

            MessageBox.Show("Données impotées avec succès");
        }


        //Export data manually

        public void ExportData()
        {
            SaveData();
            var dialog = new Microsoft.Win32.OpenFolderDialog();
            dialog.Title = "Sélectionner une destination de sauvegarde";
            if (dialog.ShowDialog() != true) return;

            string destFolder = dialog.FolderName;
            string sourceFolder = Path.Combine(XmlHelper.GetAppDataPath(), "XmlData");

            foreach (string file in Directory.GetFiles(sourceFolder, "*.xml"))
            {
                File.Copy(file, Path.Combine(destFolder, Path.GetFileName(file)), overwrite: true);
            }

            MessageBox.Show("Copie de suavegarde enregistré à : " + destFolder);
        }
    }
}
