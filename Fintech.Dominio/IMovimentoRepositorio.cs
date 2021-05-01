using System;
using System.Collections.Generic;

namespace Fintech.Dominio
{
    public interface IMovimentoRepositorio
    {
        void Inserir(Movimento movimento);
        void Atualizar(Movimento movimento) => throw new InvalidOperationException();
        List<Movimento> Selecionar(int numeroAgencia, int numeroConta);
        void Excluir(int id)
        {
            throw new InvalidOperationException();
        }
    }
}