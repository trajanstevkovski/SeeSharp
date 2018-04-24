using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleStoreApp
{
    public class ShoppingCart
    {
        public string FullName { get; set; }
        public List<OrderedProduct> OrderedProducts { get; set; }

        public ShoppingCart(string fullName)
        {
            this.FullName = fullName;
            this.OrderedProducts = new List<OrderedProduct>();
        }

        public void CartInfo()
        {
            int index = 1;
            Console.WriteLine();
            Console.WriteLine($"Username: {FullName}\n");
            if(OrderedProducts.Count != 0)
            {
                foreach (var product in OrderedProducts)
                {
                    Console.WriteLine($"{index}: " + product.ProductInfo());
                    index++;
                }
            }
            else Console.WriteLine("Shoping cart empty");
            
        }

        public void FinalReceipt()
        {
            foreach (var product in OrderedProducts)
            {
                Console.WriteLine($"| {product.ProductVendor,15} | {product.ProductName,35} | {product.Quantaty,5} | {product.TotalPrice,8} |");
            }
        }
    }
}
