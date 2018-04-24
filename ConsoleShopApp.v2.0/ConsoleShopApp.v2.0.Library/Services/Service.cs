using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ConsoleShopApp.v2._0.Library.Entities;
using ConsoleShopApp.v2._0.Library.Repository;

namespace ConsoleShopApp.v2._0.Library.Services
{
    public class Service : IService
    {
        public bool TrySetUserName(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\tPlease enter a valid Name. Thank you!\n");
                Console.ResetColor();
                return false;
            }
            else if (input.Length < 3)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("\tYour name must contain more than 3 characters or numbers\n");
                Console.ResetColor();
                return false;
            }
            else
            {
                Console.WriteLine("\tYou have successfuly loged in\n");
                return true;
            }
        }

        public bool TrySetStreet(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\tPlease enter a valid street name. Thank you!\n");
                Console.ResetColor();
                return false;
            }
            else
            {
                Console.WriteLine("\tYou have successfuly entered street name\n");
                return true;
            }
        }

        public bool TrySetShippngService(int input)
        {
            switch (input)
            {
                case 1:
                    return true;
                case 2:
                    return true;
                default:
                    return false;
            }
        }

        public bool TrySetPayPallAcount(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }
            var eMailParts = input.Split(new char[] { '@' });
            if(eMailParts.Length != 2)
            {
                return false;
            }
            return true;
        }

        public bool TrySetCreditCard(long input)
        {
            if(input.ToString().Length < 12)
            {
                return false;
            }
            if(input.ToString().Length > 12)
            {
                return false;
            }
            return true;
        }

        public List<string> GetVendorNames()
        {
            return ProductRepository.GetVendorNames();
        }

        public List<Product> GetProductsByVendorName(string input)
        {
            return ProductRepository.GetProductsByVendorName(input);
        }

        public List<Product> GetAllProducts()
        {
            return ProductRepository.GetAllProducts().ToList();
        }

        public List<Product> GetProductsByUserInputVendorName(string input)
        {
            return ProductRepository.GetProductsByVendorName(input);
        }

        public List<Product> GetProductsByUserInputProductName(string input)
        {
            return ProductRepository.GetProductByProductName(input);
        }

        public List<Product> SortByPriceAsc(List<Product> list)
        {
            return list.OrderBy(c => c.Price).ToList();
        }

        public List<Product> SortByPriceDsc(List<Product> list)
        {
            return list.OrderBy(c => c.Price).Reverse().ToList();
        }

        public List<Product> SortByProductNameAsc(List<Product> list)
        {
            return list.OrderBy(c => c.ProductName).ToList();
        }

        public List<Product> SortByProductNameDsc(List<Product> list)
        {
            return list.OrderBy(c => c.ProductName).Reverse().ToList();
        }

        public List<ShippedOrder> FilterOrders(List<ShippedOrder> list, int input)
        {
            List<ShippedOrder> newList = new List<ShippedOrder>();
            if(input == 1)
            {
                newList = list.Where(price => price.Price < 5000).ToList();
            }
            if(input == 2)
            {
                newList = list.Where(price => price.Price > 5000).ToList();
            }
            return newList;
        }

        public bool CheckIsValidOrderCode(List<Product> list, int code)
        {
            bool check = false;
            foreach (var item in list)
            {
                if(item.OrderCode == code)
                {
                    check = true;
                }
            }
            return check;
        }

        public bool CheckSearchResults(List<Product> list)
        {
            if (list.Count != 0)
            {
                return true;
            }
            else return false;
        }

        public bool CheckCartAndUserInfo(ShoppingCart cart)
        {
            if (cart.OrderedProducts.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\tShopping cart empty plese buy some product in order to coninue to payment\n");
                Console.ResetColor();
                return false;
            }
            else if (cart.Payment == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\tNo payment and shipping details entered\n");
                Console.ResetColor();
                return false;
            }
            else return true;
        }

        public OrderedProduct GetOrderedProduct(int code, int quantaty, List<Product> list, ShoppingCart shoppingCart)
        {
            Product product = list.Find(p => p.OrderCode == code);
            string productName = product.VendorName + " " + product.ProductName;
            int index = shoppingCart.OrderedProducts.FindIndex(p => p.ProductName == productName);
            if(index == -1)
            {
                return new OrderedProduct(productName, quantaty, product.Price);
            }
            else
            {
                OrderedProduct item = shoppingCart.OrderedProducts.Find(order => order.ProductName == productName);
                shoppingCart.OrderedProducts.Remove(item);
                return new OrderedProduct(item.ProductName, item.Quantaty + quantaty, product.Price, item.Id);
            }
            
        }

        public List<OrderedProduct> SortShoppingCart(ShoppingCart cart)
        {
            return cart.OrderedProducts.OrderBy(id => id.Id).ToList();
        }

        public void WrongInputMsg()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\tPlease select one of the folowing menu inputs,\n");
            Console.ResetColor();
        }

        public void ShowAvaliblePaymentMethods()
        {
            string output = "";
            int index = 0;
            foreach (Enumerations.PaymentMethod item in Enum.GetValues(typeof(Enumerations.PaymentMethod)))
            {
                if (item == Enumerations.PaymentMethod.Undifined)
                {
                    continue;
                }
                index++;
                output += $"\t{index}. {item} ";
            }
            Console.WriteLine(output);
        }

        public void ShowAvalibleShippingService()
        {
            string output = "";
            int index = 0;
            foreach (Enumerations.ShippingService item in Enum.GetValues(typeof(Enumerations.ShippingService)))
            {
                index++;
                output += $"\t{index}. {item} ";
            }
            Console.WriteLine(output);
        }

        public void ShowAvalibleCitys()
        {
            string output = "";
            int index = 0;
            foreach (Enumerations.City item in Enum.GetValues(typeof(Enumerations.City)))
            {
                if (item == Enumerations.City.Undifined)
                {
                    continue;
                }
                index++;
                output += $"\t{index}. {item} ";
            }
            Console.WriteLine(output);

        }

    }
}
