using System;
using DesignPatternChallenge.Contracts;

namespace DesignPatternChallenge.Gateways.Stripe
{
    public class StripeProcessor : IPaymentProcessor
    {
        public string ProcessTransaction(decimal amount, string cardNumber)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Valor deve ser maior que zero", nameof(amount));
            }

            if (string.IsNullOrWhiteSpace(cardNumber))
            {
                throw new ArgumentException("Número do cartão não pode ser vazio", nameof(cardNumber));
            }

            Console.WriteLine($"Stripe: Processando ${amount:N2}...");

            // Simula processamento e gera ID de transação específico do Stripe
            string transactionId = $"STRIPE-{Guid.NewGuid().ToString().Substring(0, 8).ToUpper()}";

            Console.WriteLine($"Stripe: Transação processada com sucesso - ID: {transactionId}");

            return transactionId;
        }
    }
}
