using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleStoreApp
{
    public class Product
    {
        public string ProductVendor { get; set; }
        public string ProductName { get; set; }
        public int OrderCode { get; set; }
        public int Price { get; set; }

        public Product(string vendorName, string productName, int orderCode, int price)
        {
            this.ProductVendor = vendorName;
            this.ProductName = productName;
            this.OrderCode = orderCode;
            this.Price = price;
        }

        public string ProductInfo()
        {
            //return String.Format("| Order Code: {0,4} | Product Name: {1,5} {2,25} | Price: {3,6} |", OrderCode, ProductVendor, ProductName, Price);
            return $"Order Code: {OrderCode} Product Name: {ProductVendor} {ProductName} Price : {Price}";
        }

    }
}
