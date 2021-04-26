using System;

namespace Fintech.Dominio
{
    public class ContaEspecial : ContaCorrente
    {
        public ContaEspecial()
        {

        }

        public ContaEspecial(Agencia agencia, int numero, string digitoVerificador, decimal limite) : base(agencia, numero, digitoVerificador)
        {
            Limite = limite;
        }

        public decimal Limite { get; set; }
        public override bool EmissaoChequeHabilitada { get; set; } = true;

        public override void EfetuarOperacao(decimal valor, Operacao operacao)
        {
            switch (operacao)
            {
                case Operacao.Deposito:
                    Saldo += valor;
                    AdicionarMovimento(new Movimento(operacao, valor));
                    break;
                case Operacao.Saque:
                    if (Saldo + Limite >= valor)
                    {
                        Saldo -= valor;
                        AdicionarMovimento(new Movimento(operacao, valor));
                    }
                    break;
            }

        }
    }
}