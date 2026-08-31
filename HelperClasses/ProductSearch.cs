using UniprixOperations.ProductManager;
using UniprixOperations.XmlDataManager;

namespace UniprixOperations.HelperClasses
{
    public static class ProductSearch
    {
        public static ExpiredProduct? SearchSectionNearest(int sectionId)
        {
            ExpiredProduct? mostRecentProduct = null;

            foreach (ExpiredProduct product in DataManager.Instance.ExpiredProducts.ExpiredProductList)
            {
                if (product.SectionId == sectionId)
                {
                    if (mostRecentProduct == null || product.ExpiryDate < mostRecentProduct.ExpiryDate)
                    {
                        mostRecentProduct = product;
                    }
                }
            }

            return mostRecentProduct;
        }

        public static void SortSectionProductsNearest(List<ExpiredProduct> sectionProducts)
        {
            QuickSortProductsNearest(sectionProducts, 0, sectionProducts.Count - 1);
        }

        public static List<ExpiredProduct> GetUnsortedSectionProductsName(int? sectionId, string searchQuery)
        {
            List<ExpiredProduct> sectionProducts = new List<ExpiredProduct>();
            if (sectionId != null)
            {

                if (searchQuery == null || searchQuery.Trim() == "")
                {
                    foreach (ExpiredProduct product in DataManager.Instance.ExpiredProducts.ExpiredProductList)
                    {
                        if (product.SectionId == sectionId)
                        {
                            sectionProducts.Add(product);
                        }
                    }
                }
                else
                {
                    foreach (ExpiredProduct product in DataManager.Instance.ExpiredProducts.ExpiredProductList)
                    {
                        if (product.SectionId == sectionId && product.Name.Contains(searchQuery))
                        {
                            sectionProducts.Add(product);
                        }
                    }
                }

            }
            else
            {
                if (searchQuery == null || searchQuery.Trim() == "")
                {
                    foreach (ExpiredProduct product in DataManager.Instance.ExpiredProducts.ExpiredProductList)
                    {
                        sectionProducts.Add(product);
                    }
                }
                else
                {
                    foreach (ExpiredProduct product in DataManager.Instance.ExpiredProducts.ExpiredProductList)
                    {
                        if (product.Name.Contains(searchQuery))
                        {
                            sectionProducts.Add(product);
                        }
                    }
                }
            }
            return sectionProducts;
        }

        public static List<ExpiredProduct> GetUnsortedSectionProductsUpcCode(int? sectionId, string upcCode)
        {
            List<ExpiredProduct> sectionProducts = new List<ExpiredProduct>();
            if(sectionId != null)
            {
                if (upcCode == null)
                {
                    foreach (ExpiredProduct product in DataManager.Instance.ExpiredProducts.ExpiredProductList)
                    {
                        if (product.SectionId == sectionId)
                        {
                            sectionProducts.Add(product);
                        }
                    }
                }
                else
                {
                    foreach (ExpiredProduct product in DataManager.Instance.ExpiredProducts.ExpiredProductList)
                    {
                        if (product.SectionId == sectionId && product.UPCCode.ToString().Contains(upcCode))
                        {
                            sectionProducts.Add(product);
                        }
                    }
                }
            }
            else
            {
                if (upcCode == null)
                {
                    foreach (ExpiredProduct product in DataManager.Instance.ExpiredProducts.ExpiredProductList)
                    {
                        sectionProducts.Add(product);
                    }
                }
                else
                {
                    foreach (ExpiredProduct product in DataManager.Instance.ExpiredProducts.ExpiredProductList)
                    {
                        if (product.UPCCode.ToString().Contains(upcCode))
                        {
                            sectionProducts.Add(product);
                        }
                    }
                }
            }
            return sectionProducts;
        }

        public static List<ExpiredProduct> GetUnsortedSectionProductsQuantity(int? sectionId, int? quantity)
        {
            List<ExpiredProduct> sectionProducts = new List<ExpiredProduct>();
            if(sectionId != null)
            {
                if (quantity == null)
                {
                    foreach (ExpiredProduct product in DataManager.Instance.ExpiredProducts.ExpiredProductList)
                    {
                        if (product.SectionId == sectionId)
                        {
                            sectionProducts.Add(product);
                        }
                    }
                }
                else
                {
                    foreach (ExpiredProduct product in DataManager.Instance.ExpiredProducts.ExpiredProductList)
                    {
                        if (product.Quantity == quantity)
                        {
                            sectionProducts.Add(product);
                        }
                    }
                }
            }
            else
            {
                if (quantity == null)
                {
                    foreach (ExpiredProduct product in DataManager.Instance.ExpiredProducts.ExpiredProductList)
                    {
                        sectionProducts.Add(product);
                    }
                }
                else
                {
                    foreach (ExpiredProduct product in DataManager.Instance.ExpiredProducts.ExpiredProductList)
                    {
                        if (product.Quantity == quantity)
                        {
                            sectionProducts.Add(product);
                        }
                    }
                }
            }
            return sectionProducts;
        }

