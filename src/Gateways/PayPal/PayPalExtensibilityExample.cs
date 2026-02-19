using System;
using DesignPatternChallenge.Contracts;

namespace DesignPatternChallenge.Gateways.PayPal
{
    // ═══════════════════════════════════════════════════════════════
    // STEP 1: Implement ICardValidator
    // ═══════════════════════════════════════════════════════════════

    /// <summary>
    /// PayPal specific card validator.
    /// </summary>
    public class PayPalValidator : ICardValidator
    {
        public bool ValidateCard(string cardNumber)
        {
            if (string.IsNullOrWhiteSpace(cardNumber))
            {
                Console.WriteLine("PayPal: Card number cannot be empty");
                return false;
            }

            Console.WriteLine("PayPal: Validating card...");

            // PayPal accepts any 16-digit card
            bool isValid = cardNumber.Length == 16;

            if (!isValid)
            {
                Console.WriteLine("PayPal: Card must have 16 digits");
            }

            return isValid;
        }
    }

    // ═══════════════════════════════════════════════════════════════
    // STEP 2: Implement IPaymentProcessor
    // ═══════════════════════════════════════════════════════════════

    /// <summary>
    /// PayPal specific payment processor.
    /// </summary>
    public class PayPalProcessor : IPaymentProcessor
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

            Console.WriteLine($"PayPal: Processing ${amount:N2} USD...");

            // Generates a PayPal-specific transaction ID
            string transactionId = $"PAYPAL-{Guid.NewGuid().ToString().Substring(0, 10).ToUpper()}";

            Console.WriteLine($"PayPal: Transaction processed successfully - ID: {transactionId}");

            return transactionId;
        }
    }

    // ═══════════════════════════════════════════════════════════════
    // STEP 3: Implement ITransactionLogger
    // ═══════════════════════════════════════════════════════════════

    /// <summary>
    /// PayPal specific logger.
    /// </summary>
    public class PayPalLogger : ITransactionLogger
    {
        public void Log(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                return;
            }

            // PayPal specific log format
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Console.WriteLine($"[PayPal Log] {timestamp} - {message}");
        }
    }

    // ═══════════════════════════════════════════════════════════════
    // STEP 4: Implement IPaymentGatewayFactory (Concrete Factory)
    // ═══════════════════════════════════════════════════════════════

    /// <summary>
    /// Concrete factory for PayPal.
    /// Creates the complete family of PayPal components.
    /// </summary>
    public class PayPalFactory : IPaymentGatewayFactory
    {
        public ICardValidator CreateValidator()
        {
            return new PayPalValidator();
        }

        public IPaymentProcessor CreateProcessor()
        {
            return new PayPalProcessor();
        }

        public ITransactionLogger CreateLogger()
        {
            return new PayPalLogger();
        }
    }
}

// ═════════════════════════════════════════════════════════════════════
// RESULT: ZERO MODIFICATIONS REQUIRED IN EXISTING CODE!
// ═════════════════════════════════════════════════════════════════════
//
// To use:
// 1. Add PayPal to GatewayType enum (optional, can use direct injection)
// 2. Add case to PaymentGatewayFactoryProvider (optional)
// 3. Or use directly: new PaymentService(new PayPalFactory())
//
// ═══════════════════════════════════════════════════════════════════════
