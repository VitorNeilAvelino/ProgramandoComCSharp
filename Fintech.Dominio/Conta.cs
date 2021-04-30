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

        public virtual Movimento EfetuarOperacao(decimal valor, Operacao operacao)
        {
            var sucesso = true;
            Movimento movimento = null;

            switch (operacao)
            {
                case Operacao.Deposito:
                    Saldo += valor;
                    break;
                case Operacao.Saque:
                    if (Saldo >= valor)
                    {
                        Saldo -= valor;
                    }
                    else
                    {
                        sucesso = false;
                    }
                    break;
            }

            if (sucesso)
            {
                movimento = new Movimento(operacao, valor);

                AdicionarMovimento(movimento);
            }

            return movimento;
        }

        protected void AdicionarMovimento(Movimento movimento)
        {
            movimento.Conta = this;

            Movimentos.Add(movimento);
        }
    }
}