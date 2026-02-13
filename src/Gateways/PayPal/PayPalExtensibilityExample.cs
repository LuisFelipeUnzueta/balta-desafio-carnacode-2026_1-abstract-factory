using System;
using DesignPatternChallenge.Contracts;

namespace DesignPatternChallenge.Gateways.PayPal
{
    // ═══════════════════════════════════════════════════════════════
    // PASSO 1: Implementar ICardValidator
    // ═══════════════════════════════════════════════════════════════

    /// <summary>
    /// Validador de cartões específico do PayPal.
    /// </summary>
    public class PayPalValidator : ICardValidator
    {
        public bool ValidateCard(string cardNumber)
        {
            if (string.IsNullOrWhiteSpace(cardNumber))
            {
                Console.WriteLine("PayPal: Número do cartão não pode ser vazio");
                return false;
            }

            Console.WriteLine("PayPal: Validando cartão...");

            // PayPal aceita qualquer cartão de 16 dígitos
            bool isValid = cardNumber.Length == 16;

            if (!isValid)
            {
                Console.WriteLine("PayPal: Cartão deve ter 16 dígitos");
            }

            return isValid;
        }
    }

    // ═══════════════════════════════════════════════════════════════
    // PASSO 2: Implementar IPaymentProcessor
    // ═══════════════════════════════════════════════════════════════

    /// <summary>
    /// Processador de pagamentos específico do PayPal.
    /// </summary>
    public class PayPalProcessor : IPaymentProcessor
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

            Console.WriteLine($"PayPal: Processando ${amount:N2} USD...");

            // Gera ID de transação específico do PayPal
            string transactionId = $"PAYPAL-{Guid.NewGuid().ToString().Substring(0, 10).ToUpper()}";

            Console.WriteLine($"PayPal: Transação processada com sucesso - ID: {transactionId}");

            return transactionId;
        }
    }

    // ═══════════════════════════════════════════════════════════════
    // PASSO 3: Implementar ITransactionLogger
    // ═══════════════════════════════════════════════════════════════

    /// <summary>
    /// Logger específico do PayPal.
    /// </summary>
    public class PayPalLogger : ITransactionLogger
    {
        public void Log(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                return;
            }

            // Formato específico de log do PayPal
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Console.WriteLine($"[PayPal Log] {timestamp} - {message}");
        }
    }

    // ═══════════════════════════════════════════════════════════════
    // PASSO 4: Implementar IPaymentGatewayFactory (Concrete Factory)
    // ═══════════════════════════════════════════════════════════════

    /// <summary>
    /// Factory concreta para o PayPal.
    /// Cria a família completa de componentes do PayPal.
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
// RESULTADO: ZERO MODIFICAÇÕES NECESSÁRIAS EM CÓDIGO EXISTENTE!
// ═════════════════════════════════════════════════════════════════════
//
// Para usar:
// 1. Adicionar PayPal ao enum GatewayType (opcional, pode usar injeção direta)
// 2. Adicionar case no PaymentGatewayFactoryProvider (opcional)
// 3. Ou usar diretamente: new PaymentService(new PayPalFactory())
//
// ═══════════════════════════════════════════════════════════════════════
