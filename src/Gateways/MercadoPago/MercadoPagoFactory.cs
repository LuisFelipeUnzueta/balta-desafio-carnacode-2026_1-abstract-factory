using DesignPatternChallenge.Contracts;

namespace DesignPatternChallenge.Gateways.MercadoPago
{
    public class MercadoPagoFactory : IPaymentGatewayFactory
    {
        public ICardValidator CreateValidator()
        {
            return new MercadoPagoValidator();
        }

        public IPaymentProcessor CreateProcessor()
        {
            return new MercadoPagoProcessor();
        }

        public ITransactionLogger CreateLogger()
        {
            return new MercadoPagoLogger();
        }
    }
}
