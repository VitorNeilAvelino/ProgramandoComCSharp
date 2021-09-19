namespace Fintech.Dominio.Interfaces
{
    public interface ICrudRepositorio<T>
    {
        void Inserir(T cliente);
        void Atualizar(T cliente);
        T Selecionar(int id);
        void Excluir(int id);
    }
}