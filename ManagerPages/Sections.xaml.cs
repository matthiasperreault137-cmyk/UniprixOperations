using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using UniprixOperations.HelperClasses;
using UniprixOperations.PageData.ManagerPageData;
using UniprixOperations.ProductManager;
using UniprixOperations.XmlDataManager;
using GridLength = System.Windows.GridLength;
using GridUnitType = System.Windows.GridUnitType;

namespace UniprixOperations.ManagerPages
{
    /// <summary>
    /// Logique d'interaction pour Sections.xaml
    /// </summary>
    public partial class Sections : System.Windows.Controls.Page, IPageDef
    {
        private List<ExpiredProduct> expiredProducts;
        private int? SelectedSectionId { get; set; }
        private int? SelectedProductId { get; set; }
        private string SearchMethod { get; set; }
        private string SearchText { get; set; }

        private SectionsData data;
        public Sections(SectionsData data)
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
            LoadSections();
        }

        public void RefreshProducts()
        {
            if (SelectedSectionId != null)
            {
                LoadProducts(SectionSearch.FindSectionById(SelectedSectionId ?? 0));
            }
        }

        //Section List Loading 


        private System.Windows.Controls.Grid CreateSection(Section section)
        {
            var grid = new System.Windows.Controls.Grid
            {
                Height = 80
            };

            grid.ColumnDefinitions.Add(new System.Windows.Controls.ColumnDefinition());
            grid.ColumnDefinitions.Add(new System.Windows.Controls.ColumnDefinition
            {
                Width = new GridLength(0.3, GridUnitType.Star)
            });

            var nearestExpiry = ProductSearch.SearchSectionNearest(section.Id)?.ExpiryDate.ToString("yyyy-MM-dd") ?? "Aucun Produit Expiré";

            var contentPanel = new StackPanel { Orientation = Orientation.Horizontal };
            contentPanel.Children.Add(new TextBlock
            {
                Text = section.Name,
                VerticalAlignment = VerticalAlignment.Center
            });
            contentPanel.Children.Add(new TextBlock
            {
                Text = nearestExpiry,
                VerticalAlignment = VerticalAlignment.Center,
                Foreground = new SolidColorBrush(Color.FromRgb(136, 136, 136)),
                FontSize = 13,
                Margin = new Thickness(12, 0, 0, 0)
            });

            var button = new System.Windows.Controls.Button();
            button.Style = (Style)FindResource("ExpiredProductButton");
            button.Content = contentPanel;
            button.Click += (s, e) => SelectSection(section.Id, section.Name);
            System.Windows.Controls.Grid.SetColumnSpan(button, 2);
            grid.Children.Add(button);

            return grid;
        }
        private void LoadSections()
        {
            SSectionList.Children.Clear();
            UnloadProducts();
            foreach (Section s in DataManager.Instance.Sections.SectionList)
            {
                SSectionList.Children.Add(CreateSection(s));
            }
        }

        private void SelectSection(int id, string name)
        {
            if (SelectedSectionId == id)
            {
                SelectedSectionId = null;
                SSectionDisplay.Text = "Aucune Rangée Sélectionnée";
                UnloadProducts();
            }
            else
            {
                SelectedSectionId = id;
                SSectionDisplay.Text = "Sélectionné : " + name;
                UnloadProducts();
                LoadProducts(SectionSearch.FindSectionById(id));                //Recherche la section dans la lsites des sections a l'aide de son id  pourrasit etre optimisé en éviutant d'envoyer seulement le selected if en parametre mais bref...
            }
        }




        //Product List Display

