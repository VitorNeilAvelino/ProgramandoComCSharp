namespace Fintech.Dominio
{
    public class ContaCorrente : Conta
    {
        public ContaCorrente()
        {

        }

        public ContaCorrente(Agencia agencia, int numero, string digitoVerificador) : base(agencia, numero, digitoVerificador)
        {

        }

        public virtual bool EmissaoChequeHabilitada { get; set; }
    }
}