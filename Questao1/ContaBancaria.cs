using System;
using System.Globalization;

namespace Questao1
{
    public class ContaBancaria {

        private const double Rate = 3.50;
        
        public int AccountNumber { get; private set; }
        public string Holder { get; private set; }
        public double Balance { get; private set; }

        public ContaBancaria(int accountNumber, string holder, double balance)
        {
            AccountNumber = accountNumber;
            Holder = holder;
            Balance = balance;
        }

        public ContaBancaria(int accountNumber, string holder)
        {
            AccountNumber = accountNumber;
            Holder = holder;
            Balance = 0;
        }

        public void Deposito(double amount)
        {
            if (amount > 0)
            {
                Balance += amount;
            }
            else
            {
                Console.WriteLine("Erro: O valor do depósito deve ser maior que zero(0). Por isso não foi possível fazer o deposito.");
            }
        }

        public void Saque(double amount)
        {
            if (amount > 0)
            {
                Balance -= amount + Rate;
            }
            else
            {
                Console.WriteLine("Erro: O valor do saque deve ser maior que zero(0). Por isso não foi possível fazer o saque.");
            }
        }

        public override string ToString()
        {
            return $"Conta {AccountNumber}, Titular: {Holder}, Saldo: $ {Balance.ToString("F2", CultureInfo.InvariantCulture)}";
        }

    }
}
