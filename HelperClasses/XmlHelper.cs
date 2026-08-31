using System.IO;
using System.Xml.Linq;
using System.Xml.Serialization;
using UniprixOperations.ProductManager;
using UniprixOperations.TaskManagement;
using UniprixOperations.UserManagement;
using UniprixOperations.XmlDataManager;
using UniprixOperations.MessageManagement;
namespace UniprixOperations.HelperClasses
{
    public static class XmlHelper
    {

        //Sérializeurs
        public static readonly XmlSerializer IdCountersSerializer =
            new XmlSerializer(typeof(IdCountersWrapper));

        public static readonly XmlSerializer UsersSerializer =
            new XmlSerializer(typeof(UserWrapper));

        public static readonly XmlSerializer ExpiredProductsSerializer =
            new XmlSerializer(typeof(ExpiredProducts));

        public static readonly XmlSerializer SectionsSerializer =
            new XmlSerializer(typeof(Sections));

        public static readonly XmlSerializer DailyTasksSerializer =
            new XmlSerializer(typeof(DailyTasks));

        public static readonly XmlSerializer RecurringTasksSerializer =
            new XmlSerializer(typeof(RecurringTasks));

        public static readonly XmlSerializer TaskHistorySerializer =
            new XmlSerializer(typeof(TaskHistory));

        public static readonly XmlSerializer MessagesSerializer =
            new XmlSerializer(typeof(Messages));

        //Renvoie chemin pour accès au special folder

        public static string GetAppDataPath()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "UniprixOperations");
        }


        //Load et save Xml Doc

        public static void SaveXml(XDocument doc, string folderPath, string fileName)
        {
            string path = Path.Combine(folderPath, fileName);
            doc.Save(path);
        }


    }
}
