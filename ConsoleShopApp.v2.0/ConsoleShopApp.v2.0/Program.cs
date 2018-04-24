using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ConsoleShopApp.v2._0.Library.Entities;
using ConsoleShopApp.v2._0.Library.Services;
using ConsoleShopApp.v2._0.Library.Extensions;

namespace ConsoleShopApp.v2._0
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.SetWindowSize(120,50);
            MainApp();
        }

        public static void MainApp()
        {
            var service = new Service();
            ShoppingCart shoppingCart = new ShoppingCart();

            Console.WriteLine("\tWelcome to ConsoleShopApp\n");
            Console.WriteLine("\tPlease enter Username in order to continue shopping\n");

            #region UserName ValidationInput
            bool isValid = false;
            do
            {
                string input = Console.ReadLine().Trim();
                isValid = service.TrySetUserName(input);
                if (isValid)
                {
                    shoppingCart.UserName = input;
                    isValid = true;
                }
            } while (!isValid);
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"\tWelcome {shoppingCart.UserName}\n\n");
            Console.ResetColor();
            #endregion

            MainMenu();
            bool exit = true;
            while (exit)
            {
                exit = MenuInteraction(shoppingCart, service);
            }

        }

        public static void MainMenu()
        {
            Console.WriteLine("\tTo browse shop inventory enter 1,\n" +
                "\tTo enter and modify shopping cart enter 2,\n" +
                "\tTo enter payment and shiping details enter 3\n" +
                "\tTo confirm order and continue to payment enter 4\n" +
                "\tTo orders history enter 5\n" +
                "\tExit shop enter 9,\n");
        }

        public static void InventoryShopMenuNavigation()
        {
            Console.WriteLine("\tTo list vendors enter 1,\n" +
                "\tTo list all products enter 2,\n" +
                "\tTo search product list enter 3,\n" +
                "\tBack to main menu enter 9,\n");
        }

        public static void MakeOrderOrSortProducts()
        {
            Console.WriteLine("\tTo make order enter 1\n" +
                "\tTo sort by Price lower first enter 2\n" +
                "\tTo sort by Price higher first enter 3\n" +
                "\tTo sort by Product name ascending enter 4\n" +
                "\tTo sort by Product name descending enter 5\n" +
                "\tTo goback enter 9\n");
        }

        public static bool MenuInteraction(ShoppingCart shoppingCart, Service service)
        {
            bool isValidMenuInput = Int32.TryParse(Console.ReadLine(), out int menuInput);
            switch (menuInput)
            {
                case 1:
                    #region Browse Products
                    bool exit = true;
                    InventoryShopMenuNavigation();
                    while (exit)
                    {
                        bool isValidInventoryInput = Int32.TryParse(Console.ReadLine(), out int inventoryInput);
                        switch (inventoryInput)
                        {
                            case 1:
                                List<string> vendorNames = service.GetVendorNames();
                                Console.WriteLine(vendorNames.ShowInventory());
                                Console.WriteLine("\tTo select vendor enter the number infront of vendor name\n" +
                                    "\tBack to browse menu enter 9\n");
                                bool exitVendorMenu = true;
                                while (exitVendorMenu)
                                {
                                    bool isValidVendorInput = Int32.TryParse(Console.ReadLine(), out int vendorInput);
                                    if(vendorInput > 0 && vendorInput <= vendorNames.Count && vendorInput != 9)
                                    {
                                        List<Product> listofProductsByVendorName = service.GetProductsByVendorName(vendorNames[vendorInput - 1]);
                                        Console.WriteLine(listofProductsByVendorName.ShowInventory());
                                        MakeOrderOrSortProducts();
                                        MakeAnOrder(listofProductsByVendorName, shoppingCart, service);
                                        InventoryShopMenuNavigation();
                                        break;
                                    } 
                                    if(vendorInput <= 0 && vendorInput > vendorNames.Count && vendorInput != 9)
                                    {
                                        service.WrongInputMsg();
                                    }
                                    if (vendorInput == 9)
                                    {
                                        InventoryShopMenuNavigation();
                                        exitVendorMenu = false;
                                    }
                                    else exitVendorMenu = true;
                                }
                                break;
                            case 2:
                                List<Product> allProducts = service.GetAllProducts();
                                Console.WriteLine(allProducts.ShowInventory());
                                MakeOrderOrSortProducts();
                                MakeAnOrder(allProducts, shoppingCart, service);
                                InventoryShopMenuNavigation();
                                break;
                            case 3:
                                Console.WriteLine("\tTo search by vendor name enter 1\n" +
                                    "\tTo search by product name part enter 2\n" +
                                    "\tTo go back enter 9\n");
                                bool isValidSearchInput = Int32.TryParse(Console.ReadLine(), out int searchInput);
                                List<Product> userSearchedList = new List<Product>();
                                switch (searchInput)
                                {
                                    case 1:
                                        while (true)
                                        {
                                            Console.WriteLine("\tSearch products by vendor\n");
                                            string input = Console.ReadLine();
                                            if (string.IsNullOrEmpty(input.Trim()))
                                            {
                                                Console.ForegroundColor = ConsoleColor.Red;
                                                Console.WriteLine("\tPlease enter a valid search input\n");
                                                Console.ResetColor();
                                            }
                                            else
                                            {
                                                if (service.CheckSearchResults(service.GetProductsByUserInputVendorName(input)))
                                                {
                                                    userSearchedList = service.GetProductsByUserInputVendorName(input);
                                                    Console.WriteLine(userSearchedList.ShowInventory());
                                                    MakeOrderOrSortProducts();
                                                    MakeAnOrder(userSearchedList, shoppingCart, service);
                                                    InventoryShopMenuNavigation();
                                                    break;
                                                }
                                                else
                                                {
                                                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                                                    Console.WriteLine("\tNo product matched search criteria!\n");
                                                    Console.ResetColor();
                                                    InventoryShopMenuNavigation();
                                                    break;
                                                }
                                            }
                                        }
                                        break;
                                    case 2:
                                        while (true)
                                        {
                                            Console.WriteLine("\tSearch products by product name\n");
                                            string input = Console.ReadLine();
                                            if (string.IsNullOrEmpty(input.Trim()))
                                            {
                                                Console.ForegroundColor = ConsoleColor.Red;
                                                Console.WriteLine("\tPlease enter a valid search input\n");
                                                Console.ResetColor();
                                            }
                                            else
                                            {
                                                if (service.CheckSearchResults(service.GetProductsByUserInputProductName(input)))
                                                {
                                                    userSearchedList = service.GetProductsByUserInputProductName(input);
                                                    Console.WriteLine(userSearchedList.ShowInventory());
                                                    MakeOrderOrSortProducts();
                                                    MakeAnOrder(userSearchedList, shoppingCart, service);
                                                    InventoryShopMenuNavigation();
                                                    break;
                                                }
                                                else
                                                {
                                                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                                                    Console.WriteLine("\tNo product matched search criteria!\n");
                                                    Console.ResetColor();
                                                    InventoryShopMenuNavigation();
                                                    break;
                                                }
                                            }
                                        }
                                        break;
                                    case 9:
                                        //exit menu;
                                        InventoryShopMenuNavigation();
                                        break;
                                    default:
                                        service.WrongInputMsg();
                                        InventoryShopMenuNavigation();
                                        break;
                                }
                                break;
                            case 9:
                                //Exit menu
                                break;
                            default:
                                service.WrongInputMsg();
                                break;
                        }
                        if(inventoryInput == 9)
                        {
                            MainMenu();
                            exit = false;
                        }else exit = true;

                    }
                    break;
                #endregion
                case 2:
                    #region Shopping Cart
                    if (shoppingCart.OrderedProducts.Count == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("\tShopping Cart is empty!\n");
                        Console.ResetColor();
                        MainMenu();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.WriteLine($"\tShopping cart for: {shoppingCart.UserName}\n\n");
                        Console.WriteLine(service.SortShoppingCart(shoppingCart).ShowInventory());
                        Console.ResetColor();
                        Console.WriteLine("\tTo remove item from shopping cart enter 1\n" +
                            "\tTo go back enter 9\n");
                        bool isValidCartInput = Int32.TryParse(Console.ReadLine(), out int cartInput);
                        if(cartInput == 1 && cartInput != 9)
                        {
                            Console.WriteLine("\tEnter the index number of the product you want to remove\n");
                            bool isValidRemoveIndex = Int32.TryParse(Console.ReadLine(), out int removeIndex);
                            if (removeIndex <= shoppingCart.OrderedProducts.Count && removeIndex > 0)
                            {
                                shoppingCart.OrderedProducts.Remove(shoppingCart.OrderedProducts[removeIndex - 1]);
                                Console.ForegroundColor = ConsoleColor.Blue;
                                Console.WriteLine("\tSuccessfuly removed product\n");
                                Console.ResetColor();
                                MainMenu();
                                break;
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("\tThere is'n Product with that index number\n\tPlease try again\n");
                                Console.ResetColor();
                                MainMenu();
                                break;
                            }
                        }
                        else if(cartInput != 1 && cartInput != 9)
                        {
                            service.WrongInputMsg();
                        }
                        if(cartInput == 9)
                        {
                            MainMenu();
                            break;
                        }
                    }
                    break;
                #endregion
                case 3:
                    #region Shipment And Payment
                    while (true)
                    {
                        if (shoppingCart.OrderedProducts.Count == 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\tYou have no products in your shopping cart\n");
                            Console.ResetColor();
                            break;
                        }
                        if (shoppingCart.ShippingDetails != null)
                        {
                            Console.WriteLine("\tIs this valid shipping information\n");
                            shoppingCart.ShippingDetails.ShippingDetailsInfo();
                            Console.WriteLine("\tEnter 1 for Yes and 2 for No\n");
                            bool isValidInput = Int32.TryParse(Console.ReadLine(), out int input);
                            switch (input)
                            {
                                case 1:
                                    if (shoppingCart.Payment != null)
                                    {
                                        Console.WriteLine("\tIs this payment information valid\n");
                                        Console.WriteLine(shoppingCart.Payment.PaymentInfo());
                                        Console.WriteLine("\tPress 1 for Yes and 2 for No");
                                        bool isValidInfo = Int32.TryParse(Console.ReadLine(), out int info);
                                        if (info == 1)
                                        {
                                            break;
                                        }
                                        else if (info == 2)
                                        {
                                            PaymentDetails(shoppingCart, service, input);
                                            break;
                                        }
                                        else
                                        {
                                            service.WrongInputMsg();
                                            break;
                                        }
                                    }else
                                    {
                                        PaymentDetails(shoppingCart, service, 0);
                                    }
                                    break;
                                case 2:
                                    ShippingDetails(shoppingCart, service, input);
                                    break;
                                default:
                                    service.WrongInputMsg();
                                    break;
                            }

                        }
                        else
                        {
                            Console.WriteLine("\tTo proced to payment enter shipping details\n");
                            ShippingDetails(shoppingCart, service, 0);
                            if (shoppingCart.Payment == null)
                            {
                                PaymentDetails(shoppingCart, service, 0);
                            }
                        }
                            break;
                    }
                    MainMenu();
                    break;
                    #endregion
                case 4:
                    #region Confirm order and Payment
                    if (service.CheckCartAndUserInfo(shoppingCart))
                    {
                        shoppingCart.FinalReceiptCheckOut();
                        Console.WriteLine("\tTo confirm order enter 1 to go back enter 2\n");
                        bool isValidInput = Int32.TryParse(Console.ReadLine(), out int input);
                        if (input == 1)
                        {
                            int count = shoppingCart.OrderedProducts.Count;
                            OrderedProduct.ResetId();
                            shoppingCart.SetHistoryOrder(count);
                            Console.WriteLine("\tProcesing...");
                            System.Threading.Thread.Sleep(3000);
                            shoppingCart.PaymentSuccess += PaymentDone;
                            shoppingCart.OnPaymentSuccess();
                            shoppingCart.PaymentSuccess -= PaymentDone;
                            Console.WriteLine("\tProcesing...");
                            System.Threading.Thread.Sleep(3000);
                            shoppingCart.PaymentSuccess += ShippingStarted;
                            shoppingCart.OnPaymentSuccess();
                            shoppingCart.PaymentSuccess -= ShippingStarted;
                            MainMenu();
                            break;
                        }
                        else if (input == 2)
                        {
                            MainMenu();
                            break;
                        }
                        else
                        {
                            service.WrongInputMsg();
                            MainMenu();
                            break;
                        }
                    }
                    else
                    {
                        MainMenu();
                    }
                    break;
                    #endregion
                case 5:
                    #region Orders History
                    if(shoppingCart.HistoryOfOrders.Count == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkMagenta;
                        Console.WriteLine("\tOrder history is empty.\n");
                        Console.ResetColor();
                        MainMenu();
                        break;
                    }
                    Console.WriteLine("\tTo list orders under 5.000,00 MKD enter 1\n");
                    Console.WriteLine("\tTo list orders over 5.000,00 MKD enter 2\n");
                    Console.WriteLine("\tTo go back enter 9\n");
                    bool isValidSortInput = Int32.TryParse(Console.ReadLine(), out int sortInput);
                    if(sortInput != 1 && sortInput != 2 && sortInput != 9)
                    {
                        service.WrongInputMsg();
                    }
                    else if(sortInput == 9)
                    {
                        MainMenu();
                        break;
                    }
                    else if(sortInput == 1)
                    {
                        service.FilterOrders(shoppingCart.HistoryOfOrders, sortInput).ShowHistoryOrders();
                        MainMenu();
                        break;
                    } else
                    {
                        service.FilterOrders(shoppingCart.HistoryOfOrders, sortInput).ShowHistoryOrders();
                        MainMenu();
                        break;
                    }
                    break;
                #endregion
                case 9:
                    // exit app;
                    break;
                default:
                    service.WrongInputMsg();
                    break;
            }
            if(menuInput == 9)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\tThank your for visiting our ConsoleShopApp.\n" +
                    "\tWe hope to see you back soon.\n");
                return false;
            }
            return true;
        }

        public static void MakeAnOrder(List<Product> listofProductsByVendorName, ShoppingCart shoppingCart, Service service)
        {
            bool exitSortOrderMenu = true;
            while (exitSortOrderMenu)
            {
                bool isValidSortOrderInput = Int32.TryParse(Console.ReadLine(), out int sortOrderInput);
                switch (sortOrderInput)
                {
                    case 1:
                        Console.WriteLine("\tEnter order code:\n");
                        bool isValidOrderCode = Int32.TryParse(Console.ReadLine(), out int orderCode);
                        if (service.CheckIsValidOrderCode(listofProductsByVendorName, orderCode))
                        {
                            Console.WriteLine("\tEnter quantaty:\n");
                            bool isValidQuantatyInput = Int32.TryParse(Console.ReadLine(), out int quantatyInput);
                            if(quantatyInput < 1)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("\tEntered quantaty must not be less that one pair\n");
                                Console.ResetColor();
                                break;
                            }
                            else
                            {
                                shoppingCart.OrderedProducts.Add(service.GetOrderedProduct(orderCode, quantatyInput, listofProductsByVendorName, shoppingCart));
                                Console.ForegroundColor = ConsoleColor.Blue;
                                Console.WriteLine("\tProduct added to shopping cart. Thank you for buying from us!\n");
                                Console.ResetColor();
                                Console.WriteLine(listofProductsByVendorName.ShowInventory());
                                MakeOrderOrSortProducts();
                                break;
                            }
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\tInvalid order code. Please select existing order code. Thank you!\n");
                            Console.ResetColor();
                            Console.WriteLine(listofProductsByVendorName.ShowInventory());
                            MakeOrderOrSortProducts();
                        }
                        break;
                    case 2:
                        listofProductsByVendorName = service.SortByPriceAsc(listofProductsByVendorName);
                        Console.WriteLine(service.SortByPriceAsc(listofProductsByVendorName).ShowInventory());
                        MakeOrderOrSortProducts();
                        break;
                    case 3:
                        listofProductsByVendorName = service.SortByPriceDsc(listofProductsByVendorName);
                        Console.WriteLine(service.SortByPriceDsc(listofProductsByVendorName).ShowInventory());
                        MakeOrderOrSortProducts();
                        break;
                    case 4:
                        listofProductsByVendorName = service.SortByProductNameAsc(listofProductsByVendorName);
                        Console.WriteLine(service.SortByProductNameAsc(listofProductsByVendorName).ShowInventory());
                        MakeOrderOrSortProducts();
                        break;
                    case 5:                       
                        listofProductsByVendorName = service.SortByProductNameDsc(listofProductsByVendorName);
                        Console.WriteLine(service.SortByProductNameDsc(listofProductsByVendorName).ShowInventory());
                        MakeOrderOrSortProducts();
                        break;
                    case 9:
                        //exit menu
                        break;
                    default:
                        service.WrongInputMsg();
                        break;
                }

                if (sortOrderInput == 9)
                {
                    exitSortOrderMenu = false;
                }
                else exitSortOrderMenu = true;
            }
        }

        public static void ShippingDetails(ShoppingCart shoppingCart, Service service, int input)
        {
            Console.WriteLine("\tAt the moment we only ship products in this city's\n");
            bool exit = true;
            int city = 0;
            while (exit)
            {
                service.ShowAvalibleCitys();
                Console.WriteLine("\tEnter number to select city\n");
                bool isValidCity = Int32.TryParse(Console.ReadLine(), out city);
                if (city > 0 && city <= 4)
                {
                    exit = false;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"\tInput is not valid! Please chose one of the following city's.\n");
                    Console.ResetColor();
                    exit = true;
                }
            }
            int shippingService = 0;
            bool isValidShippingService = true;
            while (isValidShippingService)
            {
                Console.WriteLine("\tSelect shipping service");
                service.ShowAvalibleShippingService();
                bool shipService = Int32.TryParse(Console.ReadLine(), out int trySetShippingService);
                if (service.TrySetShippngService(trySetShippingService))
                {
                    shippingService = trySetShippingService;
                    isValidShippingService = false;
                }
                else
                {
                    isValidShippingService = true;
                    service.WrongInputMsg();
                }
            }
            string street = "";
            bool isValidStreet = true;
            while (isValidStreet)
            {
                Console.WriteLine("\tEnter street:\n");
                string streatInput = Console.ReadLine();
                if (service.TrySetStreet(streatInput.Trim()))
                {
                    street = streatInput;
                    isValidStreet = false;
                }
                else isValidStreet = true;
            }
            
            string streetNumber;
            while (true)
            {
                Console.WriteLine("\tEnter street number:\n");
                streetNumber = Console.ReadLine();
                if (!string.IsNullOrEmpty(streetNumber))
                {
                    break;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\tEnter valid street number\n");
                    Console.ResetColor();
                    continue;
                }
            }
            if(input == 0)
            {
                shoppingCart.SetShippingDetails(new ShippingDetails(street, streetNumber, city, shippingService));
            } else if(input == 2)
            {
                shoppingCart.ShippingDetails.Number = streetNumber;
                shoppingCart.ShippingDetails.Street = street;
                shoppingCart.ShippingDetails.City = (Enumerations.City)city;
                shoppingCart.ShippingDetails.ShippingService = (Enumerations.ShippingService)shippingService;
            }
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\tShipping details entered seccessfuly!\n");
            Console.ResetColor();
        }

        public static void PaymentDetails(ShoppingCart shoppingCart, Service service, int input)
        {
            Console.WriteLine("\tPlease Enter Payment details\n");
            bool paymentMethodValid = true;
            int methodOfPaying = 0;
            while (paymentMethodValid)
            {
                Console.WriteLine("\tSelect Payment method\n");
                service.ShowAvaliblePaymentMethods();
                bool IsValidPaymentMethod = Int32.TryParse(Console.ReadLine(), out int paymentMethod);
                if (paymentMethod == 1 || paymentMethod == 2)
                {
                    methodOfPaying = paymentMethod;
                    paymentMethodValid = false;
                }
                else paymentMethodValid = true;
            }
            
            if(methodOfPaying != 0)
            {
                if(methodOfPaying == 1)
                {
                    bool validPPAcount = true;
                    while (validPPAcount)
                    {
                        Console.WriteLine("\tPlease enter paypall acount\n");
                        Console.WriteLine("\tIn this order");
                        Console.WriteLine("\texample@example.com\n");
                        string ppAcount = Console.ReadLine();
                        if (!service.TrySetPayPallAcount(ppAcount))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\tPlease enter valid PayPall acount\n");
                            Console.ResetColor();
                            validPPAcount = true;
                        }
                        else if (shoppingCart.Payment != null)
                        {
                            shoppingCart.Payment.EditPaymentInfo(ppAcount, methodOfPaying);
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("\tSuccessfuly edited payment info\n");
                            Console.ResetColor();
                            validPPAcount = true;
                        }
                        else
                        {
                            shoppingCart.Payment = new Payment(methodOfPaying, ppAcount);
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("\tSuccessfuly entered payment info\n");
                            Console.ResetColor();
                            break;
                        }
                    }
                }
                if (methodOfPaying == 2)
                {
                    bool validCC = true;
                    while (validCC)
                    {
                        Console.WriteLine("\tPlease enter creditcard number\n");
                        Console.WriteLine("\tExample: 123456789012, must contain 12 characters\n");
                        bool isValidCreditCard = long.TryParse(Console.ReadLine(), out long creditCard);
                        if (!isValidCreditCard)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\tPlease enter valid creditcard number\n" +
                                "\tExample: 123456789012, must contain 12 characters\n");
                            Console.ResetColor();
                            validCC = true;
                        }
                        else if (!service.TrySetCreditCard(creditCard))
                        {
                            Console.WriteLine("\tPlease enter valid creditcard number\n" +
                                "\tExample: 123456789012, must contain 12 characters\n");
                            validCC = true;
                        }
                        else if (shoppingCart.Payment != null)
                        {
                            shoppingCart.Payment.EditPaymentInfo(creditCard.ToString(), methodOfPaying);
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("\tSuccessfuly edited payment info\n");
                            Console.ResetColor();
                            validCC = false;
                        }
                        else
                        {
                            shoppingCart.Payment = new Payment(methodOfPaying, creditCard);
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("\tSuccessfuly entered payment info\n");
                            Console.ResetColor();
                            validCC = false;
                        }
                    }
                }
            }
            else
            {
                service.WrongInputMsg();
            }
        }



        static void PaymentDone()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\tPayment has been successfull\n");
            Console.ResetColor();
        }

        static void ShippingStarted()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"\tYour order has been Shipped. Approximately 7 days from now ({DateTime.Now.AddDays(7)}) will be delevered\n");
            Console.ResetColor();
        }
        
    }

}
