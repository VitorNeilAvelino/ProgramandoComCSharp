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

        public override Movimento EfetuarOperacao(decimal valor, Operacao operacao)
        {
            var sucesso = true;
            Movimento movimento = null;

            switch (operacao)
            {
                case Operacao.Deposito:
                    Saldo += valor;
                    break;
                case Operacao.Saque:
                    if (Saldo + Limite >= valor)
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
    }
}