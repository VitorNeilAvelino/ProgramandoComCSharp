namespace Fintech.Dominio
{
    public class Endereco
    {
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Cidade { get; set; }
        public string Cep { get; set; }

        public override string ToString()
        {
            return $"{Logradouro}, {Numero} - {Cidade} {Cep}";
        }
    }
}