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
                throw new ArgumentException("Valor deve ser maior que zero", nameof(amount));
            }

            if (string.IsNullOrWhiteSpace(cardNumber))
            {
                throw new ArgumentException("Número do cartão não pode ser vazio", nameof(cardNumber));
            }

            Console.WriteLine($"MercadoPago: Processando R$ {amount:N2}...");

            // Simula processamento e gera ID de transação específico do MercadoPago
            string transactionId = $"MP-{Guid.NewGuid().ToString().Substring(0, 8).ToUpper()}";

            Console.WriteLine($"MercadoPago: Transação processada com sucesso - ID: {transactionId}");

            return transactionId;
        }
    }
}
