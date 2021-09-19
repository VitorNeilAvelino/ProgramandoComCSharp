using Microsoft.VisualStudio.TestTools.UnitTesting;
using Fintech.Dominio;

namespace Fintech.Repositorios.SqlServer.Tests
{
    [TestClass()]
    public class MovimentoRepositorioTests
    {
        private readonly MovimentoRepositorio repositorio = new("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Fintech;Integrated Security=True");

        [TestMethod()]
        public void InserirTest()
        {
            var movimento = new Movimento(Operacao.Deposito, 180);
            movimento.Conta = new ContaCorrente(new Agencia { Numero = 123 }, 456, "5");

            repositorio.Inserir(movimento);
        }

        [TestMethod()]
        public void SelecionarTest()
        {
            var movimentos = repositorio.SelecionarAsync(1, 456).Result;

            Assert.IsTrue(movimentos.Count > 0);
        }
    }
}