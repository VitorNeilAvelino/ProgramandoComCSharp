using System.Collections.Generic;

namespace Fintech.Dominio
{
    public abstract class Conta
    {
        public Conta()
        {

        }

        protected Conta(Agencia agencia, int numero, string digitoVerificador)
        {
            Agencia = agencia;
            Numero = numero;
            DigitoVerificador = digitoVerificador;
        }

        public Agencia Agencia { get; set; }
        public int Numero { get; set; }
        public string DigitoVerificador { get; set; }
        public decimal Saldo { get; protected set; }
        public Cliente Cliente { get; set; }
        public List<Movimento> Movimentos { get; private set; } = new List<Movimento>();

        public virtual void EfetuarOperacao(decimal valor, Operacao operacao)
        {
            switch (operacao)
            {
                case Operacao.Deposito:
                    Saldo += valor;
                    AdicionarMovimento(new Movimento(operacao, valor));
                    break;
                case Operacao.Saque:
                    if (Saldo >= valor)
                    {
                        Saldo -= valor;
                        AdicionarMovimento(new Movimento(operacao, valor));
                    }
                    break;
            }

            AdicionarMovimento(new Movimento(operacao, valor));
        }

        protected void AdicionarMovimento(Movimento movimento)
        {
            Movimentos.Add(movimento);
        }
    }
}