        public static List<ExpiredProduct> GetUnsortedSectionProductsDateRange(int? sectionId, DateOnly? startDate, DateOnly? endDate)
        {
            List<ExpiredProduct> sectionProducts = new List<ExpiredProduct>();
            if (sectionId != null)
            {
                if (startDate == null && endDate == null)
                {
                    foreach (ExpiredProduct product in DataManager.Instance.ExpiredProducts.ExpiredProductList)
                    {
                        if (product.SectionId == sectionId)
                        {
                            sectionProducts.Add(product);
                        }
                    }
                }
                else if(startDate == null)
                {
                    foreach (ExpiredProduct product in DataManager.Instance.ExpiredProducts.ExpiredProductList)
                    {
                        if (product.SectionId == sectionId && product.ExpiryDate <= endDate)
                        {
                            sectionProducts.Add(product);
                        }
                    }
                }
                else if(endDate == null)
                {
                    foreach (ExpiredProduct product in DataManager.Instance.ExpiredProducts.ExpiredProductList)
                    {
                        if (product.SectionId == sectionId && product.ExpiryDate >= startDate)
                        {
                            sectionProducts.Add(product);
                        }
                    }
                }
                else
                {
                    foreach (ExpiredProduct product in DataManager.Instance.ExpiredProducts.ExpiredProductList)
                    {
                        if (product.SectionId == sectionId && product.ExpiryDate >= startDate && product.ExpiryDate <= endDate)
                        {
                            sectionProducts.Add(product);
                        }
                    }
                }
            }
            else
            {
                if (startDate == null && endDate == null)
                {
                    foreach (ExpiredProduct product in DataManager.Instance.ExpiredProducts.ExpiredProductList)
                    {
                        sectionProducts.Add(product);
                    }
                }
                else if (startDate == null)
                {
                    foreach (ExpiredProduct product in DataManager.Instance.ExpiredProducts.ExpiredProductList)
                    {
                        if (product.ExpiryDate <= endDate)
                        {
                            sectionProducts.Add(product);
                        }
                    }
                }
                else if (endDate == null)
                {
                    foreach (ExpiredProduct product in DataManager.Instance.ExpiredProducts.ExpiredProductList)
                    {
                        if (product.ExpiryDate >= startDate)
                        {
                            sectionProducts.Add(product);
                        }
                    }
                }
                else
                {
                    foreach (ExpiredProduct product in DataManager.Instance.ExpiredProducts.ExpiredProductList)
                    {
                        if (product.ExpiryDate >= startDate && product.ExpiryDate <= endDate)
                        {
                            sectionProducts.Add(product);
                        }
                    }
                }
            }
            return sectionProducts;
        }


        //QuickSort implementation for sorting products by nearest expiry date
        private static void QuickSortProductsNearest(List<ExpiredProduct> products, int left, int right)
        {
            if (left >= right)
            {
                return;
            }
            else
            {
                int pivotIndex = HoaresPartition(products, left, right);

                QuickSortProductsNearest(products, left, pivotIndex);
                QuickSortProductsNearest(products, pivotIndex + 1, right);
            }
        }
        private static int MedianOfThree(List<ExpiredProduct> products, int left, int right)
        {
            int mid = (left + right) / 2;
            DateOnly a = products[left].ExpiryDate;
            DateOnly b = products[mid].ExpiryDate;
            DateOnly c = products[right].ExpiryDate;

            if (a < b)
            {
                if (b < c)
                {
                    return mid;
                }
                else if (a < c)
                {
                    return right;
                }
                else
                {
                    return left;
                }
            }
            else
            {
                if (a < c)
                {
                    return left;
                }
                else if (b < c)
                {
                    return right;
                }
                else
                {
                    return mid;
                }
            }
        }
        private static int HoaresPartition(List<ExpiredProduct> products, int left, int right)
        {
            DateOnly pivotValue = products[MedianOfThree(products, left, right)].ExpiryDate;
            int i = left - 1;
            int j = right + 1;

            while (true)
            {
                do
                {
                    i++;
                } while (products[i].ExpiryDate < pivotValue);
                do
                {
                    j--;
                } while (products[j].ExpiryDate > pivotValue);
                if (i >= j)
                {
                    return j;
                }
                var temp = products[i];
                products[i] = products[j];
                products[j] = temp;
            }
        }
    }
}
