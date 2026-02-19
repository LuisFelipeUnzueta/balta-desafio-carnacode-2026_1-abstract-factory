using System;
using DesignPatternChallenge.Contracts;

namespace DesignPatternChallenge.Gateways.PagSeguro
{
    public class PagSeguroProcessor : IPaymentProcessor
    {
        public string ProcessTransaction(decimal amount, string cardNumber)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Amount must be greater than zero", nameof(amount));
            }

            if (string.IsNullOrWhiteSpace(cardNumber))
            {
                throw new ArgumentException("Card number cannot be empty", nameof(cardNumber));
            }

            Console.WriteLine($"PagSeguro: Processing $ {amount:N2}...");

            // Simulates processing and generates a PagSeguro-specific transaction ID
            string transactionId = $"PAGSEG-{Guid.NewGuid().ToString().Substring(0, 8).ToUpper()}";

            Console.WriteLine($"PagSeguro: Transaction processed successfully - ID: {transactionId}");

            return transactionId;
        }
    }
}
