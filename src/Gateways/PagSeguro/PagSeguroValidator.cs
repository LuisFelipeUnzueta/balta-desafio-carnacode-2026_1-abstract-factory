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
                Console.WriteLine("PagSeguro: Número do cartão não pode ser vazio");
                return false;
            }

            Console.WriteLine("PagSeguro: Validando cartão...");

            // Regra específica do PagSeguro: 16 dígitos
            bool isValid = cardNumber.Length == 16;

            if (!isValid)
            {
                Console.WriteLine("PagSeguro: Cartão deve ter 16 dígitos");
            }

            return isValid;
        }
    }
}
