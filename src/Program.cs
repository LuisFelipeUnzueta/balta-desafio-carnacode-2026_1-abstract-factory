using System;
using DesignPatternChallenge.Contracts;
using DesignPatternChallenge.Enums;
using DesignPatternChallenge.Factories;
using DesignPatternChallenge.Services;
using DesignPatternChallenge.Gateways.PagSeguro;
using DesignPatternChallenge.Gateways.MercadoPago;
using DesignPatternChallenge.Gateways.Stripe;

namespace DesignPatternChallenge
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("╔══════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║        PAYMENT SYSTEM - ABSTRACT FACTORY PATTERN             ║");
            Console.WriteLine("║              CarnaCode 2026 - Challenge 01                   ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════════╝");
            Console.WriteLine();

            // ═══════════════════════════════════════════════════════════════
            // EXAMPLE 1: Using the Factory Provider (recommended approach)
            // ═══════════════════════════════════════════════════════════════
            Console.WriteLine("═══════════════════════════════════════════════════════════════");
            Console.WriteLine("  EXAMPLE 1: Using Factory Provider with Enum (Type-Safe)");
            Console.WriteLine("═══════════════════════════════════════════════════════════════");
            Console.WriteLine();

            DemonstratePayment(GatewayType.PagSeguro, 150.00m, "1234567890123456");
            Console.WriteLine();

            DemonstratePayment(GatewayType.MercadoPago, 200.00m, "5234567890123456");
            Console.WriteLine();

            DemonstratePayment(GatewayType.Stripe, 350.00m, "4234567890123456");
            Console.WriteLine();

            // ═══════════════════════════════════════════════════════════════
            // EXAMPLE 2: Direct Factory Injection (useful for DI Containers)
            // ═══════════════════════════════════════════════════════════════
            Console.WriteLine("═══════════════════════════════════════════════════════════════");
            Console.WriteLine("  EXAMPLE 2: Direct Factory Injection (DI Container Style)");
            Console.WriteLine("═══════════════════════════════════════════════════════════════");
            Console.WriteLine();

            DemonstrateDirectInjection();
            Console.WriteLine();

            // ═══════════════════════════════════════════════════════════════
            // EXAMPLE 3: Error Handling - Validation Failure
            // ═══════════════════════════════════════════════════════════════
            Console.WriteLine("═══════════════════════════════════════════════════════════════");
            Console.WriteLine("  EXAMPLE 3: Validation Failure Demonstration");
            Console.WriteLine("═══════════════════════════════════════════════════════════════");
            Console.WriteLine();

            DemonstrateValidationFailure();
            Console.WriteLine();

            // ═══════════════════════════════════════════════════════════════
            // EXAMPLE 4: Extensibility - How to add a new gateway
            // ═══════════════════════════════════════════════════════════════
            Console.WriteLine("═══════════════════════════════════════════════════════════════");
            Console.WriteLine("  EXTENSIBILITY: To add a new gateway (e.g., PayPal)");
            Console.WriteLine("═══════════════════════════════════════════════════════════════");
            Console.WriteLine();
            Console.WriteLine("  1. Create PayPalValidator : ICardValidator");
            Console.WriteLine("  2. Create PayPalProcessor : IPaymentProcessor");
            Console.WriteLine("  3. Create PayPalLogger : ITransactionLogger");
            Console.WriteLine("  4. Create PayPalFactory : IPaymentGatewayFactory");
            Console.WriteLine("  5. Add PayPal to GatewayType enum");
            Console.WriteLine("  6. Add case to PaymentGatewayFactoryProvider");
            Console.WriteLine();
            Console.WriteLine("  ✅ NO MODIFICATION required in PaymentService!");
            Console.WriteLine("  ✅ Existing code continues to work!");
            Console.WriteLine();
        }

        /// <summary>
        /// Demonstrates payment processing using the Factory Provider.
        /// This is the recommended approach for production use.
        /// </summary>
        static void DemonstratePayment(GatewayType gatewayType, decimal amount, string cardNumber)
        {
            Console.WriteLine($"→ Processing with {gatewayType}:");
            Console.WriteLine(new string('─', 63));

            // Get the appropriate factory through the provider
            IPaymentGatewayFactory factory = PaymentGatewayFactoryProvider.GetFactory(gatewayType);

            // Inject the factory into the service
            var paymentService = new PaymentService(factory);

            // Process the payment
            bool success = paymentService.ProcessPayment(amount, cardNumber);

            Console.WriteLine(new string('─', 63));
            Console.WriteLine($"✓ Status: {(success ? "SUCCESS" : "FAILURE")}");
        }

        /// <summary>
        /// Demonstrates direct factory injection.
        /// Useful when using DI Containers like Microsoft.Extensions.DependencyInjection
        /// </summary>
        static void DemonstrateDirectInjection()
        {
            Console.WriteLine("→ Direct Injection Demonstration:");
            Console.WriteLine(new string('─', 63));

            // In a real DI Container, you would register it like this:
            // services.AddScoped<IPaymentGatewayFactory, PagSeguroFactory>();
            // services.AddScoped<PaymentService>();

            // Here we simulate manual injection
            IPaymentGatewayFactory factory = new StripeFactory();
            var paymentService = new PaymentService(factory);

            bool success = paymentService.ProcessPayment(99.90m, "4111111111111111");

            Console.WriteLine(new string('─', 63));
            Console.WriteLine($"✓ Status: {(success ? "SUCCESS" : "FAILURE")}");
        }

        /// <summary>
        /// Demonstrates how the system handles failed validations.
        /// </summary>
        static void DemonstrateValidationFailure()
        {
            Console.WriteLine("→ Attempting to process with Visa card on MercadoPago:");
            Console.WriteLine("   (MercadoPago only accepts Mastercard - starts with 5)");
            Console.WriteLine(new string('─', 63));

            IPaymentGatewayFactory factory = PaymentGatewayFactoryProvider.GetFactory(GatewayType.MercadoPago);
            var paymentService = new PaymentService(factory);

            // Visa card (starts with 4) - MercadoPago only accepts Mastercard (5)
            bool success = paymentService.ProcessPayment(100.00m, "4234567890123456");

            Console.WriteLine(new string('─', 63));
            Console.WriteLine($"✓ Status: {(success ? "SUCCESS" : "FAILURE (expected)")}");
        }
    }
}
