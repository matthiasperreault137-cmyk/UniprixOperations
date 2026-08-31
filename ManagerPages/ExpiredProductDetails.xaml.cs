using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Effects;
using UniprixOperations.ProductManager;
using UniprixOperations.XmlDataManager;

namespace UniprixOperations.ManagerPages
{
    /// <summary>
    /// Logique d'interaction pour ExpiredProductDetails.xaml
    /// </summary>
    public partial class ExpiredProductDetails : Page, IPageDef
    {
        private bool isEditing = false;
        private ExpiredProduct expiredProduct;
        private IPageDef parent;
        public ExpiredProductDetails(ExpiredProduct expiredProduct, IPageDef parent)
        {
            InitializeComponent();
            this.expiredProduct = expiredProduct;
            this.parent = parent;
            Refresh();
        }


        public void Refresh()
        {
            Load();
        }


        private void Load()
        {
            EPDName.Text = expiredProduct.Name;
            EPDCodeUpc.Text = expiredProduct.UPCCode;
            EPDExpiryDate.Text = expiredProduct.ExpiryDate.ToString("yyyy-MM-dd");
            EPDQuantity.Text = expiredProduct.Quantity.ToString();
            if (expiredProduct.ShelfStatus == true)
            {
                CheckStatus.Text = "Oui";
                EPDShelfStatus.IsChecked = true;
            }
            else
            {
                CheckStatus.Text = "Non";
                EPDShelfStatus.IsChecked = false;
            }
        }


        //Deletion of the expired product
        private void DeleteProduct(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Produit Supprimé");
            DataManager.Instance.ExpiredProducts.ExpiredProductList.Remove(expiredProduct);
            this.parent.Refresh();
        }

        //Modification of the product

        private void ModifyProduct(object sender, RoutedEventArgs e)
        {
            if (isEditing)
            {
                LockFields();
            }
            else
            {
                UnlockFields();
            }
        }
        private void LockFields()
        {
            EPDName.IsReadOnly = true;
            EPDCodeUpc.IsReadOnly = true;
            EPDExpiryDate.IsReadOnly = true;
            EPDQuantity.IsReadOnly = true;
            EPDShelfStatus.IsEnabled = false;


            if (EPDName.Text != null && EPDName.Text != "")
            {
                expiredProduct.Name = EPDName.Text;
            }
            else
            {
                MessageBox.Show("Le nom ne peut pas être vide");
            }


            if (EPDCodeUpc.Text != null && EPDCodeUpc.Text != "")
            {
                expiredProduct.UPCCode = EPDCodeUpc.Text;
            }
            else
            {
                MessageBox.Show("Entrez un code UPC valide");
            }



            if (DateOnly.TryParse(EPDExpiryDate.Text, out DateOnly expiryDate))
            {
                expiredProduct.ExpiryDate = expiryDate;
            }
            else
            {
                MessageBox.Show("Entrez une date d'expiration valide : yyyy-MM-dd");
            }

            if (int.TryParse(EPDQuantity.Text, out int quantity))
            {
                expiredProduct.Quantity = quantity;
            }
            else
            {
                MessageBox.Show("Entrez une quantité valide.");
            }
            if (EPDShelfStatus.IsChecked == true)
            {
                expiredProduct.ShelfStatus = true;
            } 
            else 
            {
                expiredProduct.ShelfStatus = false;
            }
            isEditing = false;
            EPDModify.Content = "Modifier";
            Load();
            this.parent.Refresh();




        }

        private void UnlockFields()
        {

            EPDModify.Content = "Enregister";
            EPDName.IsReadOnly = false;
            EPDCodeUpc.IsReadOnly = false;
            EPDExpiryDate.IsReadOnly = false;
            EPDQuantity.IsReadOnly = false;
            EPDShelfStatus.IsEnabled = true;

            isEditing = true;
        }

        //Check Events

        private void CheckEvent(object sender, RoutedEventArgs e)
        {
            CheckStatus.Text = "Oui";

        }
        private void UncheckEvent(object sender, RoutedEventArgs e)
        {
            CheckStatus.Text = "Non";
        }
    }
}
