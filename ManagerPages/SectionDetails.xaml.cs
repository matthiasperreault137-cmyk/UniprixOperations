using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using UniprixOperations.ProductManager;
using UniprixOperations.XmlDataManager;

namespace UniprixOperations.ManagerPages
{
    /// <summary>
    /// Logique d'interaction pour SectionDetails.xaml
    /// </summary>
    public partial class SectionDetails : Page, IPageDef
    {

        private bool isEditing;
        private Section section;
        private SectionList parent;
        public SectionDetails(Section section, SectionList parent)
        {
            InitializeComponent();
            this.section = section;
            this.parent = parent;
            Refresh();
        }

        public void Refresh()
        {
            Load();
        }


        //Loading

        private void Load()
        {
            SDName.Text = section.Name;
            SDDescription.Text = section.Description;
        }


        //Modify

        private void ModifySection(object sender, RoutedEventArgs e)
        {
            if (isEditing)
            {
                isEditing = false;
                LockFields();

            }
            else
            {
                isEditing = true;
                UnlockFields();
            }
        }

        private void LockFields()
        {
            SDName.IsReadOnly = true;
            SDDescription.IsReadOnly = true;
            SDModify.Content = "Modifier";
            if(SDName.Text == null || SDName.Text == "")
            {
                MessageBox.Show("Veuillez entrez un nom pour la section\nPLease enter a name for this section");
            }
            else
            {
                section.Name = SDName.Text;
            }
            if(SDDescription.Text == null || SDDescription.Text == "")
            {
                MessageBox.Show("Veuillez entrez une descrition pour la section\nPlease enter a description for this section");
            }
            else
            {
                section.Description = SDDescription.Text;
            }
            Refresh();
            
        }

        private void UnlockFields()
        {
            SDName.IsReadOnly = false;
            SDDescription.IsReadOnly = false;
            SDModify.Content = "Enregistrer";
        }


        //DeleteSection


        private void DeleteSection(object sender,  RoutedEventArgs e)
        {
            var result = MessageBox.Show(
                "Attention, les produits contenus dans cette section seront désormais seulement accessibles dans l'onglet 'Tous les produits' ",
                "Confirmation",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question
             );

            if (result == MessageBoxResult.Yes)
            {
                DataManager.Instance.Sections.SectionList.Remove(section);
                MessageBox.Show("Section supprimé\nSection Deleted");
                Refresh();
                parent.Refresh();
            }
            else
            {
                return;
            }
        }
    }
}
