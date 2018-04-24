using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ConsoleShopApp.v2._0.Library.Entities;

namespace ConsoleShopApp.v2._0.Library.Services
{
    public interface IService
    {
        bool TrySetUserName(string input);
        bool TrySetStreet(string input);
        bool TrySetShippngService(int input);
        bool TrySetPayPallAcount(string input);
        bool TrySetCreditCard(long input);

        List<string> GetVendorNames();
        List<Product> GetProductsByVendorName(string input);
        List<Product> GetAllProducts();
        List<Product> GetProductsByUserInputVendorName(string input);
        List<Product> GetProductsByUserInputProductName(string input);
        List<Product> SortByPriceAsc(List<Product> list);
        List<Product> SortByPriceDsc(List<Product> list);
        List<Product> SortByProductNameAsc(List<Product> list);
        List<Product> SortByProductNameDsc(List<Product> list);
        List<ShippedOrder> FilterOrders(List<ShippedOrder> list, int input);

        bool CheckIsValidOrderCode(List<Product> list, int code);
        bool CheckSearchResults(List<Product> list);
        bool CheckCartAndUserInfo(ShoppingCart cart);

        OrderedProduct GetOrderedProduct(int code, int quantaty, List<Product> list, ShoppingCart cart);
        List<OrderedProduct> SortShoppingCart(ShoppingCart cart);

        void WrongInputMsg();
        void ShowAvaliblePaymentMethods();
        void ShowAvalibleShippingService();
        void ShowAvalibleCitys();
    }
}
