using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
//using ApiTrans.Data.PaypalService;

    
namespace ApiTrans.Services.ContextBilleterasVirtuales
{
    class Context
    {
        private IStrategy _strategy;

        public Context(IStrategy strategy)
        {
            this._strategy = strategy;
        }
        public void SetStrategy(IStrategy strategy)
        {
            this._strategy = strategy;
        }
        public string PagoBilletera(int id)
        {
            Console.WriteLine("Context: Sorting data using the strategy (not sure how it'll do it)");
            string xd = this._strategy.Pago(id);
            return xd;
            //Console.WriteLine("termina pago de billetera");
        }
    }
    public interface IStrategy
    {
        string Pago(int data);
    }
    class PaypalStrategy : IStrategy
    {
        public string Pago(int data)
        {
            return $"{data} estrategia Paypal";
        }
    }}