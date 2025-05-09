﻿namespace Questao5.Domain.Entities
{
    public class CurrentAccount : Entity
    {
        public override string Id
        {
            get => IdContaCorrente;
            set => IdContaCorrente = value;
        }

        public string IdContaCorrente { get; private set; }
        public int Numero { get; private set; }
        public string Nome { get; private set; }
        public bool Ativo { get; private set; }

        public CurrentAccount(int numero, string nome, bool ativo)
        {
            Numero = numero;
            Nome = nome;
            Ativo = ativo;
        }

        public CurrentAccount() { }
    }
}
