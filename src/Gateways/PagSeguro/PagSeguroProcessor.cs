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
                throw new ArgumentException("Valor deve ser maior que zero", nameof(amount));
            }

            if (string.IsNullOrWhiteSpace(cardNumber))
            {
                throw new ArgumentException("Número do cartão não pode ser vazio", nameof(cardNumber));
            }

            Console.WriteLine($"PagSeguro: Processando R$ {amount:N2}...");

            // Simula processamento e gera ID de transação específico do PagSeguro
            string transactionId = $"PAGSEG-{Guid.NewGuid().ToString().Substring(0, 8).ToUpper()}";

            Console.WriteLine($"PagSeguro: Transação processada com sucesso - ID: {transactionId}");

            return transactionId;
        }
    }
}
