using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleShopApp.v2._0.Library.Entities
{
    public class OrderedProduct
    {
        public int Id { get; private set; }
        public string ProductName { get; set; }
        public int Quantaty { get; set; }
        public int TotalPrice { get; }

        //private static int orderedProductId;
        private static int _id = 1;
        public OrderedProduct(string productName, int quantaty, int price)
        {
            this.ProductName = productName;
            this.Quantaty = quantaty;
            this.TotalPrice = SetTotalPrice(price, quantaty);
            //this.Id = Interlocked.Increment(ref orderedProductId);
            this.Id = _id;
            _id++;
        }

        public OrderedProduct(string productName, int quantaty, int price, int id)
        {
            this.ProductName = productName;
            this.Quantaty = quantaty;
            this.TotalPrice = SetTotalPrice(price, quantaty);
            this.Id = id;
        }

        private int SetTotalPrice(int price, int quantaty)
        {
            return price * quantaty;
        }

        public static void ResetId()
        {
            _id = 1;
        }

    }
}
