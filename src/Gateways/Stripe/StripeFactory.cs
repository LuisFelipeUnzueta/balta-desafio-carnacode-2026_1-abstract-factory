using DesignPatternChallenge.Contracts;

namespace DesignPatternChallenge.Gateways.Stripe
{
    public class StripeFactory : IPaymentGatewayFactory
    {
        public ICardValidator CreateValidator()
        {
            return new StripeValidator();
        }

        public IPaymentProcessor CreateProcessor()
        {
            return new StripeProcessor();
        }

        public ITransactionLogger CreateLogger()
        {
            return new StripeLogger();
        }
    }
}
