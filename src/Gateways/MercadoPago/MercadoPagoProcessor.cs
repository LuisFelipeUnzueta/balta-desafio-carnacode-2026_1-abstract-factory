using System;
using DesignPatternChallenge.Contracts;

namespace DesignPatternChallenge.Gateways.MercadoPago
{
    public class MercadoPagoProcessor : IPaymentProcessor
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

            Console.WriteLine($"MercadoPago: Processing $ {amount:N2}...");

            // Simulates processing and generates a MercadoPago-specific transaction ID
            string transactionId = $"MP-{Guid.NewGuid().ToString().Substring(0, 8).ToUpper()}";

            Console.WriteLine($"MercadoPago: Transaction processed successfully - ID: {transactionId}");

            return transactionId;
        }
    }
}
