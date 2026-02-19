using System;
using DesignPatternChallenge.Contracts;

namespace DesignPatternChallenge.Services
{
    public class PaymentService
    {
        private readonly IPaymentGatewayFactory _factory;
        public PaymentService(IPaymentGatewayFactory factory)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        /// <summary>
        /// Processes a payment using the components of the configured gateway.
        /// 
        /// Flow:
        /// 1. Create components through the factory
        /// 2. Validate the card
        /// 3. Process the transaction
        /// 4. Log the result
        /// </summary>
        public bool ProcessPayment(decimal amount, string cardNumber)
        {
            // Parameter validation
            if (amount <= 0)
            {
                throw new ArgumentException("Amount must be greater than zero", nameof(amount));
            }

            if (string.IsNullOrWhiteSpace(cardNumber))
            {
                throw new ArgumentException("Card number cannot be empty", nameof(cardNumber));
            }

            try
            {
                // 1. Use the factory to create all necessary components
                // Ensures that all components are from the same family (compatible)
                var validator = _factory.CreateValidator();
                var processor = _factory.CreateProcessor();
                var logger = _factory.CreateLogger();

                // 2. Validate the card
                if (!validator.ValidateCard(cardNumber))
                {
                    logger.Log($"Validation failed for card ending in {cardNumber.Substring(cardNumber.Length - 4)}");
                    return false;
                }

                // 3. Process the transaction
                string transactionId = processor.ProcessTransaction(amount, cardNumber);

                // 4. Log success
                logger.Log($"Transaction {transactionId} processed successfully - Amount: $ {amount:N2}");

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Failed to process payment: {ex.Message}");
                throw;
            }
        }
    }
}
