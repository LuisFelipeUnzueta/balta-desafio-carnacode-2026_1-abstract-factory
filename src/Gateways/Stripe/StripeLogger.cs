using System;
using DesignPatternChallenge.Contracts;

namespace DesignPatternChallenge.Gateways.Stripe
{
    public class StripeLogger : ITransactionLogger
    {
        public void Log(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                return;
            }

            // Formato específico de log do Stripe
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            Console.WriteLine($"[Stripe Log] {timestamp}: {message}");
        }
    }
}
