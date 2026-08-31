using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using UniprixOperations.HelperClasses;
using UniprixOperations.PageData.ManagerPageData;
using UniprixOperations.ProductManager;
using UniprixOperations.XmlDataManager;

namespace UniprixOperations.ManagerPages
{
    /// <summary>
    /// Logique d'interaction pour ManagerAllExpiredProducts.xaml
    /// </summary>
    public partial class ManagerAllExpiredProducts : Page , IPageDef
    {
        private int? SelectedProductId { get; set; }
        private string SearchMethod { get; set; }
        private string SearchText { get; set; }

        private List<ExpiredProduct> ExpiredProducts { get; set; } = new List<ExpiredProduct>();


        private ManagerAllExpiredProductsData data;
        public ManagerAllExpiredProducts(ManagerAllExpiredProductsData data)
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

        public void Refresh()
        {
            LoadProducts();
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
        private void LoadProducts()
        {
            UnloadProducts();
            List<ExpiredProduct> products = Search();

            ExpiredProducts = products;

            foreach (var product in products)
            {
                MAPExpiredProductList.Children.Add(CreateProduct(product));
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
                MAPProductDetailsFrame.Content = new ExpiredProductDetails(product, this);
            }
        }
        private void UnloadProducts()
        {
            MAPExpiredProductList.Children.Clear();
            UnloadProductDetails();

        }

        //Product Details Display

        private void UnloadProductDetails()
        {
            MAPProductDetailsFrame.Content = null;
        }



        //Search Handling
        private void DateChange(object sender, SelectionChangedEventArgs e)
        {
            Refresh();
        }

        private void SearchFilterChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

            ComboBox comboBox = (ComboBox)sender;

            SearchMethod = ((ComboBoxItem)comboBox.SelectedItem).Content.ToString();
            if (SearchMethod == "Date")
            {
                MAPSearchBox.Visibility = Visibility.Collapsed;
                MAPDatePickerPanel.Visibility = Visibility.Visible;
                SearchText = null;
                MAPSearchBox.Text = "";
            }
            else
            {
                MAPSearchBox.Visibility = Visibility.Visible;
                MAPDatePickerPanel.Visibility = Visibility.Collapsed;
                MAPStartDate.SelectedDate = null;
                MAPEndDate.SelectedDate = null;
            }

            if (this.IsLoaded)
            {
                LoadProducts();
            }
        }
        private List<ExpiredProduct> Search()
        {
            List<ExpiredProduct> products = new List<ExpiredProduct>();
            if (SearchMethod == "Nom")
            {
                products = ProductSearch.GetUnsortedSectionProductsName(null, SearchText);

                ProductSearch.SortSectionProductsNearest(products);
            }
            else if (SearchMethod == "Code Upc")
            {
                products = ProductSearch.GetUnsortedSectionProductsUpcCode(null, SearchText);

                ProductSearch.SortSectionProductsNearest(products);
            }
            else if (SearchMethod == "Quantité")
            {
                if (int.TryParse(SearchText, out int quantity))
                {
                    products = ProductSearch.GetUnsortedSectionProductsQuantity(null, int.Parse(SearchText));

                    ProductSearch.SortSectionProductsNearest(products);
                }
            }
            else if(SearchMethod == "Date")
            {
                DateOnly? startDate = MAPStartDate.SelectedDate.HasValue
                    ? DateOnly.FromDateTime(MAPStartDate.SelectedDate.Value)
                    : null;

                DateOnly? endDate = MAPEndDate.SelectedDate.HasValue
                    ? DateOnly.FromDateTime(MAPEndDate.SelectedDate.Value)
                    : null;

                products = ProductSearch.GetUnsortedSectionProductsDateRange(null, startDate, endDate);

                ProductSearch.SortSectionProductsNearest(products);

            }
            else
            {
                products = ProductSearch.GetUnsortedSectionProductsName(null, SearchText);


                ProductSearch.SortSectionProductsNearest(products);

                products.Reverse();
            }
            return products;
        }
        private void SearchTextChanged(Object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            TextBox text = (TextBox)sender;


            SearchText = text.Text;

            if (this.IsLoaded)
            {
                LoadProducts();
            }
        }


        //Printing

        private void PrintProducts(object sender, RoutedEventArgs e)
        {
            Printer.PrintProducts(ExpiredProducts);
        }



        //Delete products

        private void DeleteProducts(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show(
                "Êtes vous sûr de vouloir supprimer tout les produits sélectionnés?\nAre you sure ypu want to delete all the selected Products",
                "",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                foreach (ExpiredProduct product in ExpiredProducts)
                {
                    DataManager.Instance.ExpiredProducts.ExpiredProductList.Remove(product);
                }
            }
        }

    }
}
