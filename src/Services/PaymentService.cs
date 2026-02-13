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
        /// Processa um pagamento usando os componentes do gateway configurado.
        /// 
        /// Fluxo:
        /// 1. Cria componentes através da factory
        /// 2. Valida o cartão
        /// 3. Processa a transação
        /// 4. Registra o resultado
        /// </summary>
        public bool ProcessPayment(decimal amount, string cardNumber)
        {
            // Validação de parâmetros
            if (amount <= 0)
            {
                throw new ArgumentException("Valor deve ser maior que zero", nameof(amount));
            }

            if (string.IsNullOrWhiteSpace(cardNumber))
            {
                throw new ArgumentException("Número do cartão não pode ser vazio", nameof(cardNumber));
            }

            try
            {
                // 1. Usa a factory para criar todos os componentes necessários
                // Garante que todos os componentes sejam da mesma família (compatíveis)
                var validator = _factory.CreateValidator();
                var processor = _factory.CreateProcessor();
                var logger = _factory.CreateLogger();

                // 2. Valida o cartão
                if (!validator.ValidateCard(cardNumber))
                {
                    logger.Log($"Validação falhou para o cartão terminado em {cardNumber.Substring(cardNumber.Length - 4)}");
                    return false;
                }

                // 3. Processa a transação
                string transactionId = processor.ProcessTransaction(amount, cardNumber);

                // 4. Registra o sucesso
                logger.Log($"Transação {transactionId} processada com sucesso - Valor: R$ {amount:N2}");

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERRO] Falha ao processar pagamento: {ex.Message}");
                throw;
            }
        }
    }
}
