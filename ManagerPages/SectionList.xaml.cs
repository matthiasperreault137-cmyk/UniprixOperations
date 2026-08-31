using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using UniprixOperations.HelperClasses;
using UniprixOperations.PageData.ManagerPageData;
using UniprixOperations.UserManagement;
using UniprixOperations.ProductManager;

namespace UniprixOperations.ManagerPages
{
    /// <summary>
    /// Logique d'interaction pour SectionList.xaml
    /// </summary>
    public partial class SectionList : Page, IPageDef
    {
        private string searchText;
        private Section? selectedSection;
        private SectionListData data;
        public SectionList(SectionListData data)
        {
            InitializeComponent();
            this.data = data;
            Refresh();
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


        //Refresh
        public void Refresh()
        {
            load();
            SLSectionDetails.Content = null;
        }



        //Loading List
        private void load()
        {
            SLSections.Children.Clear();
            List<Section> sections = SectionSearch.SortByName(searchText);
            foreach(Section section in sections)
            {
                Button b = CreateSectionButton(section);
                SLSections.Children.Add(b);
            }
            
        }
        private Button CreateSectionButton(Section section)
        {
            Button b = new Button();
            b.Content = section.Name;
            b.Click += (s, e) => Select(section);
            b.Height = 90;
            b.Style = (Style)FindResource("ManagerSection");
            return b;

        }
        private void Select(Section section)
        {
            if (selectedSection == section)
            {
                selectedSection = null;
                SLSectionDetails.Content = null;
            }
            else
            {
                selectedSection = section;
                SLSectionDetails.Content = new SectionDetails(section, this);
            }


        }


        //Searching

        private void SearchTextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            TextBox text = (TextBox)sender;


            searchText = SLSearch.Text;

            if (this.IsLoaded)
            {
                Refresh();
            }
        }


    }
}
