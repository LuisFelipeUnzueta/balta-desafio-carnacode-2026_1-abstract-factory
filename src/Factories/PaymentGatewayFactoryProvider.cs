using System;
using DesignPatternChallenge.Contracts;
using DesignPatternChallenge.Enums;
using DesignPatternChallenge.Gateways.PagSeguro;
using DesignPatternChallenge.Gateways.MercadoPago;
using DesignPatternChallenge.Gateways.Stripe;

namespace DesignPatternChallenge.Factories
{
    public static class PaymentGatewayFactoryProvider
    {
        public static IPaymentGatewayFactory GetFactory(GatewayType gatewayType)
        {
            return gatewayType switch
            {
                GatewayType.PagSeguro => new PagSeguroFactory(),
                GatewayType.MercadoPago => new MercadoPagoFactory(),
                GatewayType.Stripe => new StripeFactory(),
                _ => throw new ArgumentException($"Gateway '{gatewayType}' não é suportado", nameof(gatewayType))
            };
        }

        public static IPaymentGatewayFactory GetFactory(string gatewayName)
        {
            if (string.IsNullOrWhiteSpace(gatewayName))
            {
                throw new ArgumentException("Nome do gateway não pode ser vazio", nameof(gatewayName));
            }

            // Tenta fazer parse para o enum
            if (Enum.TryParse<GatewayType>(gatewayName, true, out var gatewayType))
            {
                return GetFactory(gatewayType);
            }

            throw new ArgumentException($"Gateway '{gatewayName}' não é suportado", nameof(gatewayName));
        }
    }
}
