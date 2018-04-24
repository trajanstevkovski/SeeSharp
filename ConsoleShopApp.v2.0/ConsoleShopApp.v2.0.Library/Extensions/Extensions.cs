using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ConsoleShopApp.v2._0.Library.Entities;

namespace ConsoleShopApp.v2._0.Library.Extensions
{
    public static class Extensions
    {
        public static string ShowInventory<T>(this List<T> list)
        {
            string output = "";
            if(list.GetType() == typeof(List<string>))
            {
                int index = 1;
                foreach (var item in list)
                {
                    output += $"\t{index}. {item}\n";
                    index++;
                }
            }
            if(list.GetType() == typeof(List<Product>))
            {
                foreach (Product item in list as List<Product>)
                {
                    output += $"\tOrder code: {item.OrderCode} " +
                        $"Product name: {item.VendorName} {item.ProductName}" +
                        $" Price: {item.Price.SetPriceInMKD()}\n";
                }
            }
            if(list.GetType() == typeof(List<OrderedProduct>)){
                foreach (OrderedProduct item in list as List<OrderedProduct>)
                {
                    output += $"\tId: {item.Id} " +
                        $"Product Name: {item.ProductName} " +
                        $"Quantaty: {item.Quantaty} " +
                        $"Total Price: {item.TotalPrice.SetPriceInMKD()} \n";
                }
            }
            return output;
        }

        public static string SetPriceInMKD(this int price)
        {
            string stringPrice = price.ToString();
            var culture = CultureInfo.GetCultureInfo("mk-MK");
            return string.Format(culture, "{0:n} MKD", price);
        }

        public static int GetTotalPrice(this List<OrderedProduct> list)
        {
            int totalPrice = 0;
            foreach (var item in list)
            {
                totalPrice += item.TotalPrice;
            }
            return totalPrice;
        }

        public static void ShowHistoryOrders(this List<ShippedOrder> list)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            foreach (var item in list)
            {
                Console.WriteLine($"\tOrder id: {item.OrderId} Product count: {item.ProductsCount} Final invoice: {item.Price.SetPriceInMKD()}\n");
            }
            Console.ResetColor();
        }
    }
}
