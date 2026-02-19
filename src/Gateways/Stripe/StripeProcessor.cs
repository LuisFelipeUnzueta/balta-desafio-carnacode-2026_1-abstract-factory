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
                throw new ArgumentException("Amount must be greater than zero", nameof(amount));
            }

            if (string.IsNullOrWhiteSpace(cardNumber))
            {
                throw new ArgumentException("Card number cannot be empty", nameof(cardNumber));
            }

            Console.WriteLine($"Stripe: Processing ${amount:N2}...");

            // Simulates processing and generates a Stripe-specific transaction ID
            string transactionId = $"STRIPE-{Guid.NewGuid().ToString().Substring(0, 8).ToUpper()}";

            Console.WriteLine($"Stripe: Transaction processed successfully - ID: {transactionId}");

            return transactionId;
        }
    }
}
