using System.Windows;
using System.Windows.Controls;
using UniprixOperations.HelperClasses;
using UniprixOperations.PageData.ManagerPageData;
using UniprixOperations.ProductManager;
using UniprixOperations.XmlDataManager;

namespace UniprixOperations.ManagerPages
{
    /// <summary>
    /// Logique d'interaction pour ManagerAddExpiredProduct.xaml
    /// </summary>
    public partial class ManagerAddExpiredProduct : Page, IPageDef
    {
        private ManagerAddExpiredProductData data;

        public int? SelectedSectionId { get; set; }
        public ManagerAddExpiredProduct(ManagerAddExpiredProductData data)
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


        public void Refresh()
        {
            LoadSectionList();
            EmptyFields();
        }

        //List Loading Functions
        private void LoadSectionList()
        {
            MAEPSectionList.Children.Clear();
            foreach (Section s in DataManager.Instance.Sections.SectionList)
            {
                Button b = AddSectionButton(s);
                MAEPSectionList.Children.Add(b);
                Console.WriteLine("New section button added");
            }
        }
        private Button AddSectionButton(Section section)
        {
            Button b = new Button
            {
                Content = section.Name,
                Height = 90
            };
            b.Click += (s, e) => Select(section.Id, section.Name);
            b.Style = (Style)FindResource("ManagerSection");
            return b;
        }
        private void Select(int id, string name)
        {
            if (SelectedSectionId == id)
            {
                SelectedSectionId = null;
                MAEPSelectedSectionDisplay.Text = "Aucune Rangée Sélectionnée";
            }
            else
            {
                SelectedSectionId = id;
                MAEPSelectedSectionDisplay.Text = "Sélectionné : " + name;
            }
        }


        //Product Adding Function

        private void AddProduct(Object sender, RoutedEventArgs e)
        {
            if (SelectedSectionId == null)
            {
                MessageBox.Show("Veuillez sélectionner une section pour le produit expiré.");
                return;
            }
            int sectionId = SelectedSectionId.Value;



            string name = MAEPName.Text;
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Veuillez entrer un nom pour le produit expiré.");
                return;
            }





            string CodeUpc;
            if (MAEPCodeUpc.Text != null)
            {
                if (long.TryParse(MAEPCodeUpc.Text, out long x))
                {
                   CodeUpc = MAEPCodeUpc.Text;
                }
                else
                {
                    MessageBox.Show("Veuillez entrer un code UPC valide pour le produit expiré.");
                    return;
                }
            }
            else
            {
                MessageBox.Show("Veuillez entrer un code UPC pour le produit expiré.");
                return;
            }





            if (MAEPDatePicker.SelectedDate == null)
            {
                MessageBox.Show("Veuillez sélectionner une date d'expiration pour le produit expiré.");
                return;
            }
            DateOnly expiryDate = DateOnly.FromDateTime(MAEPDatePicker.SelectedDate.Value);





            int quantity;
            if (MAEPQuantity != null)
            {
                if (int.TryParse(MAEPQuantity.Text, out int x))
                {
                    if (x >= 0)
                    {
                        quantity = x;
                    }
                    else
                    {
                        MessageBox.Show("Veuillez entrer une quantité valide pour le produit expiré.");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Veuillez entrer une quantité valide pour le produit expiré.");
                    return;
                }
            }
            else
            {
                MessageBox.Show("Veuillez entrer une quantité pour le produit expiré.");
                return;
            }

            bool shelfStatus = MAEPShelfStatus.IsChecked ?? false;

            ExpiredProduct newProduct = new ExpiredProduct(name, CodeUpc, expiryDate, shelfStatus, quantity, sectionId);
            DataManager.Instance.ExpiredProducts.ExpiredProductList.Add(newProduct);
            EmptyFields();
            MessageBox.Show("Produit expiré ajouté avec succès !");
        }
        private void EmptyFields()
        {
            MAEPName.Text = "";
            MAEPCodeUpc.Text = "";
            MAEPDatePicker.SelectedDate = null;
            MAEPQuantity.Text = "";
            MAEPShelfStatus.IsChecked = false;
            SelectedSectionId = null;
            MAEPSelectedSectionDisplay.Text = "Aucune Rangée Sélectionnée";
        }
    }
}