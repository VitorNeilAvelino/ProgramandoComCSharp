namespace Fintech.Dominio
{
    public class ContaCorrente : Conta
    {
        public virtual bool EmissaoChequeHabilitada { get; set; }
    }
}