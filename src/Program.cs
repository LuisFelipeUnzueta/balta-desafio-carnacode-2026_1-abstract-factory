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
            Console.WriteLine("║     SISTEMA DE PAGAMENTOS - ABSTRACT FACTORY PATTERN        ║");
            Console.WriteLine("║              CarnaCode 2026 - Desafio 01                     ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════════╝");
            Console.WriteLine();

            // ═══════════════════════════════════════════════════════════════
            // EXEMPLO 1: Usando o Factory Provider (abordagem recomendada)
            // ═══════════════════════════════════════════════════════════════
            Console.WriteLine("═══════════════════════════════════════════════════════════════");
            Console.WriteLine("  EXEMPLO 1: Usando Factory Provider com Enum (Type-Safe)");
            Console.WriteLine("═══════════════════════════════════════════════════════════════");
            Console.WriteLine();

            DemonstratePayment(GatewayType.PagSeguro, 150.00m, "1234567890123456");
            Console.WriteLine();

            DemonstratePayment(GatewayType.MercadoPago, 200.00m, "5234567890123456");
            Console.WriteLine();

            DemonstratePayment(GatewayType.Stripe, 350.00m, "4234567890123456");
            Console.WriteLine();

            // ═══════════════════════════════════════════════════════════════
            // EXEMPLO 2: Injeção Direta de Factory (útil para DI Containers)
            // ═══════════════════════════════════════════════════════════════
            Console.WriteLine("═══════════════════════════════════════════════════════════════");
            Console.WriteLine("  EXEMPLO 2: Injeção Direta de Factory (DI Container Style)");
            Console.WriteLine("═══════════════════════════════════════════════════════════════");
            Console.WriteLine();

            DemonstrateDirectInjection();
            Console.WriteLine();

            // ═══════════════════════════════════════════════════════════════
            // EXEMPLO 3: Tratamento de Erros - Validação Falhando
            // ═══════════════════════════════════════════════════════════════
            Console.WriteLine("═══════════════════════════════════════════════════════════════");
            Console.WriteLine("  EXEMPLO 3: Demonstração de Validação com Falha");
            Console.WriteLine("═══════════════════════════════════════════════════════════════");
            Console.WriteLine();

            DemonstrateValidationFailure();
            Console.WriteLine();

            // ═══════════════════════════════════════════════════════════════
            // EXEMPLO 4: Facilidade de Extensão - Como adicionar novo gateway
            // ═══════════════════════════════════════════════════════════════
            Console.WriteLine("═══════════════════════════════════════════════════════════════");
            Console.WriteLine("  EXTENSIBILIDADE: Para adicionar um novo gateway (ex: PayPal)");
            Console.WriteLine("═══════════════════════════════════════════════════════════════");
            Console.WriteLine();
            Console.WriteLine("  1. Criar PayPalValidator : ICardValidator");
            Console.WriteLine("  2. Criar PayPalProcessor : IPaymentProcessor");
            Console.WriteLine("  3. Criar PayPalLogger : ITransactionLogger");
            Console.WriteLine("  4. Criar PayPalFactory : IPaymentGatewayFactory");
            Console.WriteLine("  5. Adicionar PayPal ao enum GatewayType");
            Console.WriteLine("  6. Adicionar case no PaymentGatewayFactoryProvider");
            Console.WriteLine();
            Console.WriteLine("  ✅ NENHUMA MODIFICAÇÃO necessária em PaymentService!");
            Console.WriteLine("  ✅ Código existente continua funcionando!");
            Console.WriteLine();
        }

        /// <summary>
        /// Demonstra processamento de pagamento usando o Factory Provider.
        /// Esta é a abordagem recomendada para uso em produção.
        /// </summary>
        static void DemonstratePayment(GatewayType gatewayType, decimal amount, string cardNumber)
        {
            Console.WriteLine($"→ Processando com {gatewayType}:");
            Console.WriteLine(new string('─', 63));

            // Obtém a factory apropriada através do provider
            IPaymentGatewayFactory factory = PaymentGatewayFactoryProvider.GetFactory(gatewayType);

            // Injeta a factory no serviço
            var paymentService = new PaymentService(factory);

            // Processa o pagamento
            bool success = paymentService.ProcessPayment(amount, cardNumber);

            Console.WriteLine(new string('─', 63));
            Console.WriteLine($"✓ Status: {(success ? "SUCESSO" : "FALHA")}");
        }

        /// <summary>
        /// Demonstra injeção direta de factory.
        /// Útil quando se usa DI Containers como Microsoft.Extensions.DependencyInjection
        /// </summary>
        static void DemonstrateDirectInjection()
        {
            Console.WriteLine("→ Demonstração de Injeção Direta:");
            Console.WriteLine(new string('─', 63));

            // Em um DI Container real, você registraria assim:
            // services.AddScoped<IPaymentGatewayFactory, PagSeguroFactory>();
            // services.AddScoped<PaymentService>();

            // Aqui simulamos a injeção manual
            IPaymentGatewayFactory factory = new StripeFactory();
            var paymentService = new PaymentService(factory);

            bool success = paymentService.ProcessPayment(99.90m, "4111111111111111");

            Console.WriteLine(new string('─', 63));
            Console.WriteLine($"✓ Status: {(success ? "SUCESSO" : "FALHA")}");
        }

        /// <summary>
        /// Demonstra como o sistema lida com validações que falham.
        /// </summary>
        static void DemonstrateValidationFailure()
        {
            Console.WriteLine("→ Tentando processar com cartão Visa no MercadoPago:");
            Console.WriteLine("   (MercadoPago aceita apenas Mastercard - começa com 5)");
            Console.WriteLine(new string('─', 63));

            IPaymentGatewayFactory factory = PaymentGatewayFactoryProvider.GetFactory(GatewayType.MercadoPago);
            var paymentService = new PaymentService(factory);

            // Cartão Visa (começa com 4) - MercadoPago só aceita Mastercard (5)
            bool success = paymentService.ProcessPayment(100.00m, "4234567890123456");

            Console.WriteLine(new string('─', 63));
            Console.WriteLine($"✓ Status: {(success ? "SUCESSO" : "FALHA (esperado)")}");
        }
    }
}
