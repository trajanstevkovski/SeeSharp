using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleShopApp.v2._0.Library.Entities
{
    public class Payment
    {
        public Enumerations.PaymentMethod PaymentMethod { get; set; }
        private long CreditCardNumber { get; set; }
        public string  UserName { get; set; }

        public Payment(int paymentMethod, string userName)
        {
            SetPaymentMethod(paymentMethod);
            this.UserName = userName;
        }

        public Payment(int paymentMethod, long creditCard)
        {
            SetPaymentMethod(paymentMethod);
            this.CreditCardNumber = creditCard;
        }

        public void SetPaymentMethod(int payment)
        {
            switch (payment)
            {
                case 1:
                    PaymentMethod = Enumerations.PaymentMethod.PayPall;
                    break;
                case 2:
                    PaymentMethod = Enumerations.PaymentMethod.CreditCard;
                    break;
                default:
                    PaymentMethod = Enumerations.PaymentMethod.Undifined;
                    break;
            }
        } 

        public void EditPaymentInfo(string input, int paymentMethod)
        {
            SetPaymentMethod(paymentMethod);
            long validCC;
            if (long.TryParse(input, out validCC))
            {
                this.CreditCardNumber = validCC;
                this.UserName = string.Empty;
            }
            else
            {
                this.UserName = input;
                this.CreditCardNumber = 0;
            }
        }

        public string PaymentInfo()
        {
            string output = "";
            if (CreditCardNumber.ToString().Length != 12)
            {
                output = string.Format($"\tPayment method: {PaymentMethod} \n" +
                    $"\tUsername: {UserName}\n");
            }
            else
            {
                long lastFour = CreditCardNumber % 10000;
                output = string.Format($"\tPayment method: {PaymentMethod} \n" +
                $"\tCredit Card Number: **** - **** - **** - {lastFour}\t");
            }
            return output;
        }


    }
}
