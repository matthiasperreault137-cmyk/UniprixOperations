using System.Windows;
using System.Windows.Controls;
/*
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
*/
using UniprixOperations.HelperClasses;
using UniprixOperations.PageData.EmployeePageData;
using UniprixOperations.ProductManager;
using UniprixOperations.XmlDataManager;

namespace UniprixOperations.EmployeePages
{
    /// <summary>
    /// Logique d'interaction pour EmployeeAddExpiredProduct.xaml
    /// </summary>
    public partial class EmployeeAddExpiredProduct : Page ,IPageDef
    {
        private EmployeeAddExpiredProductData data;
        private int? SelectedSectionId { get; set; }
        public EmployeeAddExpiredProduct(EmployeeAddExpiredProductData data)
        {
            InitializeComponent();
            this.data = data;
            Refresh();
        }
        private void OpenTasks(Object sender, RoutedEventArgs e)
        {
            Navigator.OpenEmployeeTasks();
        }
        private void OpenMainMenu(Object sender, RoutedEventArgs e)
        {
            Navigator.OpenEmployeeMainMenu();
        }
        private void OpenExpiredProducts(Object sender, RoutedEventArgs e)
        {
            Navigator.OpenEmployeeAddExpiredProduct();
        }




        public void Refresh()
        {
            LoadSectionList();
            EmptyFields();
        }

        //List Loading Functions
        private void LoadSectionList()
        {
            EAEPSectionList.Children.Clear();
            foreach (Section s in DataManager.Instance.Sections.SectionList)
            {
                Button b = AddSectionButton(s);
                EAEPSectionList.Children.Add(b);
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
            b.Style = (Style)FindResource("EmployeeSection");
            return b;
        }
        private void Select(int id, string name)
        {
            if (SelectedSectionId == id)
            {
                SelectedSectionId = null;
                EAEPSelectedSectionDisplay.Text = "Aucune Rangée Sélectionnée";
            }
            else
            {
                SelectedSectionId = id;
                EAEPSelectedSectionDisplay.Text = "Sélectionné : " + name;
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



            string name = EAEPName.Text;
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Veuillez entrer un nom pour le produit expiré.");
                return;
            }





            string CodeUpc;
            if (EAEPCodeUpc.Text != null)
            {
                if (long.TryParse(EAEPCodeUpc.Text, out long x))
                {
                    CodeUpc = EAEPCodeUpc.Text;
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





            if (EAEPDatePicker.SelectedDate == null)
            {
                MessageBox.Show("Veuillez sélectionner une date d'expiration pour le produit expiré.");
                return;
            }
            DateOnly expiryDate = DateOnly.FromDateTime(EAEPDatePicker.SelectedDate.Value);





            int quantity;
            if (EAEPQuantity != null)
            {
                if (int.TryParse(EAEPQuantity.Text, out int x))
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

            bool shelfStatus = EAEPShelfStatus.IsChecked ?? false;

            ExpiredProduct newProduct = new ExpiredProduct(name, CodeUpc, expiryDate, shelfStatus, quantity, sectionId);
            DataManager.Instance.ExpiredProducts.ExpiredProductList.Add(newProduct);
            EmptyFields();
            MessageBox.Show("Produit expiré ajouté avec succès !");
        }
        private void EmptyFields()
        {
            EAEPName.Text = "";
            EAEPCodeUpc.Text = "";
            EAEPDatePicker.SelectedDate = null;
            EAEPQuantity.Text = "";
            EAEPShelfStatus.IsChecked = false;
            SelectedSectionId = null;
            EAEPSelectedSectionDisplay.Text = "Aucune Rangée Sélectionnée";
        }
    }
}
