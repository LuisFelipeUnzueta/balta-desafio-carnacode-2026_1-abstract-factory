using System;
using DesignPatternChallenge.Contracts;

namespace DesignPatternChallenge.Gateways.PagSeguro
{
    public class PagSeguroLogger : ITransactionLogger
    {
        public void Log(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                return;
            }

            // Formato específico de log do PagSeguro
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            Console.WriteLine($"[PagSeguro Log] {timestamp}: {message}");
        }
    }
}
