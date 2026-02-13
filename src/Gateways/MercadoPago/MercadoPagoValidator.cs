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
                Console.WriteLine("MercadoPago: Número do cartão não pode ser vazio");
                return false;
            }

            Console.WriteLine("MercadoPago: Validando cartão...");

            // Regra específica do MercadoPago: 16 dígitos e deve começar com '5'
            bool isValid = cardNumber.Length == 16 && cardNumber.StartsWith("5");

            if (!isValid)
            {
                if (cardNumber.Length != 16)
                {
                    Console.WriteLine("MercadoPago: Cartão deve ter 16 dígitos");
                }
                else if (!cardNumber.StartsWith("5"))
                {
                    Console.WriteLine("MercadoPago: Aceita apenas Mastercard (começa com 5)");
                }
            }

            return isValid;
        }
    }
}
