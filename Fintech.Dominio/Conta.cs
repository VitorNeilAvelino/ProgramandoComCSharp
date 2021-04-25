namespace Fintech.Dominio
{
    public abstract class Conta
    {
        public Agencia Agencia { get; set; }
        public int Numero { get; set; }
        public string DigitoVerificador { get; set; }
        public decimal Saldo { get; protected set; }
        public Cliente Cliente { get; set; }

        public virtual void EfetuarOperacao(decimal valor, Operacao operacao)
        {
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
                    break;
            }
        }
    }
}