using System;
using DesignPatternChallenge.Contracts;

namespace DesignPatternChallenge.Gateways.MercadoPago
{
    public class MercadoPagoValidator : ICardValidator
    {
        public bool ValidateCard(string cardNumber)
        {
            if (string.IsNullOrWhiteSpace(cardNumber))
            {
                Console.WriteLine("MercadoPago: Card number cannot be empty");
                return false;
            }

            Console.WriteLine("MercadoPago: Validating card...");

            // MercadoPago specific rule: 16 digits and must start with '5'
            bool isValid = cardNumber.Length == 16 && cardNumber.StartsWith("5");

            if (!isValid)
            {
                if (cardNumber.Length != 16)
                {
                    Console.WriteLine("MercadoPago: Card must have 16 digits");
                }
                else if (!cardNumber.StartsWith("5"))
                {
                    Console.WriteLine("MercadoPago: Only accepts Mastercard (starts with 5)");
                }
            }

            return isValid;
        }
    }
}
