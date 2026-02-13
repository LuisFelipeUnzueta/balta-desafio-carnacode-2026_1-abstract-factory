using System;
using DesignPatternChallenge.Contracts;

namespace DesignPatternChallenge.Gateways.MercadoPago
{
    public class MercadoPagoLogger : ITransactionLogger
    {
        public void Log(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                return;
            }

            // Formato específico de log do MercadoPago
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            Console.WriteLine($"[MercadoPago Log] {timestamp}: {message}");
        }
    }
}
