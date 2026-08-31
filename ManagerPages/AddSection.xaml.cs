using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using UniprixOperations.HelperClasses;
using UniprixOperations.PageData.ManagerPageData;
using UniprixOperations.ProductManager;
using UniprixOperations.XmlDataManager;

namespace UniprixOperations.ManagerPages
{
    /// <summary>
    /// Logique d'interaction pour AddSection.xaml
    /// </summary>
    public partial class AddSection : Page , IPageDef
    {
        private AddSectionData data;
        public AddSection(AddSectionData data)
        {
            InitializeComponent();
            this.data = data;
            Refresh();
        }


        //Navigation

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
            ASName.Text = "";
            ASDescription.Text = "";
        }


        //Add section

        private void Add(object sender, RoutedEventArgs e)
        {
            if(ASName.Text == null || ASName.Text == "")
            {
                MessageBox.Show("Veuillez entrer un nom pour la section \n Please enter a name for the section");
                return;
            }
            string name = ASName.Text;
            if(ASDescription.Text == null || ASDescription.Text == "")
            {
                MessageBox.Show("Veuillez entrer une description pour la section \n Please enter a description for the section");
                return;
            }
            string description = ASDescription.Text;
            Section section = new Section(name, description);
            DataManager.Instance.Sections.SectionList.Add(section);
            MessageBox.Show("Section Ajouté avec succès");
            Refresh();
        }

    }
}
