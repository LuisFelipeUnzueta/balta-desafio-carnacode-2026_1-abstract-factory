namespace DesignPatternChallenge.Contracts
{
    public interface IPaymentProcessor
    {
        string ProcessTransaction(decimal amount, string cardNumber);
    }
}
