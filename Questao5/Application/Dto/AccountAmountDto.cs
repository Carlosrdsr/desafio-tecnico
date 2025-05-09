﻿namespace Questao5.Application.Dto
{
    public class AccountAmountDto
    {
        public int Numero { get; private set; }
        public string Nome { get; private set; }
        public DateTime DataConsulta { get; private set; }
        public decimal Saldo { get; private set; }

        public AccountAmountDto(int numero, string nome, DateTime dataConsulta, decimal saldo)
        {
            Numero = numero;
            Nome = nome;
            DataConsulta = dataConsulta;
            Saldo = saldo;
        }
    }
}
