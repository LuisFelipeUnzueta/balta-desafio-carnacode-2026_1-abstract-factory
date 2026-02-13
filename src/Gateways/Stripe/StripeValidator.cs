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
                Console.WriteLine("Stripe: Número do cartão não pode ser vazio");
                return false;
            }

            Console.WriteLine("Stripe: Validando cartão...");

            // Regra específica do Stripe: 16 dígitos e deve começar com '4'
            bool isValid = cardNumber.Length == 16 && cardNumber.StartsWith("4");

            if (!isValid)
            {
                if (cardNumber.Length != 16)
                {
                    Console.WriteLine("Stripe: Cartão deve ter 16 dígitos");
                }
                else if (!cardNumber.StartsWith("4"))
                {
                    Console.WriteLine("Stripe: Aceita apenas Visa (começa com 4)");
                }
            }

            return isValid;
        }
    }
}
