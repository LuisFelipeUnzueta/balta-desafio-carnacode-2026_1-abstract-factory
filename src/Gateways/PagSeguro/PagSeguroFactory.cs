using DesignPatternChallenge.Contracts;

namespace DesignPatternChallenge.Gateways.PagSeguro
{
    public class PagSeguroFactory : IPaymentGatewayFactory
    {
        public ICardValidator CreateValidator()
        {
            return new PagSeguroValidator();
        }

        public IPaymentProcessor CreateProcessor()
        {
            return new PagSeguroProcessor();
        }

        public ITransactionLogger CreateLogger()
        {
            return new PagSeguroLogger();
        }
    }
}
