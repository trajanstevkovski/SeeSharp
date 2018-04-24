using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleStoreApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, List<Product>> shop = SetShopInventory();
            Console.WriteLine("Welcome to Console Store");
            Console.WriteLine("Enter your Full name");
            string fullName = Console.ReadLine();
            ShoppingCart shopingCart = new ShoppingCart(fullName);
            Console.WriteLine();
            Console.WriteLine($"Welcome {fullName},\n");
            StoreMenu();
            while (true)
            {
                var isValidMainMenuInput = Int32.TryParse(Console.ReadLine(), out int mainMenuInput);
                switch (mainMenuInput)
                {
                    case 1:
                        VendorMenu(VendorList(shop));
                        while (true)
                        {
                            List<string> vendorName = VendorList(shop);
                            var isValidVendorMenuInput = Int32.TryParse(Console.ReadLine(), out int vendorMenuInput);
                            if(vendorMenuInput <= 0 || vendorMenuInput != 9 && vendorMenuInput > vendorName.Count)
                            {
                                Console.BackgroundColor = ConsoleColor.Red;
                                Console.WriteLine("Please Chose action from the folowing Menu, Thank you\n");
                                Console.BackgroundColor = ConsoleColor.Black;
                                VendorMenu(VendorList(shop));
                            }
                            if(vendorMenuInput > 0 && vendorMenuInput <= vendorName.Count)
                            {
                                ProductListByVendorName(vendorName[vendorMenuInput - 1], shop);
                                ProducMenu();
                                while (true)
                                {
                                    var isValidOrderInput = Int32.TryParse(Console.ReadLine(), out int orderInput);
                                    switch (orderInput)
                                    {
                                        case 1:
                                            List<Product> productList = shop[vendorName[vendorMenuInput - 1]];
                                            Console.WriteLine("Enter Order Code");
                                            var isValidOrderCode = Int32.TryParse(Console.ReadLine(), out int orderCode);
                                            if (CheckOrderCode(orderCode, productList))
                                            {
                                                Console.WriteLine("Enter Quantaty");
                                                var isValidQuantaty = Int32.TryParse(Console.ReadLine(), out int quantaty);
                                                shopingCart.OrderedProducts.Add(GetOrderedProduct(productList, quantaty, orderCode, shopingCart));
                                                Console.BackgroundColor = ConsoleColor.DarkBlue;
                                                Console.WriteLine("You have seccessfuly added product to your shoping cart\n");
                                                Console.BackgroundColor = ConsoleColor.Black;
                                                ProductListByVendorName(vendorName[vendorMenuInput - 1], shop);
                                                ProducMenu();
                                                continue;
                                            } else
                                            {
                                                Console.BackgroundColor = ConsoleColor.Red;
                                                Console.WriteLine("Enter valid order code\n");
                                                Console.BackgroundColor = ConsoleColor.Black;
                                                Console.WriteLine();
                                                ProductListByVendorName(vendorName[vendorMenuInput - 1], shop);
                                                ProducMenu();
                                                break;
                                            }
                                        case 9:
                                            // Return to ProductMenu
                                            break;
                                        default:
                                            Console.BackgroundColor = ConsoleColor.Red;
                                            Console.WriteLine("Please Chose action from the folowing Menu, Thank you\n\n");
                                            Console.BackgroundColor = ConsoleColor.Black;
                                            ProductListByVendorName(vendorName[vendorMenuInput - 1], shop);
                                            ProducMenu();
                                            break;
                                    }

                                    // Return to Product Menu
                                    if (orderInput == 9)
                                    {
                                        VendorMenu(VendorList(shop));
                                        break;
                                    }
                                }
                            }

                            if (vendorMenuInput == 9)
                            {
                                break;
                            }
                        }
                        StoreMenu();
                        continue;
                    case 2:
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        shopingCart.CartInfo();
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.WriteLine();
                        StoreMenu();
                        break;
                    case 3:
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        shopingCart.CartInfo();
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.WriteLine();
                        while(true || shopingCart.OrderedProducts.Count == 0)
                        {
                            if (shopingCart.OrderedProducts.Count > 0)
                            {
                                Console.WriteLine("To Remove Item from shopping cart  enter number of row\n");
                                Console.WriteLine("To Main Menu enter 0\n");
                                var isValidRemoveIndex = Int32.TryParse(Console.ReadLine(), out int removeIndex);
                                if(removeIndex == 0)
                                {
                                    StoreMenu();
                                    break;
                                }
                                if(removeIndex > shopingCart.OrderedProducts.Count)
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("There is no item with that index. Try again with existing Index\n");
                                    Console.ForegroundColor = ConsoleColor.Gray;
                                    shopingCart.CartInfo();
                                    break;
                                }else
                                {
                                    OrderedProduct removeProduct = shopingCart.OrderedProducts[removeIndex - 1];
                                    Console.WriteLine(removeProduct);
                                    shopingCart.OrderedProducts.Remove(removeProduct);
                                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                                    shopingCart.CartInfo();
                                    Console.ForegroundColor = ConsoleColor.Gray;
                                    Console.WriteLine("To Remove Item from shopping cart  enter number of row\n");
                                    Console.WriteLine("To Main Menu enter 0\n");
                                    break;
                                }
                            }
                            else
                            {
                                Console.WriteLine("There are no items to be removed\n");
                                StoreMenu();
                                break;
                            }
                        }
                        break;
                    case 4:
                        FinalReceipt(shopingCart);
                        Console.WriteLine();
                        Console.WriteLine("To confirm order enter 1\n");
                        Console.WriteLine("Continue shopping enter 2\n");
                        var isValidConfirm = Int32.TryParse(Console.ReadLine(), out int confirmOrder);
                        if(confirmOrder == 1)
                        {
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("Order Confirmed\n" +
                                "Thank you, Come back again.");
                            Console.ForegroundColor = ConsoleColor.Gray;
                            mainMenuInput = 9;
                            break;
                        } else if(confirmOrder == 2)
                        {
                            StoreMenu();
                        }
                        else
                        {
                            break;
                        }
                        continue;
                    case 9:
                        //ExitShop
                        break;
                    default:
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.WriteLine("Please Chose action from the folowing Menu, Thank you\n");
                        Console.BackgroundColor = ConsoleColor.Black;
                        StoreMenu();
                        break;
                }

                //ExitShop
                if(mainMenuInput == 9)
                {
                    break;
                }
            }
            Console.ForegroundColor = ConsoleColor.Blue;    
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("Thank you for buying from us");
            Console.ReadLine();
        }

        static Dictionary<string, List<Product>> SetShopInventory()
        {
            List<Product> produtsByProductVendor = new List<Product>
            {
                new Product("Nike", "AIR MAX 2017", 102, 1200),
                new Product("Nike", "LEBRON XV", 103, 2100),
                new Product("Nike", "KOBE A.D. 1", 104, 1850),
                new Product("Nike", "NIKE ZOOM KD10", 105, 1300),
                new Product("Nike", "LEBRON SOLDIER XI SFG", 106, 1800),
                new Product("Nike", "NIKE METCON 4", 107, 2200),
                new Product("Nike", "NIKE AIR MAX PRIME SL", 108, 1000),
                new Product("Nike", "W AIR ZOOM PEGASUS 34 SHIELD", 109, 2800),
                new Product("Nike", "NIKE ZOOM TRAIN COMMAND", 110, 1750),
                new Product("Nike", "NIKE REAX 8 TR", 111, 2400),
                new Product("Adidas", "TERREX SWIFT R GTX", 201, 1200),
                new Product("Adidas", "CRAZYTRAIN ELITE M", 202, 1400),
                new Product("Adidas", "TERREX VOYAGER CW C", 203, 2100),
                new Product("Adidas", "CRAZY TIME II", 204, 900),
                new Product("Adidas", "RESPONSE ST M", 205, 1800),
                new Product("Adidas", "PUREBOOST XPOSE", 206, 1200),
                new Product("Adidas", "TERREX TIVID MID CP", 207, 1500),
                new Product("Adidas", "ALPHABOUNCE 1 M", 208, 2800),
                new Product("Adidas", "CRAZY EXPLOSIVE 201", 209, 3100),
                new Product("Adidas", "CRAZYTRAIN PRO 3.0 M", 210, 2000),
                new Product("Under Armour", "VERGE 2.0 MID GTX", 300, 2000),
                new Product("Under Armour", "PERFORMANCE SNEAKERS-UA CURRY 4", 301, 2800),
                new Product("Under Armour", "NEWELL RIDGE LOW", 302, 3100),
                new Product("Under Armour", "SPEEDFORM GEMINI VENT", 303, 4000),
                new Product("Under Armour", "HIGHLIGHT DELTA 2", 304, 2500),
                new Product("Converse", "CHUCK TAYLOR ALL STAR", 400, 1000),
                new Product("Converse", "CHUCK TAYLOR ALL STAR HIGH STREET", 401, 1200),
                new Product("Converse", "CUP", 402, 1200),
                new Product("Converse", "CHUCK II", 403, 900),
                new Product("Converse", "STARKI", 404, 1100),
                new Product("Converse", "STAR PLAYER", 405, 1400),
            };
            Dictionary<string, List<Product>> shop = new Dictionary<string, List<Product>>()
            {
                {
                    "Nike", new List<Product>()
                {
                        new Product("Nike", "AIR MAX 2017", 102, 1200),
                        new Product("Nike", "LEBRON XV", 103, 2100),
                        new Product("Nike", "KOBE A.D. 1", 104, 1850),
                        new Product("Nike", "NIKE ZOOM KD10", 105, 1300),
                        new Product("Nike", "LEBRON SOLDIER XI SFG", 106, 1800),
                        new Product("Nike", "METCON 4", 107, 2200),
                        new Product("Nike", "AIR MAX PRIME SL", 108, 1000),
                        new Product("Nike", "W AIR ZOOM PEGASUS 34 SHIELD", 109, 2800),
                        new Product("Nike", "ZOOM TRAIN COMMAND", 110, 1750),
                        new Product("Nike", "REAX 8 TR", 111, 2400)
                }
                },
                {
                    "Adidas", new List<Product>()
                    {
                        new Product("Adidas", "TERREX SWIFT R GTX", 201, 1200),
                        new Product("Adidas", "CRAZYTRAIN ELITE M", 202, 1400),
                        new Product("Adidas", "TERREX VOYAGER CW C", 203, 2100),
                        new Product("Adidas", "CRAZY TIME II", 204, 900),
                        new Product("Adidas", "RESPONSE ST M", 205, 1800),
                        new Product("Adidas", "PUREBOOST XPOSE", 206, 1200),
                        new Product("Adidas", "TERREX TIVID MID CP", 207, 1500),
                        new Product("Adidas", "ALPHABOUNCE 1 M", 208, 2800),
                        new Product("Adidas", "CRAZY EXPLOSIVE 201", 209, 3100),
                        new Product("Adidas", "CRAZYTRAIN PRO 3.0 M", 210, 2000)
                    }
                },
                {
                    "Under Armour", new List<Product>()
                    {
                        new Product("Under Armour", "VERGE 2.0 MID GTX", 300, 2000),
                        new Product("Under Armour", "PERFORMANCE SNEAKERS-UA CURRY 4", 301, 2800),
                        new Product("Under Armour", "NEWELL RIDGE LOW", 302, 3100),
                        new Product("Under Armour", "SPEEDFORM GEMINI VENT", 303, 4000),
                        new Product("Under Armour", "HIGHLIGHT DELTA 2", 304, 2500)
                    }
                },
                {
                    "Converse", new List<Product>()
                    {
                        new Product("Converse", "CHUCK TAYLOR ALL STAR", 400, 1000),
                        new Product("Converse", "CHUCK TAYLOR ALL STAR HIGH STREET", 401, 1200),
                        new Product("Converse", "CUP", 402, 1200),
                        new Product("Converse", "CHUCK II", 403, 900),
                        new Product("Converse", "STARKI", 404, 1100),
                        new Product("Converse", "STAR PLAYER", 405, 1400)
                    }
                }
            };
            return shop;
        }

        static void StoreMenu()
        {
            Console.WriteLine("Shoping Menu:\n");
            Console.WriteLine("To Browse products enter 1\n" +
                "For Shoping Cart enter 2\n" +
                "To Remove order enter 3\n" +
                "To Confirm order and get Receipt enter 4\n" +
                "to Exit Shop enter 9\n");
        }

        static List<string> VendorList(Dictionary<string, List<Product>> shop)
        {
            List<string> vendorList = new List<string>();
            foreach (var vendor in shop.Keys)
            {
                vendorList.Add(vendor);
            }
            return vendorList;
        }

        static void VendorMenu(List<string> vendors)
        {
            int index = 1;
            Console.WriteLine("Browse vendor menu:\n");
            foreach (var vendor in vendors)
            {
                Console.WriteLine($"{index}. {vendor}");
                index++;
            }
            Console.WriteLine();
            Console.WriteLine("Select Vendor by Entering number in front of Vendor Name\n");
            Console.WriteLine("To get back to Main Menu enter 9\n");

        }

        static void ProductListByVendorName(string vendor, Dictionary<string,List<Product>> shop)
        {
            List<Product> products = shop[vendor];
            foreach (var product in products)
            {
                Console.WriteLine(product.ProductInfo());
            }
        }

        static void ProducMenu()
        {
            Console.WriteLine();
            Console.WriteLine("To Make an Order enter 1\n" +
                "To return to Vendor Menu enter 9\n");
        }

        static bool CheckOrderCode(int code, List<Product> productList)
        {
            bool check = false;
            foreach (var product in productList)
            {
                if(product.OrderCode == code)
                {
                    check = true;
                }
            }
            return check;
            
        }

        static OrderedProduct GetOrderedProduct(List<Product> productList, int quantaty, int code, ShoppingCart cart)
        {
            Product item = productList.Find(product => product.OrderCode == code);
            int index = cart.OrderedProducts.FindIndex(order => order.ProductName == item.ProductName);
            OrderedProduct orderedProduct = cart.OrderedProducts.Find(order => order.ProductName == item.ProductName);
            if (index == -1)
            {
                return new OrderedProduct(item.ProductName, item.ProductVendor, quantaty, item.Price);
            } else
            {
                int quantatyNew = quantaty + orderedProduct.Quantaty;
                cart.OrderedProducts.Remove(orderedProduct);
                return new OrderedProduct(item.ProductName, item.ProductVendor, quantatyNew, item.Price);
            }
        }

        static void FinalReceipt(ShoppingCart cart)
        {
            int totalPrice = 0;
            //totalPrice = cart.OrderedProducts.Sum(price => totalPrice += price.TotalPrice);
            foreach (var product in cart.OrderedProducts)
            {
                totalPrice += product.TotalPrice;
            }
            Console.WriteLine("             =============================================");
            Console.WriteLine($"                            {cart.FullName,10}              ");
            Console.WriteLine("             =============================================");
            Console.WriteLine("{0,55} | {1,5} | {2,8} |", "Product Name", "Qty", "Price");
            Console.WriteLine("----------------------------------------------------------------------------");
            cart.FinalReceipt();
            Console.WriteLine("----------------------------------------------------------------------------");
            Console.WriteLine($"|Total:{totalPrice,67} |\n");

        }

    }
}
