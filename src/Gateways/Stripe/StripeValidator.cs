using System;
using DesignPatternChallenge.Contracts;

namespace DesignPatternChallenge.Gateways.Stripe
{
    public class StripeValidator : ICardValidator
    {
        public bool ValidateCard(string cardNumber)
        {
            if (string.IsNullOrWhiteSpace(cardNumber))
            {
                Console.WriteLine("Stripe: Card number cannot be empty");
                return false;
            }

            Console.WriteLine("Stripe: Validating card...");

            // Stripe specific rule: 16 digits and must start with '4'
            bool isValid = cardNumber.Length == 16 && cardNumber.StartsWith("4");

            if (!isValid)
            {
                if (cardNumber.Length != 16)
                {
                    Console.WriteLine("Stripe: Card must have 16 digits");
                }
                else if (!cardNumber.StartsWith("4"))
                {
                    Console.WriteLine("Stripe: Only accepts Visa (starts with 4)");
                }
            }

            return isValid;
        }
    }
}
