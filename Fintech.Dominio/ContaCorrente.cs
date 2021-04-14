namespace Fintech.Dominio
{
    public class ContaCorrente
    {
        // Página 152

        //public int NumeroBanco { get; set; }
        //public string NumeroAgencia { get; set; } // 12345-0
        //public string NumeroConta { get; set; }
        //public decimal Saldo { get; set; }
        //public Cliente Cliente { get; set; }

        public Agencia Agencia { get; set; }
        public int Numero { get; set; }
        public string DigitoVerificador { get; set; }
        public decimal Saldo { get; set; }
        public Cliente Cliente { get; set; }

        public void EfetuarOperacao(decimal valor, Operacao operacao)
        {
            switch (operacao)
            {
                case Operacao.Deposito:
                    Saldo += valor;
                    break;
                case Operacao.Saque:
                    Saldo -= valor;
                    break;
            }
        }
    }
}