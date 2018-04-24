using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using ConsoleShopApp.v2._0.Library.Extensions;

namespace ConsoleShopApp.v2._0.Library.Entities
{
    public delegate void PaymentSuccessDelegate();

    public class ShoppingCart
    {
        public string UserName { get; set; }
        public List<OrderedProduct> OrderedProducts { get; set; }
        public ShippingDetails ShippingDetails { get; set; }
        public Payment Payment { get; set; }
        public List<ShippedOrder> HistoryOfOrders { get; set; } = new List<ShippedOrder>();

        private PaymentSuccessDelegate _handler;
        public event PaymentSuccessDelegate PaymentSuccess
        {
            add
            {
                _handler += value;
            }
            remove
            {
                _handler -= value;
            }
        }

        public void OnPaymentSuccess()
        {
            _handler?.Invoke();
        }

        public ShoppingCart()
        {
            this.OrderedProducts = new List<OrderedProduct>();
        }

        public void SetShippingDetails(ShippingDetails shippingDetails)
        {
            this.ShippingDetails = shippingDetails;
        }

        private void FinalReceipt()
        {
            foreach (var product in OrderedProducts)
            {
                Console.WriteLine($"| {product.ProductName,53} | {product.Quantaty,5} | {product.TotalPrice.SetPriceInMKD(),15} |");
            }
        }

        public void SetHistoryOrder(int count)
        {

            //Treba da se napravi ShippedOrder da gi stava vrednostite na mesto
            this.HistoryOfOrders.Add(new ShippedOrder( count , OrderedProducts.GetTotalPrice()));
            this.OrderedProducts.RemoveAll(p => p.TotalPrice > 1);
        }

        public void FinalReceiptCheckOut()
        {
            int totalPrice = 0;
            foreach (var product in OrderedProducts)
            {
                totalPrice += product.TotalPrice;
            }
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("\t\tReceipt from ConsoleShopApp\n");
            Console.WriteLine($"\tShipping Address:  {ShippingDetails.Street} {ShippingDetails.Number}\n" +
                $"\tShipping Service:  {ShippingDetails.ShippingService}\n" +
                $"\tShipping City:  {ShippingDetails.City}\n\n");
            Console.WriteLine(Payment.PaymentInfo()+"\n\n"); 
            Console.WriteLine("             =============================================");
            Console.WriteLine($"                            {this.UserName,10}              ");
            Console.WriteLine("             =============================================");
            Console.WriteLine("{0,55} | {1,5} | {2,15} |", "Product Name", "Qty", "Price");
            Console.WriteLine("-----------------------------------------------------------------------------------");
            FinalReceipt();
            Console.WriteLine("-----------------------------------------------------------------------------------");
            Console.WriteLine($"|Total:{totalPrice.SetPriceInMKD(),74} |\n");
            Console.ResetColor();
        }

    }
}