        private System.Windows.Controls.Grid CreateProduct(ExpiredProduct expiredProduct)
        {
            var grid = new System.Windows.Controls.Grid
            {
                Height = 80
            };

            grid.ColumnDefinitions.Add(new System.Windows.Controls.ColumnDefinition());
            grid.ColumnDefinitions.Add(new System.Windows.Controls.ColumnDefinition
            {
                Width = new GridLength(0.3, GridUnitType.Star)
            });

            var contentPanel = new StackPanel { Orientation = Orientation.Horizontal };
            contentPanel.Children.Add(new TextBlock
            {
                Text = expiredProduct.Name,
                VerticalAlignment = VerticalAlignment.Center
            });
            contentPanel.Children.Add(new TextBlock
            {
                Text = expiredProduct.ExpiryDate.ToString("yyyy-MM-dd"),
                VerticalAlignment = VerticalAlignment.Center,
                Foreground = new SolidColorBrush(Color.FromRgb(136, 136, 136)),
                FontSize = 13,
                Margin = new Thickness(12, 0, 0, 0)
            });

            var button = new System.Windows.Controls.Button();
            button.Style = (Style)FindResource("ExpiredProductButton");
            button.Content = contentPanel;
            button.Click += (s, e) => SelectProduct(expiredProduct);
            System.Windows.Controls.Grid.SetColumnSpan(button, 2);
            grid.Children.Add(button);

            return grid;
        }
        private void LoadProducts(Section section)
        {
            UnloadProducts();
            expiredProducts = Search();

            foreach (var product in expiredProducts)
            {
                SExpiredProductList.Children.Add(CreateProduct(product));
            }

        }
        private void SelectProduct(ExpiredProduct product)
        {
            if (SelectedProductId == product.Id)
            {
                SelectedProductId = null;
                UnloadProductDetails();
            }
            else
            {
                SelectedProductId = product.Id;
                SProductDetailsFrame.Content = new ExpiredProductDetails(product, this);
            }
        }
        private void UnloadProducts()
        {
            SExpiredProductList.Children.Clear();
            UnloadProductDetails();

        }

        //Product Details Display

        private void UnloadProductDetails()
        {
            SProductDetailsFrame.Content = null;
        }



        //Search Handling

        private void DateChange(object sender, SelectionChangedEventArgs e)
        {
            RefreshProducts();
        }

        private void SearchFilterChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

            ComboBox comboBox = (ComboBox)sender;

            SearchMethod = ((ComboBoxItem)comboBox.SelectedItem).Content.ToString();

            if (SearchMethod == "Date")
            {
                SSearchBox.Visibility = Visibility.Collapsed;
                SDatePickerPanel.Visibility = Visibility.Visible;
                SearchText = null;
                SSearchBox.Text = "";
            }
            else
            {
                SSearchBox.Visibility = Visibility.Visible;
                SDatePickerPanel.Visibility = Visibility.Collapsed;
                SStartDate.SelectedDate = null;
                SEndDate.SelectedDate = null;
            }

            if (SelectedSectionId != null)
            {
                LoadProducts(SectionSearch.FindSectionById(SelectedSectionId ?? 0));
            }
        }
        private List<ExpiredProduct> Search()
        {
            List<ExpiredProduct> products = new List<ExpiredProduct>();
            if (SearchMethod == "Nom")
            {
                products = ProductSearch.GetUnsortedSectionProductsName(SelectedSectionId, SearchText);

                ProductSearch.SortSectionProductsNearest(products);
            }
            else if (SearchMethod == "Code Upc")
            {
                products = ProductSearch.GetUnsortedSectionProductsUpcCode(SelectedSectionId, SearchText);

                ProductSearch.SortSectionProductsNearest(products);
            }
            else if (SearchMethod == "Quantité")
            {
                if (int.TryParse(SearchText, out int quantity))
                {
                    products = ProductSearch.GetUnsortedSectionProductsQuantity(SelectedSectionId, int.Parse(SearchText));

                    ProductSearch.SortSectionProductsNearest(products);
                }
            }
            else if (SearchMethod == "Date")
            {
                DateOnly? startDate = SStartDate.SelectedDate.HasValue
                    ? DateOnly.FromDateTime(SStartDate.SelectedDate.Value)
                    : null;

                DateOnly? endDate = SEndDate.SelectedDate.HasValue
                    ? DateOnly.FromDateTime(SEndDate.SelectedDate.Value)
                    : null;

                products = ProductSearch.GetUnsortedSectionProductsDateRange(SelectedSectionId, startDate, endDate);

                ProductSearch.SortSectionProductsNearest(products);
            }
            else
            {
                products = ProductSearch.GetUnsortedSectionProductsName(SelectedSectionId, SearchText);


                ProductSearch.SortSectionProductsNearest(products);

                products.Reverse();
            }
            return products;
        }
        private void SearchTextChanged(Object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            TextBox text = (TextBox)sender;


            SearchText = text.Text;

            LoadProducts(SectionSearch.FindSectionById(SelectedSectionId ?? 0));
        }


        //Printing

        private void PrintProducts(object sender, RoutedEventArgs e)
        {
            Printer.PrintProducts(expiredProducts);
        }


        //Product deletion

        private void DeleteProducts(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show(
                "Êtes vous sûr de vouloir supprimer tout les produits sélectionnés?\nAre you sure ypu want to delete all the selected Products",
                "",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                foreach (ExpiredProduct product in expiredProducts)
                {
                    DataManager.Instance.ExpiredProducts.ExpiredProductList.Remove(product);
                }
            }
        }
    }
}
