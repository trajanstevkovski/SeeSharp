using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleShopApp.v2._0.Library.Entities
{
    public class Product
    {
        public string VendorName { get; set; }
        public string ProductName { get; set; }
        public int OrderCode { get; set; }
        public int Price { get; set; }

        public Product(string vendorName, string productName, int orderCode, int price)
        {
            this.VendorName = vendorName;
            this.ProductName = productName;
            this.OrderCode = orderCode;
            this.Price = price;
        }
    }
}
