using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleStoreApp
{
    public class OrderedProduct
    {
        public string ProductName { get; set; }
        public string ProductVendor { get; set; }
        public int Quantaty { get; set; }
        public int TotalPrice { get; set; }

        public OrderedProduct(string productName, string productVendor, int quantaty, int price)
        {
            this.ProductName = productName;
            this.ProductVendor = productVendor;
            this.Quantaty = quantaty;
            this.TotalPrice = quantaty * price;
        }

        public void AddExitstingProduct(int quantaty, int price)
        {
            this.Quantaty = Quantaty + quantaty;
            this.TotalPrice = TotalPrice + (quantaty * price);
        }

        public string ProductInfo()
        {
            return $"{ProductVendor} {ProductName} | Quantaty: {Quantaty} Price: {TotalPrice}";
        }
    }
}
