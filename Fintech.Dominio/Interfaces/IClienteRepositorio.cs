namespace Fintech.Dominio.Interfaces
{
    public interface IClienteRepositorio : ICrudRepositorio<Cliente>
    {
        Cliente Selecionar(string cpf);
    }
}