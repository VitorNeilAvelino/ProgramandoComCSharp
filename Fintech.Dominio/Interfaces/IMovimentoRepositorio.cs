using System.Collections.Generic;

namespace Fintech.Dominio.Interfaces
{
    public interface IMovimentoRepositorio : ICrudRepositorio<Movimento>
    {
        List<Movimento> Selecionar(int numeroAgencia, int numeroConta);
    }
}