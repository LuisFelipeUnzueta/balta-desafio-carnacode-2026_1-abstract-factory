using System;
using DesignPatternChallenge.Contracts;

namespace DesignPatternChallenge.Gateways.PagSeguro
{
    public class PagSeguroValidator : ICardValidator
    {
        public bool ValidateCard(string cardNumber)
        {
            if (string.IsNullOrWhiteSpace(cardNumber))
            {
                Console.WriteLine("PagSeguro: Card number cannot be empty");
                return false;
            }

            Console.WriteLine("PagSeguro: Validating card...");

            // PagSeguro specific rule: 16 digits
            bool isValid = cardNumber.Length == 16;

            if (!isValid)
            {
                Console.WriteLine("PagSeguro: Card must have 16 digits");
            }

            return isValid;
        }
    }
}
