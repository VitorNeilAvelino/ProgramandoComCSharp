using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fintech.Dominio.Interfaces
{
    public interface ICrudRepositorio<T>
    {
        void Inserir(T cliente);
        void Atualizar(T cliente);
        List<T> Selecionar();
        Task<List<T>> SelecionarAsync();
        T Selecionar(int id);
        List<T> Selecionar(Predicate<T> consulta);            
        void Excluir(int id);
    }
}