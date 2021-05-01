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

        public Movimento EfetuarOperacao(decimal valor, Operacao operacao)
        {
            return EfetuarOperacao(valor, operacao, Limite);
        }
    }
}