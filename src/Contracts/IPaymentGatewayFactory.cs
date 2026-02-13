namespace DesignPatternChallenge.Contracts
{
    public interface IPaymentGatewayFactory
    {
        ICardValidator CreateValidator();
        IPaymentProcessor CreateProcessor();
        ITransactionLogger CreateLogger();
    }
}
