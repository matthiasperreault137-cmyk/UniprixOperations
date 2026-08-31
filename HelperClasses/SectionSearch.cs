using UniprixOperations.ProductManager;
using UniprixOperations.XmlDataManager;

namespace UniprixOperations.HelperClasses
{
    public static class SectionSearch
    {
        public static Section? FindSectionById(int sectionId)
        {
            foreach (var section in DataManager.Instance.Sections.SectionList)
            {
                if (section.Id == sectionId)
                {
                    return section;
                }
            }
            return null; // Return null if not found
        }
        public static List<Section> SortByName(string name)
        {
            List<Section> sections = new List<Section>();
            
            if(name == null || name == "") 
            {
                foreach (Section section in DataManager.Instance.Sections.SectionList)
                {
                    sections.Add(section);
                }
            }
            else
            {
                foreach (Section section in DataManager.Instance.Sections.SectionList)
                {
                    if (section.Name.Contains(name))
                    {
                        sections.Add(section);
                    }
                }
            }
            return sections;
        }
    }
}
