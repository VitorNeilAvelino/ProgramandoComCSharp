using System.Collections.Generic;
using System.Linq;

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
        public decimal Saldo
        {
            get
            {
                return TotalDepositos - TotalSaques;
            }
            protected set { }
        }

        public Cliente Cliente { get; set; }
        public List<Movimento> Movimentos { get; set; } = new List<Movimento>();
        public decimal TotalDepositos
        {
            get
            {
                return Movimentos.Where(m => m.Operacao == Operacao.Deposito).Sum(m => m.Valor);
            }
        }

        public decimal TotalSaques => Movimentos.Where(m => m.Operacao == Operacao.Saque).Sum(m => m.Valor);

        public Movimento EfetuarOperacao(decimal valor, Operacao operacao, decimal limite = 0)
        {
            //var sucesso = true;
            Movimento movimento = null;

            switch (operacao)
            {
                case Operacao.Deposito:
                    Saldo += valor;
                    break;
                case Operacao.Saque:
                    if (Saldo + limite >= valor)
                    {
                        Saldo -= valor;
                    }
                    else
                    {
                        //sucesso = false;
                        throw new SaldoInsuficienteException("Saldo insuficiente.");
                    }
                    break;
            }

            //if (sucesso)
            //{
                movimento = new Movimento(operacao, valor);

                AdicionarMovimento(movimento);
            //}

            return movimento;
        }

        protected void AdicionarMovimento(Movimento movimento)
        {
            movimento.Conta = this;

            Movimentos.Add(movimento);
        }
    }
}